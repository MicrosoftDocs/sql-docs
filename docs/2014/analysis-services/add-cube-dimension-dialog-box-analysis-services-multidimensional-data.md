---
title: "Add Cube Dimension Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.addcubedimensiondialog.f1"
helpviewer_keywords: 
  - "Add Cube Dimension dialog box"
ms.assetid: 625a3b1f-183b-445f-9bb7-96945c324767
author: minewiskan
ms.author: owend
manager: craigg
---
# Add Cube Dimension Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Add Cube Dimension** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to add a reference to a database dimension to a cube. You can display the **Add Cube Dimension** dialog box by doing one of the following:  
  
-   Click **Add Cube Dimension** in the **Toolbar** pane on the **Cube Structure** or **Dimension Usage** tab in Cube Designer.  
  
-   Right-click the **Dimensions** pane on the **Cube Structure** tab in Cube Designer and select **Add Cube Dimension** from the context menu.  
  
-   Right-click the **Grid** pane on the **Dimension Usage** tab in Cube Designer and select **Add Cube Dimension** from the context menu.  
  
> [!NOTE]  
>  Each cube dimension can have only one relationship to a measure group. However, you can create more than one cube dimension and add it to the cube, if the database dimension on which the cube dimension is based is related to measure groups through more than one relationship in the data source view. Such dimensions are referred to as role-playing dimensions and commonly occur with time dimensions.  
  
## Options  
 **Select dimension**  
 Select an existing database dimension to add a cube dimension based on it to the selected cube. Multiple cube dimensions can be defined from the same database dimension.  
  
> [!NOTE]  
>  If more than one cube dimension based on the same database dimension is added to a cube, the additional cube dimensions are called role-playing dimensions.  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)  
  
  
