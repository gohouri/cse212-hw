public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    public string Type { get; set; } = "";
    public Feature[] Features { get; set; } = Array.Empty<Feature>();
}

public class Feature
{
    public string Type { get; set; } = "";
    public Properties Properties { get; set; } = new Properties();
}

public class Properties
{
    public double? Mag { get; set; }
    public string Place { get; set; } = "";
}