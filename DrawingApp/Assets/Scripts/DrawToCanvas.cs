using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawToCanvas : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RawImage _canvas;

    private Texture2D texture;

    [SerializeField]private Canvas _mainCanvas;

    [SerializeField] private Android sql;

    [SerializeField] private Canvas NextScreen;


    private int _brushSize = 10;

    [SerializeField] private Color Verdriet = new Color(72f / 225f, 68f / 225f, 173f / 225f);
    [SerializeField] private Color BlijHeid = new Color(72f / 225f, 68f / 225f, 173f / 225f);
    [SerializeField] private Color Boosheid = new Color(72f / 225f, 68f / 225f, 173f / 225f);
    [SerializeField] private Color Angst = new Color(72f / 225f, 68f / 225f, 173f / 225f);

    // Blue
    private Color brushColor = new Color(72f/225f, 68f / 225f, 173f / 225f);

    public void OnValueChanged(float value)
    {
        _brushSize = (int)value;

        Debug.Log(value);
    }

    public void SetToBlue()
    {
        brushColor = Verdriet;
    }

    public void SetToYellow()
    {
        brushColor = BlijHeid;
    }

    public void SetToRed()
    {
        brushColor = Boosheid;
    }

    public void SetToGreen()
    {
        brushColor = Angst;
    }

    public void OnBeginDrag(PointerEventData data)
    {
      //  Debug.Log("OnBeginDrag: " + data.position);
    }

    public void OnDrag(PointerEventData data)
    {
        _canvas.texture = texture;
        

        Vector3 position = Input.mousePosition;

        Vector3 canvasTouchDiff = _canvas.transform.position - new Vector3(position.x, position.y, 0);

        Vector3[] corners = new Vector3[4];
        _canvas.rectTransform.GetWorldCorners(corners);
        Rect newRect = new Rect(corners[0], (corners[2] - corners[0]));
      //  Debug.Log(newRect.Contains(Input.mousePosition));

        if (newRect.Contains(position))
        {

            float xPositionDeltaPoint = (position.x);
            float yPositionDeltaPoint = (position.y);


            Vector2 localCursor = Vector2.zero;
            //This method transforms the mouse position, to a position relative to the image's pivot
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), data.position, data.pressEventCamera, out localCursor))
                return;

            float rectToPixelScale = _canvas.texture.width / GetComponent<RectTransform>().rect.width;
            localCursor = new Vector2(localCursor.x * rectToPixelScale, localCursor.y * rectToPixelScale);


            Circle(texture, (int)localCursor.x + (1024/2), (int)localCursor.y + (1024 / 2), _brushSize, brushColor);
        }
        texture.Apply();

}

    public void OnEndDrag(PointerEventData data)
    {
      //      Debug.Log("OnEndDrag: " + data.position);
    }



    private void Start()
    {
        Debug.Log(System.DateTime.Now);
        _canvas = GetComponent<RawImage>();

        texture = new Texture2D(1024, 1024);
    }

  
    public void Circle(Texture2D tex, int cx, int cy, int r, Color col)
    {
        int x, y, px, nx, py, ny, d;
        Color[] tempArray = tex.GetPixels();

        for (x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
            for (y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tempArray[py * 1024 + px] = col;
                tempArray[py * 1024 + nx] = col;
                tempArray[ny * 1024 + px] = col;
                tempArray[ny * 1024 + nx] = col;
            }
        }
        tex.SetPixels(tempArray);
        tex.Apply();
    }
    public void onEnter()
    {
        Texture2D result = (Texture2D)_canvas.texture;

        int blue_amount = 0;
        int yellow_amount = 0;
        int red_amount = 0;
        int green_amount = 0;

       // Color help = new Color();

        for (int x = 0; x < result.width; x++)
        {
            for (int y = 0; y < result.height; y++)
            {
                //help = texture.GetPixel(x, y);   

                if (texture.GetPixel(x, y).r >= Verdriet.r - 0.1f &&
                    texture.GetPixel(x, y).r <= Verdriet.r + 0.1f &&
                    texture.GetPixel(x, y).g >= Verdriet.g - 0.1f&&
                    texture.GetPixel(x, y).g <= Verdriet.g + 0.1f &&
                     texture.GetPixel(x, y).b >= Verdriet.b - 0.1f &&
                    texture.GetPixel(x, y).b <= Verdriet.b + 0.1f) blue_amount++;

                else if (texture.GetPixel(x, y).r >= BlijHeid.r - 0.1f &&
                    texture.GetPixel(x, y).r <= BlijHeid.r + 0.1f &&
                    texture.GetPixel(x, y).g >= BlijHeid.g - 0.1f &&
                    texture.GetPixel(x, y).g <= BlijHeid.g + 0.1f &&
                     texture.GetPixel(x, y).b >= BlijHeid.b - 0.1f &&
                    texture.GetPixel(x, y).b <= BlijHeid.b + 0.1f) yellow_amount++;

                else if (texture.GetPixel(x, y).r >= Boosheid.r - 0.1f &&
                    texture.GetPixel(x, y).r <= Boosheid.r + 0.1f &&
                    texture.GetPixel(x, y).g >= Boosheid.g - 0.1f &&
                    texture.GetPixel(x, y).g <= Boosheid.g + 0.1f &&
                     texture.GetPixel(x, y).b >= Boosheid.b - 0.1f &&
                    texture.GetPixel(x, y).b <= Boosheid.b + 0.1f) red_amount++;

                else if (texture.GetPixel(x, y).r >= Angst.r - 0.1f &&
                    texture.GetPixel(x, y).r <= Angst.r + 0.1f &&
                    texture.GetPixel(x, y).g >= Angst.g - 0.1f &&
                    texture.GetPixel(x, y).g <= Angst.g + 0.1f &&
                     texture.GetPixel(x, y).b >= Angst.b - 0.1f &&
                    texture.GetPixel(x, y).b <= Angst.b + 0.1f) green_amount++;

            }
        }

        _canvas.texture = result;

        string date = System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString();

        sql.insert_DayTable("D" + date, blue_amount, yellow_amount, red_amount, green_amount);
        sql.InsertTableName("D" + date);


        NextScreen.gameObject.SetActive(true); //.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
