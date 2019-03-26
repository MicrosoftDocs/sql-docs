---
title: "Step 3: Modify the Flat File connection manager | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 459e3995-2116-4f15-aaa2-32f26113869c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 2-3: Modify the Flat File connection manager

In this task, you modify the Flat File connection manager from Lesson 1. That Flat File connection manager is configured to statically load a single file. To enable the Flat File connection manager to iteratively load files, you change the ConnectionString property of the connection manager to use the user-defined variable `User::varFileName`, which contains the path of the file to be loaded at run time.  
  
By modifying the connection manager to use the value of the user-defined variable to change the ConnectionString property, the connection manager connects to different flat files. At run time, each iteration of the Foreach Loop container updates the `User::varFileName` variable. Updating the variable, in turn, causes the connection manager to connect to a different flat file, and the data flow task to process a different set of data.  
  
## Configure the Flat File connection manager to use a variable  
  
1.  In the **Connection Managers** pane, right-click **Sample Flat File Source Data**, and select **Properties**.  

2.  In the **Properties** window make sure the **PackagePath** starts with **\Package.Connections**. If not, in the **Connection Managers** pane, right-click **Sample Flat File Source Data**, and select **Convert to Package Connection**
  
3.  In the **Properties** window, for **Expressions**, select the empty cell, and then select the ellipsis button **(...)**.  
  
4.  In the **Property Expressions Editor** dialog, in the **Property** column, select **ConnectionString**.  
  
5.  In the **Expression** column, select the ellipsis button **(...)** to open the **Expression Builder** dialog box.  
  
6.  In the **Expression Builder** dialog, expand the **Variables** node.  
  
7.  Drag the variable **User::varFileName** into the **Expression** box.  
  
8.  Select **OK** to close the **Expression Builder** dialog.  
  
9.  Select **OK** again to close the **Property Expressions Editor** dialog.  
  
## Go to next task  
[Step 4: Test the Lesson 2 tutorial package](../integration-services/lesson-2-4-testing-the-lesson-2-tutorial-package.md)  
  
  
  
