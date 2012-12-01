using System;

namespace DotMaster.Core.Interfaces
{
    public interface ISourceDataProvider<TXref, TBase>
        where TXref : class, ICrossReference<TBase>
        where TBase : IBaseObject
    {
        event Action<TXref> OnData;
    }

    public class BaseSourceDataProvider<TXref, TBase> : ISourceDataProvider<TXref, TBase>
        where TXref : class, ICrossReference<TBase>
        where TBase : IBaseObject
    {
        public event Action<TXref> OnData;

        public ISource Source { get; private set; }

        public BaseSourceDataProvider(ISource source)
        {
            Source = source;
        }

        public void Provide(TXref xref)
        {
            if (xref == null)
            {
                throw new ArgumentNullException("xref");
            }
            if (string.IsNullOrWhiteSpace(xref.SourceKey))
            {
                throw new ArgumentException("Source key can not be empty", "xref");
            }
            xref.Source = Source;
            if (OnData != null)
            {
                OnData(xref);
            }
        }
    }
}