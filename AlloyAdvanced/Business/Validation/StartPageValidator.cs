using AlloyAdvanced.Models.Pages;
using EPiServer.Validation;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.Validation
{
    public class StartPageValidator : IValidate<StartPage>
    {
        public IEnumerable<ValidationError> Validate(StartPage instance)
        {
            var errors = new List<ValidationError>();

            //if (instance.StartDate >= instance.EndDate)
            //{
            //    errors.Add(new ValidationError
            //    {
            //        ErrorMessage = "StartDate must be before EndDate.",
            //        PropertyName = "StartDate",
            //        RelatedProperties = new string[] { "EndDate" },
            //        Severity = ValidationErrorSeverity.Error
            //    });
            //}

            if (instance.Name.ToLower().Contains("frak"))
            {
                errors.Add(new ValidationError
                {
                    ErrorMessage = "'frak' is a bad word. You can use it in the name of a page but we don't recommend it.",
                    PropertyName = "Name",
                    Severity = ValidationErrorSeverity.Warning
                });
            }

            return errors;
        }
    }
}