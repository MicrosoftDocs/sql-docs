---
title: "FilterGroupEnum"
description: "FilterGroupEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "FilterGroupEnum"
helpviewer_keywords:
  - "FilterGroupEnum enumeration [ADO]"
apitype: "COM"
---
# FilterGroupEnum
Specifies the group of records to be filtered from a [Recordset](./recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adFilterAffectedRecords**|2|Filters for viewing only records affected by the last [Delete](./delete-method-ado-recordset.md), [Resync](./resync-method.md), [UpdateBatch](./updatebatch-method.md), or [CancelBatch](./cancelbatch-method-ado.md) call.|  
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
 [Filter Property](./filter-property.md)