using UnityEngine;

[CreateAssetMenu(fileName = "SadEndingData", menuName = "Ending/SadEndingData")]
public class SadEndingData : ScriptableObject
{
    public SadEndingType EndingType;
    public Sprite EndingImage;
    public string TargetSceneName;
    public string Description;  
}