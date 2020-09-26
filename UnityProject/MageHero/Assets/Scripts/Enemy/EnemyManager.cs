using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class EnemyManager : MonoBehaviour
{
    [SerializeField] protected Tilemap MovableMap;
    [SerializeField] protected int XStartPos;
    [SerializeField] protected int YStartPos;

    [SerializeField] protected float XStartPosWorld;
    [SerializeField] protected float YStartPosWorld;

    protected bool[,] isMovable;
    protected bool[,] isRayCast;

    public string[] surface;

    [SerializeField] protected int width = 9;
    [SerializeField] protected int height = 16;

    [SerializeField] protected float delWidth;
    [SerializeField] protected float delHeight;

    [SerializeField] protected Transform player;

    protected bool isReady = false;

    public bool GetIsReady() { return isReady; }

    protected List<EnemyController> enemy = new List<EnemyController>();

    // Start is called before the first frame update
    void Start()
    {

        isMovable = new bool[width, height];
        isRayCast = new bool[width, height];

        Debug.Log(MovableMap.GetTile(new Vector3Int(0, 0, 0)).name);///////////////////////////

        for (int x = XStartPos; x < XStartPos + width; x++)
            for (int y = YStartPos; y < YStartPos + height; y++)
                isMovable[x - XStartPos, y - YStartPos] = Contains(MovableMap.GetTile(new Vector3Int(x, y, 0)));

        delWidth = (MovableMap.CellToWorld(new Vector3Int(1, 1, 0)) - MovableMap.CellToWorld(new Vector3Int(0, 0, 0))).x;
        delHeight = (MovableMap.CellToWorld(new Vector3Int(1, 1, 0)) - MovableMap.CellToWorld(new Vector3Int(0, 0, 0))).z;

        XStartPosWorld = MovableMap.CellToWorld(new Vector3Int(XStartPos, YStartPos, 0)).x;
        YStartPosWorld = MovableMap.CellToWorld(new Vector3Int(XStartPos, YStartPos, 0)).z;

        isReady = true;

    }

    private bool Contains(TileBase tileBase)
    {

        bool isMove = false;

        for(int i = 0; i < surface.Length; i++)
        {

            if (surface[i].Equals(tileBase.name)) isMove = true;

        }

        return isMove;

    }

    protected void FillRayCastMap()
    {

        int CellsX = 0;
        int CellsY = 0;

        for (float x = XStartPosWorld; x < XStartPosWorld + width * delWidth; x += delWidth)
        {

            CellsY = 0;

            for (float y = YStartPosWorld; y < YStartPosWorld + height * delHeight; y += delHeight)
            {

                if (isMovable[CellsX, CellsY])
                {

                    RaycastHit hit;

                    transform.position = new Vector3(x + delWidth / 2, player.position.y, y + delHeight / 2);
                    Vector3 direction = player.position - transform.position;

                    if (Physics.Raycast(transform.position, direction, out hit)) isRayCast[CellsX, CellsY] = hit.transform.Equals(player);
                }
                else isRayCast[CellsX, CellsY] = false;

                CellsY++;

            }

            CellsX++;

        }


    }

    public void addEnemy(EnemyController _enemy, out int XstartCell, out int YStartCell, out float delX, out float delY, out int widht, out int height)
    {

        if (!enemy.Contains(_enemy))
            enemy.Add(_enemy);

        XstartCell = XStartPos;
        YStartCell = YStartPos;
        delX = delWidth;
        delY = delHeight;
        widht = this.width;
        height = this.height;

    }

    public void removeEnemy(EnemyController _enemy)
    {

        if (enemy.Contains(_enemy))
            enemy.Remove(_enemy);

    }
}
