---
title: DENY (Transact-SQL)
description: DENY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DENY"
  - "DENY_TSQL"
helpviewer_keywords:
  - "schema-level securables [SQL Server]"
  - "security [SQL Server], denying access"
  - "DENY statement"
  - "permissions [SQL Server], denying"
  - "server-level securables [SQL Server]"
  - "inherited security settings [SQL Server]"
  - "denying permissions [SQL Server], DENY statement"
  - "principal security [SQL Server]"
  - "database-level securables [SQL Server]"
  - "denying permissions [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# DENY (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Denies a permission to a principal. Prevents that principal from inheriting the permission through its group or role memberships. DENY takes precedence over all permissions, except that DENY does not apply to object owners or members of the sysadmin fixed server role.
  **Security Note** Members of the sysadmin fixed server role and object owners cannot be denied permissions."
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
-- Simplified syntax for DENY  
DENY   { ALL [ PRIVILEGES ] } 
     | <permission>  [ ( column [ ,...n ] ) ] [ ,...n ]  
    [ ON [ <class> :: ] securable ] 
    TO principal [ ,...n ]   
    [ CASCADE] [ AS principal ]  
[;]

<permission> ::=  
{ see the tables below }  
  
<class> ::=  
{ see the tables below }  
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
DENY   
    <permission> [ ,...n ]  
    [ ON [ <class_> :: ] securable ]   
    TO principal [ ,...n ]  
    [ CASCADE ]  
[;]  
  
<permission> ::=  
{ see the tables below }  
  
<class> ::=  
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
 This option does not deny all possible permissions. Denying ALL is equivalent to denying the following permissions.  
  
-   If the securable is a database, ALL means BACKUP DATABASE, BACKUP LOG, CREATE DATABASE, CREATE DEFAULT, CREATE FUNCTION, CREATE PROCEDURE, CREATE RULE, CREATE TABLE, and CREATE VIEW.  
  
-   If the securable is a scalar function, ALL means EXECUTE and REFERENCES.  
  
-   If the securable is a table-valued function, ALL means DELETE, INSERT, REFERENCES, SELECT, and UPDATE.  
  
-   If the securable is a stored procedure, ALL means EXECUTE.  
  
-   If the securable is a table, ALL means DELETE, INSERT, REFERENCES, SELECT, and UPDATE.  
  
-   If the securable is a view, ALL means DELETE, INSERT, REFERENCES, SELECT, and UPDATE.  
  
> [!NOTE]  
>  The DENY ALL syntax is deprecated. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Deny specific permissions instead.  
  
 PRIVILEGES  
 Included for ISO compliance. Does not change the behavior of ALL.  
  
 *permission*  
 Is the name of a permission. The valid mappings of permissions to securables are described in the sub-topics listed below.  
  
 *column*  
 Specifies the name of a column in a table on which permissions are being denied. The parentheses () are required.  
  
 *class*  
 Specifies the class of the securable on which the permission is being denied. The scope qualifier **::** is required.  
  
 *securable*  
 Specifies the securable on which the permission is being denied.  
  
 TO *principal*  
 Is the name of a principal. The principals to which permissions on a securable can be denied vary, depending on the securable. See the securable-specific topics listed below for valid combinations.  
  
 CASCADE  
 Indicates that the permission is denied to the specified principal and to all other principals to which the principal granted the permission. Required when the principal has the permission with GRANT OPTION.  
  
 AS *principal*  
 Specifies the principal from which the principal executing this query derives its right to deny the permission.
 Use the AS principal clause to indicate that the principal recorded as the denier of the permission should be a principal other than the person executing the statement. For example, presume that user Mary is principal_id 12 and user Raul is principal 15. Mary executes `DENY SELECT ON OBJECT::X TO Steven WITH GRANT OPTION AS Raul;` Now the sys.database_permissions table will indicate that the grantor_prinicpal_id of the deny statement was 15 (Raul) even though the statement was actually executed by user 13 (Mary).
  
The use of AS in this statement does not imply the ability to impersonate another user.  
  
## Remarks  
 The full syntax of the DENY statement is complex. The syntax diagram above was simplified to draw attention to its structure. Complete syntax for denying permissions on specific securables is described in the topics listed below.  
  
 DENY will fail if CASCADE is not specified when denying a permission to a principal that was granted that permission with GRANT OPTION specified.  
  
 The sp_helprotect system stored procedure reports permissions on a database-level securable.  
  
> [!CAUTION]  
>  A table-level DENY does not take precedence over a column-level GRANT. This inconsistency in the permissions hierarchy has been preserved for the sake of backward compatibility. It will be removed in a future release.  
  
> [!CAUTION]  
>  Denying CONTROL permission on a database implicitly denies CONNECT permission on the database. A principal that is denied CONTROL permission on a database will not be able to connect to that database.  
  
> [!CAUTION]  
>  Denying CONTROL SERVER permission implicitly denies CONNECT SQL permission on the server. A principal that is denied CONTROL SERVER permission on a server will not be able to connect to that server.  
  
## Permissions  
 The caller (or the principal specified with the AS option) must have either CONTROL permission on the securable, or a higher permission that implies CONTROL permission on the securable. If using the AS option, the specified principal must own the securable on which a permission is being denied.  
  
 Grantees of CONTROL SERVER permission, such as members of the sysadmin fixed server role, can deny any permission on any securable in the server. Grantees of CONTROL permission on the database, such as members of the db_owner fixed database role, can deny any permission on any securable in the database. Grantees of CONTROL permission on a schema can deny any permission on any object in the schema. If the AS clause is used, the specified principal must own the securable on which permissions are being denied.  
  
## Examples

 The following table lists the securables and the topics that describe the securable-specific syntax.  
  
|Securables|Syntax|
|----------|------|
|Application Role|[DENY Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-database-principal-permissions-transact-sql.md)|  
|Assembly|[DENY Assembly Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-assembly-permissions-transact-sql.md)|  
|Asymmetric Key|[DENY Asymmetric Key Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-asymmetric-key-permissions-transact-sql.md)|  
|Availability Group|[DENY Availability Group Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-availability-group-permissions-transact-sql.md)|  
|Certificate|[DENY Certificate Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-certificate-permissions-transact-sql.md)|  
|Contract|[DENY Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-service-broker-permissions-transact-sql.md)|  
|Database|[DENY Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-database-permissions-transact-sql.md)|  
|Database Scoped Credential|[DENY Database Scoped Credential (Transact-SQL)](../../t-sql/statements/deny-database-scoped-credential-transact-sql.md)|  
|Endpoint|[DENY Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-endpoint-permissions-transact-sql.md)|  
|Full-Text Catalog|[DENY Full-Text Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-full-text-permissions-transact-sql.md)|  
|Full-Text Stoplist|[DENY Full-Text Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-full-text-permissions-transact-sql.md)|  
|Function|[DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)|  
|Login|[DENY Server Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-server-principal-permissions-transact-sql.md)|  
|Message Type|[DENY Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-service-broker-permissions-transact-sql.md)|  
|Object|[DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)|  
|Queue|[DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)|  
|Remote Service Binding|[DENY Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-service-broker-permissions-transact-sql.md)|  
|Role|[DENY Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-database-principal-permissions-transact-sql.md)|  
|Route|[DENY Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-service-broker-permissions-transact-sql.md)|  
|Schema|[DENY Schema Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-schema-permissions-transact-sql.md)|  
|Search Property List|[DENY Search Property List Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-search-property-list-permissions-transact-sql.md)|  
|Server|[DENY Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-server-permissions-transact-sql.md)|  
|Service|[DENY Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-service-broker-permissions-transact-sql.md)|  
|Stored Procedure|[DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)|  
|Symmetric Key|[DENY Symmetric Key Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-symmetric-key-permissions-transact-sql.md)|  
|Synonym|[DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)|  
|System Objects|[DENY System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-system-object-permissions-transact-sql.md)|  
|Table|[DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)|  
|Type|[DENY Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-type-permissions-transact-sql.md)|  
|User|[DENY Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-database-principal-permissions-transact-sql.md)|  
|View|[DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)|  
|XML Schema Collection|[DENY XML Schema Collection Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-xml-schema-collection-permissions-transact-sql.md)|  
  
## See Also  
 [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)   
 [sp_addlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogin-transact-sql.md)   
 [sp_adduser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adduser-transact-sql.md)   
 [sp_changedbowner &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedbowner-transact-sql.md)   
 [sp_dropuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropuser-transact-sql.md)   
 [sp_helprotect &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helprotect-transact-sql.md)   
 [sp_helpuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpuser-transact-sql.md)  
  
  
