---
title: "sys.database_audit_specifications (Transact-SQL)"
description: sys.database_audit_specifications (Transact-SQL)
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: rwestMSFT
ms.date: 04/04/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "database_audit_specifications_TSQL"
  - "sys.database_audit_specifications_TSQL"
  - "sys.database_audit_specifications"
  - "database_audit_specifications"
helpviewer_keywords:
  - "sys.database_audit_specifications catalog view"
dev_langs:
  - "TSQL"
ms.assetid: bf80e5c6-0588-4eb7-86ff-aa7c73461335
---
# sys.database_audit_specifications (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article contains information about the database audit specifications in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] audit on a server instance. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
|Column name|Data type|Description|
|---|---|---|
|Name|**sysname**|Name of the auditing specification.|
|database_specification_id|**int**|ID of the database specification.|
|create_date|**datetime**|Date the audit specification was created.|
|modify_date|**datetime**|Date the audit specification was last modified.|
|audit_guid|**uniqueidentifer**|GUID for the audit that contains this specification. Used during enumeration of member database audit specifications during database attach/startup.|
|is_state_enabled|**bit**|Audit specification state:<br /><br />0 - DISABLED<br /><br />1 - ENABLED|

## Remarks

If a database is in ready-only mode, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit feature can't add Database Audit Specifications.  

## Permissions

Principals with the **ALTER ANY DATABASE AUDIT** or **VIEW DEFINITION** permissions, the dbo role, and members of the db_owners fixed database role have access to this catalog view. In addition, the principal must not be denied **VIEW DEFINITION** permission.  

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## See also

- [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)
- [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)
- [sys.dm_audit_class_type_map &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
