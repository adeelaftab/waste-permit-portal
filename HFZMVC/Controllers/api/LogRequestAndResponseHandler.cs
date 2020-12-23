using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HFZMVC.Controllers.api
{
  public class LogRequestAndResponseHandler : DelegatingHandler
  {
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken) {


      // log request body
      string requestBody = await request.Content.ReadAsStringAsync();

      // let other handlers process the request
      // DateTime Before = DateTime.Now;
      var result = await base.SendAsync(request, cancellationToken);
      //DateTime After = DateTime.Now;
      //try
      //{
      //  //Get the FileName to Write the Log file Request and Response
      //  String LogFileName = GetLogFile(request.RequestUri.LocalPath, "AllRequest");
      //  LogToFile(LogFileName, $"Request:\r\n{request.Method.Method} {request.RequestUri.PathAndQuery}\r\n");
      //  LogToFile(LogFileName, $"Start Time: {Before} End Time: {After} TimeTaken = {After-Before}\r\n");
      //  //Trace.WriteLine(responseBody);
      //}
      //catch
      //{

      //}
      if (result.Content != null) {
        // once response body is ready, log it
        var responseBody = await result.Content.ReadAsStringAsync();
        //result.Content.Headers.ContentLength = responseBody.Length;
        //result.Headers.Add("Content-Length", responseBody.Length.ToString());
        if (result.StatusCode != System.Net.HttpStatusCode.OK) {
          //Do not generate error when saving to the file
          try {
            //Get the FileName to Write the Log file Request and Response
            String LogFileName = GetLogFile(request.RequestUri.LocalPath, result.StatusCode.ToString());
            LogToFile(LogFileName, $"Request:\r\n{request.Method.Method} {request.RequestUri.PathAndQuery}\r\n");
            LogToFile(LogFileName, $"Request Header:\r\n{request.Headers}\r\n");
            LogToFile(LogFileName, $"Request Body:\r\n{requestBody}\r\n");
            LogToFile(LogFileName, $"\r\n\r\nResponse:\r\n");
            LogToFile(LogFileName, responseBody);
            //Trace.WriteLine(responseBody);
          } catch {

          }
        }
        //if (result.StatusCode != System.Net.HttpStatusCode.OK)

      }

      return result;
    }

    private String GetLogFile(String LocalPath, String StatusCode) {
      List<String> LogPath = new List<String> {
        System.Web.Hosting.HostingEnvironment.MapPath("~/APIRequestLog")
      };
      foreach (var path in LocalPath.Split('/')) {
        if (path.Length == 32)
          break;
        LogPath.Add(path);
      }
      LogPath.Add(StatusCode);

      String LogFilePath = System.IO.Path.Combine(LogPath.ToArray());
      if (!System.IO.Directory.Exists(LogFilePath))
        System.IO.Directory.CreateDirectory(LogFilePath);

      return System.IO.Path.Combine(LogFilePath, DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".log");
    }

    private void LogToFile(String FileName, String Content) {
      System.IO.File.AppendAllText(FileName, Content);
    }
  }
}