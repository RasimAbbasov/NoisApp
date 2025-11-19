using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.DTOs.SizeDtos
{
    public class SizeSummaryDto
    {
        public int Id {  get; set; }
        public string Code { get; set; } 
        public string Name { get; set; } 
        public int SortOrder { get; set; }       
    }
}
