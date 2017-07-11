---
title: "DISCOVER_JOBS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_JOBS rowset"
ms.assetid: b4d83bb6-aed3-4513-b516-cefadf95dad2
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_JOBS Rowset
  Provides information about the active jobs executing on the server. A job is a part of a command that executes a specific task on behalf of the command.  
  
## Rowset Columns  
 The **DISCOVER_JOBS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**JOB_CREATION_TIME**|**DBTYPE_DBTIMESTAMP**||The server UTC date and time when the job was created.|  
|**JOB_DESCRIPTION**|**DBTYPE_WSTR**||The job description assigned by server service.|  
|**JOB_EXECUTION_TIME_MS**|**DBTYPE_I8**||The time, in milliseconds, that the job is active.|  
|**JOB_ID**|**DBTYPE_I4**||The unique identifier of the job.|  
|**JOB_START_TIME**|**DBTYPE_DBTIMESTAMP**||The server UTC date and time when the job was started.|  
|**JOB_THREADPOOL_ID**|**DBTYPE_I4**||The thread pool from which the current job was started.|  
|**JOB_TOTAL_TIME_MS**|**DBTYPE_I8**||The time, in milliseconds, since the job started.|  
|**SPID**|**DBTYPE_I4**||The session ID.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_JOBS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|SPID|DBTYPE_I4|Optional.|  
|JOB_ID|DBTYPE_I4|Optional.|  
|JOB_DESCRIPTION|DBTYPE_WSTR|Optional.|  
|JOB_THREADPOOL_ID|DBTYPE_I4|Optional.|  
|JOB_MIN TOTAL_TIME_MS|DBTYPE_I8|Optional.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  