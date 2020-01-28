using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.Components
{
    public class Direction
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int Step { get; set; }
        public string Instruction { get; set; }
    }
}
