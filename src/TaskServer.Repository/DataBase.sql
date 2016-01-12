CREATE DATABASE [task_server_db]
Go
use [task_server_db]
Go

create table UserAccounts
(
    [UserId] [uniqueidentifier] NOT NULL,
	[UserName][nvarchar](50) NULL,
	[UserLoginName][nvarchar](50) NULL,
	[UserRole][int] NOT NULL,
	[UserPasswordHash][uniqueidentifier] NOT NULL,
	primary key([UserId])
)
go
create table TaskStatuses
(
   [Id] int not null,
   [Name] nvarchar(50) not null,
   primary key ([Id])
)
go

create table TaskPriorities
(
   [Id] int not null,
   [Name] nvarchar(50) not null,
   primary key ([Id])
)
go
create table Tasks
(
       [TaskId] [uniqueidentifier] NOT NULL,
	   [ParentTaskId] [uniqueidentifier]  NULL,
	   [TaskName][nvarchar](100) NULL,
	   [TaskTarget][nvarchar](100) NULL,
	   [TaskResult][nvarchar](100) NULL,
	
	   [AuthorId] [uniqueidentifier]  NOT NULL,
	   [ManagerId] [uniqueidentifier]  NOT NULL,
	   [EmployeeId] [uniqueidentifier]  NOT NULL,
	   [TaskCreatedDateTime] [datetime]  NOT NULL,
	   [TaskStartDateTime] [datetime]  NOT NULL,
	   [TaskEndDateTime] [datetime]  NOT NULL,
	 
	   [TaskStatusId] [int]  NOT NULL,
	   [TaskPriorityId] [int]  NOT NULL,

	   [TaskDescription][nvarchar](100) NULL,
	   primary key([TaskId])

)
go 
insert into TaskPriorities(id,name) values(1,N'Низкий');
insert into TaskPriorities(id,name) values(2,N'Средний');
insert into TaskPriorities(id,name) values(3,N'Высокий');
go 
insert into TaskStatuses(id,name) values(1,N'Новая задача');
insert into TaskStatuses(id,name) values(2,N'В работе');
insert into TaskStatuses(id,name) values(3,N'Выполнено');
insert into TaskStatuses(id,name) values(4,N'Принято');
insert into TaskStatuses(id,name) values(5,N'На доработку');
insert into TaskStatuses(id,name) values(6,N'Отменена');
insert into TaskStatuses(id,name) values(7,N'Завершена');

go
insert into [UserAccounts](UserId,UserRole,UserName, UserLoginName,UserPasswordHash) 
  values('8C4F7B49-00C9-4616-9BBF-560F464EA783',200,'Power User','Power User','8D721EC8-4C9D-632F-6F06-7F89CC14862C')

insert into [UserAccounts](UserId,UserRole,UserName, UserLoginName,UserPasswordHash) 
  values('45B63E3B-E6F8-4394-B940-8DC9B17378CC',300,'Admin','Admin','3842CAC4-B9A0-8223-0DCC-509A6F75849B')

insert into [UserAccounts](UserId,UserRole,UserName, UserLoginName,UserPasswordHash) 
  values('8839BCDF-E955-43A9-9F45-D9EAC9694392',100,'User','User','7EC8CBEC-5C4B-FEE2-2830-8FD9F2A7BAF3')

