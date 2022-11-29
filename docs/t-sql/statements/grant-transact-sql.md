---
title: GRANT (Transact-SQL)
description: GRANT (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/12/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GRANT_TSQL"
  - "GRANT"
helpviewer_keywords:
  - "granting permissions [SQL Server], GRANT statement"
  - "schema-level securables [SQL Server]"
  - "GRANT statement"
  - "cross-database permissions"
  - "GRANT statement, about GRANT statement"
  - "server-level securables [SQL Server]"
  - "database-level securables [SQL Server]"
  - "permissions [SQL Server], granting"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# GRANT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Grants permissions on a securable to a principal.  The general concept is to GRANT \<some permission> ON \<some object> TO \<some user, login, or group>. For a general discussion of permissions, see [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md).  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
-- Simplified syntax for GRANT  
GRANT { ALL [ PRIVILEGES ] }  
      | permission [ ( column [ ,...n ] ) ] [ ,...n ]  
      [ ON [ class :: ] securable ] TO principal [ ,...n ]   
      [ WITH GRANT OPTION ] [ AS principal ]  
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
GRANT
    <permission> [ ,...n ]  
    [ ON [ <class_type> :: ] securable ]
    TO principal [ ,...n ]  
    [ WITH GRANT OPTION ]  
[;]  
  
<permission> ::=  
{ see the tables below }  
  
<class_type> ::=  
{  
      LOGIN  
    | DATABASE  
    | OBJECT  
    | ROLE  
    | SCHEMA  
    | USER  
}  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

ALL  
This option is deprecated and maintained only for backward compatibility. It does not grant all possible permissions. Granting ALL is equivalent to granting the following permissions:

  - If the securable is a database, ALL means BACKUP DATABASE, BACKUP LOG, CREATE DATABASE, CREATE DEFAULT, CREATE FUNCTION, CREATE PROCEDURE, CREATE RULE, CREATE TABLE, and CREATE VIEW.  
  
  - If the securable is a scalar function, ALL means EXECUTE and REFERENCES.  
  
  - If the securable is a table-valued function, ALL means DELETE, INSERT, REFERENCES, SELECT, and UPDATE.  
  
  - If the securable is a stored procedure, ALL means EXECUTE.  
  
  - If the securable is a table, ALL means DELETE, INSERT, REFERENCES, SELECT, and UPDATE.  
  
  - If the securable is a view, ALL means DELETE, INSERT, REFERENCES, SELECT, and UPDATE.  

PRIVILEGES  
Included for ISO compliance. Does not change the behavior of ALL.  
  
*permission*  
Is the name of a permission. The valid mappings of permissions to securables are described in the subtopics listed below.  
  
*column*  
Specifies the name of a column in a table on which permissions are being granted. The parentheses () are required.  
  
*class*  
Specifies the class of the securable on which the permission is being granted. The scope qualifier **::** is required.  
  
*securable*  
Specifies the securable on which the permission is being granted.  
  
TO *principal*  
Is the name of a principal. The principals to which permissions on a securable can be granted vary, depending on the securable. See the subtopics listed below for valid combinations.  
  
GRANT OPTION  
Indicates that the grantee will also be given the ability to grant the specified permission to other principals.  
  
AS *principal*  
Use the AS principal clause to indicate that the principal recorded as the grantor of the permission should be a principal other than the person executing the statement. For example, presume that user Mary is principal_id 12 and user Raul is principal 15. Mary executes `GRANT SELECT ON OBJECT::X TO Steven WITH GRANT OPTION AS Raul;` Now the sys.database_permissions table will indicate that the grantor_prinicpal_id was 15 (Raul) even though the statement was actually executed by user 13 (Mary).  

Using the AS clause is typically not recommended unless you need to explicitly define the permission chain. For more information, see the **Summary of the Permission Check Algorithm** section of [Permissions (Database Engine)](../../relational-databases/security/permissions-database-engine.md).

The use of AS in this statement does not imply the ability to impersonate another user.

## Remarks

The full syntax of the GRANT statement is complex. The syntax diagram above was simplified to draw attention to its structure. Complete syntax for granting permissions on specific securables is described in the articles listed below.  

The REVOKE statement can be used to remove granted permissions, and the DENY statement can be used to prevent a principal from gaining a specific permission through a GRANT.  

Granting a permission removes DENY or REVOKE of that permission on the specified securable. If the same permission is denied at a higher scope that contains the securable, the DENY takes precedence. But revoking the granted permission at a higher scope does not take precedence.  

Database-level permissions are granted within the scope of the specified database. If a user needs permissions to objects in another database, create the user account in the other database, or grant the user account access to the other database, as well as the current database.  

> [!CAUTION]  
> A table-level DENY does not take precedence over a column-level GRANT. This inconsistency in the permissions hierarchy has been preserved for the sake of backward compatibility. It will be removed in a future release.  

The sp_helprotect system stored procedure reports permissions on a database-level securable.  

## WITH GRANT OPTION

The **GRANT** ... **WITH GRANT OPTION** specifies that the security principal receiving the permission is given the ability to grant the specified permission to other security accounts. When the principal that receives the permission is a role or a Windows group, the **AS** clause must be used when the object permission needs to be further granted to users who are not members of the group or role. Because only a user, rather than a group or role, can execute a **GRANT** statement, a specific member of the group or role must use the **AS** clause to explicitly invoke the role or group membership when granting the permission. The following example shows how the **WITH GRANT OPTION** is used when granted to a role or Windows group.  
  
```sql
-- Execute the following as a database owner  
GRANT EXECUTE ON TestProc TO TesterRole WITH GRANT OPTION;  
EXEC sp_addrolemember TesterRole, User1;  
-- Execute the following as User1  
-- The following fails because User1 does not have the permission as the User1  
GRANT EXECUTE ON TestMe TO User2;  
-- The following succeeds because User1 invokes the TesterRole membership  
GRANT EXECUTE ON TestMe TO User2 AS TesterRole;  
```

## Chart of SQL Server Permissions

For a poster sized chart of all [!INCLUDE[ssDE](../../includes/ssde-md.md)] permissions in pdf format, see [https://aka.ms/sql-permissions-poster](https://aka.ms/sql-permissions-poster).  

## Permissions

The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION, or a higher permission that implies the permission being granted. If using the AS option, additional requirements apply. See the securable-specific article for details.

Object owners can grant permissions on the objects they own. Principals with CONTROL permission on a securable can grant permission on that securable.

Grantees of CONTROL SERVER permission, such as members of the sysadmin fixed server role, can grant any permission on any securable in the server. Grantees of CONTROL permission on a database, such as members of the db_owner fixed database role, can grant any permission on any securable in the database. Grantees of CONTROL permission on a schema can grant any permission on any object within the schema.

## Examples

The following table lists the securables and the articles that describe the securable-specific syntax.  

| Securable | GRANT Syntax|
| ---------| ------ |
|Application Role|[GRANT Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-principal-permissions-transact-sql.md)|  
|Assembly|[GRANT Assembly Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-assembly-permissions-transact-sql.md)|  
|Asymmetric Key|[GRANT Asymmetric Key Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-asymmetric-key-permissions-transact-sql.md)|  
|Availability Group|[GRANT Availability Group Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-availability-group-permissions-transact-sql.md)|  
|Certificate|[GRANT Certificate Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-certificate-permissions-transact-sql.md)|  
|Contract|[GRANT Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)|  
|Database|[GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md)|
|Database Scoped Credential|[GRANT Database Scoped Credential (Transact-SQL)](../../t-sql/statements/grant-database-scoped-credential-transact-sql.md)|  
|Endpoint|[GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md)|  
|Full-Text Catalog|[GRANT Full-Text Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-full-text-permissions-transact-sql.md)|  
|Full-Text Stoplist|[GRANT Full-Text Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-full-text-permissions-transact-sql.md)|  
|Function|[GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)|  
|Login|[GRANT Server Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-principal-permissions-transact-sql.md)|  
|Message Type|[GRANT Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)|  
|Object|[GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)|  
|Queue|[GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)|  
|Remote Service Binding|[GRANT Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)|  
|Role|[GRANT Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-principal-permissions-transact-sql.md)|  
|Route|[GRANT Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)|  
|Schema|[GRANT Schema Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-schema-permissions-transact-sql.md)|  
|Search Property List|[GRANT Search Property List Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-search-property-list-permissions-transact-sql.md)|  
|Server|[GRANT Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-permissions-transact-sql.md)|  
|Service|[GRANT Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)|  
|Stored Procedure|[GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)|  
|Symmetric Key|[GRANT Symmetric Key Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-symmetric-key-permissions-transact-sql.md)|  
|Synonym|[GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)|  
|System Objects|[GRANT System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-system-object-permissions-transact-sql.md)|  
|Table|[GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)|  
|Type|[GRANT Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-type-permissions-transact-sql.md)|  
|User|[GRANT Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-principal-permissions-transact-sql.md)|  
|View|[GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)|  
|XML Schema Collection|[GRANT XML Schema Collection Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-xml-schema-collection-permissions-transact-sql.md)|  

## See Also

- [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)
- [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)
- [sp_addlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogin-transact-sql.md)
- [sp_adduser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adduser-transact-sql.md)
- [sp_changedbowner &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedbowner-transact-sql.md)
- [sp_dropuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropuser-transact-sql.md)
- [sp_helprotect &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helprotect-transact-sql.md)
- [sp_helpuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpuser-transact-sql.md)
