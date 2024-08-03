namespace Civilization.Models
{
    public class Technology
    {
        public string Name { get; set; }
        public int ResearchTime { get; set; }
        public List<string> Prerequisites { get; set; }

        public Technology(string name, int researchTime, List<string> prerequisites = null)
        {
            Name = name;
            ResearchTime = researchTime;
            Prerequisites = prerequisites ?? new List<string>();
        }

        // Função effects por enquanto em branco
        public void Effects()
        {
        }
    }
}
