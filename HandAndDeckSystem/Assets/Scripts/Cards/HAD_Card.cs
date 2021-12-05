using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Rename class
public class HAD_Card : MonoBehaviour, HAD_ICard
{
    public HAD_Player Owner { get; set; }
    public Vector3 Anchor { get; set; }

    int cost = 0;

    public int Cost
    {
        get => cost;
        set => cost = value <= 0 ? 0 : value;
    }
    public ERarity Rarity { get; set; }

    public string Name { get; set; } = "DefaultCard";

    public SpriteRenderer Sprite { get; set; }

    public bool IsValid => Sprite;

    private void Start()
    {
        if (!Sprite) Sprite = GetComponentInChildren<SpriteRenderer>();

        if (!IsValid) throw new Exception("Error object in valid !");
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
