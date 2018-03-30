---
title: "Browsing Models in Excel (SQL Server Data Mining Add-ins) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "mining models, browsing"
  - "browse models"
  - "mining models, viewing"
ms.assetid: a8cca1d7-602a-449a-875c-99da564965bc
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Browsing Models in Excel (SQL Server Data Mining Add-ins)
  ![Browse Model button in Data Mining ribbon](../../2014/analysis-services/media/dmc-browse.gif "Browse Model button in Data Mining ribbon")  
  
 Visually exploring the model is usually the fastest and easiest way to get an understanding of the rules and relationships discovered by analysis. By using the Data Mining Client for Excel, you can browse both temporary models created during the current Excel session, and models stored in an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 To browse a model, you select a model and its associated structure from a list of available models, and the add-in automatically picks the viewer that is appropriate for that data mining algorithm. You can drill into the most interesting trends, filter the completed data mining model, and copy graphs and data to Excel or to Visio.  
  
## Using the Browse Model Wizard  
  
1.  Click the **Data Mining** tab.  
  
2.  In the **Model Usage** group, click **Browse**.  
  
3.  In the **Select Model** dialog box, choose a mining model from the list, and click **Next**.  
  
4.  The wizard opens a **Browse** window that is appropriate for the type of model that you selected.  
  
## List of Data Mining Viewers  
 Depending on the data mining algorithm that you used when you created the model, the **Browse** window will look a bit different. It can include graphs to help interpret the results, legends that contain additional detail, and controls for interacting with the data.  
  
 The following topics provide guidance in how to use each of the viewers, including tips on interpreting the complex graphs, and how to change, copy, or work with the results.  
  
 [Browsing an Association Rules Model](../../2014/analysis-services/browsing-an-association-rules-model.md)  
  
 [Browsing a Clustering Model](../../2014/analysis-services/browsing-a-clustering-model.md)  
  
 [Browsing a Decision Trees Model](../../2014/analysis-services/browsing-a-decision-trees-model.md)  
  
 [Browsing a Forecasting Model](../../2014/analysis-services/browsing-a-forecasting-model.md)  
  
 [Browsing a Naive Bayes Model](../../2014/analysis-services/browsing-a-naive-bayes-model.md)  
  
 [Browsing a Neural Network Model](../../2014/analysis-services/browsing-a-neural-network-model.md)  
  
## See Also  
 [Viewing Data Mining Models in Visio &#40;Data Mining Add-ins&#41;](../../2014/analysis-services/viewing-data-mining-models-in-visio-data-mining-add-ins.md)   
 [Manage Models &#40;SQL Server Data Mining Add-ins&#41;](../../2014/analysis-services/manage-models-sql-server-data-mining-add-ins.md)  
  
  