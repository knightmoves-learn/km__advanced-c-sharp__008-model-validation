using System.ComponentModel.DataAnnotations;

public class Home
{
    [Required]
    public int Id { get; init;}

    [Required]
    public string OwnerLastName { get; init;}
    
    [StringLength(40)]
    public string? StreetAddress { get; init;}

    public string? City { get; init;}

    [Range(0, 40)]
    public int? MonthlyElectricUsage { get; init;}

    public Home (int id, string ownerLastName, string streetAddress, string city, int monthlyElectricUsage)
    {
        Id = id;
        OwnerLastName = ownerLastName;
        StreetAddress = streetAddress;
        City = city;
        MonthlyElectricUsage = monthlyElectricUsage;
    }
}




