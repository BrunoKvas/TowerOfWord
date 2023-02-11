using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class AnimationFoundWord : MonoBehaviour
{
    [SerializeField] private TextMeshPro _charsText;

    private List<string> _sicrtsWords;
    private GameObject[][] _positonCircle;


    public static AnimationFoundWord Instance;
   
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _sicrtsWords = PuzzleGenerator.Instance.SecretsWords;
        IncalisSicrtsWords();

    }
    void IncalisSicrtsWords()
    {
        _positonCircle = new GameObject[_sicrtsWords.Count][];

        for (int i = 0; i < _sicrtsWords.Count; i++)
        {
            _positonCircle[i] = new GameObject[_sicrtsWords[i].Length];
        }
    }


    public void CompletionPositonCircle(GameObject image, int x, int y)
    {

        _positonCircle[x][y] = image;
    }

    public void CreateFoundWord(char Char, Vector3 pos, int x,int y)
    {
       _charsText.text = Char.ToString();
       var letter =  Instantiate(_charsText, pos, Quaternion.identity);
       AnimationMoveWord(letter, x, y);
       letter.transform.SetParent(transform);

    }

    
    void AnimationMoveWord(TextMeshPro Char, int x, int y)
    {
        Char.transform.DOMove(_positonCircle[x][y].transform.position, 0.6f).SetDelay(0.04f * y);
        Char.transform.DOScale(new Vector2(0.05f, 0.05f), 0.6f); 
        var clorImog = _positonCircle[x][y].GetComponent<SpriteRenderer>();
        clorImog.DOColor(Color.green, 0.2f).SetDelay(0.6f);
    }


}
