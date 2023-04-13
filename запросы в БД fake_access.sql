Create table  Clients
(
	id int not null IDENTITY (1, 1),
	name nvarchar(50) not null,
	lastname nvarchar(50) not null,
	middlename nvarchar(50) not null,
	phonenumber nvarchar(20),
	Email nvarchar(255) not null,
	CONSTRAINT [PK_Clients] PRIMARY KEY ([id])
)

insert into Clients ( [name], [lastname], [middlename], [Email]) values ( N'Дмитрий', N'Пепеляев', N'Владимирович', 'yosoj11391@yandex.ru')
insert into Clients ( [name], [lastname], [middlename], [Email]) values (N'Сергей', N'Шарапов', N'Павлович', 'wagreinnugullau-8750@gmail.com')
insert into Clients ( [name], [lastname], [middlename], [Email]) values (N'Олег', N'Орехов', N'Борисович', 'freizaxoutocra-3218@gmail.com')
insert into Clients ( [name], [lastname], [middlename], [Email]) values (N'Григорий', N'Беляев', N'Евгеньевич', 'roinnuniddeiza-5965@gmail.com')
insert into Clients ( [name], [lastname], [middlename], [Email]) values (N'Владистас', N'Синотов', N'Павлович', 'silvername@hs.com')

select * from Clients

drop table Clients