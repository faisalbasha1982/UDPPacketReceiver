using System;

public class Value
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public class Zone
{
    public string DisplayName { get; set; }
    public List<Value> Values { get; set; }
    public string ZoneName { get; set; }
}

public class RootObject
{
    public string __type { get; set; }
    public string Timestamp { get; set; }
    public List<Zone> Zones { get; set; }
}
