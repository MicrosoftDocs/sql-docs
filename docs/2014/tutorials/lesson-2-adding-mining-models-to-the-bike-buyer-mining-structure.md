---
title: "Lesson 2: Adding Mining Models to the Bike Buyer Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 03fe44c5-6452-4ed0-95f6-9682670c0f52
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 2: Adding Mining Models to the Bike Buyer Mining Structure
  In this lesson, you will add two mining models to the Bike Buyer mining structure that you created [Lesson 1: Creating the Bike Buyer Mining Structure](../../2014/tutorials/lesson-1-creating-the-bike-buyer-mining-structure.md). These mining models will allow you to explore the data using one model, and to create predictions using another.  
  
 To explore how potential customers can be categorized by their characteristics, you will create a mining model based on the [Microsoft Clustering Algorithm](../../2014/analysis-services/data-mining/microsoft-clustering-algorithm.md). In a later lesson, you will explore how this algorithm finds clusters of customers who share similar characteristics. For example, you might find that certain customers tend to live close to each other, commute by bicycle, and have similar education backgrounds. You can use these clusters to better understand how different customers are related, and to use the information to create a marketing strategy that targets specific customers.  
  
 To predict whether a potential customer is likely to buy a bicycle, you will create a mining model based on the [Microsoft Decision Trees Algorithm](../../2014/analysis-services/data-mining/microsoft-decision-trees-algorithm.md). This algorithm looks through the information that is associated with each potential customer, and finds characteristics that are useful in predicting if they will buy a bicycle. It then compares the values of the characteristics of previous bike buyers against new potential customers to determine whether the new potential customers are likely to buy a bicycle.  
  
## ALTER MINING STRUCTURE Statement  
 In order to add a mining model to the mining structure, you use the [ALTER MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/alter-mining-structure-dmx?view=sql-server-2016) statement. The code in the statement can be broken into the following parts:  
  
-   Identifying the mining structure  
  
-   Naming the mining model  
  
-   Defining the key column  
  
-   Defining the input and predictable columns  
  
-   Identifying the algorithm and parameter changes  
  
 The following is a generic example of the ALTER MINING MODEL statement:  
  
```  
ALTER MINING STRUCTURE [<mining structure name>]  
ADD MINING MODEL [<mining model name>]  
(  
    [<key column>],  
    <mining model columns>,  
) USING <algorithm name>( <algorithm parameters> )  
WITH FILTER (<expression>)  
```  
  
 The first line of the code identifies the existing mining structure to which the mining models will be added:  
  
```  
ALTER MINING STRUCTURE [<mining structure name>]  
```  
  
 The next line of the code names the mining model that will be added to the mining structure:  
  
```  
ADD MINING MODEL [<mining model name>]  
```  
  
 For information about naming an object in DMX, see [Identifiers &#40;DMX&#41;](/sql/dmx/identifiers-dmx).  
  
 The next lines of the code define columns from the mining structure that will be used by the mining model:  
  
```  
[<key column>],  
<mining model columns>  
```  
  
 You can only use columns that already exist in the mining structure, and the first column in the list must be the key column from the mining structure.  
  
 The next line of the code defines the mining algorithm that generates the mining model and the algorithm parameters that you can set on the algorithm:  
  
```  
) USING <algorithm name>( <algorithm parameters> )  
```  
  
 For more information about the algorithm parameters that you can adjust, see [Microsoft Decision Trees Algorithm](../../2014/analysis-services/data-mining/microsoft-decision-trees-algorithm.md) and [Microsoft Clustering Algorithm](../../2014/analysis-services/data-mining/microsoft-clustering-algorithm.md).  
  
 You can specify that a column in the mining model be used for prediction by using the following syntax:  
  
```  
<mining model column> PREDICT  
```  
  
 The final line of the code, which is optional, defines a filter that is applied when training and testing the model. For more information about how to apply filters to mining models, see [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md).  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Add a decision tree mining model to the Bike Buyer structure by using the [!INCLUDE[msCoName](../includes/msconame-md.md)] Decision Trees algorithm  
  
-   Add a clustering mining model to the Bike Buyer structure by using the [!INCLUDE[msCoName](../includes/msconame-md.md)] Clustering algorithm  
  
-   Because you want to see results for all cases, you will not yet add a filter to either model.  
  
## Adding a Decision Tree Mining Model to the Structure  
 The first step is to add a mining model based on the [!INCLUDE[msCoName](../includes/msconame-md.md)] Decision Trees algorithm.  
  
#### To add a decision tree mining model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX** to open Query Editor and a new, blank query.  
  
2.  Copy the generic example of the ALTER MINING STRUCTURE statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <mining structure name>   
    ```  
  
     with:  
  
    ```  
    [Bike Buyer]  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining model name>   
    ```  
  
     with:  
  
    ```  
    Decision Tree  
    ```  
  
5.  Replace the following:  
  
    ```  
    <mining model columns>,  
    ```  
  
     with:  
  
    ```  
    (  
       CustomerKey,  
       [Age],  
       [Bike Buyer] PREDICT,  
       [Commute Distance],  
       [Education],  
       [Gender],  
       [House Owner Flag],  
       [Marital Status],  
       [Number Cars Owned],  
       [Number Children At Home],  
       [Occupation],  
       [Region],  
       [Total Children],  
       [Yearly Income]  
    ```  
  
     In this case, the `[Bike Buyer]` column has been designated as the PREDICT column.  
  
6.  Replace the following:  
  
    ```  
    USING <algorithm name>( <algorithm parameters> )   
    ```  
  
     with:  
  
    ```  
    Using Microsoft_Decision_Trees  
    WITH DRILLTHROUGH  
    ```  
  
     The WITH DRILLTHROUGH statement allows you to explore the cases that were used to build the mining model.  
  
     The resulting statement should now be as follows:  
  
    ```  
    ALTER MINING STRUCTURE [Bike Buyer]  
    ADD MINING MODEL [Decision Tree]  
    (  
       CustomerKey,  
       [Age],  
       [Bike Buyer] PREDICT,  
       [Commute Distance],  
       [Education],  
       [Gender],  
       [House Owner Flag],  
       [Marital Status],  
       [Number Cars Owned],  
       [Number Children At Home],  
       [Occupation],  
       [Region],  
       [Total Children],  
       [Yearly Income]  
    ) USING Microsoft_Decision_Trees  
    WITH DRILLTHROUGH  
    ```  
  
7.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
8.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `DT_Model.dmx`.  
  
9. On the toolbar, click the **Execute** button.  
  
## Adding a Clustering Mining Model to the Structure  
 You can now add a mining model to the Bike Buyer mining structure based on the [!INCLUDE[msCoName](../includes/msconame-md.md)] Clustering algorithm. Because the clustering mining model will use all the columns defined in the mining structure, you can use a shortcut to add the model to the structure by omitting the definition of the mining columns.  
  
#### To add a Clustering mining model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX** to open Query Editor opens and a new, blank query.  
  
2.  Copy the generic example of the ALTER MINING STRUCTURE statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <mining structure name>   
    ```  
  
     with:  
  
    ```  
    [Bike Buyer]  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining model>   
    ```  
  
     with:  
  
    ```  
    Clustering Model  
    ```  
  
5.  Delete the following:  
  
    ```  
    (  
        [<key column>],  
        <mining model columns>,  
    )  
    ```  
  
6.  Replace the following:  
  
    ```  
    USING <algorithm name>( <algorithm parameters> )  
    ```  
  
     with:  
  
    ```  
    USING Microsoft_Clustering  
    ```  
  
     The complete statement should now be as follows:  
  
    ```  
    ALTER MINING STRUCTURE [Bike Buyer]  
    ADD MINING MODEL [Clustering]  
    USING Microsoft_Clustering   
    ```  
  
7.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
8.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Clustering_Model.dmx`.  
  
9. On the toolbar, click the **Execute** button.  
  
 In the next lesson, you will process the models and the mining structure.  
  
## Next Lesson  
 [Lesson 3: Processing the Bike Buyer Mining Structure](../../2014/tutorials/lesson-3-processing-the-bike-buyer-mining-structure.md)  
  
  
