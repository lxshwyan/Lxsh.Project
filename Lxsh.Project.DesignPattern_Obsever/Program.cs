using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Obsever
{
    class Program
    {
        static void Main(string[] args)
        {
            ISubject subject = new Subject()
            {
                SubjectState = "李小双"
            };

            IObserver dbLogOobserver = new DBLogObserver();
            IObserver filelogobserver = new FilelogObserver();
            subject.Add(dbLogOobserver);
            subject.Add(filelogobserver);

         
            subject.Nofity();
        }
    }
}
