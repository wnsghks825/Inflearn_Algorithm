using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    //동적배열
    /*
    class MyList<T>
    {
        const int DEFAULT_SIZE = 1;
        T[] _data = new T[DEFAULT_SIZE];
        public int Count;//실제로 사용중인 데이터의 갯수
        public int Capacity { get { return _data.Length; } }//예약된 데이터의 갯수. 배열의 크기와 Capacity는 일치


        //O(1)
        //추가
        public void Add(T item)
        {
            //1. 공간이 충분히 남아있는지 확인한다.
            if (Count >= Capacity)
            {
                //공간 다시 확보(늘려서)
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                    newArray[i] = _data[i];
                _data = newArray;

            }
            //2. 공간에 데이터를 추가한다. 
            _data[Count] = item;
            Count++;
        }

        //인덱서
        //O(1)
        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }
        //O(n)
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
                _data[i] = _data[i + 1];
            _data[Count - 1] = default(T);
            Count--;
        }
        
    }
    */

    //연결 리스트
    /*
    class Room<T>
    {
        public T Data;
        public Room<T> Next;
        public Room<T> Prev;
    }
    class RoomList<T>
    {
        public int Count = 0;
        public Room<T> Head = null;    //첫번째
        public Room<T> Tail;    //마지막

        public Room<T> AddLast(T data)
        {
            Room<T> newRoom = new Room<T>();
            newRoom.Data = data;

            //만약 아직 방이 아예 없었다면, 새로 추가한 첫번째 방이 곧 Head이다.
            if (Head == null)
                Head = newRoom;

            //기존의 [마지막 방]과 [새로 추가되는 방]을 연결해준다. 
            if (Tail != null)
            {
                Tail.Next = newRoom;
                newRoom.Prev = Tail;
            }

            //[새로 추가되는 방]을 [마지막 방]으로 인정한다.
            Tail = newRoom;
            Count++;
            return newRoom;
        }

        public void Remove(Room<T> room)
        {
            //[기존의 첫번째 방 다음 방]을 [첫번째 방]으로 인정한다. 
            if (Head == room)
                Head = Head.Next;

            //[기존의 마지막 방 이전 방]을 [마지막 방]으로 인정한다. 
            if (Tail == room)
                Tail = Tail.Prev;

            if (room.Prev != null)
                room.Prev.Next = room.Next;

            if (room.Next != null)
                room.Next.Prev = room.Prev;

            Count--;
        }
    }
    */


    class Board
    {
        const char CIRCLE = '\u25cf';

        public TileType[,] _tile;
        public int _size;


        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size)
        {
            if (size % 2 == 0)
                return;
            _tile = new TileType[size, size];
            _size = size;

            //GenerateByBinaryTree();
            GenerateBySideWinder();

        }
        public void GenerateByBinaryTree()
        {
            //일단 길을 막아버리는 작업
            for(int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if(x % 2 ==0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;

                    else
                        _tile[y, x] = TileType.Empty;
                }
            }
            //랜덤으로 우측 혹은 아래로 길 뚫기
            //Binary Tree Algorithm
            Random rand = new Random();

            for(int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }

                    else
                    {
                        _tile[y+1, x] = TileType.Empty;
                    }
                }
            }
        }
        public void GenerateBySideWinder()
        {
            //일단 길을 막아버리는 작업
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;

                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            //랜덤으로 우측 혹은 아래로 길 뚫기
            Random rand = new Random();

            for (int y = 0; y < _size; y++)
            {
                int count = 1;
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        count++;
                    }

                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        _tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {

                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
