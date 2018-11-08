---
title: "Specify Column Mapping Dialog Box (Mining Accuracy Chart) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.accuracychart.coltotablecolmapping.f1"
ms.assetid: 68e9e2d2-173f-4363-a515-fc60bfee3af0
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify Column Mapping Dialog Box (Mining Accuracy Chart)
  Use the **Specify Column Mapping** tab to select tables from an external data source and map the columns to a data mining model. You can then use the external data to test the accuracy of a mining model and displays the results in the accuracy chart.  
  
 **For more information:** [Testing and Validation &#40;Data Mining&#41;](data-mining/testing-and-validation-data-mining.md)  
  
## Options  
 **Mining Structure**  
 Displays the selected mining structure that contains the model that you will test.  
  
 **Select Structure**  
 Click to open the **Select Mining Structure** dialog box and select a different mining structure.  
  
 **Select Input Table(s)**  
 Displays the selected input tables that are used to generate the lift chart. Select the table that contains the test data that you will use to test the accuracy of the models.  
  
> [!NOTE]  
>  If the pane does not contain any tables, click **Select Case Table** to add tables from a data source view.  
  
 **Remove Table**  
 Removes the selected table. This button is disabled if a table has not been selected or if no tables are displayed in the **Select Input Tables** list.  
  
 **Select Case Table**  
 Click to open the **Select Table** dialog box and select a data source view.  
  
 **Note** This button appears only if a case table has not been selected. To enable the button so that you can select a different case table, clear the list by selecting all existing tables and then clicking **Remove Table**.  
  
 **Select Nested Table**  
 Opens the **Select Table** dialog box. This button appears only if a case table has been selected. If the associated mining structure does not contain a nested table, this button is disabled.  
  
 **Modify Join**  
 Opens the **Specify Nested Join** dialog box. This button is active only if the nested table is selected.  
  
## See Also  
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)   
 [Mining Accuracy Chart Designer &#40;Data Mining&#41;](mining-accuracy-chart-designer-data-mining.md)  
  
  
