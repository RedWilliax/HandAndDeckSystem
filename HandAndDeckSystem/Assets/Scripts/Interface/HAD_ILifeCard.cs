using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HAD_ILifeCard
{
    int Life { get; set; }

    void SetLife(int _life);

    void AddLife(int _value);

    void RemoveLife(int _value);
}
