using Newtonsoft.Json;

namespace GameDevTesting;

[JsonObject]
public class Node
{
    private string _fileName;
    private string _firstName;
    private string _secondName;
    private int _age;
    private DateTime _birthday;
    private string _info;

    [JsonProperty("file_name")]
    public string FileName
    {
        get => _fileName;
        set => _fileName = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonProperty("first_name")]
    public string FirstName
    {
        get => _firstName;
        set => _firstName = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    [JsonProperty("second_name")]
    public string SecondName
    {
        get => _secondName;
        set => _secondName = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    [JsonProperty("age")]
    public int Age
    {
        get => _age;
        set => _age = value;
    }
    
    [JsonProperty("birthday")]
    public DateTime Birthday
    {
        get => _birthday;
        set => _birthday = value;
    }
    
    [JsonProperty("info")]
    public string Info
    {
        get => _info;
        set => _info = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Node() {
    
    }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="firstName"></param>
    /// <param name="secondName"></param>
    /// <param name="age"></param>
    /// <param name="birthday"></param>
    /// <param name="info"></param>
    public Node(string fileName, string firstName, string secondName, int age, DateTime birthday, string info)
    {
        _fileName = fileName;
        if (_fileName == "random")
            _fileName += ".node";
        _firstName = firstName;
        _secondName = secondName;
        _age = age;
        _birthday = birthday;
        _info = info;
    }

    public override string ToString()
    {
        return $"Node\nFile Name: {_fileName},\nFirst name: {_firstName},\nSecond Name: {_secondName}," +
               $"\nAge: {_age},\nBirthday: {_birthday},\nInfo: {_info}.\n";
    }
}