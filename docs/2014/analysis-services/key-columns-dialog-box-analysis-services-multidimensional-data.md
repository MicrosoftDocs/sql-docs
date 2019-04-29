---
title: "Key Columns Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql.asvs.dimensiondesigner.dbv.dataitemCollection.f1"
helpviewer_keywords: 
  - "DataItem Collection dialog box"
ms.assetid: 585f27f2-d5eb-4516-b29a-2084010b7d51
author: minewiskan
ms.author: owend
manager: craigg
---
# Key Columns Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Key Columns** dialog box to change the **KeyColumns** property of an attribute. For more information, see [Modify the KeyColumn Property of an Attribute](multidimensional-models/attribute-properties-modify-the-keycolumn-property.md).  
  
 **To display the Key Columns dialog box**  
  
-   In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], select an attribute, and in the **Properties** window, click the ellipsis button (**...**) associated with the **KeyColumns** property of that attribute.  
  
## Options  
 **Source table**  
 Select the source table for which you want to select its key columns. You can select the source table from a list of all tables in the Data Source View.  
  
 **Available Columns**  
 Select the columns that you want to use as key columns. You can select the columns from a list of columns in the specified **Source table** that have not yet been selected as key columns.  
  
 To add the selected columns to the **Key Columns** list, click the **>** button.  
  
 **Key Columns**  
 Define the order of the selected key columns. The order of the key columns is important in defining the correct composite key. To order or reorder the list of key columns, select a column, and then click the **Up** or **Down** buttons.  
  
 To remove a column from the **Key Columns** list, select the column and click the **\<** button.  
  
 **Up**  
 Click to move the column selected in **Key Columns** up one position.  
  
> [!NOTE]  
>  This option is enabled only if the list contains more than one column and a column is selected.  
  
 **Down**  
 Click to move the column selected in **Key Columns** down one position.  
  
> [!NOTE]  
>  This option is enabled only if the list contains more than one column and a column is selected.  
  
 **>**  
 Click to add a new column to the end of the columns listed in **Key Columns**.  
  
 **<**  
 Click to remove the selected column from the columns listed in **Key Columns**.  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)  
  
  
