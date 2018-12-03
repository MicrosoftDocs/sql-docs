---
title: "Step 3: Modifying the Flat File Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 459e3995-2116-4f15-aaa2-32f26113869c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 3: Modifying the Flat File Connection Manager
  In this task, you will modify the Flat File connection manager that you created and configured in Lesson 1. When originally created, the Flat File connection manager was configured to statically load a single file. To enable the Flat File connection manager to iteratively load files, you must modify the ConnectionString property of the connection manager to accept the user-defined variable `User:varFileName`, which contains the path of the file to be loaded at run time.  
  
 By modifying the connection manager to use the value of the user-defined variable, `User::varFileName`, to populate the ConnectionString property of the connection manager, the connection manager will be able to connect to different flat files. At run time, each iteration of the Foreach Loop container will dynamically update the `User::varFileName` variable. Updating the variable, in turn, causes the connection manager to connect to a different flat file, and the data flow task to process a different set of data.  
  
### To configure the Flat File connection manager to use a variable for the connection string  
  
1.  In the **Connection Managers** pane, right-click **Sample Flat File Source Data**, and select **Properties**.  
  
2.  In the Properties window, for **Expressions**, click in the empty cell, and then click the ellipsis button **(...)**.  
  
3.  In the **Property Expressions Editor** dialog box, in the **Property** column, type or select `ConnectionString`.  
  
4.  In the **Expression** column, click the ellipsis button **(...)** to open the **Expression Builder** dialog box.  
  
5.  In the **Expression Builder** dialog box, expand the **Variables** node.  
  
6.  Drag the variable, **User::varFileName**, into the **Expression** box.  
  
7.  Click **OK** to close the **Expression Builder** dialog box.  
  
8.  Click **OK** again to close the **Property Expressions Editor** dialog box.  
  
## Next Lesson Task  
 [Step 4: Testing the Lesson 2 Tutorial Package](../integration-services/lesson-2-4-testing-the-lesson-2-tutorial-package.md)  
  
  
