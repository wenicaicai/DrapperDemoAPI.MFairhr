

	-- 文本往右移动 Tab
	-- 文本往左移动 Shift+Tab

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
	FROM Score GROUP BY CourseId) AS COUNTDATA;

	--查找所有课程的成绩排名2~3名的学生信息以及课程成绩

	SELECT J.StudentName,scoreGroupOrder.* FROM (
	SELECT StudentId,CourseScore,
	ROW_NUMBER() OVER(PARTITION BY CourseId ORDER BY CourseScore DESC) AS n
	FROM Score) scoreGroupOrder INNER JOIN Student J ON (scoreGroupOrder.StudentId=J.Id)
	WHERE scoreGroupOrder.n BETWEEN 2 AND 3

	--计算学生平均分以及名次

	SELECT StudentId,AVG(CourseScore) avgscore,
	ROW_NUMBER() OVER(ORDER BY AVG(CourseScore) DESC) AS n
	FROM Score GROUP BY StudentId;
	
	--查询各科成绩前三名

	SELECT scoresort.CourseId,scoresort.CourseScore,
	(CASE WHEN scoresort.n=1 THEN scoresort.CourseScore ELSE NULL END) AS '第1',
	(CASE WHEN scoresort.n=2 THEN scoresort.CourseScore ELSE NULL END) AS '第2',
	(CASE WHEN scoresort.n=3 THEN scoresort.CourseScore ELSE NULL END) AS '第3'
	FROM (
	SELECT CourseId,CourseScore,
	ROW_NUMBER() OVER(PARTITION BY CourseId ORDER BY CourseScore DESC) n FROM Score) AS scoresort
	WHERE scoresort.n <=3



	--查询各科成绩前三名

	SELECT scoresort.CourseId,
	MAX(CASE WHEN scoresort.n=1 THEN scoresort.CourseScore ELSE NULL END) AS '第1',
	MAX(CASE WHEN scoresort.n=2 THEN scoresort.CourseScore ELSE NULL END) AS '第2',
	MAX(CASE WHEN scoresort.n=3 THEN scoresort.CourseScore ELSE NULL END) AS '第3'
	FROM (
	SELECT CourseId,CourseScore,
	ROW_NUMBER() OVER(PARTITION BY CourseId ORDER BY CourseScore DESC) n FROM Score
	) AS scoresort
	WHERE scoresort.n <=3
	GROUP BY scoresort.CourseId

		
	-- 查询只有两门课程的学生的学号、姓名

	SELECT * FROM Student J INNER JOIN (
	SELECT StudentId FROM Score
	GROUP BY StudentId
	HAVING COUNT(*)=2) AS targetStudent ON J.Id=targetStudent.StudentId

	--32.
	-- 查询平均分大于85分的学生

	SELECT * FROM Student J
	INNER JOIN(
	SELECT StudentId,AVG(CourseScore) AS avgscore FROM Score
	GROUP BY StudentId
	HAVING(AVG(CourseScore)>85)) Q ON J.Id=Q.StudentId

	--34.查询课程名称“数学”，且分数低于60分的学生姓名、学科信息

	SELECT J.*,
		   Q.CourseScore
	FROM   Student J
		   INNER JOIN (SELECT StudentId,
							  CourseScore
					   FROM   Score M
					   WHERE  CourseScore < 60
							  AND EXISTS(SELECT NULL
										 FROM   Course N
										 WHERE  M.CourseId = N.Id
												AND N.CourseName = '数学')) Q
				   ON J.Id = Q.StudentId 
				   
	--35.查询所有学生的课程情况、学科成绩情况

	SELECT J.StudentId,
	MAX(CASE WHEN Q.CourseName='语文' THEN CourseScore ELSE NULL END) AS '语文',
	MAX(CASE WHEN Q.CourseName='数学' THEN CourseScore ELSE NULL END) AS '数学', 
	MAX(CASE WHEN Q.CourseName='英语' THEN CourseScore ELSE NULL END) AS '英语'
	FROM Score J INNER JOIN Course Q ON J.CourseId=Q.Id
	GROUP BY J.StudentId;

	
	--38.课程编号:3 且学科成绩：80+的学生信息
	SELECT Q.StudentName,J.CourseScore FROM Score J
	INNER JOIN Student Q ON(J.StudentId=Q.Id)
	WHERE CourseId=3 AND CourseScore>80;

	--47.查询没学过“张三”老师讲授的任一门课程的学生姓名
		--做法：查询所有学过张三老师课程的学生

	SELECT * FROM Student M WHERE Id NOT IN (
	SELECT J.StudentId FROM Score J
	WHERE EXISTS(
		SELECT Q.CourseName,Q.TeacherId,Q.Id FROM Course Q
		WHERE EXISTS(
			SELECT NULL FROM Teacher T
			WHERE Q.TeacherId=T.Id AND T.TeacherName='张三'
		) AND J.CourseId = Q.Id
	));

	--48.查询两门以上不及格学科的学生信息、得分
	SELECT J.StudentId,AVG(CourseScore)totalavg FROM Score J
	WHERE J.CourseScore<60
	GROUP BY StudentId
	HAVING(COUNT(DISTINCT CourseId)>=2);

	----------------------------------------------------------------------------------------------------------------
	INSERT INTO Employee(BirthDate,FirstName,LastName,Gender,HireDate) VALUES('1953-09-02','Georgi','Facello',1,'1986-06-26');
	INSERT INTO Employee(BirthDate,FirstName,LastName,Gender,HireDate) VALUES('1964-06-02','Bezalel','Simmel',2,'1985-11-21');
	INSERT INTO Employee(BirthDate,FirstName,LastName,Gender,HireDate) VALUES('1955-01-21','Kyoichi','Maliniak',1,'1989-09-12');
	INSERT INTO Employee(BirthDate,FirstName,LastName,Gender,HireDate) VALUES('1953-04-20','Anneke','Preusig',2,'1989-06-02');

	--对于employees表中，输出first_name排名(按first_name升序排序)为奇数的first_name



			


