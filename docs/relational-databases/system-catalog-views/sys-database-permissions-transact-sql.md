---
title: "sys.database_permissions (Transact-SQL)"
description: sys.database_permissions returns a row for every permission or column-exception permission in the database. 
author: VanMSFT
ms.author: vanto
ms.date: 06/16/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "database_permissions"
  - "sys.database_permissions_TSQL"
  - "database_permissions_TSQL"
  - "sys.database_permissions"
helpviewer_keywords:
  - "sys.database_permissions catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# sys.database_permissions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Returns a row for every permission or column-exception permission in the database. For columns, there is a row for every permission that is different from the corresponding object-level permission. If the column permission is the same as the corresponding object permission, there is no row for it and the permission applied is that of the object.  
  
> [!IMPORTANT]  
> Column-level permissions override object-level permissions on the same entity.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**class**|**tinyint**|Identifies class on which permission exists. For more information, see [sys.securable_classes (Transact-SQL)](sys-securable-classes-transact-sql.md).<br /><br /> 0 = Database<br />1 = Object or Column<br />3 = Schema<br />4 = Database Principal<br />5 = Assembly - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />6 = Type<br />10 = XML Schema Collection - <br />                      **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />15 = Message Type - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />16 = Service Contract - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />17 = Service - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />18 = Remote Service Binding - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />19 = Route - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />23 =Full-Text Catalog - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />24 = Symmetric Key - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />25 = Certificate - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />26 = Asymmetric Key - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />29 = Fulltext Stoplist - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />31 = Search Property List - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />32 = Database Scoped Credential - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br />34 = External Language - **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.|  
|**class_desc**|**nvarchar(60)**|Description of class on which permission exists.<br /><br /> DATABASE<br /><br /> OBJECT_OR_COLUMN<br /><br /> SCHEMA<br /><br /> DATABASE_PRINCIPAL<br /><br /> ASSEMBLY<br /><br /> TYPE<br /><br /> XML_SCHEMA_COLLECTION<br /><br /> MESSAGE_TYPE<br /><br /> SERVICE_CONTRACT<br /><br /> SERVICE<br /><br /> REMOTE_SERVICE_BINDING<br /><br /> ROUTE<br /><br /> FULLTEXT_CATALOG<br /><br /> SYMMETRIC_KEYS<br /><br /> CERTIFICATE<br /><br /> ASYMMETRIC_KEY<br /><br /> FULLTEXT STOPLIST<br /><br /> SEARCH PROPERTY LIST<br /><br /> DATABASE SCOPED CREDENTIAL<br /><br /> EXTERNAL LANGUAGE|  
|**major_id**|**int**|ID of thing on which permission exists, interpreted according to class. Usually, the `major_id` simply the kind of ID that applies to what the class represents. <br /><br /> 0 = The database itself <br /><br /> >0 = Object-IDs for user objects <br /><br /> \<0 = Object-IDs for system objects |  
|**minor_id**|**int**|Secondary-ID of thing on which permission exists, interpreted according to class. Often, the `minor_id` is zero, because there is no subcategory available for the class of object. Otherwise, it is the Column-ID of a table.|  
|**grantee_principal_id**|**int**|Database principal ID to which the permissions are granted.|  
|**grantor_principal_id**|**int**|Database principal ID of the grantor of these permissions.|  
|**type**|**char(4)**|Database permission type. For a list of permission types, see the next table.|  
|**permission_name**|**nvarchar(128)**|Permission name.|  
|**state**|**char(1)**|Permission state:<br /><br /> D = Deny<br /><br /> R = Revoke<br /><br /> G = Grant<br /><br /> W = Grant With Grant Option|  
|**state_desc**|**nvarchar(60)**|Description of permission state:<br /><br /> DENY<br /><br /> REVOKE<br /><br /> GRANT<br /><br /> GRANT_WITH_GRANT_OPTION|  

## Database Permissions

The following types of permissions are possible.
  
|Permission type|Permission name|Applies to securable|  
|---------------------|---------------------|--------------------------|  
|AADS |ALTER ANY DATABASE EVENT SESSION |DATABASE |  
|AAMK |ALTER ANY MASK |DATABASE |  
|AEDS |ALTER ANY EXTERNAL DATA SOURCE |DATABASE |  
|AEFF |ALTER ANY EXTERNAL FILE FORMAT |DATABASE |  
|AL|ALTER|APPLICATION ROLE, ASSEMBLY, ASYMMETRIC KEY, CERTIFICATE, CONTRACT, DATABASE, FULLTEXT CATALOG, MESSAGE TYPE, OBJECT, REMOTE SERVICE BINDING, ROLE, ROUTE, SCHEMA, SERVICE, SYMMETRIC KEY, USER, XML SCHEMA COLLECTION|  
|ALAK|ALTER ANY ASYMMETRIC KEY|DATABASE|  
|ALAR|ALTER ANY APPLICATION ROLE|DATABASE|  
|ALAS|ALTER ANY ASSEMBLY|DATABASE|  
|ALCF|ALTER ANY CERTIFICATE|DATABASE|  
|ALDS|ALTER ANY DATASPACE|DATABASE|  
|ALED|ALTER ANY DATABASE EVENT NOTIFICATION|DATABASE|  
|ALFT|ALTER ANY FULLTEXT CATALOG|DATABASE|  
|ALMT|ALTER ANY MESSAGE TYPE|DATABASE|  
|ALRL|ALTER ANY ROLE|DATABASE|  
|ALRT|ALTER ANY ROUTE|DATABASE|  
|ALSB|ALTER ANY REMOTE SERVICE BINDING|DATABASE|  
|ALSC|ALTER ANY CONTRACT|DATABASE|  
|ALSK|ALTER ANY SYMMETRIC KEY|DATABASE|  
|ALSM|ALTER ANY SCHEMA|DATABASE|  
|ALSV|ALTER ANY SERVICE|DATABASE|  
|ALTG|ALTER ANY DATABASE DDL TRIGGER|DATABASE|  
|ALUS|ALTER ANY USER|DATABASE|  
|AUTH|AUTHENTICATE|DATABASE|  
|BADB|BACKUP DATABASE|DATABASE|  
|BALO|BACKUP LOG|DATABASE|  
|CL|CONTROL|APPLICATION ROLE, ASSEMBLY, ASYMMETRIC KEY, CERTIFICATE, CONTRACT, DATABASE, FULLTEXT CATALOG, MESSAGE TYPE, OBJECT, REMOTE SERVICE BINDING, ROLE, ROUTE, SCHEMA, SERVICE, SYMMETRIC KEY, TYPE, USER, XML SCHEMA COLLECTION|  
|CO|CONNECT|DATABASE|  
|CORP|CONNECT REPLICATION|DATABASE|  
|CP|CHECKPOINT|DATABASE|  
|CRAG|CREATE AGGREGATE|DATABASE|  
|CRAK|CREATE ASYMMETRIC KEY|DATABASE|  
|CRAS|CREATE ASSEMBLY|DATABASE|  
|CRCF|CREATE CERTIFICATE|DATABASE|  
|CRDB|CREATE DATABASE|DATABASE|  
|CRDF|CREATE DEFAULT|DATABASE|  
|CRED|CREATE DATABASE DDL EVENT NOTIFICATION|DATABASE|  
|CRFN|CREATE FUNCTION|DATABASE|  
|CRFT|CREATE FULLTEXT CATALOG|DATABASE|  
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
|CRSO|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions.<br /><br /> CREATE SEQUENCE|DATABASE|  
|CRSV|CREATE SERVICE|DATABASE|  
|CRTB|CREATE TABLE|DATABASE|  
|CRTY|CREATE TYPE|DATABASE|  
|CRVW|CREATE VIEW|DATABASE|  
|CRXS|**Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.<br /><br /> CREATE XML SCHEMA COLLECTION|DATABASE|  
|DABO |ADMINISTER DATABASE BULK OPERATIONS | DATABASE |
|DL|DELETE|DATABASE, OBJECT, SCHEMA|  
|EAES |EXECUTE ANY EXTERNAL SCRIPT |DATABASE |
|EX|EXECUTE|ASSEMBLY, DATABASE, OBJECT, SCHEMA, TYPE, XML SCHEMA COLLECTION|  
|IM|IMPERSONATE|USER|  
|IN|INSERT|DATABASE, OBJECT, SCHEMA|  
|RC|RECEIVE|OBJECT|  
|RF|REFERENCES|ASSEMBLY, ASYMMETRIC KEY, CERTIFICATE, CONTRACT, DATABASE, FULLTEXT CATALOG, MESSAGE TYPE, OBJECT, SCHEMA, SYMMETRIC KEY, TYPE, XML SCHEMA COLLECTION|  
|SL|SELECT|DATABASE, OBJECT, SCHEMA|  
|SN|SEND|SERVICE|  
|SPLN|SHOWPLAN|DATABASE|  
|SUQN|SUBSCRIBE QUERY NOTIFICATIONS|DATABASE|  
|TO|TAKE OWNERSHIP|ASSEMBLY, ASYMMETRIC KEY, CERTIFICATE, CONTRACT, DATABASE, FULLTEXT CATALOG, MESSAGE TYPE, OBJECT, REMOTE SERVICE BINDING, ROLE, ROUTE, SCHEMA, SERVICE, SYMMETRIC KEY, TYPE, XML SCHEMA COLLECTION|  
|UP|UPDATE|DATABASE, OBJECT, SCHEMA|  
|VW|VIEW DEFINITION|APPLICATION ROLE, ASSEMBLY, ASYMMETRIC KEY, CERTIFICATE, CONTRACT, DATABASE, FULLTEXT CATALOG, MESSAGE TYPE, OBJECT, REMOTE SERVICE BINDING, ROLE, ROUTE, SCHEMA, SERVICE, SYMMETRIC KEY, TYPE, USER, XML SCHEMA COLLECTION|  
|VWCK |VIEW ANY COLUMN ENCRYPTION KEY DEFINITION|DATABASE |  
|VWCM |VIEW ANY COLUMN MASTER KEY DEFINITION|DATABASE |  
|VWCT|VIEW CHANGE TRACKING|TABLE, SCHEMA|  
|VWDS|VIEW DATABASE STATE|DATABASE|  
  
## Permissions

 Any user can see their own permissions. To see permissions for other users, requires VIEW DEFINITION, ALTER ANY USER, or any permission on a user. To see user-defined roles, requires ALTER ANY ROLE, or membership in the role (such as public).  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples

### <a id="a-listing-all-the-permissions-of-database-principals"></a> A. List all the permissions of database principals

 The following query lists the permissions explicitly granted or denied to database principals.  
  
> [!IMPORTANT]  
> The permissions of fixed database roles do not appear in `sys.database_permissions`. Therefore, database principals may have additional permissions not listed here.  
  
```sql
SELECT pr.principal_id
    ,pr.name
    ,pr.type_desc
    ,pr.authentication_type_desc
    ,pe.state_desc
    ,pe.permission_name  
FROM sys.database_principals AS pr  
INNER JOIN sys.database_permissions AS pe ON pe.grantee_principal_id = pr.principal_id;  
```  

### <a id="b-listing-permissions-on-schema-objects-within-a-database"></a> B. List permissions on schema objects within a database

 The following query joins [sys.database_principals](sys-database-principals-transact-sql.md) and `sys.database_permissions` to [sys.objects](sys-objects-transact-sql.md) and [sys.schemas](schemas-catalog-views-sys-schemas.md) to list permissions granted or denied to specific schema objects.  
  
```sql
SELECT pr.principal_id
    ,pr.name
    ,pr.type_desc
    ,pr.authentication_type_desc
    ,pe.state_desc
    ,pe.permission_name
    ,s.name + '.' + o.name AS ObjectName
FROM sys.database_principals AS pr
INNER JOIN sys.database_permissions AS pe ON pe.grantee_principal_id = pr.principal_id
INNER JOIN sys.objects AS o ON pe.major_id = o.object_id
INNER JOIN sys.schemas AS s ON o.schema_id = s.schema_id;
```  

### C. List permissions for a specific object

You can use the previous example to query permissions specific to a single database object.

For example, consider the following granular permissions granted to a database user `test` in the [sample database](../../samples/adventureworks-install-configure.md) [!INCLUDE [sssampledbdwobject-md](../../includes/sssampledbdwobject-md.md)]:

```sql
GRANT SELECT ON dbo.vAssocSeqOrders TO [test];
```

Find the granular permissions assigned to `dbo.vAssocSeqOrders`:

```sql
SELECT pr.principal_id
    ,pr.name
    ,pr.type_desc
    ,pr.authentication_type_desc
    ,pe.state_desc
    ,pe.permission_name
    ,s.name + '.' + o.name AS ObjectName
FROM sys.database_principals AS pr
INNER JOIN sys.database_permissions AS pe ON pe.grantee_principal_id = pr.principal_id
INNER JOIN sys.objects AS o ON pe.major_id = o.object_id
INNER JOIN sys.schemas AS s ON o.schema_id = s.schema_id
WHERE o.name = 'vAssocSeqOrders'
    AND s.name = 'dbo';
```

Returns the output:

```output
principal_id    name    type_desc    authentication_type_desc    state_desc    permission_name    ObjectName
5    test    SQL_USER    INSTANCE    GRANT    SELECT    dbo.vAssocSeqOrders
```

## See also

- [Securables](../../relational-databases/security/securables.md)
- [Permissions Hierarchy (Database Engine)](../../relational-databases/security/permissions-hierarchy-database-engine.md)
- [Security Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)
- [Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)

## Next steps

- [Grant a Permission to a Principal](../security/authentication-access/grant-a-permission-to-a-principal.md)
- [GRANT (Transact-SQL)](../../t-sql/statements/grant-transact-sql.md)