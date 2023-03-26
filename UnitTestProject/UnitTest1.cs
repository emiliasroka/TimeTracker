using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;
using TimeTracker.Classes;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TitlesComparisonTest()
        {
            //Arrange
            var cz1 = new NoteModel()
            {
                Title = "Shopping"
            };

            var cz2 = new NoteModel()
            {
                Title = "Tasks"
            };
            int com = 0;

            //Act
            com = cz1.CompareTo(cz2);

            //Assert
            Assert.AreEqual(1, com);
        }


        [TestMethod]
        public void TitleNameTest()
        {
            //Arrange
            NoteModel z;
            string nazwa = "testedtitle";

            //Act
            z = new NoteModel(nazwa, null);

            //Assert
            Assert.AreEqual(nazwa, z.Title);
        }

        [TestMethod]

        public void CorrectCloneTest()
        {
            // Arrange
            string nazwa = "title";
            NoteModel z = new NoteModel(nazwa, null);

            // Act
            NoteModel notes = (NoteModel)z.Clone();

            // Assert
            Assert.AreEqual(z.ToString(), notes.ToString());

        }

        [TestMethod]
        public void WorkTaskKonstruktor()
        {
            // Arange
            XElement item = new XElement("task");
            XElement createAt = new XElement("createdatetime");
            createAt.Value = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ms");
            item.Add(createAt);
            XElement desc = new XElement("description");
            desc.Value = "zrob zakupy";
            item.Add(desc);

            // Act
            var iten = new WorkTask(item);

            // Assert
            Assert.AreEqual(desc.Value, iten.Description);
        }
    }
}
