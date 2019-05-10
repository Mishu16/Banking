using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingWebsite.Utilities
{
    public class TransactionsFilter : ActionFilterAttribute
    {
            
            }

    public class TransactionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}