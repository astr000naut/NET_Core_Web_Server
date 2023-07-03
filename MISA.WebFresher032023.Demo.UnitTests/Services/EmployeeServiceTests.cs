﻿using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Castle.Core.Configuration;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using AutoMapper;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.Common.Enums;
using NSubstitute.ExceptionExtensions;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;

namespace MISA.WebFresher032023.Demo.UnitTests.Services
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        // Chuẩn bị EmployeeInputDto
        // employeeRepository
        // mapper

        // Th1 - Department ID not valid
        // Th2 - Department ID valid - Code Exist
        // Th3 - Department ID valid - Code Valid - ISCreated true
        // Th4 - Department ID valid - Code Valid - IS Created false
        // Th5 - Department ID valid - Code Valid - Throw DbException

        // Th1 - Department ID not valid
        [Test]
        public async Task CreateAsync_DepartmentIDNotValid_ReturnException()
        {
            // Arrange
            var EmployeeInputDto = new EmployeeInputDto()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.ValidateDepartmentId(EmployeeInputDto.DepartmentId).Returns(false);
            var mapper = Substitute.For<IMapper>();

            var employeeService = new EmployeeService(employeeRepository, mapper);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ConflictException>(async () => await employeeService.CreateAsync(EmployeeInputDto));
            Assert.That(ex.Message, Is.EqualTo(Error.InvalidDepartmentIdMsg));
        }

        // Th2 - Department ID valid - Code Exist
        [Test]
        public async Task CreateAsync_DepartmentIdValid_CodeExist_ReturnException()
        {
            // Arrange
            var EmployeeInputDto = new EmployeeInputDto()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.ValidateDepartmentId(EmployeeInputDto.DepartmentId).Returns(true);
            employeeRepository.CheckCodeExistAsync(null, EmployeeInputDto.EmployeeCode).Returns(true);

            var mapper = Substitute.For<IMapper>();

            var employeeService = new EmployeeService(employeeRepository, mapper);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ConflictException>(async () => await employeeService.CreateAsync(EmployeeInputDto));
            Assert.That(ex.Message, Is.EqualTo(Error.EmployeeCodeHasExistMsg));
        }

        // Th3 - Department ID valid - Code Valid - ReturnGuid
        [Test]
        public async Task CreateAsync_AllValid_ReturnGuid()
        {
            // Arrange
            var EmployeeInputDto = new EmployeeInputDto()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeInput = new EmployeeInput()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.ValidateDepartmentId(EmployeeInputDto.DepartmentId).Returns(true);
            employeeRepository.CheckCodeExistAsync(null, EmployeeInputDto.EmployeeCode).Returns(false);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<EmployeeInput>(EmployeeInputDto).Returns(employeeInput);

            employeeRepository.CreateAsync(employeeInput).Returns(true);

            var employeeService = new EmployeeService(employeeRepository, mapper);

            // Act 
            var guid = await employeeService.CreateAsync(EmployeeInputDto);

            // Assert
            // Assert.That(guid, Is.EqualTo(employeeInput.EmployeeId));
        }

        // Th4 - Department ID valid - Code Valid - ReturnNull
        [Test]
        public async Task CreateAsync_AllValid_ReturnNull()
        {
            // Arrange
            var EmployeeInputDto = new EmployeeInputDto()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeInput = new EmployeeInput()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.ValidateDepartmentId(EmployeeInputDto.DepartmentId).Returns(true);
            employeeRepository.CheckCodeExistAsync(null, EmployeeInputDto.EmployeeCode).Returns(false);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<EmployeeInput>(EmployeeInputDto).Returns(employeeInput);

            employeeRepository.CreateAsync(employeeInput).Returns(false);

            var employeeService = new EmployeeService(employeeRepository, mapper);

            // Act 
            var guid = await employeeService.CreateAsync(EmployeeInputDto);

            // Assert
            Assert.That(guid, Is.EqualTo(null));
        }


        // Th5 - Department ID valid - Code Valid - Throw DbException
        [Test]
        public async Task CreateAsync_AllValid_ThrowException()
        {
            // Arrange
            var EmployeeInputDto = new EmployeeInputDto()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeInput = new EmployeeInput()
            {
                EmployeeCode = "NV-9876",
                EmployeeFullName = "Tran Quang Vinh",
                DepartmentId = Guid.Parse("424e77a1-dd49-4bd4-ac0c-6ee95342c676")
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.ValidateDepartmentId(EmployeeInputDto.DepartmentId).Returns(true);
            employeeRepository.CheckCodeExistAsync(null, EmployeeInputDto.EmployeeCode).Returns(false);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<EmployeeInput>(EmployeeInputDto).Returns(employeeInput);

            employeeRepository.CreateAsync(employeeInput).Returns<Task<bool>>(x => { throw new DbException(Error.DbQueryFail, Error.DbQueryFailMsg, Error.DbQueryFailMsg); });


            var employeeService = new EmployeeService(employeeRepository, mapper);

            // Act 
            var ex = Assert.ThrowsAsync<DbException>(async () => await employeeService.CreateAsync(EmployeeInputDto));
            Assert.That(ex.Message, Is.EqualTo(Error.DbQueryFailMsg));
        }
    }
}
