using System;

[Serializable]
public class RepositoryLocationInfo
{
    private string _name;
    private string _url;
    private bool _enabled;

    public string Name { get { return _name; } set { _name = value; } }
    public string URL { get { return _url; } set { _url = value; } }
    public bool Enabled { get { return _enabled; } set { _enabled = value; } }

    public RepositoryLocationInfo(string name, string URL, bool enabled)
    {
        _name = name;
        _url = URL;
        _enabled = enabled;
    }

}