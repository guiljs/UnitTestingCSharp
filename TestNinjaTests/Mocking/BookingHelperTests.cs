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
    public class BookingHelperOverlappingBookingsExistTests
    {
        private Mock<IBookingRepository> _mockRepository;
        private Booking _existingBooking;

        [SetUp]
        public void Setup()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            _mockRepository = new Mock<IBookingRepository>();
            _mockRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>()
            {
                _existingBooking
            }.AsQueryable());
        }
        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            }, _mockRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
            }, _mockRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _mockRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        [Test()]
        public void StatusIsCancelled_ShouldReturnEmptyString()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking { Status = "Cancelled" });
            Assert.That(result, Is.Empty);
        }

        [Test()]
        public void NoOverlapping_ShouldReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking { });
            Assert.That(result, Is.Empty);
        }

        [Test()]
        public void ThereIsOverlapping_ShouldReturnBooking()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var listBookings = new List<Booking> {
                new Booking {
                    Id = 1, Reference = "Ref1", ArrivalDate = new DateTime(2019, 02, 28), DepartureDate = new DateTime(2020, 02, 28)
                }
            };

            var booking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2018, 03, 01),
                DepartureDate = new DateTime(2022, 03, 01)
            };

            mockUnitOfWork.Setup(m => m.Query<Booking>()
                            .Where(b => b.Id != booking.Id && b.Status != "Cancelled"))
                            .Returns(listBookings.AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(booking);
            Assert.That(result, Is.EqualTo("Ref1"));
        }
    }
}