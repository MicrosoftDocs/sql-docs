---
title: "EditMode Property | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::EditMode"
helpviewer_keywords: 
  - "EditMode property"
ms.assetid: a1b04bb2-8c8b-47f9-8477-bfd0368b6f68
author: MightyPen
ms.author: genemi
manager: craigg
---
# EditMode Property
Indicates the editing status of the current record.  
  
## Return Value  
 Returns an [EditModeEnum](../../../ado/reference/ado-api/editmodeenum.md) value.  
  
## Remarks  
 ADO maintains an editing buffer associated with the current record. This property indicates whether changes have been made to this buffer, or whether a new record has been created. Use the **EditMode** property to determine the editing status of the current record. You can test for pending changes if an editing process has been interrupted and determine whether you need to use the [Update](../../../ado/reference/ado-api/update-method.md) or [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method.  
  
 In *immediate update mode* the **EditMode** property is reset to **adEditNone** after a successful call to the **Update** method is called. When a call to [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md) does not successfully delete the record or records in the data source (for example, because of referential integrity violations), the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) remains in edit mode (**EditMode** = **adEditInProgress**). Therefore, **CancelUpdate** must be called before moving off the current record (for example with [Move](../../../ado/reference/ado-api/move-method-ado.md), [NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md), or [Close](../../../ado/reference/ado-api/close-method-ado.md) ).  
  
 In *batch update mode* (in which the provider caches multiple changes and writes them to the underlying data source only when you call the [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) method), the value of the **EditMode** property is changed when the first operation is performed and it is not reset by a call to the **Update** method. Subsequent operations do not change the value of the **EditMode** property, even if different operations are performed. For example, if the first operation is to add a new record, and the second makes changes to an existing record, the property of **EditMode** will still be **adEditAdd**. The **EditMode** property is not reset to **adEditNone** until after the call to **UpdateBatch**. To determine what operations have been performed, set the [Filter](../../../ado/reference/ado-api/filter-property.md) property to [adFilterPending](../../../ado/reference/ado-api/filtergroupenum.md) so that only records with pending changes will be visible and examine the [Status](../../../ado/reference/ado-api/status-property-ado-recordset.md) property of each record to determine what changes have been made to the data.  
  
> [!NOTE]
>  **EditMode** can return a valid value only if there is a current record. **EditMode** will return an error if [BOF or EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) is true, or if the current record has been deleted.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [CursorType, LockType, and EditMode Properties Example (VB)](../../../ado/reference/ado-api/cursortype-locktype-and-editmode-properties-example-vb.md)   
 [CursorType, LockType, and EditMode Properties Example (VC++)](../../../ado/reference/ado-api/cursortype-locktype-and-editmode-properties-example-vc.md)   
 [AddNew Method (ADO)](../../../ado/reference/ado-api/addnew-method-ado.md)   
 [Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)   
 [CancelUpdate Method (ADO)](../../../ado/reference/ado-api/cancelupdate-method-ado.md)   
 [Update Method](../../../ado/reference/ado-api/update-method.md)
