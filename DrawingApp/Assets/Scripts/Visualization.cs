using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualization : MonoBehaviour
{
    [SerializeField] private Android _sql;
    [SerializeField] private Transform _pointPos;
    [SerializeField] private Text _date;
    [SerializeField] private Slider _slider;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private Canvas _calendar;

    private Vector2 Position1 = new Vector2(300, 200);
    private Vector2 Position2 = new Vector2(-80, 500);
    private Vector2 Position3 = new Vector2(0, 0);


    private string[] testStrings;
    private List<string> TableList = new List<string>();
    private int[] currInfo = new int[4];
    public string clickedDay = " ";

    private List<int[]> infoList = new List<int[]>();

    // Start is called before the first frame update
    void Start()
    {
        //   _sql.InsertTableName("Hello");

        //  testStrings = _sql.read_tableNames();
        


        TableList = _sql.Tables;

        _slider.maxValue = TableList.Count - 1;
        _slider.value = TableList.IndexOf(clickedDay);


        currInfo = _sql.read_color(clickedDay);

        for (int i = 0; i < _slider.maxValue + 1; i++)
        {
            currInfo = _sql.read_color(TableList[i]);
            infoList.Add(currInfo);
        }

        currInfo[0] = 10000;
        currInfo[1] = 500;
        currInfo[2] = 2000;
        currInfo[3] = 0;

        infoList[0] = currInfo; 

        _date.text = clickedDay;

      //  SetCursorPosition();

        //Get index clicked day
    }

    private void SetCursorPosition(int[] info)
    {
     //   _pointPos.position = new Vector3(0, 0, 0);

        float bluePos =  (info[0] / (1024 * 2));
        float YellowPos = (info[1] / (1024 * 2));
        float redPos = (info[2] / (1024 * 2));
        float greenPos = (info[3] / (1024 * 2));

        float X = (_background.sprite.texture.width/2) - bluePos + YellowPos - redPos + greenPos;

        float Y = (_background.sprite.texture.height / 2) + bluePos + YellowPos - redPos - greenPos;

        _pointPos.position = Vector3.Lerp(_pointPos.position, new Vector3(X, Y, _pointPos.position.z), Time.deltaTime);
    }

    

    // Update is called once per frame
    void Update()
    {
       // currInfo = _sql.read_color(TableList[1]);

        _date.text = TableList[(int)_slider.value];

        _date.text = _date.text[1].ToString() + _date.text[2].ToString() + " - " + _date.text[3].ToString() + " - "  + _date.text.Substring(4, 4);

        // SetCursorPosition(infoList[(int)_slider.value]);

        // Debug.Log((int)_slider.value);

        //_slider.value = TableList.IndexOf(clickedDay);
        // Debug.Log(TableList[1]);

        // get color out of table

        //_pointPos.position = Vector3.Lerp(_pointPos.position, new Vector3(X, Y, _pointPos.position.z), Time.deltaTime);

        switch(_slider.value)
        {
            case 0:
                _pointPos.position = Vector3.Lerp(_pointPos.position, new Vector2(Position1.x + (_background.sprite.texture.width / 2), Position1.y + (_background.sprite.texture.height / 2)), Time.deltaTime);
                break;
            case 1:
                _pointPos.position = Vector3.Lerp(_pointPos.position, new Vector2(Position2.x + (_background.sprite.texture.width / 2), Position2.y + (_background.sprite.texture.height / 2)), Time.deltaTime);
                break;
            case 2:
                _pointPos.position = Vector3.Lerp(_pointPos.position, new Vector2(Position3.x + (_background.sprite.texture.width / 2), Position3.y + (_background.sprite.texture.height / 2)), Time.deltaTime);
                break;
        }
    }

    public void GoBack()
    {
        _calendar.gameObject.SetActive(true); //.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
