---
title: "Predicting Associations (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 9140c5f2-b340-45a6-9c27-d870d15aafea
author: minewiskan
ms.author: owend
manager: kfile
---
# Predicting Associations (Intermediate Data Mining Tutorial)
  After the models have been processed, you can use the information about associations stored in the model to create predictions. In the final task of this lesson, you learn how to build prediction queries against the association models that you created. This lesson assumes that you are familiar with how to use the Prediction Query Builder and want to learn how to build prediction queries against association models. For more information how to use Prediction Query Builder, see [Data Mining Query Interfaces](../../2014/analysis-services/data-mining/data-mining-query-tools.md).  
  
## Creating a Singleton Prediction Query  
 Prediction queries on an association model can be very useful:  
  
-   Recommend items to a customer, based on prior or related purchases  
  
-   Find related events.  
  
-   Identify relationships in or across sets of transactions.  
  
 To build a prediction query, you first select the association model you want to use, and then you specify the input data. Inputs can come from an external data source, such as a list of values, or you can build a singleton query and provide values as you go.  
  
 For this scenario, you will first create some singleton prediction queries, to get an idea of how prediction works. Then you will create a query for batch predictions that you could use for making recommendations based on a customer's current purchases.  
  
#### To create a prediction query on an association model  
  
1.  Click the **Mining Model Prediction** tab of Data Mining Designer.  
  
2.  In the **Mining Model** pane, click **Select Model**. (You can skip this step and the next step if the correct model is already selected.)  
  
3.  In the **Select Mining Model** dialog box, expand the node that represents the mining structure **Association**, and select the model **Association**. Click **OK**.  
  
     For now, you can ignore the input pane.  
  
4.  In the grid, click the empty cell under **Source** and select **Prediction Function.** In the cell under **Field**, select `PredictAssociation`.  
  
     You can also use the **Predict** function to predict associations. If you do, be sure to choose the version of the **Predict** function that takes a table column as argument.  
  
5.  In the **Mining Model** pane, select the nested table `vAssocSeqLineItems`, and drag it into the grid, to the **Criteria/Argument** box for the `PredictAssociation` function.  
  
     Dragging and dropping table and column names lets you build complex statements without syntax errors. However, it replaces the current contents of the cell, which include other optional arguments for the `PredictAssociation` function. To view the other arguments, you can temporarily add a second instance of the function to the grid for reference.  
  
6.  Click the **Criteria/Argument** box and type the following text after the table name: `,3`  
  
     The complete text in the **Criteria/Argument** box should be as follows:  
  
     `[Association].[v Assoc Seq Line Items],3`  
  
7.  Click the **Results** button in the upper corner of the Prediction Query Builder.  
  
 The expected results contain a single column with the heading **Expression**. The **Expression** column contains a nested table with a single column and the following three rows. Because you did not specify an input value, these predictions represent the most likely product associations for the model as a whole.  
  
|Model|  
|-----------|  
|Women's Mountain Shorts|  
|Water Bottle|  
|Touring-3000|  
  
 Next, you will use the **Singleton Query Input** pane to specify a product as input to the query, and view the products that are most likely associated with that item.  
  
#### To create a singleton prediction query with nested table inputs  
  
1.  Click the **Design** button in the corner of the Prediction Query Builder to switch back to the query building grid.  
  
2.  On the **Mining Model** menu, select **Singleton Query**.  
  
3.  In the **Mining Model** dialog box, select the **Association** model.  
  
4.  In the grid, click the empty cell under **Source** and select **Prediction Function.** In the cell under **Field**, select `PredictAssociation`.  
  
5.  In the **Mining Model** pane, select the nested table `vAssocSeqLineItems`, and drag it into the grid, to the **Criteria/Argument** box for the `PredictAssociation` function. Type `,3` after the nested table name just as in the previous procedure.  
  
6.  In the **Singleton Query Input** dialog box, click the **Value** box next to **vAssoc Seq Line Items**, and then click the **(...)** button.  
  
7.  In the **Nested Table Input** dialog box, select `Touring Tire` in the **Key column** pane, and then click **Add**.  
  
8.  Click the **Results** button.  
  
 The results now show the predictions for products that are most likely associated with the Touring Tire.  
  
|Model|  
|-----------|  
|Touring Tire Tube|  
|Sport-100|  
|Water Bottle|  
  
 However, you already know from exploring the model that the Touring Tire Tube is frequently purchased with the Touring Tire; you are more interested in knowing what products you can recommend to customers who purchase these items together. You will change the query so that it predicts related products based on two items in the basket. You will also modify the query to add the probability for each predicted product.  
  
#### To add inputs and probabilities to the singleton prediction query  
  
1.  Click the **Design** button in the corner of the Prediction Query Builder to switch back to the query building grid.  
  
2.  In the **Singleton Query Input** dialog box, click the **Value** box next to **vAssoc Seq Line Items**, and then click the **(...)** button.  
  
3.  In the **Key column** pane, select `Touring Tire`, and then click **Add**.  
  
4.  In the grid, click the empty cell under **Source** and select **Prediction Function.** In the cell under **Field**, select `PredictAssociation`.  
  
5.  In the **Mining Model** pane, select the nested table `vAssocSeqLineItems`, and drag it into the grid, to the **Criteria/Argument** box for the `PredictAssociation` function. Type `,3` after the nested table name just as in the previous procedure.  
  
6.  In the **Nested Table Input** dialog box, select `Touring Tire Tube` in the **Key column** pane, and then click **Add**.  
  
7.  In the grid, in the row for the `PredictAssociation` function, click the **Criteria/Argument** box, and change the arguments to add the argument, INCLUDE_STATISTICS.  
  
     The complete text in the **Criteria/Argument** box should be as follows:  
  
     `[Association].[v Assoc Seq Line Items], INCLUDE_STATISTICS, 3`  
  
8.  Click the **Results** button.  
  
 The results in the nested table now change to show the predictions, together with support and probability. For more information about how to interpret these values, see [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-association-models-analysis-services-data-mining.md).  
  
|Model|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|  
|-----------|--------------|------------------|--------------------------|  
|Sport-100|4334|0.291...|0.252...|  
|Water Bottle|2866|0.192...|0.175...|  
|Patch Kit|2113|0.142...|0.132|  
  
## Working with Results  
 When there are many nested tables in the results, you might want to flatten the results for easier viewing. To do this, you can manually modify the query and add the `FLATTENED` keyword.  
  
#### To flatten nested rowsets in a prediction query  
  
1.  Click the **SQL** button in the corner of the Prediction Query Builder.  
  
     The grid changes to an open pane where you can view and modify the DMX statement that was created by the Prediction Query Builder.  
  
2.  After the `SELECT` keyword, type `FLATTENED`.  
  
     The complete text of the query should be as follows:  
  
    ```  
    SELECT FLATTENED  
      PredictAssociation([Association].[v Assoc Seq Line Items],INCLUDE_STATISTICS,3)  
    FROM  
      [Association]  
    NATURAL PREDICTION JOIN  
    (SELECT (SELECT 'Touring Tire' AS [Model]  
      UNION SELECT 'Touring Tire Tube' AS [Model]) AS [v Assoc Seq Line Items]) AS t  
    ```  
  
3.  Click the **Results** button in the upper corner of the Prediction Query Builder.  
  
 Note that after you have manually edited a query, you will not be able to switch back to Design view without losing the changes. If you wish to save the query, you can copy the DMX statement that you created manually to a text file. When you change back to Design view, the query is reverted to the last version that was valid in Design view.  
  
## Creating Multiple Predictions  
 Suppose you want to know the best predictions for individual customers, based on past purchases. You can use external data as input to the prediction query, such as tables containing the customer ID and the most recent product purchases. The requirements are that the data tables be already defined as an Analysis Services data source view; moreover, the input data must contain case and nested tables like those used in the model. They need not have the same names, but the structure must be similar. For the purpose of this tutorial, you will use the original tables on which the model was trained.  
  
#### To change the input method for the prediction query  
  
1.  In the **Mining Model** menu, select **Singleton Query** again, to clear the check mark.  
  
2.  An error message appears warning that your singleton query will be lost. Click **Yes**.  
  
     The name of the input dialog box changes to **Select Input Table(s)**.  
  
 Because you are interested in creating a prediction query that provides Customer ID and a list of products as input, you will add the customer table as the case table, and the purchases table as the nested table. Then you will add prediction functions to create recommendations.  
  
#### To create a prediction query using nested table inputs  
  
1.  In the Mining Model pane, select the Association Filtered model.  
  
2.  In the **Select Input Table(s)** dialog box, click **Select Case Table**.  
  
3.  In the **Select Table** dialog box, for **Data Source**, select AdventureWorksDW2008. In the **Table/View Name** list, select vAssocSeqOrders, and then click **OK**.  
  
     The table vAssocSeqOrders is added to the pane.  
  
4.  In the **Select Input Table(s)** dialog box, click **Select Nested Table**.  
  
5.  In the **Select Table** dialog box, for **Data Source**, select AdventureWorksDW2008. In the **Table/View name** list, select vAssocSeqLineItems, and then click **OK**.  
  
     The table vAssocSeqLineItems is added to the pane.  
  
6.  In the **Specify Nested Join** dialog box, drag the OrderNumber field from the case table and drop it onto the OrderNumber field in the nested table.  
  
     You can also click **Add Relationship** and create the relationship by selecting columns from a list.  
  
7.  In the **Specify Relationship** dialog box, verify that the OrderNumber fields are mapped correctly, and then click **OK**.  
  
8.  Click **OK** to close the **Specify Nested Join** dialog box.  
  
     The case and nested tables are updated in the design pane to show the joins connecting the external data columns to the columns in the model. If the relationships are wrong, you can right-click the join line and select **Modify Connections** to edit the column mapping, or you can right-click the join line and select **Delete** to remove the relationship completely.  
  
9. Add a new row to the grid. For **Source**, select **vAssocSeqOrders table**. For **Field**, select CustomerKey.  
  
10. Add a new row to the grid. For **Source**, select **vAssocSeqOrders table**. For **Field**, select Region.  
  
11. Add a new row to the grid. For **Source**, select **Prediction Function**, and for **Field**, select `PredictAssociation`.  
  
12. Drag vAssocSeqLineItems, into the **Criteria/Argument** box of the `PredictAssociation` row. Click at the end of the **Criteria/Argument** box and then type the following text: `INCLUDE_STATISTICS,3`  
  
     The complete text in the **Criteria/Argument** box should be: `[Association].[v Assoc Seq Line Items], INCLUDE_STATISTICS, 3`  
  
13. Click the **Result** button to view the predictions for each customer.  
  
 You might try creating a similar prediction query on the multiple models, to see whether filtering changes the prediction results. For more information about creating predictions and other types of queries, see [Association Model Query Examples](../../2014/analysis-services/data-mining/association-model-query-examples.md).  
  
## See Also  
 [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-association-models-analysis-services-data-mining.md)   
 [PredictAssociation &#40;DMX&#41;](/sql/dmx/predictassociation-dmx)   
 [Create a Prediction Query Using the Prediction Query Builder](../../2014/analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md)  
  
  
