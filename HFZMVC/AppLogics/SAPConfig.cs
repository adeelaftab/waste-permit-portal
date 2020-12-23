using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.Finance;
using HFZMVC.Models.UserManagement;
using Newtonsoft.Json;
using SAP.Middleware.Connector;
namespace HFZMVC.AppLogics
{
  public class SAPConfig : IDestinationConfiguration
  {

    readonly string AppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
    readonly string SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
    readonly string SystemID = ConfigurationManager.AppSettings["SystemID"];
    readonly string User = ConfigurationManager.AppSettings["User"];
    readonly string Password = ConfigurationManager.AppSettings["Password"];
    readonly string Client = ConfigurationManager.AppSettings["Client"];
    readonly string Language = ConfigurationManager.AppSettings["Language"];
    readonly string PoolSize = ConfigurationManager.AppSettings["PoolSize"];
    readonly string SAPRouter = ConfigurationManager.AppSettings["SAPRouter"];
    public bool ChangeEventsSupported() {
      return true;
    }
    public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

    public RfcConfigParameters GetParameters(string destionationName) {
      RfcConfigParameters parms = new RfcConfigParameters();
      //SAP Parameters
      if (destionationName.Equals("mySAPdestination")) {
       

        parms.Add(RfcConfigParameters.AppServerHost, AppServerHost);
        parms.Add(RfcConfigParameters.SystemNumber, SystemNumber);
        parms.Add(RfcConfigParameters.SystemID, SystemID);
        parms.Add(RfcConfigParameters.User, User);
        parms.Add(RfcConfigParameters.Password, Password);
        parms.Add(RfcConfigParameters.Client, Client);
        parms.Add(RfcConfigParameters.Language, Language);
        parms.Add(RfcConfigParameters.PoolSize, PoolSize);
        parms.Add(RfcConfigParameters.SAPRouter, SAPRouter);
        //

        //parms.Add(RfcConfigParameters.AppServerHost, "10.20.28.5");
        //parms.Add(RfcConfigParameters.SystemNumber, "00");
        //parms.Add(RfcConfigParameters.SystemID, "BP1");
        //parms.Add(RfcConfigParameters.User, "WEBSERVICE");
        //parms.Add(RfcConfigParameters.Password, "Web1@Service2");
        //parms.Add(RfcConfigParameters.Client, "500");
        //parms.Add(RfcConfigParameters.Language, "EN");
        //parms.Add(RfcConfigParameters.PoolSize, "5");
        //parms.Add(RfcConfigParameters.SAPRouter, "/H/51.144.179.153");

      }
      return parms;
    }
  }
  public class CallinSAPI
  {
    internal static void CallingSAP() {
      RfcDestination dest = null;
      try {
        dest = RfcDestinationManager.GetDestination("mySAPdestination");

        RfcRepository repo = dest.Repository;

        IRfcFunction func = repo.CreateFunction("ZSD_HFZ_CR_SALESORDER");

        // Send Data With RFC Structure  
        IRfcStructure impStruct = func.GetStructure("I_DATA");
        impStruct.SetValue("PONO", "Adeel11");
        impStruct.SetValue("CUSTOMER", "0000107523");
        impStruct.SetValue("PRODUCT_NAME", "000000007000000003");
        impStruct.SetValue("ORDER_QTY", 1); //Numeric
        impStruct.SetValue("NOTES", "Test case");
        impStruct.SetValue("KBETR", 400.23); //Total Amount
        impStruct.SetValue("TOTAL", 400.23);
        impStruct.SetValue("PERMIT_FEES", 264.83);
        impStruct.SetValue("SERVICE_FEES", 20);
        impStruct.SetValue("RD_FEES", 60);
        impStruct.SetValue("VAT", 23.12);

        func.SetValue("I_DATA", impStruct);
        func.Invoke(dest); //EXPORT PARAMETER CUSTOMER

        // Get customer Number
        var result = func[0].ToString();
        string[] res = result.Split('=');
        string CustomerNo = res[1];
        //Get response
        var response = func[1].ToString();
        string[] resp = response.Split(new string[] { "=STRUCTURE BAPIRETURN1 " }, StringSplitOptions.None);
        response = resp[1];
        var responseContentArray = GetContent(response);
        //Get datasent
        var datasent = func[2].ToString();         // First item
        string[] datas = datasent.Split(new string[] { "=STRUCTURE ZSD_HFZ_CUSTOMER " }, StringSplitOptions.None);
        datasent = datas[1];
        var datasentContentArray = GetContent(datasent);
        RfcSessionManager.EndContext(dest);
      } catch (Exception ex) {
        RfcSessionManager.EndContext(dest);
        throw ex;
      }
    }
    internal static string GenerateOrder(SAPOrderData InvoiceData) {
      RfcDestination dest = null;
      try {
        dest = RfcDestinationManager.GetDestination("mySAPdestination");

        RfcRepository repo = dest.Repository;

        IRfcFunction func = repo.CreateFunction("ZSD_HFZ_CR_SALESORDER");

        // Send Data With RFC Structure  
        IRfcStructure impStruct = func.GetStructure("I_DATA");
        impStruct.SetValue("PONO", InvoiceData.PermitID.ToString());
        impStruct.SetValue("CUSTOMER", InvoiceData.SAPCustomerID);
        impStruct.SetValue("PRODUCT_NAME", "00000000" + InvoiceData.ProductCode);//"000000007000000003"
        impStruct.SetValue("ORDER_QTY", InvoiceData.OrderQty); //Numeric
        impStruct.SetValue("NOTES", "Test case");
        impStruct.SetValue("KBETR", (decimal)InvoiceData.PermitFees); //Total Amount
        impStruct.SetValue("TOTAL", (decimal)InvoiceData.Total);
        impStruct.SetValue("PERMIT_FEES", (decimal)InvoiceData.PermitFees);
        impStruct.SetValue("SERVICE_FEES", (decimal)InvoiceData.ServiceFees);
        impStruct.SetValue("RD_FEES", (decimal)InvoiceData.RDFees);
        impStruct.SetValue("VAT", (decimal)InvoiceData.VAT);

        func.SetValue("I_DATA", impStruct);
        func.Invoke(dest); //EXPORT PARAMETER CUSTOMER

        // Get customer Number
        var result = func[0].ToString();
        RfcSessionManager.EndContext(dest);
        string[] res = result.Split('=');
        string orderNo = res[1];

        if (!string.IsNullOrEmpty(orderNo)) {
          return orderNo;
        } else {
          return null;
        }


      } catch (Exception ex) {
        RfcSessionManager.EndContext(dest);
        throw;
      }
    }
    public static string AddCustomer(RegisterViewModel CustomerData) {
      RfcDestination dest = null;
      try {
        dest = RfcDestinationManager.GetDestination("mySAPdestination");

        RfcRepository repo = dest.Repository;

        IRfcFunction func = repo.CreateFunction("ZSD_HFZ_CR_CUSTOMER");

        // Send Data With RFC Structure  
        IRfcStructure impStruct = func.GetStructure("I_CUSTOMER");
        impStruct.SetValue("CUSTOMER_NAME", CustomerData.Name);
        impStruct.SetValue("CITY", "Sharjah");
        impStruct.SetValue("LOCATION", "Sharjah");
        impStruct.SetValue("STREET", CustomerData.Address);
        impStruct.SetValue("STREET_NO", CustomerData.Address);
        impStruct.SetValue("POSTL_COD1", "49133");
        impStruct.SetValue("PO_BOX", "49133");
        impStruct.SetValue("TEL1_NUMBR", CustomerData.PhoneNumber);
        impStruct.SetValue("E_MAIL", CustomerData.EmailID);
        impStruct.SetValue("CP_FNAME", CustomerData.CompanyName);
        impStruct.SetValue("CP_SNAME", CustomerData.CompanyName);
        impStruct.SetValue("CP_PHONE", CustomerData.PhoneNumber);
        impStruct.SetValue("STCEG", CustomerData.VatlicenseNo);
        impStruct.SetValue("ENTITY", "1");
        func.SetValue("I_CUSTOMER", impStruct);
        func.Invoke(dest); //EXPORT PARAMETER CUSTOMER

        // Get customer Number
        var result = func[0].ToString();
        string[] res = result.Split('=');
        string CustomerNo = res[1];
        //Get response
        var response = func[1].ToString();
        string[] resp = response.Split(new string[] { "=STRUCTURE BAPIRETURN1 " }, StringSplitOptions.None);
        response = resp[1];
        var responseContentArray = GetContent(response);
        //Get datasent
        var datasent = func[2].ToString();         // First item
        string[] datas = datasent.Split(new string[] { "=STRUCTURE ZSD_HFZ_CUSTOMER " }, StringSplitOptions.None);
        datasent = datas[1];
        var datasentContentArray = GetContent(datasent);
        RfcSessionManager.EndContext(dest);
        if (!string.IsNullOrEmpty(CustomerNo)) {
          return CustomerNo;
        } else {
          return null;
        }
      } catch (Exception ex) {
        RfcSessionManager.EndContext(dest);
        throw ex;
      }
    }

    public static System.Threading.Tasks.Task<string> AddHazardCertificate(Models.PermitRequest.HazardSAPModel HazardData) {
      RfcDestination dest = null;
      return System.Threading.Tasks.Task.Run(() => {

        try {
          dest = RfcDestinationManager.GetDestination("mySAPdestination");

          RfcRepository repo = dest.Repository;

          IRfcFunction func = repo.CreateFunction("ZWDC_APP_CR");

          // Send Data With RFC Structure  

          IRfcStructure impStruct = func.GetStructure("I_APP");
          impStruct.SetValue("WDC_APPNO", HazardData.WDC_APPNO);
          impStruct.SetValue("WDC_APPDT", HazardData.WDC_APPDT);
          impStruct.SetValue("KUNNR", HazardData.KUNNR);
          impStruct.SetValue("WDC_NAME", HazardData.WDC_NAME);
          impStruct.SetValue("WDC_MOBILE", HazardData.WDC_MOBILE);
          impStruct.SetValue("WDC_EMAIL", HazardData.WDC_EMAIL);
          impStruct.SetValue("WDC_VALIDIY", HazardData.WDC_VALIDIY);
          impStruct.SetValue("MSDS", Convert.FromBase64String(HazardData.MSDS));
          impStruct.SetValue("PICTUE", Convert.FromBase64String(HazardData.PICTUE));
          impStruct.SetValue("APPLICATION", Convert.FromBase64String(HazardData.APPLICATION));
          //impStruct.SetValue("MSDS", raw);
          //impStruct.SetValue("PICTUE", raw);
          //impStruct.SetValue("APPLICATION", raw);

          func.SetValue("I_APP", impStruct);
          func.Invoke(dest); //EXPORT PARAMETER CUSTOMER

          // Get customer Number
          var result = func[1].ToString();
          string[] res = result.Split('=');
          string AppCustomerNo = res[1];
          RfcSessionManager.EndContext(dest);
          return AppCustomerNo;

        } catch (Exception ex) {
          RfcSessionManager.EndContext(dest);
          throw ex;
        }
      });

    }
    internal static Array GetContent(string response) {
      response = response.Replace(" }", "");
      response = response.Replace("{ ", "");
      string[] res = response.Split(new string[] { "FIELD" }, StringSplitOptions.None);
      string[] result = new string[res.Length];
      int i = 0;
      foreach (var item in res) {
        string[] itemres = item.Split('=');
        string json = JsonConvert.SerializeObject(itemres);
        result[i] = json;
      }
      return result;
    }

  }
}