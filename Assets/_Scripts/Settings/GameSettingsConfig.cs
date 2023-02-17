namespace RiaShooter.Scripts.Settings
{
    internal struct GameSettingsConfig
    {
        public bool Sound;
        public bool Music;
        public int Language;
        public bool PostEffects;
        public int AntiAlizing;

        public GameSettingsConfig(bool sound = true, bool music = true, int language = 1, bool postEffects = true, int antiAlizing = 2)
        {
            Sound = sound;
            Music = music;
            Language = language;
            PostEffects = postEffects;
            AntiAlizing = antiAlizing;
        }
    }
}
