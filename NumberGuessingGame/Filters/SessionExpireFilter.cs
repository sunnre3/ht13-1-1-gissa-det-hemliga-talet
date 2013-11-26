using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Web
{
	public class SessionExpireFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var ctx = HttpContext.Current;

			if (ctx.Session != null)
			{
				// Check if a new session ID was generated,
				// e.g a new session has been created.
				if (ctx.Session.IsNewSession)
				{
					// If the user has a cookie eventhough the session is new
					// that means that a session has expired.
					var cookie = ctx.Request.Headers["Cookie"];
					if (cookie != null && cookie.IndexOf("ASP.NET_SessionId") >= 0)
					{

					}
				}
			}

			base.OnActionExecuting(filterContext);
		}
	}
}