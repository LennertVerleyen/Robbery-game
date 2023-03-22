using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore 
{
    
    public Vector3 LeftEdge { get; set; }
    public Vector3 RightEdge { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }

    public static DataStore Instance
    {
        get
        {
            if (instance == null) instance = new DataStore();
            return instance;
        }
    }
    private static DataStore instance;
    private DataStore() { }
}
