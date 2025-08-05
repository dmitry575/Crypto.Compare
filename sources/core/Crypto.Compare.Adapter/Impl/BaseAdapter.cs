using Crypto.Compare.Adapter.Config;

namespace Crypto.Compare.Adapter.Impl;

public abstract class BaseAdapter : IAdapter
{
    
    protected readonly bool _enabled;
    protected readonly string _name;

    protected BaseConfig _config;
    public BaseAdapter(BaseConfig config)
    {
        _config = config;
        _enabled = config.Enabled;
        _name = config.Name;

        
    }
    public virtual string Name => _name;
    public virtual bool Enabled => _enabled;
    public abstract ICollection<string> GetSymbols();

    public BaseConfig Config => _config;

    
}
