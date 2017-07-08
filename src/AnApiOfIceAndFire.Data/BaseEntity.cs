using Dapper;
using Dapper.Contrib.Extensions;

namespace AnApiOfIceAndFire.Data
{
    public abstract class BaseEntity
    {
        public const char SplitDelimiter = ';';

        [ExplicitKey]
        public int Id { get;  set; }
        public string Name { get;  set; }
    }
}