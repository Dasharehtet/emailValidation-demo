// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");

var emailCompanies = new EmailCompanies();
emailCompanies.TryAdd("e@g.com", 1);

Console.WriteLine("Hello, World!");


internal class EmailCompanies
{
    private readonly IDictionary<string, HashSet<int>> _dictionaryCompanies = new Dictionary<string, HashSet<int>>();
    private readonly EmailAddressAttribute _emailValidator = new();

    public void TryAdd(string email, IEnumerable<int> companies)
    {
        if (EmailIsValid(email))
        {
            if (_dictionaryCompanies.TryGetValue(email, out var currentCompanies))
            {
                currentCompanies.UnionWith(companies);
            }
            else
            {
                _dictionaryCompanies.Add(email, new HashSet<int>(companies));
            }
        }
    }

    public void TryAdd(string email, int company)
    {
        if (EmailIsValid(email))
        {
            if (_dictionaryCompanies.TryGetValue(email, out var currentCompanies))
            {
                currentCompanies.Add(company);
            }
            else
            {
                _dictionaryCompanies.Add(email, new HashSet<int>() { company });
            }
        }
    }

    public IEnumerable<int>? GetCompaniesByEmail(string email)
    {
        if (EmailIsValid(email))
        {
            if (_dictionaryCompanies.TryGetValue(email, out var companies))
            {
                return companies;
            }
        }
        return null;
    }

    private bool EmailIsValid(string email)
    {
        return !string.IsNullOrEmpty(email) && _emailValidator.IsValid(email);
    }
}