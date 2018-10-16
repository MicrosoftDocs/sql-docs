---
title: "DISCOVER_LOCKS Rowset | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: schema-rowsets
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DISCOVER_LOCKS Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
  
  
