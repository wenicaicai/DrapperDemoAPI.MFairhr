

--13.
--上过张三老师的课程的学生信息
SELECT J.StudentName,Q.CourseScore,K.CourseName,M.TeacherName FROM Student J
INNER JOIN Score Q ON J.Id=Q.StudentId
INNER JOIN Course K ON Q.CourseId=K.Id
INNER JOIN Teacher M ON K.TeacherId=M.Id;

--NOT IN 上面的结果集合
SELECT * FROM Student WHERE Id NOT IN (
SELECT J.Id FROM Student J
INNER JOIN Score Q ON J.Id=Q.StudentId
INNER JOIN Course K ON Q.CourseId=K.Id
INNER JOIN Teacher M ON K.TeacherId=M.Id
WHERE M.TeacherName='张三')


--15 两门+科目 不及格的学生信息
--先查询符合条件的学生ID,再关联相应的学生信息表
SELECT * FROM Student J
WHERE EXISTS(
SELECT NULL FROM Score Q
WHERE CourseScore<60 AND J.Id=Q.StudentId
GROUP BY StudentId
HAVING(COUNT(DISTINCT CourseId))>=2
)

--18.查询各科成绩最高分、最低分和平均分：以如下形式显示：课程ID，课程name，最高分，最低分，平均分，及格率，中等率，优良率，优秀率

--及格为>=60，中等为：70-80，优良为：80-90，优秀为：>=90 (超级重点)

--及格率，中等率，优良率,优秀率

--MSSQL默认取整数，故取1.0变为小数

	SELECT CourseId,
	CAST(CAST(100*(passcount/totalcount) AS decimal(18,1)) AS VARCHAR(5))+'%' AS passcountRate,
	CAST((middlecount/totalcount) AS decimal(18,3)) AS middlecountRate,
	CAST((seniorcount/totalcount) AS decimal(18,3))  AS seniorcountRate,
	CAST((excellentcount/totalcount) AS decimal(18,3)) AS excellentcountRate,
	excellentcount_otherway,
	seniorcount_otherway,
	middlecount_otherway,
	passcount_otherway
	 FROM (
	SELECT
	CourseId,COUNT(*) AS totalcount,
	SUM(CASE WHEN CourseScore>=60 THEN 1.0 ELSE 0 END) AS passcount,
	SUM(CASE WHEN CourseScore>=60 AND CourseScore<=79 THEN 1.0 ELSE 0 END) AS middlecount,
	SUM(CASE WHEN CourseScore>=80 AND CourseScore<=89 THEN 1.0 ELSE 0 END) AS seniorcount,
	SUM(CASE WHEN CourseScore>=90 THEN 1.0 ELSE 0 END) AS excellentcount,
	CAST(AVG(CASE WHEN CourseScore>=90 THEN 1.0 ELSE 0 END) AS decimal(5,3)) AS excellentcount_otherway,
	CAST(AVG(CASE WHEN CourseScore>=80 AND CourseScore<=89 THEN 1.0 ELSE 0 END) AS decimal(5,3)) AS seniorcount_otherway,
	CAST(AVG(CASE WHEN CourseScore>=60 AND CourseScore<=79 THEN 1.0 ELSE 0 END)AS decimal(5,3)) AS middlecount_otherway,
	CAST(AVG(CASE WHEN CourseScore>=60 THEN 1.0 ELSE 0 END) AS decimal(5,3)) AS passcount_otherway
	FROM Score GROUP BY CourseId) AS COUNTDATA
