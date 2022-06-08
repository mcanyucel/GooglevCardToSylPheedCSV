// See https://aka.ms/new-console-template for more information
using FolkerKinzel.Contacts.IO;

Console.WriteLine("This application simplifies Google Contacts vCard exports by reducing the entries to");
Console.WriteLine("First Name, Last Names, Display Name (First + Last), Email Address (first one if many)");
Console.WriteLine("The vCard file should be in the same folder with this executable and named as 'contacts.vcf'");
Console.WriteLine("The resultant file will be in the same folder with this executable and named as 'contacts.csv'");
Console.WriteLine("It uses the third party library 'FolkerKinzel.Contacts.IO'");
Console.WriteLine("==================================================================");

while (true)
{
    Console.WriteLine("Press q to exit or any other key to start:");
    var key = Console.ReadKey();

    if (key.Key == ConsoleKey.Q) break;

    Console.WriteLine("Reading vCard file...");
    var contactList = ContactPersistence.LoadVcf("contacts.vcf");
    var csvListText = new List<string>();   
    if (contactList.Count > 0)
    {
        Console.WriteLine($"A total of {contactList.Count} entries found, starting to parse them...");
        foreach (var contact in contactList)
        {
            if (contact.EmailAddresses?.Count() > 0)
            {
                csvListText.Add($"{contact.DisplayName},{contact.EmailAddresses.First()}");
            }
        }
        Console.WriteLine("Parsing completed, writing results to file.");
        File.WriteAllLines("contacts.csv", csvListText, System.Text.Encoding.UTF8);
        Console.WriteLine("Done.");
    }
    else
    {
        Console.WriteLine("The file contains no vCard entries");
    }
    Console.WriteLine("---------------------------------------");
}