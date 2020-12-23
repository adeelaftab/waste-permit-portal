using HFZMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace HFZMVC
{
  public class AppUtil
  {

    public static string DecryptPassword(string cipherText) {

      //string  encryptionDecryptionKey = "Beeah@1234";
      string encryptionDecryptionKey = "";
      byte[] cipherBytes = Convert.FromBase64String(cipherText);

      using (Aes aesEncryptor = Aes.Create()) {
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionDecryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        aesEncryptor.Key = pdb.GetBytes(32);
        aesEncryptor.IV = pdb.GetBytes(16);

        using (MemoryStream ms = new MemoryStream()) {
          using (CryptoStream cs = new CryptoStream(ms, aesEncryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
          }

          cipherText = Encoding.Unicode.GetString(ms.ToArray());
        }
      }

      return cipherText;

    }

    public static string Encrypt(string clearText) {

      //string  encryptionDecryptionKey = "Beeah@1234";
      string encryptionDecryptionKey = "";

      byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

      using (Aes aesEncryptor = Aes.Create()) {
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionDecryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        aesEncryptor.Key = pdb.GetBytes(32);
        aesEncryptor.IV = pdb.GetBytes(16);

        using (MemoryStream ms = new MemoryStream()) {
          using (CryptoStream cs = new CryptoStream(ms, aesEncryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
          }

          clearText = Convert.ToBase64String(ms.ToArray());
        }
      }

      return clearText;
    }

    //This method will check For valid user session 
    // Also used to Assign userId
    public static int checkLogin() {
      if (HttpContext.Current == null) {
        return 0;
      }
      HttpSessionState Session = HttpContext.Current.Session;
      var UserID = 0;
      if (Session[AppVariables.SessionUserId] != null)
        int.TryParse(Session[AppVariables.SessionUserId].ToString(), out UserID);
      return UserID;

    }
    public static int getUserRole() {
      HttpSessionState Session = HttpContext.Current.Session;
      var roleID = 0;
      if (Session[AppVariables.SessionUserId] != null)
        int.TryParse(Session[AppVariables.SessionUserRole].ToString(), out roleID);
      return roleID;

    }
    public static string getUserName() {
      HttpSessionState Session = HttpContext.Current.Session;
      var userName = "";
      if (Session[AppVariables.SessionUsername] != null) {
        userName = Session[AppVariables.SessionUsername].ToString();
      }
      return userName;

    }
    public static int getEntity() {
      return 1;

    }

    public async static Task<bool> checkIsPermission(string permissionName, WasteManageEntities db = null) {
      WasteManageEntities _db = db == null ? new WasteManageEntities() : db;
      bool response = false;
      var permissionEntityQuery = await _db.eMenu_Master.Where(e => e.strPageName == permissionName).ToListAsync();
      var permissionEntity = permissionEntityQuery.FirstOrDefault();
      if (permissionEntity != null) {
        int userrole = AppUtil.getUserRole();
        var roleMappingQuery = await _db.RoleMenuMappings.Where(e => e.MenuID == permissionEntity.nMenuID
        && e.RoleID == userrole
        ).ToListAsync();
        var roleMapping = roleMappingQuery.FirstOrDefault();
        if (roleMapping != null) {
          response = roleMapping.IsView.HasValue ? roleMapping.IsView.Value : false;
        }


      }

      if (db == null)
        _db.Dispose();    //destroy the db object 
      return response;
    }

    public static int GetUserIdByRole(int role, WasteManageEntities db = null) {
      WasteManageEntities _db = db == null ? new WasteManageEntities() : db;
     var userMapping= db.UserRoleMappings.Where(e => e.RoleID == role).FirstOrDefault();
      if (userMapping != null) {
        return userMapping.UserID;
      } else {
        return 0;
      }

    }
    public static void LogItems(string toLog) {


    }

    public static void ExceptionLog(Exception ex) {
      try {

        using (WasteManageEntities _db = new WasteManageEntities()) {
          var logobject = new AppLog();
          logobject
            .CreatedOn = DateTime.Now;
          if (HttpContext.Current.Session[AppVariables.SessionUserId] != null) {
            logobject.Session = "UserId:" + HttpContext.Current.Session[AppVariables.SessionUserId].ToString();
          }
          if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null) {
            logobject.UserIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
          }
          if (HttpContext.Current.Request.Browser != null) {
            logobject.Browser = HttpContext.Current.Request.Browser.Browser;
          }
          StringBuilder errorMessage = new StringBuilder();
          do {
            errorMessage.Append("Exceptin Type: " + ex.GetType().Name +Environment.NewLine);
            errorMessage.Append("Message: " + ex.Message +Environment.NewLine+Environment.NewLine);
            errorMessage.Append("Stack Trace: " + ex.StackTrace + Environment.NewLine);
            ex = ex.InnerException;



          } while (ex != null);
          logobject.Logmessage = errorMessage.ToString();
          _db.AppLogs.Add(logobject);
          _db.SaveChanges();


        }
      } catch (Exception exception) {

       //unhandled
      }
      
    }

  }
}