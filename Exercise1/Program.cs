using System;
using System.Collections.Generic;

namespace Exercise
{
    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            { -1, 15, -1, 35, -1, -1 },
            { 15, -1, 05, 10, -1, -1 },
            { -1, 05, -1, -1, -1, -1 },
            { 35, 10, -1, -1, 05, -1 },
            { -1, -1, -1, 05, -1, 05 },
            { -1, -1, -1, -1, 05, -1 }
        };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int>() { 1, 3 },
            new List<int>() { 0, 2, 3 },
            new List<int>() { 1 },
            new List<int>() { 0, 1, 4 },
            new List<int>() { 3, 5 },
            new List<int>() { 4 }
        };

        public void Dijikstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int[] parent = new int[6];
            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;
            parent[start] = start;

            while (true)
            {
                //제일 좋은 후보를 찾기(가까이에 있는)

                //가장 유력한 후보의 거리와 번호를 저장한다.
                int closest = Int32.MaxValue;
                int now = -1;
                for(int i = 0; i < 6; i++)
                {
                    //이미 방문한 정점은 스킵
                    if (visited[i])
                        continue;
                    //아직 발견(예약)된 적이 없거나, 기존 후보보다 멀리 있다면 스킵
                    if (distance[i] == Int32.MaxValue || distance[i] > closest)
                        continue;
                    //여태껏 발견한 좋은 후보라는 의미, 정보 갱신
                    closest = distance[i];
                    now = i;

                }

                //다음 후보가 하나도 없다.
                if (now == -1)
                    break;

                //제일 좋은 후보를 찾았으니 방문한다. 
                visited[now] = true;

                //방문한 정점과 인접한 정점들을 조사해서, 상황에 따라 발견한 최단거리를 갱신한다. 
                for(int next = 0; next < 6; next++)
                {
                    //연결되지 않은 정점 스킵
                    if (adj[now, next] == -1)
                        continue;
                    //이미 방문한 정점 스킵
                    if (visited[next])
                        continue;

                    //새로 조사된 정점의 최단거리를 계산한다. 
                    int nextDist = distance[now] + adj[now, next];

                    //만약 기존에 발견한 최단거리가 새로 조사된 최단거리보다 크면, 정보 갱신
                    if (nextDist < distance[next])
                    {
                        distance[next] = nextDist;
                        parent[start] = now;
                    }
                    
                    distance[3] = 35;
                }
            }
        }
        public void BFS(int start)
        {
            bool[] found = new bool[6];

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            found[start] = true;

            while (q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                //인접한 것을 체크하면서 한번도 발견하지 않았다면 예약

                //행렬 방식
                for(int next = 0; next < 6; next++)
                {
                    if (adj[now, next] == 0)    //인접하지 않았다면 스킵
                        continue;
                    if (found[next])        //이미 발견했다면 스킵
                        continue;

                    q.Enqueue(next);
                    found[next] = true;
                }

            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //그래프 순회 방법은 여러 가지가 있다.
            //DFS 깊이 우선 탐색
            //BFS 너비 우선 탐색

            Graph graph = new Graph();
            graph.BFS(0);
        }
    }
}
