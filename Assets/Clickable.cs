using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    private int _hp;
    private GameMaster _gameMaster;

    public void Initialize(GameMaster gameMaster, int hp, Vector3 position)
    {
        _gameMaster = gameMaster;
        _hp = hp;
        transform.position = position;
    }

    public void OnMouseDown()
    {
        ParticleSystem.Play();

        _gameMaster.MineCurrentGround(GameMaster.MoneyPerClick);
    }

    public void RemoveHp(int amount)
    {
        _hp -= amount;
        if (_hp <= 0)
        {
            _gameMaster.GroundDestroyed(this);
            
            Destroy(gameObject);
        }
    }
}
