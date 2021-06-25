using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoData : MonoBehaviour
{
    public Canvas _calender;

    public void Return()
    {
        _calender.gameObject.SetActive(true); //.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
