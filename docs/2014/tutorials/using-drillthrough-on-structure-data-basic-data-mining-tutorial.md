---
title: "Using Drillthrough on Structure Data (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: a693979c-0564-4d6d-b35d-cbbc8f350469
author: minewiskan
ms.author: owend
manager: kfile
---
# Using Drillthrough on Structure Data (Basic Data Mining Tutorial)
  As part of their advertising campaign, [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is sending a mailer to potential customers in the 34-40 age demographic. The marketing department has decided that they would also like to send the mailer to the customers who purchased bikes from [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] more than five years ago. In this lesson you will identify customers with older bikes and retrieve their contact information. This information is not included in the model, but is included in the structure. To retrieve the contact information you will first ensure that drillthrough is enabled for the structure and then you will use drillthrough to reveal the names and addresses of the targeted customers.  
  
### To enable drillthrough on a mining model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], on the **Mining Models** tab of Data Mining Designer, right-click the **TM_Decision_Tree** model, and select **Properties**.  
  
2.  In the Properties windows, click **AllowDrillthrough**, and select **True**.  
  
3.  In the Mining Models tab, right-click the model, and select **Process Model**.  
  
 For more information, see [Drillthrough Queries &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/drillthrough-queries-data-mining.md)  
  
### To view drillthrough data from a mining model  
  
1.  In Data Mining Designer, click the **Mining Model Viewer** tab.  
  
2.  Select the **TM_Decision_Tree** model from the **Mining Model** list.  
  
3.  Change the **Background** value to `1`. By doing this, you show only the part of the model that is related to customer who bought bikes.  
  
4.  Select the Microsoft Tree viewer from the **Viewer** list. This will force the viewer to refresh with the new filter conditions. Then, locate the **Age >=34 and <41** node and right-click the node.  
  
5.  Select **Drill Through**, and then select **Model and Structure Columns** to open the **Drill Through** window.  
  
6.  Scroll to the **Structure.Date First Purchase** column to view the purchase dates for the older bikes.  
  
7.  To copy the data to the Clipboard, right-click any row in the table, and select **Copy All**.  
  
 Congratulations, you have completed the basic data mining tutorial. Now that you are comfortable using the data mining tools, we recommend that you also complete the intermediate data mining tutorial, which demonstrates how to create models for forecasting, market basket analysis, and sequence clustering.  
  
## Previous Task in Lesson  
 [Creating Predictions &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/creating-predictions-basic-data-mining-tutorial.md)  
  
## See Also  
 [Create a Prediction Query Using the Prediction Query Builder](../../2014/analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md)  
  
  
