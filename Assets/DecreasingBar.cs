using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreasingBar : MonoBehaviour
{
    public Image barImage;   // The UI Image component representing the bar
    
    public float decreaseRate = 0.1f; // The rate at which the bar decreases

    private float currentFill = 1.0f; // Start with a full bar

    public float changeAnim;


    void Start()
    {
        if (barImage == null)
        {
            Debug.LogError("Bar Image is not assigned.");
        }

    }
    void Update()
    {

            if (barImage != null)
        {
            // Decrease the fill amount over time
            currentFill -= decreaseRate * Time.deltaTime;
            currentFill = Mathf.Clamp(currentFill, 0.0f, 1.0f); // Clamp the value between 0 and 1

            // Update the bar's fill amount
            barImage.fillAmount = currentFill;
        }
    }
}
