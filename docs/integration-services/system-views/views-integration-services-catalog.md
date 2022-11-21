---
description: "Views (Integration Services Catalog)"
title: "Views (Integration Services Catalog) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "views [Integration Services]"
ms.assetid: d0294d43-4852-46dc-9afa-d0c19ea9aa03
author: chugugrace
ms.author: chugu
---
# Views (Integration Services Catalog)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

  This section describes the [!INCLUDE[tsql](../../includes/tsql-md.md)] views that are available for administering [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects that have been deployed to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Query the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] views to inspect objects, settings, and operational data that are stored in the **SSISDB** catalog.  
  
 The default name of the catalog is SSISDB. The objects that are stored in the catalog include projects, packages, parameters, environments, and operational history.  
  
 You can use the database views and stored procedures directly, or write custom code that calls the managed API. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and the managed API query the views and call the stored procedures that are described in this section to perform many of their tasks.  
  
## In This Section  
 [catalog.catalog_properties &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-catalog-properties-ssisdb-database.md)  
 Displays the properties of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.effective_object_permissions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-effective-object-permissions-ssisdb-database.md)  
 Displays the effective permissions for the current principal for all objects in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.environment_variables &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-environment-variables-ssisdb-database.md)  
 Displays the environment variable details for all environments in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.environments &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-environments-ssisdb-database.md)  
 Displays the environment details for all environments in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. Environments contain variables that can be referenced by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects.  
  
 [catalog.execution_parameter_values &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-execution-parameter-values-ssisdb-database.md)  
 Displays the actual parameter values that are used by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages during an instance of execution.  
  
 [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md)  
 Displays the instances of package execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. Packages that are executed with the Execute Package task run in the same instance of execution as the parent package.  
  
 [catalog.explicit_object_permissions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-explicit-object-permissions-ssisdb-database.md)  
 Displays only the permissions that have been explicitly assigned to the user.  
  
 [catalog.extended_operation_info &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-extended-operation-info-ssisdb-database.md)  
 Displays extended information for all operations in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.folders &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-folders-ssisdb-database.md)  
 Displays the folders in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.object_parameters &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-object-parameters-ssisdb-database.md)  
 Displays the parameters for all packages and projects in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.object_versions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-object-versions-ssisdb-database.md)  
 Displays the versions of objects in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. In this release, only versions of projects are supported in this view.  
  
 [catalog.operation_messages &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operation-messages-ssisdb-database.md)  
 Displays messages that are logged during operations in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md)  
 Displays the details of all operations in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.packages &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-packages-ssisdb-database.md)  
 Displays the details for all packages that appear in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.environment_references &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-environment-references-ssisdb-database.md)  
 Displays the environment references for all projects in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.projects &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-projects-ssisdb-database.md)  
 Displays the details for all projects that appear in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.validations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-validations-ssisdb-database.md)  
 Displays the details of all project and package validations in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
[catalog.master_properties &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-master-properties-ssisdb-database.md)  
Displays the properties of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Master.

[catalog.worker_agents &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-worker-agents-ssisdb-database.md)  
Displays the information of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Worker.  
