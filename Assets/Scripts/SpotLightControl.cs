using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this script to the spot light that needs to blink

public class SpotLightControl : MonoBehaviour
{
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        // start blinking
        StartBlinking();
    }

    // call this function to make the spot light blinking
    void StartBlinking()
    {
        StartCoroutine(SchduleBlinking());
    }

    // this function define the blinking behavior
    IEnumerator SchduleBlinking()
    {
        Light spotLight = gameObject.GetComponent<Light>();

        // the pattern in the while loop will be repeated forever
        while (true)
        {
            // TODO: add your own blinking pattern here
            // >> use "spotLight.intensity = i;" to control the light intensity, 0 <= i <= 100, i should be an integer
            // >> use "yield return new WaitForSeconds(t);" to wait for t seconds, t should be a float number

            // example: light on for 0.5 seconds, then off for 0.5 seconds
            // turn on the light
            spotLight.intensity = 100;
            // wait for 0.5 seconds
            yield return new WaitForSeconds(1.5f);
            // turn off the light
            
            state = Random.Range(0, 3);
            print(state);
            if(state == 0)
            {
                spotLight.intensity = 100;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.1f);
                spotLight.intensity = 40;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.1f);

                spotLight.intensity = 100;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.1f);
                spotLight.intensity = 40;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.1f);
            }
            if(state == 1)
            {
                spotLight.intensity = 100;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.3f);
                spotLight.intensity = 50;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.1f);

                spotLight.intensity = 100;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.3f);
                spotLight.intensity = 50;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.1f);
            }
            if(state == 2)
            {
                spotLight.intensity = 100;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.1f);
                spotLight.intensity = 60;
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
