---
title: "DISCOVER_LOCKS Rowset | Microsoft Docs"
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
  - "DISCOVER_LOCKS rowset"
ms.assetid: dea48167-212c-40b7-a416-434042a1b697
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_LOCKS Rowset
  Provides information about the current standing locks on the server.  
  
## Rowset Columns  
 The **DISCOVER_LOCKS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**LOCK_CREATION_TIME**|**DBTYPE_DBTIMESTAMP**||The UTC server time at the moment lock was requested.|  
|**LOCK_GRANT_TIME**|**DBTYPE_DBTIMESTAMP**||The UTC server time at the moment lock was granted on the resource.|  
|**LOCK_ID**|**DBTYPE_GUID**||The unique identifier of the lock, as a GUID.|  
|**LOCK_OBJECT_ID**|**DBTYPE_WSTR**||The unique identifier of object being locked.|  
|**LOCK_STATUS**|**DBTYPE_I4**||The lock status.<br /><br /> 0 means "Waiting to lock the object."<br /><br /> 1 means "Lock Granted."|  
|**LOCK_TRANSACTION_ID**|**DBTYPE_GUID**||The unique identifier of the transaction, as a GUID.|  
|**LOCK_TYPE**|**DBTYPE_I4**||A bit mask of Lock Types; for more information, see the Remarks section of this topic.|  
|**SPID**|**DBTYPE_I4**||The session ID.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_LOCKS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|SPID|DBTYPE_I4|Optional.|  
|LOCK_TRANSACTION_ID|DBTYPE_GUID|Optional.|  
|LOCK_OBJECT_ID|DBTYPE_WSTR|Optional.|  
|LOCK_STATUS|DBTYPE_I4|Optional.|  
|LOCK_TYPE|DBTYPE_I4|Optional.|  
|LOCK_MIN_TOTAL_MS|DBTYPE_I8|Optional.|  
  
## Remarks  
  
## Lock Types  
  
|Lock Name|Value|Description|  
|---------------|-----------|-----------------|  
|LOCK_NONE|0x0000000|No lock.|  
|LOCK_SESSION_LOCK|0x0000001|Inactive session; does not interfere with other locks.|  
|LOCK_READ|0x0000002|Read lock during processing.|  
|LOCK_WRITE|0x0000004|Write lock during processing.|  
|LOCK_COMMIT_READ|0x0000008|Commit lock, shared.|  
|LOCK_COMMIT_WRITE|0x0000010|Commit lock, exclusive.|  
|LOCK_COMMIT_ABORTABLE|0x0000020|Abort at commit progress.|  
|LOCK_COMMIT_INPROGRESS|0x0000040|Commit in progress.|  
|LOCK_INVALID|0x0000080|Invalid lock.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  