---
title: "RecordStatusEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "RecordStatusEnum"
helpviewer_keywords: 
  - "RecordStatusEnum enumeration [ADO]"
ms.assetid: 506fdd70-4452-4e83-95d5-c94311988dfa
author: MightyPen
ms.author: genemi
manager: craigg
---
# RecordStatusEnum
Specifies the [status](../../../ado/reference/ado-api/status-property-ado-recordset.md) of a record with regard to batch updates and other bulk operations.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adRecCanceled**|0x100|Indicates that the record was not saved because the operation was canceled.|  
|**adRecCantRelease**|0x400|Indicates that the new record was not saved because the existing record was locked.|  
|**adRecConcurrencyViolation**|0x800|Indicates that the record was not saved because optimistic concurrency was in use.|  
|**adRecDBDeleted**|0x40000|Indicates that the record has already been deleted from the data source.|  
|**adRecDeleted**|0x4|Indicates that the record was deleted.|  
|**adRecIntegrityViolation**|0x1000|Indicates that the record was not saved because the user violated integrity constraints.|  
|**adRecInvalid**|0x10|Indicates that the record was not saved because its bookmark is invalid.|  
|**adRecMaxChangesExceeded**|0x2000|Indicates that the record was not saved because there were too many pending changes.|  
|**adRecModified**|0x2|Indicates that the record was modified.|  
|**adRecMultipleChanges**|0x40|Indicates that the record was not saved because it would have affected multiple records.|  
|**adRecNew**|0x1|Indicates that the record is new.|  
|**adRecObjectOpen**|0x4000|Indicates that the record was not saved because of a conflict with an open storage object.|  
|**adRecOK**|0|Indicates that the record was successfully updated.|  
|**adRecOutOfMemory**|0x8000|Indicates that the record was not saved because the computer has run out of memory.|  
|**adRecPendingChanges**|0x80|Indicates that the record was not saved because it refers to a pending insert.|  
|**adRecPermissionDenied**|0x10000|Indicates that the record was not saved because the user has insufficient permissions.|  
|**adRecSchemaViolation**|0x20000|Indicates that the record was not saved because it violates the structure of the underlying database.|  
|**adRecUnmodified**|0x8|Indicates that the record was not modified.|  
  
## ADO/WFC Equivalent  
 AdoEnums.RecordStatus.  
  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.RecordStatus.CANCELED|  
|AdoEnums.RecordStatus.CANTRELEASE|  
|AdoEnums.RecordStatus.CONCURRENCYVIOLATION|  
|AdoEnums.RecordStatus.DBDELETED|  
|AdoEnums.RecordStatus.DELETED|  
|AdoEnums.RecordStatus.INTEGRITYVIOLATION|  
|AdoEnums.RecordStatus.INVALID|  
|AdoEnums.RecordStatus.MAXCHANGESEXCEEDED|  
|AdoEnums.RecordStatus.MODIFIED|  
|AdoEnums.RecordStatus.MULTIPLECHANGES|  
|AdoEnums.RecordStatus.NEW|  
|AdoEnums.RecordStatus.OBJECTOPEN|  
|AdoEnums.RecordStatus.OK|  
|AdoEnums.RecordStatus.OUTOFMEMORY|  
|AdoEnums.RecordStatus.PENDINGCHANGES|  
|AdoEnums.RecordStatus.PERMISSIONDENIED|  
|AdoEnums.RecordStatus.SCHEMAVIOLATION|  
|AdoEnums.RecordStatus.UNMODIFIED|  
  
## Applies To  
 [Status Property (ADO Recordset)](../../../ado/reference/ado-api/status-property-ado-recordset.md)
