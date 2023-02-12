using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.WSA;

/// <summary>
/// 地图生成器
/// </summary>
namespace RobotFighting
{
    public class MapGenerator : MonoBehaviour
    {
//    public Transform tilePrefab;
//    public Transform obstaclePrefab;
//    public Vector2 mapSize;

//    [Range(0, 1)] 
//    public float outlinePercent;
//    [Range(0, 1)] 
//    public float obstaclePercent;

//    private List<Coord> allTileCoords;
//    private Queue<Coord> shuffledTileCoords;

//    public int seed = 10;
//    private Coord mapCentre;

//    private void Start()
//    {
//        GenerateMap();
//    }

//    public void GenerateMap()
//    {
//        allTileCoords = new List<Coord>();
//        for (int x = 0; x < mapSize.x; x++)
//        {
//            for (int y = 0; y < mapSize.y; y++)
//            {
//                allTileCoords.Add(new Coord(x, y));
//            }
//        }

//        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));
//        mapCentre = new Coord((int) mapSize.x / 2, (int) mapSize.y / 2);

//        String holderName = "Generated Map";
//        if (transform.Find(holderName))
//        {
//            DestroyImmediate(transform.Find(holderName).gameObject);
//        }

//        Transform mapHolder = new GameObject(holderName).transform;
//        mapHolder.parent = transform;

//        for (int x = 0; x < mapSize.x; x++)
//        {
//            for (int y = 0; y < mapSize.y; y++)
//            {
//                Vector3 tilePosition = CoordToPositon(x, y);
//                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
//                newTile.localScale = Vector3.one * (1 - outlinePercent);
//                newTile.parent = mapHolder;
//            }
//        }

//        bool[,] obstacleMap = new bool[(int) mapSize.x, (int) mapSize.y];
//        int obstacleCount = (int) (mapSize.x * mapSize.y * obstaclePercent);
//        int currentObstacleCount = 0;

//        for (int i = 0; i < obstacleCount; i++)
//        {
//            Coord randomCoord = GetRandomCoord();
//            obstacleMap[randomCoord.x, randomCoord.y] = true;
//            currentObstacleCount++;

//            if (randomCoord != mapCentre && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
//            {
//                Vector3 obstaclePosition = CoordToPositon(randomCoord.x, randomCoord.y);

//                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * .5f, Quaternion.identity) as Transform;
//                newObstacle.parent = mapHolder;
//            }
//            else
//            {
//                obstacleMap[randomCoord.x, randomCoord.y] = false;
//                currentObstacleCount--;
//            }
//        }
//    }

//    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
//    {
//    private bool[,] mapFlags = new bool[obstacleMap.GetLenth(0), obstacleMap.GetLenth(1)];
//    private Queue<Coord> queue = new Queue<Coord>();

//    Queue.Enqueue(mapCentre);
//    mapFlags[mapCentre.x, mapCentre.y] = true;

//    while(queue.count > 0)
//    {
//        Coord tile = queue.Dequeue();

//        for (int x = -1; x <= 1; x++)
//        {
//            for (int y = -1; y <= 1; y++)
//            {
//                int neighbourX = tile.x + x;
//                int neighbourY = tile.y + y;
//                if (x == 0 || y == 0)
//                {
//                    if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
//                    {
//                        if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
//                        {
//                            mapFlags[neighbourX, neighbourY] = true;
//                            queue.Enqueue(new Coord(neighbourX, neighbourY));
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
//    Vector3 CoordToPositon(int x,int y)
//    {
//        return new Vector3(-mapSize.x / 2 + 0.5f + x,0,-mapSize.y/2+0.5f+y);
//    }
//    Coord GetRandomCoord()
//    {
//        Coord randomCoord = shuffledTileCoords.Dequeue();
//        shuffledTileCoords.Enqueue(randomCoord);
//        return randomCoord;
//    }
//    public struct Coord
//    {
//        public int x;
//        public int y;
//        public Coord(int _x,int _y)
//        {
//            x = _x;
//            y = _y;
//        }
//    }
    }
}