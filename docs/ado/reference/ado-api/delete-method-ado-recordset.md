---
title: "Delete Method (ADO Recordset)"
description: "Delete Method (ADO Recordset)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::raw_Delete"
  - "Recordset15::Delete"
helpviewer_keywords:
  - "Delete method [ADO]"
apitype: "COM"
---
# Delete Method (ADO Recordset)
Deletes the current record or a group of records.  
  
## Syntax  
  
```  
  
recordset.Delete AffectRecords  
```  
  
#### Parameters  
 *AffectRecords*  
 An [AffectEnum](../../../ado/reference/ado-api/affectenum.md) value that determines how many records the **Delete** method will affect. The default value is **adAffectCurrent**.  
  
> [!NOTE]
>  **adAffectAll** and **adAffectAllChapters** are not valid arguments to **Delete**.  
  
## Remarks  
 Using the **Delete** method marks the current record or a group of records in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object for deletion. If the **Recordset** object doesn't allow record deletion, an error occurs. If you are in immediate update mode, deletions occur in the database immediately. If a record cannot be successfully deleted (due to database integrity violations, for example), the record will remain in edit mode after the call to [Update](../../../ado/reference/ado-api/update-method.md). This means that you must cancel the update with [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) before moving off the current record (for example, with [Close](../../../ado/reference/ado-api/close-method-ado.md), [Move](../../../ado/reference/ado-api/move-method-ado.md), or [NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md)).  
  
 If you are in batch update mode, the records are marked for deletion from the cache and the actual deletion happens when you call the [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) method. Use the [Filter](../../../ado/reference/ado-api/filter-property.md) property to view the deleted records.  
  
 Retrieving field values from the deleted record generates an error. After deleting the current record, the deleted record remains current until you move to a different record. Once you move away from the deleted record, it is no longer accessible.  
  
 If you nest deletions in a transaction, you can recover deleted records with the [RollbackTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) method. If you are in batch update mode, you can cancel a pending deletion or group of pending deletions with the [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) method.  
  
 If the attempt to delete records fails because of a conflict with the underlying data (for example, a record has already been deleted by another user), the provider returns warnings to the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection but does not halt program execution. A run-time error occurs only if there are conflicts on all the requested records.  
  
 If the [Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) dynamic property is set, and the **Recordset** is the result of executing a JOIN operation on multiple tables, then the **Delete** method will only delete rows from the table named in the [Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) property.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Delete Method Example (VB)](../../../ado/reference/ado-api/delete-method-example-vb.md)   
 [Delete Method Example (VBScript)](../../../ado/reference/ado-api/delete-method-example-vbscript.md)   
 [Delete Method Example (VC++)](../../../ado/reference/ado-api/delete-method-example-vc.md)   
 [Delete Method (ADO Fields Collection)](../../../ado/reference/ado-api/delete-method-ado-fields-collection.md)   
 [Delete Method (ADO Parameters Collection)](../../../ado/reference/ado-api/delete-method-ado-parameters-collection.md)   
 [DeleteRecord Method (ADO)](../../../ado/reference/ado-api/deleterecord-method-ado.md)
