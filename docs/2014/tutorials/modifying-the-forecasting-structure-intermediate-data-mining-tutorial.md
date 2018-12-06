---
title: "Modifying the Forecasting Structure (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 1a6c138e-643b-4ae6-ad08-93631f149c20
author: minewiskan
ms.author: owend
manager: craigg
---
# Modifying the Forecasting Structure (Intermediate Data Mining Tutorial)
  The mining structure that you created in the previous task contains a single forecasting model. Before you process and explore the model, you must change its structure slightly and modify one of its properties.  
  
## Modifying the Mining Structure  
 You can change the mining structure by using the **Mining Structure** tab of Data Mining Designer. When you created the model with the Data Mining Wizard, you used three columns: ReportingDate, ModelRegion, and Quantity. However, the **Forecasting** table also contains an Amount column, which you can use to forecast the amount of sales. By using the **Mining Structure** tab, you can add this column from the data source view to the mining structure.  
  
#### To add the Amount column to the Forecasting mining structure  
  
1.  On the **Mining Structure** tab of Data Mining Designer, in the **Data Source View** pane, select the Amount column in the vTimeSeries table.  
  
2.  Drag the Amount column from the **Data Source View** pane into the list of columns for the **Forecasting** structure.  
  
     The Amount column is now included in the **Forecasting** mining structure.  
  
## Modifying the Columns in the Mining Model  
 Because you added a new column to the structure, you must define how the model will use the column. You can specify how the column will be used on the **Mining Models** tab of Data Mining Designer.  
  
 The **Mining Models** tab lists the columns that the mining structure contains in the **Structure** column of the grid, and lists the columns that the mining model contains in the column that has the name of the model, in this case **Forecasting**. Click the names of the columns to make modifications. In the **Forecasting** mining model, the Amount column is used as an input column and is also used to forecast future sales. Therefore, you must set the properties of the column so that it can be used as both an input column and a predictable column.  
  
> [!NOTE]  
>  In the **Mining Models** tab, you can also create new models based on the same structure, and you can adjust the algorithm and column properties for each model. However, you must process the model before these changes take effect.  
  
#### To define how the Amount column will be used  
  
1.  In the **Forecasting** column of the grid on the **Mining Models** tab, click the cell in the Amount row.  
  
2.  Select **Predict** from the list.  
  
     The Amount column is now both an input column and a predictable column.  
  
 You can also change the properties of individual columns by selecting the column and opening the **Properties** window. To open the **Properties** window, right-click the column name, and then select **Properties**. If you change a property within the column for an individual model, you can change the properties only for that model. However, when you change a property within the **Structure** column, the change affects every model that is associated with the structure. Whenever you make changes to the model or structure, you must reprocess to see the effects.  
  
## Next Task in Lesson  
 [Customizing and Processing the Forecasting Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/customize-process-forecasting-model-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)   
 [Mining Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-models-analysis-services-data-mining.md)  
  
  
