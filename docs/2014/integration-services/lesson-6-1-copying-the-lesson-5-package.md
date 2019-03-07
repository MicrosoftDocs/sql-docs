---
title: "Step 1: Copying the Lesson 5 Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: a25fcc13-987e-4f3d-8f0c-76f7e6e59920
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 1: Copying the Lesson 5 Package
  In this task, you will create a copy of the Lesson 5.dtsx package that you created in Lesson 5. Alternatively, you can add the completed lesson 5 package that is included with the tutorial to the project, and then copy it instead. You will use this new copy throughout the rest of Lesson 6.  
  
### To copy the Lesson 5 package  
  
1.  If SQL Server Data Tools is not already open, click Start, point to All Programs, point to Microsoft SQL Server 2012, and then click SQL Server Data Tools.  
  
2.  On the File menu, click Open, click Project/Solution, select SSIS Tutorial and click Open, and then double-click SSIS Tutorial.sln.  
  
3.  In Solution Explorer, right-click Lesson 5.dtsx, and then click Copy.  
  
4.  In Solution Explorer, right-click SSIS Packages, and then click Paste.  
  
     By default, the copied package is named Lesson 6.dtsx.  
  
5.  In the Solution Explorer, double-click Lesson 6.dtsx to open the package.  
  
6.  Right-click anywhere in the background of the Control Flow tab then click Properties.  
  
7.  In the Properties window, update the Name property to Lesson 6.  
  
8.  Click the box for the ID property, then click the dropdown arrow, and then click \<Generate New ID>.  
  
### To add the completed Lesson 5 package  
  
1.  Open SQL Server Data Tools and open the SSIS Tutorial project.  
  
2.  In Solution Explorer, right-click SSIS Packages, and click Add Existing Package.  
  
3.  In the Add Copy of Existing Package dialog box, in Package location, select File system.  
  
4.  Click the browse (...) button, Lesson 5.dtsx on your machine, and then click **Open**.  
  
     To download all of the lesson packages for this tutorial, do the following.  
  
    1.  Navigate to [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=275027)  
  
    2.  Click the **DOWNLOADS** tab.  
  
    3.  Click the SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.  
  
5.  Copy and paste the Lesson 5 package as described in steps 3-8 in the previous procedure.  
  
     After copying the Lesson 5 package, if you currently have the packages from the previous lessons in your solution, right-click each package from lessons 1-5 and click Exclude From Project. When done you should have only Lesson 6.dtsx in your solution.  
  
## Next Task in Lesson  
 [Step 2: Converting the Project to the Project Deployment Model](lesson-6-2-converting-the-project-to-the-project-deployment-model.md)  
  
  
