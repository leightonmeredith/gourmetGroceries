using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GG.Services;
using GG.Data;
using GG.Components;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace GG.Web.Tests
{
    [TestClass]
    public class RecipeServiceTest
    {
        [TestMethod]
        public void GetRecipe_ReturnSingleRecipe()
        {
            //Arrange
            //var db = new GGDataContext();
            var db = new Mock<GGDataContext>();

            var expectedRecipe = new Recipe()
            {
                Id = 1,
                Description = "test recipe description",
                Name = "Test recipe",
                Image = "tet recipe img"
            };
            var recipes = new List<Recipe>() { expectedRecipe }.AsQueryable();

            var dbMock = new Mock<IDbSet<IRecipe>>();
            //dbMock.Setup(x => x.FindAsync(It.IsAny(object[])));



            //dbMock.Setup(x => x.Provider).Returns(recipes.Provider);
            //dbMock.Setup(x => x.Expression).Returns(recipes.Expression);
            //dbMock.Setup(x => x.ElementType).Returns(recipes.ElementType);
            //dbMock.Setup(x => x.GetEnumerator()).Returns(recipes.GetEnumerator);


            //db.Setup(x => x.Recipes).Returns(dbMock.Object);

            var recipeService = new RecipeService(db.Object);

            //Act
            var actualRecipe = recipeService.GetRecipeById(expectedRecipe.Id);

            //Assert
            Assert.IsNotNull(actualRecipe);
            Assert.AreEqual(expectedRecipe.Id, actualRecipe.Id);
            Assert.AreEqual(expectedRecipe.Name, actualRecipe.Id);
        }
    }
}
