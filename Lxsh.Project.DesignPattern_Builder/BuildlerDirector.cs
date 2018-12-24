using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Builder
{
    public class BuildlerDirector
    {
        AbstractPerson person = null;
        public void SetPerson(AbstractPerson person)
        {
            this.person = person;
        }
        public void CreatePerson()
        {
            this.person.CreateHead();
            this.person.CreateBody();
            this.person.CreateHand();
            this.person.CreateLeg();
        }
    }
}