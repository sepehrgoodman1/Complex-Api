namespace Taav.Contracts.UserManagements.Dto;

public class CreateUserDto
{
    public CreateUserDto(
        string userName,
        string phoneNumber,
        string countryCallingCode)
    {
        UserName = userName;
        PhoneNumber = phoneNumber;
        CountryCallingCode = countryCallingCode;
    }

    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string CountryCallingCode { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}