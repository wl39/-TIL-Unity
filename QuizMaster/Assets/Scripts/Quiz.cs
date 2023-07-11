using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField]  GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScroeKeeper scroeKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider ProgressBar;

    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scroeKeeper = FindObjectOfType<ScroeKeeper>();
        ProgressBar.maxValue = questions.Count;
        ProgressBar.value = 0;
    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        
        if (timer.loadNextQuestion) {
            if (ProgressBar.value == ProgressBar.maxValue) {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void DisplayAnswer(int index) {
        if (index == correctAnswerIndex) {
            questionText.text = "Correct!";
            scroeKeeper.IncrementCorrectAnswers();
        } else {
            questionText.text = "Sorry... The correct answer was:\x0A";
            questionText.text += currentQuestion.GetAnswer(correctAnswerIndex);
            scoreText.text = "Score: " + scroeKeeper.CalculateScore() + "%";
        }

        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scroeKeeper.CalculateScore() + "%";

        
    }

    void GetNextQuestion() {
        if (questions.Count > 0) {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            ProgressBar.value++;
            scroeKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion() {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        
        if (questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);
    }

    public void DisplayQuestion() {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++) {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();

            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite() {
        for (int i = 0; i < answerButtons.Length; i++) {
            answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }
}
