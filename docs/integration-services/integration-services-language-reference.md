---
description: "Integration Services Language Reference"
title: "Integration Services Language Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: c67b72f1-0a1e-42f0-878a-84e85efc915b
author: chugugrace
ms.author: chugu
---
# Integration Services Language Reference

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

  This section describes the [!INCLUDE[tsql](../includes/tsql-md.md)] API for administering [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects that have been deployed to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] stores objects, settings, and operational data in a database referred to as the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog. The default name of the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog is SSISDB. The objects that are stored in the catalog include projects, packages, parameters, environments, and operational history.  
  
 The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog stores its data in internal tables that are not visible to users. However it exposes the information that you need through a set of public views that you can query. It also provides a set of stored procedures that you can use to perform common tasks on the catalog.  
  
 Typically you manage [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] objects in the catalog by opening [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. However you can also use the database views and stored procedures directly, or write custom code that calls the managed API. [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] and the managed API query the views and call the stored procedures that are described in this section to perform many of their tasks.  
  
## In This Section  
 [Views &#40;Integration Services Catalog&#41;](../integration-services/system-views/views-integration-services-catalog.md)  
 Query the views to inspect [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] objects, settings, and operational data.  
  
 [Stored Procedures &#40;Integration Services Catalog&#41;](../integration-services/system-stored-procedures/stored-procedures-integration-services-catalog.md)  
 Call the stored procedures to add, remove, or modify [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] objects and settings.  
  
 [Functions &#40;Integration Services Catalog&#41;](./functions-dm-execution-performance-counters.md)  
 Call the functions to administer [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects.  
  
