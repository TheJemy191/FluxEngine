using FluentAssertions;

namespace Flux.Resources.Test
{
    public class ResourcesManagerTest
    {
        [Fact]
        public void MultipleGetAreSame()
        {
            var manager = new TestResourceManager();

            var test1 = manager.Get("Test1").Value;
            var test2 = manager.Get("Test1").Value;

            test1.Should().BeSameAs(test2);
        }

        [Fact]
        public void DifferentGetAreNotSame()
        {
            var manager = new TestResourceManager();

            var test1 = manager.Get("Test1").Value;
            var test2 = manager.Get("Test2").Value;

            test1.Should().NotBeSameAs(test2);
        }

        [Fact]
        public void GetMustIncrementRefCount()
        {
            var manager = new TestResourceManager();

            manager.Get("Test1");

            manager.GetRefCount("Test1").Should().Be(1);
        }

        [Fact]
        public void DisposeMustDecrementRefCount()
        {
            var manager = new TestResourceManager();

            var test1 = manager.Get("Test1");
            test1.Dispose();

            manager.GetRefCount("Test1").Should().Be(0);
        }

        [Fact]
        public void InexistingRefCountReturnZero()
        {
            var manager = new TestResourceManager();

            manager.GetRefCount("Test1").Should().Be(0);
        }

        [Fact]
        public void PurgeUnloadResources()
        {
            var manager = new TestResourceManager();

            var test1 = manager.Get("Test1");
            test1.Dispose();
            manager.Purge();
            var test2 = manager.Get("Test1");

            test1.Value.Unloaded.Should().BeTrue();
            test1.Value.Should().NotBeSameAs(test2.Value);
        }
    }
}