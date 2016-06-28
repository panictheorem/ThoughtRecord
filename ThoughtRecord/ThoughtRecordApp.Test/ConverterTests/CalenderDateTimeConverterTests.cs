using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.ViewModels.ValueConverters;

namespace ThoughtRecordApp.Test.ConverterTests
{
    [TestClass]
    public class CalenderDateTimeConverterTests
    {

        [TestMethod]
        public void CalendarDateTimeConverter_Convert_Null()
        {
            //Arrange
            CalendarDateTimeConverter dateTimeConverter = new CalendarDateTimeConverter();

            //Act
            var date = dateTimeConverter.Convert(null, null, null, null);

            //Assert
            Assert.AreEqual(date, null);
        }
    }
}
