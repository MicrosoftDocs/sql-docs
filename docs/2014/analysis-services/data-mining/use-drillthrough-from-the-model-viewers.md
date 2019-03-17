---
title: "Use Drillthrough from the Model Viewers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: e5e065ad-c688-4c2c-8c82-7f3038e04915
author: minewiskan
ms.author: owend
manager: craigg
---
# Use Drillthrough from the Model Viewers
  Depending on the model type, you can use drillthrough from the browse viewers on the **Mining Model Viewer** tab of Data Mining Designer to explore the cases used in the mining model or to see additional columns in the mining structure. Although many model types do not support drillthrough because the patterns in the model cannot be directly linked to specific cases, the following model types do support drillthrough.  
  
 Note that drillthrough must have been enabled on the model, and you must have the appropriate permissions. The drillthrough option might also be disabled if the model is in an unprocessed state, regardless of whether the model was previously processed and has content. To retrieve model case data by using drillthrough, the cache of the structure and model must be current.  
  
### Use drillthrough in the Microsoft Tree Viewer  
  
1.  In Data Mining Designer, select a decision trees model, and select **Browse Model** to open the model in the **Microsoft Tree Viewer**. In SQL Server Management Studio, right-click the model, and select **Browse**  
  
2.  Right-click any node in the tree graph, and select **Drill through**.  
  
3.  Select one of the following options: **Model Columns Only** or **Model and Structure Columns**. If you do not have permissions, an option might not be available.  
  
4.  The **Drill Through** dialog box opens, displaying the case data and/or structure data. The title bar of the dialog box also contains a description that identifies the node from which the drillthrough query was executed.  
  
5.  Right-click anywhere in the results and select **Copy All** to save the results to the Clipboard.  
  
### Use drillthrough in the Microsoft Cluster Viewer  
  
1.  In Data Mining Designer, select a clustering model, and select **Browse Model** to open the model in the **Microsoft Cluster Viewer**. In SQL Server Management Studio, right-click the model, and select **Browse**.  
  
2.  On the **Cluster** tab, right-click any node.  
  
3.  Select **Drill through**, and then select one of the following options: **Model Columns Only** or **Model and Structure Columns**. If you do not have permissions, an option might not be available.  
  
4.  The **Drill Through** dialog box opens, displaying the case data and/or structure data. The title bar of the dialog box also contains a description that identifies the cluster for the cases.  
  
5.  Right-click anywhere in the results and select **Copy All** to save the results to the Clipboard.  
  
### Use drillthrough in the Microsoft Association Rules Viewer  
  
1.  In Data Mining Designer, select an association model, and select **Browse Model** to open the model in the **Microsoft Association Rules Viewer**. In SQL Server Management Studio, right-click the model, and select **Browse**  
  
2.  On the **Rules** tab, right-click any row that represents a rule. On the **Itemsets** tab, click on any row that contains an itemset.  
  
3.  Select **Drill through**, and then select one of the following options: **Model Columns Only** or **Model and Structure Columns**. If you do not have permissions, an option might not be available.  
  
4.  The **Drill Through** dialog box opens, displaying the case data and/or structure data. The title bar of the dialog box also contains a description that identifies the rule name.  
  
5.  Right-click anywhere in the results and select **Copy All** to save the complete case results to the Clipboard. You can also select **Copy** to copy just the selected case. If the model contains a nested table column, only the name of the nested table column is pasted; to retrieve the data values inside the nested table column for each case you must create a query on the model content.  
  
### Use drillthrough in the Microsoft Sequence Cluster Viewer  
  
1.  In Data Mining Designer, select a clustering model, and select **Browse Model** to open the model in the **Microsoft Cluster Viewer**. In SQL Server Management Studio, right-click the model, and select **Browse**.  
  
2.  On the **Cluster Diagram tab**, right-click any node that represents a cluster. From the **Cluster Profiles** tab, click anywhere in a cluster profile or in the cluster representing the total model population.  
  
3.  Select **Drill through**, and then select one of the following options: **Model Columns Only** or **Model and Structure Columns**. If you do not have permissions, an option might not be available.  
  
4.  The **Drill Through** dialog box opens, displaying the case data and/or structure data. The title bar of the dialog box also contains a description that identifies the cluster for the cases.  
  
5.  Right-click anywhere in the results and select **Copy All** to save the results to the Clipboard. If the model contains a nested table column, only the name of the nested table column is pasted; to retrieve the data values inside the nested table column for each case you must create a query on the model content.  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Drillthrough on Mining Models](drillthrough-on-mining-models.md)   
 [Drillthrough on Mining Structures](drillthrough-on-mining-structures.md)  
  
  
