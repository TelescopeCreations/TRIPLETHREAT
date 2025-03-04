//@input Component.Text questionText
//@input Component.Text optionLeftText
//@input Component.Text optionRightText
//@input Component.Text scoreText
//@input SceneObject headTracker

var questions = [
    { question: "Which vibe do you prefer?", options: ["Classic & Elegant", "Modern & Trendy"], answer: 0 },
    { question: "Favorite wedding setting?", options: ["Beach", "Ballroom"], answer: 1 }
];

var currentQuestion = 0;
var score = 0;
var threshold = 10; // Degrees to register tilt

function loadQuestion() {
    var q = questions[currentQuestion];
    script.questionText.text = q.question;
    script.optionLeftText.text = q.options[0];
    script.optionRightText.text = q.options[1];
}

function checkHeadTilt() {
    var rotation = script.headTracker.getTransform().getWorldRotation();
    var headTilt = rotation.toEulerAngles().y * 180 / Math.PI;

    if (headTilt < -threshold) { // Tilt Left
        answerSelected(0);
    } else if (headTilt > threshold) { // Tilt Right
        answerSelected(1);
    }
}

function answerSelected(choice) {
    if (choice === questions[currentQuestion].answer) {
        score++;
    }

    currentQuestion++;
    if (currentQuestion < questions.length) {
        loadQuestion();
    } else {
        script.questionText.text = "Quiz Over! Score: " + score;
    }
    script.scoreText.text = "Score: " + score;
}

var event = script.createEvent("UpdateEvent");
event.bind(checkHeadTilt);

loadQuestion();
