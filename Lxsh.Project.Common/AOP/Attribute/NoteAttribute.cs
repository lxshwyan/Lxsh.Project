using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common
{
    public class NoteAttribute : Attribute
    {
        public NoteAttribute()
        { }

        public NoteAttribute(string note)
        {
            this.Note = note;
        }

        /// <summary>
        /// 解释
        /// </summary>
        public string Note { get; set; }
    }
}
