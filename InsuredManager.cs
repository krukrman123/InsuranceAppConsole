using InsuranceAppConsole;

public static class InsuredManager
{
    private static InsuredList insuredList;

    /// <summary>
    /// Creates a new insured person, collects their details, and adds them to the insured list.
    /// </summary>
    public static void CreateInsured(InsuredList insuredList)
    {
        Console.Clear();
        string firstName = InputHelper.EnterWord("Zadejte jméno: ");
        string lastName = InputHelper.EnterWord("Zadejte příjmení: ");
        int age = InputHelper.EnterAge();
        string phoneNumber;

        // Loop until a unique phone number is entered
        while (true)
        {
            phoneNumber = InputHelper.EnterPhoneNumber();

            if (!insuredList.SearchInsured(phoneNumber).Any())
            {
                break; 
            }
            else
            {
                MessageHandler.ShowMessage(MessageType.Error, "Pojištěná osoba s tímto telefonním číslem již existuje. Zadejte jiné telefonní číslo.");
                Console.Clear();
            }
        }

        Insured newInsured = new Insured(firstName, lastName, age, phoneNumber);
        MessageHandler.ShowMessage(MessageType.Success, "Pojištěná osoba byla úspěšně vytvořena.");
        insuredList.AddInsured(newInsured);
    }


    /// <summary>
    /// Searches for insured people based on a search term (first name, last name, or phone number).
    /// </summary>
    public static void SearchInsured(InsuredList insuredList)
    {
        Console.Clear();
        string searchTerm = InputHelper.EnterSearchTerm(insuredList);
        var results = insuredList.SearchInsured(searchTerm);

        if (results.Count > 0)
        {
            DisplaySearchResults(results, insuredList);
        }
        else
        {
            MessageHandler.ShowMessage(MessageType.Error, "Žádné záznamy nenalezeny.");
            return;
        }

    }

    /// <summary>
    /// Displays the search results in a formatted way.
    /// </summary>
    private static void DisplaySearchResults(List<Insured> results, InsuredList insuredList)
    {
        Console.Clear();
        Console.WriteLine("===================== VÝSLEDKY VYHLEDÁVÁNÍ ==========================");
        Console.WriteLine("{0,-20} | {1,-20} | {2,-5} | {3}", "Jméno", "Příjmení", "Věk", "Telefonní číslo");
        Console.WriteLine("---------------------------------------------------------------------");

        foreach (var insured in results)
        {
            Console.WriteLine(insured);
        }

        Console.WriteLine("\n==================== AKCE ====================");
        Console.WriteLine("1 - Smazat pojištěnce");
        Console.WriteLine("2 - Upravit pojištěnce");
        Console.WriteLine("0 - Zpět");

        int actionChoice = InputHelper.GetValidatedChoice(0, 2);

        switch (actionChoice)
        {
            case 1:
                Console.Write("Zadejte telefonní číslo pojištěnce, kterého chcete smazat: ");
                string phoneToDelete = InputHelper.EnterPhoneNumber();
                var insuredToDelete = insuredList.SearchInsured(phoneToDelete).FirstOrDefault();

                if (insuredToDelete != null)
                {
                    if (ConfirmDeletion(phoneToDelete))
                    {
                        insuredList.RemoveInsured(insuredToDelete);
                    }
                    else
                    {
                        MessageHandler.ShowMessage(MessageType.Info, "Smazání bylo zrušeno.");
                    }
                }
                else
                {
                    MessageHandler.ShowMessage(MessageType.Error, "Pojištěná osoba s tímto telefonním číslem nebyla nalezena.");
                }
                break;

            case 2:
                Console.Write("Zadejte telefonní číslo pojištěnce, kterého chcete upravit: ");
                string phoneToUpdate = InputHelper.EnterPhoneNumber();
                var insuredToUpdate = insuredList.SearchInsured(phoneToUpdate).FirstOrDefault();

                if (insuredToUpdate != null)
                {
                    UpdateInsuredDetails(insuredToUpdate);
                }
                else
                {
                    MessageHandler.ShowMessage(MessageType.Error, "Pojištěná osoba s tímto telefonním číslem nebyla nalezena.");
                }
                break;

            case 0:
                break;

            default:
                MessageHandler.ShowMessage(MessageType.Error, "Neplatná volba.");
                break;
        }


    }



    /// <summary>
    /// Allows the user to update details of an insured person.
    /// </summary>
    public static void UpdateInsured(InsuredList insuredList)
    {
        string phoneNumber = InputHelper.EnterPhoneNumber();
        var insured = insuredList.SearchInsured(phoneNumber).FirstOrDefault();

        if (insured != null)
        {
            UpdateInsuredDetails(insured);
        }
        else
        {
            MessageHandler.ShowMessage(MessageType.Error, "Pojištěná osoba s tímto telefonním číslem nebyla nalezena.");
        }
    }

    /// <summary>
    /// Handles the process of updating the insured person's details.
    /// </summary>
    private static void UpdateInsuredDetails(Insured insured)
    {
       

        int choice;
        do
        {
        Console.Clear();
            Console.WriteLine("Informace o pojištěnci:");
            Console.WriteLine("======================================");
            Console.WriteLine("Jméno: {0}", insured.FirstName);
            Console.WriteLine("Příjmení: {0}", insured.LastName);
            Console.WriteLine("Věk: {0}", insured.Age);
            Console.WriteLine("Telefonní číslo: {0}", insured.PhoneNumber);
            Console.WriteLine("======================================");
            Console.WriteLine("Pojištěná osoba nalezena. Co chcete změnit?");
            Console.WriteLine("1 - Jméno");
            Console.WriteLine("2 - Příjmení");
            Console.WriteLine("3 - Věk");
            Console.WriteLine("4 - Telefonní číslo");
            Console.WriteLine("0 - Zpět");

            choice = InputHelper.GetValidatedChoice(0, 4);

            switch (choice)
            {
                case 1:
                    UpdateFirstName(insured); 
                    break;

                case 2:
                    UpdateLastName(insured);  
                    break;

                case 3:
                    UpdateAge(insured); 
                    break;

                case 4:
                    UpdatePhoneNumber(insured); 
                    break;

                case 0:
                    Console.WriteLine("Úprava zrušena.");
                    break;
            }
        } while (choice != 0);
    }


    /// <summary>
    /// Updates the insured person's first name.
    /// </summary>
    private static void UpdateFirstName(Insured insured)
    {
        insured.FirstName = InputHelper.EnterWord("Nové jméno: ");
        MessageHandler.ShowMessage(MessageType.Info, "Jméno bylo úspěšně aktualizováno.");
    }

    /// <summary>
    /// Updates the insured person's last name.
    /// </summary>
    private static void UpdateLastName(Insured insured)
    {
        insured.LastName = InputHelper.EnterWord("Nové příjmení: ");
        MessageHandler.ShowMessage(MessageType.Info, "Příjmení bylo úspěšně aktualizováno.");
    }

    /// <summary>
    /// Updates the insured person's age.
    /// </summary>
    private static void UpdateAge(Insured insured)
    {
        insured.Age = InputHelper.EnterAge();
        MessageHandler.ShowMessage(MessageType.Info, "Věk byl úspěšně aktualizován.");
    }

    /// <summary>
    /// Updates the insured person's phone number.
    /// </summary>
    private static void UpdatePhoneNumber(Insured insured)
    {
        string newPhoneNumber = InputHelper.EnterPhoneNumber();

        if (insuredList.SearchInsured(newPhoneNumber).Any(i => i != insured))
        {
            MessageHandler.ShowMessage(MessageType.Error, "Toto telefonní číslo je již použito jinou pojištěnou osobou.");
            return;
        }

        insured.PhoneNumber = newPhoneNumber;
        MessageHandler.ShowMessage(MessageType.Info, "Telefonní číslo bylo úspěšně aktualizováno.");
    }


    /// <summary>
    /// Deletes an insured person from the list.
    /// </summary>
    public static void DeleteInsured(InsuredList insuredList)
    {
        Console.Clear();

        if (!insuredList.GetAll().Any())
        {
            MessageHandler.ShowMessage(MessageType.Info, "Seznam pojištěnců je prázdný.");
            return;
        }

        insuredList.ShowList();
        Console.Write("Zadejte telefonní číslo pojištěné osoby, kterou chcete smazat: ");
        string phoneNumber = InputHelper.EnterPhoneNumber();

        var insured = insuredList.SearchInsured(phoneNumber).FirstOrDefault();

        if (insured != null)
        {
            if (ConfirmDeletion(phoneNumber))
            {
                insuredList.RemoveInsured(insured);
                return;
            }
            else
            {
                Console.WriteLine("Smazání pojištěné osoby bylo zrušeno.");
                return;
            }
        }
        else
        {
            MessageHandler.ShowMessage(MessageType.Error, "Pojištěná osoba s tímto telefonním číslem nebyla nalezena.");
        }
    }



    /// <summary>
    /// Asks for confirmation before deleting an insured person.
    /// </summary>
    private static bool ConfirmDeletion(string phoneNumber)
    {
        MessageHandler.ShowMessage(MessageType.Info, $"Opravdu chcete smazat pojištěnou osobu s telefonním číslem {phoneNumber}? (Y/N): ");
        string confirmation = Console.ReadLine().ToUpper();
        return confirmation == "Y";
    }
}
