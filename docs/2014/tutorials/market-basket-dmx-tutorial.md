---
title: "Market Basket DMX Tutorial | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "DMX [Analysis Services], tutorials"
  - "data mining [Analysis Services], tutorials"
  - "statements [DMX], tutorials"
  - "Data Mining Extensions [Analysis Services], tutorials"
  - "market basket analysis [Analysis Services]"
  - "tutorials [Data Mining]"
  - "tutorials [DMX]"
ms.assetid: 6e262a1d-c89e-4033-8368-46cf25168ef5
author: minewiskan
ms.author: owend
manager: craigg
---
# Market Basket DMX Tutorial
  In this tutorial, you will learn how to create, train, and explore mining models by using the Data Mining Extensions (DMX) query language. You will then use these mining models to create predictions that describe which products tend to be purchased at the same time.  
  
 The mining models will be created from the data contained in the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] sample database, which stores data for the fictitious company [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)]. [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, European, and Asian commercial markets. Its base operation is located in Bothell, Washington, with 290 employees, and it has several regional sales teams are located throughout their international market base.  
  
## Tutorial Scenario  
 [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] has decided to create a custom application that employs data mining functionality to predict what types of products their customers tend to purchase at the same time. The goal for the custom application is to be able to specify a set of products, and predict what additional products will be purchased with the specified products. [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] will then use this information to add a "suggest" feature to their website, and also to better organize the way that they present information to their customers.  
  
 [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides several tools that can be used to accomplish this task:  
  
-   The DMX query language  
  
-   The [Microsoft Association Algorithm](../../2014/analysis-services/data-mining/microsoft-association-algorithm.md)  
  
-   Query Editor in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]  
  
 Data Mining Extensions (DMX) is a query language provided by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you can use to create and work with mining models. The [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm creates models that can predict the products that are likely to be purchased together.  
  
 The goal of this tutorial is to provide the DMX queries that will be used in the custom application.  
  
 **For more information:** [Data Mining Solutions](../../2014/analysis-services/data-mining/data-mining-solutions.md)  
  
## Mining Structure and Mining Models  
 Before you begin to create DMX statements, it is important to understand the main objects that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses to create mining models. The *mining structure* is a data structure that defines the data domain from which mining models are built. A single mining structure can contain multiple *mining models* that share the same domain. A mining model applies a mining model algorithm to the data, which is represented by a mining structure.  
  
 The building blocks of the mining structure are the mining structure columns, which describe the data that the data source contains. These columns contain information such as data type, content type, and how the data is distributed.  
  
 Mining models must contain the key column described in the mining structure, as well as a subset of the remaining columns. The mining model defines the usage for each column and defines the algorithm that is used to create the mining model. For example, in DMX you can specify that a column is a Key column or a PREDICT column. If a column is left unspecified, it is assumed to be an input column.  
  
 In DMX, there are two ways to create mining models. You can either create the mining structure and associated mining model together by using the `CREATE MINING MODEL` statement, or you can first create a mining structure by using the `CREATE MINING STRUCTURE` statement, and then add a mining model to the structure by using the `ALTER STRUCTURE` statement. These methods are described below.  
  
 `CREATE MINING MODEL`  
 Use this statement to create a mining structure and associated mining model together using the same name. The mining model name is appended with "Structure" to differentiate it from the mining structure.  
  
 This statement is useful if you are creating a mining structure that will contain a single mining model.  
  
 For more information, see [CREATE MINING MODEL &#40;DMX&#41;](/sql/dmx/create-mining-model-dmx).  
  
 CREATE MINING STRUCTURE  
 Use this statement to create a new mining structure without any models.  
  
 When you use CREATE MINING STRUCTURE, you can also create a holdout data set that can be used for testing any models that are based on the same mining structure.  
  
 For more information, see [CREATE MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/create-mining-structure-dmx).  
  
 `ALTER MINING STRUCTURE`  
 Use this statement to add a mining model to a mining structure that already exists on the server.  
  
 There are several reasons that you would want to add more than one mining model in a single mining structure. For example, you might create several mining models using different algorithms to see which one works best. Alternatively, you might create several mining models using the same algorithm, but with a parameter set differently for each mining model to find the best setting for that parameter.  
  
 For more information, see [ALTER MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/alter-mining-structure-dmx?view=sql-server-2016).  
  
 Because you will create a mining structure that contains several mining models, you will use the second method in this tutorial.  
  
 **For More Information**  
  
 [Data Mining Extensions &#40;DMX&#41; Reference](/sql/dmx/data-mining-extensions-dmx-reference), [Understanding the DMX Select Statement](/sql/dmx/understanding-the-dmx-select-statement), [Structure and Usage of DMX Prediction Queries](/sql/dmx/structure-and-usage-of-dmx-prediction-queries)  
  
## What You Will Learn  
 This tutorial is divided into the following lessons:  
  
 [Lesson 1: Creating the Market Basket Mining Structure](../../2014/tutorials/lesson-1-creating-the-market-basket-mining-structure.md)  
 In this lesson, you will learn how to use the `CREATE` statement to create mining structures.  
  
 [Lesson 2: Adding Mining Models to the Market Basket Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-market-basket-mining-structure.md)  
 In this lesson, you will learn how to use the `ALTER` statement to add mining models to a mining structure.  
  
 [Lesson 3: Processing the Market Basket Mining Structure](../../2014/tutorials/lesson-3-processing-the-market-basket-mining-structure.md)  
 In this lesson, you will learn how to use the `INSERT INTO` statement to process mining structures and their associated mining models.  
  
 [Lesson 4: Executing Market Basket Predictions](../../2014/tutorials/lesson-4-executing-market-basket-predictions.md)  
 In this lesson, you will learn how to use the `PREDICTION JOIN` statement to create predictions against mining models.  
  
## Requirements  
 Before doing this tutorial, make sure that the following are installed:  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]  
  
-   The [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database  
  
 By default, the sample databases are not installed, to enhance security. To install the official sample databases for [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], go to [http://www.CodePlex.com/MSFTDBProdSamples](https://go.microsoft.com/fwlink/?LinkId=88417) or on the Microsoft SQL Server Samples and Community Projects home page in the section Microsoft SQL Server Product Samples. Click **Databases**, then click the **Releases** tab and select the databases that you want.  
  
> [!NOTE]  
>  When you review tutorials, we recommend that you add **Next topic** and **Previous topic** buttons to the document viewer toolbar.  
  
## See Also  
 [Bike Buyer DMX Tutorial](../../2014/tutorials/bike-buyer-dmx-tutorial.md)   
 [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md)   
 [Lesson 3: Building a Market Basket Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-3-building-a-market-basket-scenario-intermediate-data-mining-tutorial.md)  
  
  
