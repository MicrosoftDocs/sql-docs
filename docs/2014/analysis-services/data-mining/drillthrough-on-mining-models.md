---
title: "Drillthrough on Mining Models | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: f179a467-7d03-4d61-8e9a-6b5afb5fc2d5
author: minewiskan
ms.author: owend
manager: craigg
---
# Drillthrough on Mining Models
  *Drillthrough* means the ability to query either a mining model or a mining structure and get detailed data that is not exposed in the model.  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] provides two different options for drilling through into case data. You can drill through to the cases that were used to build the data, or you can drill through to the cases in the mining structure.  
  
## Drillthrough to Model Cases vs. Drillthrough to Structure  
 Drilling through to **model cases** is useful for finding additional details about rules, patterns or clusters in a model. For example, you would not use customer contact information for analysis in a clustering model, even if the data was available, by using drillthrough, you can gain access to that information from the model.  
  
 In contrast, **drillthrough to structure** data is intended to provide access to information that was not made available in the model. For example, some structure columns might have been excluded from a model because the data type was incompatible or the data was not useful for analysis.  
  
## Enabling Drillthrough on a Model  
 To use drillthrough on a mining model, the following conditions must be met:  
  
-   It is possible to configure drillthrough on just the model cases and not on the mining structure, but not vice versa.  In other words, drillthrough must be enabled on the mining model to permit drillthrough to the mining structure.  
  
-   Drillthrough on both model and structure is disabled by default. If you use the Data Mining Wizard, the option to enable drillthrough to the model cases is on the final page of the wizard.  
  
-   You can add the ability to drill through on an existing mining model, but if you do, the model must be reprocessed before you can drill through to the data.  
  
-   Drillthrough will not work unless the cache that was created during the training process has been preserved. For more information about the properties that control caching, see [Drillthrough on Mining Structures](drillthrough-on-mining-structures.md).  
  
## Models that Support Drillthrough  
 If a mining model has been configured to allow drillthrough, and if you have the appropriate permissions, when you browse the model, you can click on a node in the appropriate viewer and retrieve detailed information about the cases in that particular node.  
  
 Not all models support drillthrough; it depends on the algorithm that was used to create the model. The following table lists the types of models that do not support drillthrough, or support drillthrough with limitations. If the model type is not listed here, drillthrough is supported.  
  
|**Algorithm name**|**Support for drillthrough**|  
|------------------------|----------------------------------|  
|Microsoft Naïve Bayes algorithm|Not supported.<br /><br /> These algorithms do not assign cases to specific nodes in the content.|  
|Microsoft Neural Network algorithm|Not supported.<br /><br /> These algorithms do not assign cases to specific nodes in the content.|  
|Microsoft Logistic Regression algorithm|Not supported.<br /><br /> These algorithms do not assign cases to specific nodes in the content.|  
|Microsoft Linear Regression algorithm|Supported.<br /><br /> However, because the model creates a single node, `All`, drillthrough returns all the training cases for the model. If the training set is large, loading the results may take a very long time.|  
|Microsoft Time Series algorithm|Supported.<br /><br /> However, you cannot drill through to structure or case data by using the **Mining Model Viewer** in Data Mining Designer. You must create a DMX query instead.<br /><br /> Also, you cannot drill through to specific nodes, or write a DMX query to retrieve cases in specific nodes of a time series model. You can retrieve case data from either the model or the structure by using other criteria, such as date or attribute values.<br /><br /> If you wish to view details of the ARTXP and ARIMA nodes created by the Microsoft Time Series algorithm, it might be easier to use the [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).|  
  
## Related Tasks  
 See the following topics for more information about how to use drillthrough with mining models.  
  
|Tasks|Links|  
|-----------|-----------|  
|Use drillthrough in the mining model viewers|[Use Drillthrough from the Model Viewers](use-drillthrough-from-the-model-viewers.md)|  
|Retrieve case data for a model by using drillthrough|[Drill Through to Case Data from a Mining Model](drill-through-to-case-data-from-a-mining-model.md)|  
|Enable drillthrough on an existing mining model|[Enable Drillthrough for a Mining Model](enable-drillthrough-for-a-mining-model.md)|  
|See examples of drillthrough queries for specific model types.|[Data Mining Queries](data-mining-queries.md)|  
|Enable drillthrough in the Mining Model Wizard|[Completing the Wizard &#40;Data Mining Wizard&#41;](../completing-the-wizard-data-mining-wizard.md).|  
  
## See Also  
 [Drillthrough on Mining Structures](drillthrough-on-mining-structures.md)  
  
  
