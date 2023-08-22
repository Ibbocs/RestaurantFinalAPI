using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.RequestParametrs
{
    //todo get methodlarinda pagination islet. [FromQuery]Pagination pagination- moterizede Skip(pagination.Page * pagination.Size). Take(size..page)- bele yazmaq olurrdu
    //Skip ve take isledib dedecek data sayin berillere gy-21 video 

    public record Pagination
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
