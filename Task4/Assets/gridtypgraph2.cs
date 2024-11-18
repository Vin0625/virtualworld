using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridtypgraph2 : MonoBehaviour
{
    public GameObject[] obstacleprefab;
    public GameObject character;
    private Animator characterAnimator; // Animator 변수 추가
    static int startx = 1;
    static int starty = 5;
    static int finishx = 16;
    static int finishy = 15;
    public static int gsize = 19;
    private int[,] gridgraph = new int[gsize, gsize];
    private int randomnum;

    private List<Vector2Int> path;
    private int pathIndex = 0;
    private float moveSpeed = 0.8f;

    void Start()
    {
        // 1은 장애물, 2는 start, 3은 finish
        gridgraph[startx, starty] = 2;
        for (int i = 7; i < 17; i++)
        {
            gridgraph[1, i] = 1;
        }
        for (int i = 3; i < 12; i++)
        {
            gridgraph[i, 5] = 1;
        }
        for (int i = 2; i < 15; i++)
        {
            gridgraph[i, 16] = 1;
        }
        for (int i = 6; i < 17; i++)
        {
            gridgraph[14, i] = 1;
        }
        gridgraph[finishx, finishy] = 3;

        for (int i = 0; i < gsize; i++)
        {
            for (int j = 0; j < gsize; j++)
            {
                if (gridgraph[i, j] == 1)
                {
                    // Create obstacle
                    randomnum = UnityEngine.Random.Range(0, obstacleprefab.Length);
                    Vector3 spawnPos = new Vector3(j - 9, 0, 9 - i);
                    Instantiate(obstacleprefab[randomnum], spawnPos, obstacleprefab[randomnum].transform.rotation);
                }
                else if (gridgraph[i, j] == 2)
                {
                    // Create character
                    Vector3 spawnPos = new Vector3(j - 9, 0, 9 - i);
                    character = Instantiate(character, spawnPos, character.transform.rotation);
                    characterAnimator = character.GetComponent<Animator>(); // Animator 컴포넌트 가져오기
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
            Vector3 targetPos = new Vector3(path[pathIndex].y - 9, 0, 9 - path[pathIndex].x);
            float step = moveSpeed * Time.deltaTime;
            character.transform.position = Vector3.MoveTowards(character.transform.position, targetPos, step);

            if (characterAnimator != null)
            {
                float currentSpeed = (targetPos - character.transform.position).magnitude / step;
                characterAnimator.SetFloat("Speed_f", currentSpeed);
            }

            Vector3 direction = (targetPos - character.transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, lookRotation, Time.deltaTime * moveSpeed * 4);
            }

            if (Vector3.Distance(character.transform.position, targetPos) < 0.5f)
            {
                pathIndex++;
                if (pathIndex >= path.Count)
                {
                    // 목표 지점에 도달한 경우 애니메이션 속도를 0으로 설정
                    characterAnimator.SetFloat("Speed_f", 0f);
                }
            }
        }
    }

    public List<Vector2Int> BFS()
    {
        int[] movex = new int[] { 1, -1, 0, 0 };
        int[] movey = new int[] { 0, 0, -1, 1 };
        bool[,] visited = new bool[gsize, gsize];

        Queue<Tuple<Vector2Int, List<Vector2Int>>> queue = new Queue<Tuple<Vector2Int, List<Vector2Int>>>();
        Vector2Int start = new Vector2Int(startx, starty);
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
