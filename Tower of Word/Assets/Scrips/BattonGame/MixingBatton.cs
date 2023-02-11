using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixingBatton : MonoBehaviour
{
    [SerializeField] private Block _block;
    private List<Block> _newPositionBlocks = new List<Block>();
    public void Mixnig()
    {
        PuzzleGenerator.Instance.Puzzlegenerator();
        NewPosBlocks();
        MovingBlokcs();
    }

     void MovingBlokcs()
     {
        List<Block> oldblocks = FilldBlock.Instance.GetComponentsInChildren<Block>().ToList();

        for (int i = 0; i < _newPositionBlocks.Count; i++)
        {
            for (int b = 0; b < oldblocks.Count; b++)
            {
                if (oldblocks[b].Letter() == _newPositionBlocks[i].Letter())
                {
                    oldblocks[b].gameObject.transform.DOMove(_newPositionBlocks[i].gameObject.transform.position, 0.3f);
                    Destroy(_newPositionBlocks[i].gameObject);
                    oldblocks.RemoveAt(b);
                    break;

                }
            }
        }

     }
    void NewPosBlocks()
    {
        var brackWords = PuzzleGenerator.Instance.BrokenWords();
        _newPositionBlocks = new List<Block>();

        for (int x = 0; x < brackWords.Count; x++)
        {
            for (int y = 0; y < brackWords[x].Count; y++)
            {
                if (brackWords[x][y] == '0')
                    continue;


                var block = Instantiate(_block, new Vector3(y, x - 3, -0.1f * x), Quaternion.identity);
                block.inst(brackWords[x][y]);
                _newPositionBlocks.Add(block);
                block.gameObject.SetActive(false);


            }
        }
    }
}
