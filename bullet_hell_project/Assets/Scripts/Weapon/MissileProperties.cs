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
    float _trackingDelay { get; }
    float _homingDelay { get; }
    float _homingInterval { get; }
    float _homingDistance { get; }

    public Vector3 Position
    {
        get { return _position; }
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

    public float TrackingDelay
    {
        get { return _trackingDelay;  }
    }

    public float HomingDelay
    {
        get { return _homingDelay; }
    }

    public float HomingInterval
    {
        get { return _homingInterval; }
    }

    public float HomingDistance
    {
        get { return _homingDistance; }
    }

    public MissileProperties(Vector3 position = default(Vector3), 
                             Vector3 direction = default(Vector3), 
                             float startSpeed = 0, 
                             float endSpeed = 0, 
                             float range = 0, 
                             float damage = 0, 
                             string damageTag = "", 
                             float spread = 0,
                             float homingDelay = 0.0f,
                             float homingInterval = 0.0f,
                             float homingDistance = 0.0f) 
    {
        _position = position;
        _direction = direction;
        _startSpeed = startSpeed;
        _endSpeed = endSpeed;
        _range = range;
        _damage = damage;
        _damageTag = damageTag;
        _spread = spread;
        _homingDelay = homingDelay;
        _homingInterval = homingInterval;
        _homingDistance = homingDistance;
    }

}
