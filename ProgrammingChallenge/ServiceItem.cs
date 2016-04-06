using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.exam
{
    [Serializable]
    public class ServiceItem : OrderItem
    {
        public override decimal GetTaxableAmount()
        {
            //Service items are not taxable            
            return 0;
        }

        public override OrderItem Create()
        {
            return new ServiceItem();
        }
    }
}
