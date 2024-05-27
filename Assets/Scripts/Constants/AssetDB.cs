using ImmersivePiano.Interaction.Editor.QuickActions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDb
{
    /// <summary>
    /// The instantiated prefab will be given this name
    /// </summary>
    public readonly string DisplayName;

    /// <summary>
    /// The GUID of the prefab asset
    /// </summary>
    public readonly string AssetGUID;

    /// <param name="displayName">The instantiated GameObject will be given this name.
    /// Does not need to correspond to the prefab asset name.</param>
    /// <param name="assetGUID">The GUID of the prefab asset.</param>
    /// 

    public AssetDb(string displayName, string assetGUID)
    {
        DisplayName = displayName;
        AssetGUID = assetGUID;
    }
}

public class AssetDBs
{
    public static readonly AssetDb NoteFake =
           new AssetDb(
               "NoteFake",
               "32175a25b7375ac4d806f3cc7e04d123");
}
