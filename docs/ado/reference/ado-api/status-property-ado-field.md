---
title: "Status Property (ADO Field) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Field::Status"
  - "Field::get_Status"
  - "Field::GetStatus"
helpviewer_keywords: 
  - "Status property [ADO Field]"
ms.assetid: 8cd1f7f4-0a3a-4f07-b8ba-6582e70140ad
author: MightyPen
ms.author: genemi
manager: craigg
---
# Status Property (ADO Field)
Indicates the status of a [Field](../../../ado/reference/ado-api/field-object.md) object.  
  
## Return Value  
 Returns a [FieldStatusEnum](../../../ado/reference/ado-api/fieldstatusenum.md) value. The default value is **adFieldOK**.  
  
## Remarks  
  
## Record Field Status  
 Changes to the value of a **Field** object in the Fields collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md) object are cached until the object's [Update](../../../ado/reference/ado-api/update-method.md) method is called. At that point, if the change to the Field's value caused an error, OLE DB raises the error **DB_E_ERRORSOCCURRED** (2147749409). The Status property of any of the **Field** objects in the **Fields** collection that caused the error will contain a value from the [FieldStatusEnum](../../../ado/reference/ado-api/fieldstatusenum.md) describing the cause of the problem.  
  
 To enhance performance, additions and deletions to the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collections of the **Record** object are cached until the **Update** method is called, and then the changes are made in a batch optimistic update. If the **Update** method is not called, the server is not updated. If any updates fail then an OLE DB provider error (DB_E_ERRORSOCCURRED) is returned and the **Status** property indicates the combined values of the operation and error status code. For example, **adFieldPendingInsert OR adFieldPermissionDenied**. The **Status** property for each **Field** can be used to determine why the **Field** was not added, modified, or deleted.  
  
 Many types of problems encountered when adding, modifying, or deleting a **Field** are reported through the **Status** property. For example, if the user deletes a **Field**, it is marked for deletion from the **Fields** collection. If the subsequent **Update** returns an error because the user tried to delete a **Field** for which they do not have permission, the **Field** will have a **Status** of **adFieldPermissionDenied OR adFieldPendingDelete**. Calling the [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method restores original values and sets the **Status** to **adFieldOK**.  
  
 Similarly, the **Update** method may return an error because a new **Field** was added and given an inappropriate value. In that case the new **Field** will be in the **Fields** collection and have a status of **adFieldPendingInsert** and perhaps **adFieldCantCreate** (depending upon your provider). You can supply an appropriate value for the new **Field** and call **Update** again.  
  
## Recordset Field Status  
 Changes to the value of a **Field** object in the Fields collection of either a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) are cached until the object's [Update](../../../ado/reference/ado-api/update-method.md) method is called. At that point, if the change to the Field's value caused an error, OLE DB raises the error **DB_E_ERRORSOCCURRED** (2147749409). The Status property of any of the **Field** objects in the **Fields** collection that caused the error will contain a value from the [FieldStatusEnum](../../../ado/reference/ado-api/fieldstatusenum.md) describing the cause of the problem.  
  
## Applies To  
 [Field Object](../../../ado/reference/ado-api/field-object.md)  
  
## See Also  
 [Status Property Example (Field) (VB)](../../../ado/reference/ado-api/status-property-example-field-vb.md)   
 [Status Property Example (VC++)](../../../ado/reference/ado-api/status-property-example-vc.md)   
