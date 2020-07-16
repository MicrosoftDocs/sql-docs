---
title: "Lesson 1: Creating the Market Basket Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: a817c8d1-aff4-42b4-b194-ad9cc1c60f35
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 1: Creating the Market Basket Mining Structure
  In this lesson, you will create a mining structure that allows you to predict what [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] products a customer tends to purchase at the same time. If you are unfamiliar with mining structures and their role in data mining, see [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-structures-analysis-services-data-mining.md).  
  
 The association mining structure that you will create in this lesson supports adding mining models based on the [Microsoft Association Algorithm](../../2014/analysis-services/data-mining/microsoft-association-algorithm.md). In later lessons, you will use the mining models to predict the type of products a customer tends to purchase at the same time, which is called a market basket analysis. For example, you may find that customers tend to buy mountain bikes, bike tires, and helmets at the same time.  
  
 In this lesson, the mining structure is defined by using nested tables. Nested tables are used because the data domain that will be defined by the structure is contained within two different source tables. For more information on nested tables, see [Nested Tables &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/nested-tables-analysis-services-data-mining.md).  
  
## CREATE MINING STRUCTURE Statement  
 In order to create a mining structure containing a nested table, you use the [CREATE MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/create-mining-structure-dmx) statement. The code in the statement can be broken into the following parts:  
  
-   Naming the structure  
  
-   Defining the key column  
  
-   Defining the mining columns  
  
-   Defining the nested table columns  
  
 The following is a generic example of the CREATE MINING STRUCTURE statement:  
  
```  
CREATE MINING STRUCTURE [<Mining Structure Name>]  
(  
   <key column>,  
   <mining structure columns>,  
   <table columns>  
   (  <nested key column>,  
      <nested mining structure columns> )  
)  
  
```  
  
 The first line of the code defines the name of the structure:  
  
```  
CREATE MINING STRUCTURE [Mining Structure Name]  
```  
  
 For information about naming an object in DMX, see [Identifiers &#40;DMX&#41;](/sql/dmx/identifiers-dmx).  
  
 The next line of the code defines the key column for the mining structure, which uniquely identifies an entity in the source data:  
  
```  
<key column>  
```  
  
 The next line of the code is used to define the mining columns that will be used by the mining models associated with the mining structure:  
  
```  
<mining structure columns>  
```  
  
 The next lines of the code define the nested table columns:  
  
```  
<table columns>  
(  <nested key column>,  
   <nested mining structure columns> )  
```  
  
 For information about the types of mining structure columns that you can define, see [Mining Structure Columns](../../2014/analysis-services/data-mining/mining-structure-columns.md).  
  
> [!NOTE]  
>  By default, [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] creates a 30 percent holdout data set for each mining structure; however, when you use DMX to create a mining structure, you must manually add the holdout data set, if desired.  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Create a new blank query  
  
-   Alter the query to create the mining structure  
  
-   Execute the query  
  
## Creating the Query  
 The first step is to connect to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and create a new DMX query in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
#### To create a new DMX query in SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
2.  In the **Connect to Server** dialog box, for **Server type**, select **Analysis Services**. In **Server name**, type `LocalHost`, or the name of the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you want to connect to for this lesson. Click **Connect**.  
  
3.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
## Altering the Query  
 The next step is to modify the CREATE MINING STRUCTURE statement described above to create the Market Basket mining structure.  
  
#### To customize the CREATE MINING STRUCTURE statement  
  
1.  In Query Editor, copy the generic example of the CREATE MINING STRUCTURE statement into the blank query.  
  
2.  Replace the following:  
  
    ```  
    [mining structure name]   
    ```  
  
     with:  
  
    ```  
    [Market Basket]  
    ```  
  
3.  Replace the following:  
  
    ```  
    <key column>  
    ```  
  
     with:  
  
    ```  
    OrderNumber TEXT KEY  
    ```  
  
4.  Replace the following:  
  
    ```  
    <table columns>  
    (  <nested key column>,  
       <nested mining structure columns> )  
    ```  
  
     with:  
  
    ```  
    [Products] TABLE (  
        [Model] TEXT KEY  
    )  
    ```  
  
     The TEXT KEY language specifies that the Model column is the key column for the nested table.  
  
     The complete mining structure statement should now be as follows:  
  
    ```  
    CREATE MINING STRUCTURE [Market Basket] (  
        OrderNumber TEXT KEY,  
        [Products] TABLE (  
            [Model] TEXT KEY  
        )  
    )  
    ```  
  
5.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
6.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Market Basket Structure.dmx`.  
  
## Executing the Query  
 The final step is to execute the query. After a query is created and saved, it needs to be executed (that is, the statement needs to be run) in order to create the mining structure on the server. For more information about executing queries in Query Editor, see [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../relational-databases/scripting/database-engine-query-editor-sql-server-management-studio.md).  
  
#### To execute the query  
  
-   In Query Editor, on the toolbar, click **Execute**.  
  
     The status of the query is displayed in the **Messages** tab at the bottom of Query Editor after the statement finishes executing. Messages should display:  
  
    ```  
    Executing the query   
    Execution complete  
    ```  
  
     A new structure named **Market Basket** now exists on the server.  
  
 In the next lesson, you will add mining models to the Market Basket mining structure you just created.  
  
## Next Lesson  
 [Lesson 2: Adding Mining Models to the Market Basket Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-market-basket-mining-structure.md)  
  
  
