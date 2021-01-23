using System;
using Xunit;

namespace GOTTests {
    public class UnitTest1 {
        //sample test to understand what it is all about
        [Fact]
        public void Test1() {
            //Arrange
            int larger = 1;
            int smaller = 0;

            //Act
            var result = larger > smaller;

            //Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }
    }
}
