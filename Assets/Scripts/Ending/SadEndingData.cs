using UnityEngine;

[CreateAssetMenu(fileName = "SadEndingData", menuName = "Ending/SadEndingData")]
public class SadEndingData : ScriptableObject
{
    public SadEndingType EndingType;
    public Sprite EndingImage;

    [TextArea(3, 10)]
    public string Description;  
}