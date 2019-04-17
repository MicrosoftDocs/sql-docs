---
title: "Step 5: Add and configure the Flat File source | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 5c95ce51-e0fe-4fc5-95eb-2945929f2b13
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1-5: Add and configure the Flat File source
In this task, you add and configure a Flat File source to your package. A Flat File source is a data flow component that uses metadata defined by a Flat File connection manager. This metadata specifies the format and structure of the data to be extracted from the flat file by a transform process. The Flat File source extracts data from a single flat file, using the format definitions in the Flat File connection manager.  
  
For this task, you configure the Flat File source to use the **Sample Flat File Source Data** connection manager that you previously created.  
  
## Add a Flat File source component  
  
1.  To open the **Data Flow** designer, either double-click on the **Extract Sample Currency Data** data flow task, or select the **Data Flow** tab.  
  
2.  In the **SSIS Toolbox**, expand **OtherSources**, and then drag a **Flat File Source** onto the design surface of the **Data Flow** tab.  
  
3.  On the **Data Flow** design surface, right-click the newly added **Flat File Source**, select **Rename**, and change the name to **Extract Sample Currency Data**.  
  
4.  Double-click the Flat File source to open the **Flat File Source Editor** dialog.  
  
5.  In the **Flat file connection manager** field, select **Sample Flat File Source Data**.  
  
6.  Select **Columns** and verify that the names of the columns are correct.  
  
7.  Select **OK**.  
  
8.  Right-click the Flat File source and select **Properties**.  
  
9. In the **Properties** window, verify that the **LocaleID** property is set to **English (United States)**.  
  
## Go to next task
[Step 6: Add and configure the Lookup transformations](../integration-services/lesson-1-6-adding-and-configuring-the-lookup-transformations.md)  
  
## See also  
[Flat File source](../integration-services/data-flow/flat-file-source.md)  
[Flat File Connection Manager](../integration-services/connection-manager/flat-file-connection-manager.md)  
  
  
  
