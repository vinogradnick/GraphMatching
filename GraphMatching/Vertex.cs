using System.Collections.Generic;

namespace GraphMatching
{
    /// <summary>
    /// Вершина с набром параметров
    /// </summary>
    class Vertex
    {
        /// <summary>
        /// Название вершины
        /// </summary>
        public string Name;
        /// <summary>
        /// Сочетания
        /// </summary>
        public string Matching;
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="name">название вершины</param>
        /// <param name="parent"></param>
        /// <param name="matching"></param>
        public Vertex(string name,string matching)
        {
            Name = name;
            Matching = matching;
            Childrens=new List<string>();
        }
        /// <summary>
        /// Дочерние элементы
        /// </summary>
        public List<string> Childrens { get; set; }
    }
}