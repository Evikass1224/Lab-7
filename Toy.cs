namespace ProjectFileTasks
{
    /// <summary>
    /// Игрушка с атрибутами
    /// </summary>
    [Serializable]
    public class Toy
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
