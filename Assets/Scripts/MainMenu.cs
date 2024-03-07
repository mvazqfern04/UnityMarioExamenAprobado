using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject recordsPanel, menuPanel;
    [SerializeField] TMPro.TextMeshProUGUI textRecord;



    public void PlayButton()
    {
        SceneManager.LoadScene("Nivel1");
        audioSource.Play();
    }


    public void ShowRecords()
    {
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            recordsPanel.SetActive(true);
        }
        List<int> texto = SaveManager.LoadRecord();
        //texto.Sort();

        string quePoner = "";

        /*foreach (int i in texto)
        {
            quePoner += i+"\n";
        }
        
        textRecord.text = quePoner;*/
        textRecord.text = "1:" + texto[0]+"\n";
        textRecord.text += "2:" + texto[1] + "\n";
        textRecord.text += "3:" + texto[2] + "\n";
        textRecord.text += "4:" + texto[3] + "\n";
        textRecord.text += "5:" + texto[4] + "\n";

        Debug.Log(texto.Count);
        //textRecord.text = "1: " + SaveManager.LoadRecord().ToString();
    }

    public void ShowMenu()
    {
        if (recordsPanel.activeSelf)
        {
            recordsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }
}
