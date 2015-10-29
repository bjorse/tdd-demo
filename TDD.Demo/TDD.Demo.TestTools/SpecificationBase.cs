using NUnit.Framework;

namespace TDD.Demo.TestTools
{
    [TestFixture]
    public abstract class SpecificationBase
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            Given();
            When();
        }

        protected abstract void Given();

        protected abstract void When();
    }
}
