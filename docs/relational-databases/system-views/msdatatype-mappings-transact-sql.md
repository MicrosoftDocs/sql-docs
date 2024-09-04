---
title: "MSdatatype_mappings (Transact-SQL)"
description: "MSdatatype_mappings (Transact-SQL)"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSdatatype_mappings"
  - "MSdatatype_mappings_TSQL"
helpviewer_keywords:
  - "MSdatatype_mappings view"
dev_langs:
  - "TSQL"
---
# MSdatatype_mappings (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSdatatype_mappings** view maps SQL Server data types to data types used by non-SQL Server database management systems (DBMS). This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**dbms_name**|**nvarchar(128)**|Is the name of the DBMS. Below are the possible values and their descriptions.<br /><br /> **MSSQLSERVER**: The destination is a SQL Server database.<br />**ORACLE**: The destination is an Oracle database.<br />**DB2**: The destination is an IBM Db2 database.<br />**SYBASE**: The destination is a Sybase database.|  
|**sql_type**|**nvarchar(128)**|Is the SQL Server data type.|  
|**dest_type**|**nvarchar(128)**|Is the name of the non-SQL Server data type.|  
|**dest_prec**|**bigint**|Is the precision of the non-SQL Server data type.|  
|**dest_create_params**|**int**|Internal use only.|  
|**dest_nullable**|**bit**|Is if the non-SQL Server data type supports a NULL value.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Specify Data Type Mappings for an Oracle Publisher](../../relational-databases/replication/publish/specify-data-type-mappings-for-an-oracle-publisher.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
