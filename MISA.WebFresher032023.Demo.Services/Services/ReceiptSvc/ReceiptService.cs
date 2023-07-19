using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.ReceiptRepo;
using OfficeOpenXml.Style;
using OfficeOpenXml;
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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);

                // Validate dto

                // Tạo mới receipt
                var receiptInput = _mapper.Map<ReceiptInput>(receiptInputDto);
                var newReceiptId = Guid.NewGuid();
                receiptInput.ReceiptId = newReceiptId;
                receiptInput.CreatedDate = DateTime.Now.ToLocalTime();

                await _baseRepository.CreateAsync(receiptInput);    

                

                // Tạo mới các receipt detail
                foreach (var receiptDetailInputDto in receiptInputDto.ReceiptDetailList)
                {
                   await CreateReceiptDetailAsync(newReceiptId, receiptDetailInputDto);
                }

                // Commit
                await _unitOfWork.CommitAsync(mKey);
                return newReceiptId;
            }
            finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }

        }

        public override async Task<ReceiptDto?> GetAsync(Guid id)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);

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
                receiptDto.ReceiptDetailList = receiptDetailListDto;

                return receiptDto;
            }
            catch
            {
                throw;
            }
            finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }

        }

        public override async Task<bool> UpdateAsync(Guid id, ReceiptInputDto receiptInputDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);

                // Validate dto

                // Update các receipt detail
                foreach (var receiptDetailInputDto in receiptInputDto.ReceiptDetailList)
                {
                    // enum
                    if (receiptDetailInputDto.Status == "delete")
                        await DeleteReceiptDetailAsync(receiptDetailInputDto.ReceiptDetailId);
                    else if (receiptDetailInputDto.Status == "create")
                        await CreateReceiptDetailAsync(id, receiptDetailInputDto);
                    else if (receiptDetailInputDto.Status == "update")
                        await UpdateReceiptDetailAsync(receiptDetailInputDto);

                }

                // Update Receipt
                var receiptInput = _mapper.Map<ReceiptInput>(receiptInputDto);
                receiptInput.ReceiptId = id;

                var updateSuccess = await _baseRepository.UpdateAsync(receiptInput);
                // Commit
                await _unitOfWork.CommitAsync(mKey);
                return updateSuccess;
            }
            finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        public async Task CreateReceiptDetailAsync(Guid receiptId, ReceiptDetailInputDto receiptDetailInputDto)
        {
            var receipDetailInput = _mapper.Map<ReceiptDetailInput>(receiptDetailInputDto);
            var rdNewId = Guid.NewGuid();
            receipDetailInput.ReceiptDetailId = rdNewId;
            receipDetailInput.ReceiptId = receiptId;
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

        public async Task<long> GetTotalReceive(string keySearch)
        {
            return await _receiptRepository.GetTotalReceive(keySearch);
        }
    }
}
