---
title: "Creating a Forecasting Structure and Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 5f55cbf6-0db4-4cb4-a0f5-e27441873d4f
author: minewiskan
ms.author: owend
manager: kfile
---
# Creating a Forecasting Structure and Model (Intermediate Data Mining Tutorial)
  Next, you will use the Data Mining Wizard to create a new mining structure and mining model based on the data source view that you just created. In this task you will specify that the mining model should use the [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm.  
  
### To create a forecasting mining structure  
  
1.  In Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], right-click **Mining Structures** and select **New Mining Structure**.  
  
2.  On the **Welcome to the Data Mining Wizard** page, click **Next**.  
  
3.  On the **Select the Definition Method** page, verify that **From existing relational database or data warehouse** is selected, and then click **Next**.  
  
4.  On the **Create the Data Mining Structure** page, under **Which data mining technique do you want to use?**, select **Microsoft Time Series**, and then click **Next**.  
  
5.  On the **Select Data Source View** page, under **Available data source views**, select **SalesByRegion**.  
  
6.  Click **Next**.  
  
7.  On the **Specify Table Types** page, ensure that the check box in the **Case** column for the vTimeSeries table is selected, and then click **Next**.  
  
8.  On the **Specify the Training Data** page, select the check boxes in the **Key** column for the ModelRegion and ReportingDate columns.  
  
     ReportingDate should be selected by default, because you specified this column as the logical primary key when you created the data source view. By adding ModelRegion as a second key, you are telling the algorithm to create a separate time series for each combination of model and region listed in this field.  
  
9. Select the check boxes in the **Input** and **Predictable** columns for the Quantity, column, and then click **Next**.  
  
     By selecting **Predictable**, you indicate that you want to create forecasts on the data in this column. However, because you want to base the forecasts on past data, you must also add the column as an input.  
  
10. On the page **Specify Columns' Content and Data Type**, review the selections.  
  
     The ModelRegion column is designated as a **Key** column and the ReportingDate column is automatically designated as a **Key Time** column. You can have only one of each type of key.  
  
11. Click **Next**.  
  
12. On the **Completing the Wizard** page, for **Mining structure name**, type `Forecasting`.  
  
    > [!NOTE]  
    >  The option to enable drillthrough is not available for time series models.  
  
13. In **Mining model name**, type `Forecasting`, and then click **Finish**.  
  
     Data Mining Designer opens to display the `Forecasting` mining structure that you just created.  
  
## Next Task in Lesson  
 [Modifying the Forecasting Structure &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/modifying-the-forecasting-structure-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Data Mining Designer](../../2014/analysis-services/data-mining/data-mining-designer.md)   
 [Microsoft Time Series Algorithm](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm.md)  
  
  
