---
title: "Modified Features (Contained Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "contained database, modifications to DBs"
ms.assetid: a2942509-39a2-4903-b504-ae80a300a9de
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Modified Features (Contained Database)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The following features have been modified to be supported by a partially contained database. Features are usually modified so they do not cross the database boundary.  
  
 For more information, see [Contained Databases](../../relational-databases/databases/contained-databases.md).  
  
## ALTER DATABASE  
  
### Application Level  
 When using the ALTER DATABASE statement from inside of a contained database, the syntax differs from that used for a non-contained database. This difference includes restrictions of elements of the statement that extend beyond the database to the instance. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
### Instance Level  
 The syntax for the ALTER DATABASE when used outside of a contained database differs from that used for non-contained databases. These changes prevent crossing the database boundary. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
## CREATE DATABASE  
 The CREATE DATABASE syntax for a contained database differs from that for a non-contained database. See [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)for information about new syntax requirements and allowances.  
  
## Temporary Tables  
 Local temporary tables are permitted within a contained database, but their behavior differs from those in non-contained databases. In non-contained databases, temporary table data is collated in the collation of **tempdb**. In a contained database temporary table data is collated in the collation of the contained database.  
  
 All metadata associated with temporary tables (for example, table and column names, indexes, and so on) will be in the catalog collation.  
  
 Named constraints may not be used in temporary tables.  
  
 Temporary tables may not refer to user-defined types, XML schema collections, or user-defined functions.  
  
## Collation  
 In the non-contained database model, there are three separate types of collation: Database collation, Instance collation, and tempdb collation. Contained databases use only two collations, database collation and the new catalog collation. See [Contained Database Collations](../../relational-databases/databases/contained-database-collations.md) for more details on contained database collation.  
  
## User Options  
 When enabling contained databases, the [user options Option](../../database-engine/configure-windows/configure-the-user-options-server-configuration-option.md) must be set to 0 for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## See Also  
 [Contained Database Collations](../../relational-databases/databases/contained-database-collations.md)   
 [Contained Databases](../../relational-databases/databases/contained-databases.md)  
  
  
