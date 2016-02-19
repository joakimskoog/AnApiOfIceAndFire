namespace AnApiOfIceAndFire.Domain.Models.Filters
{
    public class CharacterFilter
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public bool? IsAlive { get; set; }
    }
}