---
title: "Designing Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [Analysis Services], designing"
  - "dependent assemblies [Analysis Services]"
  - "assemblies [Analysis Services]"
ms.assetid: af4e7bd5-041b-4a40-9942-0ef6a3af46c6
author: minewiskan
ms.author: owend
manager: craigg
---
# Designing Stored Procedures
  Both the administrative object model Analysis Management Objects (AMO) and the client oriented object model [!INCLUDE[msCoName](../../includes/msconame-md.md)] ActiveX® Data Objects (Multidimensional) (ADO MD) are available in stored procedures.  
  
 Stored procedures must be in scope (either the server or the database) to be visible at the Multidimensional Expressions (MDX) level to be called. However, once a stored procedure is invoked, its scope is not limited to actions under its parent. A stored procedure may make changes or modifications anywhere on the server, subject only to the security limitations of the user process that invokes it or to the limitations of the transaction in which it is operating.  
  
 Server scope procedures are available in all contexts on the server. Database scope stored procedures are visible only in the database context of the database in which they are defined.  
  
 As with any MDX function, stored procedure must be resolved before an MDX session can continue; stored procedures lock MDX sessions while executing. Unless a specific reason exists to halt an MDX session pending user interaction, then user interactions (such as dialog boxes) are discouraged.  
  
## Dependent Assemblies  
 All dependent assemblies must be loaded into an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to be found by the common language runtime (CLR). [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] stores the dependent assemblies in the same folder as the main assembly, so the CLR automatically resolves all function references to functions in those assemblies.  
  
## See Also  
 [Multidimensional Model Assemblies Management](../multidimensional-models/multidimensional-model-assemblies-management.md)   
 [Defining Stored Procedures](../multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)  
  
  
