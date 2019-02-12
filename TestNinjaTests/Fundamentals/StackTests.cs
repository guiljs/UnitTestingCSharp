using NUnit.Framework;
using TestNinja.Fundamentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Fundamentals.Tests
{
    [TestFixture()]
    public class StackTests
    {
        [Test()]
        public void Push_NullObject_ReturnArgumentNullException()
        {
            var stack = new Stack<int?>();

            Assert.That(() =>
            {
                stack.Push(null);

            }, Throws.ArgumentNullException);
        }

        [Test()]
        public void Push_NewObject_ReturnListCountOne()
        {
            var stack = new Stack<int?>();

            stack.Push(1);

            Assert.That(stack.Count, Is.EqualTo(1));

        }

        [Test()]
        public void Pop_ListCountIsZero_ReturnInvalidOperationException()
        {
            var stack = new Stack<int?>();

            Assert.That(() =>
            {
                stack.Pop();
            }, Throws.InvalidOperationException);
        }

        [Test()]
        public void Pop_ListCountIsGreaterThenZero_ReturnItemAndRemove()
        {
            var stack = new Stack<int?>();
            stack.Push(1);

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test()]
        public void Peek_ListCountIsZero_ReturnInvalidOperationException()
        {
            var stack = new Stack<int?>();

            Assert.That(() =>
            {
                stack.Peek();
            }, Throws.InvalidOperationException);
        }

        [Test()]
        public void Peek_ListCountIsGreaterThenZero_ReturnItemWithoutRemovingIt()
        {
            var stack = new Stack<int?>();
            stack.Push(1);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.Count, Is.EqualTo(1));
        }
    }
}