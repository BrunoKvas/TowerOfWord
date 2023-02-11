using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static LevelManager;

public class AssembleWord : MonoBehaviour
{
    private RaycastHit2D _oneBlockRay;
    private RaycastHit2D[] _blocksRay;
    private StDirectionMove _StateDirectionMove;
    private Block _blockOne;
    private List<Block> _blocks = new List<Block>();
    
    public static AssembleWord Instance;   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public LevelState ClickPosition()
    {
        _blocks = new List<Block>();
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return SelectFirstBlock(clickPosition);
        
    }

    LevelState SelectFirstBlock(Vector2 clickPosition)
    {
         _oneBlockRay = Physics2D.Raycast(clickPosition, clickPosition,0.2f);
        
        if (_oneBlockRay)
        {
            _blockOne = _oneBlockRay.transform.gameObject.GetComponent<Block>();
            _blockOne.WillChangeTheColor(0);
            _blockOne.Scale();
           
            return LevelState.ChooseDirectionoveMent;
        }

        return LevelState.SelectInitialBlock;             
                             
    }
    public List<Block> DetermineTheDirectionOfMovement()
    {
        if (_oneBlockRay)
        {
                        
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);           

            if (clickPosition.x - _oneBlockRay.transform.position.x > 0.5)
            {
                _blocksRay = Physics2D.RaycastAll(_oneBlockRay.transform.position,
                Vector3.right, clickPosition.x - _oneBlockRay.transform.position.x);

                if (_StateDirectionMove != StDirectionMove.Reght)
                {
                     ResetBlocks();
                    _StateDirectionMove = StDirectionMove.Reght;
                }

            }
            else if (clickPosition.y - _oneBlockRay.transform.position.y <= -0.5)
            {
                _blocksRay = Physics2D.RaycastAll(_oneBlockRay.transform.position,
                Vector3.up, clickPosition.y - _oneBlockRay.transform.position.y);

                if (_StateDirectionMove != StDirectionMove.Down)
                {
                    ResetBlocks();
                    _StateDirectionMove = StDirectionMove.Down;
                }
                
            }
            else
            {
                _blocksRay = new RaycastHit2D[1];
                _blocksRay[0] = _oneBlockRay;
            }
            FillListBlock(_blocksRay);
           

        }
        return _blocks;
    }    
    void FillListBlock(RaycastHit2D[] blockRay)
    {

        if (_blocks.Count > blockRay.Length)
        {
            _blocks[_blocks.Count - 1].WillChangeTheColor(3);
            _blocks.RemoveAt(_blocks.Count - 1);

        }      

        for (int i = 0; i < blockRay.Length; i++)
        {
            if(_blocks.Count -1 < i)
            {
                var block = blockRay[i].transform.gameObject.GetComponent<Block>();
                block.WillChangeTheColor(0);
                _blocks.Add(block);
                _blocks[i].Scale(true);
                           
            }            
           
        }
        _blocks[_blocks.Count - 1].Scale();                     
    }

    void ResetBlocks()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (i != 0)
            {
                _blocks[i].Scale(true);
                _blocks[i].WillChangeTheColor(3);
            }
        }
        _blocks = new List<Block>() { _blockOne };
    }
   
}
public enum StDirectionMove
{
    Up,
    Down,
    Left,
    Reght

}
   

