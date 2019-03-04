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
    public class HousekeeperHelperTests
    {
        private const string FILENAME = "filename.txt";

        [Test()]
        public void SendStatementEmails_WhenCalled_ReturnTrue()
        {
            var houseKeeper = new Housekeeper
            {
                Email = "mary@email.com",
                FullName = "Mary Jones",
                Oid = 1,
                StatementEmailBody = "Email body"
            };
            var data = new List<Housekeeper>();
            data.Add(houseKeeper);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Query<Housekeeper>()).Returns(data.AsQueryable());

            var mockStatementManager = new Mock<IStatementManager>();
            mockStatementManager.Setup(m => m.SaveStatement(houseKeeper.Oid, houseKeeper.FullName, new DateTime())).Returns(FILENAME);

            var mockEmailSender = new Mock<IEmailSender>();
            string subject = string.Format("Sandpiper Statement {0:yyyy-MM} {1}", new DateTime(), houseKeeper.FullName);
            mockEmailSender.Setup(m => m.EmailFile(houseKeeper.Email, houseKeeper.StatementEmailBody, FILENAME, subject)).Verifiable();
            var housekeeperHelper = new HousekeeperHelper(mockUnitOfWork.Object, mockStatementManager.Object, mockEmailSender.Object);

            var result = housekeeperHelper.SendStatementEmails(new DateTime());

            mockEmailSender.Verify(m => m.EmailFile(houseKeeper.Email, houseKeeper.StatementEmailBody, FILENAME, subject));
            Assert.IsTrue(result);


        }
    }
}