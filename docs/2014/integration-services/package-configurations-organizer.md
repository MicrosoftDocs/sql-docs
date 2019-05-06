---
title: "Package Configurations Organizer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.packageconfigurationorganizer.f1"
helpviewer_keywords: 
  - "Package Configurations Organizer dialog box"
ms.assetid: f20ae6cb-9e6a-4d24-88ff-d7a903a4e8d3
author: janinezhang
ms.author: janinez
manager: craigg
---
# Package Configurations Organizer
  Use the **Package Configurations Organizer** dialog box to enable package configurations, view a list of configurations for the current package, and specify the preferred order in which the configurations should be loaded.  
  
> [!NOTE]  
>  Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
 If multiple configurations update the same property, values from configurations listed lower in the configuration list will replace values from configurations higher in the list. The last value loaded into the property is the value that is used when the package runs. Also, if the package uses a combination of direct configuration such as an XML configuration file and an indirect configuration such as an environment variable, the indirect configuration that points to the location of the direct configuration must be higher in the list.  
  
> [!NOTE]  
>  When package configurations load in the preferred order, configurations load from the top of the list shown in the **Package Configuration Organizer** dialog box to the bottom of the list. However, at run time, package configurations might not load in the preferred order. In particular, Parent Package Configurations load after configurations of other types.  
  
 Package configurations update the values of properties of package objects at run time. When a package is loaded, the values from the configurations replace the values that were set when the package was developed. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] supports different configuration types. For example, you can use an XML file that can contain multiple configurations, or an environment variable that contains a single configuration. For more information, see [Package Configurations](../../2014/integration-services/package-configurations.md).  
  
## Options  
 **Enable package configurations**  
 Select to use configurations with the package.  
  
 **Configuration Name**  
 View the name of the configuration.  
  
 **Configuration Type**  
 View the type of the location where configurations are stored.  
  
 **Configuration String**  
 View the location where the configuration values are stored. The location can be the path of a file, the name of an environment variable, the name of a parent package variable, a Registry key, or the name of a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] table.  
  
 **Target Object**  
 View the name of the object that the configuration updates. If the configuration is an XML configuration file or a SQL Server table, the column is blank because the configuration can include multiple objects.  
  
 **Target Property**  
 View the name of the property modified by the configuration. This column is blank if the configuration type supports multiple configurations.  
  
 **Add**  
 Add a configuration by using the Package Configuration Wizard.  
  
 **Edit**  
 Edit an existing configuration by rerunning the Package Configuration Wizard.  
  
 **Remove**  
 Select a configuration, and then click **Remove**.  
  
 **Arrows**  
 Select a configuration and use the up and down arrows to move it up or down in the list. Configurations are loaded in the sequence in which they appear in the list.  
  
## See Also  
 [Create Package Configurations](../../2014/integration-services/create-package-configurations.md)  
  
  
