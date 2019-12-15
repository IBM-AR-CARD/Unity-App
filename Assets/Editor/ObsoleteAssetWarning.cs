using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ObsoleteAssetWarning
{
    static ObsoleteAssetWarning()
    {
        Debug.LogWarning(
            "The Mac-version has merged with the single package for both Windows and Mac. Please download the Free-to-use package at http://u3d.as/kwj.");
    }
}