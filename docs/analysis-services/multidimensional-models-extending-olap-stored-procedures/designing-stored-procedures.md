---
title: "Designing Stored Procedures | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Designing Stored Procedures
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Both the administrative object model Analysis Management Objects (AMO) and the client oriented object model [!INCLUDE[msCoName](../../includes/msconame-md.md)] ActiveX® Data Objects (Multidimensional) (ADO MD) are available in stored procedures.  
  
 Stored procedures must be in scope (either the server or the database) to be visible at the Multidimensional Expressions (MDX) level to be called. However, once a stored procedure is invoked, its scope is not limited to actions under its parent. A stored procedure may make changes or modifications anywhere on the server, subject only to the security limitations of the user process that invokes it or to the limitations of the transaction in which it is operating.  
  
 Server scope procedures are available in all contexts on the server. Database scope stored procedures are visible only in the database context of the database in which they are defined.  
  
 As with any MDX function, stored procedure must be resolved before an MDX session can continue; stored procedures lock MDX sessions while executing. Unless a specific reason exists to halt an MDX session pending user interaction, then user interactions (such as dialog boxes) are discouraged.  
  
## Dependent Assemblies  
 All dependent assemblies must be loaded into an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to be found by the common language runtime (CLR). [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] stores the dependent assemblies in the same folder as the main assembly, so the CLR automatically resolves all function references to functions in those assemblies.  
  
## See Also  
 [Multidimensional Model Assemblies Management](../../analysis-services/multidimensional-models/multidimensional-model-assemblies-management.md)   
 [Defining Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)  
  
  
