namespace Pacman
{ 
    public class Timer
    {
        private float m_CurrentTime;
        public bool IsFinished => m_CurrentTime <= 0;
        private float m_RetimeTimer;

        public Timer(float startTime)
        {
            Start(startTime);
        }


        public void Start(float startTime)
        {
            m_CurrentTime = startTime;
            m_RetimeTimer = startTime;
        }

        public void Restart()
        {
            Start(m_RetimeTimer);
        }

        public void RemoveTime(float deltaTime)
        {
            if (m_CurrentTime <= 0) return;
            m_CurrentTime -= deltaTime;
        }
    }
}
