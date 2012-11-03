using System.ComponentModel.Composition;

namespace LiteApp.Bizcard.Helpers
{
    public static class MefExtensions
    {
        public static void SatisfyImports(this object target)
        {
            var bootstrapper = ((AppBootstrapper)App.Current.Resources["bootstrapper"]);
            bootstrapper.Container.ComposeParts(target);
        }
    }
}
