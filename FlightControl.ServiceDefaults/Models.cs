namespace FlightControl.ServiceDefaults;

public record Flight(string Id, string FlightNumber, double Latitude, double Longitude, double Altitude, double Speed, double Heading, string Status);
