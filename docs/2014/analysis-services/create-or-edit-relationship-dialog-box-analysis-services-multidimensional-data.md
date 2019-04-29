---
title: "Create or Edit Relationship Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dsvdesigner.createrelationship.f1"
helpviewer_keywords: 
  - "Create Relationship dialog box"
ms.assetid: da3c7074-623e-4ddf-a707-d3276a47cf1c
author: minewiskan
ms.author: owend
manager: craigg
---
# Create or Edit Relationship Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Create/Edit Relationship** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to define or modify a relationship in a data source view. You can display the **Create/Edit Relationship** dialog box by:  
  
-   Clicking **New Relationship** in the **Toolbar** pane of **Data Source View Designer**.  
  
-   Right-clicking a table in either the **Tables** or **Diagram** pane of **Data Source View Designer** and selecting **New Relationship**.  
  
-   Right-clicking a relationship in the **Diagram** pane of **Data Source View Designer** and selecting **Edit Relationship**.  
  
> [!NOTE]  
>  You can create relationships in **Data Source View Designer** that do not exist in the underlying data source, and remove relationships created by **Data Source View Designer** from existing foreign key relationships in the underlying data source.  
  
## Options  
 **Source (foreign key) table**  
 Select the table or named query that contains the reference to one or more columns in the destination table.  
  
 **Destination (primary key) table**  
 Select the table that contains one or more columns referenced by the source table. For self-joins, select the same table selected in **Source (foreign key) table**.  
  
 **Source columns**  
 Select the columns that reference columns in the destination table.  
  
 **Destination columns**  
 Select the columns that are referenced by columns in the source table.  
  
 **Reverse**  
 Click to reverse the direction of the relationship.  
  
 **Description**  
 Type an optional description for the relationship.  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Data Source View Designer &#40;Analysis Services - Multidimensional Data&#41;](data-source-view-designer-analysis-services-multidimensional-data.md)  
  
  
