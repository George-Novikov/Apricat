using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    internal class Lesson : DatabaseItem
    {
        public int Id { get; set; }
        public bool Learned { get; set; } = false;
        public string Level { get; set; }
        public string AudioPath { get; set; }
        public virtual void MarkLearned(User user)
        {
            this.Learned = true;
        }
    }
}
