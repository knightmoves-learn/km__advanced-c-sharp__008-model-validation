# 008 Model Validation

## Lecture

[![# 008 Model Validation](https://img.youtube.com/vi/MmpGZ9_CkQA/0.jpg)](https://www.youtube.com/watch?v=MmpGZ9_CkQA)

## Instructions

In this assignment you will add Model Validation to our Home Energy Api's Model.

In `HomeEnergyApi/Models/HomeModel.cs`...

- Covert type `Home` from a public record, to a public class
  - `Home` should contain 5 public properties.
    - `int Id`
    - `string OwnerLastName`
    - `string? StreetAddress`
    - `string? City`
    - `int? City`
  - `Home` should contain a public constructor taking one argument for each of its properties and assigning each of their values.
  - `Home.Id` and `Home.OwnerLastName` should be required fields.
  - `Home.StreetAddress` should require a maximum length of 40 characters.
  - `Home.MontlhyElectricUsage` should require a positive integer of 50,000 or less.

Additional Information:

- You should ONLY make code changes in `HomeEnergyApi/Models/HomeModel.cs` to complete this assignment.

## Building toward CSTA Standards:

- Create computational models that represent the relationships among different elements of data collected from a phenomenon or process. (3A-DA-12) https://www.csteachers.org/page/standards
- Demonstrate code reuse by creating programming solutions using libraries and APIs (3B-AP-16) https://www.csteachers.org/page/standards

## Resources

- https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0#validation-attributes
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
