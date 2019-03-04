---
title: "dtexec Utility | Microsoft Docs"
ms.custom: ""
ms.date: "08/26/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 7b6867fa-1039-49b3-90fb-85b84678a612
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# dtexec Utility
  The **dtexec** command prompt utility is used to configure and execute [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. The **dtexec** utility provides access to all the package configuration and execution features, such as parameters, connections, properties, variables, logging, and progress indicators. The **dtexec** utility lets you load packages from these sources: the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, an .ispac project file, a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store, and the file system.  
  
> **NOTE:** When you use the current version of the **dtexec** utility to run a package created by an earlier version of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the utility temporarily upgrades the package to the current package format. However, you cannot use the **dtexec** utility to save the upgraded package. For more information about how to permanently upgrade a package to the current version, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md).  
  
 This topic includes the following sections:  
  
-   [Integration Services Server and Project File](#server)  
  
-   [Installation Considerations on 64-bit Computers](#bit)  
  
-   [Considerations on Computers with Side-by-Side Installations](#side)  
  
-   [Phases of Execution](#phases)  
  
-   [Exit Codes Returned](#exit)  
  
-   [Syntax Rules](#syntaxRules)  
  
-   [Using dtexec from the xp_cmdshell](#cmdshell)  

-   [Using dtexec from Bash](#bash)
  
-   [Syntax](#syntax)  
  
-   [Parameters](#parameter)  
  
-   [Remarks](#remark)  
  
-   [Examples](#example)  
  
##  <a name="server"></a> Integration Services Server and Project File  
 When you use **dtexec** to run packages on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, **dtexec** calls the [catalog.create_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md), [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md) and [catalog.start_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database.md) stored procedures to create an execution, set parameter values and start the execution. All execution logs can be seen from the server in the related views or by using standard reports available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information about the reports, see [Reports for the Integration Services Server](../../integration-services/performance/monitor-running-packages-and-other-operations.md#reports).  
  
 The following is an example of executing a package on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
```  
DTExec /ISSERVER "\SSISDB\folderB\Integration Services Project17\Package.dtsx" /SERVER "." /Envreference 2 /Par "$Project::ProjectParameter(Int32)";1 /Par "Parameter(Int32)";21 /Par "CM.sqlcldb2.SSIS_repro.InitialCatalog";ssisdb /Par "$ServerOption::SYNCHRONIZED(Boolean)";True  
```  
  
 When you use **dtexec** to run a package from the .ispac project file, the related options are: /Proj[ect] and /Pack[age] that are used to specify the project path and package stream name. When you convert a project to the project deployment model by running the **Integration Services Project Conversion Wizard** from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the wizard generates an .ispac projec file. For more information, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
 You can use **dtexec** with third-party scheduling tools to schedule packages that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
##  <a name="bit"></a> Installation Considerations on 64-bit Computers  
 On a 64-bit computer, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installs a 64-bit version of the **dtexec** utility (dtexec.exe). If you have to run certain packages in 32-bit mode, you will have to install the 32-bit version of the **dtexec** utility. To install the 32-bit version of the **dtexec** utility, you must select either Client Tools or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] during setup.  
  
 By default, a 64-bit computer that has both the 64-bit and 32-bit versions of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] command prompt utility installed will run the 32-bit version at the command prompt. The 32-bit version runs because the directory path for the 32-bit version appears in the PATH environment variable before the directory path for the 64-bit version. (Typically, the 32-bit directory path is *\<drive>*:\Program Files(x86)\Microsoft SQL Server\110\DTS\Binn, while the 64-bit directory path is *\<drive>*:\Program Files\Microsoft SQL Server\110\DTS\Binn.)  
  
> **NOTE:** If you use SQL Server Agent to run the utility, SQL Server Agent automatically uses the 64-bit version of the utility. SQL Server Agent uses the registry, not the PATH environment variable, to locate the correct executable for the utility.  
  
 To ensure that you run the 64-bit version of the utility at the command prompt, you can take one of the following actions:  
  
-   Open a Command Prompt window, change to the directory that contains the 64-bit version of the utility (*\<drive>*:\Program Files\Microsoft SQL Server\110\DTS\Binn), and then run the utility from that location.  
  
-   At the command prompt, run the utility by entering the full path (*\<drive>*:\Program Files\Microsoft SQL Server\110\DTS\Binn) to the 64-bit version of the utility.  
  
-   Permanently change the order of the paths in the PATH environment variable by placing the 64-bit path (*\<drive>*:\Program Files\Microsoft SQL Server\110\DTS\Binn) before the 32-bit path (*\<drive>*:\ Program Files(x86)\Microsoft SQL Server\110\DTS\Binn) in the variable.  
  
##  <a name="side"></a> Considerations on Computers with Side-by-Side Installations  
 When [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] is installed on a machine that has [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)] or [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] installed, multiple versions of the **dtexec** utility are installed.  
  
 To ensure that you run the correct version of the utility, at the command prompt run the utility by entering the full path (*\<drive>*:\Program Files\Microsoft SQL Server\\<version\>\DTS\Binn).  
  
##  <a name="phases"></a> Phases of Execution  
 The utility has four phases that it proceeds through as it executes. The phases are as follows:  
  
1.  Command sourcing phase: The command prompt reads the list of options and arguments that have been specified. All subsequent phases are skipped if a **/?** or **/HELP** option is encountered.  
  
2.  Package load phase: The package specified by the **/SQL**, **/FILE**, or **/DTS** option is loaded.  
  
3.  Configuration phase: Options are processed in this order:  
  
    -   Options that set package flags, variables, and properties.  
  
    -   Options that verify the package version and build.  
  
    -   Options that configure the run-time behavior of the utility, such as reporting.  
  
4.  Validation and execution phase: The package is run, or validated without running if the **/VALIDATE** option was specified.  
  
##  <a name="exit"></a> Exit Codes Returned  
 **Exit codes returned from dtexec utility**  
  
 When a package runs, **dtexec** can return an exit code. The exit code is used to populate the ERRORLEVEL variable, the value of which can then be tested in conditional statements or branching logic within a batch file. The following table lists the values that the **dtexec** utility can set when exiting.  
  
|Value|Description|  
|-----------|-----------------|  
|0|The package executed successfully.|  
|1|The package failed.|  
|3|The package was canceled by the user.|  
|4|The utility was unable to locate the requested package. The package could not be found.|  
|5|The utility was unable to load the requested package. The package could not be loaded.|  
|6|The utility encountered an internal error of syntactic or semantic errors in the command line.|  
  
##  <a name="syntaxRules"></a> Syntax Rules  
 **Utility syntax rules**  
  
 All options must start with a slash (/) or a minus sign (-). The options that are shown here start with a slash (/), but the minus sign (-) can be substituted.  
  
 An argument must be enclosed in quotation marks if it contains a space. If the argument is not enclosed in quotation marks, the argument cannot contain white space.  
  
 Doubled quotation marks within quoted strings represent escaped single quotation marks.  
  
 Options and arguments are not case-sensitive, except for passwords.  
  
##  <a name="cmdshell"></a> Using dtexec from the xp_cmdshell  
 **Using dtexec from the xp_cmdshell**  
  
 You can run dtexec from the **xp_cmdshell** prompt. The following example shows how to run a package called UpsertData.dtsx and ignore the return code:  
  
```  
EXEC xp_cmdshell 'dtexec /f "C:\UpsertData.dtsx"'  
```  
  
 The following example shows how to run the same package and capture the return code:  
  
```  
DECLARE @returncode int  
EXEC @returncode = xp_cmdshell 'dtexec /f "C:\UpsertData.dtsx"'  
```  
  
> **IMPORTANT!!** In [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **xp_cmdshell** option is disabled by default on new installations. The option can be enabled by running the **sp_configure** system stored procedure. For more information, see [xp_cmdshell Server Configuration Option](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md).  

##  <a name="bash"></a> Using dtexec from Bash

The **Bash** shell is a popular shell for Linux. It can also be used on Windows. You can run dtexec from the Bash prompt. Notice that a semicolon (`;`) is a command delimiter operator in Bash. This is particularly important when passing in values to the package using the `/Conn[ection]` or `/Par[arameter]` or '`/Set` options since they use the semicolon to separate the name and the value of the item provided. The following example shows how to properly escape the semicolon and other items when using Bash and passing in values to a package:

```bash
dtexec /F MyPackage.dtsx /CONN "MyConnection"\;"\"MyConnectionString\""
```

##  <a name="syntax"></a> Syntax  
  
```  
dtexec /option [value] [/option [value]]...  
```  
  
##  <a name="parameter"></a> Parameters  
  
-   **/?** [*option_name*]: (Optional). Displays the command prompt options, or displays help for the specified *option_name* and then closes the utility.  
  
     If you specify an *option_name* argument, **dtexec** starts [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online and displays the dtexec Utility topic.  
  
-   **/Ca[llerInfo]**: (Optional). Specifies additional information for a package execution. When you run a package using SQL Server Agent, agent sets this argument to indicate that the package execution is invoked by SQL Server Agent. This parameter is ignored when the **dtexec** utility is run from the command line.  
  
-   **/CheckF[ile]** _filespec_: (Optional). Sets the **CheckpointFileName** property on the package to the path and file spemandcified in *filespec*. This file is used when the package restarts. If this option is specified and no value is supplied for the file name, the **CheckpointFileName** for the package is set to an empty string. If this option is not specified, the values in the package are retained.  
  
-   **/CheckP[ointing]** _{on\off}_ : (Optional). Sets a value that determines whether the package will use checkpoints during package execution. The value **on** specifies that a failed package is to be rerun. When the failed package is rerun, the run-time engine uses the checkpoint file to restart the package from the point of failure.  
  
     The default value is on if the option is declared without a value. Package execution will fail if the value is set to on and the checkpoint file cannot be found. If this option is not specified, the value set in the package is retained. For more information, see [Restart Packages by Using Checkpoints](../../integration-services/packages/restart-packages-by-using-checkpoints.md).  
  
     The **/CheckPointing on** option of dtexec is equivalent to setting the **SaveCheckpoints** property of the package to True, and the **CheckpointUsage** property to Always.  
  
-   **/Com[mandFile]** _filespec_: (Optional). Specifies the command options that run with **dtexec**. The file specified in *filespec* is opened and options from the file are read until EOF is found in the file. *filespec* is a text file. The *filespec* argument specifies the file name and path of the command file to associate with the execution of the package.  
  
-   **/Conf[igFile]** _filespec_: (Optional). Specifies a configuration file to extract values from. Using this option, you can set a run-time configuration that differs from the configuration that was specified at design time for the package. You can store different configuration settings in an XML configuration file and then load the settings before package execution by using the **/ConfigFile** option.  
  
     You can use the **/ConfigFile** option to load additional configurations at run time that you did not specify at design time. However, you cannot use the **/ConfigFile** option to replace configured values that you also specified at design time. To understand how package configurations are applied, see [Package Configurations](../../integration-services/packages/package-configurations.md).  
  
-   **/Conn[ection]** _id_or_name;connection_string [[;id_or_name;connection_string]...]_: (Optional). Specifies that the connection manager with the specified name or GUID is located in the package, and specifies a connection string.  
  
     This option requires that both parameters be specified: the connection manager name or GUID must be provided in the *id_or_name* argument, and a valid connection string must be specified in the *connection_string* argument. For more information, see [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md).  
  
     At run time, you can use the **/Connection** option to load package configurations from a location other than the location that you specified at design time. The values of these configurations then replace the values that were originally specified. However you can use the **/Connection** option only for configurations, such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configurations, that use a connection manager. To understand how package configurations are applied, see [Package Configurations](../../integration-services/packages/package-configurations.md) and [Behavior Changes to Integration Services Features in SQL Server 2016](https://msdn.microsoft.com/library/611d22fa-5ac7-485e-9a40-7131e852f794).  
  
-   **/Cons[oleLog]** [[*displayoptions*];[*list_options*;*src_name_or_guid*]...]: (Optional). Displays specified log entries to the console during package execution. If this option is omitted, no log entries are shown in the console. If the option is specified without parameters that limit the display, every log entry will display. To limit the entries that are displayed to the console, you can specify the columns to show by using the *displayoptions* parameter, and limit the log entry types by using the *list_options* parameter.  
  
    > **NOTE:**  When you run a package on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server by using the **/ISSERVER** parameter, console output is limited and most of the **/Cons[oleLog]** options are not applicable. All execution logs can be seen from the server in the related views or by using standard reports available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information about the reports, see [Reports for the Integration Services Server](../../integration-services/performance/monitor-running-packages-and-other-operations.md#reports).  
  
     The *displayoptions* values are as follows:  
  
    -   N (Name)  
  
    -   C (Computer)  
  
    -   O (Operator)  
  
    -   S (Source Name)  
  
    -   G (Source GUID)  
  
    -   X (Execution GUID)  
  
    -   M (Message)  
  
    -   T (Time Start and End)  
  
     The *list_options* values are as follows:  
  
    -   *I* - Specifies the inclusion list. Only the source names or GUIDs that are specified are logged.  
  
    -   *E* - Specifies the exclusion list. The source names or GUIDs that are specified are not logged.  
  
    -   The *src_name_or_guid* parameter specified for inclusion or exclusion is an event name, source name, or source GUID.  
  
     If you use multiple **/ConsoleLog** options on the same command prompt, they interact as follows:  
  
    -   Their order of appearance has no effect.  
  
    -   If no inclusion lists are present on the command line, exclusion lists are applied against all kinds of log entries.  
  
    -   If any inclusion lists are present on the command line, exclusion lists are applied against the union of all inclusion lists.  
  
     For several examples of the **/ConsoleLog** option, see the **Remarks** section.  
  
-   **/D[ts]** _package_path_: (Optional). Loads a package from the SSIS Package Store. Packages that are stored in the SSIS Package Store, are deployed using the legacy package deployment model. To run packages that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server using the project deployment model, use the **/ISServer** option. For more information about the package and project deployment models, see [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx).  
  
     The *package_path* argument specifies the relative path of the [!INCLUDE[ssIS](../../includes/ssis-md.md)] package, starting at the root of the SSIS Package Store, and includes the name of the [!INCLUDE[ssIS](../../includes/ssis-md.md)] package. If the path or file name specified in the *package_path* argument contains a space, you must put quotation marks around the *package_path* argument.  
  
     The **/DTS** option cannot be used together with the **/File** or **/SQL** option. If multiple options are specified, **dtexec** fails.  
  
-   **/De[crypt]**  _password_: (Optional). Sets the decryption password that is used when you load a package with password encryption.  
  
-   (Optional) Creates the debug dump files, .mdmp and .tmp, when one or more specified events occur while the package is running. The *error code* argument specifies the type of event code-error, warning, or information-that will trigger the system to create the debug dump files. To specify multiple event codes, separate each *error code* argument by a semi-colon (;). Do not include quotes with the *error code* argument.  
  
     The following example generates the debug dump files when the DTS_E_CANNOTACQUIRECONNECTIONFROMCONNECTIONMANAGER error occurs.  
  
    ```  
    /Dump 0xC020801C  
    ```  
  
     **/Dump** _error code_: By default, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] stores the debug dump files in the folder, *\<drive>*:\Program Files\Microsoft SQL Server\110\Shared\ErrorDumps.  
  
    > **NOTE:** Debug dump files may contain sensitive information. Use an access control list (ACL) to restrict access to the files, or copy the files to a folder with restricted access. For example, before you send your debug files to Microsoft support services, we recommended that you remove any sensitive or confidential information.  
  
     To apply this option to all packages that the **dtexec** utility runs, add a **DumpOnCodes** REG_SZ value to the HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\SSIS\Setup\DtsPath registry key. The data value in **DumpOnCodes** specifies the error code or codes that will trigger the system to create debug dump files. Multiple error codes must be separated by a semi-colon (;).  
  
     If you add a **DumpOnCodes** value to the registry key, and use the **/Dump** option, the system will create debug dump files that are based on both settings.  
  
     For more information about debug dump files, see [Generating Dump Files for Package Execution](../../integration-services/troubleshooting/generating-dump-files-for-package-execution.md).  
  
-   **/DumpOnError**: (Optional) Creates the debug dump files, .mdmp and .tmp, when any error occurs while the package is running.  
  
     By default, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] stores the debug dump files in the folder, *\<drive>*:\Program Files\Microsoft SQL Server\110\Shared\ErrorDumps folder.  
  
    > **NOTE:** Debug dump files may contain sensitive information. Use an access control list (ACL) to restrict access to the files, or copy the files to a folder with restricted access. For example, before you send your debug files to Microsoft support services, we recommended that you remove any sensitive or confidential information.  
  
     To apply this option to all packages that the **dtexec** utility runs, add a **DumpOnError** REG_DWORD value to the HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\SSIS\Setup\DtsPath registry key. The value of the **DumpOnError** REG_DWORD value determines whether the **/DumpOnError** option needs to be used with the **dtexec** utility:  
  
    -   A non-zero data value indicates that the system will create debug dump files when any error occurs, regardless of whether you use the **/DumpOnError** option with the **dtexec** utility.  
  
    -   A zero data value indicates that the system will not create the debug dump files unless you use the **/DumpOnError** option with the **dtexec** utility.  
  
     For more information about debug dump files, see [Generating Dump Files for Package Execution](../../integration-services/troubleshooting/generating-dump-files-for-package-execution.md)  
  
-   **/Env[Reference]** _environment reference ID_: (Optional). Specifies the environment reference (ID) that is used by the package execution, for a package that is deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. The parameters configured to bind to variables will use the values of the variables that are contained in the environment.  
  
     You use **/Env[Reference]** option together with the **/ISServer** and the **/Server** options.  
  
     This parameter is used by SQL Server Agent.  
  --   **/F[ile]** _filespec_: (Optional). Loads a package that is saved in the file system. Packages that are saved in the file system, are deployed using the legacy package deployment model. To run packages that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server using the project deployment model, use the **/ISServer** option. For more information about the package and project deployment models, see [Deployment of Projects and Packages](deploy-integration-services-ssis-projects-and-packages.md)  

  The *filespec* argument specifies the path and file name of the package. You can specify the path as either a Universal Naming Convention (UNC) path or a local path. If the path or file name specified in the *filespec* argument contains a space, you must put quotation marks around the *filespec* argument.  
  
     The **/File** option cannot be used together with the **/DTS** or **/SQL** option. If multiple options are specified, **dtexec** fails.  
  
-   **/H[elp]** [*option_name*]: (Optional). Displays help for the options, or displays help for the specified *option_name* and closes the utility.  
  
     If you specify an *option_name* argument, **dtexec** starts [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online and displays the dtexec Utility topic.  
  
-   **/ISServer** _packagepath_: (Optional). Runs a package that is deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. The *PackagePath* argument specifies the full path and file name of the package deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. If the path or file name specified in the *PackagePath* argument contains a space, you must put quotation marks around the *PackagePath* argument.  
  
     The package format is as follows:  
  
    ```  
    \<catalog name>\<folder name>\<project name>\package file name  
    ```  
  
     You use **/Server** option together with the **/ISSERVER** option. Only Windows Authentication can execute a package on the SSIS Server. The current Windows user is used to access the package. If the /Server option is omitted, the default local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is assumed.  
  
     The **/ISSERVER** option cannot be used together with the **/DTS**, **/SQL** or **/File** option. If multiple options are specified, dtexec fails.  
  
     This parameter is used by SQL Server Agent.  
  
-   **/L[ogger]** _classid_orprogid;configstring_: (Optional). Associates one or more log providers with the execution of an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package. The *classid_orprogid* parameter specifies the log provider, and can be specified as a class GUID. The *configstring* is the string that is used to configure the log provider.  
  
     The following list shows the available log providers:  
  
    -   Text file:  
  
        -   ProgID: DTS.LogProviderTextFile.1  
  
        -   ClassID: {59B2C6A5-663F-4C20-8863-C83F9B72E2EB}  
  
    -   [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]:  
  
        -   ProgID: DTS.LogProviderSQLProfiler.1  
  
        -   ClassID: {5C0B8D21-E9AA-462E-BA34-30FF5F7A42A1}  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
        -   ProgID: DTS.LogProviderSQLServer.1  
  
        -   ClassID: {6AA833A1-E4B2-4431-831B-DE695049DC61}  
  
    -   Windows Event Log:  
  
        -   ProgID: DTS.LogProviderEventLog.1  
  
        -   ClassID: {97634F75-1DC7-4F1F-8A4C-DAF0E13AAA22}  
  
    -   XML File:  
  
        -   ProgID: DTS.LogProviderXMLFile.1  
  
        -   ClassID: {AFED6884-619C-484F-9A09-F42D56E1A7EA}  
  
-   **/M[axConcurrent]** _concurrent_executables_: (Optional). Specifies the number of executable files that the package can run concurrently. The value specified must be either a non-negative integer, or -1. A value of -1 means that [!INCLUDE[ssIS](../../includes/ssis-md.md)] will allow a maximum number of concurrently running executables that is equal to the total number of processors on the computer executing the package, plus two.  
  
-   **/Pack[age]** _PackageName_: (Optional). Specifies the package that is executed. This parameter is used primarily when you execute the package from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
-   **/P[assword]** _password_: (Optional). Allows the retrieval of a package that is protected by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is used together with the **/User** option. If the **/Password** option is omitted and the **/User** option is used, a blank password is used. The *password* value may be quoted.  
  
    > **IMPORTANT!!** [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
-   **/Par[ameter]** [$Package:: | $Project:: | $ServerOption::] *parameter_name* [(data_type)]; *literal_value*: (Optional). Specifies parameter values. Multiple **/Parameter** options can be specified. The data types are CLR TypeCodes as strings. For a non-string parameter, the data type is specified in parenthesis, following the parameter name.  
  
     The **/Parameter** option can be used only with the **/ISServer** option.  
  
     You use the $Package, $Project, and $ServerOption prefixes to indicate a package parameter, project parameter, and a server option parameter, respectively. The default parameter type is package.  
  
     The following is an example of executing a package and providing myvalue for the project parameter (myparam) and the integer value 12 for the package parameter (anotherparam).  
  
     `Dtexec /isserver "SSISDB\MyFolder\MyProject\MyPackage.dtsx" /server "." /parameter $Project::myparam;myvalue /parameter anotherparam(int32);12`  
  
     You can also set connection manager properties by using parameters. You use the CM prefix to denote a connection manager parameter.  
  
     In the following example, InitialCatalog property of the SourceServer connection manager is set to `ssisdb`.  
  
    ```  
    /parameter CM.SourceServer.InitialCatalog;ssisdb  
    ```  
  
     In the following example, ServerName property of the SourceServer connection manager is set to a period (.) to indicate the local server.  
  
    ```  
    /parameter CM.SourceServer.ServerName;.  
    ```  
  
-   **/Proj[ect]** _ProjectFile_: (Optional). Specifies the project from which to retrieve the package that is executed. The *ProjectFile* argument specifies the .ispac file name. This parameter is used primarily when you execute the package from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
-   **/Rem** _comment_: (Optional). Includes comments on the command prompt or in command files. The argument is optional. The value of *comment* is a string that must be enclosed in quotation marks, or contain no white space. If no argument is specified, a blank line is inserted. *comment* values are discarded during the command sourcing phase.  
  
-   **/Rep[orting]** _level_ [*;event_guid_or_name*[*;event_guid_or_name*[...]]: (Optional). Specifies what types of messages to report. The available reporting options for *level* are as follows:  
  
     **N** No reporting.  
  
     **E** Errors are reported.  
  
     **W** Warnings are reported.  
  
     **I** Informational messages are reported.  
  
     **C** Custom events are reported.  
  
     **D** Data Flow task events are reported.  
  
     **P** Progress is reported.  
  
     **V** Verbose reporting.  
  
     The arguments of V and N are mutually exclusive to all other arguments; they must be specified alone. If the **/Reporting** option is not specified then the default level is **E** (errors), **W** (warnings), and **P** (progress).  
  
     All events are preceded with a timestamp in the format "YY/MM/DD HH:MM:SS", and a GUID or friendly name if available.  
  
     The optional parameter *event_guid_or_name* is a list of exceptions to the log providers. The exception specifies the events that are not logged that otherwise might have been logged.  
  
     You do not have to exclude an event if the event is not ordinarily logged by default  
  
-   **/Res[tart]** {*deny | force | ifPossible*}: (Optional). Specifies a new value for the <xref:Microsoft.SqlServer.Dts.Runtime.Package.CheckpointUsage%2A> property on the package. The meaning of the parameters are as follows:  
  
     *Deny* Sets <xref:Microsoft.SqlServer.Dts.Runtime.Package.CheckpointUsage%2A> property to <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSCheckpointUsage.DTSCU_NEVER>.  
  
     *Force* Sets <xref:Microsoft.SqlServer.Dts.Runtime.Package.CheckpointUsage%2A> property to <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSCheckpointUsage.DTSCU_ALWAYS>.  
  
     *ifPossible* Sets <xref:Microsoft.SqlServer.Dts.Runtime.Package.CheckpointUsage%2A> property to <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSCheckpointUsage.DTSCU_IFEXISTS>.  
  
     The default value of **force** is used if no value is specified.  
  
-   **/Set** [$Sensitive::]*propertyPath;value*: (Optional). Overrides the configuration of a parameter, variable, property, container, log provider, Foreach enumerator, or connection within a package. When this option is used, **/Set** changes the *propertyPath* argument to the value specified. Multiple **/Set** options can be specified.  
  
     In addition to using the **/Set** option with the **/F[ile]** option, you can also use the **/Set** option with the **/ISServer** option or the **/Project** option. When you use **/Set** with **/Project**, **/Set** sets parameter values. When you use **/Set** with **/ISServer**, **/Set** sets property overrides. In addition, when you use **/Set** with **/ISServer**, you can use the optional $Sensitive prefix to indicate that the property should be treated as sensitive on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     You can determine the value of *propertyPath* by running the Package Configuration Wizard. The paths for items that you select are displayed on the final **Completing the Wizard** page, and can be copied and pasted. If you have used the wizard only for this purpose, you can cancel the wizard after you copy the paths.  
  
     The following is an example of executing a package that is saved in the file system and providing a new value for a variable:  
  
     `dtexec /f mypackage.dtsx /set \package.variables[myvariable].Value;myvalue`  
  
     The following example of running a package from the .ispac project file and setting package and project parameters.  
  
     `/Project c:\project.ispac /Package Package1.dtsx /SET \Package.Variables[$Package::Parameter];1 /SET \Package.Variables[$Project::Parameter];1`  
  
     You can use the **/Set** option to change the location from which package configurations are loaded. However, you cannot use the **/Set** option to override a value that was specified by a configuration at design time. To understand how package configurations are applied, see [Package Configurations](../../integration-services/packages/package-configurations.md) and [Behavior Changes to Integration Services Features in SQL Server 2016](https://msdn.microsoft.com/library/611d22fa-5ac7-485e-9a40-7131e852f794).  
  
-   **/Ser[ver]** _server_: (Optional). When the **/SQL** or **/DTS** option is specified, this option specifies the name of the server from which to retrieve the package. If you omit the **/Server** option and the **/SQL** or **/DTS** option is specified, package execution is tried against the local server. The *server_instance* value may be quoted.  
  
     The **/Ser[ver]** option is required when the **/ISServer** option is specified.  
  
-   **/SQ[L]** _package_path_: Loads a package that is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], in **msdb** database. Packages that are stored in the **msdb** database, are deployed using the package deployment model. To run packages that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server using the project deployment model, use the **/ISServer** option. For more information about the package and project deployment models, see [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx).   
  
     The *package_path* argument specifies the name of the package to retrieve. If folders are included in the path, they are terminated with backslashes ("\\"). The *package_path* value can be quoted. If the path or file name specified in the *package_path* argument contains a space, you must put quotation marks around the *package_path* argument.  
  
     You can use the **/User**, **/Password**, and **/Server** options together with the **/SQL** option.  
  
     If you omit the **/User** option, Windows Authentication is used to access the package. If you use the **/User** option, the **/User** login name specified is associated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
     The **/Password** option is used only together with the **/User** option. If you use the **/Password** option, the package is accessed with the user name and password information provided. If you omit the **/Password** option, a blank password is used.  
  
    > **IMPORTANT!!** [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
     If the **/Server** option is omitted, the default local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is assumed.  
  
     The **/SQL** option cannot be used together with the **/DTS** or **/File** option. If multiple options are specified, **dtexec** fails.  
  
-   **/Su[m]**: (Optional). Shows an incremental counter that contains the number of rows that will be received by the next component.  
  
-   **/U[ser]** _user_name_: (Optional). Allows the retrieval of a package that is protected by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is used only when the **/SQL** option is specified. The *user_name* value can be quoted.  
  
    > **IMPORTANT!!**  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
-   **/Va[lidate]**: (Optional). Stops the execution of the package after the validatation phase, without actually running the package. During validation, use of the **/WarnAsError** option causes **dtexec** to treat a warning as an error; therefore the package fails if a warning occurs during validation.  
  
-   **/VerifyB[uild]** _major_[*;minor*[*;build*]]: (Optional). Verifies the build number of a package against the build numbers that were specified during the verification phase in the *major*, *minor*, and *build* arguments. If a mismatch occurs, the package will not execute.  
  
     The values are long integers. The argument can have one of three forms, with a value for *major* always required:  
  
    -   *major*  
  
    -   *major*;*minor*  
  
    -   *major*; *minor*; *build*  
  
-   **/VerifyP[ackageID]** _packageID_: (Optional). Verifies the GUID of the package to be executed by comparing it to the value specified in the *package_id* argument.  
  
-   **/VerifyS[igned]**: (Optional). Causes [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to check the digital signature of the package. If the package is not signed or the signature is not valid, the package fails. For more information, see [Identify the Source of Packages with Digital Signatures](../../integration-services/security/identify-the-source-of-packages-with-digital-signatures.md).  
  
    > **IMPORTANT!!** When configured to check the signature of the package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] only checks whether the digital signature is present, is valid, and is from a trusted source. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] does not check whether the package has been changed.  
  
    > **NOTE:** The optional **BlockedSignatureStates** registry value can specify a setting that is more restrictive than the digital signature option set in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or at the **dtexec** command line. In this situation, the more restrictive registry setting overrides the other settings.  
  
-   **/VerifyV[ersionID]** _versionID_: (Optional). Verifies the version GUID of a package to be executed by comparing it to the value specified in the *version_id* argument during package Validation Phase.  
  
-   **/VLog** _[Filespec]_: (Optional). Writes all Integration Services package events to the log providers that were enabled when the package was designed. To have Integration Services enable a log provider for text files and write log events to a specified text file, include a path and file name as the *Filespec* parameter.  
  
     If you do not include the *Filespec* parameter, Integration Services will not enable a log provider for text files. Integration Services will only write log events to the log providers that were enabled when the package was designed.  
  
-   **/W[arnAsError]**: (Optional). Causes the package to consider a warning as an error; therefore, the package will fail if a warning occurs during validation. If no warnings occur during validation and the **/Validate** option is not specified, the package is executed.  
  
-   **/X86**: (Optional). Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to run the package in 32-bit mode on a 64-bit computer. This option is set by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent when the following conditions are true:  
  
    -   The job step type is **SQL Server Integration Services package**.  
  
    -   The **Use 32 bit runtime** option on the **Execution options** tab of the **New Job Step** dialog box is selected.  
  
     You can also set this option for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step by using stored procedures or SQL Server Management Objects (SMO) to programmatically create the job.  
  
     This option is only used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. This option is ignored if you run the **dtexec** utility at the command prompt.  
  
##  <a name="remark"></a> Remarks  
 The order in which you specify command options can influence the way in which the package executes:  
  
-   Options are processed in the order they are encountered on the command line. Command files are read in as they are encountered on the command line. The commands in the command file are also processed in the order they are encountered.  
  
-   If the same option, parameter, or variable appears in the same command line statement more than one time, the last instance of the option takes precedence.  
  
-   **/Set** and **/ConfigFile** options are processed in the order they are encountered.  
  
##  <a name="example"></a> Examples  
 The following examples demonstrate how to use the **dtexec** command prompt utility to configure and execute [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.  
  
 **Running Packages**  
  
 To execute an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package saved to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Windows Authentication, use the following code:  
  
```  
dtexec /sq pkgOne /ser productionServer  
```  
  
 To execute an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package saved to the File System folder in the SSIS Package Store, use the following code:  
  
```  
dtexec /dts "\File System\MyPackage"  
```  
  
 To validate a package that uses Windows Authentication and is saved in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without executing the package, use the following code:  
  
```  
dtexec /sq pkgOne /ser productionServer /va  
```  
  
 To execute an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package that is saved in the file system, use the following code:  
  
```  
dtexec /f "c:\pkgOne.dtsx"   
```  
  
 To execute an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package that is saved in the file system, and specify logging options, use the following code:  
  
```  
dtexec /f "c:\pkgOne.dtsx" /l "DTS.LogProviderTextFile;c:\log.txt"  
```  
  
 To execute a package that uses Windows Authentication and is saved to the default local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and verify the version before it is executed, use the following code:  
  
```  
dtexec /sq pkgOne /verifyv {c200e360-38c5-11c5-11ce-ae62-08002b2b79ef}  
```  
  
 To execute an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package that is saved in the file system and configured externally, use the following code:  
  
```  
dtexec /f "c:\pkgOne.dtsx" /conf "c:\pkgOneConfig.cfg"  
```  
  
> **NOTE:** The *package_path* or *filespec* arguments of the /SQL, /DTS, or /FILE options must be enclosed in quotation marks if the path or file name contains a space. If the argument is not enclosed in quotation marks, the argument cannot contain white space.  
  
 **Logging Option**  
  
 If there are three log entry types of A, B, and C, the following **ConsoleLog** option without a parameter displays all three log types with all fields:  
  
```  
/CONSOLELOG  
```  
  
 The following option displays all log types, but with the Name and Message columns only:  
  
```  
/CONSOLELOG NM  
```  
  
 The following option displays all columns, but only for log entry type A:  
  
```  
/CONSOLELOG I;LogEntryTypeA  
```  
  
 The following option displays only log entry type A, with Name and Message columns:  
  
```  
/CONSOLELOG NM;I;LogEntryTypeA  
```  
  
 The following option displays log entries for log entry types A and B:  
  
```  
/CONSOLELOG I;LogEntryTypeA;LogEntryTypeB  
```  
  
 You can achieve the same results by using multiple **ConsoleLog** options:  
  
```  
/CONSOLELOG I;LogEntryTypeA /CONSOLELOG I;LogEntryTypeB  
```  
  
 If the **ConsoleLog** option is used without parameters, all fields are displayed. The inclusion of a *list_options* parameter causes the following to displays only log entry type A, with all fields:  
  
```  
/CONSOLELOG NM;I;LogEntryTypeA /CONSOLELOG  
```  
  
 The following displays all log entries except log entry type A: that is, it displays log entry types B and C:  
  
```  
/CONSOLELOG E;LogEntryTypeA  
```  
  
 The following example achieves the same results by using multiple **ConsoleLog** options and a single exclusion:  
  
```  
/CONSOLELOG E;LogEntryTypeA /CONSOLELOG  
/CONSOLELOG E;LogEntryTypeA /CONSOLELOG E;LogEntryTypeA  
/CONSOLELOG E;LogEntryTypeA;LogEntryTypeA  
```  
  
 The following example displays no log messages, because when a log file type is found in both the included and excluded lists, it will be excluded.  
  
```  
/CONSOLELOG E;LogEntryTypeA /CONSOLELOG I;LogEntryTypeA  
```  
  
 **SET Option**  
  
 The following example shows how to use the **/SET** option, which lets you change the value of any package property or variable when you start the package from the command line.  
  
```  
/SET \package\DataFlowTask.Variables[User::MyVariable].Value;newValue  
```  
  
 **Project Option**  
  
 The following example shows how to use the **/Project** and the **/Package** option.  
  
```  
/Project c:\project.ispac /Package Package1.dtsx  
```  
  
 The following example shows how to use the **/Project** and **/Package** options, and set package and project parameters.  
  
```  
/Project c:\project.ispac /Package Package1.dtsx /SET \Package.Variables[$Package::Parameter];1 /SET \Package.Variables[$Project::Parameter];1  
  
```  
  
 **ISServer Option**  
  
 The following example shows how to use the **/ISServer** option.  
  
```  
dtexec /isserver "\SSISDB\MyFolder\MyProject\MyPackage.dtsx" /server "."  
```  
  
 The following example shows how to use the **/ISServer** option and set project and connection manager parameters.  
  
```  
/Server localhost /ISServer "\SSISDB\MyFolder\Integration Services Project1\Package.dtsx" /Par "$Project::ProjectParameter(Int32)";1 /Par "CM.SourceServer.InitialCatalog";SourceDB  
  
```  
  
## Related Content  
 Blog entry, [Exit Codes, DTEXEC, and SSIS Catalog](https://www.mattmasson.com/2012/02/exit-codes-dtexec-and-ssis-catalog/), on www.mattmasson.com.  
  
  
