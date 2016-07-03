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
        public void CalendarDateTimeConverter_ConvertNowValue()
        {
            //Arrange
            DateTime now = DateTime.Now;
            CalendarDateTimeConverter dateTimeConverter = new CalendarDateTimeConverter();

            //Act
            var date = dateTimeConverter.Convert(now, null, null, null) as DateTimeOffset?;

            //Assert
            Assert.AreEqual(date.Value, now);
        }

        [TestMethod]
        public void CalendarDateTimeConverter_ConvertMinValue()
        {
            //Arrange
            CalendarDateTimeConverter dateTimeConverter = new CalendarDateTimeConverter();

            //Act
            var date = dateTimeConverter.Convert(DateTime.MinValue, null, null, null) as DateTimeOffset?;

            //Assert
            Assert.AreEqual(date.Value, DateTime.MinValue);
        }

        [TestMethod]
        public void CalendarDateTimeConverter_ConvertMaxValue()
        {
            //Arrange
            CalendarDateTimeConverter dateTimeConverter = new CalendarDateTimeConverter();

            //Act
            var date = dateTimeConverter.Convert(DateTime.MaxValue, null, null, null) as DateTimeOffset?;

            //Assert
            Assert.AreEqual(date.Value.Day, DateTime.Now.Day);
        }

        [TestMethod]
        public void CalendarDateTimeConverter_ConvertNullValue()
        {
            //Arrange
            CalendarDateTimeConverter dateTimeConverter = new CalendarDateTimeConverter();

            //Act
            var date = dateTimeConverter.Convert(null, null, null, null) as DateTimeOffset?;

            //Assert
            Assert.AreEqual(date.Value.Day, DateTime.Now.Day);
        }
    }
}
