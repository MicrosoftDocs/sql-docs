---
title: "GetSchemaObject Method (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "GetSchemaObject"
  - "Cellset::GetSchemaObject"
helpviewer_keywords: 
  - "GetSchemaObject method [ADO MD]"
ms.assetid: 36b754b4-6b17-4dd1-a925-bca46938b7c4
author: MightyPen
ms.author: genemi
manager: craigg
---
# GetSchemaObject Method (ADO MD)
Retrieves an ADO MD schema object ([Dimension](../../../ado/reference/ado-md-api/dimension-object-ado-md.md), [Hierarchy](../../../ado/reference/ado-md-api/hierarchy-object-ado-md.md), [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md), or [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md)) by its [UniqueName](../../../ado/reference/ado-md-api/uniquename-property-ado-md.md).  
  
## Syntax  
  
```  
  
Set object = CubeDef.GetSchemaObject (ObjType, UniqueName)  
```  
  
#### Parameters  
 *ObjType*  
 A [SchemaObjectTypeEnum](../../../ado/reference/ado-md-api/schemaobjecttypeenum.md) value specifying what type of schema object (Dimension, Hierarchy, Level, or Member) to retrieve.  
  
 *UniqueName*  
 A **String** specifying the **UniqueName** property value of the object to retrieve.  
  
## Remarks  
 **GetSchemaObject** retrieves objects using their unique names, as specified by the **UniqueName** property. The names of parent objects do not need to be known, and parent collections do not need to be populated to retrieve a schema object.  
  
## Applies To  
 [CubeDef Object (ADO MD)](../../../ado/reference/ado-md-api/cubedef-object-ado-md.md)  
  
## See Also  
 [CubeDef Object (ADO MD)](../../../ado/reference/ado-md-api/cubedef-object-ado-md.md)
