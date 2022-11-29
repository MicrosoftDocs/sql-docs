---
title: "MSdbms_datatype (Transact-SQL)"
description: MSdbms_datatype (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSdbms_datatype"
  - "MSdbms_datatype_TSQL"
helpviewer_keywords:
  - "MSdbms_datatype system table"
dev_langs:
  - "TSQL"
ms.assetid: 606168cc-79a8-442f-ab43-936f8f884d72
---
# MSdbms_datatype (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSdbms_datatype** table contains the complete list of native data types at each supported database management system (DBMS) used as either a Publisher or Subscriber in heterogeneous database replication. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**datatype_id**|**int**|Identifies each unique data type.|  
|**dbms_id**|**int**|Identifies the DBMS to which the type belongs.|  
|**type**|**sysname**|Data type name (native).|  
|**createparams**|**int**|Bitmap that describes what combination of length, precision, and scale is applicable to each data type, which includes:<br /><br /> **0x1** = PRECISION.<br /><br /> **0x2** = SCALE.<br /><br /> **0x4** = LENGTH.|  
  
## Remarks  
 This table contains entries for SQL Server data types because an instance of SQL Server can both subscribe to a non-SQL Server database and publish to a non-SQL Server Subscriber.  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Specify Data Type Mappings for an Oracle Publisher](../../relational-databases/replication/publish/specify-data-type-mappings-for-an-oracle-publisher.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
