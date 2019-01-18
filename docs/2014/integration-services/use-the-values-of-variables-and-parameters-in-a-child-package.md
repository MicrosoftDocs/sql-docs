---
title: "Use the Values of Variables and Parameters in a Child Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "variables [Integration Services], passing between packages"
  - "configurations [Integration Services]"
  - "packages [Integration Services], configurations"
  - "variables [Integration Services], adding"
ms.assetid: 9b939edb-4e17-48e5-8428-855beb10049c
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Use the Values of Variables and Parameters in a Child Package
  This procedure describes how to create a package configuration that uses the parent variable configuration type. This configuration type enables a child package that is run from a parent package to access a variable in the parent.  
  
> [!NOTE]  
>  You can also pass values to a child package by configuring the Execute Package Task to map parent package variables or parameters, or project parameters, to child package parameters. For more information, see [Execute Package Task](control-flow/execute-package-task.md).  
  
 It is not necessary to create the variable in the parent package before you create the package configuration in the child package. You can add the variable to the parent package at any time, but you must use the exact name of the parent variable in the package configuration. However, before you can create a parent variable configuration, there must be an existing variable in the child package that the configuration can update. For more information about adding and configuring variables, see [Add, Delete, Change Scope of User-Defined Variable in a Package](../../2014/integration-services/add-delete-change-scope-of-user-defined-variable-in-a-package.md).  
  
 The scope of the variable in the parent package that is used in a parent variable configuration can be set to the Execute Package task, to the container that has the task, or to the package. If multiple variables with the same name are defined in a package, the variable that is closest in scope to the Execute Package task is used. The closest scope to the Execute Package task is the task itself.  
  
### To add a variable to a parent package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package to which you want to add a variable to pass to a child package.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, to define the scope of the variable, do one of the following:  
  
    -   To set the scope to the package, click anywhere on the design surface of the **Control Flow** tab.  
  
    -   To set the scope to a parent container of the Execute Package task, click the container.  
  
    -   To set the scope to the Execute Package task, click the task.  
  
4.  Add and configure a variable.  
  
    > [!NOTE]  
    >  Select a data type that is compatible with the data that the variable will store.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To add a variable to a child package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package to which you want to add a parent variable configuration.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, to set the scope to the package, click anywhere on the design surface of the **Control Flow** tab.  
  
4.  Add and configure a variable.  
  
    > [!NOTE]  
    >  Select a data type that is compatible with the data that the variable will store.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To add a parent package configuration to a child package  
  
1.  If it is not already open, open the child package in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
2.  Click anywhere on the design surface of the **Control Flow** tab.  
  
3.  On the **SSIS** menu, click **Package Configurations**.  
  
4.  In the **Package Configuration Organizer** dialog box, select **Enable package configuration**, and then click **Add**.  
  
5.  On the welcome page of the Package Configuration Wizard, click **Next.**  
  
6.  On the Select Configuration Type page, in the **Configuration type** list, select **Parent package variable** and do one of the following:  
  
    -   Select **Specify configuration settings directly**, and then in the **Parent variable** box, provide the name of the variable in the parent package to use in the configuration.  
  
        > [!IMPORTANT]  
        >  Variable names are case sensitive.  
  
    -   Select or **Configuration location is stored in an environment variable,** and then in the **Environment variable list**, select the environmentvariable that contains the name of the variable.  
  
7.  Click **Next**.  
  
8.  On the Select Target Property page, expand the **Variable** node, and expand the **Properties** node of the variable to configure, and then click the property to be set by the configuration.  
  
9. Click **Next**.  
  
10. On the Completing the Wizard page, optionally, modify the default name of the configuration and review the configuration information.  
  
11. Click **Finish** to complete the wizard and return to the **Package Configuration Organizer** dialog box.  
  
12. In the **Package Configuration Organizer** dialog box, the **Configuration** box lists the new configuration.  
  
13. Click **Close**.  
  
## See Also  
 [Package Configurations](../../2014/integration-services/package-configurations.md)   
 [Create Package Configurations](../../2014/integration-services/create-package-configurations.md)   
 [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)   
 [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)  
  
  
