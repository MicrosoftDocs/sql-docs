---
title: "Lesson 4: Browsing the Bike Buyer Mining Models | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 8de3c500-f881-42da-a096-b6c03300d58d
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 4: Browsing the Bike Buyer Mining Models
  In this lesson, you will use the [SELECT (DMX)](/sql/dmx/select-dmx) statement to explore the content in the decision tree and clustering mining models that you created in [Lesson 2: Adding Mining Models to the Predictive Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-bike-buyer-mining-structure.md).  
  
 The columns contained in a mining model are not the columns defined by the mining structure, but instead are a specific set of columns that describe the trends and patterns that are found by the algorithm. These mining model columns are described in the [DMSCHEMA_MINING_MODEL_CONTENT Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-model-content-rowset) schema rowset. For example, the MODEL_NAME column in the content schema rowset contains the name of the mining model. For a clustering mining model, the NODE_CAPTION column contains the name of each cluster, and the NODE_DESCRIPTION column contains a description of the characteristics of each cluster. You can browse these columns by using the SELECT FROM \<model>.CONTENT statement in DMX. You can also use this statement to explore the data that was used to create the mining model. Drillthrough must be enabled on the mining structure in order to use this statement. For more information about the statement, see [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](/sql/dmx/select-from-model-content-dmx).  
  
 You can also return all the states of a discrete column by using the SELECT DISTINCT statement. For example, if you perform this operation on a gender column, the query will return `male` and `female`.  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Explore the content contained within the mining models  
  
-   Return the cases from the source data that was used to train the mining models  
  
-   Explore the different states available for a specific discrete column  
  
## Returning the Content of a Mining Model  
 In this lesson, you use the [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](/sql/dmx/select-from-model-dimension-content-dmx) statement to return the contents of the clustering model.  
  
 The following is a generic example of the SELECT FROM \<model>.CONTENT statement:  
  
```  
SELECT <select list> FROM [<mining model>].CONTENT  
WHERE <where clause>  
```  
  
 The first line of the code defines the columns to return from the mining model content, and the mining model they are associated with:  
  
```  
SELECT <select list> FROM [<mining model].CONTENT  
```  
  
 The .CONTENT clause next to the name of the mining model specifies that you are returning content from the mining model. For more information about the columns contained in the mining model, see [DMSCHEMA_MINING_MODEL_CONTENT Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-model-content-rowset).  
  
 You can optionally use the final line of the code to filter the results returned by the statement:  
  
```  
WHERE <where clause>  
```  
  
 For example, if you want to restrict the results of the query to only the clusters that contain a high number of cases, you can add the following WHERE clause to the SELECT statement:  
  
```  
WHERE NODE_SUPPORT > 100  
```  
  
 For more information about using the WHERE statement, see [SELECT &#40;DMX&#41;](/sql/dmx/select-dmx).  
  
#### To return the content of the clustering mining model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the SELECT FROM \<model>.CONTENT statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <select list>   
    ```  
  
     with:  
  
    ```  
    *  
    ```  
  
     You can also replace * with a list of any of the columns contained within the [DMSCHEMA_MINING_MODEL_CONTENT Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-model-content-rowset).  
  
4.  Replace the following:  
  
    ```  
    [<mining model>]   
    ```  
  
     with:  
  
    ```  
    [Clustering]  
    ```  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT * FROM [Clustering].CONTENT  
    ```  
  
5.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
6.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `SELECT_CONTENT.dmx`.  
  
7.  On the toolbar, click the **Execute** button.  
  
     The query returns the content of the mining model.  
  
## Use Drillthrough  
 The next step is to use the drillthrough statement to return a sampling of the cases that were used to train the decision tree mining model. In this lesson, you use the [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](/sql/dmx/select-from-model-content-dmx) statement to return the contents of the decision tree model.  
  
 The following is a generic example of the SELECT FROM \<model>.CASES statement:  
  
```  
SELECT <select list>   
FROM [<mining model>].CASES  
WHERE IsInNode('<node id>')  
```  
  
 The first line of the code defines the columns to return from the source data, and the mining model they are contained within:  
  
```  
SELECT <select list> FROM [<mining model>].CASES  
```  
  
 The .CASES clause specifies that you are performing a drillthrough query. In order to use drillthrough you must enable drillthrough when you create the mining model.  
  
 The final line of the code is optional and specifies the node in the mining model that you are requesting cases from:  
  
```  
WHERE IsInNode('<node id>')  
```  
  
 For more information about using the WHERE statement with IsInNode, see [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](/sql/dmx/select-from-model-content-dmx).  
  
#### To return the cases that were used to train the mining model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the SELECT FROM \<model>.CASES statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <select list>   
    ```  
  
     with:  
  
    ```  
    *  
    ```  
  
     You can also replace * with a list of any of the columns contained within the source data (such as [Bike Buyer]).  
  
4.  Replace the following:  
  
    ```  
    [<mining model>]   
    ```  
  
     with:  
  
    ```  
    [Decision Tree]  
    ```  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT *   
    FROM [Decision Tree].CASES  
    ```  
  
5.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
6.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `SELECT_DRILLTHROUGH.dmx`.  
  
7.  On the toolbar, click the **Execute** button.  
  
     The query returns the source data that was used to train the decision tree mining model.  
  
## Return the States of a Discrete Mining Model Column  
 The next step is to use the SELECT DISTINCT statement to return the different possible states in the specified mining model column.  
  
 The following is a generic example of the SELECT DISTINCT statement:  
  
```  
SELECT DISTINCT [<column>]   
FROM [<mining model>]  
```  
  
 The first line of the code defines the mining model columns for which the states are returned:  
  
```  
SELECT DISTINCT [<column>]   
```  
  
 You must include DISTINCT in order to return all of the states of the column. If you exclude DISTINCT, then the full statement becomes a shortcut for a prediction and returns the most likely state of the specified column. For more information, see [SELECT &#40;DMX&#41;](/sql/dmx/select-dmx).  
  
#### To return the states of a discrete column  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the SELECT Distinct statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    [<column,name>   
    ```  
  
     with:  
  
    ```  
    [Bike Buyer]  
    ```  
  
4.  Replace the following:  
  
    ```  
    [<mining model>]   
    ```  
  
     with:  
  
    ```  
    [Decision Tree]  
    ```  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT DISTINCT [Bike Buyer]   
    FROM [Decision Tree]  
    ```  
  
5.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
6.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `SELECT_DISCRETE.dmx`.  
  
7.  On the toolbar, click the **Execute** button.  
  
     The query returns the possible states of the Bike Buyer column.  
  
 In the next lesson, you will predict whether potential customers will be bike buyers by using the decision tree mining model.  
  
## Next Lesson  
 [Lesson 5: Executing Prediction Queries](../../2014/tutorials/lesson-5-executing-prediction-queries.md)  
  
  
