using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Boilerplate.WebApi.Infrastructure.Routing
{
    public class AlphaNumericConstraint : IRouteConstraint
    {
        private static readonly TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Match
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <param name="route">IRouter</param>
        /// <param name="routeKey">String</param>
        /// <param name="values">RouteValueDictionary</param>
        /// <param name="routeDirection">RouteDirection</param>
        /// <returns></returns>
        public bool Match(HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            //validate input params  
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (route == null)
                throw new ArgumentNullException(nameof(route));

            if (routeKey == null)
                throw new ArgumentNullException(nameof(routeKey));

            if (values == null)
                throw new ArgumentNullException(nameof(values));


            if (values.TryGetValue(routeKey, out object routeValue))
            {
                var parameterValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
                return new Regex(@"^[a-zA-Z0-9]*$",
                                RegexOptions.CultureInvariant
                                | RegexOptions.IgnoreCase, RegexMatchTimeout).IsMatch(parameterValueString);
            }

            return false;
        }
    }
}
