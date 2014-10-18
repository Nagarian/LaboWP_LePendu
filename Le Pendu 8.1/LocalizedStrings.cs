using Le_Pendu_8._1.Resources;

namespace Le_Pendu_8._1
{
    /// <summary>
    /// Permet d'accéder aux ressources de chaîne.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}