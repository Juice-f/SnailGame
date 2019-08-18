using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitchScript : MonoBehaviour
{

    public GameObject PreviousScene, NextScene;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PreviousScene.SetActive(false);
            NextScene.SetActive(true);
        }


    }
}
