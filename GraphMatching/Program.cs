using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatching
{
    class Program
    {
        
        static Vertex getVertex(string name)
        {
            foreach (var child in _vertexList)
                if (child.Name.Equals(name))
                    return child;

            return null;
        }

        
        /// <summary>
        /// Поиск в глубину
        /// </summary>
        /// <param name="vertex">Вершина для добавления</param>
        /// <param name="list">Список вершин</param>
        /// <param name="visited">Список посещенных вершин</param>
        /// <param name="matching">Список сочетаний вершин</param>
        /// <returns></returns>
        static bool DFS(string vertex, List<string> visited)
        {
            if (vertex.Equals("-1"))//Если дочерняя вершина не имеет сочетания
                return true;
            if (!visited.Contains(vertex))//Верхняя вершина не добавлена в список посещенных
            {
                visited.Add(vertex);//Добавляем вершину в список
                foreach (var item in getVertex(vertex).Childrens)//Перебираем дочерние элементы для верхней вершины
                    if (!visited.Contains(item))//Дочерняя вершина не содержится в списке
                    {
                        visited.Add(item);//Добавляем вершину в список посещенных
                        string parentName = _matchingList[item];//Получаем родительский элемент
                        if (DFS(parentName, visited))//Если вершина не имеет паросочетания
                        {
                            _matchingList[item] = vertex;//добавляем или заменяем для вершины паросочетание
                            return true;
                        }    
                    }
            }
            return false;
        }
        /// <summary>
        /// Добавляем паросочетание
        /// </summary>
        /// <param name="vertex">Вершина для добавления сочетаний</param>
        static void AddMatching(string vertex)
        {
            List<string> visited = new List<string>();//Список посещенных вершин
            Console.WriteLine(DFS(vertex, visited) ? "Вершина была успешно добавлена" : "Вершина не добавлена");
        }
        /// <summary>
        /// Список всех вершин
        /// </summary>
        static List<Vertex> _vertexList =new List<Vertex>();

        static string getPath()
        {
            string[] data =Directory.GetFiles(@"C:\Users\vinograd\source\repos\GraphMatching\GraphMatching\bin\Debug");
            int counter = 0;
            foreach (var line  in data)
            {
                Console.WriteLine($" [{counter}]  {line}");
                counter++;
            }

            return data[Convert.ToInt32(Console.ReadLine())];
        }

        private static List<string> _vertexes = new List<string>();
        static Dictionary<string,string> _matchingList = new Dictionary<string, string>();
        static StreamWriter _writer = new StreamWriter("output.txt");
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines(getPath());
            /* Чтение массива элементов */
            foreach (var vertex in data)
            {
                string name = vertex.Split(':')[0];
                Vertex main = new Vertex(name,"");
                /* Перебираем дочерние элементы */
                foreach (var child in vertex.Split(':')[1].Split(','))
                {
                    Vertex vtx = new Vertex(child,"-1");//Дочерний элемент
                    if (!_vertexes.Contains(vtx.Name))//Дочерняя вершина уже добавлена в список
                    {
                        _matchingList.Add(child,"-1");
                        _vertexes.Add(vtx.Name);
                    }
                    main.Childrens.Add(vtx.Name);//Добавляем элемент в список
                }
                _vertexList.Add(main);
            }
            /* Перебираем список вершних вершин и добавляем паросочетание */
            foreach (var element in _vertexList)
                AddMatching(element.Name);
            //Перебираем словарь для добавления в выходной список
            foreach (KeyValuePair<string, string> child in _matchingList)
                if (!child.Value.Equals("-1")) //элемент имеет паросочетание
                {
                    _writer.WriteLine(child.Key + "-" + child.Value);
                    Console.WriteLine(child.Key + "-" + child.Value);
                }

            _writer.Close();
            Console.ReadKey();
        }

        static void Print(List<Vertex> list)
        {
            foreach (var v in list)
                Console.Write(v.Name + " " + v.Matching + " |");
        }

    }

   
}
