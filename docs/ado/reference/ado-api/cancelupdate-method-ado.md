---
title: "CancelUpdate Method (ADO)"
description: "CancelUpdate Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::CancelUpdate"
helpviewer_keywords:
  - "CancelUpdate method [ADO]"
apitype: "COM"
---
# CancelUpdate Method (ADO)
Cancels any changes made to the current or new row of a [Recordset](./recordset-object-ado.md) object, or the [Fields](./fields-collection-ado.md) collection of a [Record](./record-object-ado.md) object, before calling the [Update](./update-method.md) method.  
  
## Syntax  
  
```  
  
recordset.CancelUpdaterecord.Fields.CancelUpdate  
```  
  
## Remarks  
  
## Recordset  
 Use the **CancelUpdate** method to cancel any changes made to the current row or to discard a newly added row. You cannot cancel changes to the current row or a new row after you call the **Update** method, unless the changes are either part of a transaction that you can roll back with the [RollbackTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md) method, or part of a batch update. In the case of a batch update, you can cancel the **Update** with the **CancelUpdate** or [CancelBatch](./cancelbatch-method-ado.md) method.  
  
 If you are adding a new row when you call the **CancelUpdate** method, the current row becomes the row that was current before the [AddNew](./addnew-method-ado.md) call.  
  
 If you are in edit mode and want to move off the current record (for example, by using the [Move](./move-method-ado.md), [NextRecordset](./nextrecordset-method-ado.md), or [Close](./close-method-ado.md) methods), you can use **CancelUpdate** to cancel any pending changes. You may need to do this if the update cannot successfully be posted to the data source. For example, an attempted delete that fails due to referential integrity violations will leave the **Recordset** in edit mode after a call to [Delete](./delete-method-ado-recordset.md).  
  
## Record  
 The **CancelUpdate** method cancels any pending insertions or deletions of [Field](./field-object.md) objects, and cancels pending updates of existing fields and restores them to their original values. The [Status](./status-property-ado-recordset.md) property of all fields in the **Fields** collection is set to **adFieldOK**.  
  
## Applies To  

:::row:::
    :::column:::
        [Fields Collection (ADO)](./fields-collection-ado.md)  
    :::column-end:::
    :::column:::
        [Recordset Object (ADO)](./recordset-object-ado.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Update and CancelUpdate Methods Example (VB)](./update-and-cancelupdate-methods-example-vb.md)   
 [Update and CancelUpdate Methods Example (VC++)](./update-and-cancelupdate-methods-example-vc.md)   
 [AddNew Method (ADO)](./addnew-method-ado.md)   
 [Cancel Method (ADO)](./cancel-method-ado.md)   
 [Cancel Method (RDS)](../rds-api/cancel-method-rds.md)   
 [CancelBatch Method (ADO)](./cancelbatch-method-ado.md)   
 [CancelUpdate Method (RDS)](../rds-api/cancelupdate-method-rds.md)   
 [EditMode Property](./editmode-property.md)   
 [Update Method](./update-method.md)