using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Infrastructure.Core.Algorithms.Snowflake
{
    internal class DisposableAction : IDisposable
    {
        readonly Action _action;

        public DisposableAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
