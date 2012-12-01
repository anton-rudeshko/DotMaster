﻿namespace DotMaster.Core.Interfaces
{
    public interface ICrossReference
    {
        /// <summary>
        /// На какой BO смотрит этот xref
        /// </summary>
        string BaseObjKey { get; set; }

        /// <summary>
        /// Идентификатор источника
        /// </summary>
        ISource Source { get; set; }

        /// <summary>
        /// Первичный ключ из источника
        /// </summary>
        string SourceKey { get; set; }

        /// <summary>
        /// Данные объекта, содержащиеся в этом xref
        /// </summary>
        IBaseObject Object { get; set; }
    }
}