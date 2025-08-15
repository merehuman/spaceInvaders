using System;
using static SE456.SceneContext;
using System.Diagnostics;

namespace SE456
{
    public class ScoreManager
    {
        //instance
        private static ScoreManager instance;
        private static int currentScore = 0;
        private static int highScore = 0;

        public static ScoreManager getInstance()
        {
            if (instance == null)
            {
                instance = new ScoreManager();
            }
            return instance;
        }

        private ScoreManager() { }

        public static void UpdateScore(Enum invaderType)
        {
            int points = GetPointsForInvader(invaderType);
            currentScore += points;

            if (currentScore > highScore)
            {
                highScore = currentScore;
            }

            UpdateScoreDisplay();
        }

        public static void ResetScore()
        {
            currentScore = 0;
            UpdateScoreDisplay();
        }

        public static int GetCurrentScore()
        {
            return currentScore;
        }

        public static int GetHighScore()
        {
            return highScore;
        }

        private static int GetPointsForInvader(Enum invaderType)
        {
            switch (invaderType)
            {
                case InvaderCategory.Type.Squid:
                    return 30;
                case InvaderCategory.Type.Crab:
                    return 20;
                case InvaderCategory.Type.Octopus:
                    return 10;
                case GameObject.Name.UFO:
                    return new Random().Next(50, 300); // UFO can give variable points
                default:
                    return 0;
            }
        }

        public static void UpdateScoreDisplay()
        {
            Font pScoreFont = FontMan.Find(Font.Name.ActualScore);
            Font pHighScoreFont = FontMan.Find(Font.Name.ActualHiScore);

            if (pScoreFont != null)
            {
                pScoreFont.UpdateMessage(currentScore.ToString("0 0 0 0")); // Format as 4 digits
            }

            if (pHighScoreFont != null)
            {
                pHighScoreFont.UpdateMessage(highScore.ToString("0 0 0 0")); // Format as 4 digits
            }
        }
    }
}
