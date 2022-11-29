---
title: "CancelBatch Method (ADO)"
description: "CancelBatch Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::raw_CancelBatch"
  - "Recordset15::CancelBatch"
helpviewer_keywords:
  - "CancelBatch method [ADO]"
apitype: "COM"
---
# CancelBatch Method (ADO)
Cancels a pending batch update.  
  
## Syntax  
  
```  
  
recordset.CancelBatchAffectRecords  
```  
  
#### Parameters  
 *AffectRecords*  
 Optional. An [AffectEnum](./affectenum.md) value that indicates how many records the **CancelBatch** method will affect.  
  
## Remarks  
 Use the **CancelBatch** method to cancel any pending updates in a [Recordset](./recordset-object-ado.md) in batch update mode. If the **Recordset** is in immediate update mode, calling **CancelBatch** without **adAffectCurrent** generates an error.  
  
 If you are editing the current record or are adding a new record when you call **CancelBatch**, ADO first calls the [CancelUpdate](./cancelupdate-method-ado.md) method to cancel any cached changes. After that, all pending changes in the **Recordset** are canceled.  
  
 The current record may be indeterminable after a **CancelBatch** call, especially if you were in the process of adding a new record. For this reason, it is prudent to set the current record position to a known location in the **Recordset** after the **CancelBatch** call. For example, call the [MoveFirst](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md) method.  
  
 If the attempt to cancel the pending updates fails because of a conflict with the underlying data (for example, if a record has been deleted by another user), the provider returns warnings to the [Errors](./errors-collection-ado.md) collection but does not halt program execution. A run-time error occurs only if there are conflicts on all the requested records. Use the [Filter](./filter-property.md) property (**adFilterAffectedRecords**) and the [Status](./status-property-ado-recordset.md) property to locate records with conflicts.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [UpdateBatch and CancelBatch Methods Example (VB)](./updatebatch-and-cancelbatch-methods-example-vb.md)   
 [UpdateBatch and CancelBatch Methods Example (VC++)](./updatebatch-and-cancelbatch-methods-example-vc.md)   
 [Cancel Method (ADO)](./cancel-method-ado.md)   
 [Cancel Method (RDS)](../rds-api/cancel-method-rds.md)   
 [CancelUpdate Method (ADO)](./cancelupdate-method-ado.md)   
 [CancelUpdate Method (RDS)](../rds-api/cancelupdate-method-rds.md)   
 [Clear Method (ADO)](./clear-method-ado.md)   
 [LockType Property (ADO)](./locktype-property-ado.md)   
 [UpdateBatch Method](./updatebatch-method.md)