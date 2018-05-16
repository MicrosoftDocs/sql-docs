---
title: "Choose an Accuracy Chart Type and Set Chart Options | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Choose an Accuracy Chart Type and Set Chart Options
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides multiple methods for determining the validity of your mining models. The type of accuracy chart that you can create for each model or structure depends on these factors:  
  
-   The type of algorithm that was used to create the model  
  
-   The data type of the predictable attribute  
  
-   The number of predictable attributes to measure  
  
 This topic provides an overview of the different accuracy charts.  
  
 **Note** Charts and their definitions are not saved. If you close the window that contains a chart, you must create the chart again.  
  
## Accuracy Chart Types  
 Depending on the chart type that you choose, you have the ability to further configure options, to browse the chart, or copy the chart to the Clipboard and work with the data in Excel.  
  
#### Lift chart  
 After you have configured the options for the models and the testing data, click the **Lift Chart** tab to view the results. You can also copy the chart to the Clipboard, or view details of individual trend lines or data points in the Mining Legend.  
  
#### Profit Chart  
 After you have configured the options for the models and the testing data, click the **Lift Chart** tab, select **Profit Chart** from the **Chart Type** list to set profit chart options, and then click **OK** to view the results.  
  
 You can use the **Profit Chart Settings** dialog box as many times as you want to try different cost options and redisplay the chart. The Mining Legend contains detailed information about the estimated profit for each model. You can also copy the chart and the contents of the Mining Legend to the Clipboard to work with it in Excel.  
  
#### Scatter Plot  
 If you have selected the appropriate type of model, when you click the **Lift Chart** tab, the chart type is automatically set to **Scatter Plot** and a scatter plot is displayed. No further configuration is possible. You can also copy the chart to the Clipboard and paste the chart as a graphic into Excel or another application.  
  
#### Classification Matrix  
 For a classification matrix, use the **Input Selection** tab to choose the models and the testing data, and then click the **Classification Matrix** tab to view the results.  
  
 The contents of a classification matrix are the same for all model types, and cannot be configured. You can copy the data in the chart to the Clipboard and then work with it in Excel.  
  
#### Cross-Validation Report  
 For a cross-validation report, after you have selected a mining structure or mining model in Solution Explorer, click the **Cross Validation** tab, configure all relevant options, and then click **Get Results** to generate the report. No further configuration is possible.  
  
 The format of the cross-validation report is the same for all model types, and cannot be configured. However, the content of the report differs depending on the type of model that you are analyzing, and the data type of the predictable attribute. You can also copy the results of the report to the Clipboard and work with the data in Excel.  
  
  
