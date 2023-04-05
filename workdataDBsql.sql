--таблица для пользователей программы
Create table users 
(
id int not null,
user_nickname nvarchar(40) not null,
user_password nvarchar(40) not null,
user_fullname nvarchar(40) 
)

insert into users ( [id], [user_nickname], [user_password],  [user_fullname]) values (1, 'admin', 'somepassword', N'Дмитрий')
insert into users ( [id], [user_nickname], [user_password],  [user_fullname]) values (2, 's', 's', 'test')

select * from users

--таблица покупок
Create table Sales
(
	id int not null,
	Email nvarchar(255) not null,
	code_product int not null,
	description_product nvarchar(255) 
)

insert into Sales ([id], [Email], [code_product], [description_product]) values (1, 'yosoj11391@yandex.ru', '34', N'Телефон Xiaomi Redmi Note 10 PRO')
insert into Sales ([id], [Email], [code_product], [description_product]) values (3, 'yosoj11391@yandex.ru', '57', N'Телефон Xiaomi Redmi Note 9 SE')
insert into Sales ([id], [Email], [code_product], [description_product]) values (2, 'roinnuniddeiza-5965@Gmail.com', '25', N'Телевизор Samsung')

select * from Sales