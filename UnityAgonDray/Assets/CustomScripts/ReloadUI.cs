using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReloadUI : MonoBehaviour
{
    private Transform cam;
    [SerializeField] private CannonFireScript CFScript;
    [SerializeField] private Slider slider;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float flashSpeed = 1f;
    private int flashDirection = -1;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CFScript.reloadVariable <= 0f) canvas.enabled = false;
        else
        {
            canvas.enabled = true;
            slider.value = CFScript.reloadVariable;
            this.transform.LookAt(transform.position + cam.forward);
            text.color = new Color(0, 0, 0, text.color.a + flashSpeed * flashDirection * Time.deltaTime);
            if (text.color.a >= 1) flashDirection = -1;
            else if (text.color.a <= .33) flashDirection = 1;
        }
    }

    void LateUpdate()
    {
        
    }
}
