---
title: "UpdateBatch Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::UpdateBatch"
  - "Recordset15::raw_UpdateBatch"
helpviewer_keywords: 
  - "UpdateBatch method [ADO]"
ms.assetid: 23f9314c-b027-4a51-aeae-50caa2977740
author: MightyPen
ms.author: genemi
manager: craigg
---
# UpdateBatch Method
Writes all pending batch updates to disk.  
  
## Syntax  
  
```  
  
recordset.UpdateBatch AffectRecords, PreserveStatus  
```  
  
#### Parameters  
 *AffectRecords*  
 Optional. An [AffectEnum](../../../ado/reference/ado-api/affectenum.md) value that indicates how many records the **UpdateBatch** method will affect.  
  
 *PreserveStatus*  
 Optional. A **Boolean** value that specifies whether or not local changes, as indicated by the [Status](../../../ado/reference/ado-api/status-property-ado-recordset.md) property, should be committed. If this value is set to **True**, the **Status** property of each record remains unchanged after the update is completed.  
  
## Remarks  
 Use the **UpdateBatch** method when modifying a **Recordset** object in batch update mode to transmit all changes made in a **Recordset** object to the underlying database.  
  
 If the **Recordset** object supports batch updating, you can cache multiple changes to one or more records locally until you call the **UpdateBatch** method. If you are editing the current record or adding a new record when you call the **UpdateBatch** method, ADO will automatically call the [Update](../../../ado/reference/ado-api/update-method.md) method to save any pending changes to the current record before transmitting the batched changes to the provider. You should use batch updating with either a keyset or static cursor only.  
  
> [!NOTE]
>  Specifying **adAffectGroup** as the value for this parameter will result in an error when there are no visible records in the current **Recordset** (such as a filter for which no records match).  
  
 If the attempt to transmit changes fails for any or all records because of a conflict with the underlying data (for example, a record has already been deleted by another user), the provider returns warnings to the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection and a run-time error occurs. Use the [Filter](../../../ado/reference/ado-api/filter-property.md) property (**adFilterAffectedRecords**) and the [Status](../../../ado/reference/ado-api/status-property-ado-recordset.md) property to locate records with conflicts.  
  
 To cancel all pending batch updates, use the [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) method.  
  
 If the [Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) and [Update Resync](../../../ado/reference/ado-api/update-resync-property-dynamic-ado.md) dynamic properties are set, and the **Recordset** is the result of executing a JOIN operation on multiple tables, then the execution of the **UpdateBatch** method is implicitly followed by the [Resync](../../../ado/reference/ado-api/resync-method.md) method, depending on the settings of the [Update Resync](../../../ado/reference/ado-api/update-resync-property-dynamic-ado.md) property.  
  
 The order in which the individual updates of a batch are performed on the data source is not necessarily the same as the order in which they were performed on the local **Recordset**. Update order is dependent upon the provider. Take this into account when coding updates that are related to one another, such as foreign key constraints on an insert or update.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [UpdateBatch and CancelBatch Methods Example (VB)](../../../ado/reference/ado-api/updatebatch-and-cancelbatch-methods-example-vb.md)   
 [UpdateBatch and CancelBatch Methods Example (VC++)](../../../ado/reference/ado-api/updatebatch-and-cancelbatch-methods-example-vc.md)   
 [CancelBatch Method (ADO)](../../../ado/reference/ado-api/cancelbatch-method-ado.md)   
 [Clear Method (ADO)](../../../ado/reference/ado-api/clear-method-ado.md)   
 [LockType Property (ADO)](../../../ado/reference/ado-api/locktype-property-ado.md)   
 [Update Method](../../../ado/reference/ado-api/update-method.md)
