using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRD.Core;
using RRD.DataAccess;
using BotDataModel;

namespace BotDataAccess.Implementation
{
  public  interface IMyLeaveDataAccess
    {
         List<LeaveBalance> GetLeaveBalanceByEmpId(string EmpID, int LeaveYear);
    }
}
