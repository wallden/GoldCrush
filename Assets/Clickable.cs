using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{
    private int _hp;
    private GameMaster _gameMaster;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        _gameMaster.AddCurrency(1);
        RemoveHp(1);
    }
    public void RemoveHp(int amount)
    {
        _hp -= amount;
        if (_hp <= 0)
        {
            _gameMaster.GroundDestroyed();
            
            Destroy(gameObject);
        }
    }


    public void Initialize(GameMaster gameMaster, int hp, Vector3 position)
    {
        _gameMaster = gameMaster;
        _hp = hp;
        transform.position = position;
    }
}
