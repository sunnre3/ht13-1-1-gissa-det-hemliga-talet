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
		public ActionResult Reset()
		{
			// Start a new game.
			Session[SESSION_MODEL] = new SecretNumber();

			// Return the Index view.
			return Redirect("/");
		}
		
		//
        // GET: /
        public ActionResult Index()
        {
			// If there isn't a game started yet,
			// start one.
			if(Session[SESSION_MODEL] == null)
			{
				Session[SESSION_MODEL] = new SecretNumber();
			}

			// Get the SecretNumber object.
			var sn = GetSecretNumberObj();

			// Get a SecretNumberViewModel object
			// and return it to the view.
			var model = UpdateViewModel(sn);
            return View(model);
        }

		//
		// Post: /
		[HttpPost]
		public ActionResult Index([Bind(Include = "Guess")]SecretNumberViewModel model)
		{
			// If the session has timed out
			// return another view with the option
			// to start over.
			if (!ValidateSession())
			{
				return View("Restart");
			}

			// Get the SecretNumber object.
			var sn = GetSecretNumberObj();

			if (ModelState.IsValid)
			{
				// Make a guess.
				sn.MakeGuess(model.Guess);	
			}

			// Update the view model.
			model = UpdateViewModel(sn);

			return View(model);
		}

		private SecretNumberViewModel UpdateViewModel(SecretNumber sn)
		{
			var vm = new SecretNumberViewModel();
			vm.Number = sn.Number;
			vm.GuessedNumbers = sn.GuessedNumbers;
			vm.LastGuessedNumber = sn.LastGuessedNumber;
			vm.CanMakeGuess = sn.CanMakeGuess;
			vm.NumberOfGuesses = sn.Count;

			return vm;
		}

		private bool ValidateSession()
		{
			// Check if sessions are enabled.
			if (Session != null)
			{
				// Check if a new session id was generated.
				if (Session.IsNewSession)
				{
					// If there is a cookie eventhough the session is new
					// we can assume the user had a previous game in progress
					// but waited too long and the session timed out.
					var cookie = Request.Headers["Cookie"];
					if (cookie != null && cookie.IndexOf("ASP.NET_SessionId") >= 0)
					{
						return false;
					}
				}
			}

			return true;
		}

		private SecretNumber GetSecretNumberObj()
		{
			return (SecretNumber)Session[SESSION_MODEL];
		}
    }
}
