using System;
using System.Collections.Generic;

namespace Exercise
{
    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            {0,1,0,1,0,0 },
            {1,0,1,1,0,0 },
            {0,1,0,0,0,0 },
            {1,1,0,0,1,0 },
            {0,0,0,1,0,1 },
            {0,0,0,0,1,0 }
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

        //DFS
/*
        bool[] visited = new bool[6];
        //1) 우선 now 부터 방문하고, 
        //2) now와 연결된 정점들을 하나씩 확인해서 [ 아직 미발견(미방문)] 상태라면 방문한다. 
        //행렬 방식
        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;           //우선 now부터 방문하고

            //배열 방식
            for (int next = 0; next < 6; next++)
            {
                if (adj[now, next] == 0)    //연결되어 있지 않으면 스킵
                    continue;
                if (visited[next])         //이미 방문했다면 스킵
                    continue;

                //나와 연결된 정점들 찾기
                DFS(next);
            }
        }
        public void DFS2(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;           //우선 now부터 방문하고

            foreach(int next in adj2[now])  //연결되어 있지 않으면 스킵
            {
                if (visited[next])         //이미 방문했다면 스킵
                    continue;

                //나와 연결된 정점들 찾기
                DFS2(next);
            }
        }

        public void SearchAll()
        {
            visited = new bool[6];
            for(int now = 0; now < 6; now++)
            {
                if (visited[now] == false)
                    DFS(now);
            }
        }
*/

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
