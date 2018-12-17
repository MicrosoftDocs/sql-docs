---
title: "Intermediate Data Mining Tutorial (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 404b31d5-27f4-4875-bd60-7b2b8613eb1b
author: minewiskan
ms.author: owend
manager: craigg
---
# Intermediate Data Mining Tutorial (Analysis Services - Data Mining)
  [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides an integrated environment for creating and working with data mining models. You can easily bind to data sources, create and test multiple models on the same data, and deploy models for use in predictive analysis.  
  
 In the Basic Data Mining Tutorial, you learned how to use [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to create a data mining solution, and you built three models to support a targeted mailing campaign for analyzing customer purchasing behavior and for targeting potential buyers.  
  
 This intermediate tutorial builds on that experience and introduces several new scenarios, including common business requirements such as forecasting and market basket analysis. You will learn how to create a time series model, an association model, and a sequence clustering model. Finally, you will learn how to use neural network to explore correlations in data and to use logistic regression for predictions.  
  
 The lessons are independent and can be completed separately.  
  
 To complete the following tutorials, you should to be familiar with the data mining tools and with the mining model viewers that were introduced in the Basic Data Mining Tutorial.  
  
 All scenarios use the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] data source, but you will create different data source views for different scenarios. You can do the lessons in any order as long as you create the data source first.  
  
## Lesson Scenarios  
 After your success with the targeted mailing campaign, you have been asked to apply your knowledge of data mining to develop several new models for use in business planning. These include the following tasks:  
  
-   **Forecasting:** You will create a *time series* model, to forecast the sales of products in different regions around the world. You will develop individual models for each region and learn how to use *cross-prediction*.  
  
-   **Market basket analysis:** You will create an *association model*, to analyze groupings of products that are purchased during visits to the [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] e-commerce site. Based on this market basket model, you can recommend products to customers.  
  
-   **Sequence analysis:** You build a *sequence clustering model*, to analyze the order in which customers buy products. Based on this model, you can plan changes in Web site design or new product offerings.  
  
-   **Factor analysis:** You use a *neural network* model to explore the possible causes of poor service quality in call center data. Based on the insights from the preliminary model, you will create a *logistic regression model* to predict strategies for improving customer experience.  
  
## What You Will Learn  
 This tutorial teaches you how to create and work with several types of data mining algorithms. This tutorial is divided into the following lessons:  
  
 [Lesson 1: Creating the Intermediate Data Mining Solution &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-1-create-solution-intermediate-data-mining-tutorial.md)  
 In this lesson, you will create a new project based on the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database, to support several new data sources views and many more mining models.  
  
 [Lesson 2: Building a Forecasting Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-2-building-a-forecasting-scenario-intermediate-data-mining-tutorial.md)  
 In this lesson, you will create a mining model that can be used as part of a forecasting scenario. You will also explore mining models that are built with the [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm.  
  
 You will build models for individual regions, and then build a general model that can be used for cross-prediction.  
  
 [Lesson 3: Building a Market Basket Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-3-building-a-market-basket-scenario-intermediate-data-mining-tutorial.md)  
 In this lesson, you will add a new data source view and learn how to work with nested tables and keys. Based on this data, you will create a mining model that can be used as part of a market basket scenario. You will also explore mining models that are built with the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm.  
  
 [Lesson 4: Building a Sequence Clustering Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-4-build-sequence-clustering-scenario-intermediate-data-mining.md)  
 In this lesson, you will create a mining model that can be used as part of a sequence clustering scenario. You will also learn how to explore mining models that are built with the [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Clustering algorithm.  
  
 [Lesson 5: Building Neural Network and Logistic Regression Models &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-5-build-models-intermediate-data-mining-tutorial.md)  
 In this lesson, you will create several related mining models, using the Microsoft Neural Network and Microsoft Logistic Regression algorithms. You will also learn to work with data source views to explore data underlying the models.  
  
## Requirements  
 Make sure that the following are installed:  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database.  
  
 By default, the sample databases are not installed, to enhance security. To install the official databases for [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], visit the [Microsoft SQL Sample Databases](https://go.microsoft.com/fwlink/?LinkId=88417) page and select the appropriate version of the sample database.  
  
## See Also  
 [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md)   
 [Bike Buyer DMX Tutorial](../../2014/tutorials/bike-buyer-dmx-tutorial.md)   
 [Market Basket DMX Tutorial](../../2014/tutorials/market-basket-dmx-tutorial.md)  
  
  
