---
title: "Property Object (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Property"
helpviewer_keywords: 
  - "Property object [ADO]"
ms.assetid: b2a4767c-03c7-4935-a3bc-df3e1a38a009
author: MightyPen
ms.author: genemi
manager: craigg
---
# Property Object (ADO)
Represents a dynamic characteristic of an ADO object that is defined by the provider.  
  
## Remarks  
 ADO objects have two types of properties: built-in and dynamic.  
  
 Built-in properties are those properties implemented in ADO and immediately available to any new object, using the `MyObject.Property` syntax. They do not appear as **Property** objects in an object's [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection, so although you can change their values, you cannot modify their characteristics.  
  
 Dynamic properties are defined by the underlying data provider, and appear in the **Properties** collection for the appropriate ADO object. For example, a property specific to the provider may indicate if a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object supports transactions or updating. These additional properties will appear as **Property** objects in that **Recordset** object's **Properties** collection. Dynamic properties can be referenced only through the collection, using the `MyObject.Properties(0)` or `MyObject.Properties("Name")` syntax.  
  
 You cannot delete either kind of property.  
  
 A dynamic **Property** object has four built-in properties of its own:  
  
-   The [Name](../../../ado/reference/ado-api/name-property-ado.md) property is a string that identifies the property.  
  
-   The [Type](../../../ado/reference/ado-api/type-property-ado.md) property is an integer that specifies the property data type.  
  
-   The [Value](../../../ado/reference/ado-api/value-property-ado.md) property is a variant that contains the property setting. **Value** is the default property for a **Property** object.  
  
-   The [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) property is a long value that indicates characteristics of the property specific to the provider.  
  
 This section contains the following topic.  
  
-   [Property Object Properties, Methods, and Events](../../../ado/reference/ado-api/property-object-properties-methods-and-events.md)  
  
## See Also  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)   
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [Field Object](../../../ado/reference/ado-api/field-object.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
