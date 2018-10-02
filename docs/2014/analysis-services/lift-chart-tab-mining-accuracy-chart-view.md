---
title: "Lift Chart Tab (Mining Accuracy Chart View) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.accuracychart.liftchart.f1"
ms.assetid: f1674e2e-d38e-40c7-b8d1-5585ce9a0168
author: minewiskan
ms.author: owend
manager: craigg
---
# Lift Chart Tab (Mining Accuracy Chart View)
  Use the **Lift Chart** pane to view a chart that compares all the selected mining models in the selected mining structure.  
  
 If the model predicts a discrete attribute, the pane can display either a lift chart or a profit chart. For information about lift charts, see [Lift Chart &#40;Analysis Services - Data Mining&#41;](data-mining/lift-chart-analysis-services-data-mining.md).  
  
 To create a profit chart, you must provide additional information about the cost of implementing the recommendations of the mining model, as well as the expected return. For more information, see [Profit Chart &#40;Analysis Services - Data Mining&#41;](data-mining/profit-chart-analysis-services-data-mining.md).  
  
 If the model predicts a continuous attribute, the pane will display a scatter plot graph.  
  
 For general information about methods of measuring the accuracy of a mining model, see [Lift Chart &#40;Analysis Services - Data Mining&#41;](data-mining/lift-chart-analysis-services-data-mining.md).  
  
## Options  
 **Chart Type**  
 Sets the type of chart to display in the viewer. You can select either **Lift Chart** or **Profit Chart**.  
  
 **Settings**  
 Opens the **Profit Chart Settings** dialog box, which you can use to configure a profit chart.  
  
 The profit chart is not available if a continuous predictable column was selected in the **Column Mapping** tab.  
  
 **Copy**  
 Copies the chart to the Clipboard.  
  
## See Also  
 [Mining Accuracy Chart Designer &#40;Data Mining&#41;](mining-accuracy-chart-designer-data-mining.md)   
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)   
 [Testing and Validation &#40;Data Mining&#41;](data-mining/testing-and-validation-data-mining.md)  
  
  
