using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBatton : MonoBehaviour
{


   public void ShowFirstLetter()
   {

        var blocks = FilldBlock.Instance.GetComponentsInChildren<Block>();
        var Words = PuzzleGenerator.Instance.SecretsWords;

        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].Letter()[0] == Words[0][0])
            {
                blocks[i].WillChangeTheColor(1);
                break;
            }
        }
    }


}
