using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.DAL
{
    public abstract class BaseIdentityModelCore
    {
        public virtual int Id { get; set; }
    }

    public abstract class BaseIdentityModel<T> : BaseIdentityModelCore where T: BaseIdentityModel<T>
    {
        /*public override bool Equals(object obj)
        {
            return obj is T && ((T)obj).Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id;
        }*/
    }
}
