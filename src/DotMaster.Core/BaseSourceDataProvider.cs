﻿using System;
using DotMaster.Core.Model;

namespace DotMaster.Core
{
    public abstract class BaseSourceDataProvider<TKey, TBase, TXref> : ISourceDataProvider<TKey, TBase, TXref> 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
            where TBase : class, IBaseObject<TKey, TBase, TXref>
    {
        public event Action<TXref> OnData;

        public string Source { get; private set; }

        protected BaseSourceDataProvider(string source)
        {
            Source = source;
        }

        /// <summary>
        /// В реализации следует сделать выполнять этот метод в отдельной транзакции
        /// </summary>
        public void Provide(TXref xref)
        {
            if (xref == null)
            {
                throw new ArgumentNullException("xref");
            }
            if (string.IsNullOrWhiteSpace(xref.SourceKey))
            {
                throw new ArgumentException(I18n.XrefSourceKeyIsEmpty, "xref");
            }

            // automatically update last update date if not set
            if (xref.LastUpdate == DateTime.MinValue)
            {
                xref.LastUpdate = DateTime.Now;
            }
            xref.Source = Source;
            if (OnData != null)
            {
                OnData(xref);
            }
        }
    }
}