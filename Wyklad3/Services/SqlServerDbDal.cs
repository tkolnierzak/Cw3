﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wyklad4.Services
{
    public class SqlServerDbDal : IStudentsDal
    {
        public IEnumerable<Student> GetStudents()
        {
            //...sql con
            return null;
        }
    }
}
