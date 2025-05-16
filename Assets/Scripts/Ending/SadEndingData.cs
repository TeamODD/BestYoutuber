using UnityEngine;

[CreateAssetMenu(fileName = "SadEndingData", menuName = "Ending/SadEndingData", order = 1)]
public class SadEndingData : ScriptableObject
{
    public SadEndingType EndingType;
    public Sprite EndingImage;
    [TextArea]
    public string Description;
    public string TargetSceneName; 
}
