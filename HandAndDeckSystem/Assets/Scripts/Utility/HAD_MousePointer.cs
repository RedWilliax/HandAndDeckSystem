using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_MousePointer : HAD_Singleton<HAD_MousePointer>
{
    new Camera camera;

    RaycastHit infoImpact;

    public RaycastHit InfoImpact { get => infoImpact; }

    public Vector2 MousePosition2D => new Vector2(infoImpact.point.x, infoImpact.point.z);
    public Vector3 MousePosition => infoImpact.point;

    private void Awake()
    {
        base.Awake();

        camera = GetComponent<Camera>();

        if (!camera)
            Debug.LogError("Missing Camera component ! Car about that !");

    }

    private void LateUpdate()
    {
        Ray _ray = camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(_ray, out infoImpact);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(infoImpact.point, 0.125f);

    }


}
