using Client.Entities;
using ScreenHandler.Settings;

namespace Client.Extensions;

internal static class FormExtensions
{
    public static Customer AsCustomer(this Form form)
    {
        var customer = new Customer()
        {
            FirstName = form.Sections.First(s => s.Id == "FirstName").Input.Answer!,
            LastName = form.Sections.First(s => s.Id == "LastName").Input.Answer!,
            Age = int.Parse(form.Sections.First(s => s.Id == "Age").Input.Answer!),
            EmailAddress = form.Sections.FirstOrDefault(s => s.Id == "EmailAddress")?.Input.Answer
        };

        if (string.IsNullOrWhiteSpace(customer.EmailAddress))
            customer.EmailAddress = null;

        return customer;
    }
}
