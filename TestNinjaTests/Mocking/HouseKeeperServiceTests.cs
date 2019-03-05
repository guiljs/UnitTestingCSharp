using NUnit.Framework;
using TestNinja.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TestNinja.Moq;

namespace TestNinja.Mocking.Tests
{
    [TestFixture()]
    public class HouseKeeperServiceTests
    {
        [Test()]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            //SETUP
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                new Housekeeper
                {
                    Email="a",FullName="b",Oid=1,StatementEmailBody="c"
                }
            }.AsQueryable());

            var statementManager = new Mock<IStatementManager>();
            var emailSender = new Mock<IEmailSender>();
            var xtraMessageBox = new Mock<IXtraMessageBox>();

            var service = new HouseKeeperService(unitOfWork.Object, statementManager.Object, emailSender.Object, xtraMessageBox.Object);

            //ACT
            service.SendStatementEmails(new DateTime(1981, 12, 3));

            //ASSERT
            statementManager.Verify(sm => sm.SaveStatement(1, "b", new DateTime(1981, 12, 3)));
        }
    }
}