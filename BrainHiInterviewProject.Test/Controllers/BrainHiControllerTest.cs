using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using FluentAssertions.Common;
using System;
using System.Linq;
using BrainHiInterviewProject.Repositories;
using BrainHiInterviewProject.Controllers;
using BrainHiInterviewProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrainHiInterviewProject.Tests
{
    public class BrainHiControllerTest
    {
        IBrainHiRepository _repository;
        BrainHiController _sut;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IBrainHiRepository>();

            _sut = new BrainHiController(_repository);
        }

        #region Index
        [Test]
        public void Index_CallsSearchMethod()
        {
            _sut.Index();

            _repository.Received(1).SearchProviders(null, null);
        }

        [Test]
        public void Index_ReturnsModelWithAllProviders()
        {
            _repository.SearchProviders(null, null).Returns(new List<Provider>());

            var result = _sut.Index();

            result.Model.Should().BeEquivalentTo(new IndexViewModel
            {
                Providers = new List<Provider>()
            });
            result.ViewName.Should().Be("/Views/Index.cshtml");
        }
        #endregion

        #region SearchProviders
        [Test]
        public void SearchProviders_IsDecorated()
        {
            _sut.GetType().GetMethod(nameof(BrainHiController.SearchProviders)).Should().BeDecoratedWith<HttpGetAttribute>();
        }

        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase("", null)]
        [TestCase("foo", "bah")]
        public void SearchProviders_CallsRepoMethod(
            string providerName,
            string providerSpecialty
            )
        {
            _sut.SearchProviders(providerName, providerSpecialty);

            _repository.Received(1).SearchProviders(providerName, providerSpecialty);
        }

        [Test]
        public void SearchProviders_ReturnsResultAsJson()
        {
            var providers = new List<Provider>
            {
                new Provider
                {
                    FullName = "foo",
                    Specialty = "bah"
                }
            };
            _repository.SearchProviders(Arg.Any<string>(), Arg.Any<string>()).Returns(providers);

            var result = _sut.SearchProviders("", "");

            var expectedResult = new JsonResult(new { providers }).ToString();
            result.ToString().Should().Be(expectedResult);
        }
        #endregion

        #region RegisterNewProvider
        [Test]
        public void RegisterNewProvider_IsDecorated()
        {
            _sut.GetType().GetMethod(nameof(BrainHiController.RegisterProviderPost)).IsDecoratedWith<HttpPostAttribute>();
        }

        [Test]
        public void RegisterNewProvider_NullParam_Throws()
        {
            Action act = () => _sut.RegisterProviderPost(null);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void RegisterNewProvider_CallsRepoMethod_ReturnsToHome()
        {
            var expectedProvider = new Provider
            {
                FullName = "foo",
                Specialty = "bah"
            };

            var res = _sut.RegisterProviderPost(expectedProvider);

            _repository.Received(1).CreateProvider(Arg.Is<Provider>(p =>
                p.FullName == "foo"
                && p.Specialty == "bah"));
        }
        #endregion

        #region BookAppointmentPost

        [Test]
        public void BookAppointmentPost_IsDecorated()
        {
            _sut.GetType().GetMethod(nameof(BrainHiController.BookAppointmentPost)).IsDecoratedWith<HttpPostAttribute>();
        }

        [Test]
        public void BookAppointmentPost_NullAppointment_Throws()
        {
            Action act = () => _sut.BookAppointmentPost(null, 0);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void BookAppointmentPost_CallsRepoMethod()
        {
            var expectedAppointment = new Appointment
            {
                PatientFullName = "foo"
            };
            var providerId = new Random().Next();

            _sut.BookAppointmentPost(expectedAppointment, providerId);

            _repository.Received(1).CreateAppointment(Arg.Is<Appointment>(
                a => a.PatientFullName == "foo"
                && a.ProviderId == providerId));
        }
        #endregion
    }
}