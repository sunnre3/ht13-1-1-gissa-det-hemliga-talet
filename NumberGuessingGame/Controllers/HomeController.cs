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
			var sn = GetSecretNumberObj();


			var model = UpdateViewModel(sn);

            return View(model);
        }

		//
		// Post: /
		[HttpPost]
		public ActionResult Index([Bind(Include = "Guess")]SecretNumberViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Make a guess.
				var sn = GetSecretNumberObj();
				sn.MakeGuess(model.Guess);

				// Update the view model.
				model = UpdateViewModel(sn);

				return View(model);
			}

			return HttpNotFound();
		}

		private SecretNumberViewModel UpdateViewModel(SecretNumber sn)
		{
			var vm = new SecretNumberViewModel();
			vm.Number = sn.Number;
			vm.GuessedNumbers = sn.GuessedNumbers;
			vm.LastGuessedNumber = sn.LastGuessedNumber;
			vm.CanMakeGuess = sn.CanMakeGuess;

			return vm;
		}

		private SecretNumber GetSecretNumberObj()
		{
			return (SecretNumber)Session[SESSION_MODEL];
		}

		private void SaveSecretNumberObj(SecretNumber sn)
		{
			Session[SESSION_MODEL] = sn;
		}
    }
}
