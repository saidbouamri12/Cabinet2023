using Cabinet.Core.Models;
using Cabinet.Core.modelview;
using Cabinet.EF.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Cabinet.Web.Controllers
{
	public class PaymentController : Controller
	{
		public readonly ApplicationDbContext _Context;
		


		public PaymentController(ApplicationDbContext Context)
		{
			_Context = Context;
			
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}
		public IActionResult historique_Paiement(int id)
		{
			var paitent1 = (from Pai in _Context.patients
							join pack in _Context.Packs on Pai.PatientID equals pack.idpatient
							join payment in _Context.Payments on pack.Id equals payment.packid
							join typepack in _Context.typePacks on pack.idtypepack equals typepack.id
							where Pai.PatientID == id
							select new Paitentpaymentview
							{
								PatientID = Pai.PatientID,
								FirstName = Pai.FirstName,
								LastName = Pai.LastName,
								CIN = Pai.CIN,
								DateOfBirth = Pai.DateOfBirth,
								Gender = Pai.Gender,
								ContactNumber = Pai.ContactNumber,
								ContactNumber2 = Pai.ContactNumber2,
								typePacks = _Context.typePacks.Where(x => x.id == pack.idtypepack).FirstOrDefault(),
								payments = _Context.Payments.Where(x => x.packid == pack.Id).FirstOrDefault()
								
							}).ToList();
			return View(paitent1);
		}

		
		public IActionResult Reglepay(int id)
		{
			var pay = _Context.Payments.Where(x=>x.packid==id).FirstOrDefault();
			if(pay==null)
			{
				return View();
			}
			return View(pay);
		}
		[HttpPost]
		public async Task<IActionResult> Reglepay(int id, Payment pay)
		{
			ModelState.Remove("Pack");

			Payment pay1 = _Context.Payments.Where(x => x.packid == id).FirstOrDefault();
			if(pay1==null)
			{  
				if(pay.Amount >=0 || pay.Amount==null)
				{
					Payment t = new Payment();
					t.Amount = pay.Amount;
					t.PaymentDate = DateTime.Now;
					t.packid = id;
					//	dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Payments ON");
					
					_Context.Payments.Add(t);
					_Context.SaveChanges();
					return RedirectToAction("Index", "paitent");
				}
				else
				{
					return View(pay);
				}
			}
			else
			{
				if(ModelState.IsValid)
				{
					pay1.Amount =pay.Amount;
					_Context.SaveChanges();
					return RedirectToAction("Index", "paitent");
				}
				else
				{
					return View(pay);
				}
			}

			
			
		}

		
	}
}
