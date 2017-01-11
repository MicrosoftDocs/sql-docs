---
title: "CREATE SCHEMA (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9288c654-4057-4303-bca8-a5c48609145b
caps.latest.revision: 13
author: BarbKess
---
# CREATE SCHEMA (SQL Server PDW)
Creates a schema in the current database. The CREATE SCHEMA transaction can also create tables and views within the new schema, and set GRANT, DENY, or REVOKE permissions on those objects.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE SCHEMA schema_name [ AUTHORIZATION owner_name ] [;]  
```  
  
## Arguments  
*schema_name*  
Is the name by which the schema is identified within the database.  
  
AUTHORIZATION *owner_name*  
Specifies the name of the database-level principal that will own the schema. This principal may own other schemas, and may not use the current schema as its default schema.  
  
## Remarks  
The database principal (user) that executes CREATE SCHEMA can specify another database principal as the owner of the schema being created. This requires additional permissions, as described in the "Permissions" section later in this topic.  
  
The new schema is owned by one of the following database-level principals: database user, database role, or application role. Ownership of schema-contained objects can be transferred to any database-level principal, but the schema owner always retains CONTROL permission on objects within the schema.  
  
Specifying the schema in Transact\-SQL statements can slightly improve performance by removing some of the steps required to identify objects. Specifying the schema can also avoid errors from assuming the users default schema, and can make your code easier to understand and maintain.  
  
> [!TIP]  
> SQL Server PDW and SQL Server differ slightly in how a is resolved when the schema is omitted in a 3-part name referenced in a stored procedure. If schema is not provided, SQL Server will first try with the schema name which is the same as the name of the stored procedures schema (even if the reference is pointing to another database). If no such schema exists it will resolve to **dbo**. SQL Server PDW will first try with the schema name which is the same as the name of default schema for the user in the referenced database. This is the same result we would get in SQL Server if we executed statement as part of regular batch and not in stored procedure.  
  
**Implicit Schema and User Creation**  
  
In some cases a user can use a database without having a database user account (a database principal in the database). This can happen in the following situations:  
  
-   A login has **CONTROL SERVER** privileges.  
  
-   A Windows user does not have an individual database user account (a database principal in the database), but accesses a database as a member of a Windows group which has a database user account (a database principal for the Windows group).  
  
When a user without a database user account creates an object without specifying an existing schema, a database principal and default schema will be automatically created in the database for that user. The created database principal and schema will have the same name as the name that user used when connecting to SQL Server (the SQL Server authentication login name or the Windows user name).  
  
This behavior is necessary to allow users that are based on Windows groups to create and own objects. However it can result in the unintentional creation of schemas and users. To avoid implicitly creating users and schemas, whenever possible explicitly create database principals and assign a default schema. Or explicitly state an existing schema when creating objects in a database, using two or three-part object names.  
  
## Permissions  
Requires CREATE SCHEMA permission on the database.  
  
To specify another user as the owner of the schema being created, the caller must have IMPERSONATE permission on that user. If a database role is specified as the owner, the caller must have one of the following: membership in the role or ALTER permission on the role.  
  
## Locking  
Takes an exclusive lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION objects.  
  
## Examples  
  
### Example A  
The following example creates schema `Sales` and then creates a table `Sales.Region` in that schema.  
  
```  
CREATE SCHEMA Sales;  
GO;  
  
CREATE TABLE Sales.Region   
(Region_id int NOT NULL,  
Region_Name char(5) NOT NULL)  
WITH (DISTRIBUTION = REPLICATE);  
GO  
```  
  
### Example B  
The following example creates a schema `Production` owned by `Mary`.  
  
```  
CREATE SCHEMA Production AUTHORIZATION [Contoso\Mary];  
GO  
```  
  
## See Also  
[ALTER SCHEMA &#40;SQL Server PDW&#41;](../sqlpdw/alter-schema-sql-server-pdw.md)  
[DROP SCHEMA &#40;SQL Server PDW&#41;](../sqlpdw/drop-schema-sql-server-pdw.md)  
[sys.schemas &#40;SQL Server PDW&#41;](../sqlpdw/sys-schemas-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
