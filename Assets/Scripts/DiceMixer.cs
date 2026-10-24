using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System;

public class DiceMixer : MonoBehaviour
{
    [SerializeField] GameObject DiceObject;
    [SerializeField] int DiceCount;
    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    [SerializeField] int drawScore;
    [SerializeField] int winScore;
    [SerializeField] int loseScore;

    public int WinScore => winScore;
    public int DrawScore => drawScore;
    public int LoseScore => loseScore;
    public int Dices => DiceCount;
    
    private GameObject[] dices;
    private Dice[] diceComponents;
    private Rigidbody[] rbComponents;

    public event Action<int> OnScoreCalculated;
    public event Action<int> OnDiceCounted;

    public void Roll() => DiceMix();

    private InputSystem_Actions input;
    private void OnEnable()  => input.Enable();
    private void OnDisable() => input.Disable();
    private void Awake()
    {
        input = new InputSystem_Actions();
        dices = new GameObject[DiceCount];
        diceComponents = new Dice[DiceCount];
        rbComponents = new Rigidbody[DiceCount];
        var distanceBetween = 3;
        for (int i = 0; i < DiceCount; i++)
        {
            dices[i] = Instantiate(DiceObject);
            dices[i].transform.SetParent(transform);
            dices[i].transform.localPosition = new Vector3(i * distanceBetween, 1, 0);
            diceComponents[i] = dices[i].GetComponent<Dice>();
            rbComponents[i] = dices[i].GetComponent<Rigidbody>();
        }
        OnDiceCounted?.Invoke(DiceCount);
    }

    private int score;
    
    private void Start()
    {
        
        
    }

    // Update is called once per frame
    private void Update()
    {

        if (score == 0 && diceComponents.All(dice => dice.IsStopped))
        {
            ScoreCount();
        }
        if (input.Player.Interact.WasPressedThisFrame())
        {
            DiceMix();
        }
        
    }

    private void DiceMix()
    {
        winScore = UnityEngine.Random.Range(1, DiceCount*6);
        loseScore = UnityEngine.Random.Range(1, winScore - 1);
        drawScore = UnityEngine.Random.Range(1, loseScore);
        for(int i = 0; i < dices.Length; i++)
        {
            var force = UnityEngine.Random.Range(minForce, maxForce);
            var dir = UnityEngine.Random.onUnitSphere;
            rbComponents[i].AddForce(force * dir, ForceMode.Impulse);
            rbComponents[i].AddTorque(UnityEngine.Random.onUnitSphere * force, ForceMode.Impulse);
            diceComponents[i].IsMoving();
        }
        score = 0;
    }

    private void ScoreCount()
    {
        score = 0;
        foreach (var dice in diceComponents)
        {
            score += dice.Value;
        }
        Debug.Log("Счёт: " + score);
        OnScoreCalculated?.Invoke(score);
    }
}
