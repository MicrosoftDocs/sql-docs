---
title: "Step 4: Adding a Data Flow Task to the Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 96af3073-8f11-4444-b934-fe8613a2d084
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 4: Adding a Data Flow Task to the Package
  After you have created the connection managers for the source and destination data, the next task is to add a Data Flow task to your package. The Data Flow task encapsulates the data flow engine that moves data between sources and destinations, and provides the functionality for transforming, cleaning, and modifying data as it is moved. The Data Flow task is where most of the work of an extract, transform, and load (ETL) process occurs.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] separates data flow from control flow.  
  
### To add a Data Flow task  
  
1.  Click the **Control Flow** tab.  
  
2.  In the **SSIS Toolbox**, expand **Favorites**, and drag a **Data Flow Task** onto the design surfaceof the **Control Flow** tab.  
  
    > [!NOTE]  
    >  If the SSIS Toolbox isn't available, on the main menu select SSIS then SSIS Toolbox to display the SSIS Toolbox.  
  
3.  On the **Control Flow** design surface, right-click the newly added **Data Flow Task**, click **Rename**, and change the name to `Extract Sample Currency Data`.  
  
     It is good practice to provide unique names to all components that you add to a design surface. For ease of use and maintainability, the names should describe the function that each component performs. Following these naming guidelines allows your [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages to be self-documenting. Another way to document your packages is by using annotations. For more information about annotations, see [Use Annotations in Packages](use-annotations-in-packages.md).  
  
4.  Right-click the Data Flow task, click **Properties**, and in the Properties window, verify that the `LocaleID` property is set to **English (United States)**.  
  
## Next Task in Lesson  
 [Step 5: Adding and Configuring the Flat File Source](lesson-1-5-adding-and-configuring-the-flat-file-source.md)  
  
## See Also  
 [Data Flow Task](control-flow/data-flow-task.md)  
  
  
