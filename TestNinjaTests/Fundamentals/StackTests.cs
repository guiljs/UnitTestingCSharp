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
        public void Push_NullObject_ThrowArgumentNullException()
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
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack<int?>();

            Assert.That(stack.Count, Is.EqualTo(0));

        }

        [Test()]
        public void Count_StackHasValue_ReturnCount()
        {
            var stack = new Stack<int?>();

            stack.Push(1);

            Assert.That(stack.Count, Is.EqualTo(1));

        }

        [Test()]
        public void Pop_ListCountIsZero_ThrowInvalidOperationException()
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
        public void Peek_ListCountIsZero_ThrowInvalidOperationException()
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
            stack.Push(2);
            stack.Push(3);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(3));
            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}