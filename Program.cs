using InsuranceAppConsole;

class Program
{
    /// <summary>
    /// The main method of the application.
    /// Initializes the InsuredList and displays the main menu to interact with the insured persons data.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the application.</param>
    static void Main(string[] args)
    {
        // Create an instance of InsuredList to store and manage insured persons.
        InsuredList insuredListInstance = new InsuredList();

        // Create the main menu and pass the insured list instance for management and interaction.
        Menu menu = new Menu(insuredListInstance);
    }
}
