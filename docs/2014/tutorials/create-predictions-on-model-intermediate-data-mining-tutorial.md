---
title: "Creating Predictions on a Sequence Clustering Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 94a8d4f9-a76a-49c5-9785-917010359511
author: minewiskan
ms.author: owend
manager: kfilee
---
# Creating Predictions on a Sequence Clustering Model (Intermediate Data Mining Tutorial)
  After you understand the sequence clustering model better by browsing it in the viewer, you can create prediction queries by using Prediction Query Builder on the **Mining Model Prediction** tab in Data Mining Designer. To create a prediction, you first select the sequence clustering model, and then select the input data. For inputs, you can use either an external data source, or you can build a singleton query and provide values in a dialog box.  
  
 This lesson assumes that you are already familiar with how to use the prediction query builder and want to learn how to build queries that are specific to a sequence clustering model. For general information about how to use Prediction Query Builder, see [Data Mining Query Interfaces](../../2014/analysis-services/data-mining/data-mining-query-tools.md) or the section of the Basic Data Mining tutorial, [Creating Predictions &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/creating-predictions-basic-data-mining-tutorial.md).  
  
## Creating Predictions on the Regional Model  
 For this scenario, you will first create some singleton prediction queries, to get an idea of how predictions might be different by region.  
  
#### To create a singleton query on a sequence clustering model  
  
1.  Click the **Mining Model Prediction** tab of Data Mining Designer.  
  
2.  In the **Mining Model** column menu, select **Singleton Query**.  
  
     The **Mining Model** pane and **Singleton Query Input** pane appear.  
  
3.  In the **Mining Model** pane, click **Select Model**. (You can skip this step if the sequence clustering mode is already selected.)  
  
     The **Select Mining Model** dialog box opens.  
  
4.  Expand the node that represents the mining structure **Sequence Clustering with Region**, and select the model **Sequence Clustering with Region**. Click **OK**. For now, ignore the input pane; you will specify the inputs after you have set up the prediction functions.  
  
5.  In the grid, click the empty cell under **Source** and select **Prediction Function.** In the cell under **Field**, select **PredictSequence**.  
  
    > [!NOTE]  
    >  You can also use the **Predict** function. If you do, be sure to choose the version of the **Predict** function that takes a table column as argument..  
  
6.  In the **Mining Model** pane, select the nested table `v Assoc Seq Line Items`, and drag it into the grid, to the **Criteria/Argument** box for the **PredictSequence** function.  
  
     Dragging and dropping table and column names enables you to build complex statements without syntax errors. However, it replaces the current contents of the cell, which include other optional arguments for the **PredictSequence** function. To view the other arguments, you can temporarily add a second instance of the function to the grid for reference.  
  
7.  Click the **Result** button in the upper corner of the Prediction Query Builder.  
  
 The expected results contain a single column with the heading **Expression**. The **Expression** column contains a nested table with three columns as follows:  
  
|$SEQUENCE|Line Number|Model|  
|---------------|-----------------|-----------|  
|1||Mountain-200|  
  
 What do these results mean? Remember that you did not specify any inputs. Therefore, the prediction is made against the entire population of cases, and Analysis Services returns the most likely prediction overall.  
  
### Adding Inputs to a Singleton Prediction Query  
 Until now, you have not specified any inputs. In the next task, you will use the **Singleton Query Input** pane to specify some inputs to the query. First, you will use [Region] as an input to the regional sequence clustering model, to determine whether the predicted sequences are the same for all regions. You will then learn how to modify the query to add the probability for each prediction, and flatten the results to make them easier to view.  
  
##### To generate predictions for a specific customer group  
  
1.  Click the **Design** button in the upper left-hand corner of the Prediction Query Builder to switch back to the query building grid.  
  
2.  In the **Singleton Query Input** dialog box, click the **Value** box for `Region`, and select **Europe**.  
  
3.  Click the **Result** button to view predictions for customers in Europe.  
  
4.  Click the **Design** button in the upper left-hand corner of the Prediction Query Builder to switch back to the query building grid.  
  
5.  In the **Singleton Query Input** dialog box, click the **Value** box for `Region`, and select **North America**.  
  
6.  Click the **Result** button to view predictions for customers in North America.  
  
### Adding Probabilities by Using a Custom Expression  
 To output the probability for each prediction is slightly more complicated, because the probability is an attribute of the prediction and is output as a nested table. If you are familiar with Data Mining Extensions (DMX), you can easily alter the query to add a sub-select statement on the nested table. However, you can also create a sub-select statement in the Prediction Query Builder by adding a custom expression.  
  
##### To output probabilities for a predicted sequence by using a custom expression  
  
1.  Click the **Design** button in the upper left-hand corner of the Prediction Query Builder to switch back to the query building grid.  
  
2.  In the grid, under **Source**, click a new row, and select **Custom Expression**.  
  
3.  Leave the box under **Field** blank.  
  
4.  For **Alias**, type `t`.  
  
5.  In the **Criteria/Argument** box, type the complete sub-select statement as shown in the following code sample. Be sure to include the starting and ending parentheses.  
  
    ```  
    (SELECT PredictProbability([Model]) FROM PredictSequence([Sequence Clustering with Region].[v Assoc Seq Line Items]))  
    ```  
  
6.  Click the **Result** button to view predictions for customers in Europe.  
  
 The results now contain two nested tables, one with the prediction, and one with the probability for the prediction. If the query does not work, you can switch to query design view and review the complete query statement, which should be as follows:  
  
```  
SELECT  
  PredictSequence([Sequence Clustering with Region].[v Assoc Seq Line Items]),  
  ( (SELECT PredictProbability([Model]) FROM PredictSequence([Sequence Clustering with Region].[v Assoc Seq Line Items]))) as [t]  
FROM  
  [Sequence Clustering with Region]  
NATURAL PREDICTION JOIN  
(SELECT 'Europe' AS [Region]) AS t  
```  
  
### Working with Results  
 When there are many nested tables in the results, you might want to flatten the results for easier viewing. To do this, you can manually modify the query and add the `FLATTENED` keyword.  
  
##### To flatten nested rowsets in a prediction query  
  
1.  Click the **Query** button in the corner of the Prediction Query Builder.  
  
     The grid changes to an open pane where you can view and modify the DMX statement that was created by the Prediction Query Builder.  
  
2.  After the `SELECT` keyword, type `FLATTENED`.  
  
     The complete text of the query should be similar to the following:  
  
    ```  
    SELECT FLATTENED  
      PredictSequence([Sequence Clustering with Region].[v Assoc Seq Line Items]),  
      ( (SELECT PredictProbability([Model]) FROM PredictSequence([Sequence Clustering with Region].[v Assoc Seq Line Items]))) as [t]  
    FROM  
      [Sequence Clustering with Region]  
    NATURAL PREDICTION JOIN  
    (SELECT 'Europe' AS [Region]) AS t  
    ```  
  
3.  Click the **Results** button in the upper corner of the Prediction Query Builder.  
  
 After you have manually edited a query, you will not be able to switch back to Design view without losing the changes. You can, however, save the DMX statement that you created manually to a text file, and then change back to Design view. When you do so, the query is reverted to the last version that was valid in Design view.  
  
## Creating Predictions on the Related Model  
 The previous examples used a case table column, Region, as the input to the singleton prediction query, because you were interested in knowing whether the model had found any differences between regions. However, after exploring the model, you decided that the differences are not strong enough to justify customizing product recommendations by region. What you are really interested in predicting is the items that customers select. Therefore, in the queries that follow, you will use the sequence clustering model that does not include Region, to generate recommendations for all customers.  
  
### Using Nested Table Columns as Input  
 First you will create a singleton prediction query that takes a single item as input and returns the next most likely item. To get a prediction of this kind, you have to use a nested table column as the input value. This is because the attribute that you are predicting, Model, is part of a nested table. Analysis Services provides the **Nested Table Input** dialog box to help you easily create prediction queries on nested table attributes, by using the Prediction Query Builder.  
  
##### To use a nested table as input to a prediction  
  
1.  Click the **Design** button in the upper left-hand corner of the Prediction Query Builder to switch back to the query building grid.  
  
2.  In the **Singleton Query Input** dialog box, click the **Value** box for `Region`, and select the empty row to clear the input for this field.  
  
3.  In the **Singleton Query Input** dialog box, click the **Value** box for `vAssocSeqLineItems`, and then click the (...) button.  
  
4.  In the **Nested Table Input** dialog box, click **Add**.  
  
5.  In the new row, click the box under `Model`, and select Touring Tire from the list. Click **OK**.  
  
6.  Click the **Result** button to view the predictions.  
  
 The model recommends the following next items for all customers who choose the Touring Tire as the first item. You already know from exploring the model that customers frequently purchase the products Touring Tire and Touring Tire Tube together, so these recommendations look good.  
  
|$SEQUENCE|Line Number|Model|  
|---------------|-----------------|-----------|  
|1||Touring Tire Tube|  
|2||Sport-100|  
|3||Long-Sleeve Logo Jersey|  
  
### Creating a Bulk Prediction Query using Nested Table Inputs  
 Now that you are satisfied that the model creates the kind of predictions that you can use in making recommendations, you will create a prediction query that is mapped to an external data source. That data source will provide values representing current products. Because you are interested in creating a prediction query that provides Customer ID and a list of products as input, you will add the customer table as the case table, and the purchases table as the nested table. Then you will add prediction functions as you did previously to create recommendations.  
  
 This is the same procedure that you use to create predictions for the market basket scenario in Lesson 3; however, in a sequence clustering model predictions also need the order as input.  
  
##### To create a prediction query using nested table inputs  
  
1.  In the **Mining Model** pane, select the Sequence Clustering model, if it is not already selected.  
  
2.  In the **Select Input Table(s)** dialog box, click **Select Case Table**.  
  
3.  In the **Select Table** dialog box, for Data Source, select Orders. In the **Table/View Name** list, select vAssocSeqOrders, and then click **OK**.  
  
4.  In the **Select Input Table(s)** dialog box, click **Select Nested Table**.  
  
5.  In the **Select Table** dialog box, for **Data Source**, select Orders. In the **Table/View name** list, select vAssocSeqLineItems, and then click **OK**.  
  
     Analysis Services will try to detect relationships and create them automatically if the data types match and the column names are similar. If the relationships that it creates are wrong, you can right-click the join line and select **Modify Connections** to edit the column mapping, or you can right-click the join line and select **Delete** to remove the relationship completely. In this case, because the tables were already joined in the data source view, those relationships are automatically added to the design pane.  
  
6.  Add a new row to the grid. For **Source**, select vAssocSeqOrders, and for **Field**, select CustomerKey.  
  
7.  Add a new row to the grid. For **Source**, select **Prediction Function**, and for **Field**, select **PredictSequence**.  
  
8.  Drag vAssocSeqLineItems, into the **Criteria/Argument** box. Click at the end of the **Criteria/Argument** box and then type the following arguments: `2`.  
  
     The complete text in the **Criteria/Argument** box should be: `[Sequence Clustering].[v Assoc Seq Line Items],2`  
  
9. Click the **Result** button to view the predictions for each customer.  
  
 You have completed the tutorial on sequence clustering models.  
  
## Next Steps  
 If you have finished all the sections in the [Intermediate Data Mining Tutorial &#40;Analysis Services - Data Mining&#41;](../../2014/tutorials/intermediate-data-mining-tutorial-analysis-services-data-mining.md), the next step might be to learn to use Data Mining Extensions (DMX) statements to build models and generate predictions. For more information, see [Creating and Querying Data Mining Models with DMX: Tutorials &#40;Analysis Services - Data Mining&#41;](../../2014/tutorials/create-query-data-mining-models-dmx-tutorials.md).  
  
 If you are familiar with programming concepts, you can also use Analysis Management Objects (AMO) to programmatically work with data mining objects. For more information, see [AMO Data Mining Classes](https://docs.microsoft.com/bi-reference/amo/amo-data-mining-classes).  
  
## See Also  
 [Sequence Clustering Model Query Examples](../../2014/analysis-services/data-mining/sequence-clustering-model-query-examples.md)   
 [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-sequence-clustering-models.md)  
  
  
