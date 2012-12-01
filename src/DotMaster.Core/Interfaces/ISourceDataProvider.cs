using System;

namespace DotMaster.Core.Interfaces
{
    public interface ISourceDataProvider
    {
        event Action<ICrossReference> OnData;
    }

    public class BaseSourceDataProvider : ISourceDataProvider
    {
        public event Action<ICrossReference> OnData;

        public ISource Source { get; private set; }

        public BaseSourceDataProvider(ISource source)
        {
            Source = source;
        }

        public void Provide(ICrossReference xref)
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