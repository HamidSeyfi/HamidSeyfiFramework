using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.BaseSystemModel.Helper
{
    public enum LogType
    {
        Menu = 10,
        InputUser = 20,
        OutputUser = 30,
        Exception = 40,
        LogFilterActionExecuting = 100,
        LogFilterResultExecuted = 110,

    }


    public enum Operation
    {
        Create = 1,
        Edit = 2,
        Details = 3,
        Delete = 4
    }

    public enum Culture
    {
        Fa = 1,
        En = 2
    }



}
