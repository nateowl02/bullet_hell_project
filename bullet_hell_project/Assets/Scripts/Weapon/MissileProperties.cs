using UnityEngine;

public class MissileProperties
{
    Vector3 _position { get; }
    Vector3 _direction { get; }
    float _startSpeed { get; }
    float _endSpeed { get; }
    float _range { get; }
    float _damage { get; }
    string _damageTag { get; }
    float _spread { get; }
    float _initialDelay { get; }

    public Vector3 Position
    {
        get { return _position;  }
    }

    public Vector3 Direction 
    {
        get { return _direction; }
    }

    public float StartSpeed
    {
        get { return _startSpeed; }
    }

    public float EndSpeed
    {
        get { return _endSpeed; }
    }

    public float Range
    { 
        get { return _range; }
    }

    public float Damage
    {
        get { return _damage; }
    }

    public string DamageTag
    {
        get { return _damageTag; }
    }

    public float Spread
    {
        get { return _spread; }
    }

    public float InitialDelay
    {
        get { return _initialDelay; }
    }


    public MissileProperties(Vector3 position = default(Vector3), 
                             Vector3 direction = default(Vector3), 
                             float startSpeed = 0, 
                             float endSpeed = 0, 
                             float range = 0, 
                             float damage = 0, 
                             string damageTag = "", 
                             float spread = 0,
                             float initialDelay = 0) 
    {
        _position = position;
        _direction = direction;
        _startSpeed = startSpeed;
        _endSpeed = endSpeed;
        _range = range;
        _damage = damage;
        _damageTag = damageTag;
        _spread = spread;
        _initialDelay = initialDelay;
    }

}
