using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.IntegrationUI.Response
{
    public interface IOwnerResponse
    {
        Guid Id { get; set; }
        string UserName { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string AdditionalData { get; set; }
    }
}
