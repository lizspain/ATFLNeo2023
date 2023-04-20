using System.Linq;
using UnityEngine.Windows.WebCam;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PicCapture : MonoBehaviour
{
    private void Start()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    private PhotoCapture photoCaptureObject = null;

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }


    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk!");
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }
}


/*
public class PicCapture : MonoBehaviour
{
    [SerializeField] private RawImage imageDisplay;
    [SerializeField] private int imageWidth = 512;
    [SerializeField] private int imageHeight = 512;
    [SerializeField] private string savePath = "Assets/Images/";
    [SerializeField] private Button captureButton;

    public WebCamTexture webCamTexture;
    private Texture2D capturedImage;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            webCamTexture = new WebCamTexture(devices[0].name, imageWidth, imageHeight);
            webCamTexture.Play();
            Debug.Log("Webcam found");
        }
        else
        {
            Debug.LogError("No webcam found!");
        }

        captureButton.onClick.AddListener(TakePicture);
    }

    public void TakePicture()
    {
        capturedImage = new Texture2D(imageWidth, imageHeight);
        capturedImage.SetPixels(webCamTexture.GetPixels());
        capturedImage.Apply();

        byte[] bytes = capturedImage.EncodeToPNG();
        string filename = "capture_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        File.WriteAllBytes(savePath + filename, bytes);
        Debug.Log("Image saved to " + savePath + filename);

        imageDisplay.texture = capturedImage;
    }
}


/*
public class PicCapture : MonoBehaviour
{
    public RawImage rawImage; // Reference to the RawImage object for displaying the photo
    public string saveFolderPath; // Folder path to save the photo
    public string photoFile; // Variable to store the filepath of the webcam photo

    private WebCamTexture webcamTexture; // Reference to the WebCamTexture

    private void Awake()
    {
        // Initialize the WebCamTexture
        webcamTexture = new WebCamTexture();
        // Set the RawImage texture to the WebCamTexture
        rawImage.texture = webcamTexture;

     
            // Get the available webcam devices
            WebCamDevice[] devices = WebCamTexture.devices;

            // Check if any webcams are available
            if (devices.Length > 0)
            {
                // You can specify the webcam device by name
                // Example: webCamTexture = new WebCamTexture(devices[0].name, webcamWidth, webcamHeight, webcamFPS);
                // Or you can specify the webcam device by index
                // Example: webCamTexture = new WebCamTexture(devices[0].name, webcamWidth, webcamHeight, webcamFPS);

                // Replace the index or name with the appropriate value for your webcam
                webCamTexture = new WebCamTexture(devices[0].name, webcamWidth, webcamHeight, webcamFPS);

                // Set the rawImage texture to display the webcam feed
                rawImage.texture = webCamTexture;

                // Start the webcam
                webCamTexture.Play();
            }
            else
            {
                Debug.LogError("No webcam device found.");
            }
 

    }

    private void Start()
    {
        // Start the WebCamTexture
        webcamTexture.Play();
    }

    public void TakePhoto()
    {
        // Check if the WebCamTexture is running
        if (webcamTexture.isPlaying)
        {
            // Stop the WebCamTexture to freeze the frame
            webcamTexture.Stop();
            // Capture the photo
            StartCoroutine(CapturePhoto());
        }
        else
        {
            Debug.LogError("WebCamTexture is not running.");
        }
    }

    private IEnumerator CapturePhoto()
    {
        // Wait for one frame to ensure the webcam is properly stopped
        yield return new WaitForEndOfFrame();

        // Check if the WebCamTexture is running
        if (webcamTexture.isPlaying)
        {
            // Create a new Texture2D with the same dimensions as the RawImage
            Texture2D texture = new Texture2D(rawImage.texture.width, rawImage.texture.height);
            // Read the pixels from the RawImage texture into the Texture2D
            texture.SetPixels(webcamTexture.GetPixels());
            texture.Apply();

            // Save the Texture2D as a PNG file
            byte[] bytes = texture.EncodeToPNG();
            string filename = "photo_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            string filePath = System.IO.Path.Combine(saveFolderPath, filename);
            System.IO.File.WriteAllBytes(filePath, bytes);

            // Update the photoFile variable with the filepath of the saved photo
            photoFile = filePath;

            // Update the RawImage texture with the captured photo
            rawImage.texture = texture;

            // Restart the WebCamTexture to resume live streaming
            webcamTexture.Play();
        }
        else
        {
            Debug.LogError("WebCamTexture is not running.");
        }
    }
}




public class PicCapture : MonoBehaviour
{
    [SerializeField] private RawImage imageDisplay;
    [SerializeField] private int imageWidth = 512;
    [SerializeField] private int imageHeight = 512;
    [SerializeField] private string savePath = "Assets/Images/";
    [SerializeField] private Button captureButton; // Add this field to reference the button

    public WebCamTexture webCamTexture;
    private Texture2D capturedImage;

  void Start()
    {
     // Get a list of available webcams and select the first one
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            webCamTexture = new WebCamTexture(devices[0].name, imageWidth, imageHeight);
            webCamTexture.Play();
            Debug.Log("Webcam found");
        }
        else
        {
            Debug.LogError("No webcam found!");
        }


        // Add a listener to the capture button
        captureButton.onClick.AddListener(TakePicture);
    }
 

   public void TakePicture()
    {
        // Stop the webcam and capture an image
        webCamTexture.Stop();
        capturedImage = new Texture2D(imageWidth, imageHeight);
        capturedImage.SetPixels(webCamTexture.GetPixels());
        capturedImage.Apply();

        // Save the captured image to disk
        byte[] bytes = capturedImage.EncodeToPNG();
        string filename = "capture_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        File.WriteAllBytes(savePath + filename, bytes);
        Debug.Log("Image saved to " + savePath + filename);

        // Display the captured image on screen
        imageDisplay.texture = capturedImage;

        // Restart the webcam
        webCamTexture.Play();
    }

/*
    //Other code set below
    PhotoCapture photoCaptureObject = null;

    static readonly int TotalImagesToCapture = 3;
    int capturedImageCount = 0;

    // Use this for initialization
    void Start()
    {
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            Debug.Log("Created PhotoCapture Object");
            photoCaptureObject = captureObject;

            CameraParameters c = new CameraParameters();
            c.hologramOpacity = 0.0f;
            c.cameraResolutionWidth = targetTexture.width;
            c.cameraResolutionHeight = targetTexture.height;
            c.pixelFormat = CapturePixelFormat.BGRA32;

            captureObject.StartPhotoModeAsync(c, delegate (PhotoCapture.PhotoCaptureResult result) {
                Debug.Log("Started Photo Capture Mode");
                TakePicture();
            });
        });
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        Debug.Log("Saved Picture To Disk!");

        if (capturedImageCount < TotalImagesToCapture)
        {
            TakePicture();
        }
        else
        {
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
    }

    void TakePicture()
    {
        capturedImageCount++;
        Debug.Log(string.Format("Taking Picture ({0}/{1})...", capturedImageCount, TotalImagesToCapture));
        string filename = string.Format(@"CapturedImage{0}.jpg", capturedImageCount);
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

        photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;

        Debug.Log("Captured images have been saved at the following path.");
        Debug.Log(Application.persistentDataPath);
    }
*/
