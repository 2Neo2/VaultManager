using Newtonsoft.Json;
namespace GameDevTesting;

class Program
{
    private static readonly Vault Vault = new Vault();
    private static int _idRandom;
    
    /// <summary>
    /// Intro for user.
    /// </summary>
    private static void Info()
    {
        Console.WriteLine("1) Add node;");
        Console.WriteLine("2) Find node by name;");
        Console.WriteLine("3) Save vault into directory;");
        Console.WriteLine("4) Upload vault from directory;");
        Console.WriteLine("5) See all nodes.");
    }

    /// <summary>
    /// Add random node to directory.
    /// </summary>
    private static void AddNode()
    {
        Console.WriteLine("Do you want add a node from your file using JSON or programmatically create a random node ? (Path/Random)");
        string answer = Console.ReadLine() ?? "";
        string? path = null;
        
        if (Equals(answer, "Path"))
        {
            path = GetFilePath("Input path to your file: ", true);
        } else if (!answer.Equals("Random"))
        {
            throw new AggregateException("Your answer should be <Path> or <Random>");
        }

        if (Equals(path, null))
        {
            Vault.AddNode(
                new Node(
                    path ?? "random" + _idRandom, 
                    Randomizer.GetRandomString(10), 
                    Randomizer.GetRandomString(10), 
                    Randomizer.GetRandomInt(), 
                    Randomizer.GetRandomDateTime(),
                    Randomizer.GetRandomString(100)
                )
            );
        }
        else
        {
            try
            {
                Vault.AddNode(JsonConvert.DeserializeObject<Node>(File.ReadAllText(path)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        ++_idRandom;
        Console.WriteLine("Node successfully added!");
    }

    /// <summary>
    /// Find random node in vault.
    /// </summary>
    private static void FindNodeByName()
    {
        PrintNodes();

        Console.WriteLine("Input file name of some node: ");
        string fileNameSearch = Console.ReadLine() ?? "";
        Node? result = Vault.FindNodeByName(fileNameSearch);
        if (result != null)
        {
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine("Node is not find!");
        }
    }
    
    /// <summary>
    /// Save vault to directory.
    /// </summary>
    /// <param name="directoryPath">Path for save nodes in that directory.</param>
    private static void SaveVaultToDirectory(string directoryPath)
    {
        try
        {
            Vault.SaveToDirectory(directoryPath);
            Console.WriteLine("Nodes successfully saved!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Upload vault from directory.
    /// </summary>
    /// <param name="directoryPath">Path for upload nodes in that directory.</param>
    private static void UploadVaultFromDirectory (string directoryPath)
    {
        Vault.UploadFilesFromDirectory(directoryPath);
        Console.Clear();
        Console.WriteLine("All nodes in Vault:\n");
        PrintNodes();
    }

    /// <summary>
    /// Print all nodes in _vault.
    /// </summary>
    private static void PrintNodes()
    {
        Console.WriteLine("\nAll nodes: \n");
        foreach (var node in Vault)
        {
            Node? currentNode = (Node)node;
            if (!Equals(currentNode, null)) Console.WriteLine((Node)node);
        }
    }

    /// <summary>
    /// Get file or directory path.
    /// </summary>
    /// <param name="message">Message for user.</param>
    /// <param name="isFile">The value that indicates what we need to check.</param>
    /// <returns>Path to file or directory.</returns>
    /// <exception cref="AggregateException"></exception>
    private static string GetFilePath(string message, bool isFile)
    {
        Console.WriteLine(message);
        string path = Console.ReadLine() ?? "";

        if (isFile)
        {
            if (!File.Exists(path))
                throw new AggregateException("File does not exist!");
        }
        else
        {
            if (!Directory.Exists(path))
                throw new AggregateException("Directory does not exist!");
        }

        return path;
    }
    
    public static void Main(string[] args)
    {
        
        while (true)
        {
             Console.Clear();
             Console.WriteLine("Choose number of one option below: ");
             Info();
             switch (Console.ReadKey().Key)
             { 
                 case ConsoleKey.D1:
                     Console.Clear();
                     AddNode();
                     break;
                 case ConsoleKey.D2:
                     Console.Clear();
                     FindNodeByName();
                     break;
                 case ConsoleKey.D3:
                     Console.Clear();
                     SaveVaultToDirectory(GetFilePath("Input path to save directory:", false));
                     break;
                 case ConsoleKey.D4:
                     Console.Clear();
                     UploadVaultFromDirectory(GetFilePath("Input directory path to upload Vault:", false));
                     break;
                 case ConsoleKey.D5:
                     Console.Clear();
                     PrintNodes();
                     break;
             }
    
             Console.WriteLine("Input some button on your keyboard.");
             Console.ReadKey();
             Console.Clear();
                
             Console.WriteLine("Do you want continue ? (Yes/No)");
             string answer = Console.ReadLine() ?? "";
             if (answer == "No")
             {
                 return;
             } 
             else if (answer != "Yes")
             {
                 throw new AggregateException("Answer should be Yes/No");
             }
        }
    }
}