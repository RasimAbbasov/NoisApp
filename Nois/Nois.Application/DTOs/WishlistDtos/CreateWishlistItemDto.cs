using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.DTOs.WishlistDtos
{
    public class CreateWishlistItemDto
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
