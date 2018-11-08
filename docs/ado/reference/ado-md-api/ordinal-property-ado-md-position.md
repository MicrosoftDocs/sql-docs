---
title: "Ordinal Property (ADO MD Position) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Position::Ordinal"
  - "Ordinal"
helpviewer_keywords: 
  - "Ordinal property [ADO MD]"
ms.assetid: 6efe8b5d-a2d5-43a9-a5ea-f9244f8d4ec9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Ordinal Property (ADO MD Position)
Uniquely identifies a [position](../../../ado/reference/ado-md-api/position-object-ado-md.md) along an axis.  
  
## Return Values  
 Returns a **Long** integer and is read-only.  
  
## Remarks  
 The **Ordinal** property of a [Position](../../../ado/reference/ado-md-api/position-object-ado-md.md) object corresponds to the index of the **Position** within the [Positions](../../../ado/reference/ado-md-api/positions-collection-ado-md.md) collection.  
  
 A cell can quickly be retrieved using the **Ordinal** of the **Position** along each axis with the [Item](../../../ado/reference/ado-md-api/item-property-ado-md-cellset.md) property of the [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object.  
  
## Applies To  
 [Position Object (ADO MD)](../../../ado/reference/ado-md-api/position-object-ado-md.md)  
  
## See Also  
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)   
 [Item Property (ADO MD Cellset)](../../../ado/reference/ado-md-api/item-property-ado-md-cellset.md)   
 [Ordinal Property (ADO MD Cell)](../../../ado/reference/ado-md-api/ordinal-property-ado-md-cell.md)
