using System;
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
        public const string UpdateCustomer = "Proc_UpdateCustomer";
        public const string GetCustomerById = "Proc_GetCustomerById";
        public const string FilterCustomer = "Proc_FilterCustomer";
        public const string CheckCustomerCodeExist = "Proc_CheckCustomerCodeExist";
        public const string DeleteCustomer = "Proc_DeleteCustomerById";
        public const string DeleteMultipleCustomer = "Proc_DeleteMultipleCustomer";

        #region Group Stored Procedure name
        public const string FilterGroup = "Proc_FilterGroup";
        #endregion

        #region Account Stored Procedure name
        public const string CreateAccount = "Proc_InsertAccount";
        public const string UpdateAccount = "Proc_UpdateAccount";
        public const string GetAccountById = "Proc_GetAccountById";
        public const string FilterAccount = "Proc_FilterAccount";
        public const string DeleteAccount = "Proc_DeleteAccountById";
        public const string CheckAccountCodeExist = "Proc_CheckAccountCodeExist";
        #endregion


        #region Receipt Stored Procedure name
        public const string CreateReceipt = "Proc_InsertReceipt";
        public const string GetReceiptById = "Proc_GetReceiptById";
        public const string FilterReceipt = "Proc_FilterReceipt";
        public const string UpdateReceipt = "Proc_UpdateReceipt";
        public const string DeleteReceipt = "Proc_DeleteReceiptById";
        public const string DeleteMultipleReceipt = "Proc_DeleteMultipleReceipt";

        #endregion
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

                #region Account proc
                case "Account":
                    return GetAccountById;
                case "AccountCreate":
                    return CreateAccount;
                case "FilteredList<Account>":
                    return FilterAccount;
                case "AccountDelete":
                    return DeleteAccount;
                case "AccountCheckCodeExist":
                    return CheckAccountCodeExist;
                #endregion

                #region Receipt proc
                case "Receipt":
                    return GetReceiptById;
                case "ReceiptCreate":
                    return CreateReceipt;
                case "ReceiptUpdate":
                    return UpdateReceipt;
                case "FilteredList<Receipt>":
                    return FilterReceipt;
                case "ReceiptDelete":
                    return DeleteReceipt;
                case "ReceiptDeleteMultiple":
                    return DeleteMultipleReceipt;
                #endregion
                default: throw new Exception("Không tìm thấy tên Stored Procedure");
            }

        }
       
    }

}
