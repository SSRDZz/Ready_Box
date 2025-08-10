using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boxer : MonoBehaviour
{
    public BoxerLogic self;
    public Boxer opponent;

    void Start()
    {
        self = new BoxerLogic();
        Debug.Log(self.Hp);
    }

    void Update()
    {
        
    }
}
