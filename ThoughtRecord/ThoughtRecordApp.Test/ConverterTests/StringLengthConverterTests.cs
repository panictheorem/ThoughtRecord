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
    public class StringLengthConverterTests
    {
        [TestMethod]
        public void StringLengthConverter_Convert_NullValue()
        {
            // Arrange
            StringLengthConverter converter = new StringLengthConverter();

            //Act
            object displayName = converter.Convert(null, null, null, null);

            //Assert
            Assert.AreEqual(string.Empty, displayName as string);
        }

        [TestMethod]
        public void StringLengthConverter_Convert_ValueOverMaxLength()
        {
            // Arrange
            StringLengthConverter converter = new StringLengthConverter();
            string value = new string('a',StringLengthConverter.MaxStringLength + 10);

            //Act
            object result = converter.Convert(value, null, null, null);
            string truncatedString = result as string;
            int truncatedLength = truncatedString.Length;

            //Assert
            Assert.AreEqual(StringLengthConverter.MaxStringLength + 3, truncatedLength);
        }

        [TestMethod]
        public void StringLengthConverter_Convert_ValueUnderMaxLength()
        {
            // Arrange
            StringLengthConverter converter = new StringLengthConverter();
            string value = new string('a', StringLengthConverter.MaxStringLength - 10);
            int initialLength = value.Length;

            //Act
            object result = converter.Convert(value, null, null, null);
            string truncatedString = result as string;
            int truncatedLength = truncatedString.Length;

            //Assert
            Assert.AreEqual(initialLength, truncatedLength);
        }

        [TestMethod]
        public void StringLengthConverter_Convert_ValueEqualsMaxLength()
        {
            // Arrange
            StringLengthConverter converter = new StringLengthConverter();
            string value = new string('a', StringLengthConverter.MaxStringLength);
            int initialLength = value.Length;

            //Act
            object result = converter.Convert(value, null, null, null);
            string truncatedString = result as string;
            int truncatedLength = truncatedString.Length;

            //Assert
            Assert.AreEqual(StringLengthConverter.MaxStringLength, truncatedLength);
        }

        [TestMethod]
        public void StringLengthConverter_Convert_ValueHasSpacesAndBreaks()
        {
            // Arrange
            StringLengthConverter converter = new StringLengthConverter();
            string value = "\n\r\n\r";

            //Act
            object displayName = converter.Convert(value, null, null, null);

            //Assert
            Assert.AreEqual((displayName as string).Trim(), string.Empty);
        }
    }
}
