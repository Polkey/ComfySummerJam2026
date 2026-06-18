using JetBrains.Annotations;
using System;
using UnityEngine;

[Serializable]
public struct TutorialPopupData : IPopupData
{
    public float Duration => duration;

    public float duration;
    public string text;
    public Sprite sprite;
}