using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{

    public GameObject indstillingerPanel;

    public void StartGame()
    {
        Toolbox.FindRequiredComponent<GameMaster>().InitiateStory();
    }

    public void Indstillinger()
    {
        indstillingerPanel.SetActive(true);
    }

    public void Afslut()
    {
        Application.Quit();
    }
}