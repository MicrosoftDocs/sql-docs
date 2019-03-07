---
title: "Input Selection Tab (Mining Accuracy Chart View) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.accuracychart.columnmapping.f1"
ms.assetid: f8b1193c-5c86-4c7e-8e35-158d293184fa
author: minewiskan
ms.author: owend
manager: craigg
---
# Input Selection Tab (Mining Accuracy Chart View)
  Use the **Input Selection** tab of the **Mining Accuracy Chart** designer to specify the source of the data that is used to test the model and build the accuracy chart.  
  
 **For more information:** [Testing and Validation &#40;Data Mining&#41;](data-mining/testing-and-validation-data-mining.md)  
  
## Options  
 **Synchronize Prediction**  **Columns and Values**  
 Select to coordinate the predictable attributes in the grid so that, even if they have a different name, they are derived from the same predictable mining structure column during model training.  
  
 **Note** This option is selected by default. You should only clear this box for cases in which you know that two mining structure columns derive from the same underlying relational or multi-dimensional source, and that the columns contain the same states or have been discretized in the same way.  
  
 **Select predictable mining model columns to show in the lift chart**  
 A grid that contains columns to control which models are included in the lift chart and how they are used in the lift chart.  
  
|Value|Description|  
|-----------|-----------------|  
|**Show**|Select the box next to the name of each predictable column in the mining model that you want to display in the chart.<br /><br /> If the chart is too complex to view easily, clear the box next to one or more columns to simplify the chart.<br /><br /> Note: You cannot create an accuracy chart unless at least one column is selected.|  
|**Mining Model**|Lists the mining models that are contained in the mining structure.|  
|**Predictable Column Name**|Select a predictable column that is contained in the mining models that are used to create the lift chart.|  
|**Predict Value**|Select a value for the predictable column. If you leave this blank, the lift chart predicts how well the model performs for all states of the predictable column.|  
  
 **Select data set to be used for Accuracy Chart**  
 An option group that contains three options for specifying accuracy test data.  
  
|Value|Description|  
|-----------|-----------------|  
|**Use mining model test cases**|Use the testing set that was created when you partitioned the mining structure, and apply the filter that is defined on the model. For information about model filters, see [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](data-mining/mining-models-analysis-services-data-mining.md)|  
|**Use mining structure test cases**|Use the testing set that was created when you partitioned the mining structure.|  
|**Specify a different data set**|Specify a table from an existing data source view to use as a test data set.|  
  
## Filtering Options  
 If you select the option **Specify a different data set**, you can define a data source view and create filters to apply to that data. When you create a filter, you are creating a WHERE clause in the query that returns the test data from the data source view.  
  
 **Note** You cannot specify a filter on the mining model by using the **Input Selection** tab. To create a model filter, click the **Mining Models** tab and edit the model properties.  
  
 If you did not create a holdout set for testing when you created the mining structure, you can select this option and then specify the original data source view as a test set. By using  this workaround, you can also set filters on the original data set.  
  
 **Specify Column Mapping**  
 Opens the **Specify Column Mapping** dialog box, where you select the data source, specify case and nested tables, and map external data columns to the mining structure columns.  
  
 For more information, see [Specify Column Mapping Dialog Box &#40;Mining Accuracy Chart&#41;](specify-column-mapping-dialog-box-mining-accuracy-chart.md).  
  
 **Filter Expression**  
 Displays the filter condition that you built by using the filter editors.  
  
 **Open Filter Editor**  
 Opens the **Data Set Filter** dialog box, which lets you select external tables, and set conditions on case table columns, and the **Filter** dialog box, which helps you build conditions that apply to individual columns in the selected table, or to columns in nested tables.  
  
## See Also  
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)   
 [Mining Accuracy Chart Designer &#40;Data Mining&#41;](mining-accuracy-chart-designer-data-mining.md)   
 [Apply a Filter to a Mining Model](data-mining/apply-a-filter-to-a-mining-model.md)   
 [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](data-mining/mining-models-analysis-services-data-mining.md)  
  
  
