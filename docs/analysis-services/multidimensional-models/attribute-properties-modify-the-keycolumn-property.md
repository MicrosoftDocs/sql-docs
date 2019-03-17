---
title: "Modify the KeyColumn Property of an Attribute | Microsoft Docs"
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
# Attribute Properties - Modify the KeyColumn Property
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can modify the **KeyColumns** property of an attribute. For example, you might want to specify a composite key instead of a single key as the key for the attribute.  
  
### To modify the KeyColumns property of an attribute  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project in which you want to modify the **KeyColumns** property.  
  
2.  Open Dimension Designer by doing one of the following procedures:  
  
    -   In **Solution Explorer**, right-click the dimension in the **Dimensions** folder, and then either click **Open** or **View Designer**.  
  
         -or-  
  
    -   In Cube Designer, on the **Cube Structure** tab, expand the cube dimension in the **Dimensions** pane and click **Edit \<dimension>**.  
  
3.  On the **Dimension Structure** tab, in the **Attributes** pane, click the attribute whose **KeyColumns** property you want to modify.  
  
4.  In the **Properties** window, click the value for the **KeyColumns** property.  
  
5.  Click the browse **(...)** button that appears in the value cell of the property box.  
  
     The **Key Columns** dialog box opens.  
  
6.  To remove an existing key column, in the **Key Columns** list, select the column, and then click the **\<** button.  
  
7.  To add a key column, in the **Available Columns** list, select the column, and then click the **>** button.  
  
    > [!NOTE]  
    >  If you define multiple key columns, the order in which those columns appear in the **Key Columns** list affects the display order. For example, a month attribute has two key columns: month and year. If the year column appears in the list before the month column, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will sort by year and then by month. If the month column appears before the year column, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will sort by month and then by year.  
  
8.  To change the order of key columns, select a column, and then click the **Up** or **Down** button.  
  
## See Also  
 [Dimension Attribute Properties Reference](../../analysis-services/multidimensional-models/dimension-attribute-properties-reference.md)  
  
  
