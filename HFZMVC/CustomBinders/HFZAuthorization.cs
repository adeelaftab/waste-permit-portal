using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HFZMVC.CustomBinders
{
  public class HFZAuthorization:AuthorizeAttribute
  {
    public override void OnAuthorization(AuthorizationContext filterContext) {
      var checkSession = filterContext.RequestContext.HttpContext.Session[AppVariables.SessionUsername];
      if (checkSession==null) {
        RedirectUser(filterContext);
      } else {

      }

    }


    public void RedirectUser(AuthorizationContext actionContext) {
      actionContext.Result = new RedirectToRouteResult(
                                         new RouteValueDictionary {
                                                { "action", "Login" },
                                                { "controller", "User" } });


    }
  }
}