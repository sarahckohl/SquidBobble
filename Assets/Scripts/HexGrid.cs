using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GridTile
{

    private readonly int _column;
    private readonly int _row;
    public Bubble bubble {
        get;
        private set;
    }





    public int column
    {
        get
        {
            return _column;
        }
    }

    public int row
    {
        get
        {
            return _row;
        }
    }

    public GridTile(int xcord, int ycord, Bubble myBubble=null)
    {
        _column = xcord;
        _row = ycord;
        bubble = myBubble;
    }

    public override string ToString() {

        StringBuilder sb = new StringBuilder(3);
        sb.Append("(");

        if (bubble != null)
        {

            switch (bubble.color)
            {
                case Bubble.BubbleColor.Red:
                    sb.Append("R");
                    break;
                case Bubble.BubbleColor.Orange:
                    sb.Append("O");
                    break;
                case Bubble.BubbleColor.Yellow:
                    sb.Append("Y");
                    break;
                case Bubble.BubbleColor.Green:
                    sb.Append("G");
                    break;
                case Bubble.BubbleColor.Blue:
                    sb.Append("U");
                    break;
                case Bubble.BubbleColor.Violet:
                    sb.Append("V");
                    break;
                case Bubble.BubbleColor.Black:
                    sb.Append("B");
                    break;
                case Bubble.BubbleColor.White:
                    sb.Append("W");
                    break;
                default:
                    sb.Append(" ");
                    break;
            }
        }
        else
        { 
            sb.Append(" ");
        }
        sb.Append(")");

        return sb.ToString();

    }


}

public class HexGrid : ScriptableObject {



    //private static int StageHeight = 8;//make 11 when scaled better
    private static int _StageHeight = 8;
    public static int StageHeight
    {
        get { return _StageHeight; }
    }

    private static int _StageWidth = 8;
    public static int StageWidth
    {
        get { return _StageWidth; }
    }

    public static float StageRadius
    {
        get
        {
            return StageWidth / 2;
        }

    }


    //we can surface these... for now..
    public static float tileDiameter = 1f;

    private GridTile[][] hexArray;

    public static float tileRadius {
        get {
            return tileDiameter / 2;
        }
    }
    public static float rowHeight = .9f;
    //this might better be kept in the tile class
    public static float rowOffset = 0f;

    //


    public HexGrid() {

        Debug.Log("Creating Hexgrid of size "+HexGrid.StageHeight+","+HexGrid.StageWidth);

        hexArray = new GridTile[HexGrid.StageHeight + 1][];

        for (int i = 1; i < hexArray.Length; i++)
        {
            //logic for even/odd rows. keep handy
                int rowlength = ((i % 2 == 0) ? HexGrid.StageWidth + 1 : HexGrid.StageWidth); 
                hexArray[i] = new GridTile[rowlength];

            for (int j = 1; j < rowlength; j++)
            {
                Debug.Log("Creating bubble " + j + " of " + rowlength + "in row " + i);
                hexArray[i][j] = new GridTile(i,j);
            }

        }

        Debug.Log("COMPLETE");
    }


    public override string ToString() {

        Debug.Log("Printing HEXGRID OF SIZE "+hexArray.Length);

        StringBuilder sb = new StringBuilder(100);

        for (int i = 1; i <= HexGrid.StageHeight; i++)
        {
            if (i % 2 != 0)
            {
                sb.Append("  ");
            }

            //logic for even/odd rows. keep handy
            for (int j = 1; j<hexArray[i].Length; j++)
            {
                sb.Append(hexArray[i][j]);
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }


    //needs offsets for center-screen (0,0) origin
    public static Vector2 tilePosition(float column, float row)
    {

        float tilex = column * HexGrid.tileDiameter;

        if ((row + HexGrid.rowOffset) % 2 == 0)
        {
            tilex += HexGrid.tileDiameter / 2;
        }

        float tiley = row * HexGrid.tileDiameter;

        return new Vector2(tilex, tiley);

    }

    public void snapBubble(GameObject bubble)
    {
        Bubble bubbleObject = bubble.GetComponent<Bubble>();
        Vector2 bubblePosition = bubble.transform.position;

        int xdest;
        int ydest;

        arrayPositionFromCoordinates(bubblePosition, out xdest, out ydest);

        hexArray[xdest][ydest] = new GridTile(xdest,ydest,bubbleObject);

    }


    static void arrayPositionFromCoordinates(Vector2 position, out int column, out int row)
    {
        int xoffset = 4;
        int yoffset = 10;  //yoffset - coordinate will give grid position

        column = (int)Mathf.Floor(position.x) + xoffset;
        row = yoffset - (int)Mathf.Floor(position.y);

    }


    //finds closest HexGrid tile
    public static void getGridCoordinates(Vector2 position, out float xCord, out float yCord)
    {

        //"return" of y to ot
        yCord = Mathf.Floor(position.y / rowHeight);

        Debug.Log("yCord is: "+ yCord);
        float offset = 0;
        if ((((yCord + rowOffset) % 2) == 0))
        {
            offset = HexGrid.tileRadius;
            Debug.Log("This one gets offset by " + offset);
        }


        ///^^^Fix this. Offset needs to look more sensible, go to the side it's closer to or go with the momentum
        //"returns" of x to out
        xCord = Mathf.Floor(position.x/tileDiameter)+offset;

    }
   
}
