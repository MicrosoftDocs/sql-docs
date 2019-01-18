---
title: "Apply Prediction Functions to a Model | Microsoft Docs"
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
# Apply Prediction Functions to a Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  To create a prediction query in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining, you must first select the mining model on which the query will be based. You can select any mining model that exists in the current project.  
  
 After you have selected a model, add a *prediction function* to the query. A prediction function can be used to get a prediction, but you can also add prediction functions that return related statistics, such as a the probability of the predicted value, or information that was used in generating the prediction.  
  
 Prediction functions can return the following types of values:  
  
-   The name of the predictable attribute, and the value that is predicted.  
  
-   Statistics about the distribution and variance of the predicted values.  
  
-   The probability of a specified outcome, or of all possible outcomes.  
  
-   The top or bottom scores or values.  
  
-   Values associated with a specified node, object, or attribute.  
  
 The type of prediction functions that are available depend on the type of model you are working with. For example, prediction functions applied to decision tree models can return rules and node descriptions; prediction functions for time series models can return  the lag and other information specific to time series.  
  
 For a list of the prediction functions that are supported for almost all model types, see [General Prediction Functions &#40;DMX&#41;](../../dmx/general-prediction-functions-dmx.md).  
  
 For examples of how to query a specific type of mining model, see the algorithm reference topic, in [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md).  
  
### Choose a mining model to use for prediction  
  
1.  From [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click the model, and select **Build Prediction Query**.  
  
     --OR --  
  
     In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the tab, **Mining Model Prediction**, and then click **Select Model** in the  **Mining Model** table.  
  
2.  In the **Select Mining Model** dialog box, select a mining model, and then click **OK**.  
  
     You can choose any model within the current [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. To create a query using a model in a different database, you must either open a new query window in the context of that database, or open the solution file that contains that model.  
  
### Add prediction functions to a query  
  
1.  In the **Prediction Query Builder**, configure the input data used for prediction, either by providing values in the **Singleton Query Input** dialog box, or by mapping the model to an external data source.  
  
     For more information, see [Choose and Map Input Data for a Prediction Query](../../analysis-services/data-mining/choose-and-map-input-data-for-a-prediction-query.md).  
  
    > [!WARNING]  
    >  It is not required that you provide inputs to generate predictions. When there is no input, the algorithm will typically return the mostly likely predicted value across all possible inputs.  
  
2.  Click the **Source** column, and choose a value from the list:  
  
    |||  
    |-|-|  
    |**\<model name>**|Select this option to include values from the mining model in the output. You can only add predictable columns.<br /><br /> When you add a column from the model, the result returned is the non-distinct list of values in that column.<br /><br /> The columns that you add with this option are included in the SELECT portion of the resulting DMX statement.|  
    |**Prediction Function**|Select this option to browse a list of prediction functions.<br /><br /> The values or functions you select are added to the SELECT portion of the resulting DMX statement.<br /><br /> The list of prediction functions is not filtered or constrained by the type of model you have selected. Therefore, if you have any doubt about whether the function is supported for the current model type, you can just add the function to the list and see if there is an error.<br /><br /> List items that are preceded by $ (such as $AdjustedProbability) represent columns from the nested table that is output when you use the function, **PredictHistogram**. These are shortcuts that you can use to return a single column and not a nested table.|  
    |**Custom Expression**|Select this option to type a custom expression and then assign an alias to the output.<br /><br /> The custom expression is added to the SELECT portion of the resulting DMX prediction query.<br /><br /> This option is useful if you want to add text for output with each row, to call VB functions, or to call custom stored procedures.<br /><br /> For information about using VBA and Excel functions from DMX, see [VBA functions in MDX and DAX](../../mdx/vba-functions-in-mdx-and-dax.md).|  
  
3.  After adding each function or expression, switch to DMX view to see how the function is added within the DMX statement.  
  
    > [!WARNING]  
    >  The Prediction Query Builder does not validate the DMX until you click **Results**. Often, you will find that the expression that is produced by the query builder is not valid DMX. Typical causes are referencing a column that is not related to the predictable column, or trying to predict a column in a nested table, which requires a sub-SELECT statement. At this point, you can switch to DMX view and continue editing the statement.  
  
### Example: Create a query on a clustering model  
  
1.  If you do not have a clustering model available for building this sample query, create the model, [TM_Clustering], using the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c).  
  
2.  From [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click the model, [TM_Clustering], and select **Build Prediction Query**.  
  
3.  From the **Mining Model** menu, select **Singleton Query**.  
  
4.  In the **Singleton Query Input** dialog box, set the following values as inputs:  
  
    -   Gender = M  
  
    -   Commute Distance = 5-10 miles  
  
5.  In the query grid, for **Source**, select TM_Clustering mining model, and add the column, [Bike Buyer].  
  
6.  For **Source**, select **Prediction Function**, and add the function, **Cluster**.  
  
7.  For **Source**, select **Prediction Function**, add the function, **PredictSupport**, and drag the model column [Bike Buyer] into the **Criteria/Argument** box. Type **Support** in the **Alias** column.  
  
     Copy the expression representing the prediction function and column reference from the **Criteria/Argument** box.  
  
8.  For **Source**, select **Custom Expression**, type an alias, and then reference the Excel CEILING function by using the following syntax:  
  
    ```  
    Excel![CEILING](<arguments) as <return type>  
    ```  
  
     Paste the column reference in as the argument to the function.  
  
     For example, the following expression returns the CEILING of the support value:  
  
    ```  
    EXCEL!CEILING(PredictSupport([TM_Clustering].[Bike Buyer]),2)  
    ```  
  
     Type CEILING in the **Alias** column.  
  
9. Click **Switch to query text view** to review the DMX statement that was generated, and then click **Switch to query result view** to see the columns output by the prediction query.  
  
     The following table shows the expected results:  
  
    |Bike Buyer|$Cluster|SUPPORT|CEILING|  
    |----------------|--------------|-------------|-------------|  
    |0|Cluster 8|954|953.948638926372|  
  
 If you want to add other clauses elsewhere in the statement-for example, if you want to add a WHERE clause-you cannot add it by using the grid; you must switch to DMX view first.  
  
## See Also  
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)  
  
  
