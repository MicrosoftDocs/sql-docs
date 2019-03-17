---
title: "Apply Filters to Model Testing Data | Microsoft Docs"
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
# Apply Filters to Model Testing Data
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When you specify an external data source to use in testing a model, you can optionally apply a filter to restrict the input data. For example, you might want to test the model specifically for predictions on customers in a certain income range.  
  
 For example, in the Adventure Works targeted mailing scenario, you can create a filter expression like the following one on ProspectiveBuyer, which is the table that contains the testing data, and restrict testing cases by income range:  
  
 `[YearlyIncome] = '50000'`  
  
 The behavior of filters is slightly different, depending on whether you are filtering model training data or a testing data set:  
  
-   When you define a filter on a testing data set, you create a WHERE clause on the incoming data. If you are filtering an input data set used for evaluating a model, the filter expression is translated to a Transact-SQL statement and applied to the input table when the chart is created. As a result, the number of test cases can be greatly reduced.  
  
-   When you apply a filter to a mining model, the filter expression that you create is translated to a Data Mining Extensions (DMX) statement, and applied to the individual model. Therefore, when you apply a filter to a model, only a subset of the original data is used to train the model. This can cause problems if you filter the training model with one set of criteria, so that the model is tuned to a certain set of data, and then test the model with another set of criteria.  
  
-   If you defined a testing data set when you created the structure, the model cases used for training include only those cases that are in the mining structure training set **and** which meet the conditions of the filter. Thus, when you are testing a model and select the option **Use mining model test cases**, the testing cases will include only the cases that are in the mining structure test set and which meet the conditions of the filter. However, if you did not define a holdout data set, the model cases used for testing include all the cases in the data set that meet the filter conditions  
  
-   Filter conditions that you apply on a model also affect drillthrough queries on the model cases.  
  
 In summary, when you test multiple models, even if all the models are based on the same mining structure, you must be aware that the models potentially use different subsets of data for training and testing. This can have the following effects on accuracy charts:  
  
-   The total number of cases in the testing sets can vary among the models being tested.  
  
-   The percentages for each model may not align in the chart, if the models use different subsets of training data or testing data.  
  
 To determine whether a model contains a predefined filter that might affect results, you can look for the **Filter** property in the **Property** pane, or you can query the model by using the data mining schema rowsets. For example, the following query returns the filter text for the specified model:  
  
 `SELECT [FILTER] FROM $system.DMSCHEMA_MINING_MODELS WHERE MODEL_NAME = 'name of model'`  
  
> [!WARNING]  
>  If you want to remove the filter from an existing mining model, or change the filter conditions, you must reprocess the mining model.  
  
 For more information about the kinds of filters you can apply, and how filter expressions are evaluated, see [Model Filter Syntax and Examples &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/model-filter-syntax-and-examples-analysis-services-data-mining.md).  
  
### Create a filter on external testing data  
  
1.  Double-click the mining structure that contains the model you want to test, to open Data Mining Designer.  
  
2.  Select the **Mining Accuracy Chart** tab, and then select the **Input Selection** tab.  
  
3.  On the **Input Selection** tab, under **Select data set to be used for Accuracy Chart**, select the option **Specify a different data set**.  
  
4.  Click the browse button **(...)** to open a dialog box and choose the external data set.  
  
5.  Choose the case table, and add a nested table if necessary. Map columns in the model to columns in the external data set as necessary. Close the **Specify Column Mapping** dialog box to save the source table definition.  
  
6.  Click **Open Filter Editor** to define a filter for the data set.  
  
     The **Data Set Filter** dialog box opens. If the structure contains a nested table, you can create a filter in two parts. First, set conditions on the case table by using the **Data Set Filter** dialog box, and then set conditions on the nested rows by using the **Filter** dialog box.  
  
7.  In the **Data Set Filter** dialog box, click the top row in the grid, under **Mining Structure Column**, and select a table or column from the list.  
  
     If the data source view contains multiple tables, or a nested table, you must first select the table name. Otherwise, you can select columns from the case table directly.  
  
     Add a new row for each column that you want to filter.  
  
8.  Use **Operator**, and **Value** columns to define how the column is filtered.  
  
     **Note** Type values without using quotation marks.  
  
9. Click the **And/Or** text box and select a logical operator to define how multiple conditions are combine.  
  
10. Optionally, click the browse button **(...)** at the right of the **Value** text box to open the **Filter** dialog box and set conditions on the nested table or on the individual case table columns.  
  
11. Verify that the completed filter conditions are correct by viewing the text in the **Expression** pane.  
  
12. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     The filter condition is applied to the data source when you create the accuracy chart.  
  
## See Also  
 [Choose and Map Model Testing Data](../../analysis-services/data-mining/choose-and-map-model-testing-data.md)   
 [Using Nested Table Data as an Input for an Accuracy Chart](../../analysis-services/data-mining/using-nested-table-data-as-an-input-for-an-accuracy-chart.md)   
 [Choose an Accuracy Chart Type and Set Chart Options](../../analysis-services/data-mining/choose-an-accuracy-chart-type-and-set-chart-options.md)  
  
  
