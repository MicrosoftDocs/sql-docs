---
title: "TABLE_PRIVILEGES (Transact-SQL)"
description: "TABLE_PRIVILEGES (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "TABLE_PRIVILEGES_TSQL"
  - "TABLE_PRIVILEGES"
helpviewer_keywords:
  - "INFORMATION_SCHEMA.TABLE_PRIVILEGES view"
  - "TABLE_PRIVILEGES view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# TABLE_PRIVILEGES (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns one row for each table privilege that is granted to or granted by the current user in the current database.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**GRANTOR**|**nvarchar(**128**)**|Privilege grantor.|  
|**GRANTEE**|**nvarchar(**128**)**|Privilege grantee.|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the table.<br /><br /> **Important:** The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**TABLE_NAME**|**sysname**|Table name.|  
|**PRIVILEGE_TYPE**|**varchar(**10**)**|Type of privilege.|  
|**IS_GRANTABLE**|**varchar(**3**)**|Specifies whether the grantee can grant permissions to others.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.database_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)   
 [sys.server_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)  
  
