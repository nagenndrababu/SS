using BotDataAccess.Constants;
using BotDataAccess.Implementation;
using BotDataModel;
using RRD.DataAccess;
using RRD.DataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDataAccess.Modules
{
    public sealed class LeaveDataAccess : BaseDataAccess, IMyLeaveDataAccess
    {
        #region Variable Declaration
        IDataLayer leaveDataAccess = null;
        #endregion

        #region Constructor
        public LeaveDataAccess(string sConnectionString)
        {
            leaveDataAccess = new GenericDataLayer(sConnectionString);
        }
        #endregion
        public List<LeaveBalance> GetLeaveBalanceByEmpId(string EmpID, int LeaveYear)
        {
            List<LeaveBalance> lstLeaveBalance = new List<LeaveBalance>();
            IDictionary<string, IDbDataParameter> dicParameter = new Dictionary<string, IDbDataParameter>();
            DataSet dsResults = new DataSet();
            try
            {                           
                
                
                    dicParameter = leaveDataAccess.SetParams(new DbType[] { DbType.String,DbType.String,DbType.String , DbType.Int32 },
                    new object[] { EmpID,"", EmpID, LeaveYear }, new string[] { "Employee", "LeaveType", "Userid", "Year" });
#if DEBUG
                   
#endif
                    dsResults = leaveDataAccess.ExecuteQuery(StoredProcedure.LeaveBalanceByEmpId, dicParameter);
#if DEBUG
                 
#endif
                    lstLeaveBalance = dsResults.Tables[0].Rows.ToTypeList<LeaveBalance>();
                
            }
            catch (Exception exLeaveBalanceByEmpId)
            {
                LeaveBalance bal = new LeaveBalance();
                bal.LeaveType = exLeaveBalanceByEmpId.ToString();
                lstLeaveBalance.Add(bal);               
            }
            finally
            {
                dsResults.Clear();
                dsResults.Dispose();
                dicParameter.Clear();
            }
            return lstLeaveBalance;
        }
    }
 }
