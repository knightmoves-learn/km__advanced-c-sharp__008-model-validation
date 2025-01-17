// public record Home(int id, string ownerLastName, string streetAddress, string city, int monthlyElectricUsage);
using System.ComponentModel.DataAnnotations;
public class Home
{
    [Required]
    public int Id { get; }

    [Required]
    public string OwnerLastName { get; }

    [StringLength(40)]
    public string? StreetAddress { get; }

    public string? City { get; }

    [Range(0, 50000, ErrorMessage = "Monthly electric usage is limited to positive numbers of 50,000 kWh or less")]
    public int? MonthlyElectricUsage { get; }

    public Home(int id, string ownerLastName, string? streetAddress, string? city, int? monthlyElectricUsage)
    {
        Id = id;
        OwnerLastName = ownerLastName;
        StreetAddress = streetAddress;
        City = city;
        MonthlyElectricUsage = monthlyElectricUsage;
    }
}