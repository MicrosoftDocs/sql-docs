---
title: "Ownership and user-schema separation in SQL Server"
description: Learn how user-schema separation allows flexibility in managing SQL Server database object permissions. Schemas group objects into separate namespaces. 
ms.date: "08/09/2021"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
ms.reviewer: vanto
author: AndreasWolter
ms.author: anwolter
---
# Ownership and user-schema separation in SQL Server

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A core concept of SQL Server security is that owners of objects have irrevocable permissions to administer them. You can't remove privileges from an object owner, and you can't drop users from a database if they own objects in it.  
  
## User-schema separation  

 User-schema separation allows for more flexibility in managing database object permissions. A *schema* is a named container for database objects, which allows you to group objects into separate namespaces. For example, the AdventureWorks sample database contains schemas for Production, Sales, and HumanResources.  
  
 The four-part naming syntax for referring to objects specifies the schema name.  
  
```sql
Server.Database.DatabaseSchema.DatabaseObject  
```  
  
### Schema owners and permissions  

 Schemas can be owned by any database [principal](../authentication-access/principals-database-engine.md), and a single principal can own multiple schemas. You can apply security rules to a schema, which are inherited by all objects in the schema. Once you set up access permissions for a schema, those permissions are automatically applied as new objects are added to the schema. Users can be assigned a default schema, and multiple database users can share the same schema.  
  
 By default, when developers create objects in a schema, the objects are owned by the security principal that owns the schema, not the developer. Object ownership can be transferred with [ALTER AUTHORIZATION Transact-SQL](../../../t-sql/statements/alter-authorization-transact-sql.md) statement. A schema can also contain objects that are owned by different users and have more granular permissions than those assigned to the schema, although this isn't recommended because it adds complexity to managing permissions. Objects can be moved between schemas, and schema ownership can be transferred between principals. Database users can be dropped without affecting schemas.  
  
### Built-in schemas for backward compatibility 

 SQL Server ships with nine pre-defined schemas that have the same names as the built-in database users and roles: *db_accessadmin, db_backupoperator, db_datareader, db_datawriter, db_ddladmin, db_denydatareader, db_denydatawriter, db_owner, db_securityadmin*. These exist for backward compatibility. The recommendation is to not use them for user objects. You can drop the schemas that have the same names as the fixed database roles - unless they're already in use, in which case the drop-command will simply return an error and block the drop of the used schema.

```sql  
IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_accessadmin')
DROP SCHEMA [db_accessadmin]
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_backupoperator')
DROP SCHEMA [db_backupoperator]
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_datareader')
DROP SCHEMA [db_datareader]
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_datawriter')
DROP SCHEMA [db_datawriter]
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_ddladmin')
DROP SCHEMA [db_ddladmin]
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_denydatareader')
DROP SCHEMA [db_denydatareader]
GO

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_denydatawriter')
DROP SCHEMA [db_denydatawriter]
GO

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_owner')
DROP SCHEMA [db_owner]
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_securityadmin')
DROP SCHEMA [db_securityadmin]
GO
```

If you drop these schemas from the model database, they won't appear in new databases. Schemas that contain objects cannot be dropped.

The following schemas cannot be dropped:  
  
- `dbo`  
- `guest`  
- `sys`  
- `INFORMATION_SCHEMA`  
  
> [!NOTE]
> The `sys` and `INFORMATION_SCHEMA` schemas are reserved for system objects. You cannot create objects in these schemas and you cannot drop them.  
  
#### The dbo schema  

 The `dbo` schema is the default schema of every database. By default, users created with the CREATE USER Transact-SQL command have `dbo` as their default schema. The `dbo` schema is owned by the `dbo` user account. 
  
 Users who are assigned the `dbo` as default schema don't inherit the permissions of the `dbo` user account. No permissions are inherited from a schema by users; schema permissions are inherited by the database objects contained in the schema.  The default schema for a user is solely used for object-reference in case the user omits the schema when querying objects.
  
> [!NOTE]
> When database objects are referenced by using a one-part name, SQL Server first looks in the user's default schema. If the object is not found there, SQL Server looks next in the `dbo` schema. If the object is not in the `dbo` schema, an error is returned.  
  
## External resources  

 For more information on object ownership and schemas, see the following resources.  
  
|Resource|Description|  
|--------------|-----------------|  
|[User-Schema Separation](/previous-versions/sql/sql-server-2008-r2/ms190387(v=sql.105))|Describes the changes introduced by user-schema separation. Includes new behavior, its impact on ownership, catalog views, and permissions.|  
  
## See also

- [Protecting Your SQL Server Intellectual Property](../protecting-your-sql-server-intellectual-property.md)
- [Getting Started with Database Engine Permissions](../authentication-access/getting-started-with-database-engine-permissions.md)
- [Server-Level Roles](../authentication-access/server-level-roles.md)
- [Securing ADO.NET Applications](/dotnet/framework/data/adonet/securing-ado-net-applications)
