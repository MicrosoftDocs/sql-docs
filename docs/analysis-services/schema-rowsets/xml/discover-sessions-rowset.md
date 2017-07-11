---
title: "DISCOVER_SESSIONS Rowset | Microsoft Docs"
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
  - "DISCOVER_SESSIONS rowset"
ms.assetid: 47a79542-3142-4e62-a66f-6c4dbfe0f5c0
caps.latest.revision: 18
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_SESSIONS Rowset
  Provides resource usage and activity information about the currently opened sessions on the server.  
  
## Rowset Columns  
 The **DISCOVER_SESSIONS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**SESSION_COMMAND_COUNT**|**DBTYPE_I4**||The number of commands that started execution since the beginning of the session.|  
|**SESSION_CONNECTION_ID**|**DBTYPE_I4**||The connection identifier for the session.|  
|**SESSION_CPU_TIME_MS**|**DBTYPE_UI8**||The CPU time, in milliseconds, consumed by all requests since the beginning of the session.|  
|**SESSION_CURRENT_DATABASE**|**DBTYPE_WSTR**||The name of the database that is being used by the current command execution, or the database that was used by the last command executed.|  
|**SESSION_ELAPSED_TIME_MS**|**DBTYPE_UI8**||Elapsed time, in milliseconds, since the start of the session.|  
|**SESSION_ID**|**DBTYPE_WSTR**||The session unique identifier, as a GUID.|  
|**SESSION_IDLE_TIME_MS**|**DBTYPE_UI8**||The idle time, in milliseconds, since the start of the session.|  
|**SESSION_LAST_COMMAND**|**DBTYPE_WSTR**||The text of the current command executing or the last command executed.|  
|**SESSION_LAST_COMMAND_CPU_TIME_MS**|**DBTYPE_UI8**||The CPU time, in milliseconds, consumed by **SESSION_LAST_COMMAND**.|  
|**SESSION_LAST_COMMAND_ELAPSED_TIME_MS**|**DBTYPE_UI8**||The elapsed time, in milliseconds, since the start of **SESSION_LAST_COMMAND**.|  
|**SESSION_LAST_COMMAND_END_TIME**|**DBTYPE_DBTIMESTAMP**||The UTC server time at the moment the last command finished executing.|  
|**SESSION_LAST_COMMAND_START_TIME**|**DBTYPE_DBTIMESTAMP**||The UTC server time at the moment the last command started executing.|  
|**SESSION_PROPERTIES**|**DBTYPE_WSTR**||Reserved for future use.|  
|**SESSION_READ_KB**|**DBTYPE_UI8**||The accumulated value of data read from disk, in kilobytes, since the start of the session.|  
|**SESSION_READS**|**DBTYPE_UI8**||The accumulated number of disk reads since the start of the session.|  
|**SESSION_SPID**|**DBTYPE_I4**||The session ID.|  
|**SESSION_START_TIME**|**DBTYPE_DBTIMESTAMP**||The date and time the session started as UTC time to the server.|  
|**SESSION_STATUS**|**DBTYPE_I4**||The activity status of the session.<br /><br /> 0 means "Idle": No current activity is ongoing.<br /><br /> 1 means "Active": The session is executing some requested task.<br /><br /> 2 means is "Blocked": The session is waiting for some resource to continue executing the suspended task.<br /><br /> 3 means "Cancelled": The session has been tagged as cancelled.|  
|**SESSION_USED_MEMORY**|**DBTYPE_I4**||The current size of memory used by the session in kilobytes. The value reported is RAM usage by SPID, with no distinction between shrinkable and non-shrinkable memory. Unlike other DMVS that report on memory usage, DISCOVER_SESSIONS does not break out memory usage by category.<br /><br /> Note that SESSION_USED_MEMORY tends to under-report actual memory usage because it excludes objects shared across multiple sessions.  Only those objects that are unique to the session are represented in the memory calculation.|  
|**SESSION_USER_NAME**|**DBTYPE_WSTR**||The session user name.|  
|**SESSION_WRITE_KB**|**DBTYPE_UI8**||The accumulated value of data written to disk, in kilobytes, since the start of the session.|  
|**SESSION_WRITES**|**DBTYPE_UI8**||The accumulated number of disk writes since the start of the session.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_SESSIONS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|SESSION_ID|DBTYPE_WSTR|Optional.|  
|SESSION_SPID|DBTYPE_I4|Optional.|  
|SESSION_CONNECTION_ID|DBTYPE_I4|Optional.|  
|SESSION_USER_NAME|DBTYPE_WSTR|Optional.|  
|SESSION_CURRENT_DATABASE|DBTYPE_WSTR|Optional.|  
|SESSION_ELAPSED_TIME_MS|DBTYPE_UI8|Optional.|  
|SESSION_CPU_TIME_MS|DBTYPE_UI8|Optional.|  
|SESSION_IDLE_TIME_MS|DBTYPE_UI8|Optional.|  
|SESSION_STATUS|DBTYPE_I4|Optional.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  