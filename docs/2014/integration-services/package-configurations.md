---
title: "Package Configurations | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "package configuration syntax [Integration Services]"
  - "SQL Server Integration Services packages, configurations"
  - "SSIS packages, configurations"
  - "XML [Integration Services]"
  - "Integration Services packages, configurations"
  - "configuration syntax [Integration Services]"
  - "indirect configurations [Integration Services]"
  - "configurations [Integration Services]"
  - "direct configurations [Integration Services]"
  - "packages [Integration Services], configurations"
ms.assetid: d20e0311-1fc9-4ddc-a381-6d127cf11b69
author: janinezhang
ms.author: janinez
manager: craigg
---
# Package Configurations
  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides package configurations that you can use to update the values of properties at run time.  
  
> [!NOTE]  
>  Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
 A configuration is a property/value pair that you add to a completed package. Typically, you create a package set properties on the package objects during package development, and then add the configuration to the package. When the package runs, it gets the new values of the property from the configuration. For example, by using a configuration, you can change the connection string of a connection manager, or update the value of a variable.  
  
 Package configurations provide the following benefits:  
  
-   Configurations make it easier to move packages from a development environment to a production environment. For example, a configuration can update the path of a source file, or change the name of a database or server.  
  
-   Configurations are useful when you deploy packages to many different servers. For example, a variable in the configuration for each deployed package can contain a different disk space value, and if the available disk space does not meet this value, the package does not run.  
  
-   Configurations make packages more flexible. For example, a configuration can update the value of a variable that is used in a property expression.  
  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] supports several different methods of storing package configurations, such as XML files, tables in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database, and environment and package variables.  
  
 Each configuration is a property/value pair. The XML configuration file and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] configuration types can include multiple configurations.  
  
 The configurations are included when you create a package deployment utility for installing packages. When you install the packages, the configurations can be updated as a step in the package installation.  
  
## Understanding How Package Configurations Are Applied at Run Time  
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
  
 For more information about these options, and how the behavior of these options differs between [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] and earlier versions, see [Behavior Changes to Integration Services Features in SQL Server 2014](../../2014/integration-services/behavior-changes-to-integration-services-features-in-sql-server-2014.md).  
  
## Package Configuration Types  
 The following table describes the package configuration types.  
  
|Type|Description|  
|----------|-----------------|  
|XML configuration file|An XML file contains the configurations. The XML file can include multiple configurations.|  
|Environment variable|An environment variable contains the configuration.|  
|Registry entry|A Registry entry contains the configuration.|  
|Parent package variable|A variable in the package contains the configuration. This configuration type is typically used to update properties in child packages.|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] table|A table in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database contains the configuration. The table can include multiple configurations.|  
  
### XML Configuration Files  
 If you select the **XML configuration file** configuration type, you can create a new configuration file, reuse an existing file and add new configurations, or reuse an existing file but overwrite existing file content.  
  
 An XML configuration file includes two sections:  
  
-   A heading that contains information about the configuration file. This element includes attributes such as when the file was created and the name of the person who generated the file.  
  
-   Configuration elements that contain information about each configuration. This element includes attributes such as the property path and the configured value of a property.  
  
 The following XML code demonstrates the syntax of an XML configuration file. This example shows a configuration for the Value property of an integer variable named `MyVar`.  
  
```  
<?xml version="1.0"?>  
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
  
### Registry Entry  
 If you want to use a Registry entry to store the configuration, you can either use an existing key or create a new key in HKEY_CURRENT_USER. The Registry key that you use must have a value named `Value`. The value can be a DWORD or a string.  
  
 If you select the **Registry entry** configuration type, you type the name of the Registry key in the Registry entry box. The format is \<registry key>. If you want to use a Registry key that is not at the root of HKEY_CURRENT_USER, use the format \<Registry key\registry key\\...> to identify the key. For example, to use the MyPackage key located in SSISPackages, type `SSISPackages\MyPackage`.  
  
### SQL Server  
 If you select the **SQL Server** configuration type, you specify the connection to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database in which you want to store the configurations. You can save the configurations to an existing table or create a new table in the specified database.  
  
 The following SQL statement shows the default CREATE TABLE statement that the Package Configuration Wizard provides.  
  
```  
CREATE TABLE [dbo].[SSIS Configurations]  
(  
ConfigurationFilter NVARCHAR(255) NOT NULL,  
ConfiguredValue NVARCHAR(255) NULL,  
PackagePath NVARCHAR(255) NOT NULL,  
ConfiguredValueType NVARCHAR(20) NOT NULL  
)  
  
```  
  
 The name that you provide for the configuration is the value stored in the **ConfigurationFilter** column.  
  
## Direct and Indirect Configurations  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides direct and indirect configurations. If you specify configurations directly, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] creates a direct link between the configuration item and the package object property. Direct configurations are a better choice when the location of the source does not change. For example, if you are sure that all deployments in the package use the same file path, you can specify an XML configuration file.  
  
 Indirect configurations use environment variables. Instead of specifying the configuration setting directly, the configuration points to an environment variable, which in turn contains the configuration value. Using indirect configurations is a better choice when the location of the configuration can change for each deployment of a package.  
  
## Related Tasks  
 [Create Package Configurations](../../2014/integration-services/create-package-configurations.md)  
  
## Related Content  
  
-   Technical article, [Understanding Integration Services Package Configurations](https://go.microsoft.com/fwlink/?LinkId=165643), on msdn.microsoft.com  
  
-   Blog entry, [Creating packages in code - Package Configurations](https://go.microsoft.com/fwlink/?LinkId=217663), on www.sqlis.com.  
  
-   Blog entry, [API Sample - Programmatically add a configuration file to a package](https://go.microsoft.com/fwlink/?LinkId=217664), on blogs.msdn.com.  
  
  
