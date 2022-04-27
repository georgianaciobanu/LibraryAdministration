using Xamarin.Forms.Internals;

namespace SimbioMed.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}