using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public GameObject Player;

    public List<GameObject> Enemies;

    public List<GameObject> ItemsToCollect;

    public TMP_Text _textItemsToCollect;
    public GameObject gameOverPanel, winningPanel;

    private Player _player;
    private bool _gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
        _gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameEnded)
        {
            _textItemsToCollect.text = "x " + _player.getCollectedItems();

            if (_player.currentHealth < 0)
            {
                gameOverPanel.SetActive(true);
                _gameEnded = true;
            }

            if (_player.getCollectedItems() == 3)
            {
                winningPanel.SetActive(true);
                _gameEnded = true;
            }
        }
    }
}
