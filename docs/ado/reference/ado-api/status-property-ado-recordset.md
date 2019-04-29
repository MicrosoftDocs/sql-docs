---
title: "Status Property (ADO Recordset) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::GetStatus"
  - "Recordset15::Status"
helpviewer_keywords: 
  - "Status property [ADO Recordset]"
ms.assetid: 41d70d89-880f-4850-9d17-19d9790cc8eb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Status Property (ADO Recordset)
Indicates the status of the current record with respect to batch updates or other bulk operations.  
  
## Return Value  
 Returns a sum of one or more [RecordStatusEnum](../../../ado/reference/ado-api/recordstatusenum.md) values.  
  
## Remarks  
 Use the **Status** property to see what changes are pending for records modified during batch updating. You can also use the **Status** property to view the status of records that fail during bulk operations, such as when you call the [Resync](../../../ado/reference/ado-api/resync-method.md), [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md), or [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) methods on a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object, or set the [Filter](../../../ado/reference/ado-api/filter-property.md) property on a **Recordset** object to an array of bookmarks. With this property, you can determine how a given record failed and resolve it accordingly.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Status Property Example (Recordset) (VB)](../../../ado/reference/ado-api/status-property-example-recordset-vb.md)   
 [Status Property Example (VC++)](../../../ado/reference/ado-api/status-property-example-vc.md)   
