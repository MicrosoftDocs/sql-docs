---
description: "fn_syscollector_get_execution_details (Transact-SQL)"
title: "fn_syscollector_get_execution_details (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "fn_syscollector_get_execution_details_TSQL"
  - "fn_syscollector_get_execution_details"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_syscollector_get_execution_details function"
ms.assetid: d59ddf0c-72c0-4c57-bc83-aef260e4e105
author: rwestMSFT
ms.author: randolphwest
---
# fn_syscollector_get_execution_details (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a portion of the [!INCLUDE[ssIS](../../includes/ssis-md.md)] log (sysssislog) matching the package_execution_id for the given package. The table contains one row for each logging entry that is generated at run time by packages or their tasks and containers.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_syscollector_get_execution_details ( log_id )  
```  
  
## Arguments  
 *log_id*  
 The local unique identifier for the execution log. *log_id* is **int**.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|id|**int**|The unique identifier of the logging entry.|  
|event|**sysname**|The name of the event that generated the logging entry.|  
|computer|**nvarchar**|The computer on which the package ran when the logging entry was generated.|  
|operator|**nvarchar**|The user name of the person or agent that ran the package that generated the logging entry.|  
|source|**nvarchar**|The name of the executable that generated the logging entry.|  
|sourceid|**uniqueidentifier**|The GUID of the executable that generated the logging entry.|  
|executionid|**uniqueidentifier**|The GUID of the execution instance of the executable that generated the logging entry.|  
|starttime|**datetime**|The time that the package began to run.|  
|endtime|**datetime**|The time that the package completed.|  
|datacode|**int**|An integer value that identifies the event associated with the log entry. "0" indicates that the event provided no identifier.|  
|databytes|**image**|A byte array that identifies a return value.|  
|message|**nvarchar**|A description of the event and the information associated with the event.|  
  
## Permissions  
 Requires SELECT permission for **dc_operator**.  
  
## See Also  
 [Enable Package Logging in SQL Server Data Tools](../../integration-services/performance/integration-services-ssis-logging.md#server_logging)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
