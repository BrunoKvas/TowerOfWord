using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation : MonoBehaviour
{
    public static BlockAnimation Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void ColorBlock(int valye, ref List<Block> _blocks)
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].WillChangeTheColor(valye);

        }
    }

   public void Shake(ref List<Block> _blocks)
   {
        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].transform.DOShakePosition(0.1f, 0.07f, 1, 0.01f).SetLoops(2, LoopType.Restart);
        }
   }



    public void DetermineDirectionMovement(Block block)
    {
        var PosUP = TakeBlocks(block.gameObject.transform, Vector2.up, 5f);
        if (PosUP.Length != 0)
            Move(PosUP, block.gameObject.transform);
        else
        {
            var PosLeft = TakeBlocks(block.gameObject.transform, Vector2.left, 1f);
            if (PosLeft.Length != 0)
                Move(PosLeft, block.gameObject.transform);
        }

    }

    RaycastHit2D[] TakeBlocks(Transform RemotBlock, Vector2 Direction, float length)
    {
        var Pos = Physics2D.RaycastAll((Vector2)RemotBlock.position + Direction, Direction, length);
        return Pos;
    }

    void Move(RaycastHit2D[] Direction, Transform RemotBlock)
    {
        for (int i = 0; i < Direction.Length; i++)
        {
            if (i == 0)
                Direction[i].transform.DOMove(RemotBlock.position, 0.2f);
            else
                Direction[i].transform.DOMove(Direction[i - 1].transform.position, 0.2f);
        }
    }


}
