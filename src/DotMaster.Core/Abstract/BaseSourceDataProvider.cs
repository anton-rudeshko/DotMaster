using System;
using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Abstract
{
    public abstract class BaseSourceDataProvider<TBase, TXref> : ISourceDataProvider<TBase, TXref> 
            where TXref : class, ICrossReference<TBase, TXref>
            where TBase : class, IBaseObject<TBase, TXref>
    {
        public event Action<TXref> OnData;

        public string Source { get; private set; }

        protected BaseSourceDataProvider(string source)
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