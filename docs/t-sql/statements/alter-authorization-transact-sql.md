---
title: "ALTER AUTHORIZATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/28/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_AUTHORIZATION_TSQL"
  - "ALTER AUTHORIZATION"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "owners [SQL Server], transferring"
  - "modifying entity ownership"
  - "full-text search [SQL Server], permissions"
  - "owners [SQL Server], entities"
  - "ALTER AUTHORIZATION statement"
  - "entity ownership [SQL Server]"
  - "transferring ownership"
  - "search property lists [SQL Server], permissions"
  - "TAKE OWNERSHIP"
ms.assetid: 8c805ae2-91ed-4133-96f6-9835c908f373
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER AUTHORIZATION (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Changes the ownership of a securable.    
    
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)    
    
## Syntax    
    
```    
-- Syntax for SQL Server  
ALTER AUTHORIZATION    
   ON [ <class_type>:: ] entity_name    
   TO { principal_name | SCHEMA OWNER }    
[;]    
    
<class_type> ::=    
    {    
        OBJECT | ASSEMBLY | ASYMMETRIC KEY | AVAILABILITY GROUP | CERTIFICATE     
      | CONTRACT | TYPE | DATABASE | ENDPOINT | FULLTEXT CATALOG     
      | FULLTEXT STOPLIST | MESSAGE TYPE | REMOTE SERVICE BINDING    
      | ROLE | ROUTE | SCHEMA | SEARCH PROPERTY LIST | SERVER ROLE     
      | SERVICE | SYMMETRIC KEY | XML SCHEMA COLLECTION    
    }    
```    

```
-- Syntax for SQL Database  
  
ALTER AUTHORIZATION    
   ON [ <class_type>:: ] entity_name    
   TO { principal_name | SCHEMA OWNER }    
[;]    
    
<class_type> ::=    
    {    
      OBJECT | ASSEMBLY | ASYMMETRIC KEY | CERTIFICATE     
    | TYPE | DATABASE | FULLTEXT CATALOG     
    | FULLTEXT STOPLIST     
    | ROLE | SCHEMA | SEARCH PROPERTY LIST     
    | SYMMETRIC KEY | XML SCHEMA COLLECTION    
    }    
```    

    
```    
-- Syntax for Azure SQL Data Warehouse  
  
ALTER AUTHORIZATION ON    
    [ <class_type> :: ] <entity_name>     
    TO { principal_name | SCHEMA OWNER }    
[;]    
    
<class_type> ::= {    
      SCHEMA     
    | OBJECT     
}    
    
<entity_name> ::=    
{    
      schema_name    
    | [ schema_name. ] object_name    
}    
```    
    
```    
-- Syntax for Parallel Data Warehouse  
  
ALTER AUTHORIZATION ON    
    [ <class_type> :: ] <entity_name>     
    TO { principal_name | SCHEMA OWNER }    
[;]    
    
<class_type> ::= {    
      DATABASE     
    | SCHEMA     
    | OBJECT     
}    
    
<entity_name> ::=    
{    
      database_name 
    | schema_name    
    | [ schema_name. ] object_name    
}    
```    
    
## Arguments    
\<class_type>
 Is the securable class of the entity for which the owner is being changed. OBJECT is the default.    
    
|||    
|-|-|    
|OBJECT|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], Azure SQL Data Warehouse, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].|    
|ASSEMBLY|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|ASYMMETRIC KEY|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|AVAILABILITY GROUP |**APPLIES TO**: SQL Server 2012 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|
|CERTIFICATE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|CONTRACT|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|    
|DATABASE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. For more information,see [ALTER AUTHORIZATION FOR databases](#AlterDB) section below.|    
|ENDPOINT|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|    
|FULLTEXT CATALOG|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|FULLTEXT STOPLIST|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|MESSAGE TYPE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|    
|REMOTE SERVICE BINDING|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|    
|ROLE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|ROUTE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|    
|SCHEMA|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], Azure SQL Data Warehouse, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].|    
|SEARCH PROPERTY LIST|**APPLIES TO**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|SERVER ROLE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|    
|SERVICE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|    
|SYMMETRIC KEY|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|TYPE|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
|XML SCHEMA COLLECTION|**APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|    
    
 *entity_name*    
 Is the name of the entity.    
    
 *principal_name* | SCHEMA OWNER    
 Name of the security principal that will own the entity. Database objects must be owned by a database principal; a database user or role. Server objects (such as databases) must be owned by a server principal (a login). Specify **SCHEMA OWNER** as the *principal_name* to indicate that the object should be owned by the principal that owns the schema of the object.    
    
## Remarks    
 ALTER AUTHORIZATION can be used to change the ownership of any entity that has an owner. Ownership of database-contained entities can be transferred to any database-level principal. Ownership of server-level entities can be transferred only to server-level principals.    
    
> [!IMPORTANT]    
>  Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], a user can own an OBJECT or TYPE that is contained by a schema owned by another database user. This is a change of behavior from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md) and [TYPEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/typeproperty-transact-sql.md).    
    
 Ownership of the following schema-contained entities of type "object" can be transferred: tables, views, functions, procedures, queues, and synonyms.    
    
 Ownership of the following entities cannot be transferred: linked servers, statistics, constraints, rules, defaults, triggers, [!INCLUDE[ssSB](../../includes/sssb-md.md)] queues, credentials, partition functions, partition schemes, database master keys, service master key, and event notifications.    
    
 Ownership of members of the following securable classes cannot be transferred: server, login, user, application role, and column.    
    
 The SCHEMA OWNER option is only valid when you are transferring ownership of a schema-contained entity. SCHEMA OWNER will transfer ownership of the entity to the owner of the schema in which it resides. Only entities of class OBJECT, TYPE, or XML SCHEMA COLLECTION are schema-contained.    
    
 If the target entity is not a database and the entity is being transferred to a new owner, all permissions on the target will be dropped.    
    
> [!CAUTION]    
>  In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the behavior of schemas changed from the behavior in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Code that assumes that schemas are equivalent to database users may not return correct results. Old catalog views, including sysobjects, should not be used in a database in which any of the following DDL statements has ever been used: CREATE SCHEMA, ALTER SCHEMA, DROP SCHEMA, CREATE USER, ALTER USER, DROP USER, CREATE ROLE, ALTER ROLE, DROP ROLE, CREATE APPROLE, ALTER APPROLE, DROP APPROLE, ALTER AUTHORIZATION. In a database in which any of these statements has ever been used, you must use the new catalog views. The new catalog views take into account the separation of principals and schemas that was introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. For more information about catalog views, see [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md).    
    
 Also, note the following:    
    
> [!IMPORTANT]    
>  The only reliable way to find the owner of a object is to query the **sys.objects** catalog view. The only reliable way to find the owner of a type is to use the TYPEPROPERTY function.    
    
## Special Cases and Conditions    
 The following table lists special cases, exceptions, and conditions that apply to altering authorization.    
    
|Class|Condition|    
|-----------|---------------|    
|OBJECT|Cannot change ownership of triggers, constraints, rules, defaults, statistics, system objects, queues, indexed views, or tables with indexed views.|    
|SCHEMA|When ownership is transferred, permissions on schema-contained objects that do not have explicit owners will be dropped. Cannot change the owner of sys, dbo, or information_schema.|    
|TYPE|Cannot change ownership of a TYPE that belongs to sys or information_schema.|    
|CONTRACT, MESSAGE TYPE, or SERVICE|Cannot change ownership of system entities.|    
|SYMMETRIC KEY|Cannot change ownership of global temporary keys.|    
|CERTIFICATE or ASYMMETRIC KEY|Cannot transfer ownership of these entities to a role or group.|    
|ENDPOINT|The principal must be a login.|    
  
## <a name="AlterDB"></a> ALTER AUTHORIZATION for databases  
**APPLIES TO**: [!INCLUDE[ssSQL15](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
### For SQL Server:  
**Requirements for the new owner:**   
The new owner principal must be one of the following:  

-   A SQL Server authentication login.  
-   A Windows authentication login representing a Windows user (not a group).  
-   A Windows user that authenticates through a Windows authentication login representing a Windows group.  
  
**Requirements for the person executing the ALTER AUTHORIZATION statement:**  
If you are not a member of the **sysadmin** fixed server role, you must have at least TAKE OWNERSHIP permission on the database, and must have IMPERSONATE permission on the new owner login.   

### For Azure SQL Database:  
**Requirements for the new owner:**   
The new owner principal must be one of the following:  

-   A SQL Server authentication login.  
-   A federated user (not a group) present in Azure AD.  
-   A managed user (not a group) or an application present in Azure AD.    

> [!NOTE]  
> If the new owner is an Azure Active Directory user, it cannot exist as a user in the database where the new owner will become the new DBO. Such Azure AD user must be first removed from the database before executing the ALTER AUTHORIZATION statement changing the database ownership to the new user. For more information about configuring an Azure Active Directory users with SQL Database, see [Connecting to SQL Database or SQL Data Warehouse By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/).   
  
**Requirements for the person executing the ALTER AUTHORIZATION statement:**  
You must connect to the target database to change the owner of that database.  

The following types of accounts can change the owner of a database. 

* The service-level principal login. (The SQL Azure administrator provisioned when the SQL Database server was created.)  
* The Azure Active Directory administrator for the Azure SQL Server.   
* The current owner of the database.   
 
  
The following table summarizes the requirements:  
  
Executor  |Target  |Result    
---------|---------|---------  
SQL Server Authentication login     |SQL Server Authentication login         |Success  
SQL Server Authentication login     |Azure AD user         |Fail           
Azure AD user     |SQL Server Authentication login         |Success           
Azure AD user     |Azure AD user         |Success           
  
To verify an Azure AD owner of the database execute the following Transact-SQL command in a user database (in this example `testdb`).  
    
```    
SELECT CAST(owner_sid as uniqueidentifier) AS Owner_SID   
FROM sys.databases   
WHERE name = 'testdb';  
```    
    
The output will be an identifier (such as 6D8B81F6-7C79-444C-8858-4AF896C03C67) which corresponds to Azure AD ObjectID assigned to `richel@cqclinic.onmicrosoft.com`  
When a SQL Server authentication login user is the database owner, execute the following statement in the master database to verify the database owner:  
    
```    
SELECT d.name, d.owner_sid, sl.name   
FROM sys.databases AS d  
JOIN sys.sql_logins AS sl  
ON d.owner_sid = sl.sid;  
    
```    
  
### Best practice  
  
Instead of using Azure AD users as individual owners of the database, use an Azure AD group as a member of the **db_owner** fixed database role. The following steps, show how to configure a disabled login as the database owner, and make an Azure Active Directory group (`mydbogroup`) a member of the **db_owner** role. 
1.  Login to SQL Server as Azure AD admin, and change the owner of the database to a disabled SQL Server authentication login. For example, from the user database execute:  
  ```    
  ALTER AUTHORIZATION ON database::testdb TO DisabledLogin;  
  ```    
2.  Create an Azure AD group that should own the database and add it as a user to the user database. For example:  
  ```    
  CREATE USER [mydbogroup] FROM EXTERNAL PROVIDER;  
  ```    
3.  In the user database add the user representing the Azure AD group, to the **db_owner** fixed database role. For example:  
  ```    
  ALTER ROLE db_owner ADD MEMBER mydbogroup;  
  ```    
  
Now the `mydbogroup` members can centrally manage the database as members of the **db_owner** role.  
- When members of this group are removed from the Azure AD group, they automatically loose the dbo permissions for this database.  
- Similarly if new members are added to `mydbogroup` Azure AD group, they automatically gain the dbo access for this database.  
  
To check if a specific user has the effective dbo permission, have the user execute the following statement:  
    
```    
SELECT IS_MEMBER ('db_owner');  
```    
  
A return value of 1 indicates the user is a member of the role.  
   
    
## Permissions    
 Requires TAKE OWNERSHIP permission on the entity. If the new owner is not the user that is executing this statement, also requires either, 1) IMPERSONATE permission on the new owner if it is a user or login; or 2) if the new owner is a role, membership in the role, or ALTER permission on the role; or 3) if the new owner is an application role, ALTER permission on the application role.    
    
## Examples    
    
### A. Transfer ownership of a table    
 The following example transfers ownership of table `Sprockets` to user `MichikoOsada`. The table is located inside schema `Parts`.    
    
```    
ALTER AUTHORIZATION ON OBJECT::Parts.Sprockets TO MichikoOsada;    
GO    
```    
    
 The query could also look like the following:    
    
```    
ALTER AUTHORIZATION ON Parts.Sprockets TO MichikoOsada;    
GO    
```    
    
 If the objects schema is not included as part of the statement, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will look for the object in the users default schema. For example:    
    
```    
ALTER AUTHORIZATION ON Sprockets TO MichikoOsada;    
ALTER AUTHORIZATION ON OBJECT::Sprockets TO MichikoOsada;    
```    
    
### B. Transfer ownership of a view to the schema owner    
 The following example transfers ownership the view `ProductionView06` to the owner of the schema that contains it. The view is located inside schema `Production`.    
    
```    
ALTER AUTHORIZATION ON OBJECT::Production.ProductionView06 TO SCHEMA OWNER;    
GO    
```    
    
### C. Transfer ownership of a schema to a user    
 The following example transfers ownership of the schema `SeattleProduction11` to user `SandraAlayo`.    
    
```    
ALTER AUTHORIZATION ON SCHEMA::SeattleProduction11 TO SandraAlayo;    
GO    
```    
    
### D. Transfer ownership of an endpoint to a SQL Server login    
 The following example transfers ownership of endpoint `CantabSalesServer1` to `JaePak`. Because the endpoint is a server-level securable, the endpoint can only be transferred to a server-level principal.    
    
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].    
    
```    
ALTER AUTHORIZATION ON ENDPOINT::CantabSalesServer1 TO JaePak;    
GO    
```    
    
### E. Changing the owner of a table    
 Each of the following examples changes the owner of the `Sprockets` table in the `Parts` database to the database user `MichikoOsada`.    
```    
ALTER AUTHORIZATION ON Sprockets TO MichikoOsada;    
ALTER AUTHORIZATION ON dbo.Sprockets TO MichikoOsada;    
ALTER AUTHORIZATION ON OBJECT::Sprockets TO MichikoOsada;    
ALTER AUTHORIZATION ON OBJECT::dbo.Sprockets TO MichikoOsada;    
```    
    
### F. Changing the owner of a database    
 **APPLIES TO**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].    
    
 The following example change the owner of the `Parts` database to the login `MichikoOsada`.    
    
```    
ALTER AUTHORIZATION ON DATABASE::Parts TO MichikoOsada;    
```    
  
### G. Changing the owner of a SQL Database to an Azure AD User  
In the following example, an Azure Active Directory administrator for SQL Server in an organization with an active directory named `cqclinic.onmicrosoft.com`, can change the current ownership of a database `targetDB` and make an AAD user  `richel@cqclinic.onmicorsoft.com` the new database owner using the following command:  
    
```    
ALTER AUTHORIZATION ON database::targetDB TO [rachel@cqclinic.onmicrosoft.com];   
```    
    
 Note that for Azure AD users the brackets around the user name must be used.  
  
    
## See Also    
 [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md)     
 [TYPEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/typeproperty-transact-sql.md)     
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)    
 
