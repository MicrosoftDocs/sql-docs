---
title: "Hide or Freeze Columns (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.hideunhidecolumnsdb.f1"
ms.assetid: 5407aee5-6a07-4559-a2ba-2ca00a242f02
author: minewiskan
ms.author: owend
manager: craigg
---
# Hide or Freeze Columns (SSAS Tabular)
  In the model designer, if there are columns that you don't want to display in a table, you can temporarily hide them. Hiding a column gives you more room on the screen to add new columns or to work with only relevant columns of data. You can hide and unhide columns from the **Column** menu in the model designer, and from the right-click menu available from each column header. To keep an area of a model visible while you scroll to another area of the model, you can lock specific columns in one area by freezing them.  
  
> [!IMPORTANT]  
>  The ability to hide columns is not intended to be used for data security, only to simplify and shorten the list of columns visible in the model designer or reports. To secure data, you can define security roles. Roles can limit viewable metadata and data to only those objects defined in the role. For more information, see [Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md).  
  
 When you hide a column, you have the option to hide the column while you are working in the model designer or in reports. If you hide all columns, the entire table appears blank in the model designer.  
  
> [!NOTE]  
>  If you have many columns to hide, you can create a perspective instead of hiding and unhiding columns. A perspective is a custom view of the data that makes it easier to work with subset of related data. For more information, see [Create and Manage Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)  
  
### To hide an individual column  
  
1.  In the model designer, select the table that contains the column that you want to hide.  
  
2.  Right-click the column, then click **Hide Columns**, and then click **From Designer and Reports**, **From Reports**, or **From Designer**.  
  
### To hide multiple columns  
  
1.  In the model designer, select the table that contains the columns that you want to hide.  
  
2.  Click on the **Columns** menu, and then click **Hide and Unhide**.  
  
3.  In the **Hide and Unhide Columns** dialog box, locate each column that you want to hide, and then deselect one or both of **In Designer** and **In Reports**.  
  
4.  Click **OK**.  
  
### To freeze columns  
  
1.  In the model designer, select the table that contains the columns that you want to freeze.  
  
2.  Select one or more columns to freeze.  
  
3.  Click on the **Columns** menu, and then click **Freeze**..  
  
    > [!NOTE]  
    >  When a column(s) is frozen, it is moved to left (or front) of the table in the designer. Unfreezing the column does not move it back to its original location.  
  
## See Also  
 [Tables and Columns &#40;SSAS Tabular&#41;](tables-and-columns-ssas-tabular.md)   
 [Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)   
 [Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md)  
  
  
