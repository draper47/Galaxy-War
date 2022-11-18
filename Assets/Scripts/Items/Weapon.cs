using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //new variable for item discription
    public string itemDisc;
    //new variable for item image
    public Sprite itemImage;
    //new variable for attack strength
    public int attackStrength;

    // Start is called before the first frame update
    void Start()
    {
        //Print values of the given variables
        Debug.Log("This is a " + name + ". It is " + itemDisc + ". The strenth is " + attackStrength + ".");
    }
}
