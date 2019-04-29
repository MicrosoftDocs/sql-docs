---
title: "Data Set Filter or Model Filter Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: a9602174-b7e2-4e16-8ded-dfd8eb9264d7
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Set Filter or Model Filter Dialog Box
  This dialog box helps you build the filters that you can apply to a data set.  The data set can be an external data set used for testing, or the case data for a mining model. The name of the dialog box changes depending on whether the filter is for an external data set or for a mining model.  
  
 If you apply the filter to a new data set, the data mining model is evaluated by using only those cases in the data set that meet the conditions. If you apply the filter to the mining model itself, the model will be trained and tested by using only the cases in the existing test data set that meet the filter criteria.  
  
-   The **Data Set Filter** dialog box is available from the **Input Selection** tab of the **Mining Accuracy Chart** designer.  
  
-   The **Model Filter** dialog box is available from the **Mining Models** tab of the Data Miningdesigner.  
  
-   The **Conditions** grid contains columns where you can specify a table or column name, an operator, and target values. By using this grid you are essentially creating a WHERE clause.  
  
> [!TIP]  
>  To test accuracy on a subset of the original training data, you can add the data source view that was used to define the training set as the external testing data, and then add conditions in the **Data Set Filter** grid.  
  
 **For more information:** [Testing and Validation &#40;Data Mining&#41;](data-mining/testing-and-validation-data-mining.md)  
  
## Options  
 **Conditions**  
 Displays table names, followed by column names with conditions.  
  
|Value|Description|  
|-----------|-----------------|  
|**And/Or**|Choose an operator to join multiple conditions.|  
|**Mining Structure Column**|Click to select a data source, and then click successive lines in the grid to add columns from the data source.<br /><br /> The first line in the grid specifies the data source view. After you select a data source view, **Mining Structure Column** displays a table icon, and the **Value** field displays the combination of all criteria that you have defined for this data source.<br /><br /> After you have selected a data source, the **Mining Structure Column** box provides a dropdown list of individual columns in the data source.|  
|**Operator**|Select an operator from the list.|  
|**Value**|For tables, the **Value** field shows the combination of all filters applied to the data source. You can also click the build **(...)** button at the right of the text box to open the **Filter** dialog box and build a condition.|  
  
 **Expression**  
 Displays the set of criteria that you built by using the grid.  
  
 **Edit Query**  
 Changes the filter editing mode so that you can type a filter expression directly in the **Expression** text box.  
  
> [!NOTE]  
>  After you have made manual changes to the filter expression, you cannot return to grid edit mode, even after you have saved the expression in the **Filter Expression** box on the **Input Selection** tab. If you want to build an expression by using the grid, you must delete the existing filter expression and start over.  
  
 **Revert Query Edits**  
 Restores the grid to its previous state and cancels any changes that you made to the filter expression.  
  
## See Also  
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)   
 [Mining Accuracy Chart Designer &#40;Data Mining&#41;](mining-accuracy-chart-designer-data-mining.md)  
  
  
