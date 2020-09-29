using BrainHiInterviewProject.Models;
using BrainHiInterviewProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BrainHiInterviewProject.Controllers
{
    public class BrainHiController : Controller
    {
        [Route("/")]
        public ViewResult Index()
        {
            var model = new IndexViewModel
            {
                Providers = _repository.SearchProviders(null, null)
            };

            return View("/Views/Index.cshtml", model);
        }

        [Route("/registerprovider")]
        public ViewResult RegisterProvider() => View("/Views/RegisterProvider.cshtml");

        [Route("/bookappointment")]
        public ViewResult BookAppointment(
            int providerId
            )
        {
            var model = new BookAppointmentViewModel
            {
                ProviderId = providerId
            };

            return View("/Views/BookAppointment.cshtml", model);
        }

        [Route("/searchproviders")]
        [HttpGet]
        public JsonResult SearchProviders(
            string providerName,
            string providerSpecialty
            )
        {
            var providers = _repository.SearchProviders(providerName, providerSpecialty);

            return Json(new { providers });
        }

        [Route("/registerproviderpost")]
        [HttpPost]
        public ActionResult RegisterProviderPost(
            Provider provider
            )
        {
            CheckNullParam(provider, nameof(provider));

            _repository.CreateProvider(provider);

            return RedirectToAction("Index");
        }

        [Route("/bookappointmentpost")]
        [HttpPost]
        public ActionResult BookAppointmentPost(
            Appointment appointment,
            int providerId
            )
        {
            CheckNullParam(appointment, nameof(appointment));

            appointment.ProviderId = providerId;
            _repository.CreateAppointment(appointment);

            return RedirectToAction("Index");
        }

        private void CheckNullParam(
            object param,
            string paramName
            )
        {
            if (param == null)
                throw new ArgumentException($"${paramName} cannot be null");
        }

        public BrainHiController(
            IBrainHiRepository repository
            )
        {
            _repository = repository;
        }

        private IBrainHiRepository _repository;

    }
}
