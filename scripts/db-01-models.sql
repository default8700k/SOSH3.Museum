--

create schema if not exists models;

--

create table if not exists models.requests (
    id bigserial primary key,
    ip inet,
    url varchar(128) not null,
    method varchar(16) not null,
    user_agent varchar(256) not null,
    date_time timestamp without time zone not null
);

--
