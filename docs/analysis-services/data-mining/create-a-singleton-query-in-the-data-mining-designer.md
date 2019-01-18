---
title: "Create a Singleton Query in the Data Mining Designer | Microsoft Docs"
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
# Create a Singleton Query in the Data Mining Designer
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A singleton query is useful if you want to create a prediction for a single case. For more information about singleton queries, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md).  
  
 In the **Mining Model Prediction** tab of Data Mining Designer, you can create many different types of queries. You can create a query by using the designer, or by typing Data Mining Extensions (DMX) statements. You can also start with the designer and modify the query that it creates by changing the DMX statements, or by adding a WHERE or ORDER BY clause.  
  
 To switch between the query design view and the query text view, click the first button on the toolbar. When you are in the query text view, you can view the DMX code that Prediction Query Builder creates. You can also run the query, modify the query, and run the modified query. However, the modified query is not persisted if you switch back to the query design view.  
  
 The following code shows an example of a singleton query against the targeted mailing model, TM_Decision_Tree.  
  
```  
SELECT [Bike Buyer], PredictProbability([Bike Buyer]) as ProbableBuyer  
FROM [TM_Decision_Tree]  
NATURAL PREDICTION JOIN  
(SELECT '2' AS [Number Children At Home], '45' as [Age])  
AS [t]  
```  
  
 The following steps explain how to create this prediction query.  
  
### To create a singleton query by using the Data Mining Designer  
  
1.  Click the **Mining Model Prediction** tab in Data Mining Designer.  
  
2.  Click **Select Model** on the **Mining Model** table.  
  
     The **Select Mining Model** dialog box opens to show all the mining structures that exist in the current project.  
  
     Select the model that you want to use for creating a prediction.  
  
     For example, to create the sample code shown at the start of this topic, select TM_Decision_Tree, and then click **OK**.  
  
3.  Click **Singleton query** on the toolbar of the **Mining Model Prediction** tab.  
  
     The **Singleton Query Input** table appears on the tab, with the columns automatically mapped to the columns in the **Mining Model** table.  
  
4.  On the **Singleton Query Input** table, select values in the **Value** column to describe the case for which you want to create a prediction.  
  
     For example, select **2** for **Number Children At Home**, and then type **45** for **Age**.  
  
5.  Drag a predictable column from the **Mining Model** table to the **Source** column at the bottom of the tab. Optionally, you can type an alias for the column.  
  
     For example, drag **Bike Buyer** to the **Source** column.  
  
6.  Add any additional functions to the query by selecting **Prediction Function** or **Custom Expression** from the drop-down list in the **Source** column.  
  
     For example, click **Prediction Function**, and select **PredictProbability**.  
  
7.  Click **Criteria/Argument** in the **PredictProbability** row, and type the name of the column to predict, and optionally a specific value to predict.  
  
     For example, type **[Bike Buyer], 1**.  
  
8.  Click the **Alias** box in the **PredictProbability** row, and type a name to refer to the new column.  
  
     For example, type **ProbableBuyer**.  
  
9. Click **Switch to query result view** on the toolbar of the **Mining Model Prediction** tab.  
  
     A new screen opens to show the result of the query. To view the DMX statement that you just created, click **SQL**.  
  
## See Also  
 [Prediction Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/prediction-queries-data-mining.md)  
  
  
