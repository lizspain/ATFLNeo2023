using UnityEngine;
using System.IO.Ports;
using System.Collections;

public class DYMOPostalScaleReader : MonoBehaviour
{
    // Scale name and read interval
    public string scaleName = "M25 25 lb Digital Postal Scale";
    public float readInterval = 0.1f; // in seconds

    public int int1; // Public integer variable to store the scale value

    private SerialPort serialPort;
    private float timer;

    void Start()
    {
        // Find the serial port for the scale
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports)
        {
            string deviceName = GetDeviceName(port);
            if (deviceName == scaleName)
            {
                // Open the serial port
                serialPort = new SerialPort(port, 9600);
                serialPort.Open();
                serialPort.ReadTimeout = 100;
                break;
            }
        }

        if (serialPort == null)
        {
            Debug.LogError("Could not find serial port for " + scaleName);
            return;
        }

        timer = 0f;
    }

    void Update()
    {
        // Read data from the serial port at the specified interval
        timer += Time.deltaTime;
        if (timer >= readInterval)
        {
            try
            {
                string scaleData = serialPort.ReadLine(); // Read a line of data from the serial port
                int.TryParse(scaleData, out int1); // Parse the data as an integer and store it in int1
                timer = 0f;
            }
            catch (System.TimeoutException)
            {
                // Handle timeout exception
                Debug.LogWarning("Timeout reading from DYMO postal scale.");
            }
        }
    }

    void OnDestroy()
    {
        // Close the serial port when the script is destroyed
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    private string GetDeviceName(string portName)
    {
        // Get the friendly name for the device associated with the specified COM port
        string deviceName = "";
        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Enum\USB");
        foreach (string subKeyName in key.GetSubKeyNames())
        {
            Microsoft.Win32.RegistryKey subKey = key.OpenSubKey(subKeyName);
            string[] portNames = subKey.GetValue("PortName") as string[];
            if (portNames != null && portNames.Contains(portName))
            {
                deviceName = subKey.GetValue("FriendlyName") as string;
                break;
            }
        }
        return deviceName;
    }
}
