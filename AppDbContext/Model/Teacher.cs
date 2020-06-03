using System.ComponentModel.DataAnnotations;

namespace AppDbContext.Model
{
    public class Teacher: BaseEntity<int>
    {
        [MaxLength(20)]
        public string TeacherName { get; set; }
    }

    /*
     * 

insert into Teacher(TeacherName) values('张三');
insert into Teacher(TeacherName) values('李四');
insert into Teacher(TeacherName) values('王五');
     
     */
}
