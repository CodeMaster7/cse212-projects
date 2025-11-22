// This class represents the top-level JSON object from USGS
// The JSON structure has a "features" array containing all earthquakes
public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary

    // The "features" property in the JSON is an array of Feature objects
    // PropertyNameCaseInsensitive in the deserializer allows "Features" to match "features"
    public List<Feature> Features { get; set; }
}

// Each feature represents one earthquake event
public class Feature
{
    // The "properties" object contains details about the earthquake
    // like location, magnitude, time, etc.
    public Properties Properties { get; set; }
}

// The properties object contains the actual earthquake data we care about
public class Properties
{
    // "place" - a text description of where the earthquake occurred
    // Example: "10km NE of Los Angeles, CA"
    public string Place { get; set; }

    // "mag" - the magnitude of the earthquake
    // Example: 2.5, 4.7, etc.
    // Using double because magnitudes can be decimal values
    public double Mag { get; set; }
}