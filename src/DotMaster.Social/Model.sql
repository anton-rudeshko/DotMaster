drop table "Profile" cascade constraints;

drop table "ProfileXref" cascade constraints;

drop table "Address" cascade constraints;

drop table "AddressXref" cascade constraints;

drop table "Country" cascade constraints;

drop sequence hibernate_sequence;

create table "Profile" (
    ObjKey NUMBER(10,0) not null,
    LastUpdate TIMESTAMP(4) not null,
    MasterStatus NUMBER(10,0) not null,
    FullName NVARCHAR2(255) not null,
    Age NUMBER(10,0),
    Sex NUMBER(10,0),
    Occupation NVARCHAR2(255),
    primary key (ObjKey)
);

create table "ProfileXref" (
    ObjKey NUMBER(10,0) not null,
    LastUpdate TIMESTAMP(4) not null,
    Source NVARCHAR2(255) not null,
    SourceKey NVARCHAR2(255) not null,
    BaseObjKey NUMBER(10,0) not null,
    FullName NVARCHAR2(255) not null,
    Age NUMBER(10,0),
    Sex NUMBER(10,0),
    Occupation NVARCHAR2(255),
    primary key (ObjKey)
);

create table "Address" (
    ObjKey NUMBER(10,0) not null,
    LastUpdate TIMESTAMP(4) not null,
    MasterStatus NUMBER(10,0) not null,
    Line NVARCHAR2(255) not null,
    Country_id NUMBER(10,0),
    Profile_id NUMBER(10,0),
    primary key (ObjKey)
);

create table "AddressXref" (
    ObjKey NUMBER(10,0) not null,
    LastUpdate TIMESTAMP(4) not null,
    Source NVARCHAR2(255) not null,
    SourceKey NVARCHAR2(255) not null,
    BaseObjKey NUMBER(10,0) not null,
    Line NVARCHAR2(255) not null,
    Profile_id NUMBER(10,0),
    Country_id NUMBER(10,0),
    primary key (ObjKey)
);

create table "Country" (
    Id NUMBER(10,0) not null,
    Name NVARCHAR2(255),
    IsoCode NVARCHAR2(255),
    primary key (Id)
);

alter table "ProfileXref" 
    add constraint FKAE6447666E18B7E5 
    foreign key (BaseObjKey) 
    references "Profile";

alter table "Address" 
    add constraint FK8C1490CBEC980B3B 
    foreign key (Country_id) 
    references "Country";

alter table "Address" 
    add constraint FK8C1490CB22914803 
    foreign key (Profile_id) 
    references "Profile";

alter table "AddressXref" 
    add constraint FKC5A5FAEAC3DBA79E 
    foreign key (BaseObjKey) 
    references "Address";

create sequence hibernate_sequence;
