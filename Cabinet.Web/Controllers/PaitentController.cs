using Cabinet.Core.Models;
using Cabinet.Core.modelview;
using Cabinet.EF.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace Cabinet.Web.Controllers
{
	public class PaitentController : Controller
	{
		public readonly ApplicationDbContext _Context;
		private readonly UserManager<ApplicationUser> _userManager;
		public PaitentController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager) 
		{
			_Context = Context;
			_userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			
			//var paitent1 = (from Pai in _Context.patients
			//		 join pack in _Context.Packs on Pai.PatientID equals pack.idpatient
			//		 //join payment in _Context.Payments on pack.Id equals payment.packid
			//		 join typepack in _Context.typePacks on pack.idtypepack equals typepack.id
			//		 select new Paitentpaymentview
			//		 {
			//			 PatientID = Pai.PatientID,
			//			 FirstName = Pai.FirstName,
			//			 LastName = Pai.LastName,
			//			 CIN = Pai.CIN,
			//			 DateOfBirth = Pai.DateOfBirth,
			//			 Gender = Pai.Gender,
			//			 ContactNumber = Pai.ContactNumber,
			//			 ContactNumber2 =Pai.ContactNumber2,
			//			 typePacks = _Context.typePacks.Where(x=>x.id== pack.idtypepack).FirstOrDefault(),
			//			 payments = _Context.Payments.Where(x=>x.packid==pack.Id).ToList()
			//		 }).ToList();
			var paitent = _Context.patients.ToList();
			ViewBag.doctorUser = await _userManager.GetUsersInRoleAsync("Doctor");
			return View(paitent);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Patient patient)
		{
			ModelState.Remove("Pack");
			if(ModelState.IsValid)
			{
				if(patient.CIN !="")
				{
					var p = _Context.patients.FirstOrDefault(p => p.CIN == patient.CIN);
					if(p != null) 
					{
						TempData["error"] = "Paitent exist";
						return RedirectToAction("Index", "Paitent");
					}
					else
					{
						
						_Context.patients.Add(patient);
						_Context.SaveChanges();
						TempData["success"] = "Ajouter Paitent success";
						return RedirectToAction("Index", "Paitent");
					}
				}
				else
				{
					_Context.patients.Add(patient);
					_Context.SaveChanges();
					TempData["success"] = "Ajouter child success";
					return RedirectToAction("Index", "Paitent");
				}
				
			}
			else 
			{
				TempData["error"] = "Check input empty";
				return View(patient);
			}
			
		}

		public IActionResult Edit(int id)
		{
			var patient = _Context.patients.FirstOrDefault(p => p.PatientID == id);
			
			return View(patient);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Patient patient)
		{
			ModelState.Remove("Appointment");
			ModelState.Remove("Pack");
			if (id != patient.PatientID)
			{
				return NotFound();
			}
			
			if (ModelState.IsValid)
			{
				var p = _Context.patients.FirstOrDefault(p => p.PatientID == patient.PatientID);
				if (p != null)
				{
					if(p.CIN == patient.CIN)
					{
						
						p.FirstName =patient.FirstName;
						p.LastName =patient.LastName;
						p.DateOfBirth = patient.DateOfBirth;
						p.Gender = patient.Gender;
						p.CIN = patient.CIN;
						p.Email = patient.Email;
						p.Allergies	= patient.Allergies;
						p.ChronicConditions = patient.ChronicConditions;
						p.CIN = patient.CIN;
						p.ContactNumber = patient.ContactNumber;
						p.ContactNumber2 = patient.ContactNumber2;
						p.Address = patient.Address;
						p.MedicalHistory = patient.MedicalHistory;
                       // _Context.patients.Update(patient);
						_Context.SaveChanges();
						TempData["success"] = "paitent updated";
						return RedirectToAction("Index", "Paitent");
					}
					else
					{
						var cinchecker = _Context.patients.FirstOrDefault(p => p.CIN == patient.CIN);
						if (cinchecker != null )
						{
							TempData["error"] = "check CIN exist";

                            return View(patient);
						}
						else
						{
							TempData["success"] = "paitent updated";
							p.FirstName = patient.FirstName;
							p.LastName = patient.LastName;
							p.DateOfBirth = patient.DateOfBirth;
							p.Gender = patient.Gender;
							p.CIN = patient.CIN;
							p.Email = patient.Email;
							p.Allergies = patient.Allergies;
							p.ChronicConditions = patient.ChronicConditions;
							p.CIN = patient.CIN;
							p.ContactNumber = patient.ContactNumber;
							p.ContactNumber2 = patient.ContactNumber2;
							p.Address = patient.Address;
							p.MedicalHistory = patient.MedicalHistory;
							_Context.SaveChanges();
							return RedirectToAction("Index", "Paitent");
						}
					}
					
				}
				else
				{
					TempData["error"] = "Check Input";
					return View(patient);
				}
				
			}
			return View(patient);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			var p = _Context.patients.FirstOrDefault(p => p.PatientID == id);
			if (p != null)
			{
                TempData["success"] = "paitent Deleted";
                _Context.patients.Remove(p);
				_Context.SaveChanges();
				return RedirectToAction("Index", "Paitent");
			}
			else
			{
				TempData["error"] = "paitent not deleted";
                return RedirectToAction("Index", "Paitent");
            }
			
		}
		public IActionResult Details(int id)
		{
			var patient = _Context.patients.Find(id);

			if (patient == null)
			{
				TempData["error"] = "paitent Not exsict";
                return RedirectToAction("Index", "Paitent"); // Return a 404 Not Found response if the patient is not found
            }

			return View(patient);
		}

		 

	}
}
