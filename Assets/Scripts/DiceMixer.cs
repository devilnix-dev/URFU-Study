using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class DiceMixer : MonoBehaviour
{
    [SerializeField] GameObject DiceObject;
    [SerializeField] int DiceCount;
    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    private GameObject[] dices;
    private Dice[] diceComponents;
    private Rigidbody[] rbComponents;

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
    }

    private int score;
    
    private void Start()
    {
        DiceMix();
        
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
        for(int i = 0; i < dices.Length; i++)
        {
            var force = Random.Range(minForce, maxForce);
            var dir = Random.onUnitSphere;
            rbComponents[i].AddForce(force * dir, ForceMode.Impulse);
            rbComponents[i].AddTorque(Random.onUnitSphere * force, ForceMode.Impulse);
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
    }
}
