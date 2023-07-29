using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private static SelectionManager _instance = null;
    private SelectionManager() { }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }

    public static SelectionManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] SelectionPlaceholder[] generators = new SelectionPlaceholder[] { };
    [SerializeField] int attempt = 0;
    [SerializeField] int attemptCap = 8;

    public void AddAttempt()
    {
        attempt++;
    }

    public bool IsMaxAttempt()
    {
        return attempt < attemptCap;
    }

    public void RefreshAll()
    {
        foreach (SelectionPlaceholder s in generators)
        {
            s.blockPlaced();
        }
    }
}
