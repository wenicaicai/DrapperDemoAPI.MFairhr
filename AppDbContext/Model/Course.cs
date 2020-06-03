using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppDbContext.Model
{
    public class Course : BaseEntity<int>
    {
        [MaxLength(20)]
        public string CourseName { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}

/**
 * 
insert into Course(CourseName,TeacherId) values( '语文' , 2);
insert into Course(CourseName,TeacherId) values( '数学' , 1);
insert into Course(CourseName,TeacherId) values('英语' , 3);
*/
