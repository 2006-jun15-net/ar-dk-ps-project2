﻿using ClassRegistration.Domain.Model;
using System.Threading.Tasks;

namespace ClassRegistration.Domain
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Search a student by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentModel> FindById (int id);
    }
}
