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

        public const string CreateEmployee = "Proc_InsertEmployee";
        public const string GetEmployeeById = "Proc_GetEmployeeById";
        public const string FilterEmployee = "Proc_FilterEmployee";
        public const string UpdateEmployee = "Proc_UpdateEmployee";
        public const string DeleteEmployee = "Proc_DeleteEmployeeById";


        public const string CreateDepartment = "Proc_InsertDepartment";
        public const string UpdateDepartment = "Proc_UpdateDepartment";
        public const string FilterDepartment = "Proc_FilterDepartment";
        public const string DeleteDepartment = "Proc_DeleteDepartmentById";


        public static string GetProcedureNameByEntityClassName(string entityClassName)
        {
            switch (entityClassName)
            {
                case "DepartmentCreate":
                    return CreateDepartment;
                case "DepartmentUpdate":
                    return UpdateDepartment;
                case "FilteredList<Department>":
                    return FilterDepartment;
                case "DepartmentDelete":
                    return DeleteDepartment;

                case "Employee":
                    return GetEmployeeById;
                case "EmployeeUpdate":
                    return UpdateEmployee;
                case "FilteredList<Employee>":
                    return FilterEmployee;
                case "EmployeeDelete":
                    return DeleteEmployee;

                default: throw new Exception("Không tìm thấy tên Stored Procedure");
            }

        }
       
    }

}
