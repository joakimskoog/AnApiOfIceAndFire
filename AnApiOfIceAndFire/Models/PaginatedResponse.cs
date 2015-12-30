using System.Collections.Generic;

namespace AnApiOfIceAndFire.Models
{
    public class PaginatedResponse<TData>
    {
        public PaginationLinks Links { get; }
        public IEnumerable<TData> Data { get; }

        public PaginatedResponse(PaginationLinks links, IEnumerable<TData> data)
        {
            Links = links;
            Data = data;
        }
    }
}