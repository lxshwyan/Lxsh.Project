
namespace Lxsh.Project.NetCoreWebApi
{
    public class MinganCheckInput
    {
        [MinGanCheck]
        public string Text { get; set; }
        public string Remark { get; set; }
    }
    
    public class MinganReplaceInput
    {
        [MinGanReplace]
        public string Text { get; set; }
        public string Remark { get; set; }
    }
}