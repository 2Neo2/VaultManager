using System.Collections;
using System.Text;
using Newtonsoft.Json;

namespace GameDevTesting;

public class Vault : IEnumerable
{
    private Node?[] _nodes;
    private int _currentSize;
    private FileStream _fileStream;

    public Vault()
    {
        _nodes = new Node?[4];
        _currentSize = 0;
    }

    /// <summary>
    /// Add node to list from vault.
    /// </summary>
    /// <param name="node">Adding node.</param>
    public void AddNode(Node? node)
    {
        if (Equals(node, null)) throw new AggregateException("Adding node is null!");
        if (_nodes.Length == _currentSize)
        {
            Node?[] tmp = new Node?[_currentSize * 2];
            Array.Copy(_nodes, tmp, _currentSize - 1);
            _nodes = tmp;
        }
        
        _nodes[_currentSize] = node;
        ++_currentSize;
    }

    /// <summary>
    /// Filling nodes from another directory.
    /// </summary>
    /// <param name="pathToDirectory"></param>
    /// <exception cref="AggregateException"></exception>
    public void UploadFilesFromDirectory(string pathToDirectory)
    {
        if (!Directory.Exists(pathToDirectory))
        {
            throw new AggregateException("Directory does not exists!");
        }

        string[] fileEntries = Directory.GetFiles(pathToDirectory);
        
        foreach (var file in fileEntries)
        {
            try
            {
                var pathArray = file.Split('/');
                if (!Equals(pathArray[pathArray.Length - 1], ".DS_Store")) AddNode(GetNodeByFilePath(file) ?? 
                    throw new AggregateException("Node is null, check filepath or json script!"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    /// <summary>
    /// Getting node from file path.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private static Node? GetNodeByFilePath(string filePath)
    {
        return JsonConvert.DeserializeObject<Node>(File.ReadAllText(filePath));
    }

    /// <summary>
    /// Find Node by file name.
    /// </summary>
    /// <param name="fileName">File name.</param>
    /// <returns>Returns the found node.</returns>
    public Node? FindNodeByName(string fileName)
    {
        Node? findNode = null;
        for(int i = 0; i < _currentSize; ++i)
        {
            // Can not be null, `cause we iterate by _currentSize.
            if (_nodes[i].FileName != fileName) continue;
            findNode = _nodes[i];
            break;
        }
        return findNode;
    }

    /// <summary>
    /// Save nodes from vault to directory.
    /// </summary>
    /// <param name="pathToDirectory">Path to directory where we will save nodes.</param>
    public void SaveToDirectory(string pathToDirectory)
    {
        for(int i = 0; i < _currentSize; ++i)
        {
            try
            {
                if (!Equals(_nodes[i], null))
                {
                    string jsonObj = JsonConvert.SerializeObject(_nodes[i], Formatting.None);
                    // Can not be null, `cause we check it on top.
                    _fileStream = new FileStream(pathToDirectory + $"/{_nodes[i].FileName}", FileMode.OpenOrCreate);
                    _fileStream.Write(Encoding.Unicode.GetBytes(jsonObj));
                    _fileStream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    
    /// <summary>
    /// Implement interface method.
    /// </summary>
    /// <returns>Return Enumerator from _nodes.</returns>
    public IEnumerator GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }
}