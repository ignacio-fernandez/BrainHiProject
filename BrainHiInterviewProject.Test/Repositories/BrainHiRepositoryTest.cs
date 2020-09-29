using BrainHiInterviewProject.Repositories;
using NUnit.Framework;
using FluentAssertions;
using BrainHiInterviewProject.Models;
using System;
using System.Linq;

namespace BrainHiInterviewProject.Tests.Repositories
{
    public class BrainHiRepositoryTest
    {
        private BrainHiRepository _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new BrainHiRepository();
        }

        [TearDown]
        public void TearDown()
        {
            _sut.Providers = null;
            _sut.Appointments = null;
        }

        #region CreateProvider
        [Test]
        public void CreateProvider_SavesProvider()
        {
            var expectedProvider = new Provider
            {
                FullName = "foo",
                Specialty = "bah",
            };

            _sut.CreateProvider(expectedProvider);

            _sut.Providers.Count.Should().Be(1);
            _sut.Providers.Single().Id.Should().Be(0);
            _sut.Providers.Single().FullName.Should().Be("foo");
            _sut.Providers.Single().Specialty.Should().Be("bah");
        }

        [Test]
        public void CreateProvider_MultipleProviders()
        {
            var provider1 = new Provider
            {
                FullName = "one",
                Specialty = "one"
            };
            var provider2 = new Provider
            {
                FullName = "two",
                Specialty = "two"
            };

            _sut.CreateProvider(provider1);
            _sut.CreateProvider(provider2);

            _sut.Providers.Count.Should().Be(2);
            _sut.Providers.First().Id.Should().Be(0);
            _sut.Providers.Last().Id.Should().Be(1);
        }
        #endregion

        #region CreateAppointment
        [Test]
        public void CreateAppointment_ProviderDoesntExist_ThrowsArgumentException()
        {
            Action act = () => _sut.CreateAppointment(new Appointment
            {
                ProviderId = new Random().Next()
            });

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateAppointment_SavesAppointment()
        {
            var provider = new Provider
            {
                Id = new Random().Next()
            };
            _sut.CreateProvider(provider);
            var appointment = new Appointment
            {
                ProviderId = provider.Id
            };

            _sut.CreateAppointment(appointment);

            _sut.Appointments.Count.Should().Be(1);
            _sut.Appointments.Single().Should().BeEquivalentTo(appointment);
            _sut.Appointments.Single().Id.Should().Be(0);
        }

        [Test]
        public void CreateAppointment_MultipleAppointments()
        {
            var provider = new Provider
            {
                Id = new Random().Next()
            };
            _sut.CreateProvider(provider);
            var appointment1 = new Appointment
            {
                ProviderId = provider.Id,
                PatientFullName = "foo"
            };
            var appointment2 = new Appointment
            {
                ProviderId = provider.Id,
                PatientFullName = "bah"
            };

            _sut.CreateAppointment(appointment1);
            _sut.CreateAppointment(appointment2);

            _sut.Appointments.Count.Should().Be(2);
            _sut.Appointments.First().PatientFullName.Should().Be("foo");
            _sut.Appointments.First().Id.Should().Be(0);
            _sut.Appointments.Last().PatientFullName.Should().Be("bah");
            _sut.Appointments.Last().Id.Should().Be(1);
        }
        #endregion

        #region SearchProviders
        [Test]
        public void SearchProviders_NoMatch_ReturnsEmptyList()
        {
            var result = _sut.SearchProviders(null, null);

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Test]
        public void SearchProviders_NullParams_ReturnsAllProviders()
        {
            var numberOfProviders = new Random().Next(100);
            CreateMockProviders(numberOfProviders, "foo");

            var result = _sut.SearchProviders(null, null);

            result.Count.Should().Be(numberOfProviders);
        }

        [Test]
        public void SearchProviders_SearchByName_ReturnsSingleProvider()
        {
            var numberOfProviders = new Random().Next(100);
            CreateMockProviders(numberOfProviders, "foo");
            var randomProvider = new Random().Next(numberOfProviders);

            var result = _sut.SearchProviders(name: $"provider-{randomProvider}", null);

            result.Single().FullName.Should().Be($"provider-{randomProvider}");
        }

        [Test]
        public void SearchProviders_SearchBySpecialty_ReturnsProviders()
        {
            var numberOfProviders = new Random().Next(100);
            CreateMockProviders(numberOfProviders, "foo");
            CreateMockProviders(numberOfProviders, "bah");

            var result = _sut.SearchProviders(null, "bah");

            result.Count.Should().Be(numberOfProviders);
        }

        [Test]
        public void SearchProviders_SearchByBoth_ReturnsCorrectResult()
        {
            var numberOfProviders = new Random().Next(100);
            var randomProvider = new Random().Next(numberOfProviders);
            CreateMockProviders(numberOfProviders, "foo");
            CreateMockProviders(numberOfProviders, "bah");

            var result = _sut.SearchProviders($"provider-{randomProvider}", "foo");

            result.Single().FullName.Should().Be($"provider-{randomProvider}");
            result.Single().Specialty.Should().Be("foo");
        }

        private void CreateMockProviders(int numberOfProviders, string specialty)
        {
            for (var i = 0; i < numberOfProviders; i++)
            {
                _sut.CreateProvider(new Provider
                {
                    FullName = $"provider-{i}",
                    Specialty = specialty
                });
            }
        }
        #endregion
    }
}
