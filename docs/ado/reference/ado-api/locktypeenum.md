---
title: "LockTypeEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "LockTypeEnum"
helpviewer_keywords: 
  - "LockTypeEnum enumeration [ADO]"
ms.assetid: d2894eaf-4450-4ace-aa51-c8b875fd3010
author: MightyPen
ms.author: genemi
manager: craigg
---
# LockTypeEnum
Specifies the type of lock placed on records during editing.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adLockBatchOptimistic**|4|Indicates optimistic batch updates. Required for batch update mode.|  
|**adLockOptimistic**|3|Indicates optimistic locking, record by record. The provider uses optimistic locking, locking records only when you call the [Update](../../../ado/reference/ado-api/update-method.md) method.|  
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
  
|||  
|-|-|  
|[Clone Method (ADO)](../../../ado/reference/ado-api/clone-method-ado.md)|[LockType Property (ADO)](../../../ado/reference/ado-api/locktype-property-ado.md)|  
|[Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)|[WillExecute Event (ADO)](../../../ado/reference/ado-api/willexecute-event-ado.md)|
