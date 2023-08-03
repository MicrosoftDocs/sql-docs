---
title: "Item Property (ADO MD Cellset)"
description: "Item Property (ADO MD Cellset)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Item"
  - "Cellset::Item"
helpviewer_keywords:
  - "Item property [ADO MD]"
apitype: "COM"
---
# Item Property (ADO MD Cellset)
Retrieves a cell from a [cellset](./cellset-object-ado-md.md) using its coordinates.  
  
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
 Use the **Item** property to return a [Cell](./cell-object-ado-md.md) object within a [Cellset](./cellset-object-ado-md.md) object. If the **Item** property cannot find the cell corresponding to the *Positions* argument, an error occurs.  
  
 The **Item** property is the default property for the **Cellset** object. The following syntax forms are interchangeable:  
  
```  
  
Cellset.Item ( Positions )Cellset ( Positions )  
```  
  
 The *Positions* argument specifies which cell to return. You can specify the cell by ordinal position or by the position along each axis. When specifying the cell by position along each axis, you can specify the numeric value of the position or the names of the members for each position.  
  
 The ordinal position is a number that uniquely identifies one cell within the **Cellset**. Conceptually, cells are numbered in a **Cellset** as if the **Cellset** were a *p*-dimensional array, where *p* is the number of axes. Cells are addressed in row-major order. Below is the formula for calculating the ordinal number of a cell:  
  
 If member names are passed as strings to **Item**, the members must be listed in increasing order of the numeric axis identifiers. Within an axis, the members must be listed in increasing order of dimension nesting - that is, the outermost dimension's member comes first, followed by members of inner dimensions. Each dimension should be represented by a separate string, and the list of member strings should be separated by commas.  
  
> [!NOTE]
>  Retrieving cells by member name may not be supported by your data provider. See the documentation for your provider for more information.  
  
## Applies To  
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)  
  
## See Also  
 [Cell Object (ADO MD)](./cell-object-ado-md.md)   
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)