using DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;
using UserProjectBusinessLogicLayer.Models;

namespace UserProjectBusinessLogicLayer.Contracts.Validations;
public interface IUserValidator
{
    Task<ValidationResultModel> ValidateAsync(UserModel user);
}
