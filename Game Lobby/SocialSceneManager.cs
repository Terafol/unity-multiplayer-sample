using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class SocialSceneManager : MonoBehaviour
{
    // Variables publiques pour les éléments de l'interface utilisateur
    public Button returnButton;                
    public TextMeshProUGUI returnButtonText;
    public TMP_InputField userNameInputField;
    public Button addButton;                
    public TextMeshProUGUI addButtonText;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI friendListText;
    public TextMeshProUGUI friendRequestText;
    public TextMeshProUGUI requestSentText;

    // Variables pour stocker la réponse de la liste d'amis et gérer l'API
    public FriendListResponse friendListResponse;
    private APIManager apiManager;
    public CheckNickname checkNickname;
    public FriendRequestError friendRequestError;

    // Méthode appelée au démarrage de la scène
    void Start()
    {
        // Initialiser APIManager
        apiManager = FindObjectOfType<APIManager>(); // Assurez-vous que l'objet APIManager existe dans la scène

        // Initialiser les textes de l'interface utilisateur avec les données de langue actuelles
        returnButtonText.text = DataManager.Instance.languageDictionary.Social.ReturnButton;
        addButtonText.text = DataManager.Instance.languageDictionary.Social.AddButton;
        errorText.text = "";
        friendListText.text = DataManager.Instance.languageDictionary.Social.FriendListText;
        friendRequestText.text = DataManager.Instance.languageDictionary.Social.FriendRequestText;
        requestSentText.text = DataManager.Instance.languageDictionary.Social.RequestSentText;

        // Lancer la coroutine pour récupérer la liste d'amis
        StartCoroutine(GetFriendList());

        // Ajouter des écouteurs d'événements pour les boutons
        returnButton.onClick.AddListener(OnReturnButtonClick);
        addButton.onClick.AddListener(OnAddButtonClick);
    }

    // Méthode appelée lors du clic sur le bouton de retour
    void OnReturnButtonClick()
    {
        // Charger la scène "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }

    // Méthode appelée lors du clic sur le bouton d'ajout d'ami
    void OnAddButtonClick()
    {
        // Lancer la coroutine pour envoyer une demande d'ami
        StartCoroutine(SendFriendRequest());
    }

    // Coroutine pour envoyer une demande d'ami
    private IEnumerator SendFriendRequest()
    {
        // Définir les en-têtes pour la requête
        string[][] headers = new string[][]
        {
            new string[] { "accept", "application/json" },
            new string[] { "token", DataManager.Instance.userSettingsDictionary.Token },
            new string[] { "Content-Type", "application/json" }
        };

        // Définir le pseudo de l'ami à ajouter
        checkNickname.nickname = userNameInputField.text;

        // Lancer la requête API pour envoyer la demande d'ami
        yield return StartCoroutine(apiManager.API("POST", headers, checkNickname, DataManager.Instance.generalSettingsDictionary.Api.FriendRequestUrl, (UnityWebRequest request) => 
        {
            // Gérer les erreurs de connexion ou de protocole
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                // Désérialiser la réponse d'erreur et l'afficher
                friendRequestError = JsonUtility.FromJson<FriendRequestError>(request.downloadHandler.text);
                errorText.text = friendRequestError.message;
                Debug.LogError("Error: " + request.error);
                Debug.LogError(request.downloadHandler.text);
            }
            else
            {
                // Afficher un message de succès si la demande d'ami a été envoyée avec succès
                Debug.Log(request.downloadHandler.text);
                errorText.text = DataManager.Instance.languageDictionary.Social.RequestSentText;
            }
        }));
    }

    // Coroutine pour récupérer la liste d'amis
    private IEnumerator GetFriendList()
    {
        // Définir les en-têtes pour la requête
        string[][] headers = new string[][]
        {
            new string[] { "accept", "application/json" },
            new string[] { "token", DataManager.Instance.userSettingsDictionary.Token },
        };

        // Lancer la requête API pour récupérer la liste d'amis
        yield return StartCoroutine(apiManager.API("GET", headers, null, DataManager.Instance.generalSettingsDictionary.Api.FriendListUrl, (UnityWebRequest request) => 
        {
            // Gérer les erreurs de connexion ou de protocole
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.downloadHandler.text);
            }
            else
            {
                // Désérialiser la réponse JSON et afficher la liste d'amis
                friendListResponse = JsonUtility.FromJson<FriendListResponse>(request.downloadHandler.text);
                for (int i = 0; i < friendListResponse.data.Count; i++)
                {
                    Debug.Log(friendListResponse.data[i].friend_user.nickname);
                }
            }
        }));
    }
}
