using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private int _stress;
    [SerializeField] private int _famous;
    [SerializeField] private int _subscriber;

    public int Stress => _stress;
    public int Famous => _famous;
    public int Subscriber => _subscriber;

    public void SetStress(int value) => _stress -= value;
    public void SetFamous(int value) => _famous -= value;   
    public void SetSubscriber(int value) => _subscriber -= value;    
}
