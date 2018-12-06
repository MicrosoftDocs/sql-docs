---
title: "Using Nested Table Data as an Input for an Accuracy Chart | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Mining Accuracy Chart [Analysis Services], nested tables"
  - "Mining Accuracy Chart [Analysis Services], input tables"
  - "nested tables"
  - "adding nested tables"
ms.assetid: 162e0686-ada3-4dd3-9151-9589926e6613
author: minewiskan
ms.author: owend
manager: craigg
---
# Using Nested Table Data as an Input for an Accuracy Chart
  When you test the accuracy of a mining model by using external data, if the mining model contains nested tables, the external data must also contain a case table and an associated nested table.  
  
 This topic describes how to work with nested tables used for model testing, how to map nested and case tables in the mode and in the external data, and how to apply a filter to a nested table.  
  
 When working with nested tables, keep in mind these tips:  
  
-   If you select the option **Use mining model test cases** or **Use mining structure test cases**, you do not need to specify a case table or nested table. With those options, the definition of the test data is stored in the mining structure and the test data is automatically selected when you create the accuracy chart.  
  
-   If a relationship already exists between the case and nested table in the data source, the columns in the mining structure are automatically mapped to the columns that have the same name in the input table.  
  
-   You cannot select a nested table until you have specified the case table. Also, you cannot specify a nested table as an input unless the mining model also uses a case table and nested table structure.  
  
### Use a nested table as input to an accuracy chart  
  
1.  Double-click the mining structure to open it in Data Mining Designer.  
  
2.  Select the **Mining Accuracy Chart** tab, and then select the **Input Selection** tab.  
  
3.  In **Select data set to be used for accuracy chart**, select the option **Specify a different data set**.  
  
4.  Click the browse button **(...)** to choose the external data set from a list of data source views on the current server.  
  
5.  Click **Select Case Table**. In the **Select Table** dialog box, choose the table from the data source view that contains the case data, and then click **OK**.  
  
6.  Click **Select Nested Table**. In the **Select Table** dialog box, select the table that contains the nested data, and then click **OK**.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     If you need to modify the relationship between the nested table and the case table, click **Modify Join** to open the **Create Relationship** dialog box.  
  
## See Also  
 [Choose and Map Model Testing Data](choose-and-map-model-testing-data.md)   
 [Apply Filters to Model Testing Data](apply-filters-to-model-testing-data.md)  
  
  
