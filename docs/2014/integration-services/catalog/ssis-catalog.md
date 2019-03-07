---
title: "SSIS Catalog | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 24bd987e-164a-48fd-b4f2-cbe16a3cd95e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SSIS Catalog
  The `SSISDB` catalog is the central point for working with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] (SSIS) projects that you've deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For example, you set project and package parameters, configure environments to specify runtime values for packages, execute and troubleshoot packages, and manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server operations.  
  
 The objects that are stored in the `SSISDB` catalog include projects, packages, parameters, environments, and operational history.  
  
 You inspect objects, settings, and operational data that are stored in the `SSISDB` catalog, by querying the views in the `SSISDB` database. You manage the objects by calling stored procedures in the `SSISDB` database or by using the UI of the `SSISDB` catalog. In many cases, the same task can be performed in the UI or by calling a stored procedure.  
  
 To maintain the `SSISDB` database, it is recommended that you apply standard enterprise policies for managing user databases. For information about creating maintenance plans, see [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md).  
  
 The `SSISDB` catalog and the `SSISDB` database support Windows PowerShell. For more information about using SQL Server with Windows PowerShell, see [SQL Server PowerShell](../../powershell/sql-server-powershell.md). For examples of how to use Windows PowerShell to complete tasks such as deploying a project, see the blog entry, [SSIS and PowerShell in SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=242539), on blogs.msdn.com.  
  
 For more information about viewing operations data, see [Monitoring for Package Executions and Other Operations](../performance/monitor-running-packages-and-other-operations.md).  
  
 You access the `SSISDB` catalog in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by connecting to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine and then expanding the **Integration Services Catalogs** node in Object Explorer. You access the `SSISDB` database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by expanding the Databases node in Object Explorer.  
  
> [!NOTE]  
>  You cannot rename the `SSISDB` database.  
  
> [!NOTE]  
>  If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that the `SSISDB` database is attached to, stops or does not respond, the ISServerExec.exe process ends. A message is written to a Windows Event log.  
>   
>  If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources failover as part of a cluster failover, the running packages do not restart. You can use checkpoints to restart packages. For more information, see [Restart Packages by Using Checkpoints](../packages/restart-packages-by-using-checkpoints.md).  
  
## Catalog Object Identifiers  
 When you create a new object in the catalog, assign a name to the object. The object name is an identifier. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] defines rules for which characters can be used in an identifier. Names for the following objects must follow identifier rules.  
  
-   Folder  
  
-   Project  
  
-   Environment  
  
-   Parameter  
  
-   Environment Variable  
  
### Folder, Project, Environment  
 Consider the following rules when renaming a folder, project, or environment.  
  
-   Invalid characters include ASCII/Unicode characters 1 through 31, quote ("), less than (\<), greater than (>), pipe (|), backspace (\b), null (\0), and tab (\t).  
  
-   The name might not contain leading or trailing spaces.  
  
-   \@ is not allowed as the first character, but subsequent characters might use \@.  
  
-   The length of the name must be greater than 0 and less than or equal to 128.  
  
### Parameter  
 Consider the following rules when naming a parameter.  
  
-   The first character of the name must be a letter as defined in the Unicode Standard 2.0, or an underscore (_).  
  
-   Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, or an underscore (_).  
  
### Environment Variable  
 Consider the following rules when naming an environment variable.  
  
-   Invalid characters include ASCII/Unicode characters 1 through 31, quote ("), less than (\<), greater than (>), pipe (|), backspace (\b), null (\0), and tab (\t).  
  
-   The name might not contain leading or trailing spaces.  
  
-   \@ is not allowed as the first character, but subsequent characters might use \@.  
  
-   The length of the name must be greater than 0 and less than or equal to 128.  
  
-   The first character of the name must be a letter as defined in the Unicode Standard 2.0, or an underscore (_).  
  
-   Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, or an underscore (_).  
  
## Catalog Configuration  
 You fine-tune how the catalog behaves by adjusting the catalog properties. Catalog properties define how sensitive data is encrypted, and how operations and project versioning data is retained. To set catalog properties, use the **Catalog Properties** dialog box or call the [catalog.configure_catalog &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-configure-catalog-ssisdb-database) stored procedure. To view the properties, use the dialog box or query [catalog.catalog_properties &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-catalog-properties-ssisdb-database). You access the dialog box by right-clicking `SSISDB` in Object Explorer.  
  
### Operations and Project Version Cleanup  
 Status data for many of the operations in the catalog is stored in internal database tables. For example, the catalog tracks the status of package executions and project deployments. To maintain the size of the operations data, the **SSIS Server Maintenance Job** in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is used to remove old data. This [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job is created when [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is installed.  
  
 You can update or redeploy an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project by deploying it with the same name to the same folder in the catalog. By default, each time you redeploy a project, the `SSISDB` catalog retains the previous version of the project. To maintain the size of the operations data, the **SSIS Server Maintenance Job** is used to remove old versions of projects.  
  
 The following `SSISDB` catalog properties define how this [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job behaves. You can view and modify the properties by using the **Catalog Properties** dialog box or by using [catalog.catalog_properties &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-catalog-properties-ssisdb-database) and [catalog.configure_catalog &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-configure-catalog-ssisdb-database).  
  
 **Clean Logs Periodically**  
 The job step for operations cleanup runs when this property is set to `True`.  
  
 **Retention Period (days)**  
 Defines the maximum age of allowable operations data (in days). Older data are removed.  
  
 The minimum value is one day. The maximum value is limited only by the maximum value of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] `int` data. For information about this data type, see [int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](/sql/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql).  
  
 **Periodically Remove Old Versions**  
 The job step for project version cleanup runs when this property is set to `True`.  
  
 **Maximum Number of Versions per Project**  
 Defines how many versions of a project are stored in the catalog. Older versions of projects are removed.  
  
### Encryption Algorithm  
 The **Encryption Algorithm** property specifies the type of encryption that is used to encrypt sensitive parameter values. You can choose from the following types of encryption.  
  
-   AES_256 (default)  
  
-   AES_192  
  
-   AES_128  
  
-   DESX  
  
-   TRIPLE_DES_3KEY  
  
-   TRIPLE_DES  
  
-   DES  
  
 When you deploy an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]server, the catalog automatically encrypts the package data and sensitive values. The catalog also automatically decrypts the data when you retrieve it. The SSISDB catalog uses the `ServerStorage` protection level. For more information, see [Access Control for Sensitive Data in Packages](../security/access-control-for-sensitive-data-in-packages.md).  
  
 Changing the encryption algorithm is a time-intensive operation. First, the server has to use the previously specified algorithm to decrypt all configuration values. Then, the server has to use the new algorithm to re-encrypt the values. During this time, there cannot be other [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] operations on the server. Thus, to enable [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] operations to continue uninterrupted, the encryption algorithm is a read-only value in the  dialog box in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
 To change the **Encryption Algorithm** property setting, set the `SSISDB` database to the single-user mode and then call the catalog.configure_catalog stored procedure. Use ENCRYPTION_ALGORITHM for the *property_name* argument. For the supported property values, see [catalog.catalog_properties &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-catalog-properties-ssisdb-database). For more information about the stored procedure, see [catalog.configure_catalog &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-configure-catalog-ssisdb-database).  
  
 For more information about single-user mode, see [Set a Database to Single-user Mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md). For information about encryption and encryption algorithms in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the topics in the section, [SQL Server Encryption](../../relational-databases/security/encryption/sql-server-encryption.md).  
  
 A database master key is used for the encryption. The key is created when you create the catalog. For more information, see [Create the SSIS Catalog](ssis-catalog.md).  
  
 The following table lists the property names shown in the **Catalog Properties** dialog box and the corresponding properties in the database view.  
  
|Property Name (**Catalog Properties** dialog box)|Property Name (database view)|  
|---------------------------------------------------------|-------------------------------------|  
|Encryption Algorithm Name|ENCRYPTION_ALGORITHM|  
|Clean Logs Periodically|OPERATION_CLEANUP_ENABLED|  
|Retention Period (days)|RETENTION_WINDOW|  
|Periodically Remove Old Versions|VERSION_CLEANUP_ENABLED|  
|Maximum Number of Versions per Project|MAX_PROJECT_VERSIONS|  
|Server-wide Default Logging Level|SERVER_LOGGING_LEVEL|  
  
## Permissions  
 Projects, environments, and packages are contained in folders that are securable objects. You can grant permissions to a folder, including the MANAGE_OBJECT_PERMISSIONS permission. MANAGE_OBJECT_PERMISSIONS enables you to delegate the administration of folder contents to a user without having to grant the user membership to the ssis_admin role. You can also grant permissions to projects, environments, and operations. Operations include initializing [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], deploying projects, creating and starting executions, validating projects and packages, and configuring the `SSISDB` catalog.  
  
 For more information about database roles, see [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md).  
  
 The SSISDB catalog uses a DDL trigger, ddl_cleanup_object_permissions, to enforce the integrity of permissions information for SSIS securables. The trigger fires when a database principal, such as a database user, database role, or a database application role, is removed from the SSISDB database.  
  
 If the principal has granted or denied permissions to other principals, revoke the permissions given by the grantor, before the principal can be removed. Otherwise, an error message is returned when the system tries to remove the principal. The trigger removes all permission records where the database principal is a grantee.  
  
 It is recommended that the trigger is not disabled because it ensures that are no orphaned permission records after a database principal is dropped from the `SSISDB` database.  
  
### Managing Permissions  
 You can manage permissions by using the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] UI, stored procedures, and the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace.  
  
 To manage permissions using the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] UI, use the following dialog boxes.  
  
-   For a folder, use the **Permissions** page of the [Folder Properties Dialog Box](folder-properties-dialog-box.md).  
  
-   For a project, use the **Permissions** page in the [Project Properties Dialog Box](project-properties-dialog-box.md).  
  
 To manage permissions using Transact-SQL, call [catalog.grant_permission &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-grant-permission-ssisdb-database), [catalog.deny_permission &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-deny-permission-ssisdb-database) and [catalog.revoke_permission &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-revoke-permission-ssisdb-database). To view effective permissions for the current principal for all objects, query [catalog.effective_object_permissions &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-effective-object-permissions-ssisdb-database). This topic provides descriptions of the different types of permissions. To view permissions that have been explicitly assigned to the user, query [catalog.explicit_object_permissions &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-explicit-object-permissions-ssisdb-database).  
  
## Folders  
 A folder contains one or more projects and environments in the `SSISDB` catalog. You can use the [catalog.folders &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-folders-ssisdb-database) view to access information about folders in the catalog. You can use the following stored procedures to manage folders.  
  
-   [catalog.create_folder &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-create-folder-ssisdb-database)  
  
-   [catalog.delete_folder &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-delete-folder-ssisdb-database)  
  
-   [catalog.rename_folder &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-rename-folder-ssisdb-database)  
  
-   [catalog.set_folder_description &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-folder-description-ssisdb-database)  
  
## Projects and Packages  
 Each project can contain multiple packages. Both projects and packages can contain parameters and references to environments. You can access the parameters and environment references by using the [Configure Dialog Box](configure-dialog-box.md).  
  
 You can carry out other project tasks by calling the following stored procedures.  
  
-   [catalog.delete_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-delete-project-ssisdb-database)  
  
-   [catalog.deploy_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-deploy-project-ssisdb-database)  
  
-   [catalog.get_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-get-project-ssisdb-database)  
  
-   [catalog.move_project &#40;&#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-move-project-ssisdb-database)  
  
-   [catalog.restore_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-restore-project-ssisdb-database)  
  
 These views provide details about packages, projects, and project versions.  
  
-   [catalog.projects &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-projects-ssisdb-database)  
  
-   [catalog.packages &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-packages-ssisdb-database)  
  
-   [catalog.object_versions &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-object-versions-ssisdb-database)  
  
## Parameters  
 You use parameters to assign values to package properties at the time of package execution. To set the value of a package or project parameter and to clear the value, call [catalog.set_object_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-object-parameter-value-ssisdb-database) and [catalog.clear_object_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-clear-object-parameter-value-ssisdb-database). To set the value of a parameter for an instance of execution, call [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database). You can retrieve default parameter values by calling [catalog.get_parameter_values &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-get-parameter-values-ssisdb-database).  
  
 These views show the parameters for all packages and projects, and parameter values that are used for an instance of execution.  
  
-   [catalog.object_parameters &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-object-parameters-ssisdb-database)  
  
-   [catalog.execution_parameter_values &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-execution-parameter-values-ssisdb-database)  
  
## Server Environments, Server Variables, and Server Environment References  
 Server environments contain server variables. The variable values can be used when a package is executed or validated on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
 The following stored procedures enable you to perform many other management tasks for environments and variables.  
  
-   [catalog.create_environment &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-create-environment-ssisdb-database)  
  
-   [catalog.delete_environment &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-delete-environment-ssisdb-database)  
  
-   [catalog.move_environment &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-move-environment-ssisdb-database)  
  
-   [catalog.rename_environment &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-rename-environment-ssisdb-database)  
  
-   [catalog.set_environment_property &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-environment-property-ssisdb-database)  
  
-   [catalog.create_environment_variable &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-create-environment-variable-ssisdb-database)  
  
-   [catalog.delete_environment_variable &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-delete-environment-variable-ssisdb-database)  
  
-   [catalog.set_environment_variable_property &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-environment-variable-property-ssisdb-database)  
  
-   [catalog.set_environment_variable_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-environment-variable-value-ssisdb-database)  
  
 By calling the [catalog.set_environment_variable_protection &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-environment-variable-protection-ssisdb-database) stored procedure, you can set the sensitivity bit for a variable.  
  
 To use the value of a server variable, specify the reference between the project and the server environment. You can use the following stored procedures to create and delete references. You can also indicate whether the environment can be located in the same folder as the project or in a different folder.  
  
-   [catalog.create_environment_reference &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-create-environment-reference-ssisdb-database)  
  
-   [catalog.delete_environment_reference &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-delete-environment-reference-ssisdb-database)  
  
-   [catalog.set_environment_reference_type &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-environment-reference-type-ssisdb-database)  
  
 For more details about environments and variables, query these views.  
  
-   [catalog.environments &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-environments-ssisdb-database)  
  
-   [catalog.environment_variables &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-environment-variables-ssisdb-database)  
  
-   [catalog.environment_references &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-environment-references-ssisdb-database)  
  
## Executions and Validations  
 An execution is an instance of a package execution. Call [catalog.create_execution &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database) and [catalog.start_execution &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database) to create and start an execution. To stop an execution or a package/project validation, call [catalog.stop_operation &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-stop-operation-ssisdb-database).  
  
 To cause a running package to pause and create a dump file, call the catalog.create_execution_dump stored procedure. A dump file provides information about the execution of a package that can help you troubleshoot execution issues. For more information about generating and configuring dump files, see [Generating Dump Files for Package Execution](../troubleshooting/generating-dump-files-for-package-execution.md).  
  
 For details about executions, validations, messages that are logged during operations, and contextual information related to errors, query these views.  
  
-   [catalog.executions &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-executions-ssisdb-database)  
  
-   [catalog.operations &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-operations-ssisdb-database)  
  
-   [catalog.operation_messages &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-operation-messages-ssisdb-database)  
  
-   [catalog.extended_operation_info &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-extended-operation-info-ssisdb-database)  
  
-   [catalog.event_messages](/sql/integration-services/system-views/catalog-event-messages)  
  
-   [catalog.event_message_context](/sql/integration-services/system-views/catalog-event-message-context)  
  
 You can validate projects and packages by calling the [catalog.validate_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-validate-project-ssisdb-database) and [catalog.validate_package &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-validate-package-ssisdb-database) stored procedures. The [catalog.validations &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-validations-ssisdb-database) view provides details about validations such as the server environment references that are considered in the validation, whether it is a dependency validation or a full validation, and whether the 32-bit runtime or the 64-bit runtime is used to run the package.  
  
## Related Tasks  
  
-   [Create the SSIS Catalog](ssis-catalog.md)  
  
-   [Backup, Restore, and Move the SSIS Catalog](../backup-restore-and-move-the-ssis-catalog.md)  
  
## Related Content  
  
-   Blog entry, [SSIS and PowerShell in SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=242539), on blogs.msdn.com.  
  
-   Blog entry, [SSIS Catalog Access Control Tips](https://go.microsoft.com/fwlink/?LinkId=246669), on blogs.msdn.com.  
  
-   Blog entry, [A Glimpse of the SSIS Catalog Managed Object Model](https://go.microsoft.com/fwlink/?LinkId=254267), on blogs.msdn.com.  
  
  
