---
title: "sys.fn_builtin_permissions (Transact-SQL)"
description: "Returns a description of the built in permissions hierarchy of the server."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/14/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fn_builtin_permissions"
  - "sys.fn_builtin_permissions_TSQL"
  - "fn_builtin_permissions_TSQL"
  - "sys.fn_builtin_permissions"
helpviewer_keywords:
  - "compact permissions types"
  - "viewing permission hierarchy"
  - "hierarchies [SQL Server], permissions"
  - "built-in permissions hierarchy [SQL Server]"
  - "fn_builtin_permissions function"
  - "permissions [SQL Server], hierarchy"
  - "displaying permission hierarchy"
  - "sys.fn_builtin_permissions function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_builtin_permissions (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a description of the built in permissions hierarchy of the server. `sys.fn_builtin_permissions` can only be called on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and it returns all permissions regardless of whether they are supported on the current platform. Most permissions apply to all platforms, but some do not. For example server level permissions cannot be granted on SQL Database. For information about which platforms support each permission, see [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md).

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.fn_builtin_permissions ( [ DEFAULT | NULL ]
    | empty_string | '<securable_class>' } )
  
<securable_class> ::=
      APPLICATION ROLE | ASSEMBLY | ASYMMETRIC KEY | AVAILABILITY GROUP
    | CERTIFICATE | CONTRACT | DATABASE | DATABASE SCOPED CREDENTIAL
    | ENDPOINT | FULLTEXT CATALOG | FULLTEXT STOPLIST | LOGIN
    | MESSAGE TYPE | OBJECT | REMOTE SERVICE BINDING | ROLE | ROUTE
    | SCHEMA | SEARCH PROPERTY LIST | SERVER | SERVER ROLE | SERVICE
    | SYMMETRIC KEY | TYPE | USER | XML SCHEMA COLLECTION
```

## Arguments

#### [ DEFAULT | NULL ]

When it is called with the DEFAULT option (without quotes), the function will return a complete list of built in permissions.

NULL is equivalent to DEFAULT.

#### *empty_string*

Equivalent to DEFAULT.

#### '<securable_class>'

When called with the name of one securable class, `sys.fn_builtin_permissions` will return all permissions that apply to the class. `'<securable_class>'` is a string literal of type **nvarchar(60)** that requires quotation marks.

## Tables returned

|Column name|Data type|Collation|Description|
|-----------------|---------------|---------------|-----------------|
|class_desc|**nvarchar(60)**|Collation of the server|Description of the securable class.|
|permission_name|**nvarchar(60)**|Collation of the server|Permission name.|
|type|**varchar(4)**|Collation of the server|Compact permission type code. See the table that follows.|
|covering_permission_name|**nvarchar(60)**|Collation of the server|If not NULL, this is the name of the permission on this class that implies the other permissions on this class.|
|parent_class_desc|**nvarchar(60)**|Collation of the server|If not NULL, this is the name of the parent class that contains the current class.|
|parent_covering_permission_name|**nvarchar(60)**|Collation of the server|If not NULL, this is the name of the permission on the parent class that implies all other permissions on that class.|

### Permission types

|Permission type|Permission name|Applies to securable or class|
|---------------------|---------------------|-----------------------------------|
|AADS|ALTER ANY DATABASE EVENT SESSION<br /><br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [later versions](/troubleshoot/sql/general/determine-version-edition-update-level).|DATABASE|
|AAES|ALTER ANY EVENT SESSION|SERVER|
|AAMK|ALTER ANY MASK<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|ADBO|ADMINISTER BULK OPERATIONS|SERVER|
|AEDS|ALTER ANY EXTERNAL DATA SOURCE<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|AEFF|ALTER ANY EXTERNAL FILE FORMAT<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|AL|ALTER|APPLICATION ROLE|
|AL|ALTER|ASSEMBLY|
|AL|ALTER<br /><br />**Applies to**: [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions.|AVAILABILITY GROUP|
|AL|ALTER|ASYMMETRIC KEY|
|AL|ALTER|CERTIFICATE|
|AL|ALTER|CONTRACT|
|AL|ALTER|DATABASE|
|AL|ALTER<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. |DATABASE SCOPED CREDENTIAL|
|AL|ALTER|ENDPOINT|
|AL|ALTER|FULLTEXT CATALOG|
|AL|ALTER|FULLTEXT STOPLIST|
|AL|ALTER|LOGIN|
|AL|ALTER|MESSAGE TYPE|
|AL|ALTER|OBJECT|
|AL|ALTER|REMOTE SERVICE BINDING|
|AL|ALTER|ROLE|
|AL|ALTER|ROUTE|
|AL|ALTER|SCHEMA|
|AL|ALTER|SEARCH PROPERTY LIST|
|AL|ALTER<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER ROLE|
|AL|ALTER|SERVICE|
|AL|ALTER|SYMMETRIC KEY|
|AL|ALTER|USER|
|AL|ALTER|XML SCHEMA COLLECTION|
|ALAA|ALTER ANY SERVER AUDIT|SERVER|
|ALAG|ALTER ANY AVAILABILITY GROUP<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER|
|ALAK|ALTER ANY ASYMMETRIC KEY|DATABASE|
|ALAR|ALTER ANY APPLICATION ROLE|DATABASE|
|ALAS|ALTER ANY ASSEMBLY|DATABASE|
|ALCD|ALTER ANY CREDENTIAL|SERVER|
|ALCF|ALTER ANY CERTIFICATE|DATABASE|
|ALCK|ALTER ANY COLUMN ENCRYPTION KEY<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|ALCM|ALTER ANY COLUMN MASTER KEY<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|ALCO|ALTER ANY CONNECTION|SERVER|
|ALDA|ALTER ANY DATABASE AUDIT|DATABASE|
|ALDB|ALTER ANY DATABASE|SERVER|
|ALDC|ALTER ANY DATABASE SCOPED CONFIGURATION<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|ALDS|ALTER ANY DATASPACE|DATABASE|
|ALED|ALTER ANY DATABASE EVENT NOTIFICATION|DATABASE|
|ALES|ALTER ANY EVENT NOTIFICATION|SERVER|
|ALFT|ALTER ANY FULLTEXT CATALOG|DATABASE|
|ALHE|ALTER ANY ENDPOINT|SERVER|
|ALLG|ALTER ANY LOGIN|SERVER|
|ALLS|ALTER ANY LINKED SERVER|SERVER|
|ALMT|ALTER ANY MESSAGE TYPE|DATABASE|
|ALRL|ALTER ANY ROLE|DATABASE|
|ALRS|ALTER RESOURCES|SERVER|
|ALRT|ALTER ANY ROUTE|DATABASE|
|ALSB|ALTER ANY REMOTE SERVICE BINDING|DATABASE|
|ALSC|ALTER ANY CONTRACT|DATABASE|
|ALSK|ALTER ANY SYMMETRIC KEY|DATABASE|
|ALSM|ALTER ANY SCHEMA|DATABASE|
|ALSP|ALTER ANY SECURITY POLICY<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|ALSR|ALTER ANY SERVER ROLE<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER|
|ALSS|ALTER SERVER STATE|SERVER|
|ALST|ALTER SETTINGS|SERVER|
|ALSV|ALTER ANY SERVICE|DATABASE|
|ALTG|ALTER ANY DATABASE DDL TRIGGER|DATABASE|
|ALTR|ALTER TRACE|SERVER|
|ALUS|ALTER ANY USER|DATABASE|
|AUTH|AUTHENTICATE|DATABASE|
|AUTH|AUTHENTICATE SERVER|SERVER|
|BADB|BACKUP DATABASE|DATABASE|
|BALO|BACKUP LOG|DATABASE|
|CADB|CONNECT ANY DATABASE<br /><br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later versions.|SERVER|
|CL|CONTROL|APPLICATION ROLE|
|CL|CONTROL|ASSEMBLY|
|CL|CONTROL|ASYMMETRIC KEY|
|CL|CONTROL<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|AVAILABILITY GROUP|
|CL|CONTROL|CERTIFICATE|
|CL|CONTROL|CONTRACT|
|CL|CONTROL|DATABASE|
|CL|CONTROL<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. |DATABASE SCOPED CREDENTIAL|
|CL|CONTROL|ENDPOINT|
|CL|CONTROL|FULLTEXT CATALOG|
|CL|CONTROL|FULLTEXT STOPLIST|
|CL|CONTROL|LOGIN|
|CL|CONTROL|MESSAGE TYPE|
|CL|CONTROL|OBJECT|
|CL|CONTROL|REMOTE SERVICE BINDING|
|CL|CONTROL|ROLE|
|CL|CONTROL|ROUTE|
|CL|CONTROL|SCHEMA|
|CL|CONTROL|SEARCH PROPERTY LIST|
|CL|CONTROL SERVER|SERVER|
|CL|CONTROL<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER ROLE|
|CL|CONTROL|SERVICE|
|CL|CONTROL|SYMMETRIC KEY|
|CL|CONTROL|TYPE|
|CL|CONTROL|USER|
|CL|CONTROL|XML SCHEMA COLLECTION|
|CO|CONNECT|DATABASE|
|CO|CONNECT|ENDPOINT|
|CORP|CONNECT REPLICATION|DATABASE|
|COSQ|CONNECT SQL|SERVER|
|CP|CHECKPOINT|DATABASE|
|CRAC|CREATE AVAILABILITY GROUP<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER|
|CRAG|CREATE AGGREGATE|DATABASE|
|CRAK|CREATE ASYMMETRIC KEY|DATABASE|
|CRAS|CREATE ASSEMBLY|DATABASE|
|CRCF|CREATE CERTIFICATE|DATABASE|
|CRDB|CREATE ANY DATABASE|SERVER|
|CRDB|CREATE DATABASE|DATABASE|
|CRDE|CREATE DDL EVENT NOTIFICATION|SERVER|
|CRDF|CREATE DEFAULT|DATABASE|
|CRED|CREATE DATABASE DDL EVENT NOTIFICATION|DATABASE|
|CRFN|CREATE FUNCTION|DATABASE|
|CRFT|CREATE FULLTEXT CATALOG|DATABASE|
|CRHE|CREATE ENDPOINT|SERVER|
|CRMT|CREATE MESSAGE TYPE|DATABASE|
|CRPR|CREATE PROCEDURE|DATABASE|
|CRQU|CREATE QUEUE|DATABASE|
|CRRL|CREATE ROLE|DATABASE|
|CRRT|CREATE ROUTE|DATABASE|
|CRRU|CREATE RULE|DATABASE|
|CRSB|CREATE REMOTE SERVICE BINDING|DATABASE|
|CRSC|CREATE CONTRACT|DATABASE|
|CRSK|CREATE SYMMETRIC KEY|DATABASE|
|CRSM|CREATE SCHEMA|DATABASE|
|CRSN|CREATE SYNONYM|DATABASE|
|CRSO|CREATE SEQUENCE|SCHEMA|
|CRSR|CREATE SERVER ROLE<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER|
|CRSV|CREATE SERVICE|DATABASE|
|CRTB|CREATE TABLE|DATABASE|
|CRTE|CREATE TRACE EVENT NOTIFICATION|SERVER|
|CRTY|CREATE TYPE|DATABASE|
|CRVW|CREATE VIEW|DATABASE|
|CRXS|CREATE XML SCHEMA COLLECTION|DATABASE|
|DABO|ADMINISTER DATABASE BULK OPERATIONS<br /><br />**Applies to**: [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].|DATABASE|
|DL|DELETE|DATABASE|
|DL|DELETE|OBJECT|
|DL|DELETE|SCHEMA|
|EAES|EXECUTE ANY EXTERNAL SCRIPT<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|EX|EXECUTE|DATABASE|
|EX|EXECUTE|OBJECT|
|EX|EXECUTE|SCHEMA|
|EX|EXECUTE|TYPE|
|EX|EXECUTE|XML SCHEMA COLLECTION|
|IAL|IMPERSONATE ANY LOGIN<br /><br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later versions.|SERVER|
|IM|IMPERSONATE|LOGIN|
|IM|IMPERSONATE|USER|
|IN|INSERT|DATABASE|
|IN|INSERT|OBJECT|
|IN|INSERT|SCHEMA|
|KIDC|KILL DATABASE CONNECTION<br /><br />**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|DATABASE|
|RC|RECEIVE|OBJECT|
|RF|REFERENCES|ASSEMBLY|
|RF|REFERENCES|ASYMMETRIC KEY|
|RF|REFERENCES|CERTIFICATE|
|RF|REFERENCES|CONTRACT|
|RF|REFERENCES|DATABASE|
|RF|REFERENCES<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. |DATABASE SCOPED CREDENTIAL|
|RF|REFERENCES|FULLTEXT CATALOG|
|RF|REFERENCES|FULLTEXT STOPLIST|
|RF|REFERENCES|SEARCH PROPERTY LIST|
|RF|REFERENCES|MESSAGE TYPE|
|RF|REFERENCES|OBJECT|
|RF|REFERENCES|SCHEMA|
|RF|REFERENCES|SYMMETRIC KEY|
|RF|REFERENCES|TYPE|
|RF|REFERENCES|XML SCHEMA COLLECTION|
|SHDN|SHUTDOWN|SERVER|
|SL|SELECT|DATABASE|
|SL|SELECT|OBJECT|
|SL|SELECT|SCHEMA|
|SN|SEND|SERVICE|
|SPLN|SHOWPLAN|DATABASE|
|SUQN|SUBSCRIBE QUERY NOTIFICATIONS|DATABASE|
|SUS|SELECT ALL USER SECURABLES<br /><br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later versions.|SERVER|
|TO|TAKE OWNERSHIP|ASSEMBLY|
|TO|TAKE OWNERSHIP|ASYMMETRIC KEY|
|TO|TAKE OWNERSHIP<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|AVAILABILITY GROUP|
|TO|TAKE OWNERSHIP|CERTIFICATE|
|TO|TAKE OWNERSHIP|CONTRACT|
|TO|TAKE OWNERSHIP|DATABASE|
|TO|TAKE OWNERSHIP<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. |DATABASE SCOPED CREDENTIAL|
|TO|TAKE OWNERSHIP|ENDPOINT|
|TO|TAKE OWNERSHIP|FULLTEXT CATALOG|
|TO|TAKE OWNERSHIP|FULLTEXT STOPLIST|
|TO|TAKE OWNERSHIP|SEARCH PROPERTY LIST|
|TO|TAKE OWNERSHIP|MESSAGE TYPE|
|TO|TAKE OWNERSHIP|OBJECT|
|TO|TAKE OWNERSHIP|REMOTE SERVICE BINDING|
|TO|TAKE OWNERSHIP|ROLE|
|TO|TAKE OWNERSHIP|ROUTE|
|TO|TAKE OWNERSHIP|SCHEMA|
|TO|TAKE OWNERSHIP<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER ROLE|
|TO|TAKE OWNERSHIP|SERVICE|
|TO|TAKE OWNERSHIP|SYMMETRIC KEY|
|TO|TAKE OWNERSHIP|TYPE|
|TO|TAKE OWNERSHIP|XML SCHEMA COLLECTION|
|UMSK|UNMASK<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|UP|UPDATE|DATABASE|
|UP|UPDATE|OBJECT|
|UP|UPDATE|SCHEMA|
|VW|VIEW DEFINITION|APPLICATION ROLE|
|VW|VIEW DEFINITION|ASSEMBLY|
|VW|VIEW DEFINITION|ASYMMETRIC KEY|
|VW|VIEW DEFINITION<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|AVAILABILITY GROUP|
|VW|VIEW DEFINITION|CERTIFICATE|
|VW|VIEW DEFINITION|CONTRACT|
|VW|VIEW DEFINITION|DATABASE|
|VW|VIEW DEFINITION<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. |DATABASE SCOPED CREDENTIAL|
|VW|VIEW DEFINITION|ENDPOINT|
|VW|VIEW DEFINITION|FULLTEXT CATALOG|
|VW|VIEW DEFINITION|FULLTEXT STOPLIST|
|VW|VIEW DEFINITION|LOGIN|
|VW|VIEW DEFINITION|MESSAGE TYPE|
|VW|VIEW DEFINITION|OBJECT|
|VW|VIEW DEFINITION|REMOTE SERVICE BINDING|
|VW|VIEW DEFINITION|ROLE|
|VW|VIEW DEFINITION|ROUTE|
|VW|VIEW DEFINITION|SCHEMA|
|VW|VIEW DEFINITION|SEARCH PROPERTY LIST|
|VW|VIEW DEFINITION<br /><br />**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.|SERVER ROLE|
|VW|VIEW DEFINITION|SERVICE|
|VW|VIEW DEFINITION|SYMMETRIC KEY|
|VW|VIEW DEFINITION|TYPE|
|VW|VIEW DEFINITION|USER|
|VW|VIEW DEFINITION|XML SCHEMA COLLECTION|
|VWAD|VIEW ANY DEFINITION|SERVER|
|VWCK|VIEW ANY COLUMN ENCRYPTION KEY DEFINITION<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|VWCM|VIEW ANY COLUMN MASTER KEY DEFINITION<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.|DATABASE|
|VWCT|VIEW CHANGE TRACKING|OBJECT|
|VWCT|VIEW CHANGE TRACKING|SCHEMA|
|VWDB|VIEW ANY DATABASE|SERVER|
|VWDS|VIEW DATABASE STATE|DATABASE|
|VWSS|VIEW SERVER STATE|SERVER|
|XA|EXTERNAL ACCESS ASSEMBLY|SERVER|
|XU|UNSAFE ASSEMBLY|SERVER|

## Remarks

 `sys.fn_builtin_permissions` is a table-valued function that emits a copy of the predefined permission hierarchy. This hierarchy includes covering permissions. The `DEFAULT` result set describes a directed, acyclic graph of the permissions hierarchy, of which the root is (class = `SERVER`, permission = `CONTROL SERVER`).

 `sys.fn_builtin_permissions` does not accept correlated parameters.

 `sys.fn_builtin_permissions` will return an empty set when it is called with a class name that is not valid.

[!INCLUDE[database-engine-permissions](../../includes/paragraph-content/database-engine-permissions.md)]

## Permissions

 Requires membership in the public role.

## Examples

### A. List all built in permissions

Use `DEFAULT` or an empty string to return all permissions.
```sql
SELECT * FROM sys.fn_builtin_permissions(DEFAULT);
SELECT * FROM sys.fn_builtin_permissions('');
```

### B. List permissions that can be set on a symmetric key

Specify a class to return all possible permissions for that class.
```sql
SELECT * FROM sys.fn_builtin_permissions(N'SYMMETRIC KEY');
```

### C. List classes on which there is a SELECT permission

```sql
SELECT * FROM sys.fn_builtin_permissions(DEFAULT)
    WHERE permission_name = 'SELECT';
```

## See also

- [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)
- [GRANT (Transact-SQL)](../../t-sql/statements/grant-transact-sql.md)
- [CREATE SCHEMA (Transact-SQL)](../../t-sql/statements/create-schema-transact-sql.md)
- [DROP SCHEMA (Transact-SQL)](../../t-sql/statements/drop-schema-transact-sql.md)
- [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)
- [sys.fn_my_permissions (Transact-SQL)](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)
- [HAS_PERMS_BY_NAME (Transact-SQL)](../../t-sql/functions/has-perms-by-name-transact-sql.md)
