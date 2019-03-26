---
title: "Execute Package Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.ssms.ispackageexecute.f1"
  - "sql12.ssis.ssms.executepackage.f1"
ms.assetid: 4f7a806d-4867-4d1f-bc65-b00c1caee7b6
author: janinezhang
ms.author: janinez
manager: craigg
---
# Execute Package Dialog Box
  Use the **Execute Package** dialog box to run a package that is stored on the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server.  
  
 An [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package may contain parameters that values stored in environment variables. Before executing such a package, you must specify which environment will be used to provide the environment variable values. A project may contain multiple environments, but only one environment can be used for binding environment variable values at the time of execution. If no environment variables are used in the package, an environment is not required.  
  
 What do you want to do?  
  
-   [Open the Execute Package dialog box](#open_dialog)  
  
-   [Set the Options on the General page](#general)  
  
-   [Set the Options on the Parameters tab](#parameters)  
  
-   [Set the Options on the Connection Managers tab](#connection)  
  
-   [Set the Options on the Advanced tab](#advanced)  
  
-   [Scripting the Options in the Execute Package Dialog Box](#script)  
  
##  <a name="open_dialog"></a> Open the Execute Package dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server.  
  
     You're connecting to the instance of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] that hosts the SSISDB database.  
  
2.  In Object Explorer, expand the tree to display the **Integration Services Catalogs** node.  
  
3.  Expand the **SSISDB** node.  
  
4.  Expand the folder that contains the package you want to run.  
  
5.  Right-click the package, and then click **Execute**.  
  
##  <a name="general"></a> Set the Options on the General page  
 Select **Environment** to specify the environment that is applied with the package is run.  
  
##  <a name="parameters"></a> Set the Options on the Parameters tab  
 Use the **Parameters** tab to modify the parameter values that are used when the package runs.  
  
##  <a name="connection"></a> Set the Options on the Connection Managers tab  
 Use the Connection Managers tab to set the properties of the package connection manager(s).  
  
##  <a name="advanced"></a> Set the Options on the Advanced tab  
 Use the Advanced tab to manage properties and other package settings.  
  
 **Add**, **Edit**, **Remove**  
 Click to add, edit, or remove a property.  
  
 **Logging level**  
 Select the logging level for the package execution. For more information, see [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database).  
  
 **Dump on errors**  
 Specify whether a dump file is created when errors occur during the package execution. For more information, see [Generating Dump Files for Package Execution](troubleshooting/generating-dump-files-for-package-execution.md).  
  
 **32-bit runtime**  
 Specify that the package will execute on a 32-bit system.  
  
##  <a name="script"></a> Scripting the Options in the Execute Package Dialog Box  
 While you are in the **Execute Package** dialog box, you can also use the **Script** button on the toolbar to write [!INCLUDE[tsql](../includes/tsql-md.md)] code for you. The generated script calls the stored procedures [catalog.start_execution &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database) with the same options that you have selected in the **Execute Package** dialog box. The script appears in a new script window in [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].  
  
  
