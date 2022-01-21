using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Monster[] _monsters;
    [SerializeField] string _nextLevelName;

    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    private bool MonstersAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        return true;
    }
    // Start is called before the first frame update
    

    private void GoToNextLevel()
    {
        Debug.Log("Go to next level" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }
    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead()) {
            GoToNextLevel();

        }
    }
}
