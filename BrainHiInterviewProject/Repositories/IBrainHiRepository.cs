using BrainHiInterviewProject.Models;
using System.Collections.Generic;

namespace BrainHiInterviewProject.Repositories
{
    /// <summary>
    /// Class to store provider and appointment information, as well as public-facing methods to access data
    /// </summary>
    public interface IBrainHiRepository
    {
        /// <summary>
        /// Searches providers based on name and/or specialty
        /// </summary>
        /// <returns>Providers that match param provided. If both have values, only returns providers that match both params. If null params, returns all providers</returns>
        public IList<Provider> SearchProviders(
            string name,
            string specialty
            );

        /// <summary>
        /// Creates new provider and stores it with a unique id
        /// </summary>
        public void CreateProvider(
            Provider provider
            );

        /// <summary>
        /// Creates appointment and stores it with a unique id
        /// </summary>
        /// <throws>ArgumentException if appointment provider id does not match any existing providers</throws>
        public void CreateAppointment(
            Appointment appointment
            );
    }
}
