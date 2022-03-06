using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI beautyScore;
    public TextMeshProUGUI resistanceScore;
    public TextMeshProUGUI survivalScore;
    public TextMeshProUGUI waterScore;
    public TextMeshProUGUI foodScore;
    public TextMeshProUGUI energyScore;
    public TextMeshProUGUI weaponScore;

    void Start()
    {
        GameRules rules = Player.instance.rules;
        beautyScore.text ="Beauty Score : " + ScoreManager.instance.beautyScore + " * mutiplier : " + rules.beautyMultiplier + " = " + ScoreManager.instance.beautyScore * rules.beautyMultiplier;
        resistanceScore.text = "Resistance Score : " + ScoreManager.instance.resistanceScore;
        survivalScore.text = "Survival Score : " + ScoreManager.instance.survivalScore;
        waterScore.text ="Water Score : " + ScoreManager.instance.waterScore + " * mutiplier : " + rules.waterMultiplier + " = " + ScoreManager.instance.waterScore * rules.waterMultiplier;
        foodScore.text ="Food Score : " + ScoreManager.instance.foodScore + " * mutiplier : " + rules.foodMultiplier + " = " + ScoreManager.instance.foodScore * rules.foodMultiplier;
        energyScore.text ="Energy Score : " + ScoreManager.instance.energyScore + " * mutiplier : " + rules.energyMultiplier + " = " + ScoreManager.instance.energyScore * rules.energyMultiplier;
        weaponScore.text ="Weapon Score : " + ScoreManager.instance.weaponScore + " * mutiplier : " + rules.equipmentMultiplier + " = " + ScoreManager.instance.weaponScore * rules.equipmentMultiplier;
    }

    public void Restart()
    {
        //Debug.Log("restart");
        SceneManager.LoadScene("LON_Main");
    }

    public void Quit()
    {
        //Debug.Log("quit");
        Application.Quit();
    }
}
