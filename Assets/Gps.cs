using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gps : MonoBehaviour
{
    // Start is called before the first frame update
    //public Text myLatText;
    //public Text myLonText;
    public static float myLattitude;
    public static float myLongitude;
    public static Gps instance;
    public void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void FixedUpdate()
    {
        if(Input.location.isEnabledByUser)
        {
            myLattitude= Input.location.lastData.latitude ;
            myLongitude= Input.location.lastData.longitude ;
           
        }
        //Debug.Log(myLattitude);
        //Debug.Log(myLongitude);
       // myLatText.text = myLattitude.ToString();
        //myLonText.text = myLongitude.ToString();

    }
    IEnumerator Start()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        // Starts the location service.
        Input.location.Start(5f,20f);

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stops the location service if there is no need to query location updates continuously.
        //Input.location.Stop();
    }
}
