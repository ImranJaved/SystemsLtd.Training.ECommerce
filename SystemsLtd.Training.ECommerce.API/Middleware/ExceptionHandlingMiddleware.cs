using System.Net;
using System.Text.Json;

namespace SystemsLtd.Training.ECommerce.API.Middleware
{
    /// <summary>
    /// HeaderValueMiddleware is used to parse header values before requested controller has been invoked.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        #region Constructor
        /// <summary>
        /// ErrorHandlingMiddleware initializes class object.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<ExceptionHandlingMiddleware> logger)
        {
            Next = next;
            Configuration = configuration;
            Logger = logger;
        }
        #endregion

        #region Properties and Data Members

        public const string JsonContentType = "application/json";

        public RequestDelegate Next { get; }
        public IConfiguration Configuration { get; }
        public ILogger<ExceptionHandlingMiddleware> Logger { get; }
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
            try
            {
                await this.Next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// HandleExceptionAsync creates response in case of exception.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is EntryPointNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (exception is AccessViolationException)
            {
                code = HttpStatusCode.Forbidden;
            }
            else if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            //else if (exception is DatabaseException)
            //{
            //    code = HttpStatusCode.BadRequest;
            //}

            var result = JsonSerializer.Serialize(new { error = exception.Message });

            context.Response.ContentType = ExceptionHandlingMiddleware.JsonContentType;
            context.Response.StatusCode = (int)code;

            this.Logger.LogError(exception, exception.Message);

            return context.Response.WriteAsync(result);
        }
        #endregion
    }
}