---
title: "SSIS Package Essentials | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "package requirements"
ms.assetid: b0c86c35-e3d3-4724-9a96-4087e9d74bf3
caps.latest.revision: 27
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# SSIS Package Essentials
  A package is the object that implements [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] functionality to extract, transform, and load data. You create a package by using the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. You can also create a package by running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard or the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Connections Project Wizard. For more information, [Create Packages in SQL Server Data Tools](../../2014/integration-services/create-packages-in-sql-server-data-tools.md) in SSIS Designer and [Import Project Wizard](../../2014/integration-services/import-project-wizard.md).  
  
 A basic package includes the following elements:  
  
 **Control flow elements**  
 These required elements perform various functions, provide structure, and control the order in which elements run. The main control flow elements are tasks, containers, and precedence constraints. There must be at least one control flow element in a package.  
  
 For more information, see [Control Flow](../../2014/integration-services/control-flow.md).  
  
 **Data flow elements**  
 These optional elements extract data, modify data, and load data into data sources. The main data flow elements are sources, transformations, and destinations. There does not have to be any data flow elements in package.  
  
 For more information, see [Data Flow](../../2014/integration-services/data-flow.md).  
  
 For an example of how to create a basic package, see [Lesson 1: Creating the Project and Basic Package](../../2014/tutorials/lesson-1-creating-the-project-and-basic-package.md).  
  
## Related Tasks  
  
-   [Create Packages in SQL Server Data Tools](../../2014/integration-services/create-packages-in-sql-server-data-tools.md)  
  
-   [Add or Delete a Task or a Container in a Control Flow](../../2014/integration-services/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  
-   [Set the Properties of a Task or Container](../../2014/integration-services/set-the-properties-of-a-task-or-container.md)  
  
-   [Add or Delete a Component in a Data Flow](../../2014/integration-services/add-or-delete-a-component-in-a-data-flow.md)  
  
## Related Content  
  
1.  Video, [Creating a Basic Package (SQL Server Video)](http://go.microsoft.com/fwlink/?LinkId=131023), on MSDN.Microsoft.com  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Packages](../../2014/integration-services/integration-services-ssis-packages.md)   
 [Precedence Constraints](../../2014/integration-services/precedence-constraints.md)  
  
  