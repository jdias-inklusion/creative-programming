using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We need to add com.unity.transport from the package manager
using Unity.Networking.Transport;
using Unity.Collections;

public class ClientBehaviour : MonoBehaviour
{
    public NetworkDriver m_Driver;
    public NetworkConnection m_Connection;
    public bool task_Completed;

    // The Start method logic, includes declaring (and binding to) a NetworkDriver.
    void Start()
    {
        m_Driver = NetworkDriver.Create();
        m_Connection = default(NetworkConnection);

        var _endpoint = NetworkEndpoint.LoopbackIpv4;
        _endpoint.Port = 9000;
        m_Connection = m_Driver.Connect(_endpoint);
    }

    // the OnDestroy method logic, includes disposing of the NetworkDriver.
    public void OnDestroy()
    {
        m_Driver.Dispose();
    }

    // The Update loop logic, includes connecting to the server, handling the Connect event,
    // sending data, receiving data, and handling disconnections.
    void Update()
    {
        // We start the client Update loop the same way as
        // the server: by calling m_Driver.ScheduleUpdate().Complete()
        m_Driver.ScheduleUpdate().Complete();
        // then ensuring the connection succeeded.
        if (!m_Connection.IsCreated) {
            if (!task_Completed)
            {
                Debug.Log("Something went wrong during the connect phase");
            }
            return;
        }

        DataStreamReader _stream;
        NetworkEvent.Type _command;

        while ((_command = m_Connection.PopEvent(m_Driver, out _stream)) != NetworkEvent.Type.Empty) {
            // The NetworkEvent.Type.Connect event tells you that you received a ConnectionAccept message
            // and are now connected to the remote peer.
            if (_command == NetworkEvent.Type.Connect)
            {
                Debug.Log("We are now connected to the server");

                uint valueToSend = 1;
                m_Driver.BeginSend(m_Connection, out var writer);
                writer.WriteUInt(valueToSend);
                m_Driver.EndSend(writer);
            }
            else if (_command == NetworkEvent.Type.Data) {
                // When the NetworkEvent type is Data, read the value you received back from the server
                // then call the Disconnect method.
                uint valueToReceive = _stream.ReadUInt();
                Debug.Log("I received the value = " + valueToReceive + " back from the server");
                task_Completed = true;
                m_Connection.Disconnect(m_Driver);
                m_Connection = default(NetworkConnection);
            }
            else if (_command == NetworkEvent.Type.Disconnect) {
                Debug.Log("Client got disconnected from server");
                m_Connection = default(NetworkConnection);
            }
        }
    }
}
