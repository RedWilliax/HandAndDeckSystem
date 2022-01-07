using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERarity
{
    COMMON, // 72.5%
    RARE, // 15%
    EPIC, // 5%
    LEGENDARY, // 2.5%
    UNIQUE // 0.1%

}

public interface HAD_IRarity
{
    ERarity Rarity { get; set; }

}
