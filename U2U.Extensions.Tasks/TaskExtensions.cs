using System;
using System.Threading.Tasks;

namespace U2U.Extensions.Tasks
{
  public static class TaskExtensions
  {
    public static async void Await(this Task task, Action<Exception> errorHandler = null)
    {
      try
      {
        await task;
      }
      catch (Exception ex)
      {
        errorHandler?.Invoke(ex);
      }
    }

    public static async void Await<T>(this Task<T> task, Action<T> completed, Action<Exception> errorHandler = null)
    {
      try
      {
        T result = await task;
        completed(result);
      }
      catch (Exception ex)
      {
        errorHandler?.Invoke(ex);
      }
    }
  }
}
