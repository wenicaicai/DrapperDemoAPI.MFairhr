using System;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.Model
{
    public class Score : BaseEntity<int>
    {
        public int CourseScore { get; set; }
        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }

    /**
     * 
     * 
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(1 , 1 , 80);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(1 , 2 , 90);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(1 , 3 , 99);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(2 , 1 , 70);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(2 , 2 , 60);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(2 , 3 , 80);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(3 , 1 , 80);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(3 , 2 , 80);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(3 , 3 , 80);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(4 , 1 , 50);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(4 , 2 , 30);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(4 , 3 , 20);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(5 , 1 , 76);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(5 , 2 , 87);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(6 , 1 , 31);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(6 , 3 , 34);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(7 , 2 , 89);
insert into Score(dbo.Score.StudentId,dbo.Score.CourseId,dbo.Score.CourseScore) values(7 , 3 , 98);
     * ***/
}
