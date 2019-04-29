---
title: "Microsoft Generic Content Tree Viewer (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.contentviewer.f1"
ms.assetid: 751b4393-f6fd-48c1-bcef-bdca589ce34c
author: minewiskan
ms.author: owend
manager: craigg
---
# Microsoft Generic Content Tree Viewer (Data Mining)
  The **Microsoft Generic Content Tree Viewer** displays detailed information about the contents of a data mining mode in a standardized HTML table format. This view is useful because it exposes the underlying structure of the model, as well details about coefficients, the distribution of values, and much more.  
  
 The actual content displayed in the table varies depending on the algorithm that was used and might include columns, rules, properties, attributes, nodes, and formulas. For more information about model content, and how to interpret the information for each model type, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](data-mining/mining-model-content-analysis-services-data-mining.md).  
  
 The information that is displayed in the viewer uses a common structure that is based on the content schema rowset for mining models. The content schema rowset is a generic framework for storing patterns, statistics, and other contents of a data mining model. For a list of the columns in the data mining schema rowset for mining models, see [DMSCHEMA_MINING_MODEL_CONTENT Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-model-content-rowset).  
  
## Options  
 **Node caption (Unique ID)**  
 This pane displays a list of all the nodes in the selected mining model. The way the nodes are arranged in the tree is different depending on the type of model you are viewing.  
  
 You can click each node to display details about the node in the **Node details** pane.  
  
 **Node details**  
 Displays detailed information about the content of the selected node. Each node stores its information in a standardized format, but the contents and significance of each line of the table depends on the type of model or type of node that you are viewing. For example, the information stored for a node that represents a rule in an association model contains different information than a node that represents a tree in a decision trees model.  
  
 For information about how to interpret the node information for a specific model type, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](data-mining/mining-model-content-analysis-services-data-mining.md).  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Queries](data-mining/data-mining-queries.md)  
  
  
