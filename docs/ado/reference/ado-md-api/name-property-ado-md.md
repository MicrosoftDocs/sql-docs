---
title: "Name Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Level::Name"
  - "CubeDef::Name"
  - "Member::Name"
  - "Catalog::Name"
  - "Dimension::Name"
  - "Name"
  - "Axis::Name"
  - "Hierarchy::Name"
helpviewer_keywords: 
  - "Name property [ADO MD]"
ms.assetid: 4a04380b-51dc-4aaf-8d25-123cdd589641
author: MightyPen
ms.author: genemi
manager: craigg
---
# Name Property (ADO MD)
Indicates the name of an object.  
  
## Return Values  
 Returns a **String** and is read-only.  
  
## Remarks  
 You can retrieve the **Name** property of an object by an ordinal reference, after which you can refer to the object directly by name. For example, if `cdf.CubeDefs(0).Name` yields "Bobs Video Store", you can refer to this [CubeDef](../../../ado/reference/ado-md-api/cubedef-object-ado-md.md) as `cdf.CubeDefs("Bobs Video Store")`.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Axis Object (ADO MD)](../../../ado/reference/ado-md-api/axis-object-ado-md.md)|[Catalog Object (ADO MD)](../../../ado/reference/ado-md-api/catalog-object-ado-md.md)|[CubeDef Object (ADO MD)](../../../ado/reference/ado-md-api/cubedef-object-ado-md.md)|  
|[Dimension Object (ADO MD)](../../../ado/reference/ado-md-api/dimension-object-ado-md.md)|[Hierarchy Object (ADO MD)](../../../ado/reference/ado-md-api/hierarchy-object-ado-md.md)|[Level Object (ADO MD)](../../../ado/reference/ado-md-api/level-object-ado-md.md)|  
|[Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)|||  
  
## See Also  
 [Catalog Example (VB)](../../../ado/reference/ado-md-api/catalog-example-vb.md)   
 [Caption Property (ADO MD)](../../../ado/reference/ado-md-api/caption-property-ado-md.md)   
 [Description Property (ADO MD)](../../../ado/reference/ado-md-api/description-property-ado-md.md)   
 [UniqueName Property (ADO MD)](../../../ado/reference/ado-md-api/uniquename-property-ado-md.md)
