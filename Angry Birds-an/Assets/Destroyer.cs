using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        string tags = other.gameObject.tag;

        if(tags == "Bird" || tags == "Pig" || tags == "Obstacle")
        {
            Destroy(other.gameObject);
        }
    }
}
