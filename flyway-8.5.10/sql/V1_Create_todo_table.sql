
create Table Todos
(
    Id int not null
        constraint PK_Todos
            primary key autoincrement,
    UserId      int not null,
    Title       varchar(100),
    Description varchar(500),
    Complete    int not null
);