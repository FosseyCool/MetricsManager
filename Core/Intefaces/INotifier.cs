using System.Diagnostics;

namespace Core.Intefaces
{
    public interface INotifier
    {
        void Notify();

        bool CanRun();
    }

    public class Notifier1 : INotifier
    {
        public void Notify()
        {
            Debug.WriteLine("Debugging from Notifier 1");
        }

        public bool CanRun()
        {
            throw new System.NotImplementedException();
        }
    }

}