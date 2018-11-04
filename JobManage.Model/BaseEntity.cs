using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobManage.Model
{
    public class BaseEntity
    {
        /// <summary>
        /// 将DATaRow转成当前类实体对象
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public object ToEntity(System.Data.DataRow dataRow)
        {            
            var entityType = this.GetType();
            if (entityType == null || dataRow == null)
                return null;
            object entity = Activator.CreateInstance(entityType);
            CopyToEntity(entity, dataRow);
            return entity;
        }
        /// <summary>
        /// 将DataRow转成指定类的实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEntity<T>(System.Data.DataRow dataRow, T value) where T : new()
        {
            T item = new T();
            if (value == null || dataRow == null)
                return item;
            item = Activator.CreateInstance<T>();
            CopyToEntity(item, dataRow);
            return item;
        }
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dataRow"></param>
        private static void CopyToEntity(object entity, System.Data.DataRow dataRow)
        {
            if (entity == null || dataRow == null)
                return;
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();

            foreach (PropertyInfo property in propertyInfos)
            {
                if (!CanSetPropertyValue(property, dataRow))
                    continue;
                try
                {
                    if (dataRow[property.Name] is DBNull)
                    {
                        property.SetValue(entity, null, null);
                        continue;
                    }
                    SetPropertyValue(entity, dataRow, property);
                }
                finally
                {

                }
            }
        }
        /// <summary>
        ///  是否可赋值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static bool CanSetPropertyValue(PropertyInfo property, System.Data.DataRow dataRow)
        {
            if (!property.CanWrite)
                return false;
            if (!dataRow.Table.Columns.Contains(property.Name))
                return false;
            return true;
        }
        /// <summary>
        /// 赋值操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dataRow"></param>
        /// <param name="property"></param>
        private static void SetPropertyValue(object entity, System.Data.DataRow dataRow, PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
            {
                DateTime date = DateTime.MaxValue;
                DateTime.TryParse(dataRow[property.Name].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
                property.SetValue(entity, date, null);
            }
            else
            {
                property.SetValue(entity, dataRow[property.Name]);
            }
        }

    }
}
