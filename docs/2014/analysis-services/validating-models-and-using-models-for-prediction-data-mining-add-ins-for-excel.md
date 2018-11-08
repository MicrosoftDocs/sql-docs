---
title: "Validating Models and Using Models for Prediction (Data Mining Add-ins for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining models, validating"
  - "mining models, charting"
  - "validation [data mining]"
  - "mining models, testing"
ms.assetid: e245ac1f-1230-48e9-9091-e70b131aa2a8
author: minewiskan
ms.author: owend
manager: craigg
---
# Validating Models and Using Models for Prediction (Data Mining Add-ins for Excel)
  Testing and validating your model is an important step in the data mining process. You must know how well your mining models perform against real data before you deploy the models into a production environment.  
  
 The Data Mining Add-ins include tools that help you test the models you built, and create predictions and recommendations using the models.  
  
## Accuracy Chart  
 The **Accuracy Chart** wizard helps you create a prediction query and assess the performance of a data mining model by creating a lift chart or scatter plot chart. The lift chart helps distinguish between models in a structure that are almost the same, to help you determine which model provides the best predictions.  
  
 [Accuracy Chart &#40;SQL Server Data Mining Add-ins&#41;](accuracy-chart-sql-server-data-mining-add-ins.md)  
  
## Classification Matrix  
 The **Classification Matrix** wizard helps you create a prediction query to assess the performance of a classification model. The output is a chart that summarizes both accurate and inaccurate predictions made by the model. The matrix is a valuable tool because it not only shows how frequently the model correctly predicted a value, but also shows which values the model most frequently predicted incorrectly.  
  
 [Classification Matrix &#40;SQL Server Data Mining Add-ins&#41;](classification-matrix-sql-server-data-mining-add-ins.md)  
  
## Profit Chart  
 The **Profit Chart** wizard helps you weigh the benefits of using a data mining model and assess the costs of both false positives and false negatives  
  
 This chart type measures the prediction accuracy of the model and incorporates unit and overall costs that you specify.  
  
 [Profit Chart &#40;SQL Server Data Mining Add-ins&#41;](profit-chart-sql-server-data-mining-add-ins.md).  
  
## Cross-Validation  
 Cross-validation is an established technique in the data mining community for assessing the validity of a data set and the accuracy of a mining model on that data set. It divides a set of data into subsets, and then iteratively creates, trains, and tests models on each subset.  
  
 The **Cross-Validation** wizard lets you specify the number of folds to divide your data by, and then provides a cross-validation report that statistically describes the differences among these cross-sections. From this you can determine whether the model performs well on all training data, or might be skewed to a particular subset.  
  
 [Cross-Validation &#40;SQL Server Data Mining Add-ins&#41;](cross-validation-sql-server-data-mining-add-ins.md)  
  
## Query Wizard  
 The **Query** wizard is an interactive tool that helps you build a prediction query. Queries are how you generate recommendations, future forecasts, and so forth.  
  
 In the **Query** wizard, you pick a model, and then provide input data, either as single values or from a table or range, and the wizard helps you select columns to output. You can also add functions to your query to generate probability scores and other useful statistics.  
  
 [Query &#40;SQL Server Data Mining Add-ins&#41;](query-sql-server-data-mining-add-ins.md)  
  
## Advanced Query Editor  
 The **Advanced Query Editor** is an interactive set of dialog boxes that helps you build all kinds of DMX statements, anything from running custom queries to creating and training new models, deleting models, or creating new data sets.  
  
 [Advanced Data Mining Query Editor](advanced-data-mining-query-editor.md)  
  
## See Also  
 [Exploring and Cleaning Data](exploring-and-cleaning-data.md)   
 [Creating a Data Mining Model](creating-a-data-mining-model.md)   
 [Deploying and Scaling Mining Models &#40;Data Mining Add-ins for Excel&#41;](deploying-and-scaling-mining-models-data-mining-add-ins-for-excel.md)  
  
  
