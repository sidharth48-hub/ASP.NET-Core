
using System.Text.RegularExpressions;

namespace RoutingExample.CustomConstraints
{
    public class MonthCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
            {
                return false;
            }

            Regex regex = new Regex("^(apr|jul|oct|jan)$");

            string? month = Convert.ToString(values[routeKey]);

            if (regex.IsMatch(month))
            {
                return true;
            }
            return false;
        }
    }
}
