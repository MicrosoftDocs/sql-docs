---
title: "Data Mining Query | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataminingquery.f1"
ms.assetid: 948e358a-6245-429f-82c7-4cedc5e048fd
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Mining Query
  The design pane contains the data mining prediction query builder, which you can use to build data mining prediction queries. You can design either prediction queries based on input tables, or singleton prediction queries. Switch to the result view to run the query and view the results. The query view displays the Data Mining Extensions (DMX) query created by the prediction query builder.  
  
## Options  
 Switch view button  
 Click an icon to switch between the design and query pane. By default, the design pane is open.  
  
 To switch to the design pane, click the ![Design icon](../../integration-services/control-flow/media/ssis-designicon.gif "Design icon") icon.  
  
 To switch to the query pane, click the ![SQL icon](../../integration-services/control-flow/media/ssis-queryicon.gif "SQL icon") icon.  
  
 **Mining Model**  
 Displays the selected mining model on which you want to base predictions.  
  
 **Select Model**  
 Opens the **Select Mining Model** dialog box.  
  
 **Input Columns**  
 Displays the selected input columns used to generate the predictions.  
  
 **Source**  
 Select the source containing the field that you will use for the column from the dropdown list. You can either use the mining model selected in the **Mining Model** table, the input table(s) selected in the **Select Input Table(s)** table, a prediction function, or a custom expression.  
  
 Columns can be dragged from the tables containing the mining model and input columns to the cell.  
  
 **Field**  
 Select a column from the list of columns derived from the source table. If you selected **Prediction Function** in **Source**, this cell contains a drop-down list of the prediction functions available for the selected mining model.  
  
 **Alias**  
 The name of the column returned by the server.  
  
 **Show**  
 Select to return the column or to only use the column in the WHERE clause.  
  
 **Group**  
 Use with the **And/Or** column to group expressions together. For example, (expr1 OR expr2) AND expr3.  
  
 **And/Or**  
 Use to create a logical query. For example, (expr1 OR expr2) AND expr3.  
  
 **Criteria/Argument**  
 Specify a condition or user expression that applies to the column. Columns can be dragged from the tables containing the mining model and input columns to the cell.  
  
## See Also  
 [Data Mining Query Tools](../../analysis-services/data-mining/data-mining-query-tools.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../../dmx/data-mining-extensions-dmx-statements.md)  
  
  
