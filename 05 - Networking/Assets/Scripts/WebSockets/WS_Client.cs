// https://medium.com/unity-nodejs/websocket-client-server-unity-nodejs-e33604c6a006
// Add package by git url https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity
// Add Nuget package Websocketsharp net standard

using UnityEngine;
using WebSocketSharp;

public class Ws_Client : MonoBehaviour
{
    WebSocket ws;
    private void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
    }
    private void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello");
        }
    }
}