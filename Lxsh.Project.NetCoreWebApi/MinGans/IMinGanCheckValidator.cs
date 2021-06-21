using System.ComponentModel.DataAnnotations;

namespace Lxsh.Project.NetCoreWebApi
{
    public interface IMinGanCheckValidator
    {
        ValidationResult IsValid(object value, ValidationContext validationContext);
    }
}