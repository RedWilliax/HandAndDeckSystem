using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ECardStat
{
    NONE,
    Atck,
    Def,
    Cost,
    Exp

}

public struct StatCard
{
    ECardStat stat;

    float data;

    public float Data { get => data; set => data = value; }
    public ECardStat Stat { get => stat; set => stat = value; }
}

//Rename class
public class HAD_Card : MonoBehaviour
{
    List<StatCard> allStats = new List<StatCard>();

    public string Name { get; set; } = "Default_Name";
    public ERarity Rarity { get; set; } = ERarity.COMMON;
    public HAD_Player Owner { get; set; }
    public Vector3 Anchor { get; set; }
 
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

            HAD_Board _currentboard = _infoRaycast.collider.GetComponent<HAD_Board>();

            if (_currentboard)
                _board = _currentboard;
            else
                return false;
        }
        else
            return false;

        return true;
    }
}
