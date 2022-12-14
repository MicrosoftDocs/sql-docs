---
title: "Status Property (ADO Recordset)"
description: "Status Property (ADO Recordset)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::GetStatus"
  - "Recordset15::Status"
helpviewer_keywords:
  - "Status property [ADO Recordset]"
apitype: "COM"
---
# Status Property (ADO Recordset)
Indicates the status of the current record with respect to batch updates or other bulk operations.  
  
## Return Value  
 Returns a sum of one or more [RecordStatusEnum](./recordstatusenum.md) values.  
  
## Remarks  
 Use the **Status** property to see what changes are pending for records modified during batch updating. You can also use the **Status** property to view the status of records that fail during bulk operations, such as when you call the [Resync](./resync-method.md), [UpdateBatch](./updatebatch-method.md), or [CancelBatch](./cancelbatch-method-ado.md) methods on a [Recordset](./recordset-object-ado.md) object, or set the [Filter](./filter-property.md) property on a **Recordset** object to an array of bookmarks. With this property, you can determine how a given record failed and resolve it accordingly.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [Status Property Example (Recordset) (VB)](./status-property-example-recordset-vb.md)   
 [Status Property Example (VC++)](./status-property-example-vc.md)