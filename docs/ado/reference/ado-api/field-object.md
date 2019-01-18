---
title: "Field Object | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Field"
helpviewer_keywords: 
  - "Field object [ADO]"
ms.assetid: b10a72fc-3c4b-4186-a70b-993dc9f7a092
author: MightyPen
ms.author: genemi
manager: craigg
---
# Field Object
Represents a column of data with a common data type.  
  
## Remarks  
 Each **Field** object corresponds to a column in the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md). You use the [Value](../../../ado/reference/ado-api/value-property-ado.md) property of **Field** objects to set or return data for the current record. Depending on the functionality the provider exposes, some collections, methods, or properties of a **Field** object may not be available.  
  
 With the collections, methods, and properties of a **Field** object, you can do the following:  
  
-   Return the name of a field with the [Name](../../../ado/reference/ado-api/name-property-ado.md) property.  
  
-   View or change the data in the field with the **Value** property. **Value** is the default property of the **Field** object.  
  
-   Return the basic characteristics of a field with the [Type](../../../ado/reference/ado-api/type-property-ado.md), [Precision](../../../ado/reference/ado-api/precision-property-ado.md), and [NumericScale](../../../ado/reference/ado-api/numericscale-property-ado.md) properties.  
  
-   Return the declared size of a field with the [DefinedSize](../../../ado/reference/ado-api/definedsize-property.md) property.  
  
-   Return the actual size of the data in a given field with the [ActualSize](../../../ado/reference/ado-api/actualsize-property-ado.md) property.  
  
-   Determine what types of functionality are supported for a given field with the [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) property and [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
-   Manipulate the values of fields containing long binary or long character data with the [AppendChunk](../../../ado/reference/ado-api/appendchunk-method-ado.md) and [GetChunk](../../../ado/reference/ado-api/getchunk-method-ado.md) methods.  
  
-   If the provider supports batch updates, resolve discrepancies in field values during batch updating with the [OriginalValue](../../../ado/reference/ado-api/originalvalue-property-ado.md) and [UnderlyingValue](../../../ado/reference/ado-api/underlyingvalue-property.md) properties.  
  
 All of the metadata properties (**Name**, **Type**, **DefinedSize**, **Precision**, and **NumericScale**) are available before opening the **Field** object's **Recordset**. Setting them at that time is useful for dynamically constructing forms.  
  
 This section contains the following topic.  
  
-   [Field Object Properties, Methods, and Events](../../../ado/reference/ado-api/field-object-properties-methods-and-events.md)  
  
## See Also  
 [Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
