using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace U2U.Extensions.Tasks
{
  public struct SynchronizationContextRemover : INotifyCompletion
  {
    public bool IsCompleted
      => SynchronizationContext.Current == null;

    public void OnCompleted(Action continuation)
    {
      SynchronizationContext prevContext = SynchronizationContext.Current;
      try
      {
        SynchronizationContext.SetSynchronizationContext(null);
        continuation();
      }
      finally
      {
        SynchronizationContext.SetSynchronizationContext(prevContext);
      }
    }

    // Any method that implements an GetAwaiter method can be used with the await syntax
    public SynchronizationContextRemover GetAwaiter() => this;

    public void GetResult() { }
  }
}
