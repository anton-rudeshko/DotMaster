using DotMaster.Core.Model;

namespace DotMaster.Core
{
    public interface IMasterDataBase
    {
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        TXref QueryForXref<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <summary>
        /// Сохранить базовый объект со всеми перекрёстными ссылками в базу
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="baseObject">Базовый объект для сохранения</param>
        /// <returns>Сохранённый базовый объект</returns>
        TBase Save<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;
    }
}