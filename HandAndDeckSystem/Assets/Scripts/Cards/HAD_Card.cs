using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField] ECardType cardType;

    [SerializeField] ERarity rarity;

    [SerializeField] ListStatCard listStats;

    public string Name { get => name; set => name = value; }
    public string NameSprite { get => nameSprite; set => nameSprite = value; }
    public ECardType CardType { get => cardType; set => cardType = value; }
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
    public HAD_Board ItsBoard { get; set; } = null;
    public DataCard DataCard { get => dataCard; set => dataCard = value; }
    public bool SelectedCard { get; set; } = false;

    #region UIManager

    [SerializeField] new TMP_Text name;
    [SerializeField] Image sprite;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text life;
    [SerializeField] TMP_Text atck;
    [SerializeField] TMP_Text def;
    [SerializeField] Image xpBar;


    public void InitializeCard()
    {
        name.text = DataCard.Name;

        sprite.sprite = (Sprite)Resources.Load(DataCard.NameSprite);


        //Rework le system d'xp
        if (GetStat(ECardStat.Exp, out float _value))
            xpBar.fillAmount = _value;
        else
            xpBar.gameObject.SetActive(false);
        //

        SetTextOnType(cost, ECardStat.Cost);
        SetTextOnType(life, ECardStat.Life);
        SetTextOnType(atck, ECardStat.Atck);
        SetTextOnType(def, ECardStat.Def);
    }

    private void SetTextOnType(TMP_Text _text, ECardStat _type)
    {
        if (GetStat(_type, out float value))
            _text.text = $"{value: 0}";
        else
            _text.gameObject.SetActive(false);
    }

    private bool GetStat(ECardStat _stat, out float _value)
    {
        _value = -1;

        List<StatCard> allStats = DataCard.ListStats.statCards;

        for (int i = 0; i < allStats.Count; i++)
        {
            if (_stat == allStats[i].Stat)
            {
                _value = allStats[i].Data;
                return true;
            }
        }

        return false;
    }

    #endregion

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
        _board = ItsBoard;

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
