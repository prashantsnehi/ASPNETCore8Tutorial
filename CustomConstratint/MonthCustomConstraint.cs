using System;
using System.Text.RegularExpressions;

namespace ControllerExamples.CustomConstratint
{
    public class MonthCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey)) return false;

            string? month = Convert.ToString(values[routeKey]);

            Regex regex = new Regex("^(apr|jul|oct|jan)$");
            if (regex.IsMatch(month)) return true;

            return false;
        }
    }
}

