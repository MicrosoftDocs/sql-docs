---
title: "sys.dm_audit_class_type_map (Transact-SQL)"
description: sys.dm_audit_class_type_map (Transact-SQL)
author: sravanisaluru
ms.author: srsaluru
ms.date: "03/23/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_audit_class_type_map"
  - "sys.dm_audit_class_type_map_TSQL"
  - "dm_audit_class_type_map"
  - "dm_audit_class_type_map_TSQL"
helpviewer_keywords:
  - "sys.dm_audit_class_type_map dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: e10b5431-1bb0-47ca-8fd0-c04bd73a4410
monikerRange: "=azuresqldb-current||>=sql-server-2016||=azuresqldb-mi-current"
---
# sys.dm_audit_class_type_map (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a table that maps the class_type field in the audit log to the class_desc field in sys.dm_audit_actions. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**class_type**|**char(2)**|The class type of the entity that was audited. Maps to the class_type written to the audit log and returned by the **get_audit_file()** function. Is not nullable.|  
|**class_type_desc**|**nvarchar(120)**|The name for the auditable entity. Is not nullable.|  
|**securable_class_desc**|**nvarchar(120)**|The securable object that maps to the class_type being audited. Is NULL if the class_type does not map to a securable object. Can be related to class_desc in sys.dm_audit_actions.|  
  
## Permissions  
This view is visible to the public.  
  
## See Also  
 [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md)   
 [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-transact-sql.md)   
 [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)   
 [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)   
 [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)   
 [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)   
 [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-audit-specification-transact-sql.md)   
 [ALTER DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)   
 [DROP DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)   
 [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)   
 [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
 [sys.server_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)   
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
 [sys.dm_audit_class_type_map](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)   
 [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
  
