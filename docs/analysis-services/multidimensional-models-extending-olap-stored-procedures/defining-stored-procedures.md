---
title: "Defining Stored Procedures | Microsoft Docs"
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
# Defining Stored Procedures
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can use stored procedures to call external routines from [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. You can write an external routines called by a stored procedure in any common language runtime (CLR) language, such as C, C++, C#, Visual Basic, or Visual Basic .NET. A stored procedure can be created once and called from many contexts, such as other stored procedures, calculated measures, or client applications. Stored procedures simplify [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database development and implementation by allowing common code to be developed once and stored in a single location. Stored procedures can be used to add business functionality to your applications that is not provided by the native functionality of MDX.  
  
 This section provides the information necessary to understand, design, and implement stored procedures.  
  
|Topic|Description|  
|-----------|-----------------|  
|[Designing Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/designing-stored-procedures.md)|Describes how to design assemblies for use with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[Creating Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/creating-stored-procedures.md)|Describes how to create assemblies for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[Calling Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/calling-stored-procedures.md)|Provides information on how to use assemblies in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[Accessing Query Context in Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/accessing-query-context-in-stored-procedures.md)|Describes how to access scope and context information with assemblies.|  
|[Setting Security for Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/setting-security-for-stored-procedures.md)|Describes how to configure security for assemblies in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[Debugging Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/debugging-stored-procedures.md)|Describes how to debug assemblies in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
  
## See Also  
 [Multidimensional Model Assemblies Management](../../analysis-services/multidimensional-models/multidimensional-model-assemblies-management.md)  
  
  
