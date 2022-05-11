create table Users
(
    Id             INTEGER not null
        constraint PK_Users
            primary key autoincrement,
    Name           TEXT,
    HashedPassword TEXT,
    Salt           TEXT
);