---
title: "Lesson 2: Adding Mining Models to the Market Basket Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d96a7a7d-35d7-4b34-abb5-f0822c256253
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 2: Adding Mining Models to the Market Basket Mining Structure
  In this lesson, you will add two mining models to the Market Basket mining structure that you created in [Lesson 1: Creating the Market Basket Mining Structure](../../2014/tutorials/lesson-1-creating-the-market-basket-mining-structure.md). These mining models will allow you to create predictions.  
  
 To predict the types of products that customers tend to purchase at the same time, you will create two mining models using the [Microsoft Association Algorithm](../../2014/analysis-services/data-mining/microsoft-association-algorithm.md) and two different values for the *MINIMUM_PROBABILTY* parameter.  
  
 *MINIMUM_PROBABILTY* is a [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm parameter that helps to determine the number of rules that a mining model will contain by specifying the minimum probability that a rule must have. For example, setting this value to 0.4 specifies that a rule can be generated only if the combination of products that the rule describes has at least a forty percent probability of occurring.  
  
 You will view the effect of changing the *MINIMUM_PROBABILTY* parameter in a later lesson.  
  
## ALTER MINING STRUCTURE Statement  
 To add a mining model that contains a nested table to a mining structure, you use the [ALTER MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/alter-mining-structure-dmx?view=sql-server-2016) statement. The code in the statement can be broken into the following parts:  
  
-   Identifying the mining structure  
  
-   Naming the mining model  
  
-   Defining the key column  
  
-   Defining the input and predictable columns  
  
-   Defining the nested table columns  
  
-   Identifying the algorithm and parameter changes  
  
 The following is a generic example of the `ALTER MINING STRUCTURE` statement that adds a mining model to a structure that includes nested table columns:  
  
```  
ALTER MINING STRUCTURE [<Mining Structure Name>]  
ADD MINING MODEL [<Mining Model Name>]  
(  
    [<key column>],  
    <mining model column> <usage>,  
    <table columns>  
    (  [<nested key column>],  
       <nested mining model columns> )  
) USING <algorithm>( <algorithm parameters> )  
```  
  
 The first line of the code identifies the existing mining structure to which the mining model will be added:  
  
```  
ALTER MINING STRUCTURE [<mining structure name>]  
```  
  
 The next line of the code names the mining model that will be added to the mining structure:  
  
```  
ADD MINING MODEL [<mining model name>]  
```  
  
 For information about naming an object in Data Mining Extensions (DMX), see [Identifiers &#40;DMX&#41;](/sql/dmx/identifiers-dmx).  
  
 The next lines of the code define the columns in the mining structure that will be used by the mining model:  
  
```  
[<key column>],  
<mining model columns> <usage>,  
```  
  
 You can only use columns that already exist in the mining structure.  
  
 The first column in the list of mining model columns must be the key column in the mining structure. However, you do not have to type `KEY` after the key column to specify usage. That is because you have already defined the column as a key when you created the mining structure.  
  
 The remaining lines specify the usage of the columns in the new mining model. You can specify that a column in the mining model will be used for prediction by using the following syntax:  
  
```  
<column name> PREDICT,  
```  
  
 If you do not specify usage, you do not have to include a data mining structure column in the list. All columns that are used by the referenced data mining structure are automatically available for use by the mining models that are based on that structure. However, the model will not use the columns for training unless you specify the usage.  
  
 The last line in the code defines the algorithm and algorithm parameters that will be used to generate the mining model.  
  
```  
) USING <algorithm>( <algorithm parameters> )  
```  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Add an association mining model to the structure using the default probability  
  
-   Add an association mining model to the structure using a modified probability  
  
## Adding an Association Mining Model to the Structure Using the Default MINIMUM_PROBABILITY  
 The first task is to add a new mining model to the Market Basket mining structure based on the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm using the default value for *MINIMUM_PROBABILITY*.  
  
#### To add an Association mining model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
    > [!NOTE]  
    >  To create a DMX query against a specific [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, right-click the database instead of the instance.  
  
2.  Copy the generic example of the `ALTER MINING STRUCTURE` statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <mining structure name>   
    ```  
  
     with:  
  
    ```  
    [Market Basket]  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining model name>   
    ```  
  
     with:  
  
    ```  
    [Default Association]  
    ```  
  
5.  Replace the following:  
  
    ```  
    [<key column>],  
    <mining model columns>,  
    <table columns>  
    (  [<nested key column>],  
       <nested mining model columns> )  
    ```  
  
     with:  
  
    ```  
    OrderNumber,  
        [Products] PREDICT (  
            [Model]  
        )  
    ```  
  
     In this case, the `[Products]` table has been designated as the predictable column`.` Also, the `[Model]` column is included in the list of nested table columns because it is the key column of the nested table.  
  
    > [!NOTE]  
    >  Remember that a nested key is different from a case key. A case key is a unique identifier of the case, whereas the nested key is an attribute that you want to model.  
  
6.  Replace the following:  
  
    ```  
    USING <algorithm>( <algorithm parameters> )  
    ```  
  
     with:  
  
    ```  
    Using Microsoft_Association_Rules  
    ```  
  
     The resulting statement should now be as follows:  
  
    ```  
    ALTER MINING STRUCTURE [Market Basket]  
    ADD MINING MODEL [Default Association]  
    (  
        OrderNumber,  
        [Products] PREDICT (  
            [Model]  
        )  
    )  
    Using Microsoft_Association_Rules  
    ```  
  
7.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
8.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Default_Association_Model.dmx`.  
  
9. On the toolbar, click the **Execute** button.  
  
## Adding an Association Mining Model to the Structure Changing the Default MINIMUM_PROBABILITY  
 The next task is to add a new mining model to the Market Basket mining structure based on the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm, and change the default value for MINIMUM_PROBABILITY to 0.01. Changing the parameter will cause the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm to create more rules.  
  
#### To add an Association mining model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the `ALTER MINING STRUCTURE` statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <mining structure name>   
    ```  
  
     with:  
  
    ```  
    Market Basket  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining model name>   
    ```  
  
     with:  
  
    ```  
    [Modified Association]  
    ```  
  
5.  Replace the following:  
  
    ```  
    <mining model columns>,  
    <table columns>  
    (  [<nested key column>],  
       <nested mining model columns> )  
    ```  
  
     with:  
  
    ```  
    OrderNumber,  
    [Products] PREDICT (  
            [Model]  
        )  
    ```  
  
     In this case, the `[Products]` table has been designated as the predictable column. Also, the `[MODEL]` column is included in the list because it is the key column in the nested table.  
  
6.  Replace the following:  
  
    ```  
    USING <algorithm>( <algorithm parameters> )  
    ```  
  
     with:  
  
    ```  
    USING Microsoft_Association_Rules (Minimum_Probability = 0.1)  
    ```  
  
     The resulting statement should now be as follows:  
  
    ```  
    ALTER MINING STRUCTURE [Market Basket]  
    ADD MINING MODEL [Modified Assocation]  
    (  
        OrderNumber,  
        [Products] PREDICT (  
            [Model]  
        )  
    )  
    USING Microsoft_Association_Rules (Minimum_Probability = 0.1)  
    ```  
  
7.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
8.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Modified Association_Model.dmx`.  
  
9. On the toolbar, click the **Execute** button.  
  
 In this next lesson you will process the Market Basket mining structure together with its associated mining models.  
  
## Next Lesson  
 [Lesson 3: Processing the Market Basket Mining Structure](../../2014/tutorials/lesson-3-processing-the-market-basket-mining-structure.md)  
  
  
