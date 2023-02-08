using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        //rotate the object 360 degrees by the speed per second
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
