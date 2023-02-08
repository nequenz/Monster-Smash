using System;
using UnityEngine;


public static class ComponentExtended
{
    public static bool Is(this Component me, Type type) => me.GetComponent(type) is not null;

    public static bool Is<T>(this Component me) => Is(me,typeof(T));
}