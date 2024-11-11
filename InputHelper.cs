namespace InsuranceAppConsole
{
    public static class InputHelper
    {


        private static InsuredList insuredList;

        /// <summary>
        /// Prompts the user to enter a valid age and verifies that it is between 1 and 115.
        /// </summary>
        /// <returns>The entered age as an integer.</returns>
        public static int EnterAge()
        {
            int age;
            while (true)
            {
                Console.Write("Zadejte věk: ");  
                if (int.TryParse(Console.ReadLine(), out age) && age > 0 && age <= 115)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    MessageHandler.ShowMessage(MessageType.Error, "Neplatný věk. Zadejte číslo (až do 115)."); 
                }
                    Console.Clear();
            }
            return age;
        }

        /// <summary>
        /// Prompts the user to enter a valid phone number with exactly 9 digits.
        /// </summary>
        /// <returns>The entered phone number as a string.</returns>
        public static string EnterPhoneNumber()
        {
            string phoneNumber;
            while (true)
            {
                Console.Write("Zadejte telefonní číslo: ");
                phoneNumber = Console.ReadLine();
                if ((phoneNumber.Length == 9 && phoneNumber.All(char.IsDigit)) ||
                            (phoneNumber.Length == 3 && phoneNumber.All(char.IsDigit)))
                {
                    break;
                }
                else
                {
                    MessageHandler.ShowMessage(MessageType.Error, "Neplatné telefonní číslo. Zadejte {9} místné číslo."); 
                }
                Console.Clear();

            }
            return phoneNumber;
        }

        /// <summary>
        /// Prompts the user to enter a single word with only alphabetic characters.
        /// </summary>
        /// <param name="prompt">The message to display to the user.</param>
        /// <returns>The entered word as a string.</returns>
        public static string EnterWord(string prompt)
        {
            string word;
            while (true)
            {
                Console.Write(prompt);
                word = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(word) && word.All(char.IsLetter))
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    MessageHandler.ShowMessage(MessageType.Error, "Neplatný vstup. Zadejte platné slovo (žádná čísla).");
                }
                Console.Clear();

            }
            return word;
        }

        /// <summary>
        /// Prompts the user to enter a search term, which can be part of a first name, last name, or phone number.
        /// </summary>
        /// <returns>The entered search term as a string.</returns>
        public static string EnterSearchTerm(InsuredList insuredList)
        {
            if (!insuredList.GetAll().Any())
            {
               
                return null;  
            }

            string searchTerm;
            insuredList.ShowList();  

            while (true)
            {
                Console.Write("Zadejte jméno, příjmení nebo telefonní číslo pojištěné osoby (můžete zadat část): ");
                searchTerm = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Neplatný vstup. Zadejte alespoň jeden znak.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            return searchTerm;
        }



        /// <summary>
        /// Prompts the user to select a choice within a specified range.
        /// </summary>
        /// <param name="min">The minimum valid choice.</param>
        /// <param name="max">The maximum valid choice.</param>
        /// <returns>The validated choice as an integer.</returns>
        public static int GetValidatedChoice(int min, int max)
        {
            int choice;
            while (true)
            {
                Console.Write("Zadejte vaši volbu: "); 
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
                {
                    break;
                }
                else
                {
                    MessageHandler.ShowMessage(MessageType.Error, $"Neplatná volba. Zadejte číslo mezi {min} a {max}."); 
                }
            }
            return choice;
        }
    }
}
