using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using Newtonsoft.Json;

public class ListTheContent : MonoBehaviour
{
    // Start is called before the first frame update
    public string prefabName;
    void Start()
    {
        if(SceneController.keyWord!=null)
        {
            StartCoroutine(GetRequest());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetRequest()
    {

        string uri = "https://bit.ly/3iSheyg";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {

            // Request and wait for the desired page.
            Debug.Log("In Coroutine Function");
            yield return webRequest.SendWebRequest();
            // Debug.Log(webRequest.result);
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
            CreateList(webRequest.downloadHandler.text);
        }
    }
    public void CreateList(string jsonString)
    {
        if (jsonString != null)
        {
           Root theContent = new Root();
            //Root theContent = JsonConvert.DeserializeObject<Root>(jsonString);
            
            JsonConvert.PopulateObject(jsonString, theContent);
            if (theContent.results.Count != 0)
            {
                string DkeyWord = SceneController.keyWord;
                //Debug.Log(DkeyWord);
                
                switch (DkeyWord)
                {
                    case "Hotel": prefabName = "RestaurantDataList";
                        break;
                    case "Park": prefabName = "ParkDataList";
                        break;
                    case "Hospital": prefabName = "HospitalDataList";
                        break;
                    case "School":
                        prefabName = "SchoolDataList";
                        break;
                    default:
                        break;
                }
                for (int i = 0; i < theContent.results.Count; i++)
                {
                    GameObject thePrefab = Instantiate(Resources.Load("Assets/Prefabs/" + prefabName)) as GameObject;
                    GameObject contentHolder = GameObject.FindGameObjectWithTag("Content");
                    thePrefab.transform.parent = contentHolder.transform;
                    Text theText = thePrefab.GetComponentInChildren<Text>();
                    theText.text = theContent.results[i].name;
                    
                    Debug.Log(theContent.results[i].name);
                   // Debug.Log(theContent.results[0].geometry.location.lat);
                    //Debug.Log(theContent.results[0].geometry.location.lng);
                }
            }
            
        }
    }
}

//Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Northeast
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Southwest
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
    public Viewport viewport { get; set; }
}

public class OpeningHours
{
    public bool open_now { get; set; }
}

public class Photo
{
    public int height { get; set; }
    public List<string> html_attributions { get; set; }
    public string photo_reference { get; set; }
    public int width { get; set; }
}

public class PlusCode
{
    public string compound_code { get; set; }
    public string global_code { get; set; }
}

public class Result
{
    public Geometry geometry { get; set; }
    public string icon { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public OpeningHours opening_hours { get; set; }
    public List<Photo> photos { get; set; }
    public string place_id { get; set; }
    public PlusCode plus_code { get; set; }
    public double rating { get; set; }
    public string reference { get; set; }
    public string scope { get; set; }
    public List<string> types { get; set; }
    public string vicinity { get; set; }
}

public class Root
{
    public List<object> html_attributions { get; set; }
    public List<Result> results { get; set; }
    public string status { get; set; }
}

//Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
/*
public class Info
{
    public string title { get; set; }
    public string version { get; set; }
}

public class Schema
{
    public string type { get; set; }

    [JsonProperty("$ref")]
    public string Ref { get; set; }
    public Items items { get; set; }
    public List<string> @enum { get; set; }
}

public class Parameter
{
    public string name { get; set; }
    public string @in { get; set; }
    public bool required { get; set; }
    public Schema schema { get; set; }
    public string username { get; set; }
    public string slug { get; set; }
    public string pid { get; set; }
}

public class ApplicationJson
{
    public Schema schema { get; set; }
}

public class Content
{
    [JsonProperty("application/json")]
    public ApplicationJson ApplicationJson { get; set; }
}

public class UserRepositories
{
    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class Links
{
    public UserRepositories userRepositories { get; set; }
    public UserRepository userRepository { get; set; }
    public RepositoryPullRequests repositoryPullRequests { get; set; }
    public PullRequestMerge pullRequestMerge { get; set; }
    public UserRepositories UserRepositories { get; set; }
    public UserRepository UserRepository { get; set; }
    public RepositoryPullRequests RepositoryPullRequests { get; set; }
    public PullRequestMerge PullRequestMerge { get; set; }
}

public class _200
{
    public string description { get; set; }
    public Content content { get; set; }
    public Links links { get; set; }
}

public class Responses
{
    public _200 _200 { get; set; }
    public _204 _204 { get; set; }
}

public class Get
{
    public string operationId { get; set; }
    public List<Parameter> parameters { get; set; }
    public Responses responses { get; set; }
}

public class 20UsersUsername
{
        public Get get { get; set; }
    }

    public class Items
{
    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class UserRepository
{
    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class 20RepositoriesUsername
{
        public Get get { get; set; }
    }

    public class RepositoryPullRequests
{
    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class 20RepositoriesUsernameSlug
{
        public Get get { get; set; }
    }

    public class 20RepositoriesUsernameSlugPullrequests
{
        public Get get { get; set; }
    }

    public class PullRequestMerge
{
    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class 20RepositoriesUsernameSlugPullrequestsPid
{
        public Get get { get; set; }
    }

    public class _204
{
    public string description { get; set; }
}

public class Post
{
    public string operationId { get; set; }
    public List<Parameter> parameters { get; set; }
    public Responses responses { get; set; }
}

public class 20RepositoriesUsernameSlugPullrequestsPidMerge
{
        public Post post { get; set; }
    }

    public class Paths
{
    [JsonProperty("/2.0/users/{username}")]
    public 20UsersUsername _20UsersUsername { get; set; }

    [JsonProperty("/2.0/repositories/{username}")]
    public 20RepositoriesUsername _20RepositoriesUsername { get; set; }

    [JsonProperty("/2.0/repositories/{username}/{slug}")]
    public 20RepositoriesUsernameSlug _20RepositoriesUsernameSlug { get; set; }

    [JsonProperty("/2.0/repositories/{username}/{slug}/pullrequests")]
    public 20RepositoriesUsernameSlugPullrequests _20RepositoriesUsernameSlugPullrequests { get; set; }

    [JsonProperty("/2.0/repositories/{username}/{slug}/pullrequests/{pid}")]
    public 20RepositoriesUsernameSlugPullrequestsPid _20RepositoriesUsernameSlugPullrequestsPid { get; set; }

    [JsonProperty("/2.0/repositories/{username}/{slug}/pullrequests/{pid}/merge")]
    public 20RepositoriesUsernameSlugPullrequestsPidMerge _20RepositoriesUsernameSlugPullrequestsPidMerge { get; set; }
}

public class UserRepositories2
{
    public string operationId { get; set; }
    public Parameters parameters { get; set; }
}

public class UserRepository2
{
    public string operationId { get; set; }
    public Parameters parameters { get; set; }
}

public class RepositoryPullRequests2
{
    public string operationId { get; set; }
    public Parameters parameters { get; set; }
}

public class PullRequestMerge2
{
    public string operationId { get; set; }
    public Parameters parameters { get; set; }
}

public class Username
{
    public string type { get; set; }
}

public class Uuid
{
    public string type { get; set; }
}

public class Properties
{
    public Username username { get; set; }
    public Uuid uuid { get; set; }
    public Slug slug { get; set; }
    public Owner owner { get; set; }
    public Id id { get; set; }
    public Title title { get; set; }
    public Repository repository { get; set; }
    public Author author { get; set; }
}

public class User
{
    public string type { get; set; }
    public Properties properties { get; set; }
}

public class Slug
{
    public string type { get; set; }
}

public class Owner
{
    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class Repository
{
    public string type { get; set; }
    public Properties properties { get; set; }

    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class Id
{
    public string type { get; set; }
}

public class Title
{
    public string type { get; set; }
}

public class Author
{
    [JsonProperty("$ref")]
    public string Ref { get; set; }
}

public class Pullrequest
{
    public string type { get; set; }
    public Properties properties { get; set; }
}

public class Schemas
{
    public User user { get; set; }
    public Repository repository { get; set; }
    public Pullrequest pullrequest { get; set; }
}

public class Components
{
    public Links links { get; set; }
    public Schemas schemas { get; set; }
}

public class Root
{
    public string openapi { get; set; }
    public Info info { get; set; }
    public Paths paths { get; set; }
    public Components components { get; set; }
}
*/


