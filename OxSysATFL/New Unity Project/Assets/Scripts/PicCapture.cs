using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.WebCam;
using System.IO;

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
}

