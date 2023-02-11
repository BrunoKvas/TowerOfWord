using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    [SerializeField] private bool _side = true;
    [SerializeField] private int _indent = 1;
    [SerializeField] private List<string> _secretsWords = new List<string>();
    public List<string> SecretsWords => _secretsWords;

    private Dictionary<int, List<char>> _braekSecretsWods = new Dictionary<int, List<char>>();

    public static PuzzleGenerator Instance;
    


    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SequenceGenerationWordRan();
        BrakeWord();
        MixingLetters();

    }

    public Dictionary<int, List<char>> BrokenWords()
    {
        return _braekSecretsWods;
    }

    void BrakeWord()
    {
        for (int i = 0; i < _secretsWords.Count; i++)
        {

            _braekSecretsWods[i] = _secretsWords[i].ToList();          
        }
    }

    void SequenceGenerationWordRan()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < _secretsWords.Count - 1; i++)
        {
            int j = random.Next(i + 1);
            var temp = _secretsWords[j];
            _secretsWords[j] = _secretsWords[i];
            _secretsWords[i] = temp;
        }


        for (int i = 0; i < _secretsWords.Count; i++)
        {
            for (int b = i; b < _secretsWords.Count; b++)
            {
                if (i % 2 == 0)
                {
                    if (_secretsWords[i].Length < _secretsWords[b].Length)
                    {
                        var max = _secretsWords[b];
                        _secretsWords[b] = _secretsWords[i];
                        _secretsWords[i] = max;
                    }
                }
                else
                {
                    if (_secretsWords[i].Length > _secretsWords[b].Length)
                    {
                        var min = _secretsWords[b];
                        _secretsWords[b] = _secretsWords[i];
                        _secretsWords[i] = min;
                    }
                }

            }
        }
    }


    void MixingLetters()
    {
                   
       
        for (int i = 0; i < _secretsWords.Count; i++)
        {
            if (i != 0)
            {
                _indent = _braekSecretsWods[i - 1].Count - _braekSecretsWods[i].Count;
            }
           
            if (_side)
            {

                for (int b = 0; b < _indent; b++)
                {
                    _braekSecretsWods[i].Add('0');
                    _side = false;
                }

            }
            else
            {
                for (int b = 0; b < _indent; b++)
                {
                    _braekSecretsWods[i].Insert(0, '0');
                    _side = true;
                }
            }

        }

        for (int i = 0; i < _braekSecretsWods.Count; i++)
        {
            for (int b = _braekSecretsWods[i].Count - 1; b > -1; b--)
            {

                if (_braekSecretsWods[i][b] == '0')
                {
                    for (int q = 1; q < 4; q++)
                    {
                        if (_braekSecretsWods.ContainsKey(i + q) && _braekSecretsWods[i + q][b] != '0')
                        {
                            _braekSecretsWods[i][b] = _braekSecretsWods[i + q][b];
                            _braekSecretsWods[i + q][b] = '0';
                            break;
                        }
                    }
                   
                  
                }


            }
        }

    }




    public void Puzzlegenerator()
    {
        _braekSecretsWods = new Dictionary<int, List<char>>();
        _side = _side == true ? false: true;
        _indent = UnityEngine.Random.Range(1, 3);
        SequenceGenerationWordRan();
        BrakeWord();
        MixingLetters();
    }




}
