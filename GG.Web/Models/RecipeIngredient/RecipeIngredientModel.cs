﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public class RecipeIngredientModel
    {
        public int Id { get; set; }
        public string Count { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string Measurement { get; set; }
        public string Description { get; set; }
        public virtual IngredientModel Ingredient { get; set; }
    }
}