using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_InputManager : HAD_Singleton<HAD_InputManager>
{
    public static event Action<bool> OnLMBClick = null;
    public static event Action<bool> OnRMBClick = null;
    public static event Action<bool> OnLMBUnClick = null;

    bool LMBPressed => Input.GetMouseButtonDown(0);
    bool RMBPressed => Input.GetMouseButtonDown(1);

    bool LMBUnpressed => Input.GetMouseButtonUp(0);

    private void Update()
    {
        OnLMBClick?.Invoke(LMBPressed);
        OnRMBClick?.Invoke(RMBPressed);
        OnLMBUnClick?.Invoke(LMBUnpressed);
    }

}
