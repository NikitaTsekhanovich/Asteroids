using System;

namespace Infrastructure.Properties
{
    public interface ICanPluginInitialize
    {
        public event Action OnInitialized;
        public void Initialize();
    }
}
