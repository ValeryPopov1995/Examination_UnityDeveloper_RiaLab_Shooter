namespace RiaShooter.Scripts.Settings
{
    internal struct GameSettingsData
    {
        public bool Sound;
        public bool Music;
        public int Language;
        public bool PostEffects;
        public int AntiAlizing;

        public GameSettingsData(bool sound = true, bool music = true, int language = 1, bool postEffects = true, int antiAlizing = 2)
        {
            Sound = sound;
            Music = music;
            Language = language;
            PostEffects = postEffects;
            AntiAlizing = antiAlizing;
        }
    }
}
