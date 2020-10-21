using System;
using System.Linq;
using System.Reflection;

namespace SpaceLibrary
{
    public static class Edges
    {
        //These are internal edge types that already exist in the system
        public const string _HasAttachment   = nameof(_HasAttachment);
        public const string _AttachmentOf    = nameof(_AttachmentOf);

        //These are our own edge types
        public const string HasPrimaryAuthor = nameof(HasPrimaryAuthor);
        public const string HasAuthor        = nameof(HasAuthor);
        public const string PrimaryAuthorOf  = nameof(PrimaryAuthorOf);
        public const string AuthorOf         = nameof(AuthorOf);
        public const string AssociatedTo     = nameof(AssociatedTo);
        public const string HasMember        = nameof(HasMember);
        public const string HasCategory      = nameof(HasCategory);
        public const string CategoryOf       = nameof(CategoryOf);
        public const string HasRelated       = nameof(HasRelated);
        public const string RelatedTo        = nameof(RelatedTo);

        public static string[] GetAll()
        {
            //Uses reflection to get a list of all constants in this helper class
            return typeof(Edges).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                 .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                                 .Select(fi => fi.GetRawConstantValue() as string)
                                 .Where(s => s is object && !s.StartsWith("_"))
                                 .ToArray();
        }
    }
}
