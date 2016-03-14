using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity.InterceptionExtension;
using System.Diagnostics;

namespace CrossCuttingConcerns
{
    public class CachingInterceptionBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {

            // Before invoking the method on the original target.
            if (input.MethodBase.Name == "set_Name")
            {
                var tenantName = input.Arguments["value"].ToString();
                if (IsInCache(tenantName))
                {
                    return input.CreateMethodReturn(
                      FetchFromCache(tenantName));
                }
            }

            IMethodReturn result = getNext()(input, getNext);

            // After invoking the method on the original target.
            if (input.MethodBase.Name == "SaveTenant")
            {
                AddToCache(input.Arguments["tenant"]);
            }

            return result;

        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private bool IsInCache(string key) { return false;  }

        private object FetchFromCache(string key) { return null; }

        private void AddToCache(object item) {}
    }
}
