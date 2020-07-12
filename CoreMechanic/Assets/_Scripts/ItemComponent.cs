using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public delegate void itemHandler(ItemComponent thisItem);
    public event itemHandler PickedUpEvent;

    public void PickedUp()
    {
        PickedUpEvent?.Invoke(this);
        Destroy(gameObject);
    }
    

}
