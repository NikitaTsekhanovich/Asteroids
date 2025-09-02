namespace Application.Configs
{
    public class Config
    {
        public Config(string guid)
        {
            Guid = guid;
        }
        
        public string Guid { get; private set; }
    }
}
