using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    // Déclaration des éléments d'interface utilisateur (UI)
    
    public TextMeshProUGUI userNameText;            // Texte pour afficher le nom d'utilisateur
    public TextMeshProUGUI lvlText;                 // Texte pour afficher le niveau
    public TextMeshProUGUI idText;                  // Texte pour afficher l'identifiant utilisateur
    public Button socialButton;                     // Bouton pour accéder à la scène sociale
    public TextMeshProUGUI socialButtonText;        // Texte du bouton social
    public Button playButton;                       // Bouton pour jouer
    public TextMeshProUGUI playButtonText;          // Texte du bouton de jeu
    public Button parametersButton;                 // Bouton pour accéder aux paramètres
    public TextMeshProUGUI parametersButtonText;    // Texte du bouton des paramètres
    public Button languageButton;                   // Bouton pour changer la langue
    public TextMeshProUGUI languageButtonText;      // Texte du bouton de langue

    // Méthode appelée au démarrage de la scène
    void Start()
    {
        // Initialiser les textes des éléments UI avec les données actuelles
        userNameText.text = DataManager.Instance.loginPMGResponse.data.nickname;
        lvlText.text = "" + DataManager.Instance.loginPMGResponse.data.level;
        idText.text = "Id: " + DataManager.Instance.loginPMGResponse.data._id;
        socialButtonText.text = DataManager.Instance.languageDictionary.MainMenu.SocialButton;
        playButtonText.text = DataManager.Instance.languageDictionary.MainMenu.PlayButton;
        parametersButtonText.text = DataManager.Instance.languageDictionary.MainMenu.ParametersButton;
        languageButtonText.text = DataManager.Instance.languageDictionary.MainMenu.LanguageButton;

        // Ajouter des écouteurs d'événements pour les boutons
        socialButton.onClick.AddListener(OnSocialButtonClick); 
        // playButton.onClick.AddListener(OnPlayButtonClick); 
        parametersButton.onClick.AddListener(OnParametersButtonClick); 
        languageButton.onClick.AddListener(OnLanguageButtonClick); 
    }

    // Méthode appelée lors du clic sur le bouton social
    public void OnSocialButtonClick()
    {
        SceneManager.LoadScene("Social");
    }

    // Méthode appelée lors du clic sur le bouton des paramètres
    public void OnParametersButtonClick()
    {
        SceneManager.LoadScene("Parameters");
    }

    // Méthode appelée lors du clic sur le bouton de choix de la langue
    public void OnLanguageButtonClick()
    {
        SceneManager.LoadScene("LanguageChoice");
    }
}
