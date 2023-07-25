---
title: SQL Graph Database Sample
description: "In this tutorial, create a graph database with nodes and edges and then use the new MATCH clause to match some patterns and traverse through the graph."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 06/28/2023
ms.service: sql
ms.topic: "language-reference"
helpviewer_keywords:
  - "SQL graph"
  - "SQL graph, tsql reference"
monikerRange: "=azuresqldb-current||>=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Create a graph database and run some pattern matching queries using T-SQL

[!INCLUDE[sqlserver2017-asdb](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

This sample provides a [!INCLUDE[tsql-md](../../includes/tsql-md.md)] script to create a graph database with nodes and edges and then use the new MATCH clause to match some patterns and traverse through the graph. This sample script works on both Azure SQL Database and [!INCLUDE[sssql17](../../includes/sssql17-md.md)] and later versions.

## Sample Schema

This sample creates a graph schema for a hypothetical social network that has `People`, `Restaurant` and `City` nodes. These nodes are connected to each other using `Friends`, `Likes`, `LivesIn` and `LocatedIn` edges. The following diagram shows a sample schema with `restaurant`, `city`, `person` nodes and `LivesIn`, `LocatedIn`, `Likes` edges.

:::image type="content" source="media/sql-graph-sample/person-cities-restaurants-tables.png" alt-text="Diagram showing a sample schema with restaurant, city, person nodes and LivesIn, LocatedIn, Likes edges.":::

## Sample Script

The following sample script uses the new T-SQL syntax to create node and edge tables. Learn how to insert data into node and edge tables using `INSERT` statement and also shows how to use `MATCH` clause for pattern matching and navigation.

This script performs the following steps:

1. Create a database named `GraphDemo`.
1. Create node tables.
1. Create edge tables.

```sql
-- Create a GraphDemo database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE NAME = 'graphdemo')
    CREATE DATABASE GraphDemo;
GO

USE GraphDemo;
GO

-- Create NODE tables
CREATE TABLE Person (
  ID INTEGER PRIMARY KEY,
  name VARCHAR(100)
) AS NODE;

CREATE TABLE Restaurant (
  ID INTEGER NOT NULL,
  name VARCHAR(100),
  city VARCHAR(100)
) AS NODE;

CREATE TABLE City (
  ID INTEGER PRIMARY KEY,
  name VARCHAR(100),
  stateName VARCHAR(100)
) AS NODE;

-- Create EDGE tables.
CREATE TABLE likes (rating INTEGER) AS EDGE;
CREATE TABLE friendOf AS EDGE;
CREATE TABLE livesIn AS EDGE;
CREATE TABLE locatedIn AS EDGE;
```

Now, we'll insert data to represent the relationships.

1. Insert data into node tables.
    1. Inserting into a node table is same as inserting into a regular table.
1. Insert data into edge tables, in this case, for which restaurants each person likes into the `likes` edge. 
    1. While inserting into an edge table, provide the `$node_id` from `$from_id` and `$to_id` columns.
1. Insert data into the `livesIn` edge to associate persons with the city where they live.
1. Insert data into the `locatedIn` edge to associate restaurants with the city where they are located.
1. Insert data into the `friendOf` edge to associated friends.

```sql
-- Insert data into node tables. Inserting into a node table is same as inserting into a regular table
INSERT INTO Person (ID, name)
    VALUES (1, 'John')
         , (2, 'Mary')
         , (3, 'Alice')
         , (4, 'Jacob')
         , (5, 'Julie');

INSERT INTO Restaurant (ID, name, city)
    VALUES (1, 'Taco Dell','Bellevue')
         , (2, 'Ginger and Spice','Seattle')
         , (3, 'Noodle Land', 'Redmond');

INSERT INTO City (ID, name, stateName)
    VALUES (1,'Bellevue','WA')
         , (2,'Seattle','WA')
         , (3,'Redmond','WA');

-- Insert into edge table. While inserting into an edge table,
-- you need to provide the $node_id from $from_id and $to_id columns.
/* Insert which restaurants each person likes */
INSERT INTO likes
    VALUES ((SELECT $node_id FROM Person WHERE ID = 1), (SELECT $node_id FROM Restaurant WHERE ID = 1), 9)
         , ((SELECT $node_id FROM Person WHERE ID = 2), (SELECT $node_id FROM Restaurant WHERE ID = 2), 9)
         , ((SELECT $node_id FROM Person WHERE ID = 3), (SELECT $node_id FROM Restaurant WHERE ID = 3), 9)
         , ((SELECT $node_id FROM Person WHERE ID = 4), (SELECT $node_id FROM Restaurant WHERE ID = 3), 9)
         , ((SELECT $node_id FROM Person WHERE ID = 5), (SELECT $node_id FROM Restaurant WHERE ID = 3), 9);

/* Associate in which city live each person*/
INSERT INTO livesIn
    VALUES ((SELECT $node_id FROM Person WHERE ID = 1), (SELECT $node_id FROM City WHERE ID = 1))
         , ((SELECT $node_id FROM Person WHERE ID = 2), (SELECT $node_id FROM City WHERE ID = 2))
         , ((SELECT $node_id FROM Person WHERE ID = 3), (SELECT $node_id FROM City WHERE ID = 3))
         , ((SELECT $node_id FROM Person WHERE ID = 4), (SELECT $node_id FROM City WHERE ID = 3))
         , ((SELECT $node_id FROM Person WHERE ID = 5), (SELECT $node_id FROM City WHERE ID = 1));

/* Insert data where the restaurants are located */
INSERT INTO locatedIn
    VALUES ((SELECT $node_id FROM Restaurant WHERE ID = 1), (SELECT $node_id FROM City WHERE ID =1))
         , ((SELECT $node_id FROM Restaurant WHERE ID = 2), (SELECT $node_id FROM City WHERE ID =2))
         , ((SELECT $node_id FROM Restaurant WHERE ID = 3), (SELECT $node_id FROM City WHERE ID =3));

/* Insert data into the friendOf edge */
INSERT INTO friendOf
    VALUES ((SELECT $NODE_ID FROM Person WHERE ID = 1), (SELECT $NODE_ID FROM Person WHERE ID = 2))
         , ((SELECT $NODE_ID FROM Person WHERE ID = 2), (SELECT $NODE_ID FROM Person WHERE ID = 3))
         , ((SELECT $NODE_ID FROM Person WHERE ID = 3), (SELECT $NODE_ID FROM Person WHERE ID = 1))
         , ((SELECT $NODE_ID FROM Person WHERE ID = 4), (SELECT $NODE_ID FROM Person WHERE ID = 2))
         , ((SELECT $NODE_ID FROM Person WHERE ID = 5), (SELECT $NODE_ID FROM Person WHERE ID = 4));
```

Next, we'll query the data to find insights from the data.

1. Use the graph [MATCH](../../t-sql/queries/match-sql-graph.md) function to find which restaurants that John likes.
1. Finds the restaurants that John's friends like.
1. Find people who like a restaurant in the same city they live in.

```sql
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

Finally, a more advanced query finds the friends of friends of friends. This query excludes those cases where the relationship "loops back". For example, Alice is a friend of John; John is a friend of Mary; and Mary in turn is a friend of Alice. This causes a "loop" back to Alice. In many cases, it is necessary to explicitly check for such loops and exclude the results.

```sql
-- Find friends-of-friends-of-friends, excluding those cases where the relationship "loops back".
-- For example, Alice is a friend of John; John is a friend of Mary; and Mary in turn is a friend of Alice.
-- This causes a "loop" back to Alice. In many cases, it is necessary to explicitly check for such loops and exclude the results.
SELECT CONCAT(Person.name, '->', Person2.name, '->', Person3.name, '->', Person4.name)
FROM Person, friendOf, Person as Person2, friendOf as friendOffriend, Person as Person3, friendOf as friendOffriendOfFriend, Person as Person4
WHERE MATCH (Person-(friendOf)->Person2-(friendOffriend)->Person3-(friendOffriendOfFriend)->Person4)
AND Person2.name != Person.name
AND Person3.name != Person2.name
AND Person4.name != Person3.name
AND Person.name != Person4.name;
```

## Clean Up

Clean up the schema and database created for the sample in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
USE graphdemo;
go

DROP TABLE IF EXISTS likes;
DROP TABLE IF EXISTS Person;
DROP TABLE IF EXISTS Restaurant;
DROP TABLE IF EXISTS City;
DROP TABLE IF EXISTS friendOf;
DROP TABLE IF EXISTS livesIn;
DROP TABLE IF EXISTS locatedIn;

USE master;
go
DROP DATABASE graphdemo;
go
```

Clean up the schema and database created for the sample in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
--Connect to the graphdemo database
DROP TABLE IF EXISTS likes;
DROP TABLE IF EXISTS Person;
DROP TABLE IF EXISTS Restaurant;
DROP TABLE IF EXISTS City;
DROP TABLE IF EXISTS friendOf;
DROP TABLE IF EXISTS livesIn;
DROP TABLE IF EXISTS locatedIn;

--Connect to the master database
DROP DATABASE graphdemo;
go
```

## Next steps

- [Graph processing](../../relational-databases/graphs/sql-graph-overview.md)
- [CREATE TABLE (SQL Graph)](../../t-sql/statements/create-table-sql-graph.md)
- [INSERT (SQL Graph)](../../t-sql/statements/insert-sql-graph.md)
- [MATCH (Transact-SQL)](../../t-sql/queries/match-sql-graph.md)
