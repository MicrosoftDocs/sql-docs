---
title: "Specifying Configuration Settings for Solution Deployment | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Analysis Services Deployment Wizard, configuration settings"
  - "input files [Analysis Services]"
  - "configuration options [Analysis Services]"
  - "Analysis Services deployments, configuration settings"
  - "deploying [Analysis Services], configuration settings"
ms.assetid: 953814a3-85ef-40cc-b46a-d532aa7a6569
author: minewiskan
ms.author: owend
manager: craigg
---
# Specifying Configuration Settings for Solution Deployment
  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard reads the partition and role deployment options that you use in the deployment script from the \<*project name*>.configsettings file. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] creates this file when you build the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] uses the configuration settings of the current project to create the \<*project name*>.configsettings file.  
  
## Reviewing the Configuration Settings for Deployment  
 The following are the configuration settings stored in the \<*project name*>.configsettings file:  
  
-   **Data Source Connection Strings** These are the connection strings for each data source based on the values specified in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. The user id and password are always removed from the connection string before the remainder of the string is stored in this file. However, if the Deployment Wizard is deploying directly to an Analysis Services instance, you can add the appropriate user id and password information within the wizard to enable a successful processing of the deployment database. This connection information will not be stored in the deployment script itself if one is saved by the Deployment Wizard.  
  
-   **Impersonation Accounts** This setting specifies the user name that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses to run statements in each data source. If no impersonation account is specified, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses its logon account to run statements. If the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] logon account is granted permissions directly in the data source, all database administrators in all databases in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance have access to the data source through the logon account. If a user account and password is specified, this information is always removed before the impersonation information is stored in this file. However, if the Deployment Wizard is deploying directly to an Analysis Services instance, you can add the appropriate user id and password information within the wizard to enable a successful processing of the deployment database. This impersonation information will not be stored in the deployment script itself if one is saved by the Deployment Wizard.  
  
-   **Key Error Log Files** This setting specifies the file name and path of the key error log file for each cube, measure group, partition, and dimension in the database.  
  
-   **Storage Locations** This setting specifies the storage location for each cube, measure group, and partition in the database. If no value is provided for an object, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard uses the default location for the object. For example, partitions use the location for the measure group, measure groups use the location for the cube, and cubes use the default location for objects on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. The storage location can be a local or a Universal Naming Convention (UNC) path.  
  
-   **Report Server** This setting specifies the report server and folder location for each report action defined in each cube in the database.  
  
## Modifying the Configuration Settings for Deployment  
 In some cases, you may need to deploy the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project using different configuration settings than those stored in the \<*project name*>.configsettings file. For example, you may want to change the connection string to one or more data sources, or specify storage locations for specific partitions or measure groups.  
  
 To modify the deployment of partitions and roles in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, you must change this information within the \<*project name*>.configsettings file, as described in the procedure below. You cannot change the partition and roles settings within the project because the *\<project name>* **Properties Pages** dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] does not display these options.  
  
> [!NOTE]  
>  Configuration settings can apply to all objects or only to newly created objects. Apply configuration settings to newly created objects only when you are deploying additional objects to a previously deployed [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database and do not want to overwrite existing objects. To specify whether configuration settings apply to all objects or only to newly created ones, set this option in the \<*project name*>.deploymentoptions file. For more information, see [Specifying Partition and Role Deployment Options](deployment-script-files-partition-and-role-deployment-options.md).  
  
#### To change configuration settings after the input files have been generated  
  
-   Run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard interactively, and on the **Configuration Settings** page, specify the configuration setting for the objects being deployed.  
  
     -or-  
  
-   Run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard at the command prompt and set the wizard to run in answer file mode. For more information about answer file mode, see [Running the Analysis Services Deployment Wizard](running-the-analysis-services-deployment-wizard.md).  
  
     -or-  
  
-   Modify the \<*project name*>.configsettings file by using any text editor.  
  
## See Also  
 [Specifying the Installation Target](deployment-script-files-specifying-the-installation-target.md)   
 [Specifying Partition and Role Deployment Options](deployment-script-files-partition-and-role-deployment-options.md)   
 [Specifying Processing Options](deployment-script-files-specifying-processing-options.md)  
  
  
