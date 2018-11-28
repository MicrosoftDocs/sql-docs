---
title: "Step 1: Copying the Lesson 4 Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 8aa7d690-4649-4c0a-ac6f-9504637ee426
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 5-1 - Copying the Lesson 4 Package
In this task, you will create a copy of the Lesson 4.dtsx package that you created in Lesson 4. Alternatively, you can add the completed lesson 4 package that is included with the tutorial to the project, and then copy it instead. You will use this new copy throughout the rest of Lesson 5.  
  
### To copy the Lesson 4 package  
  
1.  If [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools is not already open, click **Start**, point to **All Programs**, point to **Microsoft SQL Server 2012**, and then click **SQL Server Data Tools**.  
  
2.  On the **File** menu, click **Open**, click **Project/Solution**, select **SSIS Tutorial** and click **Open**, and then double-click **SSIS Tutorial.sln**.  
  
3.  In Solution Explorer, right-click **Lesson 4.dtsx**, and then click **Copy**.  
  
4.  In Solution Explorer, right-click **SSIS Packages**, and then click **Paste**.  
  
    By default, the copied package is named Lesson 5.dtsx.  
  
5.  In the Solution Explorer, double-click **Lesson 5.dtsx** to open the package.  
  
6.  Right-click anywhere in the background of the **Control Flow** tab then click **Properties**.  
  
7.  In the Properties window, update the **Name** property to **Lesson 5**.  
  
8.  Click the box for the **ID** property, then click the dropdown arrow, and then click **<Generate New ID>**.  
  
### To add the completed Lesson 4 package  
  
1.  Open [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools and open the SSIS Tutorial project.  
  
2.  In Solution Explorer, right-click **SSIS Packages**, and click **Add Existing Package**.  
  
3.  In the **Add Copy of Existing Package** dialog box, in **Package location**, select **File system**.  
  
4.  Click the browse **(...)** button, navigate to **Lesson 4.dtsx** on your machine, and then click **Open**.  
  
    To download all of the lesson packages for this tutorial, do the following.  
  
    1.  Navigate to [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=275027)  
  
    2.  Click the **DOWNLOADS** tab.  
  
    3.  Click the SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.  
  
5.  Copy and paste the Lesson 4 package as described in steps 3-8 in the previous procedure.  
  
## Next Task in Lesson  
[Step 2: Enabling and Configuring Package Configurations](../integration-services/lesson-5-2-enabling-and-configuring-package-configurations.md)  
  
