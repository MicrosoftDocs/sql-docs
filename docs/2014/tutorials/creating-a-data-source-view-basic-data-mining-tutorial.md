---
title: "Creating a Data Source View (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: c1e68a88-0f82-415d-becc-78d180d4f845
author: minewiskan
ms.author: owend
manager: craigg
---
# Creating a Data Source View (Basic Data Mining Tutorial)
  A data source view is built on a data source and defines a subset of the data, which you can then use in your mining structures. You can also use the data source view to add columns, create calculated columns and aggregates, and add named views. By using data source views, you can select the data that relates to your project, establish relationships between tables, and modify the structure of the data, without modifying the original data source. For more information, see [Data Source Views in Multidimensional Models](../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md).  
  
### To create a data source view  
  
1.  In **Solution Explorer**, right-click **Data Source Views**, and select **New Data Source View**.  
  
2.  On the **Welcome to the Data Source View Wizard** page, click **Next**.  
  
3.  On the **Select a Data Source** page, under **Relational data sources**, select the Adventure Works DW 2012 data source that you created in the last task. Click **Next**.  
  
    > [!NOTE]  
    >  If you want to create a data source, right-click **Data Sources** and then click **New Data Source** to start the Data Source Wizard.  
  
4.  On the **Select Tables and Views** page, select the following objects, and then click the right arrow to include them in the new data source view:  
  
    -   **ProspectiveBuyer (dbo)** - table of prospective bike buyers  
  
    -   **vTargetMail (dbo)** - view of historical data about past bike buyers  
  
5.  Click **Next**.  
  
6.  On the **Completing the Wizard** page, by default the data source view is named Adventure Works DW 2012. Change the name to `Targeted Mailing`, and then click **Finish**.  
  
     The new data source view opens in the **Targeted Mailing.dsv [Design]** tab.  
  
## Previous Task in Lesson  
 [Creating a Data Source &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/creating-a-data-source-basic-data-mining-tutorial.md)  
  
## Next Lesson  
 [Lesson 2: Building a Targeted Mailing Structure &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/lesson-2-building-a-targeted-mailing-structure-basic-data-mining-tutorial.md)  
  
## See Also  
 [Defining a Data Source View &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/defining-a-data-source-view-analysis-services.md)  
  
  
