---
title: "Lesson 3: Processing the Bike Buyer Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: e748c2cd-339d-4e82-82f1-be2d0fc41b61
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 3: Processing the Bike Buyer Mining Structure
  In this lesson, you will use the INSERT INTO statement and the vTargetMail view from the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] sample database to process the mining structures and mining models that you created in [Lesson 1: Creating the Bike Buyer Mining Structure](../../2014/tutorials/lesson-1-creating-the-bike-buyer-mining-structure.md) and [Lesson 2: Adding Mining Models to the Bike Buyer Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-bike-buyer-mining-structure.md).  
  
 When you process a mining structure, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] reads the source data and builds the structures that support mining models. When you process a mining model, the data defined by the mining structure is passed through the data mining algorithm that you choose. The algorithm searches for trends and patterns, and then stores this information in the mining model. The mining model, therefore, does not contain the actual source data, but instead contains the information that was discovered by the algorithm. For more information about processing mining models, see [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md).  
  
 You need to reprocess a mining structure only if you change a structure column or change the source data. If you add a mining model to a mining structure that has already been processed, you can use the INSERT INTO MINING MODEL statement to train the new mining model.  
  
## Train Structure Template  
 In order to train the mining structure and its associated mining models, use the [INSERT INTO &#40;DMX&#41;](/sql/dmx/insert-into-dmx) statement. The code in the statement can be broken into the following parts:  
  
-   Identifying the mining structure  
  
-   Listing the columns in the mining structure  
  
-   Defining the training data  
  
 The following is a generic example of the INSERT INTO statement:  
  
```  
INSERT INTO MINING STRUCTURE [<mining structure name>]  
(  
   <mining structure columns>  
)  
OPENQUERY([<datasource>],'<SELECT statement>')  
```  
  
 The first line of the code identifies the mining structure that you will train:  
  
```  
INSERT INTO MINING STRUCTURE [<mining structure name>]  
```  
  
 The next line of the code specifies the columns that are defined by the mining structure. You must list each column in the mining structure, and each column must map to a column contained within the source query data.  
  
```  
(  
   <mining structure columns>  
)  
```  
  
 The final line of the code defines the data that will be used to train the mining structure:  
  
```  
OPENQUERY([<datasource>],'<SELECT statement>')  
```  
  
 In this lesson, you use `OPENQUERY` to define the source data. For information about other methods of defining the source query, see [&#60;source data query&#62;](/sql/dmx/source-data-query).  
  
## Lesson Tasks  
 You will perform the following task in this lesson:  
  
-   Process the Bike Buyer mining structure  
  
## Processing the Predictive Mining Structure  
  
#### To process the mining structure by using INSERT INTO  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the INSERT INTO statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    [<mining structure name>]   
    ```  
  
     with:  
  
    ```  
    Bike Buyer  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining structure columns>  
    ```  
  
     with:  
  
    ```  
    [Customer Key],  
    [Age],  
    [Bike Buyer],  
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
  
5.  Replace the following:  
  
    ```  
    OPENQUERY([<datasource>],'<SELECT statement>')  
    ```  
  
     with:  
  
    ```  
    OPENQUERY([Adventure Works DW],  
       'SELECT CustomerKey, Age, BikeBuyer,  
             CommuteDistance,EnglishEducation,  
             Gender,HouseOwnerFlag,MaritalStatus,  
             NumberCarsOwned,NumberChildrenAtHome,   
             EnglishOccupation,Region,TotalChildren,  
             YearlyIncome   
        FROM dbo.vTargetMail')  
    ```  
  
     The OPENQUERY statement references the [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] data source to access the view vTargetMail. The view contains the source data that will be used to train the mining models.  
  
     The complete statement should now be as follows:  
  
    ```  
    INSERT INTO MINING STRUCTURE [Bike Buyer]  
    (  
       [Customer Key],  
       [Age],  
       [Bike Buyer],  
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
    )  
    OPENQUERY([Adventure Works DW],  
       'SELECT CustomerKey, Age, BikeBuyer,  
             CommuteDistance,EnglishEducation,  
             Gender,HouseOwnerFlag,MaritalStatus,  
             NumberCarsOwned,NumberChildrenAtHome,   
             EnglishOccupation,Region,TotalChildren,  
             YearlyIncome   
        FROM dbo.vTargetMail')  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Process Bike Buyer Structure.dmx`.  
  
8.  On the toolbar, click the **Execute** button.  
  
 In the next lesson, you will explore content in the mining models you added to the mining structure in this lesson.  
  
## Next Lesson  
 [Lesson 4: Browsing the Bike Buyer Mining Models](../../2014/tutorials/lesson-4-browsing-the-bike-buyer-mining-models.md)  
  
  
