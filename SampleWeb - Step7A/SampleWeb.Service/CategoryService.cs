﻿using System;
using System.Collections.Generic;
using System.Linq;
using SampleWeb.Models;
using SampleWeb.Models.Interface;
using SampleWeb.Models.UnitOfWork;
using SampleWeb.Service.Interface;

namespace SampleWeb.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Category> _repository;

        public CategoryService(IUnitOfWork unitofwork, IRepository<Category> repository)
        {
            this._unitOfWork = unitofwork;
            this._repository = repository;
        }

        public IResult Create(Category instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this._repository.Create(instance);
                this._unitOfWork.SaveChange();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Update(Category instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this._repository.Update(instance);
                this._unitOfWork.SaveChange();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int categoryID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(categoryID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(categoryID);
                this._repository.Delete(instance);
                this._unitOfWork.SaveChange();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int categoryID)
        {
            return this._repository.GetAll().Any(x => x.CategoryID == categoryID);
        }

        public Category GetByID(int categoryID)
        {
            return this._repository.Get(x => x.CategoryID == categoryID);
        }

        public IEnumerable<Category> GetAll()
        {
            return this._repository.GetAll();
        }
    }
}