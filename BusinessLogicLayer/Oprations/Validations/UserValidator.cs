using DataAccessLayer.Entities;
using UserProjectBusinessLogicLayer.Contracts.Validations;
using UserProjectBusinessLogicLayer.Models;


namespace UserProjectBusinessLogicLayer.Oprations.Validations;
public class UserValidator : IUserValidator
{
    public async Task<ValidationResultModel> ValidateAsync(UserModel user)
    {
        // فرض کنیم که ValidationResult شامل اطلاعات معتبر بودن داده‌ها باشد
        var validationResult = new ValidationResultModel();

        // اعتبارسنجی ساده: مثلا چک کردن که نام و نام خانوادگی وارد شده باشند
        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("FirstName and LastName are required.");
        }

        // اعتبارسنجی برای تاریخ تولد
        if (user.BirthDate == default)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add("DateOfBirth is required.");
        }

        // در اینجا می‌توان اعتبارسنجی‌های دیگر را اضافه کرد

        validationResult.IsValid = validationResult.Errors.Count == 0;
        return validationResult;
    }

}
