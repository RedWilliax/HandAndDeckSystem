using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum ECardType
{
    NONE,
    Unite,
    Spell,
    Stuff
}

public enum ECardStat
{
    NONE,
    Atck,
    Def,
    Life,
    Cost,
    Exp

}

[Serializable]
public struct StatCard
{
    [SerializeField] ECardStat stat;

    [SerializeField] float data;

    public float Data { get => data; set => data = value; }
    public ECardStat Stat { get => stat; set => stat = value; }
}

[Serializable]
public struct ListStatCard
{
    [SerializeField] public List<StatCard> statCards;

    public ListStatCard(List<StatCard> statCards)
    {
        this.statCards = statCards;
    }
}

[Serializable]
public struct DataCard
{
    [SerializeField] string name;

    [SerializeField] string nameSprite;

    [SerializeField] ERarity rarity;

    [SerializeField] ListStatCard listStats;

    public string Name { get => name; set => name = value; }
    public string NameSprite { get => nameSprite; set => nameSprite = value; }
    public ERarity Rarity { get => rarity; set => rarity = value; }
    public ListStatCard ListStats { get => listStats; set => listStats = value; }

    #region ManageStat

    public void AddStat(StatCard _stat)
    {
        ManageStatCard(true, _stat);
    }

    public void RemoveStat(StatCard _stat)
    {
        ManageStatCard(false, _stat);
    }

    private void ManageStatCard(bool _add, StatCard _stat)
    {
        if (_add ? ExistStat(_stat) : !ExistStat(_stat)) return;

        if (_add)
            ListStats.statCards.Add(_stat);
        else
            ListStats.statCards.Remove(_stat);
    }

    private bool ExistStat(StatCard stat)
    {
        return ListStats.statCards.Any(n => n.Stat == stat.Stat) || stat.Stat == ECardStat.NONE;
    }

    #endregion

}

//Rename class
public class HAD_Card : MonoBehaviour
{
    DataCard dataCard;

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
