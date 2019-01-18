---
title: "Bike Buyer DMX Tutorial | Microsoft Docs"
ms.custom: ""
ms.date: "10/19/2018"
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
  - "tutorials [Data Mining]"
ms.assetid: 4b634cc1-86dc-42ec-9804-a19292fe8448
author: minewiskan
ms.author: owend
manager: craigg
---
# Bike Buyer DMX Tutorial
  In this tutorial, you will learn how create, train, and explore mining models by using the Data Mining Extensions (DMX) query language. You will then use these mining models to create predictions that determine whether a customer will purchase a bicycle.  
  
 The mining models will be created from the data contained in the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] sample database, which stores data for the fictitious company [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)]. [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, European, and Asian commercial markets. Its base operation is located in Bothell, Washington, with 290 employees, and it has several regional sales teams located throughout their international market base.  
  
## Tutorial Scenario  
 [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] has decided to extend their data analysis by creating a custom application that uses data mining functionality. Their goal for the custom application is to be able to:  
  
-   Take as input specific characteristics about a potential customer and predict whether they will buy a bicycle.  
  
-   Take as input a list of potential customers, as well as characteristics about the customers, and predict which ones will buy a bicycle.  
  
 In the first case, customer data is provided by a customer registration page, and in the second case, a list of potential customers is provided by the [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] marketing department.  
  
 In addition, the marketing department has asked for the ability to group existing customers into categories based on characteristics such as where they live, the number of children they have, and their commute distance. They want to see whether these clusters can be used to help target specific kinds of customers. This will require an additional mining model.  
  
 [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides several tools that can be used to accomplish these tasks:  
  
-   The DMX query language  
  
-   The [Microsoft Decision Trees Algorithm](../../2014/analysis-services/data-mining/microsoft-decision-trees-algorithm.md) and the [Microsoft Clustering Algorithm](../../2014/analysis-services/data-mining/microsoft-clustering-algorithm.md)  
  
-   Query Editor in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]  
  
 Data Mining Extensions (DMX) is a query language provided by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you can use to create and work with mining models. The [!INCLUDE[msCoName](../includes/msconame-md.md)] Decision Trees algorithm creates models that can be used to predict whether someone will purchase a bicycle. The resulting model can take an individual customer or a table of customers as an input. The [!INCLUDE[msCoName](../includes/msconame-md.md)] Clustering algorithm can create groupings of customers based on shared characteristics. The goal of this tutorial is to provide the DMX scripts that will be used in the custom application.  
  
 **For more information:** [Data Mining Solutions](../../2014/analysis-services/data-mining/data-mining-solutions.md)  
  
## Mining Structure and Mining Models  
 Before you begin to create DMX statements, it is important to understand the main objects that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses to create mining models. The mining structure is a data structure that defines the data domain from which mining models are built. A single mining structure can contain multiple mining models that share the same domain. A mining model applies a mining model algorithm to the data, which is represented by a mining structure.  
  
 The building blocks of the mining structure are the mining structure columns, which describe the data that the data source contains. These columns contain information such as data type, content type, and how the data is distributed.  
  
 Mining models must contain the key column described in the mining structure, as well as a subset of the remaining columns. The mining model defines the usage for each column and defines the algorithm that is used to create the mining model. For example, in DMX you can specify that a column is a Key column or a PREDICT column. If a column is left unspecified, it is assumed to be an input column.  
  
 In DMX, there are two ways to create mining models. You can either create the mining structure and associated mining model together by using the CREATE MINING MODEL statement, or you can first create a mining structure by using the CREATE MINING STRUCTURE statement, and then add a mining model to the structure by using the ALTER STRUCTURE statement. These methods are described in the following table.  
  
 CREATE MINING MODEL  
 Use this statement to create a mining structure and associated mining model together using the same name. The mining model name is appended with "Structure" to differentiate it from the mining structure. This statement is useful if you are creating a mining structure that will contain a single mining model.  
  
 For more information, see [CREATE MINING MODEL &#40;DMX&#41;](/sql/dmx/create-mining-model-dmx).  
  
 ALTER MINING STRUCTURE  
 Use this statement to add a mining model to a mining structure that already exists on the server. This statement is useful if you want to create a mining structure that contains several different mining models. There are several reasons that you would want to add more than one mining model in a single mining structure. For example, you might create several mining models that use different algorithms to see which algorithm works best. You might create several mining models that use the same algorithm, but with a parameter set differently for each mining model to find the best setting for the parameter.  
  
 For more information, see [ALTER MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/alter-mining-structure-dmx?view=sql-server-2016).  
  
 Because you will create a mining structure that contains several mining models, you will use the second method in this tutorial.  
  
 **For More Information**  
  
 [Data Mining Extensions &#40;DMX&#41; Reference](/sql/dmx/data-mining-extensions-dmx-reference), [Understanding the DMX Select Statement](/sql/dmx/understanding-the-dmx-select-statement), [Structure and Usage of DMX Prediction Queries](/sql/dmx/structure-and-usage-of-dmx-prediction-queries)  
  
## What You Will Learn  
 This tutorial is divided into the following lessons:  
  
 [Lesson 1: Creating the Bike Buyer Mining Structure](../../2014/tutorials/lesson-1-creating-the-bike-buyer-mining-structure.md)  
 In this lesson, you will learn how to use the `CREATE` statement to create mining structures.  
  
 [Lesson 2: Adding Mining Models to the Bike Buyer Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-bike-buyer-mining-structure.md)  
 In this lesson, you will learn how to use the `ALTER` statement to add mining models to a mining structure.  
  
 [Lesson 3: Processing the Bike Buyer Mining Structure](../../2014/tutorials/lesson-3-processing-the-bike-buyer-mining-structure.md)  
 In this lesson you will learn how to use the `INSERT INTO` statement to process mining structures and their associated mining models.  
  
 [Lesson 4: Browsing the Bike Buyer Mining Models](../../2014/tutorials/lesson-4-browsing-the-bike-buyer-mining-models.md)  
 In this lesson, you will learn how to use the `SELECT` statement to explore the content of the mining models.  
  
 [Lesson 5: Executing Prediction Queries](../../2014/tutorials/lesson-5-executing-prediction-queries.md)  
 In this lesson, you will learn how to use the `PREDICTION JOIN` statement to create predictions against mining models.  
  
## Requirements  
 Before doing this tutorial, make sure that the following are installed:  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssASversion2005](../includes/ssasversion2005-md.md)], [!INCLUDE[ssASversion10](../includes/ssasversion10-md.md)], [!INCLUDE[ssASCurrent](../includes/ssascurrent-md.md)], or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]  
  
-   The [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database. By default, the sample databases are not installed, to enhance security. To install official sample databases for [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], visit the [Microsoft SQL Sample Databases](https://go.microsoft.com/fwlink/?LinkId=88417) page and select the databases that you want to install..  
  
> [!NOTE]  
>  When you review tutorials, we recommend that you add **Next topic** and **Previous topic** buttons to the document viewer toolbar.  
  
## See Also  
 [Market Basket DMX Tutorial](../../2014/tutorials/market-basket-dmx-tutorial.md)   
 [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md)  
  
  
