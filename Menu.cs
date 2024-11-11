using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceAppConsole
{
    /// <summary>
    /// The Menu class provides a console interface for managing insured records, allowing users to add, view, search, update, and delete records.
    /// </summary>
    public class Menu
    {
        private List<Insured> insuredList = new List<Insured>();
        InsuredList insuredListInstance = new InsuredList();

        private readonly ConsoleColor originalColor = Console.ForegroundColor;

        /// <summary>
        /// Initializes a new instance of the Menu class with the provided insured list.
        /// Automatically starts the main menu loop.
        /// </summary>
        /// <param name="list">An instance of InsuredList containing insured records.</param>
        public Menu(InsuredList list)
        {
            Run();
        }

        /// <summary>
        /// Runs the main menu loop, prompting the user for a choice and executing the corresponding action.
        /// </summary>
        private void Run()
        {
            int choice;
            do
            {
                choice = ShowMenu();
                ExecuteChoice(choice);
            } while (choice != 0);
        }

        /// <summary>
        /// Displays the main menu options to the user and validates their choice.
        /// </summary>
        /// <returns>The selected menu option as an integer.</returns>
        private int ShowMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================ Pojišťovací záznamy ==================\n");
                Console.WriteLine("1 - Vytvořit nový pojištěnec");
                Console.WriteLine("2 - Zobrazit všechny pojištěnce");
                Console.WriteLine("3 - Hledat pojištěnce podle jména");
                Console.WriteLine("4 - Smazat pojištěnce");
                Console.WriteLine("0 - Konec");
                Console.WriteLine("=====================================================");
                Console.ForegroundColor = originalColor;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Vyberte akci: ");
                Console.ForegroundColor = originalColor;

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Neplatný vstup. Zadejte platnou volbu.");
                    Console.ForegroundColor = originalColor;
                    Console.Write("Vyberte akci: ");
                }

            } while (choice < 0 || choice > 4);

            return choice;
        }

        /// <summary>
        /// Executes the selected menu option by calling the appropriate method from the InsuredManager.
        /// </summary>
        /// <param name="choice">The selected menu option as an integer.</param>
        private void ExecuteChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    InsuredManager.CreateInsured(insuredListInstance);
                    break;
                case 2:
                    insuredListInstance.ShowList();
                    break;
                case 3:
                    InsuredManager.SearchInsured(insuredListInstance);
                    break;
                case 4:
                    InsuredManager.DeleteInsured(insuredListInstance);
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
