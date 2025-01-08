namespace journey_control.Models
{
    public class ProjectIndicator
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static List<ProjectIndicator> GetDefaultIndicators()
        {
            return new List<ProjectIndicator>
        {
            new ProjectIndicator { Name = "Sustentação", Value = "Sustentação" },
            new ProjectIndicator { Name = "Questor Cloud", Value = "Questor Cloud" },
            new ProjectIndicator { Name = "Questor Negócio", Value = "Questor Negócio" },
            new ProjectIndicator { Name = "Questor Zen 2.0", Value = "Questor Zen 2.0" },
            new ProjectIndicator { Name = "Configurador Inteligente", Value = "Configurador Inteligente" },
            new ProjectIndicator { Name = "Automatização dos Códigos de Ajustes", Value = "Automatização dos Códigos de Ajustes" },
            new ProjectIndicator { Name = "Certificados Digitais", Value = "Certificados Digitais" },
            new ProjectIndicator { Name = "Firma Simples", Value = "Firma Simples" }
        };
        }
    }
}
