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

		[Required]
		[Range(1, 100)]
		public int Guess { get; set; }

		private string GuessNumberReadable
		{
			get
			{
				if (GuessedNumbers.Count() == 0)
				{
					return "första";
				}

				else if (GuessedNumbers.Count() == 1)
				{
					return "andra";
				}

				else if (GuessedNumbers.Count() == 2)
				{
					return "tredje";
				}

				else if (GuessedNumbers.Count() == 3)
				{
					return "fjärde";
				}

				else if (GuessedNumbers.Count() == 4)
				{
					return "femte";
				}

				else if (GuessedNumbers.Count() == 5)
				{
					return "sjätte";
				}

				else
				{
					return "sista";
				}
			}
		}

		public string Title
		{
			get
			{
				if (!CanMakeGuess)
				{
					return "Inga fler gissningar!";
				}

				else if (GuessedNumbers.Count() == 0)
				{
					return GuessNumberReadable + " gissningen";
				}

				else if (GuessedNumbers.Count() == 1)
				{
					return GuessNumberReadable + " gissningen";
				}

				else if (GuessedNumbers.Count() == 2)
				{
					return GuessNumberReadable + " gissningen";
				}

				else if (GuessedNumbers.Count() == 3)
				{
					return GuessNumberReadable + " gissningen";
				}

				else if (GuessedNumbers.Count() == 4)
				{
					return GuessNumberReadable + " gissningen";
				}

				else if (GuessedNumbers.Count() == 5)
				{
					return GuessNumberReadable + " gissningen";
				}

				else
				{
					return "Sista gissningen";
				}
			}
		}

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
					outcome = "Grattis! Du klarade det på " + GuessNumberReadable + " försöket.";
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
	}
}