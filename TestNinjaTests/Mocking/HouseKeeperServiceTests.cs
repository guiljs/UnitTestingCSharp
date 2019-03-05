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
        private string _statementFilename;
        private HouseKeeperService _service;
        private Mock<IStatementManager> _statementManager;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private DateTime _statementDate = new DateTime(1981, 12, 3);
        private Housekeeper _housekeeper;

        [SetUp]
        public void Setup()
        {
            //ARRANGE
            _housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };

            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _housekeeper
            }.AsQueryable());

            _statementFilename = "filename";

            _statementManager = new Mock<IStatementManager>();
            _statementManager
                .Setup(sm => sm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFilename); //()=> Lazy evaluation 

            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _service = new HouseKeeperService(unitOfWork.Object, _statementManager.Object, _emailSender.Object, _xtraMessageBox.Object);
        }

        [Test()]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            //ACT
            _service.SendStatementEmails(_statementDate);

            //ASSERT
            _statementManager.Verify(sm => sm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test()]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = null;
            _service.SendStatementEmails(_statementDate);

            _statementManager.Verify(sm => sm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), times: Times.Never);
        }

        [Test()]
        public void SendStatementEmails_HouseKeeperEmailIsEmpty_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = "";
            _service.SendStatementEmails(_statementDate);

            _statementManager.Verify(sm => sm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), times: Times.Never);
        }

        [Test()]
        public void SendStatementEmails_HouseKeeperEmailIsWhitespce_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = " ";
            _service.SendStatementEmails(_statementDate);

            _statementManager.Verify(sm => sm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), times: Times.Never);
        }

        [Test()]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);
            VerifyEmailIsSent();
        }


        [Test()]
        public void SendStatementEmails_StatementFilenameIsNull_ShouldNotEmailTheStatement()
        {
            _statementFilename = null;
            _service.SendStatementEmails(_statementDate);

            VerifyEmailIsNotSent();
        }

        [Test()]
        public void SendStatementEmails_StatementFilenameIsEmptyString_ShouldNotEmailTheStatement()
        {
            _statementFilename = "";
            _service.SendStatementEmails(_statementDate);

            VerifyEmailIsNotSent();
        }

        [Test()]
        public void SendStatementEmails_StatementFilenameIsWhiteSpace_ShouldNotEmailTheStatement()
        {
            _statementFilename = " ";
            _service.SendStatementEmails(_statementDate);

            VerifyEmailIsNotSent();
        }

        [Test()]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessage()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).Throws<Exception>();
            _service.SendStatementEmails(_statementDate);

            VerifyMessageBoxIsDisplayed();
        }

        private void VerifyMessageBoxIsDisplayed()
        {
            _xtraMessageBox.Verify(mb => mb.Show(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<MessageBoxButtons>()
                            ));
        }

        private void VerifyEmailIsNotSent()
        {
            _emailSender.Verify(em => em.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
        }

        private void VerifyEmailIsSent()
        {
            _emailSender
                .Verify(em => em.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _statementFilename,
                It.IsAny<string>()));
        }
    }
}