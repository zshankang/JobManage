drop table if exists job_info;
create table job_info
(
	id int not null AUTO_INCREMENT comment '主键',
	job_name varchar(150) null default null comment '任务名称',
	job_assembly varchar(200) null default null comment '执行的方式的dll名称',
	job_class varchar(300) null default null comment '执行的方法类',
	job_corn varchar(100) null default null comment '执行任务的corn表达式',
	job_type int not null default 1 comment '任务类型，默认为1 简单，2 复杂',
	job_execount int null default 0 comment '任务的执行总次数,0表示无限次',
	job_starttime datetime null default null comment '任务开始时间',
	job_state int null default 0 comment '任务的状态 0 准备中，1 执行中,2 暂定，3 停止，4 结束',
	addtime TIMESTAMP null default CURRENT_TIMESTAMP comment '任务的创建时间',
	PRIMARY KEY (`id`)
);
drop table if exists job_log;
create table job_log
(
	id int not null AUTO_INCREMENT comment '主键',
	job_name varchar(150) null default null comment '任务名称',
	job_result varchar(200) null default null comment '执行的结果',
	job_exception text null default null comment '执行任务的异常信息',
	job_exetime int not null default 0 comment '执行耗时，单位ms',
	job_exedate datetime null default 0 comment '任务的执行的日期时间',
	job_exestate int null default 0 comment '执行结果 0 正常，1 异常',
	addtime TIMESTAMP null default CURRENT_TIMESTAMP comment '任务的执行时间',
	PRIMARY KEY (`id`)
);