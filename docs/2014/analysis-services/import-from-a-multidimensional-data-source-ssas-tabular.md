---
title: "Import from a Multidimensional Data Source (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 7f0793ea-a4c7-42e9-b722-2164a454ebca
author: minewiskan
ms.author: owend
manager: craigg
---
# Import from a Multidimensional Data Source (SSAS Tabular)
  You can use an Analysis Services cube database as a data source for a tabular model. In order to import data from an Analysis Services cube, you must define an MDX Query to select data to import.  
  
 Any data that is contained in a SQL Server Analysis Services database can be imported into a model. You can extract all or part of a dimension, or get slices and aggregates from the cube, such as the sum of sales, month by month, for the current year, etc. However, you should keep in mind all data that you import from a cube is flattened. Therefore, if you define a query that retrieves measures along multiple dimensions, the data will be imported with each dimension in a separate column.  
  
 Analysis Services cubes must be version SQL Server 2005, SQL Server 2008, SQL Server 2008 R2, [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
### To import data from an Analysis Services cube  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click the **Model** menu, and then click **Import from Data Source**.  
  
2.  On the **Connect to a Data Source** page, select **Microsoft Analysis Services**, and then click **Next**.  
  
3.  Follow the steps in the Table Import Wizard. You will be able to specify an MDX query on the **Specify a MDX Query** page. To use the MDX Query Designer, on the Specify a MDX Query page, click **Design**.  
  
## See Also  
 [Import Data &#40;SSAS Tabular&#41;](import-data-ssas-tabular.md)   
 [Data Sources Supported &#40;SSAS Tabular&#41;](tabular-models/data-sources-supported-ssas-tabular.md)  
  
  
