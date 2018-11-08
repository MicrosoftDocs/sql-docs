---
title: "Package Configuration Wizard UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.configwizard.selectobjects.f1"
  - "sql12.dts.configwizard.selecconfigtype.f1"
  - "sql12.dts.configwizard.finishdtsconfiguration.f1"
  - "sql12.dts.configwizard.welcome.f1"
ms.assetid: adca6938-6d5a-40ec-950e-dceb79d044fe
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Package Configuration Wizard UI Reference
  Use the **Package Configuration Wizard** to create configurations that update the properties of an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package and its objects at run time. This wizard runs when you add a new configuration or modify an existing one in the **Package Configurations Organizer** dialog box. To open the **Package Configurations Organizer** dialog box, select **Package Configurations** on the **SSIS** menu in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. For more information, see [Create Package Configurations](../../2014/integration-services/create-package-configurations.md).  
  
> [!NOTE]  
>  Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
 The following sections describe pages of the Wizard.  
  
## Welcome to the Package Configuration Wizard Page  
 Use the **SSIS Configuration Wizard** to create configurations that update the properties of a package and its objects at run time.  
  
### Options  
 **Don't show this page again**  
 Skip the welcome page the next time you open the wizard.  
  
 **Next**  
 Go the next page in the wizard.  
  
## Select Configuration Type Page  
 Use the **Select Configuration Type** page to specify the type of configuration to create.  
  
 If you need additional information to determine which type of configuration to use, see [Package Configurations](../../2014/integration-services/package-configurations.md).  
  
### Static Options  
 **Configuration type**  
 Select the type of source in which to store the configuration, using the following options:  
  
|Value|Description|  
|-----------|-----------------|  
|**XML configuration file**|Store the configuration as an XML file. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**Environment variable**|Store the configuration in one of the environment variables. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**Registry entry**|Store the configuration in the Registry. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**Parent package variable**|Store the configuration as a variable in the package that contains the task.  Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**SQL Server**|Store the configuration in a table in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
  
 **Next**  
 View the next page in the wizard sequence.  
  
### Dynamic Options  
  
#### Configuration Type Option = XML Configuration File  
 **Specify configuration settings directly**  
 Use to specify settings directly.  
  
|Value|Description|  
|-----------|-----------------|  
|**Configuration file name**|Type the path of the configuration file that the wizard generates.|  
|**Browse**|Use the **Select Configuration File Location** dialog box to specify the path of the configuration file that the wizard generates. If the file does not exist, it is created by the wizard.|  
  
 **Configuration location is stored in an environment variable**  
 Use to specify the environment variable in which to store the configuration.  
  
|Value|Description|  
|-----------|-----------------|  
|**Environment variable**|Select an environment variable from the list.|  
  
#### Configuration Type Option = Environment Variable  
 **Environment variable**  
 Select the environment variable that contains the configuration information.  
  
#### Configuration Type Option = Registry Entry  
 **Specify configuration settings directly**  
 Use to specify settings directly.  
  
|Value|Description|  
|-----------|-----------------|  
|**Registry entry**|Type the Registry key that contains the configuration information. The format is \<registry key>.<br /><br /> The Registry key must already exist in HKEY_CURRENT_USER and have a value named Value. The value can be a DWORD or a string.<br /><br /> If you want to use a Registry key is not at the root of HKEY_CURRENT_USER, use the format \<Registry key\registry key\\...> to identify the key.|  
  
 **Configuration location is stored in an environment variable**  
 Use to specify the environment variable to store the configuration in.  
  
|Value|Description|  
|-----------|-----------------|  
|**Environment variable**|Select an environment variable from the list.|  
  
#### Configuration Type Option = Parent Package Variable  
 **Specify configuration settings directly**  
 Use to specify settings directly.  
  
|Value|Description|  
|-----------|-----------------|  
|**Parent variable**|Specify the variable in the parent package that contains the configuration information.|  
  
 **Configuration location is stored in an environment variable**  
 Use to specify the environment variable that stores the configuration.  
  
|Value|Description|  
|-----------|-----------------|  
|**Environment variable**|Select an environment variable from the list.|  
  
#### Configuration Type Options = SQL Server  
 **Specify configuration settings directly**  
 Use to specify settings directly.  
  
|Value|Description|  
|-----------|-----------------|  
|**Connection**|Select a connection from the list, or click **New** to create a new connection.|  
|**Configuration table**|Select an existing table, or click **New** to write a SQL statement that creates a new table.|  
|**Configuration filter**|Select an existing configuration name or type a new name.<br /><br /> Many SQL Server configurations can be stored in the same table, and each configuration can include multiple configuration items.<br /><br /> This user-defined value is stored in the table to identify configuration items that belong to a particular configuration|  
  
 **Configuration location is stored in an environment variable**  
 Use to specify the environment variable where the configuration is stored.  
  
|Value|Description|  
|-----------|-----------------|  
|**Environment variable**|Select an environment variable from the list.|  
  
## Select Objects to Export Page  
 Use the **Select Target Property or Select Properties to Export** page to specify the object properties that the configuration contains. The ability to select multiple properties is available only if you select the XML configuration type.  
  
### Options  
 **Objects**  
 Expand the package hierarchy and select the properties to export.  
  
 **Property attributes**  
 View the attributes of a property.  
  
 **Next**  
 Go to the next page in the wizard.  
  
## Completing the Wizard Page  
 Use the **Completing the Wizard** page to provide a name for the configuration and view settings used by the wizard to create the configuration. After the wizard completes, the **Package Configurations Organizer** is displayed, which lists all configurations for the package.  
  
### Options  
 **Configuration name**  
 Type the name of the configuration.  
  
 **Preview**  
 View the settings used by the wizard to create the configuration.  
  
 **Finish**  
 Create the configuration and exit the **Package Configuration Wizard**.  
  
## See Also  
 [Create Package Configurations](../../2014/integration-services/create-package-configurations.md)  
  
  
