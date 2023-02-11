using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FilldBlock : MonoBehaviour
{
    [SerializeField] private Block _block;
 
    private Dictionary<int, List<char>> _brackWords = new Dictionary<int, List<char>>();

    public static FilldBlock Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {       
        _brackWords = PuzzleGenerator.Instance.BrokenWords();
        CreateFilld();            
    }
     
   public void CreateFilld()
   {
        float i = 0;

        for (int x = 0; x < _brackWords.Count; x++)
        {
            for (int y = 0; y < _brackWords[x].Count; y++)
            {
                if (_brackWords[x][y] == '0')               
                    continue;
                
                  
                var block = Instantiate(_block, new Vector3(y, x + 8, -0.1f * x), Quaternion.identity,transform);
                block.inst(_brackWords[x][y]);
                block.transform.DOMove(new Vector3(y, x - 3, block.transform.position.z), 0.3f + i);
                i += 0.05f;

            }
        }
    }

  

    

    
  
    
}
