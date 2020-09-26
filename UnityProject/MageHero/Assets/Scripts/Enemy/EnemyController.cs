using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent AI;
    [SerializeField] protected GameObject EnemyManager;

    protected int XStartCell;
    protected int YStartCell;
    protected float delX;
    protected float delY;
    protected int widht;
    protected int height;

    protected Vector3 moveVector;

    public abstract bool raycast(Transform target);

    public abstract void Move(bool[,] RaycastMap, Tilemap MovableMap);
}
