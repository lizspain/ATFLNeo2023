using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveCamera : MonoBehaviour
{
    WebCamTexture cam;
    //WebCamDevice [] cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = new WebCamTexture();

        GetComponent<Renderer>().material.mainTexture = cam;
        cam.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
