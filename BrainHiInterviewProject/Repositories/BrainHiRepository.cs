using BrainHiInterviewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrainHiInterviewProject.Repositories
{
    public class BrainHiRepository : IBrainHiRepository
    {
        public IList<Provider> Providers { get => _providers ?? new List<Provider>(); set { _providers = value; } }

        public IList<Appointment> Appointments { get => _appointments ?? new List<Appointment>(); set { _appointments = value; } }

        public void CreateProvider(Provider provider)
        {
            _providers = _providers ?? new List<Provider>();
            provider.Id = _providers.Count;
            _providers.Add(provider);
        }

        public void CreateAppointment(Appointment appointment)
        {
            if (!Providers.Any(p => p.Id == appointment.ProviderId))
                throw new ArgumentException($"Appointment has no existing provider id: {appointment.ProviderId}");

            _appointments = _appointments ?? new List<Appointment>();
            appointment.Id = _appointments.Count;
            _appointments.Add(appointment);
        }

        public IList<Provider> SearchProviders(string name, string specialty)
        {
            return Providers
                .Where(p => string.IsNullOrEmpty(name) || p.FullName == name)
                .Where(p => string.IsNullOrEmpty(specialty) || p.Specialty == specialty)
                .ToList();
        }

        private IList<Provider> _providers;
        private IList<Appointment> _appointments;
    }
}
