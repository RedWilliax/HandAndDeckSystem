using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundExtension
{
   
    public static bool Contains2D(this Bounds _bounds, Vector2 _vec2)
    {
        Vector2 _min = new Vector2(_bounds.min.x, _bounds.min.z);
        Vector2 _max = new Vector2(_bounds.max.x, _bounds.max.z);

        return _vec2.x <= _max.x && _vec2.x >= _min.x && _vec2.y <= _max.y && _vec2.y >= _min.y;
    }

}
