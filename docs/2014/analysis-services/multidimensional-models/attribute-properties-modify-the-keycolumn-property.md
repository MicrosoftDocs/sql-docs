---
title: "Modify the KeyColumn Property of an Attribute | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "binding attributes [Analysis Services]"
  - "attributes [Analysis Services], binding"
  - "key columns [Analysis Services]"
ms.assetid: a2643be4-8123-4cc3-baf9-e5ec54a1669d
author: minewiskan
ms.author: owend
manager: craigg
---
# Modify the KeyColumn Property of an Attribute
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
 [Dimension Attribute Properties Reference](dimension-attribute-properties-reference.md)  
  
  
