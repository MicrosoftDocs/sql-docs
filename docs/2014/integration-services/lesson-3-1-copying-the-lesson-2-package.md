---
title: "Step 1: Copying the Lesson 2 Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 4bd91402-4e37-41de-ab78-8ca5a1948a37
author: janinezhang
ms.author: janinez
manager: craigg
---
# Step 1: Copying the Lesson 2 Package
  In this task, you will create a copy of the Lesson 2.dtsx package that you created in Lesson 2. Alternatively, you can add the completed lesson 2 package that is included with the tutorial to the project, and then copy it instead. You will use this new copy throughout the rest of Lesson 3.  
  
### To create the Lesson 3 package  
  
1.  If [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools is not already open, click **Start**, point to **All Programs**, point to **Microsoft SQL Server 2012**, and then click **SQL Server Data Tools**.  
  
2.  On the **File** menu, click **Open**, click **Project/Solution**, select **SSIS Tutorial** and click **Open**, and then double-click **SSIS Tutorial.sln**.  
  
3.  In Solution Explorer, right-click **Lesson 2.dtsx**, and then click **Copy**.  
  
4.  In Solution Explorer, right-click **SSIS Packages**, and then click **Paste**.  
  
     By default, the copied package is named Lesson 3.dtsx.  
  
5.  In Solution Explorer, double-click **Lesson 3.dtsx** to open the package.  
  
6.  Right-click anywhere in the background of the **Control Flow** tab and click **Properties**.  
  
7.  In the Properties window, update the `Name` property to `Lesson 3`.  
  
8.  Click the box for the **ID** property, and then in the list, click **\<Generate New ID>**.  
  
### To add the completed Lesson2 package  
  
1.  Open [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and open the SSIS Tutorial project.  
  
2.  In Solution Explorer, right-click **SSIS Packages**, and click **Add Existing Package**.  
  
3.  In the **Add Copy of Existing Package** dialog box, in **Package location**, select **File system**.  
  
4.  Click the browse **(...)** button, navigate to **Lesson 2.dtsx** on your machine, and then click **Open**.  
  
     To download all of the lesson packages for this tutorial, do the following.  
  
    1.  Navigate to [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=275027)  
  
    2.  Click the **DOWNLOADS** tab.  
  
    3.  Click the SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.  
  
5.  Copy and paste the Lesson 3 package as described in steps 3-8 in the previous procedure.  
  
## Next Task in Lesson  
 [Step 2: Adding and Configuring Logging](lesson-3-2-adding-and-configuring-logging.md)  
  
  
