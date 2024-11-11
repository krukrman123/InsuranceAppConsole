namespace InsuranceAppConsole
{
    public class Insured
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }

        public Insured(string firstName, string lastName, int age, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="Insured"/> instance.
        /// </summary>
        public override string ToString()
        {
            return $"{FirstName,-20} | {LastName,-20} | {Age,-5} | {PhoneNumber}";
        }
    }
}