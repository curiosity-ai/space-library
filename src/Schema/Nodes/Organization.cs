using Curiosity.Library;

namespace SpaceLibrary
{
    [Node]
    public class Organization
    {
        [Key]      public string Name { get; set; }
        [Property] public string Location { get; set; }
    }

}
