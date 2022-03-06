using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<MeteoritesPool> meteorites;
    [SerializeField]
    private Button launchApoButton;

    public List<BaseBlock> allAvailableBlocks;

    public ParticleSystem explosion;
    public ParticleSystem putBlock;
    public ParticleSystem removeBlock;

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
        ScoreManager.instance.CalculateScoreBeforeApo();
        isApocalypseLaunched = true;
        foreach(MeteoritesPool pool in meteorites)
            pool.LaunchMeteorites();
        launchApoButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isApocalypseLaunched)
        {
            foreach(MeteoritesPool pool in meteorites)
            {
                if (pool.hasBeenLaunched) return;
                if (!pool.CheckAllMeteoritesDown()) return;
            }
            ScoreManager.instance.CalculateScoreAfterApo();
            Debug.Log(ScoreManager.instance.beautyScore);
            Debug.Log(ScoreManager.instance.resistanceScore);
            Debug.Log(ScoreManager.instance.survivalScore);
            //Display final screen
            isApocalypseLaunched = false;
        }
    }
}
