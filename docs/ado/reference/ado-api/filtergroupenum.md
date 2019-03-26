---
title: "FilterGroupEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "FilterGroupEnum"
helpviewer_keywords: 
  - "FilterGroupEnum enumeration [ADO]"
ms.assetid: b22e725e-84bd-4286-a070-290c278c3783
author: MightyPen
ms.author: genemi
manager: craigg
---
# FilterGroupEnum
Specifies the group of records to be filtered from a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adFilterAffectedRecords**|2|Filters for viewing only records affected by the last [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md), [Resync](../../../ado/reference/ado-api/resync-method.md), [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md), or [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) call.|  
|**adFilterConflictingRecords**|5|Filters for viewing the records that failed the last batch update.|  
|**adFilterFetchedRecords**|3|Filters for viewing the records in the current cache-that is, the results of the last call to retrieve records from the database.|  
|**adFilterNone**|0|Removes the current filter and restores all records for viewing.|  
|**adFilterPendingRecords**|1|Filters for viewing only records that have changed but have not yet been sent to the server. Applicable only for batch update mode.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.FilterGroup.AFFECTEDRECORDS|  
|AdoEnums.FilterGroup.CONFLICTINGRECORDS|  
|AdoEnums.FilterGroup.FETCHEDRECORDS|  
|AdoEnums.FilterGroup.NONE|  
|AdoEnums.FilterGroup.PENDINGRECORDS|  
  
## Applies To  
 [Filter Property](../../../ado/reference/ado-api/filter-property.md)
