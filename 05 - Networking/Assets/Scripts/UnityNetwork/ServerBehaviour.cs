using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We need to add com.unity.transport from the package manager
using Unity.Networking.Transport;
using Unity.Collections;

public class ServerBehaviour : MonoBehaviour
{
    public NetworkDriver m_Driver;
    private NativeList<NetworkConnection> _m_connections;

    // The Start method logic, includes declaring (and binding to) a NetworkDriver
    // and creating a NativeList to hold the connections.
    void Start()
    {
        // creates a NetworkDriver instance without any parameters
        m_Driver = NetworkDriver.Create();
        var _endpoint = NetworkEndpoint.AnyIpv4;
        _endpoint.Port = 9000;

        // binds the NetworkDriver instance to a specific network address and port
        // and if that doesn't fail, it calls the Listen method.
        if (m_Driver.Bind(_endpoint) != 0)
        {
            Debug.Log("Failed to bind to port 9000");
        }
        else
        {
            m_Driver.Listen();
        }

        // creates a NativeList to hold all the connections.
        _m_connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);
    }

    // The OnDestroy method logic, includes disposing of the NetworkDriver and the NativeList
    // that holds the connections.
    void OnDestroy()
    {
        // The check for m_Driver.IsCreated ensures you don't dispose of unallocated memory.
        // For example, UTP doesn’t allocate memory for disabled components.
        if (m_Driver.IsCreated)
        {
            m_Driver.Dispose();
            _m_connections.Dispose();
        }
    }

    // The Update loop logic, includes listening for connections, adding connections,
    // processing data, handling disconnections, and cleaning up stale connections.
    void Update()
    {
        m_Driver.ScheduleUpdate().Complete();

        // We must handle the connections. Start by cleaning up any stale connections from the list
        // before processing new ones. Cleaning up stale connections ensures you don't have any old
        // connections lying around when you iterate through the list to check for new events.
        for (int i = 0; i < _m_connections.Length; i++)
        {
            if (!_m_connections[i].IsCreated)
            {
                _m_connections.RemoveAtSwapBack(i);
                --i;
            }
        }

        // Accept new connections
        NetworkConnection c;
        while ((c = m_Driver.Accept()) != default(NetworkConnection))
        {
            _m_connections.Add(c);
            Debug.Log("Accepted a connection");
        }

        DataStreamReader stream;
        for (int i = 0; i < _m_connections.Length; i++)
        {
            if (!_m_connections[i].IsCreated)
                continue;
            NetworkEvent.Type cmd;
            while ((cmd = m_Driver.PopEventForConnection(_m_connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    uint number = stream.ReadUInt();
                    Debug.Log("Got " + number + " from the Client adding + 2 to it.");
                    number += 2;

                    m_Driver.BeginSend(NetworkPipeline.Null, _m_connections[i], out var writer);
                    writer.WriteUInt(number);
                    m_Driver.EndSend(writer);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    _m_connections[i] = default(NetworkConnection);
                }
            }
        }
    }
}