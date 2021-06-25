using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplinationColors : MonoBehaviour
{
    [SerializeField] private Canvas _NextCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Mouse0))
        {
            _NextCanvas.gameObject.SetActive(true); //.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
