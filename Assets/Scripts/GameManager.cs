using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MeteoritesPool meteorites;
    [SerializeField]
    private Button leftRotationButton;
    [SerializeField]
    private Button rightRotationButton;
    [SerializeField]
    private Button launchApoButton;

    public bool isApocalypseLaunched = false;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void LaunchApocalypse()
    {
        isApocalypseLaunched = true;
        meteorites.LaunchMeteorites();
        leftRotationButton.gameObject.SetActive(false);
        rightRotationButton.gameObject.SetActive(false);
        launchApoButton.gameObject.SetActive(false);
    }
}
