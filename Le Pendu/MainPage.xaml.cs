using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

namespace Le_Pendu
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Cette chaine de caractères contient le mot à chercher
        /// </summary>
        string wordToFindString;

        /// <summary>
        /// Ce chiffre correspond au nombre de proposition erronés faites par l'utilisateur
        /// </summary>
        int compteurErreur;

        /// <summary>
        /// Cette méthode est la première qui est appelé lorsqu'on l'application arrive sur cette page.
        /// Autrement dit, c'est quasiment la toute première fonction a être éxécuté dans le programme
        /// </summary>
        public MainPage()
        {
            // Ligne de base, c'est elle qui va instancier (créer) la vue (le fichier xaml) de cette page 
            this.InitializeComponent();

            // Une fois que tout est initalisé, nous allons donc charger le fichier texte puis choisir un des mots de ce fichier
            ChargerFichier();

            // Cette ligne que je n'ai pas abordé lors de la démo, permet de gèrer les états de l'application lorsque celle-ci est mise en arrière-plan, ou si le téléphone se verrouille, etc...
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        // Nous n'avons pas eu à utiliser cette fonction lors de la démo, donc je ne vais pas rentrer plus dans les détails
        // Je vous laisse donc le soin de vous renseigner si vous voulez en savoir plus =)
        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d’événement décrivant la manière dont l’utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: préparer la page pour affichage ici.

            // TODO: si votre application comporte plusieurs pages, assurez-vous que vous
            // gérez le bouton Retour physique en vous inscrivant à l’événement
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si vous utilisez le NavigationHelper fourni par certains modèles,
            // cet événement est géré automatiquement.
        }

        /// <summary>
        /// Cette méthode va charger le fichier contenant notre dictionnaire de mots, puis en choisir un pour le jeu.
        /// 
        /// Cette méthode est un peu plus particulière que les autres. J'aurais préféré éviter de vous la montrer aussi tôt, mais elle est là donc je vais vous expliquer brièvement
        /// Aussi, si vous ne comprenais pas tout les mécanismes ce n'est pas grave, vous aurez le temps de le comprendre par la suite.
        /// </summary>
        public async void ChargerFichier()
        {
            // Cette liste de string va servir à accueillir tout les mots présents dans le fichier texte
            List<string> result = new List<string>();

            // Nous récupérons le dossier d'installation de l'application
            StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;

        /// Ceci est le code comme il a était fait durant le labo
        /// Ci-dessous se trouve une version plus simple à comprendre et qui correspond plus à ce que vous serait susceptible d'utiliser
        /// Aussi si la première version est trop complexe à comprendre, retenez juste la deuxième, ce sera suffisant.

            // Nous ouvrons le fichier en lecture dans ce que l'on appelle un Stream.
            // Un stream est un objet spécial prévu pour traiter plus simplement les flux de données que ce qui se faisait en C/C++
            // Ainsi, avec un stream, vous n'avez pas besoin de vous préoccuper de la position du curseur dans le fichier ou autre
            // il suffit d'appeler les méthodes présentes sur le stream pour le lire.
            Stream fileStream = await folder.OpenStreamForReadAsync("FR_fr.txt");

            // Cette structure avec le using permet de faire en sorte de fermer automatiquement le flux de données
            // lorsque le programme sortira de l'espace définit par les accolades du using. A part pour l'inclusion des namespaces (voir en haut de ce fichier)
            // Vous ne retrouverez les using qu'essentiellement avec les streams. Et les tendances de programmations C# tendent à ne plus utiliser les streams au profit
            // des nouveaux opérateurs d'asynchronisme apportés par la version 4.5 du framework .NET
            using (StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding("iso-8859-1")))
            {
                // Ainsi nous lisons notre stream du début à la fin et nous stockons les données recueillis dans un string
                string text = sr.ReadToEnd();

                // que nous redécoupons au bout de chaque ligne pour isoler chacun des mots
                result = text.Split(new char[] { '\r', '\n' }).ToList();
            }

        /// Ceci est la deuxième version que je vous propose qui est plus simple à retenir, et plus "d'en l'air du temps"

            ////  Nous récupérons le fichier dans un objet
            //StorageFile file = await folder.GetFileAsync("FR_fr.txt");

            //// Puis nous le lisons entièrement dans un string
            //string text2 = await FileIO.ReadTextAsync(file);

            //// que nous redécoupons dans notre tableau de mots
            //result = text2.Split(new char[] { '\r', '\n' }).ToList();

        ///
            // Nous pouvons reprendre avec la suite de ce que nous avons vu dans le labo

            // Nous utilisons maitenant un nouvel objet Random qui nous permet de choisir un mot parmis notre liste
            Random random = new Random();
            int index = random.Next(result.Count);

            // et nous stockons le mot choisis aléatoirement dans notre variable de classe
            wordToFindString = result[index].ToLowerInvariant();

            // Puis nous faisons le mise en forme, maintenant que nous avons le mot
            MiseEnForme();

            // Pour ceux qui se posent la question du pourquoi nous appelons la méthode MiseEnForme ici et non pas dans le constructeur de la page
            // la réponse est la suivante : 
            // tout au long de cette méthode vous avez certainement remarqué la présence du mot-clé particulier : await
            // il s'agit d'un mot-clé apporté par la dernière version du framework .NET la version 4.5
            // Il permet de faire de l'asynchronisme, bien plus facilement que sur les anciennes versions. Son fonctionnement est relativement simple
            // quand le programme arrive sur la ligne contenant le mot await, il va lancer le traitement de la fonction à droite dans un autre processus (que je vais appeler X),
            // et attendre que la réponse de X soit reçu avant d'exécuter la suite de la méthode actuelle (celle qui a appelé X)
            // Cela est particulièrement utile en programmation événementielle car le thread principal va pouvoir exécuter d'autres morceaux de code durant tout le temps que
            // mettra X pour transmettre le résultat à la méthode en cours.
            // De plus pour pouvoir utiliser le mot-clé await dans une fonction, vous devez rajouter le mot-clé async devant le nom de votre fonction (voir ci-dessus)
            // Et c'est là que tout se joue ! En effet, à partir du moment ou une méthode à ce mot-clé async, dès qu'elle est appelée, le code lancera cette fonction en arrière-plan
            // sans attendre le qu'elle est finis. Ainsi, elle aura terminé de construire la page avant d'avoir chargé notre fichier texte.
            // C'est pour cela que nous appelons notre méthode de MiseEnForme à partir de cette méthode ChargerFichier, car à l'intérieur de celle-ci (de son référentiel à elle donc)
            // elle s'exécute en "synchrone", donc elle mettra à jour l'affichage après avoir choisi le mot aléatoirement
        }

        /// <summary>
        /// Cette méthode permet d'initialiser l'affichage du mot à trouver
        /// </summary>
        public void MiseEnForme()
        {
            string result = "";

            for (int i = 0; i < wordToFindString.Length; i++)
            {
                result += "_ ";
            }

            wordToFind.Text = result;
        }

        /// <summary>
        /// Cette méthode est un évènement qui est enclenché lorsque l'utilisateur va modifier le texte contenue dans notre TextBox
        /// </summary>
        /// <param name="sender">Cette variable va contenir une référence vers l'objet "MyTextBox" dans notre exemple</param>
        /// <param name="e">Il s'agit d'un objet qui contient des informations supplémentaire sur notre évènement</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // MyTextBox correspond au TextBox que l'on a placé sur notre fichier MainPage.xaml
            // Du fait que nous lui avons donné un nom, le controle est accessible depuis notre code C#
            // Nous pouvons donc accèder à toutes ces propriétés mais nous avons également accès aux méthodes disponibles de cet objet !

            // Ainsi pour récupèrer la chaine de caractères, il suffit d'utiliser la propriété Text de notre MyTexBox

            // De ce fait nous regardons que notre chaine de caractère contient au moins un caractère et dans ce cas-là,
            // nous traitons le nouveau caractère tapé, puis nous réinitialisons le texte contenu dans notre TextBox
            // (et c'est pour cette raison que nous avons mis une condition, car cet évènement (TextBox_TextChanged) est rappelé puisqu'il est modifié ! )
            if (MyTextBox.Text.Length > 0)
            {
                // Nous avons enfin notre caractère donc nous lancons le traitement de la lettre
                Traitement(MyTextBox.Text[0]);

                // Puis nous réinitialisons le texte dans la TextBox
                MyTextBox.Text = "";

                // A la suite du "cour" de mercredi, j'ai fait quelques recherches et j'ai trouvé comment faire disparaître le clavier
                // Il faut mettre le focus sur la page ou un autre élément de la page. De cette manière, le textBox perd le focus.
                // Cependant pour que cette fonction puisse fonctionner, il faut placer le textbox dans un conteneur qui possède la propriété IsTabStop que l'on met à true
                // (regardez le code xaml pour mieux, comprendre) Il s'agit d'un workaround mais il est nécessaire
                this.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
        }

        /// <summary>
        /// Cette méthode fait le traitement du caractère, c'est-à-dire qu'à partir du caractère passé en paramètres
        /// nous vérifions si la lettre est présente dans le mot et dans le cas contraire, on dessine le pendu
        /// </summary>
        /// <param name="c">caractère à vérifier dans le mot à trouver</param>
        public void Traitement(char c)
        {
            // on s'assure que la lettre sera en minuscule pour pouvoir faire la comparaison sans problème
            c = char.ToLowerInvariant(c);

            // Ce booléen permettra de savoir si la lettre proposé était bien dans le mot
            bool isInWord = false;

            // afin de pouvoir modifier un caractère nous devons "découper" notre string en tableau de caractère
            char[] result = wordToFind.Text.ToCharArray();

            // on boucle sur toute les lettres du mot et on compare, si la lettre correspond,
            // on modifie la lettre du mot à afficher en prenant en compte le décalage provoqué par les espaces
            for (int i = 0; i < wordToFindString.Length; i++)
            {
                if (wordToFindString[i] == c)
                {
                    result[i * 2] = c;
                    isInWord = true;
                }
            }

            // si la lettre n'est pas dans le mot on met à jour le compteur et l'image pour correspondre à l'avancement de la pendaison
            if (!isInWord)
            {
                compteurErreur++;

                // on récupère le chemin du dossier d'installation de l'application, afin d'être en mesure de récupèrer l'image.
                // NB : si quand vous tester, l'application vous retourne une erreur en vous disant qu'elle ne trouve pas l'image
                // vous devez faire clic droit sur l'image (depuis l'explorateur de solutions de visual studio) et "Propriétés"
                // puis mettre en "toujours copier" pour l'option correspondante
                string path = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                PenduImg.Source = new BitmapImage(new Uri(path + "\\Assets\\Pendu\\" + (compteurErreur + 1) + ".png", UriKind.RelativeOrAbsolute));
            }

            // on met à jour le texte du compteur
            Compteur.Text = compteurErreur.ToString();

            // on remplace le texte du textblock d'affichage du mot à chercher par notre tableau de char tout fraîchement modifié, que l'on retransforme en string
            wordToFind.Text = new string(result);

            // Puis nous vérifions si une des conditions de fin est atteinte
            VerifierVictoire();
        }

        /// <summary>
        /// Cette méthode va vérifier si la partie est terminée
        /// </summary>
        public void VerifierVictoire()
        {
            // Si on en est au 6 ème coup, alors on a perdu
            if (compteurErreur >= 6)
            {
                // On désactive le clavier pour éviter que l'utilisateur appuis sur le clavier pendant le laps de temps que met la popup pour s'afficher.
                MyTextBox.IsEnabled = false;

                // Si vous avez choisi une application Windows Phone 8.1 nous utilisons cette méthode pour afficher un message
                MessageDialog message = new MessageDialog("Vous êtes mort !");
                message.ShowAsync();
            }
            else
            {
                // Sinon on vérifie si l'on a gagné

                // Pour cela on doit regarder si les 2 mots sont identiques (mais on pourrait également vérifier si la proposition de l'utilisateur contient encore des underscores)

                // Donc pour pouvoir faire la comparaison, il faut d'abord enlever tout les espaces de la proposition faites.
                string propositionWord = wordToFind.Text.Replace(" ", "");

                // Puis il suffit de faire la comparaison des 2 strings (on applique la méthode ToLowerInvariant sur les deux pour éviter tout problème de casse potentiel)
                if (propositionWord.ToLowerInvariant() == wordToFindString.ToLowerInvariant())
                {
                    MyTextBox.IsEnabled = false;
                    MessageDialog message = new MessageDialog("Vous avez gagné ! =D");
                    message.ShowAsync();
                }
            }
        }
    }
}
