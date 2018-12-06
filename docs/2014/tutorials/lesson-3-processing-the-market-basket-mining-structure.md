---
title: "Lesson 3: Processing the Market Basket Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 095a043f-cf6f-45bb-a021-ae4e1b535c65
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 3: Processing the Market Basket Mining Structure
  In this lesson, you will use the [INSERT INTO &#40;DMX&#41;](/sql/dmx/insert-into-dmx) statement and the vAssocSeqLineItems and vAssocSeqOrders from the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] sample database to process the mining structures and mining models that you created in [Lesson 1: Creating the Market Basket Mining Structure](../../2014/tutorials/lesson-1-creating-the-market-basket-mining-structure.md) and [Lesson 2: Adding Mining Models to the Market Basket Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-market-basket-mining-structure.md).  
  
 When you process a mining structure, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] reads the source data and builds the structures that support mining models. When you process a mining model, the data defined by the mining structure is passed through the data mining algorithm that you chose. The algorithm searches for trends and patterns, and then stores this information in the mining model. The mining model, therefore, does not contain the actual source data, but instead contains the information that was discovered by the algorithm. For more information about processing mining models, see [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md).  
  
 You only have to reprocess a mining structure if you change a structure column or change the source data. If you add a mining model to a mining structure that has already been processed, you can use the `INSERT INTO MINING MODEL` statement to train the new mining model on the existing data.  
  
 Because the Market Basket mining structure contains a nested table, you will have to define the mining columns to be trained using the nested table structure, and use the `SHAPE` command to define the queries that pull the training data from the source tables.  
  
## INSERT INTO Statement  
 In order to train the Market Basket mining structure and its associated mining models, use the [INSERT INTO &#40;DMX&#41;](/sql/dmx/insert-into-dmx) statement. The code in the statement can be broken into the following parts.  
  
-   Identifying the mining structure  
  
-   Listing the columns in the mining structure  
  
-   Defining the training data using `SHAPE`  
  
 The following is a generic example of the `INSERT INTO` statement:  
  
```  
INSERT INTO MINING STRUCTURE [<mining structure name>]  
(  
   <mining structure columns>  
   [<nested table>]  
   ( SKIP, <skipped column> )  
)  
SHAPE {  
  OPENQUERY([<datasource>],'<SELECT statement>') }  
APPEND  
(   
  {OPENQUERY([<datasource>],'<nested SELECT statement>')  
}  
RELATE [<case key>] TO [<foreign key>]  
) AS [<nested table>]  
```  
  
 The first line of the code identifies the mining structure that you will train:  
  
```  
INSERT INTO MINING STRUCTURE [<mining structure name>]  
```  
  
 The next lines of the code specify the columns that are defined by the mining structure. You must list each column in the mining structure, and each column must map to a column contained within the source query data. You can use `SKIP` to ignore columns that exist in the source data but do not exist in the mining structure. For more information about how to use `SKIP`, see [INSERT INTO &#40;DMX&#41;](/sql/dmx/insert-into-dmx).  
  
```  
(  
   <mining structure columns>  
   [<nested table>]  
   ( SKIP, <skipped column> )  
)  
```  
  
 The final lines of the code define the data that will be used to train the mining structure. Because the source data is contained within two tables, you will use `SHAPE` to relate the tables.  
  
```  
SHAPE {  
  OPENQUERY([<datasource>],'<SELECT statement>') }  
APPEND  
(   
  {OPENQUERY([<datasource>],''<nested SELECT statement>'')  
}  
RELATE [<case key>] TO [<foreign key>]  
) AS [<nested table>]  
```  
  
 In this lesson, you use `OPENQUERY` to define the source data. For information about other methods of defining a query on the source data, see [&#60;source data query&#62;](/sql/dmx/source-data-query).  
  
## Lesson Tasks  
 You will perform the following task in this lesson:  
  
-   Process the Market Basket mining structure  
  
## Processing the Market Basket Mining Structure  
  
#### To process the mining structure by using INSERT INTO  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the INSERT INTO statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    [<mining structure>]  
    ```  
  
     with:  
  
    ```  
    Market Basket  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining structure columns>  
    [<nested table>]  
    ( SKIP, <skipped column> )  
    ```  
  
     with:  
  
    ```  
    [OrderNumber],  
    [Products]   
    (SKIP, [Model])  
    ```  
  
     In the statement, `Products` refers to the Products table defined by the SHAPE statement. `SKIP` is used to ignore the Model column, which exists in the source data as a key, but is not used by the mining structure.  
  
5.  Replace the following:  
  
    ```  
    SHAPE {  
      OPENQUERY([<datasource>],'<SELECT statement>') }  
    APPEND  
    (   
      {OPENQUERY([<datasource>],'<nested SELECT statement>')  
    }  
    RELATE [<case key>] TO [<foreign key>]  
    ) AS [<nested table>]  
    ```  
  
     with:  
  
    ```  
    SHAPE {  
      OPENQUERY([Adventure Works DW],'SELECT OrderNumber  
                FROM vAssocSeqOrders ORDER BY OrderNumber')}  
    APPEND  
    (   
      {OPENQUERY([Adventure Works DW],'SELECT OrderNumber, Model FROM   
        dbo.vAssocSeqLineItems ORDER BY OrderNumber, Model')  
    }  
    RELATE OrderNumber to OrderNumber   
    ) AS [Products]  
    ```  
  
     The source query references the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] data source defined in the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] sample project. It uses this data source to access the vAssocSeqLineItems and vAssocSeqOrders views. These views contain the source data that will be used to train the mining model. If you have not created this project or these views, see [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md).  
  
     Within the `SHAPE` command, you will use `OPENQUERY` to define two queries. The first query defines the parent table, and the second query defines the nested table. The two tables are related using the OrderNumber column, which exists in both tables.  
  
     The complete statement should now be as follows:  
  
    ```  
    INSERT INTO MINING STRUCTURE [Market Basket]  
    (  
       [OrderNumber],[Products] (SKIP, [Model])  
    )  
    SHAPE {  
      OPENQUERY([Adventure Works DW],'SELECT OrderNumber  
                FROM vAssocSeqOrders ORDER BY OrderNumber')}  
    APPEND  
    (   
      {OPENQUERY([Adventure Works DW],'SELECT OrderNumber, Model FROM   
        dbo.vAssocSeqLineItems ORDER BY OrderNumber, Model')  
    }  
    RELATE OrderNumber to OrderNumber   
    ) AS [Products]  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Process Market Basket.dmx`.  
  
8.  On the toolbar, click the **Execute** button.  
  
 After the query has finished running, you can view the patterns and itemsets that were found, view associations, or filter by itemset, probability, or importance. To view this information, in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], right-click the name of the data model, and then click **Browse**.  
  
 In the next lesson, you will create several predictions based on the mining models that you added to the Market Basket structure.  
  
## Next Lesson  
 [Lesson 4: Executing Market Basket Predictions](../../2014/tutorials/lesson-4-executing-market-basket-predictions.md)  
  
  
