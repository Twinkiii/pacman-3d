namespace Pacman
{
    public enum Sound
    {
        PelletCollect = 0,    
        PlayerDie = 1,        
        Win = 2,              
        Lose = 3,             
        GameBGM = 4,          
        MenuBGM = 5
    }
    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}
