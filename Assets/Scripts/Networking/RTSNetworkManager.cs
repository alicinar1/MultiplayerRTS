using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RTSNetworkManager : NetworkManager
{
    [SerializeField] private GameObject _unitSpawner;
    [SerializeField] private GameOverHandler _gameOverHandler;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        GameObject unitSpawnerInstance = Instantiate(_unitSpawner, conn.identity.transform.position, conn.identity.transform.rotation);
        NetworkServer.Spawn(unitSpawnerInstance, conn);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (SceneManager.GetActiveScene().name.StartsWith("GameScene"))
        {
            GameOverHandler gameOverHandlerInstance = Instantiate(_gameOverHandler);

            NetworkServer.Spawn(gameOverHandlerInstance.gameObject);
        }
    }
}
