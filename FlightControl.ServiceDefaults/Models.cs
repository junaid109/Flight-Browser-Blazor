namespace FlightControl.ServiceDefaults;

public record FlightCoordinate(double Latitude, double Longitude, double Altitude, DateTime Timestamp);
public record Flight(string Id, string FlightNumber, double Latitude, double Longitude, double Altitude, double Speed, double Heading, string Status, string Origin, string Destination, List<FlightCoordinate> History);
