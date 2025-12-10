using System.ComponentModel;

namespace CleanArquitecture.Application.Abstractions.Enums
{
    public enum UserClaim
    {
        [Description("http://schemas.microsoft.com/identity/claims/objectidentifier")]
        Id,

        [Description("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")]
        FirstName,

        [Description("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")]
        LastName,

        [Description("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")]
        Email,
    }
};