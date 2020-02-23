using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataGenies.Core.Models;
using DataGenies.Core.Scanners;
using Microsoft.AspNetCore.Http;

namespace DataGenies.Core.Middlewares.Responders
{
    public class ApplicationTypesResponder : IDataGeniesMiddlewareResponder
    {
        private readonly IApplicationTemplatesScanner applicationTemplatesScanner;
        private readonly DataGeniesOptions options;

        public ApplicationTypesResponder(
            DataGeniesOptions options,
            IApplicationTemplatesScanner applicationTemplatesScanner)
        {
            this.options = options;
            this.applicationTemplatesScanner = applicationTemplatesScanner;
        }

        public bool CanExecute(string httpMethod, string path)
        {
            return httpMethod == "GET" && Regex.IsMatch(path, $"^/{this.options.RoutePrefix}/api/application-types");
        }

        public async Task Respond(HttpContext httpContext, string path)
        {
            var response = httpContext.Response;

            response.ContentType = "application/json;charset=utf-8";

            var applicationTypes = this.applicationTemplatesScanner.ScanTemplates();
            var responseData = JsonSerializer.Serialize(applicationTypes);

            await response.WriteAsync(responseData, Encoding.UTF8);
        }
    }
}