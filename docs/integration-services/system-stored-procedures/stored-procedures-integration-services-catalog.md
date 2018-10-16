---
title: "Stored Procedures (Integration Services Catalog) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "stored procedures [Integration Services]"
ms.assetid: a6ccd884-108f-4fb6-95ad-00b9cb65d5d6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Stored Procedures (Integration Services Catalog)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  This section describes the [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures that are available for administering [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects that have been deployed to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Call the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] stored procedures to add, remove, modify, or execute objects that are stored in the **SSISDB** catalog.  
  
 The default name of the catalog is SSISDB. The objects that are stored in the catalog include projects, packages, parameters, environments, and operational history.  
  
 You can use the database views and stored procedures directly, or write custom code that calls the managed API. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and the managed API query the views and call the stored procedures that are described in this section to perform many of their tasks.  
  
## In This Section  
 [catalog.add_data_tap](../../integration-services/system-stored-procedures/catalog-add-data-tap.md)  
 Adds a data tap on the output of a component in a package data flow.  
  
 [catalog.add_data_tap_by_guid](../../integration-services/system-stored-procedures/catalog-add-data-tap-by-guid.md)  
 Adds a data tap to a specific data flow path in a package data flow.  
  
 [catalog.check_schema_version](../../integration-services/system-stored-procedures/catalog-check-schema-version.md)  
 Determines whether the SSISDB catalog schema and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] binaries (ISServerExec and SQLCLR assembly) are compatible.  
  
 [catalog.clear_object_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-clear-object-parameter-value-ssisdb-database.md)  
 Clears the value of a parameter for an existing [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project or package that is stored on the server.  
  
 [catalog.configure_catalog &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-configure-catalog-ssisdb-database.md)  
 Configures the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog by setting a catalog property to a specified value.  
  
 [catalog.create_environment &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-environment-ssisdb-database.md)  
 Creates an environment in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.create_environment_reference &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-environment-reference-ssisdb-database.md)  
 Creates an environment reference for a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.create_environment_variable &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-environment-variable-ssisdb-database.md)  
 Create an environment variable in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.create_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md)  
 Creates an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.create_execution_dump](../../integration-services/system-stored-procedures/catalog-create-execution-dump.md)  
 Causes a running package to pause and create a dump file.  
  
 [catalog.create_folder &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-folder-ssisdb-database.md)  
 Creates a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.delete_environment &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-delete-environment-ssisdb-database.md)  
 Deletes an environment from a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.delete_environment_reference &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-delete-environment-reference-ssisdb-database.md)  
 Deletes an environment reference from a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.delete_environment_variable &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-delete-environment-variable-ssisdb-database.md)  
 Deletes an environment variable from an environment in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.delete_folder &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-delete-folder-ssisdb-database.md)  
 Deletes a folder from the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.delete_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-delete-project-ssisdb-database.md)  
 Deletes an existing project from a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.deny_permission &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-deny-permission-ssisdb-database.md)  
 Denies a permission on a securable object in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.deploy_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-deploy-project-ssisdb-database.md)  
 Deploys a project to a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog or updates an existing project that has been deployed previously.  
  
 [catalog.get_parameter_values &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-get-parameter-values-ssisdb-database.md)  
 Resolves and retrieves the default parameter values from a project and corresponding packages in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.get_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-get-project-ssisdb-database.md)  
 Retrieves the properties of an existing project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.grant_permission &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-grant-permission-ssisdb-database.md)  
 Grants a permission on a securable object in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.move_environment &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-move-environment-ssisdb-database.md)  
 Moves an environment from one folder to another within the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.move_project &#40;&#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-move-project-ssisdb-database.md)  
 Moves a project from one folder to another within the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.remove_data_tap](../../integration-services/system-stored-procedures/catalog-remove-data-tap.md)  
 Removes a data tap from a component output that is in an execution.  
  
 [catalog.rename_environment &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-rename-environment-ssisdb-database.md)  
 Renames an environment in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.rename_folder &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-rename-folder-ssisdb-database.md)  
 Renames a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.restore_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-restore-project-ssisdb-database.md)  
 Restores a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog to a previous version.  
  
 [catalog.revoke_permission &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-revoke-permission-ssisdb-database.md)  
 Revokes a permission on a securable object in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_environment_property &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-environment-property-ssisdb-database.md)  
 Sets the property of an environment in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_environment_reference_type &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-environment-reference-type-ssisdb-database.md)  
 Sets the reference type and environment name associated with an existing environment reference for a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_environment_variable_property &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-environment-variable-property-ssisdb-database.md)  
 Sets the property of an environment variable in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_environment_variable_protection &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-environment-variable-protection-ssisdb-database.md)  
 Sets the sensitivity bit of an environment variable in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_environment_variable_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-environment-variable-value-ssisdb-database.md)  
 Sets the value of an environment variable in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md)  
 Sets the value of a parameter for an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_execution_property_override_value](../../integration-services/system-stored-procedures/catalog-set-execution-property-override-value.md)  
 Sets the value of a property for an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_folder_description &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-folder-description-ssisdb-database.md)  
 Sets the description of a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.set_object_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-object-parameter-value-ssisdb-database.md)  
 Sets the value of a parameter in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. Associates the value to an environment variable or assigns a literal value that will be used by default if no other values are assigned.  
  
 [catalog.start_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database.md)  
 Starts an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.startup](../../integration-services/system-stored-procedures/catalog-startup.md)  
 Performs maintenance of the state of operations for the SSISDB catalog.  
  
 [catalog.stop_operation &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-stop-operation-ssisdb-database.md)  
 Stops a validation or instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.validate_package &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-validate-package-ssisdb-database.md)  
 Asynchronously validates a package in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 [catalog.validate_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-validate-project-ssisdb-database.md)  
 Asynchronously validates a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
[catalog.add_execution_worker &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-add-execution-worker-ssisdb-database.md)   
Adds a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Worker to an instance of execution in Scale Out.

[catalog.enable_worker_agent &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-enable-worker-agent-ssisdb-database.md)   
Enable a Scale Out Worker for Scale Out Master working with this [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.

[catalog.disable_worker_agent &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-disable-worker-agent-ssisdb-database.md)   
Disable a Scale Out Worker for Scale Out Master working with this [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.


