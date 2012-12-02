using System;

namespace DotMaster.Core.Interfaces
{
    public interface ISourceDataProvider<TXref> 
        where TXref : class, ICrossReference
    {
        event Action<TXref> OnData;
    }

    public class BaseSourceDataProvider<TXref> : ISourceDataProvider<TXref> 
        where TXref : class, ICrossReference
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