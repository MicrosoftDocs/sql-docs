---
title: "Data Mining Query Tasks and How-tos | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Query Tasks and How-tos
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The ability to create queries is critical if you are to make use of your data mining models. This section provides links to examples of how to create queries against a data mining model by using the tools provided in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. If you need more information about what a data mining query is, or the different types of queries you can create, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md).  
  
## Creating Queries with Prediction Query Builder  
 The Prediction Query Builder is provided in both [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] as a way of graphically building queries against data mining models. The following topics explain how you can select a model, specify a data source, customize the predictions, and save output.  
  
-   [Create a Prediction Query Using the Prediction Query Builder](../../analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md)  
  
-   [Create a Singleton Query in the Data Mining Designer](../../analysis-services/data-mining/create-a-singleton-query-in-the-data-mining-designer.md)  
  
-   [Create a Prediction Query Using the Prediction Query Builder](../../analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md)  
  
-   [View and Save the Results of a Prediction Query](../../analysis-services/data-mining/view-and-save-the-results-of-a-prediction-query.md)  
  
-   [Manually Edit a Prediction Query](../../analysis-services/data-mining/manually-edit-a-prediction-query.md)  
  
-   [Apply Prediction Functions to a Model](../../analysis-services/data-mining/apply-prediction-functions-to-a-model.md)  
  
-   [Choose and Map Input Data for a Prediction Query](../../analysis-services/data-mining/choose-and-map-input-data-for-a-prediction-query.md)  
  
## Using Other Data Mining Query Tools  
 In addition to using the Prediction Query Builder, you can type a query directly into [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by using DMX or by using XMLA. You can also build prediction queries programmatically and send them to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server. The following topics provide more information about how to create and work with prediction queries outside of the Prediction Query Builder.  
  
 [Create a Singleton Prediction Query from a Template](../../analysis-services/data-mining/create-a-singleton-prediction-query-from-a-template.md)  
 Describes how to use the tools in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to build and run a prediction query.  
  
 [Create a Singleton Prediction Query from a Template](../../analysis-services/data-mining/create-a-singleton-prediction-query-from-a-template.md)  
 Describes how to use the templates that are provided in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to add parameters to a prediction query.  
  
 [Change the Time-out Value for Data Mining Queries](../../analysis-services/data-mining/change-the-time-out-value-for-data-mining-queries.md)  
 Describes how to set properties on the server that control behavior related to data mining queries.  
  
 [Create a Content Query on a Mining Model](../../analysis-services/data-mining/create-a-content-query-on-a-mining-model.md)  
 Describes how to create queries that return detailed information that is stored in the mining model by using the data mining schema rowsets.  
  
 [Create a Data Mining Query by Using XMLA](../../analysis-services/data-mining/create-a-data-mining-query-by-using-xmla.md)  
 Describes how to create a query against mining model content by using the XMLA templates in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## See Also  
 [Query and Expression Language Reference &#40;Analysis Services&#41;](http://msdn.microsoft.com/library/9597533d-35f4-4742-9d8c-7af392163527)   
 [Data Mining Stored Procedures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-stored-procedures-analysis-services-data-mining.md)  
  
  
