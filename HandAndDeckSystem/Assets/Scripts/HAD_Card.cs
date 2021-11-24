using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Rename class
public class HAD_Card : MonoBehaviour, HAD_IActionCard, HAD_ICostCard, HAD_ILifeCard, HAD_IAttackCard
{
    public static event Action OnDieCard = null;
    public HAD_Player Owner { get; set; }
    public Vector3 Anchor { get; set; }

    new string name = "DefaultCard";

    int cost = 0;

    int life = 0;

    int attack = 0;

    SpriteRenderer sprite = null;

    public string Name => name;

    public int Cost
    {
        get => cost;
        set => cost = value <= 0 ? 0 : value;
    }
    public int Life
    {
        get => life;
        set
        {
            life = value <= 0 ? 0 : value;
            if (life <= 0) OnDieCard?.Invoke();
        }
    }
    public int Attack
    {
        get => attack;

        set => attack = value <= 0 ? 0 : value;
    }

    public bool IsValid => sprite;

    private void Start()
    {
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();

        if (!IsValid) throw new Exception("Error object in valid !");
    }

    public void AddLife(int _value)
    {
        SetLife(life + _value);
    }

    public void RemoveLife(int _value)
    {
        SetLife(life - _value);
    }

    public void SetLife(int _life)
    {
        Life = _life;
    }

    public virtual void ActionCard() { }

    public void SetPositon(Vector3 _newPos)
    {
        transform.position = _newPos;
    }

    public void LookAt(Vector3 _look)
    {
        Vector3 _direction = transform.position - _look;

        Quaternion _lookAt = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = _lookAt;
    }

    public bool AboveABoard(out HAD_Board _board)
    {
        _board = null;

        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit _infoRaycast, 10))
        {
            if (!_infoRaycast.collider) return false;

            _board = _infoRaycast.collider.GetComponent<HAD_Board>();

            return _board;
        }
        return false;
    }
}
