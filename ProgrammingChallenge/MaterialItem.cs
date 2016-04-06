using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.exam
{
    [Serializable]
    public class MaterialItem : OrderItem
    {
        public override decimal GetTaxableAmount()
        {
            return GetItemTotal();
        }

        public override OrderItem Create()
        {
            return new MaterialItem();
        }
    }
}
