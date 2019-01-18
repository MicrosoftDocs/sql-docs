---
title: "Filters for Mining Models (Analysis Services - Data Mining) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Filters for Mining Models (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Data-based model filtering helps you create mining models that use subsets of data in a mining structure. Filtering gives you flexibility when you design your mining structures and data sources, because you can create a single mining structure, based on a comprehensive data source view. You can then create filters to use only a part of that data for training and testing a variety of models, instead of building a different structure and related model for each subset of data.  
  
 For example, you define the data source view on the Customers table and related tables. Next, you define a single mining structure that includes all the fields you need. Finally, you create a model that is filtered on a particular customer attribute, such as Region. You can then easily make a copy of that model, and change just the filter condition to generate a new model based on a different region.  
  
 Some real-life scenarios where you might benefit from this feature include the following:  
  
-   Creating separate models for discrete values such as gender, regions, and so forth. For example, a clothing store might use customer demographics to build separate models by gender, even though the sales data comes from a single data source for all customers.  
  
-   Experimenting with models by creating and then testing multiple groupings of the same data, such as ages 20-30 vs. ages 20-40 vs. ages 20-25.  
  
-   Specifying complex filters on nested table contents, such as requiring that a case be included in the model only if the customer has purchased at least two of a particular item.  
  
 This section explains how to build, use, and manage filters on mining models.  
  
## Creating Model Filters  
 You can create and apply filters in the following ways:  
  
-   Using the **Mining Models** tab in Data Mining Designer to build conditions with the help of filter editor dialog boxes.  
  
-   Typing a filter expression directly into the **Filter** property of the mining model.  
  
-   Setting filter conditions on a model programmatically, by using AMO.  
  
### Creating Model Filters using Data Mining Designer  
 You filter a model in Data Mining Designer by changing the **Filter** property of the mining model. You can either type a filter expression directly into the **Properties** pane, or you can open a filter dialog box to build conditions.  
  
 There are two filter dialog boxes. The first lets you create conditions that are applied to the case table. If the data source contains multiple tables, first you select a table, and then you select a column and specify operators and conditions that apply to that column. You can link multiple conditions by using **AND**/**OR** operators. The operators that are available for defining values depend on whether the column contains discrete or continuous values. For example, with continuous values, you can use **greater than** and **less than** operators. However, for discrete values, you can only use **= (equal to)**, **!= (not equal to)**, and **is null** operators.  
  
> [!NOTE]  
>  The **LIKE** keyword is not supported. If you want to include multiple discrete attributes, you must create separate conditions and link them by using the **OR** operator.  
  
 If the conditions are complex, you can use the second filter dialog box to work with one table at a time. When you close the second filter dialog box, the expression is evaluated and then combined with filter conditions that have been set on other columns in the case table.  
  
### Creating Filters on Nested Tables  
 If the data source view contains nested tables, you can use the second filter dialog box to build conditions on the rows in the nested tables.  
  
 For example, if your case table is related to customers, and the nested table shows the products that a customer has purchased, you can create a filter for customers who have purchased particular items by using the following syntax in the nested table filter: `[ProductName]='Water Bottle' OR ProductName='Water Bottle Cage'`.  
  
 You can also filter on the existence of a particular value in the nested table by using the **EXISTS** or **NOT EXISTS** keywords and a subquery. This lets you create conditions such as `EXISTS (SELECT * FROM Products WHERE ProductName='Water Bottle')`. The `EXISTS SELECT(<subquery>)` returns **true** if the nested table contains at least one row that includes the value, `Water Bottle`.  
  
 You can combine conditions on the case table with conditions on the nested table. For example, the following syntax includes a condition on the case table (`Age > 30` ), a subquery on the nested table (`EXISTS (SELECT * FROM Products)`), and multiple conditions on the nested table (`WHERE ProductName='Milk'  AND Quantity>2`) ).  
  
```  
(Age > 30 AND EXISTS (SELECT * FROM Products WHERE ProductName='Milk'  AND Quantity>2) )  
```  
  
 When you have finished building the filter, the filter text is evaluated by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], translated to a DMX expression, and then saved with the model.  
  
 For instructions on how to use the filter dialog boxes in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], see [Apply a Filter to a Mining Model](../../analysis-services/data-mining/apply-a-filter-to-a-mining-model.md).  
  
## Managing Mining Model Filters  
 Data-based model filtering greatly simplifies the task of managing mining structures and mining models, because you can easily create multiple models that are based on the same structure. You can also quickly make copies of existing mining models and then change only the filter condition. However, filters can lead to some confusion.  
  
 Here are some frequently asked questions about how to manage and interpret filters on mining models:  
  
### How can I tell whether a filter is being used?  
 There are multiple ways to determine whether a filter is applied to a model:  
  
-   In the designer, click the **Mining Models** tab, open **Properties**, and view the **Filter** property of the mining model.  
  
-   The DMV, DMSCHEMA_MINING_MODELS, outputs a column that contains the text of the filter. You can use the following query on a DMV to return the names of models and their filters:  
  
    ```  
    SELECT MODEL_NAME, [FILTER]   
    FROM $SYSTEM.DMSCHEMA_MINING_MODELS  
  
    ```  
  
-   You can get the value of the Filter property of the MiningModel object in AMO, or inspect the Filter element in XMLA.  
  
 You might also establish a naming convention for models to reflect the contents of the filter. This can make it easier to tell related models apart.  
  
### How can I save a filter?  
 The filter expression is saved as a script that is stored with the associated mining model or nested table. If you delete the filter text, it can only be restored by manually re-creating the filter expression. Therefore, if you create complex filter expressions, you should create a backup copy of the filter text.  
  
### Why can't I see any effects from the filter?  
 Whenever you change or add a filter expression, you must reprocess the structure and model before you can view the effects of the filter.  
  
### Why do I see filtered attributes in prediction query results?  
 When you apply a filter to a model, it affects only the selection of cases used for training the model. The filter does not affect the attributes known to the model, or change or suppress data that is present in the data source. As a result, queries against the model can return predictions for other types of cases, and dropdown lists of values used by the model might show attribute values excluded by the filter.  
  
 For example, suppose you train the [Bike Buyer] model using only cases involving women aged 20-30. You can still run a prediction query that predicts the probability of a man buying a bike, or predict the outcome for a woman aged 30-40. That is because the attributes and values present in the data source define what is theoretically possible, while the cases define the occurrences used for training. However, these queries would return very small probabilities, because the training data does not contain any cases with the target values.  
  
 If you need to completely hide or anonymize attribute values in the model, there are various options:  
  
-   Filter the incoming data as part of the definition of the data source view, or in the relational data source.  
  
-   Mask or encode the attribute value.  
  
-   Collapse excluded values into a category as part of the mining structure definition.  
  
## Related Resources  
 For more information about filter syntax, and examples of filter expressions, see [Model Filter Syntax and Examples &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/model-filter-syntax-and-examples-analysis-services-data-mining.md).  
  
 For information about how to use model filters when you are testing a mining model, see [Choose an Accuracy Chart Type and Set Chart Options](../../analysis-services/data-mining/choose-an-accuracy-chart-type-and-set-chart-options.md).  
  
## See Also  
 [Model Filter Syntax and Examples &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/model-filter-syntax-and-examples-analysis-services-data-mining.md)   
 [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md)  
  
  
