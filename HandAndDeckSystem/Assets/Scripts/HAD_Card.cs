using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Card : MonoBehaviour
{
    Sprite sprite = null;

    public bool IsValid => sprite;

    public virtual void ActionCard() { }

}
