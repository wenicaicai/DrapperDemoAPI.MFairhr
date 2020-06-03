using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppDbContext.Model
{
    public class Student : BaseEntity<int>
    {
        [MaxLength(20)]
        public string StudentName { get; set; }
    }
    /**
     * 
     * 
insert into Student(StudentName) values('赵雷');
insert into Student(StudentName) values('钱电');
insert into Student(StudentName) values('孙风');
insert into Student(StudentName) values('李云');
insert into Student(StudentName) values('周梅');
insert into Student(StudentName) values('吴兰');
insert into Student(StudentName) values('郑竹');
insert into Student(StudentName) values('王菊');
     */
}
