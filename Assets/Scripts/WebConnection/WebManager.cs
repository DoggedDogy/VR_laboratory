using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    public static WebManager Instance;
    public AccauntData data = new AccauntData();
    private static readonly HttpClient client = new HttpClient();
    public GameObject LoginInput;
    public GameObject PasswordInput;
    public GameObject LableToChange;
    public GameObject CardManager;
    public WorkData work;
    public string workId;
    public List<LabData> labs;
    private string baseURL = "https://localhost:44359/api/Labworks";

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void OnLogin()
    {
        data.Login = LoginInput.GetComponent<TMP_InputField>().text;
        data.Password = PasswordInput.GetComponent<TMP_InputField>().text;

        LoginModel content = new LoginModel();
        content.Login = data.Login;
        content.Password = data.Password;
        StartCoroutine(LoginAsync(content));
    }
    private IEnumerator LoginAsync(LoginModel content)
    {
        string json = JsonConvert.SerializeObject(content);
        var uwr = new UnityWebRequest(baseURL, "POST");
        uwr.certificateHandler = new CertificateRevalidation();
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
        if (json == "{}")
            Debug.Log(json.ToString());
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log("Error");
        else
        {
            Debug.Log(uwr.downloadHandler.text);
            data.Complete(JsonConvert.DeserializeObject<twoData>(uwr.downloadHandler.text));
            LableToChange.GetComponent<TMP_Text>().SetText("Авторизация прошла успешно, " + data.Name, false);
        }
        uwr.Dispose();

        yield return LoadLabsAsync();
        CardManager.GetComponent<Unity.VRTemplate.StepManager>().CardGenerator(labs);
    }

    private IEnumerator LoadLabsAsync()
    {
        string url = baseURL + "?userId=" + data.User_Id;
        Debug.Log(url);
        var uwr = UnityWebRequest.Get(url);
        uwr.certificateHandler = new CertificateRevalidation();
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error: " + uwr.error);
        }
        else
        {
            string jsonResponse = uwr.downloadHandler.text;
            Debug.Log(jsonResponse);
            labs = JsonConvert.DeserializeObject<List<LabData>>(jsonResponse);
            Debug.Log(labs);
        }
        //string json = JsonConvert.SerializeObject(data.User_Id);
        //var uwr = new UnityWebRequest(baseURL, "GET");
        //uwr.certificateHandler = new CertificateRevalidation();
        //byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        //uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        //uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //uwr.SetRequestHeader("Content-Type", "application/json");
        //uwr.SendWebRequest();

        //List<LabData> labs = new List<LabData>();
        //Debug.Log(uwr.downloadHandler.text);
        //labs = JsonConvert.DeserializeObject<List<LabData>>(uwr.downloadHandler.text);
        //uwr.Dispose();
        //return labs;
    }
    public void sendPutRequest()
    {
        Debug.Log("Put");

        StartCoroutine(PutLab());
    }
    private IEnumerator PutLab()
    {
        Debug.Log("Put");
        string json = JsonConvert.SerializeObject(work);
        string putUrl = baseURL;
        Debug.Log(putUrl);
        if (workId != "0") 
            putUrl += "/" + workId;
        var uwr = new UnityWebRequest(putUrl, "PUT");
        uwr.certificateHandler = new CertificateRevalidation();
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
        if (json == "{}")
            Debug.Log(json.ToString());
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log("Error");
        else
        {
            Debug.Log("workID = " + uwr.downloadHandler.text);
        }
        workId = JsonConvert.DeserializeObject<WorkId>(uwr.downloadHandler.text).User_Id;
        uwr.Dispose();
    }
}
public class AccauntData
{
    public void Complete(twoData one)
    {
        User_Id = one.User_Id;
        Student_Id = one.Student_Id;
        Name = one.Name;
    }

    public string User_Id { get; set; }
    public string Student_Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}
public class twoData
{
    public string User_Id { get; set; }
    public string Student_Id { get; set; }
    public string Name { get; set; }
}
public class LabData
{
    public LabData(string lab_Id, string lab_Name, int steps, string description)
    {
        Lab_Id = lab_Id;
        Lab_Name = lab_Name;
        Steps = steps;
        Description = description;
    }

    public string Lab_Id { get; set; }
    public string Lab_Name { get; set; }
    public int Steps { get; set; }
    public string Description { get; set; }
}
public class WorkData
{
    public string Lab_Id { get; set; }
    public string Student_Id { get; set; }
    public int Done_Steps { get; set; }
}
public class WorkId
{
    public string User_Id { get; set; }
}
public class LoginModel
{
    public string Login { get; set; }
    public string Password { get; set; }
}
public class CertificateRevalidation : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
