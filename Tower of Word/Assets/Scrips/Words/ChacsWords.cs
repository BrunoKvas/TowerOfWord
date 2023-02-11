using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacsWords : MonoBehaviour
{
    private int _countFoundWords = 0;
    public static ChacsWords Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public LevelState Chacs(List<Block> blocks, List<string> secretsWords)
    {
            string collectWord = blocks[0].Letter(); 

            for (int i = 1; i < blocks.Count; i++)
            {
                collectWord += blocks[i].Letter();
                blocks[i].Scale(true);

            }
         
            for (int i = 0; i < secretsWords.Count; i++)
            {
                if (collectWord == secretsWords[i])
                {
                    for (int b = 0; b < blocks.Count; b++)
                    {
                        BlockAnimation.Instance.DetermineDirectionMovement(blocks[b]);
                        AnimationFoundWord.Instance.CreateFoundWord(blocks[b]._chars[0], blocks[b].transform.position, _countFoundWords, b);
                        blocks[b].gameObject.SetActive(false);

                    }
                    secretsWords.RemoveAt(i);
                    _countFoundWords++;

                    return LevelState.WordFound; 
                }
            }          
        
        BlockAnimation.Instance.ColorBlock(1, ref blocks);
        BlockAnimation.Instance.Shake(ref blocks);
        LevelManager.Instance.StartAnimation();
        return LevelState.Animation;

    }
}
