using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CPInput : Singleton<CPInput>
{
    private const int InputMoveThreshhold = 10;

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
#else
    private static Vector3 s_lastMousePosition;
#endif


    static CPInput()
    {
        var _ = Instance;
    }

    private void LateUpdate()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
#else
        s_lastMousePosition = Input.mousePosition;
#endif
    }

    public static Vector2 GetPosition()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        if (Input.touchCount != 0)
            return Input.GetTouch(0).position;
#else
        return Input.mousePosition;
#endif
        return Vector2.zero;
    }
    public static bool IsPointerMoved()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        if (Input.touchCount != 0)
            return Input.GetTouch(0).deltaPosition.sqrMagnitude > InputMoveThreshhold;
#else
        return (Input.mousePosition - s_lastMousePosition).sqrMagnitude > InputMoveThreshhold;
#endif
        return false;
    }

    public static bool GetInputDown()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        if (Input.touchCount != 0)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                return true;
#else
        if (Input.GetMouseButtonDown(0))
            return true;
#endif
        return false;
    }

    public static bool GetInput()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        if (Input.touchCount != 0)
		    if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
			    return true;
#else
        if (Input.GetMouseButton(0))
            return true;
#endif
        return false;
    }

    public static bool GetInputUp()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        if (Input.touchCount != 0)
		    if (Input.GetTouch(0).phase == TouchPhase.Ended)
			    return true;
#else
        if (Input.GetMouseButtonUp(0))
            return true;
#endif
        return false;
    }

    public static int GetInputCount()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        return Input.touchCount;
#else
        return Input.GetMouseButton(0) ? 1 : 0;
#endif
    }
}
