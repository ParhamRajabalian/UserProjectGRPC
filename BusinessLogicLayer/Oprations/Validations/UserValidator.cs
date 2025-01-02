using DataAccessLayer.Entities;
using UserProjectBusinessLogicLayer.Contracts.Validations;
using UserProjectBusinessLogicLayer.Models;


namespace UserProjectBusinessLogicLayer.Oprations.Validations;
public class UserValidator : IUserValidator
{
    public async Task<ValidationResultModel> ValidateAsync(UserModel user)
    {
        var validationResult = new ValidationResultModel();

        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("FirstName and LastName are required.");
        }

        if (user.BirthDate == default)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("DateOfBirth is required.");
        }

        validationResult.IsValid = validationResult.Errors.Count == 0;
        return validationResult;
    }

}
