---
title: "CancelBatch Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::raw_CancelBatch"
  - "Recordset15::CancelBatch"
helpviewer_keywords: 
  - "CancelBatch method [ADO]"
ms.assetid: dbdc2574-e44e-4d95-b03d-4a5d9e9adf3c
author: MightyPen
ms.author: genemi
manager: craigg
---
# CancelBatch Method (ADO)
Cancels a pending batch update.  
  
## Syntax  
  
```  
  
recordset.CancelBatchAffectRecords  
```  
  
#### Parameters  
 *AffectRecords*  
 Optional. An [AffectEnum](../../../ado/reference/ado-api/affectenum.md) value that indicates how many records the **CancelBatch** method will affect.  
  
## Remarks  
 Use the **CancelBatch** method to cancel any pending updates in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) in batch update mode. If the **Recordset** is in immediate update mode, calling **CancelBatch** without **adAffectCurrent** generates an error.  
  
 If you are editing the current record or are adding a new record when you call **CancelBatch**, ADO first calls the [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method to cancel any cached changes. After that, all pending changes in the **Recordset** are canceled.  
  
 The current record may be indeterminable after a **CancelBatch** call, especially if you were in the process of adding a new record. For this reason, it is prudent to set the current record position to a known location in the **Recordset** after the **CancelBatch** call. For example, call the [MoveFirst](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md) method.  
  
 If the attempt to cancel the pending updates fails because of a conflict with the underlying data (for example, if a record has been deleted by another user), the provider returns warnings to the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection but does not halt program execution. A run-time error occurs only if there are conflicts on all the requested records. Use the [Filter](../../../ado/reference/ado-api/filter-property.md) property (**adFilterAffectedRecords**) and the [Status](../../../ado/reference/ado-api/status-property-ado-recordset.md) property to locate records with conflicts.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [UpdateBatch and CancelBatch Methods Example (VB)](../../../ado/reference/ado-api/updatebatch-and-cancelbatch-methods-example-vb.md)   
 [UpdateBatch and CancelBatch Methods Example (VC++)](../../../ado/reference/ado-api/updatebatch-and-cancelbatch-methods-example-vc.md)   
 [Cancel Method (ADO)](../../../ado/reference/ado-api/cancel-method-ado.md)   
 [Cancel Method (RDS)](../../../ado/reference/rds-api/cancel-method-rds.md)   
 [CancelUpdate Method (ADO)](../../../ado/reference/ado-api/cancelupdate-method-ado.md)   
 [CancelUpdate Method (RDS)](../../../ado/reference/rds-api/cancelupdate-method-rds.md)   
 [Clear Method (ADO)](../../../ado/reference/ado-api/clear-method-ado.md)   
 [LockType Property (ADO)](../../../ado/reference/ado-api/locktype-property-ado.md)   
 [UpdateBatch Method](../../../ado/reference/ado-api/updatebatch-method.md)
