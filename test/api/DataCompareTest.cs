namespace DiffProject.Test.Api
{
    using Xunit;
    using DiffProject.Infrastructure.V1;
    using DiffProject.Models.V1;


    public class DataCompareTest
    {
        [Fact]
        public void TestWithEqualText()
        {
            var leftText = new Data("1",
                SideEnum.Left, 
                @"one
                two
                three", 
                new Md5HashStrategy());

            var rightText = new Data("1",
                SideEnum.Right, 
                @"one
                two
                three", 
                new Md5HashStrategy());

            var dataCompare = new DataCompare(leftText, rightText);

            var result = dataCompare.Compare();

            Assert.Equal(result.LeftResult, result.RightResult);
        }

        [Fact]
        public void TestWithOneLineDifferen()
        {
            var leftText = new Data("1",
                SideEnum.Left,
                "one",
                new Md5HashStrategy());

            var rightText = new Data("1",
                SideEnum.Right,
                @"two",
                new Md5HashStrategy());

            var dataCompare = new DataCompare(leftText, rightText);

            var result = dataCompare.Compare();

            Assert.Equal(result.LeftResult[0].Match, result.RightResult[0].Match);
            Assert.NotEqual(result.LeftResult[0].Line, result.RightResult[0].Line);
        }

        [Fact]
        public void TestWithOneDifferenLineAmongText()
        {
            var leftText = new Data("1",
                SideEnum.Left, 
                @"one
                two
                three", 
                new Md5HashStrategy());
                
            var rightText = new Data("1",
                SideEnum.Right, 
                @"one
                four
                three", 
                new Md5HashStrategy());

            var dataCompare = new DataCompare(leftText, rightText);

            var result = dataCompare.Compare();

            Assert.Equal(result.LeftResult[1].Match, result.RightResult[1].Match);
            Assert.NotEqual(result.LeftResult[1].Line, result.RightResult[1].Line);
        }
    }
}