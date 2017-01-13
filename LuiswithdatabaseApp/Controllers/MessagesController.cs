using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using BotDataAccess.Implementation;
using BotDataAccess.Modules;
using BotDataModel;
using System.Collections.Generic;

namespace LuiswithdatabaseApp
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        IMyLeaveDataAccess myleavedataaccess = new LeaveDataAccess(ConfigurationManager.ConnectionStrings["botconnection"].ConnectionString);
  
       
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            try
            {


                if (activity.Type.ToLower() == ("Message").ToLower())
                {
                    //StateClient sc = activity.GetStateClient();

                    //BotData userData = sc.BotState.GetPrivateConversationData(
                    //    activity.ChannelId, activity.Conversation.Id, activity.From.Id);
                    //var ISvalid = userData.GetProperty<bool>("login");
                    //var userid = userData.GetProperty<string>("userid");
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                    //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //userName = userName.Replace("\\", "/");
                    //String rrdid = userName.Split('/')[1].ToString();
                    //string domain = userName.Split('/')[0].ToString();
                    String Output = string.Empty;
                    List<LeaveBalance> balanacelist = new List<LeaveBalance>();
                    balanacelist = IsLogin("20284");
                    LeaveBalance leavebalanace = balanacelist[0] as LeaveBalance;
                    Output = "You have Available " + leavebalanace.LeaveType;

                    //if (!ISvalid)
                    //{
                    //    userData.SetProperty("userid", rrdid);
                    //    userData.SetProperty("login", true);

                    //}
                    //else
                    //{
                    //    Luisdialog luisoutput = await GetFromLUIS(activity.Text);

                    //    if (luisoutput.intents != null && luisoutput.intents.Count() > 0)
                    //    {
                    //        switch (luisoutput.intents[0].intent.ToLower())
                    //        {
                    //            case "Leave balance":
                    //                List<LeaveBalance> balanacelist = new List<LeaveBalance>();
                    //                balanacelist = IsLogin(rrdid);
                    //                LeaveBalance leavebalanace = balanacelist[0] as LeaveBalance;
                    //                Output = "You have Available " + leavebalanace.BalanceAvailable + "\n" + leavebalanace;
                    //                break;
                    //            case "Leave status":
                    //                Output = "totally you have 5 Jobs";
                    //                break;
                    //            case "Greet":
                    //                Output = "How can i help you?";
                    //                break;
                    //            default:
                    //                Output = "Sorry, I am not getting you...";
                    //                break;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Output = "Sorry, I am not getting you...";
                    //    }
                    //}
                    Activity reply = activity.CreateReply(Output);
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else
                {
                    HandleSystemMessage(activity);
                }
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch(Exception excp)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                return response;
            }
        }
        public List<LeaveBalance> IsLogin(String rrdid)
        {
            List<LeaveBalance> balanacelist = new List<LeaveBalance>();
            LeaveBalance bala = new LeaveBalance();
            try
            {


               
                balanacelist = myleavedataaccess.GetLeaveBalanceByEmpId("20284", 2016);
                //DataTable dt = getemployeedata(rrdid) as DataTable;
                //if (dt.Rows.Count > 0)
                //{
                //    Output = "Welcome Thiru ! How can i help you? ";
                //}
                //else
                //{
                //    Output = "You dont have access to chat. please contact administrator";
                //}
            }
            catch(Exception excp)
            {
                bala.LeaveType = excp.ToString();
                balanacelist.Add(bala);
            }
            return balanacelist;
        }
      
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {

            }

            return null;
        }

        //private static async Task<Luisdialog> GetFromLUIS(string Query)
        //{
        //    Query = Uri.EscapeDataString(Query);
        //    Luisdialog Data = new Luisdialog();
        //    using (HttpClient client = new HttpClient())
        //    {
                
        //        string RequestURI = "https://api.projectoxford.ai/luis/v2.0/apps/a892e469-5ecc-4569-959b-f8099209e052?subscription-key=0b12dc283dc3455095ad77aea7b49a20&q=" + Query + "&verbose=true";

        //        HttpResponseMessage msg = await client.GetAsync(RequestURI);

        //        if (msg.IsSuccessStatusCode)
        //        {
        //            var JsonDataResponse = await msg.Content.ReadAsStringAsync();
        //            Data = JsonConvert.DeserializeObject<Luisdialog>(JsonDataResponse);
        //        }
        //    }
        //    return Data;
        //}

    }
}