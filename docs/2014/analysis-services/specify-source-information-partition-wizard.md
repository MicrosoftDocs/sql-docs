---
title: "Specify Source Information (Partition Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.partitionwizard.specifydsvandfacttables.f1"
ms.assetid: b6c13587-c690-45d9-af90-b3d652afc55b
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify Source Information (Partition Wizard)
  Use the **Specify Source Information** page to select the measure group in which to create the partition, and also the data source view and filter tables for your partition.  
  
> [!CAUTION]  
>  If you specify a table in **Available Tables** that is used by another partition, you must provide a query in the **Restrict Rows** page or risk duplicating data in the cube.  
  
## Options  
 **Measure group**  
 Select a measure group for this partition.  
  
 **Look in**  
 Select the data source or data source view that contains the source tables for this partition. The data source view used by the measure group is selected by default.  
  
 **Filter tables**  
 Type the string used to restrict, by table name, the tables displayed in **Available tables**.  
  
 **Find tables**  
 Select to refresh the list of tables in **Available tables**, further restricting the list if a string was specified in **Filter tables**.  
  
 **Available tables**  
 Select the tables to use as source tables for partitions. The **Partition Wizard** creates one partition for each table selected in **Available tables**.  
  
 If no filter is specified in **Filter tables**, this option lists all tables in the data source or data source view that are specified in **Look in** and that are similar in structure to the fact table used by the measure group specified in **Measure group**.  
  
 If a filter is specified in **Filter tables**, the list is further restricted by comparing the filter against the table names that meet the previous criteria. Only those tables that contain the string specified in **Filter tables** are displayed.  
  
> [!NOTE]  
>  If more than one table is selected, the **Restrict Rows** page cannot be displayed and rows cannot be restricted for the partitions created from the selected tables. To restrict rows for each partition, run the Partition Wizard once for each table from which a partition is to be created.  
  
## See Also  
 [Partitions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)  
  
  
