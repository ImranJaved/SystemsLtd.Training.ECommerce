using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SystemsLtd.Training.ECommerce.API.Middleware
{
    /// <summary>
    /// HeaderValueMiddleware is used to parse header values before requested controller has been invoked.
    /// </summary>
    public class HeaderValueMiddleware
    {
        #region Constructor
        /// <summary>
        /// HeaderValueMiddleware initializes class object.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public HeaderValueMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<HeaderValueMiddleware> logger) 
        {
            this.Next = next;
            this.Configuration = configuration;
            this.Logger = logger;
        }
        #endregion

        #region Properties and Data Members
        private const string ApplicationKeyName = "x-application-key";
        private const string AcceptLangauageKey = "Accept-Language";
        private readonly RequestDelegate Next;
        private readonly IConfiguration Configuration;
        private readonly ILogger<HeaderValueMiddleware> Logger;
        #endregion

        #region Methods
        /// <summary>
        /// Invoke method is called when middleware has been called.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            //try
            {
                if (context != null && context.Request != null && context.Request.Headers != null)
                {
                    var applicationKey = this.GetHeaderValue(context.Request.Headers, HeaderValueMiddleware.ApplicationKeyName);
                    var acceptLangauage = this.GetHeaderValue(context.Request.Headers, HeaderValueMiddleware.AcceptLangauageKey);
                }
            }
            //catch
            //{
            //    //TODO: only acceptable if header values can be optional.
            //}

            await this.Next(context);
        }

        /// <summary>
        /// GetHeaderValue returns the value of header item by provided key.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetHeaderValue(IHeaderDictionary headers, string key)
        {
            //var dataAfter2Index = headers[key].ToString().Substring(2);
            return headers[key];
        }
        #endregion
    }
}