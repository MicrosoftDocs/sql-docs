---
title: "Property Object (ADO)"
description: "Property Object (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Property"
helpviewer_keywords:
  - "Property object [ADO]"
apitype: "COM"
---
# Property Object (ADO)
Represents a dynamic characteristic of an ADO object that is defined by the provider.  
  
## Remarks  
 ADO objects have two types of properties: built-in and dynamic.  
  
 Built-in properties are those properties implemented in ADO and immediately available to any new object, using the `MyObject.Property` syntax. They do not appear as **Property** objects in an object's [Properties](./properties-collection-ado.md) collection, so although you can change their values, you cannot modify their characteristics.  
  
 Dynamic properties are defined by the underlying data provider, and appear in the **Properties** collection for the appropriate ADO object. For example, a property specific to the provider may indicate if a [Recordset](./recordset-object-ado.md) object supports transactions or updating. These additional properties will appear as **Property** objects in that **Recordset** object's **Properties** collection. Dynamic properties can be referenced only through the collection, using the `MyObject.Properties(0)` or `MyObject.Properties("Name")` syntax.  
  
 You cannot delete either kind of property.  
  
 A dynamic **Property** object has four built-in properties of its own:  
  
-   The [Name](./name-property-ado.md) property is a string that identifies the property.  
  
-   The [Type](./type-property-ado.md) property is an integer that specifies the property data type.  
  
-   The [Value](./value-property-ado.md) property is a variant that contains the property setting. **Value** is the default property for a **Property** object.  
  
-   The [Attributes](./attributes-property-ado.md) property is a long value that indicates characteristics of the property specific to the provider.  
  
 This section contains the following topic.  
  
-   [Property Object Properties, Methods, and Events](./property-object-properties-methods-and-events.md)  
  
## See Also  
 [Command Object (ADO)](./command-object-ado.md)   
 [Connection Object (ADO)](./connection-object-ado.md)   
 [Field Object](./field-object.md)   
 [Properties Collection (ADO)](./properties-collection-ado.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)