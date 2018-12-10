---
title: "Add sample data to an Analysis Services DirectQuery model in design mode | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Add sample data to a DirectQuery model in Design Mode
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
 In DirectQuery mode, table partitions are used to either create sample data subsets used during model design, or create alternatives of a full data view.
 
 When you deploy a DirectQuery tabular model, only one partition is allowed per table, and that partition by necessity must be full data view. Any additional partition is either a substitute for a full data view or sample data. In this topic, we're going to describe creating a sample partition, with a subset of data.
 
 By default, when designing a tabular model in DirectQuery mode in SSDT, the model's working database doesn't contain any data. There is one default partition for each table, and this partition directs all queries to the data source. 
  
You can, however, add a smaller amount of sample data to your model's working database for use at design time. Sample data is specified via a query on a sample partition used only during design. It's cached in-memory with the model. This will help you validate modeling decisions as you go without impacting the data source. You can test your modeling decisions with the sample dataset when using **Analyze in Excel** in SQL Server Data Tools (SSDT), or from other client applications that connect to your workspace database.  
  
> [!TIP]  
>  Even in DirectQuery mode on an empty model, you can always view a small built-in rowset for each table. In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], click **Table** > **Table Properties** to view the 50-row dataset.  
  
## Create a sample partition
 These instructions are for tabular models created at or upgraded to compatibility level 1200 or higher. Models at lower compatibility levels use different properties to get cached data. See [Enable DirectQuery mode in SSMS](../../analysis-services/tabular-models/enable-directquery-mode-in-ssms.md) for property descriptions.  
  
1.  In SQL Server Data Tools, in Diagram or Data View, click a fact table to open its properties page. Fact tables provide the aggregated, numeric data and measures in your model. You might have more than one.  
  
2.  Click **Table** > **Properties** to open the Partition Management dialog box.  
  
    Notice the default partition is **(Direct Query) \<table name>**. This is the full data view. Do not delete this partition. This partition will be used when the model is deployed.  
  
4.  Select the partition and then click **Copy**.  

    This creates a copy of the default partition, however, this copy will contain sample data you specify in a query. For example:
  
     ![ssas_tabularproject_copypartition](../../analysis-services/tabular-models/media/ssas-tabularproject-copypartition.jpg "ssas_tabularproject_copypartition")  
  
5.  Select the copied partition and then click the **SQL Query Editor** button to add a filter. Reduce your sample data size while authoring your model. For example, if you selected **FactInternetSales** from AdventureWorksDW, your filter might look like the following:  
  
    ```  
    SELECT [dbo].[FactInternetSales].* FROM [dbo].[FactInternetSales]  
    JOIN DimSalesTerritory as ST  
    ON ST.SalesTerritoryKey = FactInternetSales.SalesTerritoryKey  
    WHERE ST.SalesTerritoryGroup='North America';  
    ```  
  
6.  Click **Validate** to check for syntax errors.  
  
     Notice in DirectQuery mode, in addition to **New** , **Copy**, and **Delete** buttons on the Partitions dialog box, there is also a toggle button that alternately reads **Set as Sample** or **Set as DirectQuery**.  
  
     Only one partition can be the DirectQuery partition. You can control it by selecting any partition defined for the table and clicking **Set as Sample**.  
  
7.  Process the table .  
  


  
  
