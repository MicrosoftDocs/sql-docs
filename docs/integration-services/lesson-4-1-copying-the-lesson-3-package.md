---
title: "Step 1: Copy the Lesson 3 package | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 0d053786-5203-43f3-a613-27a8dd2bc44a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 4-1: Copy the Lesson 3 package

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



In this task, you create a copy of the Lesson 3.dtsx package from Lesson 3. If you did not complete lesson 3, you can add the completed lesson 3 package that is included with the tutorial to the project, and then make a copy of it to work with. You use this new copy throughout the rest of Lesson 4.  
  
## Create the Lesson 4 package  
  
Use this procedure if you're copying the completed Lesson 3.  To copy the sample Lesson 3, see the next section.

1.  If [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools isn't already open, select **Start** > **All Programs** > **Microsoft SQL Server 2017**, and then select **SQL Server Data Tools**.

2.  On the **File** menu, select **Open** > **Project/Solution**, select the **SSIS Tutorial** folder and select **Open**, and then double-click **SSIS Tutorial.sln**.

3.  In **Solution Explorer**, right-click **Lesson 3.dtsx**, and then select **Copy**.

4.  In **Solution Explorer**, right-click **SSIS Packages**, and then select **Paste**.

    By default, the name of the copied package is **Lesson 4.dtsx**.

5.  In **Solution Explorer**, double-click **Lesson 4.dtsx** to open the package

6.  Right-click anywhere in the background of the **Control Flow** design surface and select **Properties**.

7.  In the **Properties** window, change the **Name** property to **Lesson 4**.

8.  Select the box for the **ID** property, select the drop-down arrow, and then select **\<Generate New ID>**.

## Add the completed Lesson 3 package

1.  Open [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools and open the SSIS Tutorial project.

2.  In **Solution Explorer**, right-click **SSIS Packages** and select **Add Existing Package**.

3.  In the **Add Copy of Existing Package** dialog box, in **Package location**, select **File system**.

4.  Select the browse **(...)** button, navigate to **Lesson 3.dtsx** on your machine, and then select **Open**.

5.  Copy and paste the Lesson 3 package as described in steps 3-8 in the previous section.

  
## Go to next task  
[Step 2: Create a corrupted file](../integration-services/lesson-4-2-creating-a-corrupted-file.md)  
  
