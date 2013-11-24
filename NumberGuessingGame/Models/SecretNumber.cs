using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models
{
	public class SecretNumber
	{
		// Keeps track on previous guesses.
		private IList<GuessedNumber> _guessedNumbers;

		// Last made guess.
		private GuessedNumber _lastGuessedNumber;

		// The secret number.
		private int? _number;

		// Max number of guesses.
		public const int MaxNumberOfGuesses = 7;

		// Checks if user is allowed to make a guess.
		public bool CanMakeGuess
		{
			get
			{
				return GuessedNumbers.Count() < MaxNumberOfGuesses &&
					!_guessedNumbers.Any(x => x.Number == _number);
			}
		}

		// Returns number of guesses made.
		public int Count
		{
			get
			{
				return GuessedNumbers.Count();
			}
		}
		
		// Property of _guessedNumbers.
		public IList<GuessedNumber> GuessedNumbers
		{
			get
			{
				var GuessedNumbers = new ReadOnlyCollection<GuessedNumber>(_guessedNumbers);
				return GuessedNumbers;
			}
		}

		// Property of _lastGuessedNumber.
		public GuessedNumber LastGuessedNumber
		{
			get
			{
				return _lastGuessedNumber;
			}
		}

		// Property of _number.
		// Returns null as long as the user can make a guess
		// and returns the number when all guesses have been
		// made.
		public int? Number
		{
			get
			{
				// Return null if user still make guesses,
				// otherwise return the secret number.
				return (CanMakeGuess) ? null : _number;
			}

			private set
			{
				_number = value;
			}
		}

		// Clears all previously made guesses and
		// randomizes a new secret number.
		public void Initialize()
		{
			// Clear previous guesses.
			_guessedNumbers.Clear();

			// Clear last guess.
			_lastGuessedNumber = new GuessedNumber()
			{
				Number = null,
				Outcome = Outcome.Indefinite
			};

			// Randomize a new number.
			Number = new Random().Next(1, 101);
		}

		// Method for making a guess.
		public Outcome MakeGuess(int guess)
		{
			// Create a new instance of GuessedNumber
			// and set the number to guess.
			var guessedNumber = new GuessedNumber()
			{
				Number = guess
			};

			// If the guess isn't within range,
			// throw an exception.
			if (guessedNumber.Number > 100 || guessedNumber.Number < 1)
			{
				throw new ArgumentOutOfRangeException();
			}

			// If the user is out of guesses,
			// return NoMoreGuesses.
			else if (!CanMakeGuess)
			{
				return Outcome.NoMoreGuesses;
			}

			// If the guess is correct
			// set the Outcome to Right.
			else if (guess == _number)
			{
				guessedNumber.Outcome = Outcome.Right;
			}

			// If the guess has already been made,
			// set the Outcome t o OldGuess.
			else if (_guessedNumbers.Any(x => x.Number == guess))
			{
				guessedNumber.Outcome = Outcome.OldGuess;
			}
			
			// If the guess is higher than the
			// secret number, set the Outcome to High.
			else if (guess > _number)
			{
				guessedNumber.Outcome = Outcome.High;
			}

			// If none of the above,
			// then the guess is lower than the secret
			// number and Outcome should be Low.
			else
			{
				guessedNumber.Outcome = Outcome.Low;
			}

			// Add the guessed number to the list
			// and return the Outcome.
			_guessedNumbers.Add(guessedNumber);

			//Set the guess as the last guess made.
			_lastGuessedNumber = guessedNumber;

			return guessedNumber.Outcome;
		}

		// Runs Initialize().
		public SecretNumber()
		{
			// Create a new list.
			_guessedNumbers = new List<GuessedNumber>(7);

			// Run Initialize().
			Initialize();
		}
	}
}