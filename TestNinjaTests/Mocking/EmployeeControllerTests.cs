using NUnit.Framework;
using TestNinja.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace TestNinja.Moq.Tests
{
    [TestFixture()]
    public class EmployeeControllerTests
    {
        [Test()]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployee ()
        {
            var mock = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(mock.Object);

            controller.DeleteEmployee(1);

            mock.Verify(x => x.RemoveEmployee(1));
        }

        [Test()]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeAndRedirect()
        {
            var mock = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(mock.Object);

            controller.DeleteEmployee(1);

           // mock.Verify(x => x.(1));
        }
    }
}