using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInScreen : MonoBehaviour
{
    public Canvas NextScreen;

    public InputField Username;

    public InputField Password;
    public Android _sql;

    // Start is called before the first frame update
    void Start()
    { 
    //{
    //    _sql.insert_DayTable("D1662021", 10, 2000, 500, 10000);
    //    _sql.InsertTableName("D1662021");

    //    _sql.insert_DayTable("D1562021", 10, 0, 20000, 0);
    //    _sql.InsertTableName("D1562021");
    }

    // Update is called once per frame
        void Update()
    {
        
    }

    public void Login()
    {
        if(Username.text == "Mistrea" && Password.text == "HuizeKubus")
        {
            NextScreen.gameObject.SetActive(true); //.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
