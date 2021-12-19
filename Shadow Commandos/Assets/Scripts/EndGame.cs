using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject resume;
    public GameObject settings;
    public GameObject volume;
    public GameObject slider;
    public GameObject endText;
    public GameObject disableGun;

    private void Start()
    {
        disableGun = GameObject.Find("Gun");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
            resume.SetActive(false);
            settings.SetActive(false);
            volume.SetActive(false);
            slider.SetActive(false);
            endText.GetComponent<TextMeshProUGUI>().enabled = true;
            disableGun.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
