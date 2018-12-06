---
title: "Specifying Partition and Role Deployment Options | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Deployment Script Files - Partition and Role Deployment Options
[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard reads the partition and role deployment options from the \<*project name*>.deploymentoptions file. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] creates this file when you build the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] uses the partition and role deployment options of the current project when the \<*project name*>.deploymentoptions file is created. For more information about configuration settings, see [Understanding the Input Files Used to Create the Deployment Script](../../analysis-services/multidimensional-models/deployment-script-files-input-used-to-create-deployment-script.md).  
  
## Reviewing the Partition and Role Deployment Options  
 The deployment options in the \<*project name*>.deploymentoptions file include the following:  
  
 **Partition deployment options**  
 The \<*project name*>.deploymentoptions file specifies whether existing partitions in the destination database are retained or overwritten (default). If existing partitions are retained, only new partitions will be deployed, and the partitions and aggregation designs on all existing measure groups are left unchanged.  
  
> [!NOTE]  
>  If the measure group in which the partition exists is deleted, the partition is automatically deleted.  
  
 **Role deployment options**  
 The \<*project name*>.deploymentoptions file specifies one of the following role deployment options:  
  
-   Existing roles and role members in the destination database are retained, and only new roles and role members are deployed.  
  
-   All existing roles and members in the destination database are replaced by the roles and members being deployed.  
  
-   Existing roles and role members in the destination database are retained, and no new roles are deployed.  
  
-   **Note** When existing roles and members are retained, the permissions associated with those roles are reset to none. Security permissions are contained by the objects they secure, not by the security roles with which they are associated. For more information about how to work with this behavior by using the Analysis Service Deployment Wizard, see 'Retain Roles and Members' in the Microsoft Knowledge Base.  
  
## Modifying the Partition and Role Deployment Options  
 You may have to deploy the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project using different partition and role options than those stored in the \<*project name*>.deploymentoptions file. For example, you may want to retain existing partitions, roles, and role members, instead of replacing all existing partitions, roles, and members as indicated in the \<*project name*>.deploymentoptions file.  
  
 To modify the deployment of partitions and roles in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, you cannot change the partition and roles settings within the project because the *\<project name>* **Properties Pages** dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] does not display these options. If you want to change the deployment options for roles and partitions, you must change this information within the \<*project name*>.deploymentoptions file itself. The following procedure describes how to change the partition and role deployment options within the \<*project name*>.deploymentoptions file.  
  
#### To change the deployment of partitions or roles after the input files have been generated  
  
-   Run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard interactively, and on the **Partition and Role Deployment Options** page, specify new deployment options for the partitions and roles.  
  
     -or-  
  
-   Run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard at the command prompt, and set the wizard to run in answer file mode. (For more information about answer file mode, see [Running the Analysis Services Deployment Wizard](../../analysis-services/multidimensional-models/running-the-analysis-services-deployment-wizard.md).)  
  
     -or-  
  
-   Open the \<*project name*>.deploymentoptions in any text editor and manually change the options. The options for PartitionDeployment are DeployPartitions, RetainPartitions. The options for RoleDeployment are DeployRolesAndMembers, DeployRolesRetainMembers, RetainRoles.
  
## See Also  
 [Specifying the Installation Target](../../analysis-services/multidimensional-models/deployment-script-files-specifying-the-installation-target.md)   
 [Specifying Configuration Settings for Solution Deployment](../../analysis-services/multidimensional-models/deployment-script-files-solution-deployment-config-settings.md)   
 [Specifying Processing Options](../../analysis-services/multidimensional-models/deployment-script-files-specifying-processing-options.md)  
  
  
