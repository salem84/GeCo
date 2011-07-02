using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace GeCo.Infrastructure
{
    public class DisposableLifetimeManager : LifetimeManager
    {
        private object cachedInstance;

        public override object GetValue()
        {
            return this.cachedInstance;
        }

        public override void RemoveValue()
        {
            this.cachedInstance = null;
        }

        public override void SetValue(object newValue)
        {
            this.cachedInstance = newValue;
        }
    }
}
