using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class BatchesController : Controller
    {
        private dbERPSolutionEntities _db = new dbERPSolutionEntities();

        // GET: Batches/Index
        public ActionResult Index()
        {
            try
            {
                // Mock data for demonstration - replace with database queries
                var batches = new List<Batch>
                {
                    new Batch
                    {
                        BatchID = 1,
                        BatchName = "HSC ICT Batch 2027",
                        BatchDescription = "Complete HSC ICT course with live classes and materials",
                        StartDate = DateTime.Parse("2026-06-26"),
                        EndDate = DateTime.Parse("2026-07-26"),
                        TotalClasses = 8,
                        TotalSeats = 100,
                        EnrolledStudents = 45,
                        BatchStatus = "Active",
                        InstructorName = "Mr. Ahmed Hassan",
                        BatchTopic = "HSC ICT Preparation"
                    },
                    new Batch
                    {
                        BatchID = 2,
                        BatchName = "ASP.NET MVC Professional Batch",
                        BatchDescription = "Advanced ASP.NET MVC development with real-world projects",
                        StartDate = DateTime.Parse("2026-07-15"),
                        EndDate = DateTime.Parse("2026-10-15"),
                        TotalClasses = 24,
                        TotalSeats = 60,
                        EnrolledStudents = 32,
                        BatchStatus = "Upcoming",
                        InstructorName = "Mr. Md. Abdul Hai Al Hadi",
                        BatchTopic = "ASP.NET MVC Development"
                    }
                };

                return View(batches);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading batches: " + ex.Message;
                return View(new List<Batch>());
            }
        }

        // GET: Batches/Details/id
        public ActionResult Details(int id)
        {
            try
            {
                var batch = GetBatchById(id);
                if (batch == null)
                {
                    return HttpNotFound();
                }

                ViewBag.AvailableSeats = batch.TotalSeats - batch.EnrolledStudents;
                return View(batch);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading batch details: " + ex.Message;
                return View();
            }
        }

        // GET: Batches/Enroll/id
        public ActionResult Enroll(int id)
        {
            try
            {
                var batch = GetBatchById(id);
                if (batch == null)
                {
                    return HttpNotFound();
                }

                // Check if seats are available
                if (batch.EnrolledStudents >= batch.TotalSeats)
                {
                    ViewBag.Error = "Sorry, this batch is full. No more seats available.";
                    return RedirectToAction("Index");
                }

                var model = new BatchEnrollmentViewModel
                {
                    BatchID = batch.BatchID,
                    BatchName = batch.BatchName,
                    StartDate = batch.StartDate,
                    TotalClasses = batch.TotalClasses,
                    AvailableSeats = batch.TotalSeats - batch.EnrolledStudents,
                    BatchTopic = batch.BatchTopic,
                    InstructorName = batch.InstructorName
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading enrollment form: " + ex.Message;
                return View();
            }
        }

        // POST: Batches/Enroll
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll(BatchEnrollmentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Reload batch information
                    var batch = GetBatchById(model.BatchID);
                    model.AvailableSeats = batch.TotalSeats - batch.EnrolledStudents;
                    model.BatchName = batch.BatchName;
                    model.StartDate = batch.StartDate;
                    model.TotalClasses = batch.TotalClasses;
                    model.BatchTopic = batch.BatchTopic;
                    model.InstructorName = batch.InstructorName;

                    return View(model);
                }

                if (!model.AgreedToTerms)
                {
                    ModelState.AddModelError("AgreedToTerms", "You must agree to the terms and conditions.");
                    return View(model);
                }

                // Create enrollment record
                var enrollment = new BatchEnrollment
                {
                    BatchID = model.BatchID,
                    StudentName = model.StudentName,
                    StudentEmail = model.StudentEmail,
                    StudentPhone = model.StudentPhone,
                    StudentAddress = model.StudentAddress,
                    StudentEducation = model.StudentEducation,
                    SpecialRequests = model.SpecialRequests,
                    EnrollmentDate = DateTime.Now,
                    EnrollmentStatus = "Pending"
                };

                // Save to database (replace with actual database call)
                // _db.BatchEnrollments.Add(enrollment);
                // _db.SaveChanges();

                // Update batch enrolled students count
                var batch = GetBatchById(model.BatchID);
                batch.EnrolledStudents++;

                ViewBag.SuccessMessage = "Enrollment successful! You will receive a confirmation email shortly.";
                return RedirectToAction("EnrollmentConfirmation", new { enrollmentId = enrollment.EnrollmentID });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error during enrollment: " + ex.Message;
                return View(model);
            }
        }

        // GET: Batches/EnrollmentConfirmation/id
        public ActionResult EnrollmentConfirmation(int enrollmentId)
        {
            try
            {
                ViewBag.EnrollmentID = enrollmentId;
                ViewBag.Message = "Thank you for enrolling! We have sent a confirmation email to your registered email address.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        // Helper method to get batch by ID (replace with database query)
        private Batch GetBatchById(int id)
        {
            var batches = new List<Batch>
            {
                new Batch
                {
                    BatchID = 1,
                    BatchName = "HSC ICT Batch 2027",
                    BatchDescription = "Complete HSC ICT course with live classes and materials",
                    StartDate = DateTime.Parse("2026-06-26"),
                    EndDate = DateTime.Parse("2026-07-26"),
                    TotalClasses = 8,
                    TotalSeats = 100,
                    EnrolledStudents = 45,
                    BatchStatus = "Active",
                    InstructorName = "Mr. Ahmed Hassan",
                    BatchTopic = "HSC ICT Preparation"
                },
                new Batch
                {
                    BatchID = 2,
                    BatchName = "ASP.NET MVC Professional Batch",
                    BatchDescription = "Advanced ASP.NET MVC development with real-world projects",
                    StartDate = DateTime.Parse("2026-07-15"),
                    EndDate = DateTime.Parse("2026-10-15"),
                    TotalClasses = 24,
                    TotalSeats = 60,
                    EnrolledStudents = 32,
                    BatchStatus = "Upcoming",
                    InstructorName = "Mr. Md. Abdul Hai Al Hadi",
                    BatchTopic = "ASP.NET MVC Development"
                }
            };

            return batches.FirstOrDefault(b => b.BatchID == id);
        }
    }
}
