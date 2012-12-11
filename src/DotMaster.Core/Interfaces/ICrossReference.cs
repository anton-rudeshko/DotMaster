using System;

namespace DotMaster.Core.Interfaces
{
    public interface ICrossReference<TKey, TBase, TXref> : IEntity<TKey>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
    {
        /// <summary>
        /// На какой BO смотрит этот xref
        /// </summary>
        string BaseObjKey { get; set; }

        /// <summary>
        /// Идентификатор источника
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Первичный ключ из источника
        /// </summary>
        string SourceKey { get; set; }

        /// <summary>
        /// Данные объекта, содержащиеся в этом xref
        /// </summary>
        TBase Object { get; set; }

        /// <summary>
        /// Ссылка на базовую сущность, в которой содержится данный xref
        /// </summary>
        TBase BaseObject { get; set; }

        /// <summary>
        /// Дата и время пришедшего обновления
        /// </summary>
        DateTime UpdateDate { get; set; }
    }
}