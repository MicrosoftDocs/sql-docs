---
title: SQL Graph Database Sample | Microsoft Docs
description: A quick sample that will help you get started with the new syntax introduced in SQL graph database. 
ms.date: "04/19/2017"
ms.prod: "sql-vnext"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "SQL graph"
  - "SQL graph, tsql reference"
ms.assetid: 
caps.latest.revision: 1
author: "shkale-msft"
ms.author: "shkale"
manager: "jhubbard"
---
# Create a graph database and run some pattern matching queries using T-SQL
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]   


This sample provides a [!INCLUDE[tsql-md](../../includes/tsql-md.md)] script to create a graph database with nodes and edges and then use the new MATCH clause to match some patterns and traverse through the graph.  
 
## Sample Schema  
This sample creates a graph schema, as showed in Figure 1, for a hypothetical social network that has People, Restaurant and City nodes. These nodes are connected to each other using Friends, Likes, LivesIn and LocatedIn edges. 

![person-cities-restaurants-tables](../../relational-databases/graphs/media/person-cities-restaurants-tables.png "Sql graph database sample")  
Figure 1: Sample schema with restaurant, city, person nodes and LivesIn, LocatedIn, Likes edges.


## Sample Script
```
-- Create a graph demo database
CREATE DATABASE graphdemo
go

USE  graphdemo
go

-- Drop tables if they exist already
drop table if exists likes;
drop table if exists Person;
drop table if exists Restaurant;
drop table if exists City;
drop table if exists friendOf;
drop table if exists livesIn;
drop table if exists locatedIn;

-- Create NODE tables
CREATE TABLE Person (ID INTEGER PRIMARY KEY, name VARCHAR(100)) AS NODE;
CREATE TABLE Restaurant (ID INTEGER NOT NULL, name VARCHAR(100), city VARCHAR(100)) AS NODE;
CREATE TABLE City (ID INTEGER PRIMARY KEY, name VARCHAR(100), stateName VARCHAR(100)) AS NODE;

-- Create EDGE tables. 
CREATE TABLE likes (rating INTEGER) AS EDGE;
CREATE TABLE friendOf AS EDGE;
CREATE TABLE livesIn AS EDGE;
CREATE TABLE locatedIn AS EDGE;

-- Insert data into node tables. Inserting into a node table is same as inserting into a regular table
insert into Person values(1,'John'),(2,'Mary'),(3,'Alice'),(4,'Jacob'),(5,'Julie');
insert into Restaurant values(1,'Taco Dell','Bellevue'),(2,'Ginger and Spice','Seattle'),(3,'Noodle Land', 'Redmond');
insert into City values(1,'Bellevue','wa'),(2,'Seattle','wa'),(3,'Redmond','wa');

-- Insert into edge table. While inserting into an edge table, you need to provide the $node_id from $from_id and $to_id columns.
insert into likes values ((select $node_id from Person where id = 1), (select $node_id from Restaurant where id = 1),9);
insert into likes values ((select $node_id from Person where id = 2), (select $node_id from Restaurant where id = 2),9);
insert into likes values ((select $node_id from Person where id = 3), (select $node_id from Restaurant where id = 3),9);
insert into likes values ((select $node_id from Person where id = 4), (select $node_id from Restaurant where id = 3),9);
insert into likes values ((select $node_id from Person where id = 5), (select $node_id from Restaurant where id = 3),9);

insert into livesIn values ((select $node_id from Person where id = 1),(select $node_id from City where id = 1));
insert into livesIn values ((select $node_id from Person where id = 2),(select $node_id from City where id = 2));
insert into livesIn values ((select $node_id from Person where id = 3),(select $node_id from City where id = 3));
insert into livesIn values ((select $node_id from Person where id = 4),(select $node_id from City where id = 3));
insert into livesIn values ((select $node_id from Person where id = 5),(select $node_id from City where id = 1));

insert into locatedIn values ((select $node_id from Restaurant where id = 1),(select $node_id from City where id =1));
insert into locatedIn values ((select $node_id from Restaurant where id = 2),(select $node_id from City where id =2));
insert into locatedIn values ((select $node_id from Restaurant where id = 3),(select $node_id from City where id =3));

-- Find Restaurants that John likes
SELECT Restaurant.name
FROM Person, likes, Restaurant
WHERE MATCH (Person-(likes)->Restaurant)
AND Person.name = 'John';

-- Find Restaurants that John's friends like
SELECT Restaurant.name 
FROM Person person1, Person person2, likes, friendOf, Restaurant
WHERE MATCH(person1-(friendOf)->person2-(likes)->Restaurant)
AND person1.name='John';

-- Find people who like a restaurant in the same city they live in
SELECT Person.name
FROM Person, likes, Restaurant, livesIn, City, locatedIn
WHERE MATCH (Person-(likes)->Restaurant-(locatedIn)->City AND Person-(livesIn)->City);

```

## Clean Up  
Clean up the schema and database created for the sample.
```
use graphdemo
go

drop table if exists likes;
drop table if exists Person;
drop table if exists Restaurant;
drop table if exists City;
drop table if exists friendOf;
drop table if exists livesIn;
drop table if exists locatedIn;

use master
go
drop database graphdemo
go


```

## Script explanation  
This script uses the new T-SQL syntax to create node and edge tables. Shows how to insert data into node and edge tables using `INSERT` statement and also shows how to use `MATCH` clause for pattern matching and navigation.

|Command	|Notes
|---  |---  |
|[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-sql-graph.md)  |Create graph node or edge table  |
|[INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-sql-graph.md)  |Insert into a node or edge table  |
|[MATCH &#40;Transact-SQL&#41;](../../t-sql/statements/match-sql-graph.md)  |Use MATCH to match a pattern or traverse through the graph  |