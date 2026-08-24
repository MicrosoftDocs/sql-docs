---
title: "DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)"
description: DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)
author: sravanisaluru
ms.author: srsaluru
ms.date: "03/23/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_DATABASE_AUDIT_SPECIFICATION_TSQL"
  - "DROP DATABASE AUDIT SPECIFICATION"
helpviewer_keywords:
  - "database audit specification"
  - "DROP DATABASE AUDIT SPECIFICATION statement"
dev_langs:
  - "TSQL"
---
# DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

  Drops a database audit specification object using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit feature. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP DATABASE AUDIT SPECIFICATION audit_specification_name  
[ ; ]  
```  
  
## Arguments
 *audit_specification_name*  
 Name of an existing audit specification object.  
  
## Remarks  
 A DROP DATABASE AUDIT SPECIFICATION removes the metadata for the audit specification, but not the audit data collected before the DROP command was issued. You must set the state of a database audit specification to OFF using `ALTER DATABASE AUDIT SPECIFICATION` before it can be dropped.  
  
## Permissions  
 Users with the **ALTER ANY DATABASE AUDIT** permission can drop database audit specifications.  
  
## Examples  
  
### A. Dropping a Database Audit Specification  
 The following example drops an audit called `HIPAA_Audit_DB_Specification`.  
  
```sql  
DROP DATABASE AUDIT SPECIFICATION HIPAA_Audit_DB_Specification;  
GO  
```  
  
 For a full example of creating an audit, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
## Related content

- [CREATE SERVER AUDIT (Transact-SQL)](create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT (Transact-SQL)](alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT (Transact-SQL)](drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](alter-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](alter-authorization-transact-sql.md)
- [sys.fn_get_audit_file (Transact-SQL)](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-audit-actions-transact-sql.md)
- [sys.dm_audit_class_type_map (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-audit-class-type-map-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)
