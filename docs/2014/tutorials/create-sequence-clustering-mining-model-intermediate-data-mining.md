---
title: "Creating a Sequence Clustering Mining Model Structure (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: e9339227-6c2e-4c4b-8be2-8c1960bc4a8d
author: minewiskan
ms.author: owend
manager: craigg
---
# Creating a Sequence Clustering Mining Model Structure (Intermediate Data Mining Tutorial)
  The first step in creating a sequence clustering mining model is to use the Data Mining Wizard to create a new mining structure and a mining model based on the [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Clustering algorithm.  
  
 You will use the same data source view that you used for the market basket analysis, but you will add a column that contains the `sequence` identifier. In this scenario, the sequence means the order in which the customer added items to the shopping basket.  
  
 You will also add some columns that are used in one of the models to group customers by demographics.  
  
### To create a sequence clustering structure and model  
  
1.  In Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], right-click **Mining Structures** and select **New Mining Structure**.  
  
2.  On the **Welcome to the Data Mining Wizard** page, click **Next**.  
  
3.  On the **Select the Definition Method** page, verify that **From existing relational database or data warehouse** is selected, and then click **Next**.  
  
4.  On the **Create the Data Mining Structure** page, verify that the option **Create mining structure with a mining model** is selected. Next, click the dropdown list for the option, **Which data mining technique do you want to use?**, and select **Microsoft Sequence Clustering**. Click **Next**.  
  
     The **Select Data Source View** page appears. Under **Available data source views**, select `Orders`.  
  
     Orders is the same data source view that you used for the market basket scenario. If you have not created this data source view, see [Adding a Data Source View with Nested Tables &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/adding-a-data-source-view-with-nested-tables-intermediate-data-mining-tutorial.md).  
  
5.  Click **Next**.  
  
6.  On the **Specify Table Types** page, select the **Case** check box next to the **vAssocSeqOrders** table, and select the **Nested** check box next to the **vAssocSeqLineItems** table. Click **Next**.  
  
    > [!NOTE]  
    >  If an error occurs when you select the **Case** or **Nested** check boxes, it may be that the join in the data source view is not correct. The nested table, **vAssocSeqLineItems**, must be connected to the case table, **vAssocSeqOrders,** by a many-to-one join. You can edit the relationship by right-clicking on the join line and then reversing the direction of the join. For more information, see [Create or Edit Relationship Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](../../2014/analysis-services/create-or-edit-relationship-dialog-box-analysis-services-multidimensional-data.md).  
  
7.  On the **Specify the Training Data** page, choose the columns for use in the model by selecting a check box as follows:  
  
    -   **IncomeGroup** Select the **Input** check box.  
  
         This column contains interesting information about the customers that you can use for clustering. You will use it in the first model and then ignore it in the second model.  
  
    -   **OrderNumber** Select the `Key` check box.  
  
         This field will be used as the identifier for the case table, or `Key`. In general, you should never use the key field of the case table as an input, because the key contains unique values that are not useful for clustering.  
  
    -   **Region** Select the **Input** check box.  
  
         This column contains interesting information about the customers that you can use for clustering. You will use it in the first model and then ignore it in the second model.  
  
    -   **LineNumber** Select the `Key` and **Input** check boxes.  
  
         The **LineNumber** field will be used as the identifier for the nested table, or `Sequence Key`. The key for a nested table must always be used for input.  
  
    -   **Model** Select the **Input** and **Predictable** check boxes.  
  
     Verify that the selections are correct, and then click **Next**.  
  
8.  On the **Specify Columns' Content and Data Type** page, verify that the grid contains the columns, content types, and data types shown in the following table, and then click **Next**.  
  
    |Tables/Columns|Content Type|Data Type|  
    |---------------------|------------------|---------------|  
    |IncomeGroup|Discrete|Text|  
    |OrderNumber|Key|Text|  
    |Region|Discrete|Text|  
    |vAssocSeqLineItems|||  
    |Line Number|Key Sequence|Long|  
    |Model|Discrete|Text|  
  
9. On the **Create Testing Set** page, change the **Percentage of data for testing** to 20, and then click **Next**.  
  
10. On the **Completing the Wizard** page, for the **Mining structure name**, type `Sequence Clustering with Region`.  
  
11. For the **Mining model name**, type `Sequence Clustering with Region`.  
  
12. Check the **Allow drill through** box, and then click **Finish**.  
  
## Next Task in Lesson  
 [Processing the Sequence Clustering Model](../../2014/tutorials/processing-the-sequence-clustering-model.md)  
  
## See Also  
 [Data Mining Designer](../../2014/analysis-services/data-mining/data-mining-designer.md)   
 [Microsoft Sequence Clustering Algorithm](../../2014/analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)  
  
  
