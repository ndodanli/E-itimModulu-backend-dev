using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DataAccess
{
    public interface IDataContext : IDisposable
    {
        IAuditHelper AuditHelper { get; }
    }
    public class AuditHelper : IAuditHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUser()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null && httpContext.Items["Account"] != null)
            {
                Object accountName = httpContext.Items["Account"].GetType().GetProperty("Username").GetValue(httpContext.Items["Account"], null);
                if (accountName != null)
                    return accountName.ToString();
            }
            return null;
        }
    }

    public interface IAuditHelper
    {
        string GetCurrentUser();
    }
}