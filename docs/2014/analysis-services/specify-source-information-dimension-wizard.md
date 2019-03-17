---
title: "Specify Source Information (Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensionwizard.dimensionmaintable.f1"
ms.assetid: 0538b490-5185-49e1-a783-4ce3539a0de5
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify Source Information (Dimension Wizard)
  Use the **Select the Main Dimension Table** page to select the data source view, main dimension table, key columns, and member name column for the dimension to be created.  
  
 **To open the Dimension Wizard**  
  
-   In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], in **Solution Explorer**, right-click the **Dimensions** folder for an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, and then click **New Dimension**.  
  
## Options  
 **Data source view**  
 Select a data source view.  
  
 **Main table**  
 Select a table from the selected data source view to use as the main table for the dimension.  
  
 **Key columns**  
 Select the key columns from the table specified in **Main table** for the key attribute of the dimension.  
  
> [!NOTE]  
>  More than one column can be selected. If the table contains a composite primary key, select all the columns included in the composite primary key. The order of the key columns is important.  
  
 **Name column**  
 Select the column from the table specified in **Main table** that provides the member names for the dimension. A name column must be specified when a composite key is used. To create a name column for a composite key, we recommend that you create a named calculation in the data source view that concatenates the specified key columns. When a single key is used, the name column is optional.  
  
## See Also  
 [Dimension Wizard F1 Help](dimension-wizard-f1-help.md)   
 [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md)   
 [Dimensions in Multidimensional Models](multidimensional-models/dimensions-in-multidimensional-models.md)  
  
  
