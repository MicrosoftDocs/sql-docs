---
title: "Item Property (ADO MD Cellset) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Item"
  - "Cellset::Item"
helpviewer_keywords: 
  - "Item property [ADO MD]"
ms.assetid: 0e93d79b-b12e-4e98-889e-c2dfcca20fd0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Item Property (ADO MD Cellset)
Retrieves a cell from a [cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) using its coordinates.  
  
## Syntax  
  
```  
  
Set  
Cell = Cellset.Item ( Positions)  
```  
  
## Parameters  
 *Positions*  
 A **VariantArray** of values that uniquely specify a cell. *Positions* can be one of the following:  
  
-   An array of position numbers  
  
-   An array of member names  
  
-   The ordinal position  
  
## Remarks  
 Use the **Item** property to return a [Cell](../../../ado/reference/ado-md-api/cell-object-ado-md.md) object within a [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object. If the **Item** property cannot find the cell corresponding to the *Positions* argument, an error occurs.  
  
 The **Item** property is the default property for the **Cellset** object. The following syntax forms are interchangeable:  
  
```  
  
Cellset.Item ( Positions )Cellset ( Positions )  
```  
  
## Remarks  
 The *Positions* argument specifies which cell to return. You can specify the cell by ordinal position or by the position along each axis. When specifying the cell by position along each axis, you can specify the numeric value of the position or the names of the members for each position.  
  
 The ordinal position is a number that uniquely identifies one cell within the **Cellset**. Conceptually, cells are numbered in a **Cellset** as if the **Cellset** were a *p*-dimensional array, where *p* is the number of axes. Cells are addressed in row-major order. Below is the formula for calculating the ordinal number of a cell:  
  
 If member names are passed as strings to **Item**, the members must be listed in increasing order of the numeric axis identifiers. Within an axis, the members must be listed in increasing order of dimension nesting - that is, the outermost dimension's member comes first, followed by members of inner dimensions. Each dimension should be represented by a separate string, and the list of member strings should be separated by commas.  
  
> [!NOTE]
>  Retrieving cells by member name may not be supported by your data provider. See the documentation for your provider for more information.  
  
## Applies To  
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)  
  
## See Also  
 [Cell Object (ADO MD)](../../../ado/reference/ado-md-api/cell-object-ado-md.md)   
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)
