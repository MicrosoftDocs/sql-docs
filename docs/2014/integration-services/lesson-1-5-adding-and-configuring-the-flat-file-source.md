---
title: "Step 5: Adding and Configuring the Flat File Source | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 5c95ce51-e0fe-4fc5-95eb-2945929f2b13
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 5: Adding and Configuring the Flat File Source
  In this task, you will add and configure a Flat File source to your package. A Flat File source is a data flow component that uses metadata defined by a Flat File connection manager to specify the format and structure of the data to be extracted from the flat file by a transform process. The Flat File source can be configured to extract data from a single flat file by using the file format definition provided by the Flat File connection manager.  
  
 For this tutorial, you will configure the Flat File source to use the `Sample Flat File Source Data` connection manager that you previously created.  
  
### To add a Flat File Source component  
  
1.  Open the **Data Flow** designer, either by double-clicking the `Extract Sample Currency Data` data flow task or by clicking the **Data Flow tab**.  
  
2.  In the **SSIS Toolbox**, expand **OtherSources**, and then drag a **Flat File Source** onto the design surface of the **Data Flow** tab.  
  
3.  On the **Data Flow** design surface, right-click the newly added **Flat File Source**, click **Rename**, and change the name to `Extract Sample Currency Data`.  
  
4.  Double-click the Flat File source to open the Flat File Source Editor dialog box.  
  
5.  In the **Flat file connection manager** box, select `Sample Flat File Source Data`.  
  
6.  Click **Columns** and verify that the names of the columns are correct.  
  
7.  Click **OK**.  
  
8.  Right-click the Flat File source and click **Properties**.  
  
9. In the Properties window, verify that the `LocaleID` property is set to **English (United States)**.  
  
## Next Task in Lesson  
 [Step 6: Adding and Configuring the Lookup Transformations](lesson-1-6-adding-and-configuring-the-lookup-transformations.md)  
  
## See Also  
 [Flat File Source](data-flow/flat-file-source.md)   
 [Flat File Connection Manager Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)  
  
  
