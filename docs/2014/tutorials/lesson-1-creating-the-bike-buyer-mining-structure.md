---
title: "Lesson 1: Creating the Bike Buyer Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: a73ac60b-660f-458a-bd2f-993fbeba7226
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 1: Creating the Bike Buyer Mining Structure
  In this lesson, you will create a mining structure that allows you to predict whether a potential customer of [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] will purchase a bicycle. If you are unfamiliar with mining structures and their role in data mining, see [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-structures-analysis-services-data-mining.md).  
  
 The Bike Buyer mining structure that you will create in this lesson supports adding mining models based on the [Microsoft Clustering Algorithm](../../2014/analysis-services/data-mining/microsoft-clustering-algorithm.md)[Microsoft Decision Trees Algorithm](../../2014/analysis-services/data-mining/microsoft-decision-trees-algorithm.md). In later lessons, you will use the clustering mining models to explore the different ways in which customers can be grouped, and will use decision tree mining models to predict whether or not a potential customer will purchase a bicycle.  
  
## CREATE MINING STRUCTURE Statement  
 To create a mining structure, you use the [CREATE MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/create-mining-structure-dmx) statement. The code in the statement can be broken into the following parts:  
  
-   Naming the structure.  
  
-   Defining the key column.  
  
-   Defining the mining columns.  
  
-   Defining an optional testing data set.  
  
 The following is a generic example of the CREATE MINING STRUCTURE statement:  
  
```  
CREATE MINING STRUCTURE [<mining structure name>]  
(  
    <key column>,  
    <mining structure columns>  
)   
WITH HOLDOUT (<holdout specifier>)  
```  
  
 The first line of the code defines the name of the structure:  
  
```  
CREATE MINING STRUCTURE [<mining structure name>]  
```  
  
 For information about naming an object in Data Mining Extensions (DMX), see [Identifiers &#40;DMX&#41;](/sql/dmx/identifiers-dmx).  
  
 The next line of the code defines the key column for the mining structure, which uniquely identifies an entity in the source data:  
  
```  
<key column>,  
```  
  
 In the mining structure you will create, the customer identifier, `CustomerKey`, defines an entity in the source data.  
  
 The next line of the code is used to define the mining columns that will be used by the mining models associated with the mining structure:  
  
```  
<mining structure columns>  
```  
  
 You can use the DISCRETIZE function within \<mining structure columns> to discretize continuous columns by using the following syntax:  
  
 `DISCRETIZE(<method>,<number of buckets>)`  
  
 For more information about discretizing columns, see [Discretization Methods &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/discretization-methods-data-mining.md). For more information about the types of mining structure columns that you can define, see [Mining Structure Columns](../../2014/analysis-services/data-mining/mining-structure-columns.md).  
  
 The final line of the code defines an optional partition in the mining structure:  
  
```  
WITH HOLDOUT (<holdout specifier>)  
```  
  
 You specify some portion of the data to use for testing mining models that are related to the structure, and the remaining data is used for training the models. By default, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] creates a test data set that contains 30 percent of all case data. You will add the specification that the test data set should contain 30 percent of the cases up to a maximum of 1000 cases. If 30 percent of the cases is less than 1000, the test data set will contain the smaller amount.  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Create a new blank query.  
  
-   Alter the query to create the mining structure.  
  
-   Execute the query.  
  
## Creating the Query  
 The first step is to connect to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and create a new DMX query in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
#### To create a new DMX query in SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
2.  In the **Connect to Server** dialog box, for **Server type**, select **Analysis Services**. In **Server name**, type `LocalHost`, or type the name of the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you want to connect to for this lesson. Click **Connect**.  
  
3.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX** to open the **Query Editor** and a new, blank query.  
  
## Altering the Query  
 The next step is to modify the CREATE MINING STRUCTURE statement described above to create the Bike Buyer mining structure.  
  
#### To customize the CREATE MINING STRUCTURE statement  
  
1.  In the Query Editor, copy the generic example of the CREATE MINING STRUCTURE statement into the blank query.  
  
2.  Replace the following:  
  
    ```  
    [<mining structure>]   
    ```  
  
     with:  
  
    ```  
    [Bike Buyer]  
    ```  
  
3.  Replace the following:  
  
    ```  
    <key column>   
    ```  
  
     with:  
  
    ```  
    CustomerKey LONG KEY  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining structure columns>   
    ```  
  
     with:  
  
    ```  
    [Age] LONG DISCRETIZED(Automatic,10),  
    [Bike Buyer] LONG DISCRETE,  
    [Commute Distance] TEXT DISCRETE,  
    [Education] TEXT DISCRETE,  
    [Gender] TEXT DISCRETE,  
    [House Owner Flag] TEXT DISCRETE,  
    [Marital Status] TEXT DISCRETE,  
    [Number Cars Owned] LONG DISCRETE,  
    [Number Children At Home] LONG DISCRETE,  
    [Occupation] TEXT DISCRETE,  
    [Region] TEXT DISCRETE,  
    [Total Children]LONG DISCRETE,  
    [Yearly Income] DOUBLE CONTINUOUS  
    ```  
  
5.  Replace the following:  
  
    ```  
    WITH HOLDOUT (holdout specifier>)  
    ```  
  
     with:  
  
    ```  
    WITH HOLDOUT (30 PERCENT or 1000 CASES)  
    ```  
  
     The complete mining structure statement should now be as follows:  
  
    ```  
    CREATE MINING STRUCTURE [Bike Buyer]  
    (  
       [Customer Key] LONG KEY,  
       [Age]LONG DISCRETIZED(Automatic,10),  
       [Bike Buyer] LONG DISCRETE,  
       [Commute Distance] TEXT DISCRETE,  
       [Education] TEXT DISCRETE,  
       [Gender] TEXT DISCRETE,  
       [House Owner Flag] TEXT DISCRETE,  
       [Marital Status] TEXT DISCRETE,  
       [Number Cars Owned]LONG DISCRETE,  
       [Number Children At Home]LONG DISCRETE,  
       [Occupation] TEXT DISCRETE,  
       [Region] TEXT DISCRETE,  
       [Total Children]LONG DISCRETE,  
       [Yearly Income] DOUBLE CONTINUOUS  
    )  
    WITH HOLDOUT (30 PERCENT or 1000 CASES)  
  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Bike Buyer Structure.dmx`.  
  
## Executing the Query  
 The final step is to execute the query. After a query is created and saved, it needs to be executed. That is, the statement needs to be run in order to create the mining structure on the server. For more information about executing queries in Query Editor, see [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../relational-databases/scripting/database-engine-query-editor-sql-server-management-studio.md).  
  
#### To execute the query  
  
1.  In Query Editor, on the toolbar, click **Execute**.  
  
     The status of the query is displayed in the **Messages** tab at the bottom of Query Editor after the statement finishes executing. Messages should display:  
  
    ```  
    Executing the query   
    Execution complete  
    ```  
  
     A new structure named **Bike Buyer** now exists on the server.  
  
 In the next lesson, you will add mining models to the structure you just created.  
  
## Next Lesson  
 [Lesson 2: Adding Mining Models to the Bike Buyer Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-bike-buyer-mining-structure.md)  
  
  
