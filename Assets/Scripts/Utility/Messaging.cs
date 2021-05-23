using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is from lordofduct on unity forums
public static class Messaging<TDelegate> where TDelegate : Delegate
{
    #region Properties and Fields

    private static TDelegate _handle;
    public static TDelegate Trigger => _handle;

    #endregion

    #region Public Methods

    public static void Register(TDelegate callback) => _handle = Delegate.Combine(_handle, callback) as TDelegate;
    public static void Unregister(TDelegate callback) => _handle = Delegate.Remove(_handle, callback) as TDelegate;

    #endregion
}
