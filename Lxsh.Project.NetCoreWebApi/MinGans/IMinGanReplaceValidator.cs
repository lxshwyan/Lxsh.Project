using System.ComponentModel.DataAnnotations;

namespace Lxsh.Project.NetCoreWebApi
{
    public interface IMinGanReplaceValidator
    {
        void Replace(object value, ValidationContext validationContext);
    }
}