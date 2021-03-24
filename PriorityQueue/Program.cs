using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();
        //우선순위 큐의 시간복잡도 O(logN)

        public void Push(T data)
        {
            //힙의 맨 끝에 새로운 데이터를 삽입한다. 
            _heap.Add(data);
            //추가된 데이터를 기준으로 갈 수 있는 데까지 올라간다. 

            //시작 위치
            int now = _heap.Count - 1;

            //도장깨기(?)
            while (now > 0)
            {
                //도장깨기 시도
                int next = (now - 1) / 2;   //(now-1)/2는 부모의 인덱스
                if (_heap[now].CompareTo(_heap[next]) < 0)
                    break;  //실패

                //두 값을 교체한다. 
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                //검사 위치를 이동한다.
                now = next;
            }

        }
        public T Pop()
        {
            //반환할 데이터를 따로 저장
            T ret = _heap[0];

            //마지막 데이터를 루트로 이동한다.
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            //양옆 체크, 자신이 옆보다 크기가 작다면 내려간다. 
            //양쪽 다 크다면 더 큰 숫자 밑으로 내려간다.
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                //왼쪽값이 현재값보다 크면, 왼쪽으로 이동
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                    next = left;
                //오른쪽값이 현재값(왼쪽 이동 포함)보다 크면, 오른쪽으로 이동
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                //왼쪽/오른쪽 모두 현재값보다 작다면 종료
                if (next == now)
                    break;

                //두 값 교체
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                //검사 위치 교체
                now = next;
            }

            return ret;

        }
        public int Count()
        {
            return _heap.Count;
        }
    }
    class Knight : IComparable<Knight>
    {
        public int Id { get; set; }
        public int CompareTo(Knight other)
        {
            if (Id == other.Id)
                return 0;
            return Id > other.Id ? 1 : -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<int> q = new PriorityQueue<int>();

            //우선순위 높은 것이 먼저 빠져나온다.
            q.Push(20);
            q.Push(10);
            q.Push(30);
            q.Push(90);
            q.Push(40);

            //최단거리 알고리즘 시 작은 숫자 순서대로 뽑아오려면
            //1. 비교 조건을 바꾼다
            //2. -1을 곱해서 비교한다.

            while (q.Count() > 0)
            {
                Console.WriteLine(q.Pop());
            }
        }
    }
}
