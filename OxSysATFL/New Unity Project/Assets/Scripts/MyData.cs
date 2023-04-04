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

   /*
    [SerializeField] private InputField stringField1;
    [SerializeField] private InputField stringField2;
    [SerializeField] private InputField stringField3;
    [SerializeField] private InputField photoFilePath;
    [SerializeField] private InputField intField1;
    [SerializeField] private Dropdown intField2;
    [SerializeField] private Button saveButton;

    private MyData data = new MyData();

 void Start()
    {
        // Add a listener to the save button
        saveButton.onClick.AddListener(SaveToJson);
    }
  */


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

   /* private void SaveToJson()
    {
        // Update the data object with the input field values
        data.stringField1 = string1;
        data.stringField2 = string2;
        data.stringField3 = string3;
        data.photoFilePath = photoFilePath.text;
        data.intField1 = int.Parse(intField1.text);
        data.intField2 = intField2.value + 1;

        // Save the data to a JSON file
        string filePath = Application.dataPath + "/data.json";
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Data saved to " + filePath);
    }
   */
}

