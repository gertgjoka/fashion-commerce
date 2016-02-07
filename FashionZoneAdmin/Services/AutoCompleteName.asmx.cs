using System.Web.Services;
using FashionZone.BL;

namespace FashionZone.Admin.Services
{
    /// <summary>
    /// Summary description for AutoCompleteName
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteName : WebService
    {
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetNameList(string prefixText, int count)
        {
            return ApplicationContext.Current.Customers.GetFullNameWithDateReg(prefixText).ToArray();
        }
    }
}
