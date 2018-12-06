---
title: "Restrict Rows (Partition Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.partitionwizard.typefilterexpression.f1"
ms.assetid: eec8da8f-eab4-4ac4-a81d-995c814f88ca
author: minewiskan
ms.author: owend
manager: craigg
---
# Restrict Rows (Partition Wizard)
  Use the **Restrict Rows** page to restrict the rows that will be retrieved from the specified table and will be aggregated and included in the partition.  
  
> [!NOTE]  
>  This page is appears only if you selected a single table in the **Specify Source Information** page.  
  
> [!CAUTION]  
>  If you specified a table in **Available Tables** on the **Specify Source Information** page that is used by another partition, you must provide a query in the **Restrict Rows** page or risk duplicating data in the cube.  
  
## Options  
 **Specify a query to restrict rows**  
 Select to enter a query that restricts rows into the **Query** box.  
  
 If **Supply a WHERE clause** is empty when this option is selected, that option is populated with an SQL statement that retrieves all columns and all rows from the previously selected table.  
  
 **Query**  
 Type or modify the SQL statement used when retrieving rows from the table when the partition is processed.  
  
> [!IMPORTANT]  
>  By specifying a WHERE clause, a subset of records can be used for this partition. This is essential to prevent duplication of data when multiple partitions are based on a single fact table.  
  
 **Check**  
 Verifies that the statement in **Query** is a valid SQL statement.  
  
## See Also  
 [Partitions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)  
  
  
