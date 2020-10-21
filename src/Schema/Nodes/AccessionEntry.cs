using Curiosity.Library;

namespace SpaceLibrary
{
    [Node]
    public class AccessionEntry
    {
        [Key] public string Number { get; set; }
        [Property] public string Title { get; set; }
        [Property] public string EntryType { get; set; }
    }
}
