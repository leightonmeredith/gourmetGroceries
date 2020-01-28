using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GG.Components;

namespace GG.Web.Tests
{
    [TestClass]
    public class RecipeControllerTest
    {
        [TestMethod]
        public void ValidateRecipeExist()
        {
            //Arrange
            var recipe = new Recipe();

            //Act


            //Assert
            Assert.IsNotNull(recipe);
        }


    }
}
