---
title: "Property Object (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Property object [ADOX]"
ms.assetid: 6a56def6-dbe6-4ccc-a491-8d076889f019
author: MightyPen
ms.author: genemi
manager: craigg
---
# Property Object (ADOX)
Represents a characteristic of an ADOX object.  
  
## Remarks  
 ADOX objects have two types of properties: built-in and dynamic.  
  
 Built-in properties are those properties immediately available to any new object, using the MyObject.Property syntax. They do not appear as Property objects in an object's [Properties collection](../../../ado/reference/ado-api/properties-collection-ado.md), so although you can change their values, you cannot modify their characteristics.  
  
 Dynamic properties are defined by the underlying data provider, and appear in the Properties collection for the appropriate ADOX object.  Dynamic properties can be referenced only through the collection, using the MyObject.Properties(0) or MyObject.Properties("Name") syntax.  
  
 You cannot delete either kind of property.  
  
 A dynamic Property object has four built-in properties of its own:  
  
 The [Name](../../../ado/reference/ado-api/name-property-ado.md) property is a string that identifies the property.  
  
 The [Type](../../../ado/reference/ado-api/type-property-ado.md) property is an integer that specifies the property data type.  
  
 The [Value](../../../ado/reference/ado-api/value-property-ado.md) property is a variant that contains the property setting. Value is the default property for a Property object.  
  
 The [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) property is a long value that indicates characteristics of the property specific to the provider.  
  
 This section contains the following topic.  
  
-   [ADOX Property Object Properties, Methods, and Events](../../../ado/reference/adox-api/adox-property-object-properties-methods-and-events.md)
