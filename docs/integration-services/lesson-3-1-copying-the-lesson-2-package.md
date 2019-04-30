---
title: "Step 1: Copy the Lesson 2 package | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 4bd91402-4e37-41de-ab78-8ca5a1948a37
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 3-1: Copy the Lesson 2 package

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



In this task, you create a copy of the Lesson 2.dtsx package from Lesson 2. If you didn't complete Lesson 2, you can add the completed lesson 2 package included with this tutorial to the project, and then copy it instead. You use this new copy throughout the rest of Lesson 3.

## Create the Lesson 3 package

Use this procedure if you're copying the completed Lesson 2.  To copy the sample Lesson 2, see the next section.

1.  If [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools isn't already open, select **Start** > **All Programs** > **Microsoft SQL Server 2017**, and then select **SQL Server Data Tools**.

2.  On the **File** menu, select **Open** > **Project/Solution**, select the **SSIS Tutorial** folder and select **Open**, and then double-click **SSIS Tutorial.sln**.

3.  In **Solution Explorer**, right-click **Lesson 2.dtsx**, and then select **Copy**.

4.  In **Solution Explorer**, right-click **SSIS Packages**, and then select **Paste**.

    By default, the name of the copied package is Lesson 3.dtsx.

5.  In **Solution Explorer**, double-click **Lesson 3.dtsx** to open the package

6.  Right-click anywhere in the background of the **Control Flow** design surface and select **Properties**.

7.  In the **Properties** window, change the **Name** property to **Lesson 3**.

8.  Select the box for the **ID** property, select the drop-down arrow, and then select **\<Generate New ID>**.

## Add the completed Lesson 2 package

1.  Open [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools and open the SSIS Tutorial project.

2.  In **Solution Explorer**, right-click **SSIS Packages** and select **Add Existing Package**.

3.  In the **Add Copy of Existing Package** dialog, in **Package location**, select **File system**.

4.  Select the browse **(...)** button, navigate to **Lesson 2.dtsx** on your machine, and then select **Open**.

5.  Copy and paste the Lesson 3 package as described in steps 3-8 in the previous section.  
  
## Go to next task
[Step 2: Add and configure logging](../integration-services/lesson-3-2-adding-and-configuring-logging.md)  
  
