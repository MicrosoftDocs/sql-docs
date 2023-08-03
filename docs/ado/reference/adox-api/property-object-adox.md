---
title: "Property Object (ADOX)"
description: "Property Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Property object [ADOX]"
apitype: "COM"
---
# Property Object (ADOX)
Represents a characteristic of an ADOX object.  
  
## Remarks  
 ADOX objects have two types of properties: built-in and dynamic.  
  
 Built-in properties are those properties immediately available to any new object, using the MyObject.Property syntax. They do not appear as Property objects in an object's [Properties collection](../ado-api/properties-collection-ado.md), so although you can change their values, you cannot modify their characteristics.  
  
 Dynamic properties are defined by the underlying data provider, and appear in the Properties collection for the appropriate ADOX object.  Dynamic properties can be referenced only through the collection, using the MyObject.Properties(0) or MyObject.Properties("Name") syntax.  
  
 You cannot delete either kind of property.  
  
 A dynamic Property object has four built-in properties of its own:  
  
 The [Name](../ado-api/name-property-ado.md) property is a string that identifies the property.  
  
 The [Type](../ado-api/type-property-ado.md) property is an integer that specifies the property data type.  
  
 The [Value](../ado-api/value-property-ado.md) property is a variant that contains the property setting. Value is the default property for a Property object.  
  
 The [Attributes](../ado-api/attributes-property-ado.md) property is a long value that indicates characteristics of the property specific to the provider.  
  
 This section contains the following topic.  
  
-   [ADOX Property Object Properties, Methods, and Events](./adox-property-object-properties-methods-and-events.md)