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
		[Required]
		public int? Number { get; set; }

		public bool CanMakeGuess { get; set; }

		public List<GuessedNumber> GuessedNumbers { get; set; }

		public GuessedNumber LastGuessedNumber { get; set; }

		[Required]
		[Range(1, 100)]
		public int Guess { get; set; }

		public string Title
		{
			get
			{
				if (GuessedNumbers.Count() == 0)
				{
					return "Första gissningen";
				}

				else if (GuessedNumbers.Count() == 1)
				{
					return "Andra gissningen";
				}

				else if (GuessedNumbers.Count() == 2)
				{
					return "Tredje gissningen";
				}

				else if (GuessedNumbers.Count() == 3)
				{
					return "Fjärde gissningen";
				}

				else if (GuessedNumbers.Count() == 4)
				{
					return "Femte gissningen";
				}

				else if (GuessedNumbers.Count() == 5)
				{
					return "Sjätte gissningen";
				}

				else
				{
					return "Sista gissningen";
				}
			}
		}
	}
}