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
    public class EmotionNameDisplayConverterTests
    {
        [TestMethod]
        public void EmotionNameDisplayConverter_Convert_NullValue()
        {
            //Arrange
            EmotionNameDisplayConverter converter = new EmotionNameDisplayConverter();

            //Act
            object displayName = converter.Convert(null, null, null, null);

            //Assert
            Assert.AreEqual("unspecified :", displayName as string);
        }

        [TestMethod]
        public void EmotionNameDisplayConverter_Convert_EmptyStringValue()
        {
            //Arrange
            EmotionNameDisplayConverter converter = new EmotionNameDisplayConverter();

            //Act
            object displayName = converter.Convert("", null, null, null);

            //Assert
            Assert.AreEqual("unspecified :", displayName as string);
        }

        [TestMethod]
        public void EmotionNameDisplayConverter_Convert_StringValue()
        {
            //Arrange
            EmotionNameDisplayConverter converter = new EmotionNameDisplayConverter();
            string value = "Happy";

            //Act
            object displayName = converter.Convert(value, null, null, null);

            //Assert
            Assert.AreEqual(value + " :", displayName as string);
        }
    }
}
