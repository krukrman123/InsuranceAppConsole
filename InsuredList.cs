using InsuranceAppConsole;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;


/// <summary>
/// The `InsuredList` class manages a list of insured persons. It allows adding, removing, displaying, and searching for insured persons.
/// The list is saved to a file in JSON format to persist data across sessions.
/// </summary>
public class InsuredList
{
    // Internal list to store insured persons

    private readonly List<Insured> insuredList = new List<Insured>();

    // File path where the list of insured persons is saved in JSON format  {bin/Debug/net7.0/insuredList.Json}

    private const string FilePath = "insuredList.json";


    /// <summary>
    /// Constructor that attempts to load the list of insured persons from a file when the object is created.
    /// </summary>
    public InsuredList()
    {
        LoadFromFile(); 
    }





    /// <summary>
    /// Adds a new insured person to the list and saves the updated list to the file.
    /// </summary>
    /// <param name="insured">The insured person to be added.</param>
    internal void AddInsured(Insured insured)
    {
        if (insured == null)
        {
            MessageHandler.ShowMessage(MessageType.Error, "Pojištěná osoba nemůže být null.");
            return;
        }
        insuredList.Add(insured);
        SaveToFile(); 
    }



    /// <summary>
    /// Removes an insured person from the list and saves the updated list to the file.
    /// </summary>
    /// <param name="insured">The insured person to be removed.</param>
    internal void RemoveInsured(Insured insured)
    {
        if (insuredList.Remove(insured))
        {
            MessageHandler.ShowMessage(MessageType.Success, "Pojištěná osoba byla úspěšně odstraněna.");
            SaveToFile();
        }
        else
        {
            MessageHandler.ShowMessage(MessageType.Error, "Pojištěná osoba nebyla nalezena v seznamu.");
        }
    }


    /// <summary>
    /// Displays the list of insured persons in a formatted table.
    /// If the list is empty, it will display a message about the empty list.
    /// </summary>
    public void ShowList()
    {
        Console.Clear();
        if (insuredList.Count == 0)
        {
            MessageHandler.ShowMessage(MessageType.Info, "Seznam pojištěných osob je prázdný.");
            return;
        }

        Console.WriteLine("========================= SEZNAM POJIŠTĚNÝCH OSOB ===========================\n");
        Console.WriteLine("{0,-4} | {1,-20} | {2,-20} | {3,-5} | {4}", "ID", "Jméno", "Příjmení", "Věk", "Telefonní číslo");
        Console.WriteLine("-----------------------------------------------------------------------------");

        for (int i = 0; i < insuredList.Count; i++)
        {
            Console.WriteLine("{0,-4} | {1}", i + 1, insuredList[i].ToString());
        }

        MessageHandler.WaitForKeyPress();
    }



    /// <summary>
    /// Saves the list of insured persons to the file in JSON format.
    /// </summary>
    private void SaveToFile()
    {
        string jsonData = JsonSerializer.Serialize(insuredList);
        File.WriteAllText(FilePath, jsonData);
    }



    /// <summary>
    /// Loads the list of insured persons from the file if it exists.
    /// </summary>
    private void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            string jsonData = File.ReadAllText(FilePath);
            insuredList.AddRange(JsonSerializer.Deserialize<List<Insured>>(jsonData) ?? new List<Insured>());
        }
    }



    /// <summary>
    /// Searches for insured persons based on the provided search term. The search term can match part of the first name, last name, or phone number.
    /// </summary>
    /// <param name="searchTerm">The search term used for searching insured persons.</param>
    /// <returns>A list of insured persons that match the search term.</returns>
    public List<Insured> SearchInsured(string searchTerm)
    {
        return insuredList
            .Where(i => i.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        i.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        i.PhoneNumber.Contains(searchTerm) ||
                        i.PhoneNumber.Length >= 3 && i.PhoneNumber.Substring(i.PhoneNumber.Length - 3).Contains(searchTerm))
            .ToList();
    }



    /// <summary>
    /// Returns all insured persons as a read-only collection.
    /// </summary>
    /// <returns>A read-only collection of all insured persons.</returns>
    public ReadOnlyCollection<Insured> GetAll() => insuredList.AsReadOnly();
}
