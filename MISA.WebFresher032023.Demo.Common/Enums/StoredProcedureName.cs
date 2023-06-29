﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher032023.Demo.Common.Enum
{
    public static class StoredProcedureName
    {
        #region Employee Stored procedure name
        public const string CreateEmployee = "Proc_InsertEmployee";
        public const string GetEmployeeById = "Proc_GetEmployeeById";
        public const string FilterEmployee = "Proc_FilterEmployee";
        public const string UpdateEmployee = "Proc_UpdateEmployee";
        public const string DeleteEmployee = "Proc_DeleteEmployeeById";
        public const string CheckDepartmentCodeExist = "Proc_CheckDepartmentCodeExist";
        public const string DeleteMultipleEmployee = "Proc_DeleteMultipleEmployee";
        #endregion

        #region Department Stored procedure name
        public const string GetDepartmentById = "Proc_GetDepartmentById";
        public const string CreateDepartment = "Proc_InsertDepartment";
        public const string UpdateDepartment = "Proc_UpdateDepartment";
        public const string FilterDepartment = "Proc_FilterDepartment";
        public const string DeleteDepartment = "Proc_DeleteDepartmentById";
        public const string CheckEmployeeCodeExist = "Proc_CheckEmployeeCodeExist";
        public const string DeleteMultipleDepartment = "Proc_DeleteMultipleDepartment";
        #endregion

        public const string CreateCustomer = "Proc_InsertCustomer";
        public const string GetCustomerById = "Proc_GetCustomerById";
        public const string FilterCustomer = "Proc_FilterCustomer";
        public const string CheckCustomerCodeExist = "Proc_CheckCustomerCodeExist";
        public const string DeleteCustomer = "Proc_DeleteCustomerById";
        public const string DeleteMultipleCustomer = "Proc_DeleteMultipleCustomer";

        public const string FilterGroup = "Proc_FilterGroup";

        /// <summary>
        /// Lấy Stored procedure từ string tạo bởi EntityClassName và kiểu hành động
        /// </summary>
        /// <param name="entityClassName"></param>
        /// <returns>Tên stored procedured</returns>
        /// <exception cref="Exception"></exception>
        public static string GetProcedureNameByEntityClassName(string entityClassName)
        {
            switch (entityClassName)
            {
                #region Department proc
                case "Department":
                    return GetDepartmentById;
                case "DepartmentCreate":
                    return CreateDepartment;
                case "DepartmentUpdate":
                    return UpdateDepartment;
                case "FilteredList<Department>":
                    return FilterDepartment;
                case "DepartmentDelete":
                    return DeleteDepartment;
                case "DepartmentCheckCodeExist":
                    return CheckDepartmentCodeExist;
                case "DepartmentDeleteMultiple":
                    return DeleteMultipleDepartment;
                #endregion

                #region Employee proc
                case "Employee":
                    return GetEmployeeById;
                case "EmployeeCreate":
                    return CreateEmployee;
                case "EmployeeUpdate":
                    return UpdateEmployee;
                case "FilteredList<Employee>":
                    return FilterEmployee;
                case "EmployeeDelete":
                    return DeleteEmployee;
                case "EmployeeCheckCodeExist":
                    return CheckEmployeeCodeExist;
                case "EmployeeDeleteMultiple":
                    return DeleteMultipleEmployee;
                #endregion

                #region Customer proc
                case "Customer":
                    return GetCustomerById;
                case "CustomerCreate":
                    return CreateCustomer;
                case "FilteredList<Customer>":
                    return FilterCustomer;
                case "CustomerCheckCodeExist":
                    return CheckCustomerCodeExist;
                case "CustomerDelete":
                    return DeleteCustomer;
                case "CustomerDeleteMultiple":
                    return DeleteMultipleCustomer;
                #endregion

                #region Group proc
                case "FilteredList<Group>":
                    return FilterGroup;
                #endregion
                default: throw new Exception("Không tìm thấy tên Stored Procedure");
            }

        }
       
    }

}
