---
title: "Legacy Package Deployment (SSIS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.packageconfigurationorganizer.f1"
  - "sql13.dts.configwizard.finishdtsconfiguration.f1"
  - "sql13.dts.configwizard.selectobjects.f1"
  - "sql13.dts.configwizard.selecconfigtype.f1"
  - "sql13.dts.configwizard.welcome.f1"
  - "sql13.dts.deploymentwizard.welcome.f1"
  - "sql13.dts.deploymentwizard.confirminstallation.f1"
  - "sql13.dts.deploymentwizard.deploydtspackages.f1"
  - "sql13.dts.deploymentwizard.finish.f1"
  - "sql13.dts.deploymentwizard.configurepackages.f1"
  - "sql13.dts.deploymentwizard.selectinstfolder.f1"
  - "sql13.dts.deploymentwizard.packagevalidation.f1"
  - "sql13.dts.deploymentwizard.specifytargetsqlserver.f1"
helpviewer_keywords: 
  - "Integration Services packages, deploying"
  - "deploying packages [Integration Services]"
  - "SQL Server Integration Services packages, deploying"
  - "deploying packages [Integration Services], about deploying packages"
  - "packages [Integration Services], deploying"
  - "SSIS packages, deploying"
ms.assetid: 0f5fc7be-e37e-4ecd-ba99-697c8ae3436f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Legacy Package Deployment (SSIS)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes tools and wizards that make it simple to deploy packages from the development computer to the production server or to other computers.  
  
 There are four steps in the package deployment process:  
  
1.  The first optional step is optional and involves creating package configurations that update properties of package elements at run time. The configurations are automatically included when you deploy the packages.  
  
2.  The second step is to build the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to create a package deployment utility. The deployment utility for the project contains the packages that you want to deploy  
  
3.  The third step is to copy the deployment folder that was created when you built the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to the target computer.  
  
4.  The fourth step is to run, on the target computer, the Package Installation Wizard to install the packages to the file system or to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

## Package Configurations
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides package configurations that you can use to update the values of properties at run time.  
  
> **NOTE:** Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx).   
  
 A configuration is a property/value pair that you add to a completed package. Typically, you create a package set properties on the package objects during package development, and then add the configuration to the package. When the package runs, it gets the new values of the property from the configuration. For example, by using a configuration, you can change the connection string of a connection manager, or update the value of a variable.  
  
 Package configurations provide the following benefits:  
  
-   Configurations make it easier to move packages from a development environment to a production environment. For example, a configuration can update the path of a source file, or change the name of a database or server.  
  
-   Configurations are useful when you deploy packages to many different servers. For example, a variable in the configuration for each deployed package can contain a different disk space value, and if the available disk space does not meet this value, the package does not run.  
  
-   Configurations make packages more flexible. For example, a configuration can update the value of a variable that is used in a property expression.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports several different methods of storing package configurations, such as XML files, tables in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and environment and package variables.  
  
 Each configuration is a property/value pair. The XML configuration file and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configuration types can include multiple configurations.  
  
 The configurations are included when you create a package deployment utility for installing packages. When you install the packages, the configurations can be updated as a step in the package installation.  
  
### Understanding How Package Configurations Are Applied at Run Time  
 When you use the **dtexec** command prompt utility (dtexec.exe) to run a deployed package, the utility applies package configurations twice. The utility applies configurations both before and after it applies the options that you specified on command line.  
  
 As the utility loads and runs the package, events occur in the following order:  
  
1.  The **dtexec** utility loads the package.  
  
2.  The utility applies the configurations that were specified in the package at design time and in the order that is specified in the package. (The one exception to this is the Parent Package Variables configurations. The utility applies these configurations only once and later in the process.)  
  
3.  The utility then applies any options that you specified on the command line.  
  
4.  The utility then reloads the configurations that were specified in the package at design time and in the order specified in the package. (Again, the exception to this rule is the Parent Package Variables configurations). The utility uses any command-line options that were specified to reload the configurations. Therefore, different values might be reloaded from a different location.  
  
5.  The utility applies the Parent Package Variable configurations.  
  
6.  The utility runs the package.  
  
 The way in which the **dtexec** utility applies configurations affects the following command-line options:  
  
-   You can use the **/Connection** or **/Set** option at run time to load package configurations from a location other than the location that you specified at design time.  
  
-   You can use the **/ConfigFile** option to load additional configurations that you did not specify at design time.  
  
 However, these command-line options do have some restrictions:  
  
-   You cannot use the **/Set** or the **/Connection** option to override single values that are also set by a configuration.  
  
-   You cannot use the **/ConfigFile** option to load configurations that replace the configurations that you specified at design time.  
  
 For more information about these options, and how the behavior of these options differs between [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] and earlier versions, see [Behavior Changes to Integration Services Features in SQL Server 2016](https://msdn.microsoft.com/library/611d22fa-5ac7-485e-9a40-7131e852f794).  
  
### Package Configuration Types  
 The following table describes the package configuration types.  
  
|Type|Description|  
|----------|-----------------|  
|XML configuration file|An XML file contains the configurations. The XML file can include multiple configurations.|  
|Environment variable|An environment variable contains the configuration.|  
|Registry entry|A Registry entry contains the configuration.|  
|Parent package variable|A variable in the package contains the configuration. This configuration type is typically used to update properties in child packages.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table|A table in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database contains the configuration. The table can include multiple configurations.|  
  
#### XML Configuration Files  
 If you select the **XML configuration file** configuration type, you can create a new configuration file, reuse an existing file and add new configurations, or reuse an existing file but overwrite existing file content.  
  
 An XML configuration file includes two sections:  
  
-   A heading that contains information about the configuration file. This element includes attributes such as when the file was created and the name of the person who generated the file.  
  
-   Configuration elements that contain information about each configuration. This element includes attributes such as the property path and the configured value of a property.  
  
 The following XML code demonstrates the syntax of an XML configuration file. This example shows a configuration for the Value property of an integer variable named `MyVar`.  
  
```xml
\<?xml version="1.0"?>  
<DTSConfiguration>  
   <DTSConfigurationHeading>  
      <DTSConfigurationFileInfo  
          GeneratedBy="DomainName\UserName"  
          GeneratedFromPackageName="Package"  
          GeneratedFromPackageID="{2AF06766-817A-4E28-9878-0DE37A150648}"  
          GeneratedDate="2/01/2005 5:58:09 PM"/>  
   </DTSConfigurationHeading>  
   <Configuration ConfiguredType="Property" Path="\Package.Variables[User::MyVar].Value" ValueType="Int32">  
      <ConfiguredValue>0</ConfiguredValue>  
   </Configuration>  
</DTSConfiguration>  
  
```  
  
#### Registry Entry  
 If you want to use a Registry entry to store the configuration, you can either use an existing key or create a new key in HKEY_CURRENT_USER. The Registry key that you use must have a value named **Value**. The value can be a DWORD or a string.  
  
 If you select the **Registry entry** configuration type, you type the name of the Registry key in the Registry entry box. The format is \<registry key>. If you want to use a Registry key that is not at the root of HKEY_CURRENT_USER, use the format \<Registry key\registry key\\...> to identify the key. For example, to use the MyPackage key located in SSISPackages, type **SSISPackages\MyPackage**.  
  
#### SQL Server  
 If you select the **SQL Server** configuration type, you specify the connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database in which you want to store the configurations. You can save the configurations to an existing table or create a new table in the specified database.  
  
 The following SQL statement shows the default CREATE TABLE statement that the Package Configuration Wizard provides.  
  
```sql
CREATE TABLE [dbo].[SSIS Configurations]  
(  
ConfigurationFilter NVARCHAR(255) NOT NULL,  
ConfiguredValue NVARCHAR(255) NULL,  
PackagePath NVARCHAR(255) NOT NULL,  
ConfiguredValueType NVARCHAR(20) NOT NULL  
)  
  
```  
  
 The name that you provide for the configuration is the value stored in the **ConfigurationFilter** column.  
  
### Direct and Indirect Configurations  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides direct and indirect configurations. If you specify configurations directly, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a direct link between the configuration item and the package object property. Direct configurations are a better choice when the location of the source does not change. For example, if you are sure that all deployments in the package use the same file path, you can specify an XML configuration file.  
  
 Indirect configurations use environment variables. Instead of specifying the configuration setting directly, the configuration points to an environment variable, which in turn contains the configuration value. Using indirect configurations is a better choice when the location of the configuration can change for each deployment of a package.  

## Create Package Configurations
  Create package configurations by using the **Package Configuration Organizer** dialog box and the Package Configuration Wizard. To access these tools, click **Package Configurations** on the **SSIS** menu in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
  
 **NOTES:**
> You can also access the **Package Configuration Organizer** by clicking the ellipsis button next to the **Configuration** property. The Configuration property appears in the properties window for the package.  
> 
> Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx).    
> 
> In the **Package Configuration Organizer** dialog box, you can enable packages to use configurations, add and delete configurations, and set the preferred order in which configurations should be loaded. 
> 
> When package configurations load in the preferred order, configurations load from the top of the list shown in the **Package Configuration Organizer** dialog box to the bottom of the list. However, at run time, package configurations might not load in the preferred order. In particular, parent package configurations load after configurations of other types.  
> 
> If multiple configurations set the same object property, the value loaded last is used at run time.  
  
 From the **Package Configuration Organizer** dialog box, you run the Package Configuration Wizard, which guides you through the steps to create a configuration. To run the Package Configuration Wizard, add a new configuration in the **Package Configurations Organizer** dialog box or edit an existing one. On the wizard pages, you choose the configuration type, select whether you want to access the configuration directly or use environment variables, and select the properties to save in the configuration.  
  
 The following example shows the target properties of a variable and a package as they appear on the Completing the Wizard page of the Package Configuration Wizard.:  
  
 \Package.Variables[User::TodaysDate].Properties[RaiseChangedEvent]  
  
 \Package.Properties[MaximumErrorCount]  
  
 \Package.Properties[LoggingMode]  
  
 \Package.Properties[LocaleID]  
  
 \Package\My SQL Task.Variables[User::varTableName].Properties[Value]  
  
 In this example, the configuration updates these properties:  
  
-   The RaiseChangedEvent property of user-defined variable, `TodaysDate`.  
  
-   The MaximumErrorCount, LoggingMode, and LocaleID properties of the package.  
  
-   The Value property of user-defined variable, `varTableName`, within scope of the task, My SQL Task.  
  
 The "\Package" represents the root, and periods (.) separate the objects that define the path to the property that the configuration updates. The names of variables and properties are enclosed in brackets. The term Package is always used in configuration, regardless of the package name; however, all other objects in the path use their user-defined names.  
  
 After the wizard finishes, the new configuration is added to the configuration list in the **Package Configuration Organizer** dialog box.  
  
> **NOTE:** The last page in the Package Configuration Wizard, Completing the Wizard, lists the target properties in the configuration. If you want to update properties when you run packages by using the **dtexec** command prompt utility, you can generate the strings that represent the property paths by running the Package Configuration Wizard and then copy and paste them into the command prompt window for use with the set option of **dtexec.**  
  
 The following table describes the columns in the configuration list in the **Package Configuration Organizer** dialog box.  
  
|Column|Description|  
|------------|-----------------|  
|**Configuration Name**|The name of the configuration.|  
|**Configuration Type**|The configuration type.|  
|**Configuration String**|The location of the configuration. The location can be a path, an environment variable, a Registry key, a parent package variable name, or a table in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**Target Object**|The name of the object with a property that has a configuration. If the configuration is an XML configuration file, the column is blank, because the configuration can update multiple objects.|  
|**Target Property**|The name of the property. If the configuration writes to an XML configuration file or a SQL Server table, the column is blank, because the configuration can update multiple objects.|  
  
### To create a package configuration  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Control Flow**, **Data Flow**, **Event Handler**, or **Package Explorer** tab.  
  
4.  On the **SSIS** menu, click **Package Configurations**.  
  
5.  In the **Package Configuration Organizer** dialog box, select **Enable package configurations**, and then click **Add**.  
  
6.  On the welcome page of the Package Configuration Wizard page, click **Next**.  
  
7.  On the Select Configuration Type page, specify the configuration type, and then set the properties that are relevant to the configuration type. For more information, see [Package Configuration Wizard UI Reference](../../integration-services/packages/package-configuration-wizard-ui-reference.md).  
  
8.  On the Select Properties to Export page, select the properties of package objects to include in the configuration. If the configuration type supports only one property, the title of this wizard page is Select Target Property. For more information, see [Package Configuration Wizard UI Reference](../../integration-services/packages/package-configuration-wizard-ui-reference.md).  
  
    > **NOTE:** Only the **XML Configuration File** and **SQL Server** configuration types support including multiple properties in a configuration.  
  
9. On the Completing the Wizard page, type the name of the configuration, and then click **Finish**.  
  
10. View the configuration in the **Package Configuration Organizer** dialog box.  
  
11. Click **Close**.  

## Package Configurations Organizer
  Use the **Package Configurations Organizer** dialog box to enable package configurations, view a list of configurations for the current package, and specify the preferred order in which the configurations should be loaded.  
  
> **NOTE:** Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx).    
  
 If multiple configurations update the same property, values from configurations listed lower in the configuration list will replace values from configurations higher in the list. The last value loaded into the property is the value that is used when the package runs. Also, if the package uses a combination of direct configuration such as an XML configuration file and an indirect configuration such as an environment variable, the indirect configuration that points to the location of the direct configuration must be higher in the list.  
  
> **NOTE:** When package configurations load in the preferred order, configurations load from the top of the list shown in the **Package Configuration Organizer** dialog box to the bottom of the list. However, at run time, package configurations might not load in the preferred order. In particular, Parent Package Configurations load after configurations of other types.  
  
 Package configurations update the values of properties of package objects at run time. When a package is loaded, the values from the configurations replace the values that were set when the package was developed. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports different configuration types. For example, you can use an XML file that can contain multiple configurations, or an environment variable that contains a single configuration. For more information, see [Package Configurations](../../integration-services/packages/package-configurations.md).  
  
### Options  
 **Enable package configurations**  
 Select to use configurations with the package.  
  
 **Configuration Name**  
 View the name of the configuration.  
  
 **Configuration Type**  
 View the type of the location where configurations are stored.  
  
 **Configuration String**  
 View the location where the configuration values are stored. The location can be the path of a file, the name of an environment variable, the name of a parent package variable, a Registry key, or the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
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

## Package Configuration Wizard UI Reference
  Use the **Package Configuration Wizard** to create configurations that update the properties of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package and its objects at run time. This wizard runs when you add a new configuration or modify an existing one in the **Package Configurations Organizer** dialog box. To open the **Package Configurations Organizer** dialog box, select **Package Configurations** on the **SSIS** menu in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For more information, see [Create Package Configurations](../../integration-services/packages/create-package-configurations.md).  
  
> **NOTE:** Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx).  
  
 The following sections describe pages of the Wizard.  
  
### Welcome to the Package Configuration Wizard Page  
 Use the **SSIS Configuration Wizard** to create configurations that update the properties of a package and its objects at run time.  
  
#### Options  
 **Don't show this page again**  
 Skip the welcome page the next time you open the wizard.  
  
 **Next**  
 Go the next page in the wizard.  
  
### Select Configuration Type Page  
 Use the **Select Configuration Type** page to specify the type of configuration to create.  
  
 If you need additional information to determine which type of configuration to use, see [Package Configurations](../../integration-services/packages/package-configurations.md).  
  
#### Static Options  
 **Configuration type**  
 Select the type of source in which to store the configuration, using the following options:  
  
|Value|Description|  
|-----------|-----------------|  
|**XML configuration file**|Store the configuration as an XML file. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**Environment variable**|Store the configuration in one of the environment variables. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**Registry entry**|Store the configuration in the Registry. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**Parent package variable**|Store the configuration as a variable in the package that contains the task.  Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
|**SQL Server**|Store the configuration in a table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Selecting this value displays the dynamic options in the section, **Configuration Type**.|  
  
 **Next**  
 View the next page in the wizard sequence.  
  
#### Dynamic Options  
  
##### Configuration Type Option = XML Configuration File  
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
  
##### Configuration Type Option = Environment Variable  
 **Environment variable**  
 Select the environment variable that contains the configuration information.  
  
##### Configuration Type Option = Registry Entry  
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
  
##### Configuration Type Option = Parent Package Variable  
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
  
##### Configuration Type Options = SQL Server  
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
  
### Select Objects to Export Page  
 Use the **Select Target Property or Select Properties to Export** page to specify the object properties that the configuration contains. The ability to select multiple properties is available only if you select the XML configuration type.  
  
#### Options  
 **Objects**  
 Expand the package hierarchy and select the properties to export.  
  
 **Property attributes**  
 View the attributes of a property.  
  
 **Next**  
 Go to the next page in the wizard.  
  
### Completing the Wizard Page  
 Use the **Completing the Wizard** page to provide a name for the configuration and view settings used by the wizard to create the configuration. After the wizard completes, the **Package Configurations Organizer** is displayed, which lists all configurations for the package.  
  
#### Options  
 **Configuration name**  
 Type the name of the configuration.  
  
 **Preview**  
 View the settings used by the wizard to create the configuration.  
  
 **Finish**  
 Create the configuration and exit the **Package Configuration Wizard**.  

## <a name="child"></a> Use the Values of Variables and Parameters in a Child Package
  This procedure describes how to create a package configuration that uses the parent variable configuration type. This configuration type enables a child package that is run from a parent package to access a variable in the parent.  
  
> [!NOTE]  
>  You can also pass values to a child package by configuring the Execute Package Task to map parent package variables or parameters, or project parameters, to child package parameters. For more information, see [Execute Package Task](../../integration-services/control-flow/execute-package-task.md).  
  
 It is not necessary to create the variable in the parent package before you create the package configuration in the child package. You can add the variable to the parent package at any time, but you must use the exact name of the parent variable in the package configuration. However, before you can create a parent variable configuration, there must be an existing variable in the child package that the configuration can update. For more information about adding and configuring variables, see [Add, Delete, Change Scope of User-Defined Variable in a Package](https://msdn.microsoft.com/library/cbf40c7f-3c8a-48cd-aefa-8b37faf8b40e).  
  
 The scope of the variable in the parent package that is used in a parent variable configuration can be set to the Execute Package task, to the container that has the task, or to the package. If multiple variables with the same name are defined in a package, the variable that is closest in scope to the Execute Package task is used. The closest scope to the Execute Package task is the task itself.  
  
### To add a variable to a parent package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package to which you want to add a variable to pass to a child package.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, to define the scope of the variable, do one of the following:  
  
    -   To set the scope to the package, click anywhere on the design surface of the **Control Flow** tab.  
  
    -   To set the scope to a parent container of the Execute Package task, click the container.  
  
    -   To set the scope to the Execute Package task, click the task.  
  
4.  Add and configure a variable.  
  
    > [!NOTE]  
    >  Select a data type that is compatible with the data that the variable will store.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To add a variable to a child package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package to which you want to add a parent variable configuration.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, to set the scope to the package, click anywhere on the design surface of the **Control Flow** tab.  
  
4.  Add and configure a variable.  
  
    > [!NOTE]  
    >  Select a data type that is compatible with the data that the variable will store.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  

## Create a Deployment Utility
  The first step in deploying packages is to create a deployment utility for an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project. The deployment utility is a folder that contains the files you need to deploy the packages in an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project on a different server. The deployment utility is created on the computer on which the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project is stored.  
  
 You create a package deployment utility for an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project by first configuring the build process to create a deployment utility, and then building the project. When you build the project, all packages and package configurations in the project are automatically included. To deploy additional files such as a Readme file with the project, place the files in the **Miscellaneous** folder of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project. When the project is built, these files are also automatically included.  
  
 You can configure each project deployment differently. Before you build the project and create the package deployment utility, you can set the properties on the deployment utility to customize the way the packages in the project will be deployed. For example, you can specify whether package configurations can be updated when the project is deployed. To access the properties of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, right-click the project and click **Properties**.  
  
 The following table lists the deployment utility properties.  
  
|Property|Description|  
|--------------|-----------------|  
|AllowConfigurationChange|A value that specifies whether configurations can be updated during deployment.|  
|CreateDeploymentUtility|A value that specifies whether a package deployment utility is created when the project is built. This property must be **True** to create a deployment utility.|  
|DeploymentOutputPath|The location, relative to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, of the deployment utility.|  
  
 When you build an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, a manifest file, \<project name>.SSISDeploymentManifest.xml, is created and added, together with copies of the project packages and package dependencies, to the bin\Deployment folder in the project, or to the location specified in the DeploymentOutputPath property. The manifest file lists the packages, the package configurations, and any miscellaneous files in the project.  
  
 The content of the deployment folder is refreshed every time that you build the project. This means that any file saved to this folder that is not copied to the folder again by the build process will be deleted. For example, package configuration files saved to the deployment folders will be deleted.  
  
### To create a package deployment utility  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the solution that contains the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project for which you want to create a package deployment utility.  
  
2.  Right-click the project and click **Properties**.  
  
3.  In the **\<project name> Property Pages** dialog box, click **Deployment Utility**.  
  
4.  To update package configurations when packages are deployed, set **AllowConfigurationChanges** to **True**.  
  
5.  Set **CreateDeploymentUtility** to **True**.  
  
6.  Optionally, update the location of the deployment utility by modifying the **DeploymentOutputPath** property.  
  
7.  Click **OK**.  
  
8.  In Solution Explorer, right-click the project, and then click **Build**.  
  
9. View the build progress and build errors in the **Output** window.  

## Deploy Packages by Using the Deployment Utility
  When you have built a deployment utility to install packages from an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project on a different computer than the one on which the deployment utility was built, you must first copy the deployment folder to the destination computer.  
  
 The path of the deployment folder is specified in the DeploymentOutputPath property of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project for which you created the deployment utility. The default path is bin\Deployment, relative to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project. For more information, see [Create a Deployment Utility](../../integration-services/packages/create-a-deployment-utility.md).  
  
 You use the Package Installation Wizard to install the packages. To launch the wizard, double-click the deployment utility file after you have copied the deployment folder to the server. This file is named \<project name>.SSISDeploymentManifest, and can be found in the deployment folder on the destination computer.  
  
> [!NOTE]  
>  Depending on the version of the package that you are deploying, you might encounter an error if you have different versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed side-by-side. This error can occur because the .SSISDeploymentManifest file name extension is the same for all versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Double-clicking the file calls the installer (dtsinstall.exe) for the most recently installed version of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], which might not be the same version as the deployment utility file. To work around this problem, run the correct version of dtsinstall.exe from the command line, and provide the path of the deployment utility file.  
  
 The Package Installation Wizard guides you through the steps to install packages to the file system or to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can configure the installation in the following ways:  
  
-   Choosing the location type and location to install the packages.  
  
-   Choosing location to install package dependencies.  
  
-   Validating the packages after they are installed on the target server.  
  
 The file-based dependencies for packages are always installed to the file system. If you install a package to the file system, the dependencies are installed in the same folder as the one that you specify for the package. If you install a package to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can specify the folder in which to store the file-based dependencies.  
  
 If the package includes configurations that you want to modify for use on the destination computer, you can update the values of the properties by using the wizard.  
  
 In addition to installing packages by using the Package Installation Wizard, you can copy and move packages by using the **dtutil** command prompt utility. For more information, see [dtutil Utility](../../integration-services/dtutil-utility.md).  
  
### To deploy packages to an instance of SQL Server  
  
1.  Open the deployment folder on the target computer.  
  
2.  Double-click the manifest file, \<project name>.SSISDeploymentManifest, to start the Package Installation Wizard.  
  
3.  On the **Deploy SSIS Packages** page, select the **SQL Server deployment** option.  
  
4.  Optionally, select **Validate packages after installation** to validate packages after they are installed on the target server.  
  
5.  On the **Specify Target SQL Server** page, specify the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to install the packages to and select an authentication mode. If you select [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must provide a user name and a password.  
  
6.  On the **Select Installation Folder** page, specify the folder in the file system for the package dependencies that will be installed.  
  
7.  If the package includes configurations, you can edit configurations by updating values in the **Value** list on the Configure Packages page.  
  
8.  If you elected to validate packages after installation, view the validation results of the deployed packages.  

## Redeployment of Packages
  After a project is deployed, you may need to update or extend package functionality and then redeploy the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the updated packages. As part of the process of redeploying packages, you should review the configuration properties that are included in the deployment utility. For example, you may not want to allow configuration changes after the package is redeployed.  
  
### Process for Redeployment  
 After you finish updating the packages, you rebuild the project, copy the deployment folder to the target computer, and then rerun the Package Installation Wizard.  
  
 If you update only a few packages in the project, you may not want to redeploy the entire project. To deploy only a few packages, you can create a new [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, add the updated packages to the new project, and then build and deploy the project. Package configurations are automatically copied with the package when you add the package to a different project.  

## Package Installation Wizard UI Reference
  Use the **Package Installation Wizard** to deploy a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, including the packages and miscellaneous files it contains and any package dependencies.  
  
 Before you deploy packages, you can create configurations and then deploy them with the packages. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses configurations to dynamically update properties of packages and package objects at run time. For example, the connection string of an OLE DB connection can be set dynamically at run time by providing a configuration that maps a value to the property that contains the connection string.  
  
 You cannot run the Package Installation Wizard until you build an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project and create a deployment utility. For more information, see [Deploy Packages by Using the Deployment Utility](../../integration-services/packages/deploy-packages-by-using-the-deployment-utility.md).  
  
 The following sections describe pages of the wizard.  
  
### Welcome to the Package Installation Wizard Page  
 Use the **Package Installation Wizard** to deploy an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project for which you built a package deployment utility.  
  
 **Do not show this starting page again**  
 Select to skip the starting page when you run the wizard again.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
### Configure Packages Page  
 Use the **Configure Packages** page to edit package configurations.  
  
#### Options  
 **Configuration file**  
 Edit the contents of a configuration file by selecting the file from the list.  
  
 **Related Topics:** [Create Package Configurations](../../integration-services/packages/create-package-configurations.md)  
  
 **Path**  
 View the path of the property to be configured.  
  
 **Type**  
 View the data type of the property.  
  
 **Value**  
 Specify the value of the configuration.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
### Confirm Installation Page  
 Use the **Confirm Installation** page to start the installation of packages, to view the status, and to view the information that the wizard will use to install files from the specified project.  
  
 **Next**  
 Install the packages and their dependencies and go to the next wizard page when installation is completed.  
  
 **Status**  
 Shows the progress of the package installation.  
  
 **Finish**  
 Go to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all the required options.  
  
### Deploy SSIS Packages Page  
 Use the **Deploy SSIS Packages** page to specify where to install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages and their dependencies.  
  
#### Options  
 **File system deployment**  
 Deploy packages and dependencies in a specified folder in the file system.  
  
 **SQL Server deployment**  
 Deploy packages and dependencies in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use this option if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shares packages between servers. Any package dependencies are installed in the specified folder in the file system.  
  
 **Validate packages after installation**  
 Indicate whether to validate packages after installation.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
### Packages Validation Page  
 Use the **Packages Validation** page to view the progress and results of the package validation.  
  
 **Next**  
 Go to the next page in the wizard.  
  
### Select Installation Folder Page  
 Use the **Select Installation Folder** page to specify the file system folder in which to install the packages and their dependencies.  
  
#### Options  
 **Folder**  
 Specify the path and folder in which to copy the package and its dependencies.  
  
 **Browse**  
 Browse to the target folder by using the **Browse For Folder** dialog box.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and if have specified all of the required options.  
  
### Specify Target SQL Server Page  
 Use the **Specify Target SQL Server** page to specify options for deploying the package to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
#### Options  
 **Server name**  
 Specify the name of the server to deploy the packages to.  
  
 **Use Windows Authentication**  
 Specify whether to use Windows Authentication to log on to the server. Windows Authentication is recommended for better security.  
  
 **Use SQL Server Authentication**  
 Specify whether the package should use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to log on to the server. If you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must provide a user name and password.  
  
 **User name**  
 Specify a user name.  
  
 **Password**  
 Specify a password.  
  
 **Package path**  
 Specify the name of the logical folder, or enter "/" for the default folder.  
  
 To select the folder in the **SSIS Package** dialog box, click browse (...). However, the dialog box does not provide a means to select the default folder. If you want to use the default folder, you have to enter "/" in the text box.  
  
> [!NOTE]  
>  If you do not enter a valid package path, the following error message appears: "One or more arguments are invalid."  
  
 **Rely on server storage for encryption**  
 Select to use security features of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to help secure the packages.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
### Finish the Package Installation Page  
 Use the **Finish the Package Installation Wizard** page to view a summary of the package installation results. This page provides details such as the name of the deployed [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, the packages that were installed, the configuration files, and the installation location.  
  
 **Finish**  
 Exit the wizard by clicking **Finish**.  

