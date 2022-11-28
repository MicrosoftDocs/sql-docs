---
title: "GetSchemaObject Method (ADO MD)"
description: "GetSchemaObject Method (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "GetSchemaObject"
  - "Cellset::GetSchemaObject"
helpviewer_keywords:
  - "GetSchemaObject method [ADO MD]"
apitype: "COM"
---
# GetSchemaObject Method (ADO MD)
Retrieves an ADO MD schema object ([Dimension](./dimension-object-ado-md.md), [Hierarchy](./hierarchy-object-ado-md.md), [Level](./level-object-ado-md.md), or [Member](./member-object-ado-md.md)) by its [UniqueName](./uniquename-property-ado-md.md).  
  
## Syntax  
  
```  
  
Set object = CubeDef.GetSchemaObject (ObjType, UniqueName)  
```  
  
#### Parameters  
 *ObjType*  
 A [SchemaObjectTypeEnum](./schemaobjecttypeenum.md) value specifying what type of schema object (Dimension, Hierarchy, Level, or Member) to retrieve.  
  
 *UniqueName*  
 A **String** specifying the **UniqueName** property value of the object to retrieve.  
  
## Remarks  
 **GetSchemaObject** retrieves objects using their unique names, as specified by the **UniqueName** property. The names of parent objects do not need to be known, and parent collections do not need to be populated to retrieve a schema object.  
  
## Applies To  
 [CubeDef Object (ADO MD)](./cubedef-object-ado-md.md)  
  
## See Also  
 [CubeDef Object (ADO MD)](./cubedef-object-ado-md.md)