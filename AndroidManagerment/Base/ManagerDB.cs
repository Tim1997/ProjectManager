using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public abstract class ManagerDB<T> : IRepository<T> where T : Base.BaseModel
    {
        protected readonly string PATH_DB = Constants.BaseConstant.BASE_PATH_FOLDER_DATA;
        protected readonly string EXTENDTION = ".json";
        protected string _pathFile;
        protected List<T> _collection;

        /// <summary>
        /// khởi tạo kết nối đến collection
        /// </summary>
        /// <param name="collection"></param>
        protected void InitCollection(string collection)
        {
            if (!Directory.Exists(PATH_DB))
                Directory.CreateDirectory(PATH_DB);

            _pathFile = PATH_DB + collection + EXTENDTION;
            if (File.Exists(_pathFile))
            {
                string contents = File.ReadAllText(_pathFile);
                _collection = JsonConvert.DeserializeObject<List<T>>(contents);

                if (_collection == null)
                    _collection = new List<T>();
            }
            else
            {
                File.Create(_pathFile);
                _collection = new List<T>();
            }
        }

        /// <summary>
        /// tạo mới entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _collection.Add(entity);
            var json = JsonConvert.SerializeObject(_collection);
            File.WriteAllText(_pathFile, json);
        }

        /// <summary>
        /// đếm số document trong collection
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _collection.Count();
        }

        /// <summary>
        /// xóa document
        /// </summary>
        /// <param name="_id"></param>
        public void Delete(string _id)
        {
            var index = _collection.FindIndex(x => x.Id == _id);
            _collection.RemoveAt(index);

            var json = JsonConvert.SerializeObject(_collection);
            File.WriteAllText(_pathFile, json);
        }

        /// <summary>
        /// kiểm tra sự tồn tại của document
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool Exist(string _id)
        {
            return _collection.Any(x => x.Id == _id);
        }

        /// <summary>
        /// tìm
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T Find(Predicate<T> match)
        {
            return _collection.Find(match);
        }

        /// <summary>
        /// Lấy ra tất cả
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return _collection;
        }

        /// <summary>
        /// cập nhật giá trị
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            var item = _collection.Find(x => x.Id == entity.Id);
            item.LastUpdateDate = DateTime.Now;

            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.Name != "CreateDate")
                {
                    var v = entity.GetType().GetProperty(prop.Name).GetValue(entity, null);
                    prop.SetValue(item, v);
                }
            }
            item.LastUpdateDate = DateTime.Now;

            var json = JsonConvert.SerializeObject(_collection);
            File.WriteAllText(_pathFile, json);
        }

        /// <summary>
        /// tìm kiếm tất cả kết quả phù hợp
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public List<T> FindAll(Predicate<T> match)
        {
            return _collection.FindAll(match);
        }
    }
}
