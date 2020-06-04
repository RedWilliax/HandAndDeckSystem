using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Hand : HAD_Container
{
    public event Action OnDrawCard = null;

    public HAD_Hand(int maxCards) : base(maxCards) 
    {
    }

    public override bool AddCard(HAD_Card _card)
    {
        bool _return = base.AddCard(_card);

        OnDrawCard?.Invoke();

        return _return;
    }
}
