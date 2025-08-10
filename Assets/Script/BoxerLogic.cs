using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxerLogic
{
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int MaxStamina { get; private set; }
    public int Stamina { get; private set; }
    public int MaxFocus { get; private set; }
    public int Focus { get; private set; }

    public BoxerLogic(int maxHp = 100, int maxStamina = 10 , int maxFocus = 10)
    {
        MaxHp = maxHp;
        Hp = maxHp;
        MaxStamina = maxStamina;
        Stamina = maxStamina;
        MaxFocus = maxFocus;
        Focus = MaxFocus;
    }

    public void TakeDamage(int amount)
    {
        Hp = Mathf.Max(Hp - amount, 0);
    }

    public void RecoverStamina(int amount)
    {
        Stamina = Mathf.Min(Stamina + amount, MaxStamina);
    }
}