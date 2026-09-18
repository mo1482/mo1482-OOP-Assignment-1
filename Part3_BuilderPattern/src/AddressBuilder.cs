namespace Part3_BuilderPattern;

public class AddressBuilder
{
    private string? _street;
    private string? _city;
    private string? _state;
    private string? _zipCode;
    private string? _country;

    public AddressBuilder SetStreet(string street) 
    {
        _street = street; return this; 
    }

    public AddressBuilder SetCity(string city) 
    {
        _city = city; return this; 
    }

    public AddressBuilder SetState(string state) 
    {
        _state = state; return this; 
    }

    public AddressBuilder SetZipCode(string zipCode) 
    {
        _zipCode = zipCode; return this; 
    }

    public AddressBuilder SetCountry(string country) 
    {
        _country = country; return this; 
    }

    public Address Build()
    {
        Require(_street, nameof(_street));
        Require(_city, nameof(_city));
        Require(_state, nameof(_state));
        Require(_zipCode, nameof(_zipCode));
        Require(_country, nameof(_country));

        return new Address(_street!, _city!, _state!, _zipCode!, _country!);
    }

    private static void Require(string? value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"Address field {field} is mandatory.");
    }
}
