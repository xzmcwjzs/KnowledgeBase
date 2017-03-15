using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KnowledgeBase.Add.DAL;

namespace KnowledgeBase.Add.BLL
{
    public abstract class BaseService<T> where T : class ,new()
    {

        DbContext Db = DbContextFactory.CreateDbContext();
        public BaseDal<T> CurrentDal { get; set; }
        public BaseService()
        {
            SetCurrentDal();
        }
        public abstract void SetCurrentDal();
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {

            return CurrentDal.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadOrderEntities<S>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            return CurrentDal.LoadOrderEntities(whereLambda, orderByLambda, isAsc);

        }
        public IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, S>> orderbyLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<S>(pageSize, pageIndex, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            //return CurrentDbSession.SaveChanges();
            return Db.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateEntity(T entity)
        {
            CurrentDal.UpdateEntity(entity);
            //return CurrentDbSession.SaveChanges();
            return Db.SaveChanges() > 0;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            //CurrentDbSession.SaveChanges();
            return Db.SaveChanges() > 0;
        }
    }
}
