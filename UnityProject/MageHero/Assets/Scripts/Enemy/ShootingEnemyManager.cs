using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ShootingEnemyManager : MonoBehaviour
{

    public Text test;

    [SerializeField] private Tilemap MovableMap;
    [SerializeField] private int XStartPos;
    [SerializeField] private int YStartPos;

    [SerializeField] private float XStartPosWorld;
    [SerializeField] private float YStartPosWorld;

    private bool[,] isMovable;
    private bool[,] isRayCast;

    [SerializeField] private int width = 9;
    [SerializeField] private int height = 16;

    [SerializeField] private float delWidth;
    [SerializeField] private float delHeight;

    [SerializeField] private Transform player;

    private bool isReady = false;

    public bool GetIsReady() { return isReady; }

    List<ShootingEnemyController> enemy = new List<ShootingEnemyController>();

    // Start is called before the first frame update
    void Start()
    {

        isMovable = new bool[width, height];
        isRayCast = new bool[width, height];

        for (int x = XStartPos; x < XStartPos + width; x++)
            for (int y = YStartPos; y < YStartPos + height; y++)
                isMovable[x - XStartPos, y - YStartPos] = MovableMap.GetTile(new Vector3Int(x, y, 0)) != null;

        delWidth = (MovableMap.CellToWorld(new Vector3Int(1, 1, 0)) - MovableMap.CellToWorld(new Vector3Int(0, 0, 0))).x;
        delHeight = (MovableMap.CellToWorld(new Vector3Int(1, 1, 0)) - MovableMap.CellToWorld(new Vector3Int(0, 0, 0))).z;

        XStartPosWorld = MovableMap.CellToWorld(new Vector3Int(XStartPos, YStartPos, 0)).x;
        YStartPosWorld = MovableMap.CellToWorld(new Vector3Int(XStartPos, YStartPos, 0)).z;

        isReady = true;

    }

    private void Update()
    {

        List<ShootingEnemyController> needRaycastMap = new List<ShootingEnemyController>();

        for (int i = 0; i < enemy.Count; i++) if (!enemy[i].raycast(player)) needRaycastMap.Add(enemy[i]);

        if (needRaycastMap.Count != 0)
        {
         
            FillRayCastMap();

            for (int i = 0; i < needRaycastMap.Count; i++) needRaycastMap[i].Move(isRayCast, MovableMap);

        }



        int TrueTest = 0;
        int FalseTest = 0;

        for (int i = 0; i < isRayCast.GetLength(0); i++)
            for (int l = 0; l < isRayCast.GetLength(1); l++)
                if (isRayCast[i, l]) TrueTest++;
                else FalseTest++;

        test.text = string.Format("Raycasts: true - {0} || false - {1}\nFrameTime - {2} || FPS - {3}\n{4}", TrueTest, FalseTest, Time.deltaTime, (int)(1 / Time.deltaTime), needRaycastMap.Count != 0);

    }


    void FillRayCastMap()
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
                    Debug.DrawRay(transform.position, direction.normalized*hit.distance, isRayCast[CellsX, CellsY]?Color.green : Color.red);
                }
                else isRayCast[CellsX, CellsY] = false;

                CellsY++;

            }

            CellsX++;

        }
               

    }


    public void addEnemy(ShootingEnemyController _enemy, out int XstartCell, out int YStartCell, out float delX, out float delY, out int widht, out int height)
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

    public void removeEnemy(ShootingEnemyController _enemy)
    {

        if (enemy.Contains(_enemy))
            enemy.Remove(_enemy);

    }

}
