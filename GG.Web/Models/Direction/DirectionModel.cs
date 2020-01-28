using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public class DirectionModel
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int Step { get; set; }
        public string Instruction { get; set; }
    }
}