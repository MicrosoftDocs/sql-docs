---
title: "Update Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::Update"
helpviewer_keywords: 
  - "Update method [ADO]"
ms.assetid: 6b2a9c31-1a7e-40db-8a53-30720d0f6cc1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Update Method
Saves any changes you make to the current row of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object, or the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md) object.  
  
## Syntax  
  
```  
  
recordset.Update Fields, Values  
record.Fields.Update  
```  
  
#### Parameters  
 *Fields*  
 Optional. A **Variant** that represents a single name, or a **Variant** array that represents names or ordinal positions of the field or fields you wish to modify.  
  
 *Values*  
 Optional. A **Variant** that represents a single value, or a **Variant** array that represents values for the field or fields in the new record.  
  
## Remarks  
  
## Recordset  
 Use the **Update** method to save any changes you make to the current record of a **Recordset** object since calling the [AddNew](../../../ado/reference/ado-api/addnew-method-ado.md) method or since changing any field values in an existing record. The **Recordset** object must support updates.  
  
 To set field values, do one of the following:  
  
-   Assign values to a [Field](../../../ado/reference/ado-api/field-object.md) object's [Value](../../../ado/reference/ado-api/value-property-ado.md) property and call the **Update** method.  
  
-   Pass a field name and a value as arguments with the **Update** call.  
  
-   Pass an array of field names and an array of values with the **Update** call.  
  
 When you use arrays of fields and values, there must be an equal number of elements in both arrays. Also, the order of field names must match the order of field values. If the number and order of fields and values do not match, an error occurs.  
  
 If the **Recordset** object supports batch updating, you can cache multiple changes to one or more records locally until you call the [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) method. If you are editing the current record or adding a new record when you call the **UpdateBatch** method, ADO will automatically call the **Update** method to save any pending changes to the current record before transmitting the batched changes to the provider.  
  
 If you move from the record you are adding or editing before calling the **Update** method, ADO will automatically call **Update** to save the changes. You must call the [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method if you want to cancel any changes made to the current record or discard a newly added record.  
  
 The current record remains current after you call the **Update** method.  
  
## Record  
 The **Update** method finalizes additions, deletions, and updates to fields in the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a **Record** object.  
  
 For example, fields deleted with the **Delete** method are marked for deletion immediately but remain in the collection. The **Update** method must be called to actually delete these fields from the provider's collection.  
  
## Applies To  
  
|||  
|-|-|  
|[Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)|[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)|  
  
## See Also  
 [Update and CancelUpdate Methods Example (VB)](../../../ado/reference/ado-api/update-and-cancelupdate-methods-example-vb.md)   
 [Update and CancelUpdate Methods Example (VC++)](../../../ado/reference/ado-api/update-and-cancelupdate-methods-example-vc.md)   
 [AddNew Method (ADO)](../../../ado/reference/ado-api/addnew-method-ado.md)   
 [CancelUpdate Method (ADO)](../../../ado/reference/ado-api/cancelupdate-method-ado.md)   
 [EditMode Property](../../../ado/reference/ado-api/editmode-property.md)   
 [UpdateBatch Method](../../../ado/reference/ado-api/updatebatch-method.md)
