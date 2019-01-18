---
title: "Step 1: Copying the Lesson 1 Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 7f1616c2-2b4e-4010-be50-27d7b897403a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 1: Copying the Lesson 1 Package
  In this task, you will create a copy of the Lesson 1.dtsx package that you created in Lesson 1. If you did not complete Lesson 1, you can add the completed lesson 1 package that is included with the tutorial to the project, and then copy it instead. You will use this new copy throughout the rest of Lesson 2.  
  
### To create the Lesson 2 package  
  
1.  If [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools is not already open, click **Start**, point to **All Programs**, point to **Microsoft SQL Server 2012**, and then click **SQL Server Data Tools**.  
  
2.  On the **File** menu, click **Open**, click **Project/Solution**, click the **SSIS Tutorial** folder and click **Open**, and then double-click **SSIS Tutorial.sln**.  
  
3.  In Solution Explorer, right-click **Lesson 1.dtsx**, and then click **Copy**.  
  
4.  In Solution Explorer, right-click **SSIS Packages**, and then click **Paste**.  
  
     By default, the copied package will be named Lesson 2.dtsx.  
  
5.  In Solution Explorer, double-click **Lesson 2.dtsx** to open the package  
  
6.  Right-click anywhere in the background of the **Control Flow** design surface and click **Properties**.  
  
7.  In the Properties window, update the `Name` property to `Lesson 2`.  
  
8.  Click the box for the **ID** property, click the dropdown arrow and then click **\<Generate New ID>**.  
  
### To add the completed Lesson 1 package  
  
1.  Open [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools and open the SSIS Tutorial project.  
  
2.  In Solution Explorer, right-click **SSIS Packages**, and click **Add Existing Package**.  
  
3.  In the **Add Copy of Existing Package** dialog box, in **Package location**, select **File system**.  
  
4.  Click the browse **(...)** button, navigate to **Lesson 1.dtsx** on your machine, and then click **Open**.  
  
     To download all of the lesson packages for this tutorial, do the following.  
  
    1.  Navigate to [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=275027)  
  
    2.  Click the **DOWNLOADS** tab.  
  
    3.  Click the SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.  
  
5.  Copy and paste the Lesson 1 package as described in steps 3-8 in the previous procedure.  
  
## Next Task in Lesson  
 [Step 2: Adding and Configuring the Foreach Loop Container](lesson-2-2-adding-and-configuring-the-foreach-loop-container.md)  
  
  
