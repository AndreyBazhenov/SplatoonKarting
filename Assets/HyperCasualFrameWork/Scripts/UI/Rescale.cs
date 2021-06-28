using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float multiplier = transform.parent.GetComponent<RectTransform>().rect.height / 360f;

        GetComponent<RectTransform>().localScale = new Vector3(multiplier, multiplier, multiplier);
    }
}
