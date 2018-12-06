---
title: "View or Change Modeling Flags (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d1169735-fb18-417b-b8d6-9a161e444020
author: minewiskan
ms.author: owend
manager: craigg
---
# View or Change Modeling Flags (Data Mining)
  Modeling flags are properties that you set on a mining structure column or mining model columns to control how the algorithm processes the data during analysis.  
  
 In Data Mining Designer, you can view and modify the modeling flags associated with a mining structure or mining column by viewing the properties of the structure or model. You can also set modeling flags by using DMX, AMO, or XMLA.  
  
 This procedure describes how to change the modeling flags in the designer.  
  
### View or change the modeling flag for a structure column or model column  
  
1.  In SQL Server Design Studio, open Solution Explorer, and then double-click the mining structure.  
  
2.  To set the NOT NULL modeling flag, click the **Mining Structure** tab. To set the REGRESSOR or MODEL_EXISTENCE_ONLY flags, click the **Mining Model** tab.  
  
3.  Right-click the column you want to view or change, and select **Properties**.  
  
4.  To add a new modeling flag, click the text box next to the **ModelingFlags** property, and select the check box or check boxes for the modeling flags you want to use.  
  
     Modeling flags are displayed only if they are appropriate for the column data type.  
  
    > [!NOTE]  
    >  After you change a modeling flag, you must reprocess the model.  
  
### Get the modeling flags used in the model  
  
-   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open a DMX Query window, and type a query like the following:  
  
    ```  
    SELECT COLUMN_NAME, CONTENT_TYPE, MODELING_FLAG  
    FROM $system.DMSCHEMA_MINING_COLUMNS  
    WHERE MODEL_NAME = 'Forecasting'  
  
    ```  
  
## See Also  
 [Mining Model Tasks and How-tos](mining-model-tasks-and-how-tos.md)   
 [Modeling Flags &#40;Data Mining&#41;](modeling-flags-data-mining.md)  
  
  
