---
title: "Create Package Configurations | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Integration Services packages, configurations"
  - "Package Configurations Organizer dialog box"
  - "SSIS packages, configurations"
  - "Integration Services packages, configurations"
  - "configurations [Integration Services]"
  - "packages [Integration Services], configurations"
  - "deploying packages [Integration Services], configurations"
ms.assetid: 91ac0347-f908-44f5-bd3d-115790223af4
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Create Package Configurations
  You create package configurations by using the **Package Configuration Organizer** dialog box and the Package Configuration Wizard. To access these tools, click **Package Configurations** on the **SSIS** menu in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
> [!NOTE]  
>  You can also access the **Package Configuration Organizer**, by clicking the ellipsis button next to the **Configuration** property. The Configuration property appears in the properties window for the package.  
  
> [!NOTE]  
>  Configurations are available for the package deployment model. Parameters are used in place of configurations for the project deployment model. The project deployment model enables you to deploy [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. For more information about the deployment models, see [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
 In the **Package Configuration Organizer** dialog box, you can enable packages to use configurations, add and delete configurations, and set the preferred order in which configurations should be loaded.  
  
> [!NOTE]  
>  When package configurations load in the preferred order, configurations load from the top of the list shown in the **Package Configuration Organizer** dialog box to the bottom of the list. However, at run time, package configurations might not load in the preferred order. In particular, parent package configurations load after configurations of other types.  
  
> [!NOTE]  
>  If multiple configurations set the same object property, the value loaded last is used at run time.  
  
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
  
> [!NOTE]  
>  The last page in the Package Configuration Wizard, Completing the Wizard, lists the target properties in the configuration. If you want to update properties when you run packages by using the **dtexec** command prompt utility, you can generate the strings that represent the property paths by running the Package Configuration Wizard and then copy and paste them into the command prompt window for use with the set option of **dtexec.**  
  
 The following table describes the columns in the configuration list in the **Package Configuration Organizer** dialog box.  
  
|Column|Description|  
|------------|-----------------|  
|**Configuration Name**|The name of the configuration.|  
|**Configuration Type**|The configuration type.|  
|**Configuration String**|The location of the configuration. The location can be a path, an environment variable, a Registry key, a parent package variable name, or a table in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database.|  
|**Target Object**|The name of the object with a property that has a configuration. If the configuration is an XML configuration file, the column is blank, because the configuration can update multiple objects.|  
|**Target Property**|The name of the property. If the configuration writes to an XML configuration file or a SQL Server table, the column is blank, because the configuration can update multiple objects.|  
  
### To create a package configuration  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Control Flow**, **Data Flow**, **Event Handler**, or **Package Explorer** tab.  
  
4.  On the **SSIS** menu, click **Package Configurations**.  
  
5.  In the **Package Configuration Organizer** dialog box, select **Enable package configurations**, and then click **Add**.  
  
6.  On the welcome page of the Package Configuration Wizard page, click **Next**.  
  
7.  On the Select Configuration Type page, specify the configuration type, and then set the properties that are relevant to the configuration type. For more information, see [Package Configuration Wizard UI Reference](../../2014/integration-services/package-configuration-wizard-ui-reference.md).  
  
8.  On the Select Properties to Export page, select the properties of package objects to include in the configuration. If the configuration type supports only one property, the title of this wizard page is Select Target Property. For more information, see [Package Configuration Wizard UI Reference](../../2014/integration-services/package-configuration-wizard-ui-reference.md).  
  
    > [!NOTE]  
    >  Only the **XML Configuration File** and **SQL Server** configuration types support including multiple properties in a configuration.  
  
9. On the Completing the Wizard page, type the name of the configuration, and then click **Finish**.  
  
10. View the configuration in the **Package Configuration Organizer** dialog box.  
  
11. Click **Close**.  
  
## External Resources  
  
-   Technical article, [Understanding Integration Services Package Configurations](https://go.microsoft.com/fwlink/?LinkId=165643), on msdn.microsoft.com  
  
-   Blog entry, [Creating packages in code - Package Configurations](https://go.microsoft.com/fwlink/?LinkId=217663), on www.sqlis.com.  
  
-   Blog entry, [API Sample - Programmatically add a configuration file to a package](https://go.microsoft.com/fwlink/?LinkId=217664), on blogs.msdn.com.  
  
## See Also  
 [Package Configurations](../../2014/integration-services/package-configurations.md)   
 [Package Deployment &#40;SSIS&#41;](packages/legacy-package-deployment-ssis.md)   
 [Working with Variables Programmatically](building-packages-programmatically/working-with-variables-programmatically.md)  
  
  
