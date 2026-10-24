using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private DiceMixer mixer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI defeatText;
    [SerializeField] private TextMeshProUGUI drawText;
    [SerializeField] private TextMeshProUGUI diceCountText;
    [SerializeField] private Button rollButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        RollStart();
        rollButton.onClick.AddListener(mixer.Roll);
        rollButton.onClick.AddListener(RollStart);
        mixer.OnScoreCalculated += ShowScore;
    }
    private void OnEnable()
    {
        
    }
    private void RollStart()
    {
        winText.text = $"Победа: {mixer.WinScore}";
        defeatText.text = $"Проигрыш: {mixer.LoseScore}";
        drawText.text = $"Ничья: {mixer.DrawScore}";
        diceCountText.text = $"Кубиков: {mixer.Dices}";
    }
        private void ShowScore(int score)
    {
        scoreText.text = $"Счёт: {score}";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
