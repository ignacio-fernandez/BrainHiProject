using System;

namespace BrainHiInterviewProject.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string AppointmentReason { get; set; }
        public string PatientFullName { get; set; }
        public string PatientGender { get; set; }
        /// <summary>
        /// Patient Date Of Birth
        /// </summary>
        public DateTime PatientDOB { get; set; }
        public string PatientPhoneNumber { get; set; }
    }
}
