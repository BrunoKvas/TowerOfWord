using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _fild;
    [SerializeField] private AnimationFoundWord _animationFoundWord;
    [SerializeField] private GameObject _btHint;
    [SerializeField] private GameObject _btSettings;
    [SerializeField] private GameObject _btMenu;
    [SerializeField] private GameObject _topic;
    [SerializeField] private GameObject _victory;
    [SerializeField] private GameObject _fierwoks;

    private List<Block> _blocks;
    private LevelState State;

    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)       
            Instance = this;       
    }

    private void Start()
    {
        _fild.gameObject.SetActive(false);
        StartCoroutine(StartGame());

    }

    private void Update()
    {
        switch (State)
        {
            case LevelState.SelectInitialBlock:
                if (Input.GetMouseButtonDown(0))
                    State = AssembleWord.Instance.ClickPosition();
                break;
            case LevelState.ChooseDirectionoveMent:
                if (Input.GetMouseButton(0))
                    _blocks = AssembleWord.Instance.DetermineTheDirectionOfMovement();
                else
                    State = ChacsWords.Instance.Chacs(_blocks, PuzzleGenerator.Instance.SecretsWords);
                break;

            case LevelState.WordFound:
                ChecNumberWords();
                break;

            case LevelState.Animation:
                break;

            case LevelState.Victory:
                LevelManager.Instance.VictoryLevel();
                break;
        }

    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.4f);
        _fild.gameObject.SetActive(true);
        State = LevelState.SelectInitialBlock;

    }
    void VictoryLevel()
    {
        _btHint.SetActive(false);
        _btSettings.SetActive(false);
        _btMenu.SetActive(false);
        _topic.SetActive(false);
        _victory.SetActive(true);     
        _fild.transform.DOMove(new Vector2(_fild.transform.position.x, 5f), 0.5f).SetDelay(1.8f);
        _animationFoundWord.transform.DOMove(new Vector2(_fild.transform.position.x, 5f), 0.5f).SetDelay(1.8f);     
    }

    public void StartAnimation()
    {
        StartCoroutine(ColorAnim());
    }


    IEnumerator ColorAnim()
    {
        yield return new WaitForSeconds(0.1f);
        BlockAnimation.Instance.ColorBlock(3, ref _blocks);
        State = LevelState.SelectInitialBlock;

    }


    void ChecNumberWords()
    {
        if (PuzzleGenerator.Instance.SecretsWords.Count == 0)
            State = LevelState.Victory;
        else
            State = LevelState.SelectInitialBlock;
    }

   
}
public enum LevelState
{
    SelectInitialBlock,
    ChooseDirectionoveMent,
    WordFound,
    Animation,
    Victory

}
