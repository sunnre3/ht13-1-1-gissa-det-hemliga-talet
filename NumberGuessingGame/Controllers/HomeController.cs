using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumberGuessingGame.Models;
using NumberGuessingGame.ViewModels;

namespace NumberGuessingGame.Controllers
{
    public class HomeController : Controller
    {
		private const string SESSION_MODEL = "SessionModel";

        //
        // GET: /

        public ActionResult Index()
        {
			if(Session[SESSION_MODEL] == null)
			{
				Session[SESSION_MODEL] = new SecretNumber();
			}
			var sn = (SecretNumber)Session[SESSION_MODEL];


			var model = new SecretNumberViewModel();
			model.GuessedNumbers = sn.GuessedNumbers.ToList();
			model.CanMakeGuess = sn.CanMakeGuess;

            return View("Index", model);
        }

    }
}
