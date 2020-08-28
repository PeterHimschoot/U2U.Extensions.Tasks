using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using U2U.Extensions.Tasks;

namespace Tasks.CallingFromSyncMethods
{
  public class MyViewModel : INotifyPropertyChanged
  {
    public MyViewModel()
    {
      try
      {
        // First demo: this call results in a warning :(
        //InitTextAsync();

        // Second demo: Uncomment thrown exception in InitTextAsync
        // No clue that an exception was thrown! :( :(

        // Third demo: show TaskExtensions first Await method
        // Enable stop on thrown exceptions in Debug-> Windows -> Exception settings
        //InitTextAsync().Await();

        // Fourth demo: Implement error handler
        // Disable stop on thrown exceptions in Debug-> Windows -> Exception settings
        //InitTextAsync().Await(ex => this.Message = ex.Message);

        // Fifth demo: Show for Task<T>
        //InitTextAsync2().Await(result => Message = result);

        // Sixth demo: Wait for async call to complete
        // Put breakpoint on both Message assignments => "done" is set first, not what we want...
        //InitTextAsync2().Await(result => Message = result);
        //Message = "Done";

        // Continued: Comment previous lines, uncomment next line
        //Message = InitTextAsync2().Result; // Deadlock :( :( :(

        // Continued: Uncomment previous line, uncomment next lines
        // This will block the UI, so be carefull!
        //Message = InitTextAsync3().Result;

        // Continued: Uncomment previous line, uncomment next lines
        // This will block the UI, so be carefull!
        //Message = InitTextAsync3().GetAwaiter().GetResult();

        // This will also result in AggregateException when method throws
        //Message = InitTextAsync3(shouldThrow: true).Result;

        // This will also result in AggregateException when method throws
        //Message = InitTextAsync3(shouldThrow: true).GetAwaiter().GetResult();

      }
      catch (AggregateException)
      {
        Message = "Aggregate exception";
      }
      catch (ArgumentException)
      {
        Message = "Argument exception";
      }
      catch (Exception ex)
      {
        Message = $"Unexpected exception {ex.Message}";
      }
    }

    private string message = "...";

    public string Message
    {
      get => this.message;
      set { this.message = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propName = "")
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

    // Please do not make this method return void!
    public async Task InitTextAsync()
    {
      await Task.Delay(TimeSpan.FromSeconds(3));
      Message = "Hello world!";

      // Uncomment this line to show unhandled exceptions
      //throw new Exception("Boem!");
    }

    public async Task<string> InitTextAsync2()
    {
      await Task.Delay(TimeSpan.FromSeconds(3));
      return "Hello world!";
    }

    public async Task<string> InitTextAsync3(bool shouldThrow = false)
    {
      // This will remove the synchronization context for the whole method
      // Do not access the UI here!
      await new SynchronizationContextRemover();
      await Task.Delay(TimeSpan.FromSeconds(1));
      if (shouldThrow)
      {
        throw new ArgumentException("I was told to throw an exception");
      }
      await Task.Delay(TimeSpan.FromSeconds(1));
      await Task.Delay(TimeSpan.FromSeconds(1));
      return "Hello world!";
    }
  }
}
