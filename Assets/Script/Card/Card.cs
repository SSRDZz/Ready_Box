using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    public CardEffect cardEffect;
    public enum CostType{Stamina,Focus}
    public CostType costType = CostType.Stamina;
    public string description;
    public int cost;
    public int power;
    public int speed;
    public bool attack;
    public bool defense;
    public bool counter;

    public void TakeEffect()
    {
        
    }
}
