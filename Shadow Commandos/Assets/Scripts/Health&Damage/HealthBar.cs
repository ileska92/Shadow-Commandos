using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;
    public PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        healthText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       healthText.text = slider.value.ToString("0");
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
