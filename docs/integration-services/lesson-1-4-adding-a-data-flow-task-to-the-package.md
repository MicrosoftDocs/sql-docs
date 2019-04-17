---
title: "Step 4: Add a Data Flow task to the package | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 96af3073-8f11-4444-b934-fe8613a2d084
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1-4: Add a Data Flow task to the package

After you've created the connection managers for the source and destination data, you add a Data Flow task to your package. The Data Flow task defines the data flow engine that moves data between sources and destinations, and provides the functionality for transforming, cleaning, and modifying data as it is moved. The Data Flow task is where most of the work of an extract, transform, and load (ETL) process occurs.  
  
> [!NOTE]  
> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] separates data flow from control flow.  
  
## Add a Data Flow task  
  
1.  Select the **Control Flow** tab.  
  
2.  In the **SSIS Toolbox** pane, expand **Favorites**, and drag a **Data Flow Task** onto the design surface of the **Control Flow** tab.  
  
    > [!NOTE]  
    > If the SSIS Toolbox isn't available, select the **SSIS** menu, and then select **SSIS Toolbox** to display it.  

3.  On the **Control Flow** design surface, right-click the new **Data Flow Task**, select **Rename**, and change the name to **Extract Sample Currency Data**.  
  
    Provide unique names for all components that you add to a design surface. For ease of use and maintainability, the names should describe the function of each component. Following these naming guidelines allows your [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages to be self-documenting. Another way to document your packages is by using annotations. For more information about annotations, see [Use annotations in packages](../integration-services/use-annotations-in-packages.md).  
  
4.  Right-click the Data Flow task, select **Properties**, and in the Properties window, verify that the **LocaleID** property is set to **English (United States)**.  
  
## Go to next task
[Step 5: Add and configure the Flat File source](../integration-services/lesson-1-5-adding-and-configuring-the-flat-file-source.md)  
  
## See also  
[Data Flow task](../integration-services/control-flow/data-flow-task.md)  
  
  
  
