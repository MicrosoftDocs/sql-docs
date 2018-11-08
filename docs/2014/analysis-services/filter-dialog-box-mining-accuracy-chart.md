---
title: "Filter Dialog Box (Mining Accuracy Chart) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 71e884a9-7ec4-4459-a4c4-87f6c796d478
author: minewiskan
ms.author: owend
manager: craigg
---
# Filter Dialog Box (Mining Accuracy Chart)
  The **Filter** dialog box helps you build conditions that you can apply to a data set. The data set can be an external data set used for testing, or the case data used to train a mining model. This dialog box helps you build criteria that you can save as part of more complex filter criteria in either the **Data Set Filter** dialog box or the **Model Filter** dialog box.  
  
-   The **Data Set Filter** dialog box is available from the **Input Selection** tab of the **Mining Accuracy Chart** designer.  
  
-   The **Model Filter** dialog box is available from the **Mining Models** tab of the Data Mining designer.  
  
 If you are applying a filter to a mining model, first you use the **Model Filter** dialog box to choose a table. Then, you can use the **Filter** dialog box to build conditions that apply only to that table.  
  
 If you are creating a filter to apply to an external test data set, first you use the **Data Set Filter** dialog box to choose a table from the list of tables in a data source view. Then, you use the **Filter** dialog box to build conditions that apply only to that table.  
  
 After you have created a set of conditions that apply to a single table, [!INCLUDE[clickOK](../includes/clickok-md.md)]. The changes that you made in the **Filter** dialog box are automatically added to the filter expression in the parent dialog box, **Data Set Filter** or **Model Filter**.  
  
 If you apply the filter to the new data set, the existing data mining model is used to evaluate only those cases in the data set that meet the conditions. However, if you apply the filter to the mining model itself, the accuracy of the model is assessed for only those cases within the mining model that meet those criteria.  
  
 **For more information:** [Testing and Validation &#40;Data Mining&#41;](data-mining/testing-and-validation-data-mining.md)  
  
## Options  
 **Conditions**  
 A grid that contains columns where you specify conditions on columns from the table that you selected in the **Data Set Filter** dialog box.  
  
|Value|Description|  
|-----------|-----------------|  
|**And/Or**|Click to specify whether to apply the AND operator or the OR operator to the condition on this line. These values become available only after you have selected a column from the **Mining Structure Column** list.|  
|**Mining Structure Column**|Click to select a column from the list of the columns contained in the table that you selected from the data source in the **Data Set Filter** dialog box.|  
|**Operator**|Select an operator from the list. The operators that are available depend on the data type of the column.<br /><br /> If the column contains discrete values, only the following operators are available:<br /><br /> = (equal to), <> (not equal to), IS NOT NULL, IS NULL.<br /><br /> If the column contains continuous values, operators for greater than and less than operations are also supported.|  
|**Value**|Type a value to use as a condition.|  
  
## See Also  
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)   
 [Mining Accuracy Chart Designer &#40;Data Mining&#41;](mining-accuracy-chart-designer-data-mining.md)  
  
  
