---
title: "LockTypeEnum"
description: "LockTypeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "LockTypeEnum"
helpviewer_keywords:
  - "LockTypeEnum enumeration [ADO]"
apitype: "COM"
---
# LockTypeEnum
Specifies the type of lock placed on records during editing.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adLockBatchOptimistic**|4|Indicates optimistic batch updates. Required for batch update mode.|  
|**adLockOptimistic**|3|Indicates optimistic locking, record by record. The provider uses optimistic locking, locking records only when you call the [Update](./update-method.md) method.|  
|**adLockPessimistic**|2|Indicates pessimistic locking, record by record. The provider does what is necessary to ensure successful editing of the records, usually by locking records at the data source immediately after editing.|  
|**adLockReadOnly**|1|Indicates read-only records. You cannot alter the data.|  
|**adLockUnspecified**|-1|Does not specify a type of lock. For clones, the clone is created with the same lock type as the original.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.LockType.BATCHOPTIMISTIC|  
|AdoEnums.LockType.OPTIMISTIC|  
|AdoEnums.LockType.PESSIMISTIC|  
|AdoEnums.LockType.READONLY|  
|AdoEnums.LockType.UNSPECIFIED|  
  
## Applies To  

:::row:::
    :::column:::
        [Clone Method (ADO)](./clone-method-ado.md)  
        [LockType Property (ADO)](./locktype-property-ado.md)  
    :::column-end:::
    :::column:::
        [Open Method (ADO Recordset)](./open-method-ado-recordset.md)  
        [WillExecute Event (ADO)](./willexecute-event-ado.md)  
    :::column-end:::
:::row-end:::