
using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class DateTimeData{
   public string datetime;
}
//Aprovechar mas todo lo que da esta api
/*
{
    "abbreviation": "CEST",
    "client_ip": "79.116.247.137",
    "datetime": "2024-04-17T19:28:59.023359+02:00",
    "day_of_week": 3,
    "day_of_year": 108,
    "dst": true,
    "dst_from": "2024-03-31T01:00:00+00:00",
    "dst_offset": 3600,
    "dst_until": "2024-10-27T01:00:00+00:00",
    "raw_offset": 3600,
    "timezone": "Europe/Madrid",
    "unixtime": 1713374939,
    "utc_datetime": "2024-04-17T17:28:59.023359+00:00",
    "utc_offset": "+02:00",
    "week_number": 16
}
*/
public class ConexionApiHora : MonoBehaviour
{
    private DateTime fechaActual; 

    void Start()
    {
        StartCoroutine(HttpsRespuesta());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(HttpsRespuesta());
        }
    }

    IEnumerator HttpsRespuesta()
    {
        string url = "https://worldtimeapi.org/api/ip";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
              //  Debug.Log(request.downloadHandler.text);//Funciopna

                //Deserializar JSON
                DateTimeData dateTime = JsonUtility.FromJson<DateTimeData>(request.downloadHandler.text);

                //Extraccion del dato 
                fechaActual = Convert.ToDateTime(dateTime.datetime);
                Debug.Log("Hora actual: " + fechaActual.TimeOfDay);
            }
            else
            {
                Debug.Log("Error: " + request.error);
            }
        }
    }
}
