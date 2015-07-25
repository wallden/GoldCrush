using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{

    public Sprite GrandmaSillhouette;
    public Sprite Grandma;
    public Sprite WorkerSillhouette;
    public Sprite Worker;
    public Sprite ForemanSillhouette;
    public Sprite Foreman;
    public Sprite DrillerSillhouette;
    public Sprite Driller;
    public Sprite DiggerSillhouette;
    public Sprite Digger;
    public Sprite AlienRobotSillhouette;
    public Sprite AlienRobot;


    

    private static Dictionary<string, Sprite> _buttonImages; 
    void Start()
    {
        _buttonImages = new Dictionary<string, Sprite>
        {
            {"GrandmaSillhouette", GrandmaSillhouette},
            {"Grandma", Grandma},
            {"WorkerSillhouette", WorkerSillhouette},
            {"Worker", Worker},
            {"ForemanSillhouette", ForemanSillhouette},
            {"Foreman", Foreman},
            {"DrillerSillhouette", DrillerSillhouette},
            {"Driller", Driller},
            {"DiggerSillhouette", DiggerSillhouette},
            {"Digger", Digger},
            {"AlienRobotSillhouette", AlienRobotSillhouette},
            {"AlienRobot", AlienRobot},

        };
    }

    public static Sprite GetButtonImage(string type)
    {
        return _buttonImages[type];
    }



}
