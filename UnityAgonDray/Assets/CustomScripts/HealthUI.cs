using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private NewPlayerController playerScript;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = playerScript.health;
    }

    // Update is called once per frame
    void Update()
    {
            slider.value = playerScript.health;
    }

    void LateUpdate()
    {
        
    }
}
