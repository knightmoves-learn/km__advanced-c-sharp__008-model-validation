using System.ComponentModel.DataAnnotations;

public class Home
{
    [Required]
    public int Id { get; init;}

    [Required(ErrorMessage = "The OwnerLastName field is required.")]
    public string OwnerLastName { get; init;}
    
    [StringLength(40, ErrorMessage = "The field StreetAddress must be a string with a maximum length of 40.")]
    public string? StreetAddress { get; init;}

    public string? City { get; init;}

    [Range(0, 50000, ErrorMessage = "Monthly electric usage is limited to positive numbers of 50,000 kWh or less")]
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




