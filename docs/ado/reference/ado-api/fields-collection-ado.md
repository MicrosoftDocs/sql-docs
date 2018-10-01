---
title: "Fields Collection (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Fields"
  - "Recordset15::Fields"
  - "_Record::Fields"
helpviewer_keywords: 
  - "Fields collection [ADO]"
ms.assetid: 7c371474-b88f-4730-afa5-44163a0488d5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Fields Collection (ADO)
Contains all the [Field](../../../ado/reference/ado-api/field-object.md) objects of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) or [Record](../../../ado/reference/ado-api/record-object-ado.md) object.  
  
## Remarks  
 A **Recordset** object has a **Fields** collection made up of **Field** objects. Each **Field** object corresponds to a column in the **Recordset**. You can populate the **Fields** collection before opening the **Recordset** by calling the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method on the collection.  
  
> [!NOTE]
>  See the **Field** object topic for a more detailed explanation of how to use **Field** objects.  
  
 The **Fields** collection has an [Append](../../../ado/reference/ado-api/append-method-ado.md) method, which provisionally creates and adds a **Field** object to the collection, and an **Update** method, which finalizes any additions or deletions.  
  
 A **Record** object has two special fields that can be indexed with [FieldEnum](../../../ado/reference/ado-api/fieldenum.md) constants. One constant accesses a field containing the default stream for the **Record**, and the other accesses a field containing the absolute URL string for the **Record**.  
  
 Certain providers (for example, the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md)) may populate the **Fields** collection with a subset of available fields for the **Record** or **Recordset**. Other fields will not be added to the collection until they are first referenced by name or indexed by your code.  
  
 If you attempt to reference a nonexistent field by name, a new **Field** object will be appended to the **Fields** collection with a [Status](../../../ado/reference/ado-api/status-property-ado-field.md) of **adFieldPendingInsert**. When you call [Update](../../../ado/reference/ado-api/update-method.md), ADO will create a new field in your data source if allowed by your provider.  
  
 This section contains the following topic.  
  
-   [Fields Collection Properties, Methods, and Events](../../../ado/reference/ado-api/fields-collection-properties-methods-and-events.md)  
  
## See Also  
 [Field Object](../../../ado/reference/ado-api/field-object.md)
