using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public static class WorldHelper
{
    private static Camera _camera;

    private static Vector2 s_worldSize;

    public static Vector2 WorldSize => s_worldSize;
    public static Camera Camera => _camera ??= Camera.main;


    static WorldHelper()
    {
        float height = 2f * Camera.orthographicSize;
        float width = height * Camera.aspect;
        s_worldSize = new Vector2(width, height);
    }
}
