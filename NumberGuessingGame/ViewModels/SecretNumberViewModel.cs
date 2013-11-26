using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NumberGuessingGame.Models;
using System.ComponentModel.DataAnnotations;

namespace NumberGuessingGame.ViewModels
{
	public class SecretNumberViewModel
	{
		public int? Number { get; set; }

		public bool CanMakeGuess { get; set; }

		public IList<GuessedNumber> GuessedNumbers { get; set; }

		public GuessedNumber LastGuessedNumber { get; set; }

		[Required(ErrorMessage = "Du måste göra en gissning.")]
		[Range(1, 100, ErrorMessage = "Talet måste vara mellan 1 och 100.")]
		public int Guess { get; set; }

		// Number of guesses made.
		public int NumberOfGuesses { get; set; }

		// String property for getting the content header
		// in the view.
		public string Title
		{
			get
			{
				if (LastGuessedNumber.Outcome == Outcome.Right)
				{
					return "Rätt gissat!";
				}

				if (!CanMakeGuess)
				{
					return "Inga fler gissningar!";
				}


				var readable = GuessNumberReadable();
				return readable + " gissningen";
			}
		}

		// String property for retrieving what the last
		// guess come out as.
		public string LastGuessOutcome
		{
			get
			{
				string outcome = "";

				if (LastGuessedNumber.Outcome == Outcome.OldGuess)
				{
					return "Du har redan gissat på talet " + LastGuessedNumber.Number + "!";
				}

				else if (LastGuessedNumber.Outcome == Outcome.Right)
				{
					var readable = NumberToWord(GuessedNumbers.Count()).ToLower();
					outcome = "Grattis! Du klarade det på <strong>" + readable + "</strong> försöket.";
				}

				else if (LastGuessedNumber.Outcome == Outcome.High)
				{
					outcome = LastGuessedNumber.Number + " är för högt.";
				}

				else if (LastGuessedNumber.Outcome == Outcome.Low)
				{
					outcome = LastGuessedNumber.Number + " är för lågt.";
				}

				if (GuessedNumbers.Count() == 7 && LastGuessedNumber.Outcome != Outcome.Right)
				{
					outcome += " Inga fler gissningar! Det hemliga talet var " + Number + ".";
				}

				return outcome;
			}
		}

		// General method for retrieving a word instead of a
		// number to be more readable for our app.
		// Only accepts 0 > 7 because that's all the app
		// needs.
		public string NumberToWord(int number)
		{
			if (number == 0)
			{
				return "Noll";
			}

			else if (number == 1)
			{
				return "Första";
			}

			else if (number == 2)
			{
				return "Andra";
			}

			else if (number == 3)
			{
				return "Tredje";
			}

			else if (number == 4)
			{
				return "Fjärde";
			}

			else if (number == 5)
			{
				return "Femte";
			}

			else if (number == 6)
			{
				return "Sjätte";
			}

			else if (number == 7)
			{
				return "Sjunde";
			}

			else
			{
				throw new ArgumentException();
			}
		}

		// Provides a readable word for the title in
		// our view.
		private string GuessNumberReadable()
		{
			if (NumberOfGuesses < 6)
			{
				return NumberToWord(GuessedNumbers.Count() + 1);
			}

			else
			{
				return "Sista";
			}
		}
	}
}