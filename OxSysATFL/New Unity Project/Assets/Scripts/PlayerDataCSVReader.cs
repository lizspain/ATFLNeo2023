using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class PlayerDataCSVReader : MonoBehaviour
{
    public string fileName;
    private string filePath;
    private string playerName;
    private string lineNumberInput;
    private int playerWeight;
    private int lineNumber;

    [SerializeField] public TextMeshProUGUI lineNumberInputField;
    [SerializeField] public TextMeshProUGUI playerNameOutput;
    [SerializeField] public TextMeshProUGUI playerWeightOutput;
    public Button readDataButton;

    private void Start()
    {
        // Get the file path of the CSV file
        filePath = Application.dataPath + "/" + fileName;

        // Add an event listener to the readDataButton
        readDataButton.onClick.AddListener(ReadData);
    }

    public void ReadData()
    {
        // Read the CSV file
        StreamReader reader = new StreamReader(filePath);

        // Get the line number from the input field
        if (int.TryParse(lineNumberInputField.text, out lineNumber))
        {
            // Find the line number and output its data
            int currentLineNumber = 1;
            while (!reader.EndOfStream && currentLineNumber <= lineNumber)
            {
                string[] data = reader.ReadLine().Split(',');
                if (currentLineNumber == lineNumber)
                {
                    // Assign the name and weight data to variables
                    playerName = data[0];
                    int.TryParse(data[1], out playerWeight);
                    Debug.Log("Player name: " + playerName + " Player weight: " + playerWeight);

                    // Update the TextMeshPro UI objects
                    playerNameOutput.text = "Name: " + playerName;
                    playerWeightOutput.text = "Weight: " + playerWeight.ToString();

                    // Exit the loop
                    break;
                }
                currentLineNumber++;
            }

            // Close the reader
            reader.Close();
        }
        else
        {
            Debug.LogError("Invalid line number format: " + lineNumberInputField.text);
        }
    }
}
