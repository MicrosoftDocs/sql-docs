---
description: "sysdatatypemappings (Transact-SQL)"
title: "sysdatatypemappings (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
f1_keywords: 
  - "sysdatatypemappings"
  - "sysdatatypemappings_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysdatatypemappings view"
ms.assetid: 5dfafb70-3e3d-4465-b293-1acff1f855b6
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# sysdatatypemappings (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **sysdatatypemappings** view is used to show the mapping between SQL Server data types and data types of a non-SQL Server database management system (DBMS). This view is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**mapping_id**|**int**|The ID of the data type mapping.|  
|**source_dbms**|**sysname**|Indicates the name of the DBMS from which the data types are mapped, and can be one of the following values:<br /><br /> **MSSQLSERVER** = The source is a SQL Server database.<br /><br /> **ORACLE** = The source is an Oracle database.|  
|**source_version**|**sysname**|Indicates the product version of the source DBMS.|  
|**source_type**|**sysname**|Indicates the data type listed in the source DBMS.|  
|**source_length_min**|**bigint**|The minimum length of the data type at the source DBMS, where a value of NULL indicates that length is not used.|  
|**source_length_max**|**bigint**|The maximum length of the data type at the source DBMS, where a value of NULL indicates that length is not used.|  
|**source_precision_min**|**bigint**|The minimum precision of the data type at the source DBMS, where a value of NULL indicates that precision is not used.|  
|**source_precision_max**|**bigint**|The maximum precision of the data type at the source DBMS, where a value of NULL indicates that precision is not used.|  
|**source_scale_min**|**int**|The minimum scale of the data type at the source DBMS, where a value of NULL indicates that scale is not used.|  
|**source_scale_max**|**int**|The maximum scale of the data type at the source DBMS, where a value of NULL indicates that scale is not used.|  
|**source_nullable**|**bit**|Indicated if the destination data type supports null values.|  
|**source_createparams**|**int**|Internal use only.|  
|**destination_dbms**|**sysname**|Indicates the name of the destination DBMS, and can be one of the following values:<br /><br /> **MSSQLSERVER** = The destination is a SQL Server database.<br /><br /> **ORACLE** = The destination is an Oracle database.<br /><br /> **DB2** = The destination is an IBM DB2 database.<br /><br /> **SYBASE** = The destination is a Sybase database.|  
|**destination_version**|**sysname**|The product version of the destination DBMS.|  
|**destination_type**|**sysname**|The data type in the destination DBMS.|  
|**destination_length**|**bigint**|The length of the data type in the destination DBMS.|  
|**destination_precision**|**bigint**|The precision of the data type in the destination DBMS.|  
|**destination_scale**|**int**|The scale of the data type in the destination DBMS.|  
|**destination_nullable**|**bit**|Indicates if the data type in the destination DBMS supports a null value.|  
|**destination_createparams**|**int**|Internal use only.|  
|**dataloss**|**bit**|Indicates if data loss occurs when mapping between the data type at the source and destination DBMS.|  
|**is_default**|**bit**|Indicates if the data type mapping is used by default.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_helpdatatypemap &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdatatypemap-transact-sql.md)  
  
  
