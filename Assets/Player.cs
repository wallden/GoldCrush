using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    
    public static int PlayerLevel;
    public static long ExperienceRequiredToLevel;
    public static long CurrentExperiencePoints;
    public static long TotalExperiencePointsEarned;

    public Text ExperienceText;
    public Text CurrentLevelText;
    public Image ExperienceBar;
    

	
	void Start () {
        PlayerLevel = 1;
        ExperienceRequiredToLevel = 1000;
        CurrentLevelText.text = PlayerLevel.ToString();
	}
	
	
	void Update () {
        ExperienceText.text = CurrentExperiencePoints + "/" + ExperienceRequiredToLevel;
        ExperienceBar.fillAmount = GetCurrentPercentOfPlayerLevel();
        if (CurrentExperiencePoints >= ExperienceRequiredToLevel)
            LevelUp();
	}

    public void LevelUp()
    {
        PlayerLevel += 1;
        CurrentLevelText.text = PlayerLevel.ToString();

        CalculateNewLevelExperience();
        CurrentExperiencePoints = 0;
        ExperienceBar.fillAmount = 0;
    }

    [ContextMenu("Add 5 levels")]
    private void AddLevels()
    {
        for (int i = 0; i <= 5; i++)
        {
            LevelUp();
        }
    }

    public void CalculateNewLevelExperience()
    {

        var s = Mathf.RoundToInt((float)ExperienceRequiredToLevel * 0.05f);
        
        ExperienceRequiredToLevel += s;

    }
    public float GetCurrentPercentOfPlayerLevel()
    {
        var s = (CurrentExperiencePoints/(float)ExperienceRequiredToLevel);
        return s;
    }

  


}
