---
title: "Perspectives in Analysis Services Multidimensional Models | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Perspectives in Multidimensional Models
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A perspective is a subset of a cube created for a particular application or group of users. The cube itself is the default perspective. A perspective is exposed to a client as a cube. When a user views a perspective, it appears like another cube. Any changes made to cube data through writeback in the perspective are to the original cube. For more information about the views in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [Perspectives](../../analysis-services/multidimensional-models-olap-logical-cube-objects/perspectives.md).  
  
 Use the **Perspectives** tab in Cube Designer to create or modify perspectives in a cube. The first column of the **Perspectives** tab is the **Cube Objects** column, which lists all the objects in the cube. This corresponds to the default perspective for the cube, which is the cube itself.  
  
## Creating or Deleting Perspectives  
 You can add a perspective to the **Perspectives** tab by clicking **New Perspective** on the **Cube** menu. You can also click the **New Perspective** button on the toolbar, or right-click anywhere in the pane and click **New Perspective** on the shortcut menu. You can add any number of perspectives to a cube.  
  
 To remove a perspective, first click any cell in the column for the perspective that you want to delete. Then, on the **Cube** menu, click **Delete Perspective**. You can also click the **Delete Perspective** button on the toolbar, or right-click any cell in the perspective you want to delete, and then click **Delete Perspective** on the shortcut menu.  
  
## Renaming Perspectives  
 The first row of each perspective shows its name. When you create a perspective, the name is initially Perspective (followed by an ordinal number, starting with 1, if there is already a perspective called Perspective). You can click the name to edit it.  
  
## Hiding Objects from a Perspective  
 To hide an object from a perspective, clear the check box in the row that corresponds to the object in the column for the perspective. The cube objects that can be hidden from a perspective are:  
  
-   Measure groups  
  
-   Measures  
  
-   Dimensions  
  
-   Hierarchies  
  
-   Named sets  
  
-   KPIs  
  
-   Actions  
  
-   Calculated members  
  
 To view any object, expand the category (**Measure Groups**, **Dimensions**, **KPIs**, **Calculations**, or **Actions**) for any object type under **Cube Objects**. To view hierarchies or attributes in a dimension, first expand a dimension, and then expand the **Hierarchies** or **Attributes** row. To view measures in a measure group, expand the measure group.  
  
## See Also  
 [Cubes in Multidimensional Models](../../analysis-services/multidimensional-models/cubes-in-multidimensional-models.md)  
  
  
