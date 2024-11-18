using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridtypgraph : MonoBehaviour
{
    public GameObject[] obstacleprefab;
    public GameObject character;
    private Animator characterAnimator; // Animator 변수 추가
    static int startx = 1;
    static int starty = 1;
    static int finishx = 6;
    static int finishy = 5;
    public static int gsize = 9;
    private int[,] gridgraph = new int[gsize, gsize];
    private int randomnum;

    private List<Vector2Int> path;
    private int pathIndex = 0;
    private float moveSpeed = 0.8f;

    void Start()
    {
        // 1은 장애물, 2는 start, 3은 finish
        gridgraph[1, 1] = 2;
        gridgraph[1, 4] = 1;
        gridgraph[1, 6] = 1;
        gridgraph[2, 2] = 1;
        gridgraph[3, 3] = 1;
        gridgraph[5, 5] = 1;
        gridgraph[5, 7] = 1;
        gridgraph[6, 1] = 1;
        gridgraph[6, 4] = 1;
        gridgraph[8, 2] = 1;
        gridgraph[6, 5] = 3;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < gsize; j++)
            {
                if (gridgraph[i, j] == 1)
                {
                    // Create obstacle
                    randomnum = UnityEngine.Random.Range(0, obstacleprefab.Length);
                    Vector3 spawnPos = new Vector3(j - 4, 0, 4 - i);
                    Instantiate(obstacleprefab[randomnum], spawnPos, obstacleprefab[randomnum].transform.rotation);
                }
                else if (gridgraph[i, j] == 2)
                {
                    // Create character
                    Vector3 spawnPos = new Vector3(j - 4, 0, 4 - i);
                    character = Instantiate(character, spawnPos, character.transform.rotation);
                    characterAnimator = character.GetComponent<Animator>();
                }
            }
        }

        // BFS 실행
        path = BFS();
    }

    void Update()
    {
        if (path != null && pathIndex < path.Count)
        {
            Vector3 targetPos = new Vector3(path[pathIndex].y - 4, 0, 4 - path[pathIndex].x);
            float step = moveSpeed * Time.deltaTime;
            character.transform.position = Vector3.MoveTowards(character.transform.position, targetPos, step);

            Vector3 direction = (targetPos - character.transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, lookRotation, Time.deltaTime * moveSpeed * 4);
            }

            if (characterAnimator != null)
            {
                characterAnimator.SetFloat("Speed_f", moveSpeed);
            }
            if (Vector3.Distance(character.transform.position, targetPos) < 0.5f)
            {
                pathIndex++;
                if (pathIndex >= path.Count){
                // 목표 지점에 도달한 경우 애니메이션 속도를 0으로 설정
                characterAnimator.SetFloat("Speed_f", 0f);
                
            }
            }
        }
    }

    public List<Vector2Int> BFS()
    {
        int[] movex = new int[] { -1, 1, 0, 0 };
        int[] movey = new int[] { 0, 0, -1, 1 };
        bool[,] visited = new bool[gsize, gsize];

        Queue<Tuple<Vector2Int, List<Vector2Int>>> queue = new Queue<Tuple<Vector2Int, List<Vector2Int>>>();
        Vector2Int start = new Vector2Int(startx, starty); // 수정된 부분
        queue.Enqueue(Tuple.Create(start, new List<Vector2Int> { start }));
        visited[startx, starty] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Vector2Int currentPos = current.Item1;
            List<Vector2Int> path = current.Item2;

            if (currentPos.x == finishx && currentPos.y == finishy)
            {
                return path;
            }

            for (int i = 0; i < 4; i++)
            {
                int newY = currentPos.x + movey[i];
                int newX = currentPos.y + movex[i];

                if (newY >= 0 && newY < gsize && newX >= 0 && newX < gsize && !visited[newY, newX] && gridgraph[newY, newX] != 1)
                {
                    visited[newY, newX] = true;
                    List<Vector2Int> newPath = new List<Vector2Int>(path);
                    newPath.Add(new Vector2Int(newY, newX));
                    queue.Enqueue(Tuple.Create(new Vector2Int(newY, newX), newPath));
                }
            }
        }

        return null; // 경로를 찾지 못함
    }
}
