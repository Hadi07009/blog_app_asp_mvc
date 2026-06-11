using System;
using System.ComponentModel.DataAnnotations;

namespace BootstrapERP.Models
{
    public class Batch
    {
        [Key]
        public int BatchID { get; set; }

        [Required]
        [StringLength(200)]
        public string BatchName { get; set; }

        [Required]
        [StringLength(500)]
        public string BatchDescription { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int TotalClasses { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        public int EnrolledStudents { get; set; }

        [Required]
        [StringLength(50)]
        public string BatchStatus { get; set; } // Active, Completed, Upcoming

        [StringLength(100)]
        public string InstructorName { get; set; }

        [Required]
        [StringLength(500)]
        public string BatchTopic { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
    }

    public class BatchEnrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Required]
        public int BatchID { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string StudentEmail { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string StudentPhone { get; set; }

        [StringLength(500)]
        public string StudentAddress { get; set; }

        [StringLength(100)]
        public string StudentEducation { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [StringLength(50)]
        public string EnrollmentStatus { get; set; } // Enrolled, Pending, Completed

        [StringLength(500)]
        public string SpecialRequests { get; set; }

        public Batch Batch { get; set; }
    }

    public class BatchEnrollmentViewModel
    {
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalClasses { get; set; }
        public int AvailableSeats { get; set; }
        public string BatchTopic { get; set; }
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        public string StudentPhone { get; set; }

        [StringLength(500)]
        public string StudentAddress { get; set; }

        [StringLength(100)]
        public string StudentEducation { get; set; }

        [StringLength(500)]
        public string SpecialRequests { get; set; }

        public bool AgreedToTerms { get; set; }
    }
}
