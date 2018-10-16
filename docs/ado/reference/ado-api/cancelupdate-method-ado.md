---
title: "CancelUpdate Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::CancelUpdate"
helpviewer_keywords: 
  - "CancelUpdate method [ADO]"
ms.assetid: eaa856cc-c786-462e-890c-c896261b1741
author: MightyPen
ms.author: genemi
manager: craigg
---
# CancelUpdate Method (ADO)
Cancels any changes made to the current or new row of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object, or the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md) object, before calling the [Update](../../../ado/reference/ado-api/update-method.md) method.  
  
## Syntax  
  
```  
  
recordset.CancelUpdaterecord.Fields.CancelUpdate  
```  
  
## Remarks  
  
## Recordset  
 Use the **CancelUpdate** method to cancel any changes made to the current row or to discard a newly added row. You cannot cancel changes to the current row or a new row after you call the **Update** method, unless the changes are either part of a transaction that you can roll back with the [RollbackTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) method, or part of a batch update. In the case of a batch update, you can cancel the **Update** with the **CancelUpdate** or [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) method.  
  
 If you are adding a new row when you call the **CancelUpdate** method, the current row becomes the row that was current before the [AddNew](../../../ado/reference/ado-api/addnew-method-ado.md) call.  
  
 If you are in edit mode and want to move off the current record (for example, by using the [Move](../../../ado/reference/ado-api/move-method-ado.md), [NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md), or [Close](../../../ado/reference/ado-api/close-method-ado.md) methods), you can use **CancelUpdate** to cancel any pending changes. You may need to do this if the update cannot successfully be posted to the data source. For example, an attempted delete that fails due to referential integrity violations will leave the **Recordset** in edit mode after a call to [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md).  
  
## Record  
 The **CancelUpdate** method cancels any pending insertions or deletions of [Field](../../../ado/reference/ado-api/field-object.md) objects, and cancels pending updates of existing fields and restores them to their original values. The [Status](../../../ado/reference/ado-api/status-property-ado-recordset.md) property of all fields in the **Fields** collection is set to **adFieldOK**.  
  
## Applies To  
  
|||  
|-|-|  
|[Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)|[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)|  
  
## See Also  
 [Update and CancelUpdate Methods Example (VB)](../../../ado/reference/ado-api/update-and-cancelupdate-methods-example-vb.md)   
 [Update and CancelUpdate Methods Example (VC++)](../../../ado/reference/ado-api/update-and-cancelupdate-methods-example-vc.md)   
 [AddNew Method (ADO)](../../../ado/reference/ado-api/addnew-method-ado.md)   
 [Cancel Method (ADO)](../../../ado/reference/ado-api/cancel-method-ado.md)   
 [Cancel Method (RDS)](../../../ado/reference/rds-api/cancel-method-rds.md)   
 [CancelBatch Method (ADO)](../../../ado/reference/ado-api/cancelbatch-method-ado.md)   
 [CancelUpdate Method (RDS)](../../../ado/reference/rds-api/cancelupdate-method-rds.md)   
 [EditMode Property](../../../ado/reference/ado-api/editmode-property.md)   
 [Update Method](../../../ado/reference/ado-api/update-method.md)
