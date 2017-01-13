using RRD.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDataModel
{
    public class LeaveBalance
    {
        [ColumnMapping("LEAVE_MASTER_NAME")]
        public string LeaveType { get; set; }
        //[ColumnMapping("LEAVE_MASTER_CODE")]
        //public string LeaveCode { get; set; }
        [ColumnMapping("BalanceCarryForward")]
        public double BalanceCarryForward { get; set; }
        [ColumnMapping("LEAVE_BALANCE_OPENING")]
        public double BalanceAvailable { get; set; }
        [ColumnMapping("LEAVE_BALANCE_TAKEN")]
        public double BalanceTaken { get; set; }
        [ColumnMapping("LEAVE_BALANCE_TOTAL")]
        public double BalanceTotal { get; set; }
    }
}
