create table Users
(
    Id             int autoincrement,
    Name           varchar(100),
    HashedPassword varchar(100),
    Salt           varchar(50),
    constraint PK_Users
            primary key (Id)
);