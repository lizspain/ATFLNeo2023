using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class MyData : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI playerName;
    public TMP_InputField playerinputfield;
    [SerializeField] public TextMeshProUGUI irlName;
    public TMP_InputField irlinputfield;
    [SerializeField] public TextMeshProUGUI topName;
    public TMP_InputField topinputfield;

    public string string1;
    public string string2;
    public string string3;
    public string photoFile;
    public int int1;
    public int int2;

    private string input1;
    private string input2;
    private string input3;

    private string filename = "playerregistry.csv"; // CSV filename
    private string filepath; // Full filepath to the CSV file

    private void Awake()
    {
        // Get the full filepath to the CSV file
        filepath = Application.dataPath + "/" + filename;

        // If the CSV file doesn't exist, create it with a header line
        if (!File.Exists(filepath))
        {
            string[] headers = { "Player Name", "IRL Name", "Top Name", "Photo File" };
            File.WriteAllLines(filepath, headers);
        }
    }

    public void ReadPlayerNameInput(string x)
    {
        input1 = x;
        string1 = input1;
        Debug.Log("ReadStringInput set string1 as " + string1);
    }

    public void ReadIRLNameInput(string y)
    {
        input2 = y;
        string2 = input2;
        Debug.Log("ReadStringInput set string2 as " + string2);
    }

    public void ReadTopNameInput(string z)
    {
        input3 = z;
        string3 = input3;
        Debug.Log("ReadStringInput set string3 as " + string3);
    }

    public void SaveToCSV()
    {
        // Create a list of strings to represent the data to be written to CSV
        List<string> data = new List<string>
        {
            string1 != "" ? string1 : "N/A",
            string2 != "" ? string2 : "N/A",
            string3 != "" ? string3 : "N/A",
            photoFile != "" ? photoFile : "N/A"
        };

        // Create a StreamWriter object to write to the CSV file
        StreamWriter writer = new StreamWriter(filepath, true);

        // Write the data to the CSV file as a new line
        writer.WriteLine(string.Join(",", data));

        // Close the StreamWriter object to release the file
        writer.Close();

        Debug.Log("Saved data to CSV file: " + string.Join(",", data));
    }
}
