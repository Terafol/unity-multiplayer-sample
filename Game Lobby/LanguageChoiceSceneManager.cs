using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

public class LanguageChoiceSceneManager : MonoBehaviour
{
    public TextMeshProUGUI instructionText;     // Texte d'instruction pour l'utilisateur
    public TMP_Dropdown languageSelection;      // Dropdown pour la sélection de la langue
    public Button confirmButton;                // Bouton de confirmation
    public TextMeshProUGUI confirmButtonText;   // Texte du bouton de confirmation

    // Méthode appelée au démarrage de la scène
    void Start()
    {
        // Initialiser les textes avec les données de langue actuelles
        instructionText.text = DataManager.Instance.languageDictionary.LanguageChoiceData.InstructionText;
        confirmButtonText.text = DataManager.Instance.languageDictionary.LanguageChoiceData.ConfirmButton;

        // Trouver l'index de la langue par défaut et le définir dans le dropdown
        languageSelection.value = IndexOf(DataManager.Instance.generalSettingsDictionary.Parameters.Languages, DataManager.Instance.userSettingsDictionary.DefaultLanguage);

        // Ajouter les écouteurs d'événements pour la sélection de langue et le bouton de confirmation
        languageSelection.onValueChanged.AddListener(OnLanguageChange);
        confirmButton.onClick.AddListener(OnConfirmButtonClick); 
    }

    // Méthode appelée lors du changement de langue dans le dropdown
    private void OnLanguageChange(int index)
    {
        // Charger les données de langue correspondant à la langue sélectionnée
        LoadLanguageChoiceData(DataManager.Instance.generalSettingsDictionary.Parameters.Languages[index]);
    }

    // Méthode appelée lors du clic sur le bouton de confirmation
    private void OnConfirmButtonClick()
    {
        // Vérifier si l'utilisateur est déjà connecté
        if (DataManager.Instance.userSettingsDictionary.IsAlreadyLog == false)
        {
            // Charger la scène suivante "ConnectPlay" si l'utilisateur n'est pas connecté
            SceneManager.LoadScene("ConnectPlay");
        }
        else
        {
            // Charger la scène "MainMenu" si l'utilisateur est déjà connecté
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Charger les données de langue depuis un fichier JSON
    private void LoadLanguageChoiceData(string file)
    {
        // Charger le fichier JSON correspondant à la langue sélectionnée
        string jsonTextFile = File.ReadAllText(Application.streamingAssetsPath + "/" + DataManager.Instance.generalSettingsDictionary.Path.Language + file + ".json");
        if (jsonTextFile != null)
        {
            // Désérialiser les données JSON en un dictionnaire de langue
            DataManager.Instance.languageDictionary = JsonUtility.FromJson<LanguageDictionary>(jsonTextFile);

            // Mettre à jour les textes d'instruction et du bouton avec les nouvelles données de langue
            instructionText.text = DataManager.Instance.languageDictionary.LanguageChoiceData.InstructionText;
            confirmButtonText.text = DataManager.Instance.languageDictionary.LanguageChoiceData.ConfirmButton;

            // Mettre à jour la langue par défaut dans les paramètres généraux
            DataManager.Instance.userSettingsDictionary.DefaultLanguage = file;

            // Sauvegarder les paramètres généraux mis à jour dans le fichier JSON
            string json = JsonUtility.ToJson(DataManager.Instance.userSettingsDictionary, true);
            File.WriteAllText(Application.persistentDataPath + "/" + DataManager.Instance.generalSettingsDictionary.Path.UserSettings, json);
        }
        else
        {
            // Afficher une erreur si le fichier JSON n'est pas trouvé
            Debug.LogError("Le fichier JSON n'a pas été trouvé !");
        }
    }

    // Méthode pour trouver l'index d'un élément dans une liste
    private int IndexOf(string[] list, string value)
    {
        // Parcourir la liste pour trouver l'index de l'élément correspondant à la valeur donnée
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == value)
                return i;
        }
        return 0; // Retourner 0 si l'élément n'est pas trouvé
    }
}
