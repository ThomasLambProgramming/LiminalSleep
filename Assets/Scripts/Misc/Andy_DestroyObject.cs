using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Andy_DestroyObject : MonoBehaviour
{
    private void OnTriggerExit()
    {
        Destroy(gameObject);
    }
    //void Update()
    //{
        
    //    Destroy(gameObject);
    //    //void DestroyGameObject()
    //    //{
    //    //    Destroy(gameObject);
    //    //}
    //}       
}
