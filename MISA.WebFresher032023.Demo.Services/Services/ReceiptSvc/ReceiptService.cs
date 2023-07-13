using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.ReceiptRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.ReceiptSvc
{
    public class ReceiptService:BaseService<Receipt, ReceiptDto, ReceiptInput, ReceiptInputDto>, IReceiptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptService(IReceiptRepository receiptRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(receiptRepository, mapper, unitOfWork)
        {
            _receiptRepository = receiptRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<Guid?> CreateAsync(ReceiptInputDto receiptInputDto)
        {
            Guid uKey = Guid.NewGuid();
            try
            {
                _unitOfWork.setManipulationKey(uKey);
                await _unitOfWork.OpenAsync(uKey);
                await _unitOfWork.BeginAsync(uKey);

                // Validate dto

                // Tạo mới receipt
                var receiptInput = _mapper.Map<ReceiptInput>(receiptInputDto);
                var newReceiptId = Guid.NewGuid();
                receiptInput.receiptId = newReceiptId;
                receiptInput.CreatedDate = DateTime.Now.ToLocalTime();

                await _baseRepository.CreateAsync(receiptInput);    

                

                // Tạo mới các receipt detail
                foreach (var receiptDetailInputDto in receiptInputDto.receiptDetailList)
                {
                   await CreateReceiptDetailAsync(newReceiptId, receiptDetailInputDto);
                }

                // Commit
                await _unitOfWork.CommitAsync(uKey);
                return newReceiptId;
            }
            finally
            {
                await _unitOfWork.DisposeAsync(uKey);
                await _unitOfWork.CloseAsync(uKey);
            }

        }

        public override async Task<ReceiptDto?> GetAsync(Guid id)
        {
            Guid uKey = Guid.NewGuid();
            try
            {
                _unitOfWork.setManipulationKey(uKey);
                await _unitOfWork.OpenAsync(uKey);

                // Lấy receipt
                var receipt = await _baseRepository.GetAsync(id);
                var receiptDto = _mapper.Map<ReceiptDto>(receipt);

                // Lấy receipt detail
                var receiptDetailList = await _receiptRepository.GetReceiptDetailListAsync(id);
                var receiptDetailListDto = new List<ReceiptDetailDto>();
                foreach (var receiptDetail in receiptDetailList)
                {
                    var receiptDetailDto = _mapper.Map<ReceiptDetailDto>(receiptDetail);
                    receiptDetailListDto.Add(receiptDetailDto);
                }
                receiptDto.receiptDetailList = receiptDetailListDto;

                return receiptDto;
            }
            catch
            {
                throw;
            }
            finally
            {
                await _unitOfWork.CloseAsync(uKey);
            }

        }

        public override async Task<bool> UpdateAsync(Guid id, ReceiptInputDto receiptInputDto)
        {
            Guid uKey = Guid.NewGuid();
            try
            {
                _unitOfWork.setManipulationKey(uKey);
                await _unitOfWork.OpenAsync(uKey);
                await _unitOfWork.BeginAsync(uKey);

                // Validate dto

                // Update các receipt detail
                foreach (var receiptDetailInputDto in receiptInputDto.receiptDetailList)
                {
                    if (receiptDetailInputDto.status == "delete")
                        await DeleteReceiptDetailAsync(receiptDetailInputDto.receiptDetailId);
                    else if (receiptDetailInputDto.status == "create")
                        await CreateReceiptDetailAsync(id, receiptDetailInputDto);
                    else if (receiptDetailInputDto.status == "update")
                        await UpdateReceiptDetailAsync(receiptDetailInputDto);

                }

                // Update Receipt
                var receiptInput = _mapper.Map<ReceiptInput>(receiptInputDto);
                receiptInput.receiptId = id;

                var updateSuccess = await _baseRepository.UpdateAsync(receiptInput);
                // Commit
                await _unitOfWork.CommitAsync(uKey);
                return updateSuccess;
            }
            finally
            {
                await _unitOfWork.DisposeAsync(uKey);
                await _unitOfWork.CloseAsync(uKey);
            }
        }

        public async Task CreateReceiptDetailAsync(Guid receiptId, ReceiptDetailInputDto receiptDetailInputDto)
        {
            var receipDetailInput = _mapper.Map<ReceiptDetailInput>(receiptDetailInputDto);
            var rdNewId = Guid.NewGuid();
            receipDetailInput.receiptDetailId = rdNewId;
            receipDetailInput.receiptId = receiptId;
            receipDetailInput.CreatedDate = DateTime.Now.ToLocalTime();

            await _receiptRepository.InsertReceiptDetailAsync(receipDetailInput);
        }

        public async Task UpdateReceiptDetailAsync(ReceiptDetailInputDto receiptDetailInputDto)
        {
            var receipDetailInput = _mapper.Map<ReceiptDetailInput>(receiptDetailInputDto);
            await _receiptRepository.UpdateReceiptDetailAsync(receipDetailInput);
        }

        public async Task DeleteReceiptDetailAsync(Guid? id)
        {
            await _receiptRepository.DeleteReceiptDetailAsync(id);
        }

        public async Task<string> GetNewReceiptNoAsync()
        {
            return await _receiptRepository.GetNewReceiptNoAsync();
        }
    }
}
