---
title: "Time Series Prediction DMX Tutorial | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 38ea7c03-4754-4e71-896a-f68cc2c98ce2
author: minewiskan
ms.author: owend
manager: craigg
---
# Time Series Prediction DMX Tutorial
  In this tutorial, you will learn how to create a time series mining structure, create three custom time series mining models, and then make predictions by using those models.  
  
 The mining models are based on the data contained in the  [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] sample database, which stores data for the fictitious company [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)]. [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is a large, multinational manufacturing company.  
  
## Tutorial Scenario  
 [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] has decided to use data mining to generate sales projections. They have already built some regional forecasting models; for more information, see [Lesson 2: Building a Forecasting Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-2-building-a-forecasting-scenario-intermediate-data-mining-tutorial.md). However, the Sales Department needs to be able to periodically update the data mining model with new sales data. They also want to customize the models to provide different projections.  
  
 [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides several tools that can be used to accomplish this task:  
  
-   The Data Mining Extensions (DMX) query language  
  
-   The Microsoft Time Series Algorithm  
  
-   Query Editor in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]  
  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm creates models that can be used for prediction of time-related data. Data Mining Extensions (DMX) is a query language provided by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you can use to create mining models and prediction queries.  
  
## What You Will Learn  
 This tutorial assumes that you are already familiar with the objects that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses to create mining models. If you have not previously created a mining structure or mining model by using DMX, see [Bike Buyer DMX Tutorial](../../2014/tutorials/bike-buyer-dmx-tutorial.md).  
  
 This tutorial is divided into the following lessons:  
  
 [Lesson 1: Creating a Time Series Mining Model and Mining Structure](../../2014/tutorials/lesson-1-creating-a-time-series-mining-model-and-mining-structure.md)  
 In this lesson, you will learn how to use the `CREATE MINING MODEL` statement to add a new forecasting model and a related mining model.  
  
 [Lesson 2: Adding Mining Models to the Time Series Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-time-series-mining-structure.md)  
 In this lesson, you will learn how to use the ALTER MINING STRUCTURE statement to add new mining models to the time series structure. You will also learn how to customize the algorithm used for analyzing a time series.  
  
 [Lesson 3: Processing the Time Series Structure and Models](../../2014/tutorials/lesson-3-processing-the-time-series-structure-and-models.md)  
 In this lesson, you will learn how to train the models by using the `INSERT INTO` statement and populating the structure with data from the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database.  
  
 [Lesson 4: Creating Time Series Predictions Using DMX](../../2014/tutorials/lesson-4-creating-time-series-predictions-using-dmx.md)  
 In this lesson, you will learn how to create time series predictions.  
  
 [Lesson 5: Extending the Time Series Model](../../2014/tutorials/lesson-5-extending-the-time-series-model.md)  
 In this lesson, you will learn how to use the `EXTEND_MODEL_CASES` parameter to update the model with new data when you make predictions.  
  
## Requirements  
 Before doing this tutorial, make sure that the following are installed:  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]  
  
-   The [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database  
  
 By default, the sample databases are not installed, to enhance security. To install the official sample databases for [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], go to [http://www.CodePlex.com/MSFTDBProdSamples](https://go.microsoft.com/fwlink/?LinkId=88417) or on the Microsoft SQL Server Samples and Community Projects home page in the section Microsoft SQL Server Product Samples. Click **Databases**, then click the **Releases** tab and select the databases that you want.  
  
> [!NOTE]  
>  When you review tutorials, we recommend that you add **Next topic** and **Previous topic** buttons to the document viewer toolbar.  
  
## See Also  
 [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md)   
 [Intermediate Data Mining Tutorial &#40;Analysis Services - Data Mining&#41;](../../2014/tutorials/intermediate-data-mining-tutorial-analysis-services-data-mining.md)  
  
  
