---
title: "SQL Server Agent Jobs for Packages | Microsoft Docs"
ms.custom: ""
ms.date: 06/04/2018
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [Integration Services]"
  - "automatic package execution"
  - "scheduling packages [Integration Services]"
  - "SQL Server Agent [Integration Services]"
ms.assetid: ecf7a5f9-b8a7-47f1-9ac0-bac07cb89e31
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SQL Server Agent Jobs for Packages
  You can automate and schedule the execution of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. You can schedule packages that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, and are stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store, and the file system.  
 
> [!NOTE]
> This article describes how to schedule SSIS packages in general, and how to schedule packages on premises. You can also run and schedule SSIS packages on the following platforms:
> - **The Microsoft Azure cloud**. For more info, see [Lift and shift SQL Server Integration Services workloads to the cloud](../lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md) and [Schedule the execution of an SSIS package in Azure](../lift-shift/ssis-azure-schedule-packages.md).
> - **Linux**. For more info, see [Extract, transform, and load data on Linux with SSIS](../../linux/sql-server-linux-migrate-ssis.md) and [Schedule SQL Server Integration Services package execution on Linux with cron](../../linux/sql-server-linux-schedule-ssis-packages.md).

## Sections in This Topic  
 This topic contains the following sections:  
  
-   [Scheduling jobs in SQL Server Agent](#jobs)  
  
-   [Scheduling Integration Services packages](#packages)  
  
-   [Troubleshooting scheduled packages](#trouble)  
  
##  <a name="jobs"></a> Scheduling Jobs in SQL Server Agent  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is the service installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that lets you automate and schedule tasks by running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service must be running before jobs can run automatically. For more information, see [Configure SQL Server Agent](https://docs.microsoft.com/sql/ssms/agent/configure-sql-server-agent).  
  
 The **SQL Server Agent** node appears in Object Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] when you connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
 To automate a recurring task, you create a job by using the **New Job** dialog box. For more information, see [Implement Jobs](https://docs.microsoft.com/sql/ssms/agent/implement-jobs).  
  
 After you create the job, you must add at least one step. A job can include multiple steps, and each step can perform a different task. For more information, see [Manage Job Steps](https://docs.microsoft.com/sql/ssms/agent/manage-job-steps).  
  
 After you create the job and the job steps, you can create a schedule for running the job. However you can also create an unscheduled job that you run manually. For more information, see [Create and Attach Schedules to Jobs](https://docs.microsoft.com/sql/ssms/agent/create-and-attach-schedules-to-jobs).  
  
 You can enhance the job by setting notification options, such as specifying an operator to send an e-mail message to when the job finishes, or adding alerts. For more information, see [Alerts](https://docs.microsoft.com/sql/ssms/agent/alerts).  
  
##  <a name="packages"></a> Scheduling Integration Services Packages  
 When you create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job to schedule [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, you must add at least one step and set the type of the step to **SQL Server Integration Services Package**. A job can include multiple steps, and each step can run a different package.  
  
 Running an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package from a job step is like running a package by using the **dtexec** (dtexec.exe) and **DTExecUI** (dtexecui.exe) utilities. Instead of setting the run-time options for a package by using command-line options or the **Execute Package Utility** dialog box, you set the run-time options in the **New Job Step** dialog box. For more information about the options for running a package, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).  
  
 For more information, see [Schedule a Package by using SQL Server Agent](#schedule).  
  
 For a video that demonstrates how to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to run a package, see the video home page, [How to: Automate Package Execution by Using the SQL Server Agent (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=141771), in the MSDN Library.  
  
##  <a name="trouble"></a> Troubleshooting  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step may fail to start a package even though the package runs successfully in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and from the command line. There are some common reasons for this issue and several recommended solutions. For more information, see the following resources.  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base article, [An SSIS package does not run when you call the SSIS package from a SQL Server Agent job step](https://support.microsoft.com/kb/918760)  
  
-   Video, [Troubleshooting: Package Execution Using SQL Server Agent (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=141772), in the MSDN Library.  
  
 After a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step starts a package, the package execution may fail or the package may run successfully but with unexpected results. You can use the following tools to troubleshoot these issues.  
  
-   For packages that are stored in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] MSDB database, the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store, or in a folder on your local machine, you can use the **Log File Viewer** as well as any logs and debug dump files that were generated during the execution of the package.  
  
     **To use the Log File Viewer, do the following.**  
  
    1.  Right-click the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job in Object Explorer and then click **View History**.  
  
    2.  Locate the job execution in the **Log file summary** box with the **job failed** message in the **Message** column.  
  
    3.  Expand the job node, and click the job step to view the details of the message in the area below the **Log file summary** box.  
  
-   For packages that are stored in the SSISDB database, you can also use the **Log File Viewer** as well as any logs and debug dump files that were generated during the execution of the package. In addition, you can use the reports for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     **To find information in the reports for the package execution associated with a job execution, do the following.**  
  
    1.  Follow the steps above to view the details of the message for the job step.  
  
    2.  Locate the Execution ID listed in the message.  
  
    3.  Expand the Integration Services Catalog node in Object Explorer.  
  
    4.  Right-click SSISDB, point to Reports, then Standard Reports, and then click All Executions.  
  
    5.  In the **All Executions** report, locate the Execution ID in the **ID** column. Click **Overview**, **All Messages**, or **Execution Performance** to view information about this package execution.  
  
    For more information about the Overview, All Messages, and Execution Performance reports, see [Reports for the Integration Services Server](../../integration-services/performance/monitor-running-packages-and-other-operations.md#reports).  

## <a name="schedule"></a> Schedule a Package by using SQL Server Agent
  The following procedure provides steps to automate the execution of a package by using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step to run the package.  
  
### To automate package execution by using SQL Server Agent  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on which you want to create a job, or the instance that contains the job to which you want to add a step.  
  
2.  Expand the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node in Object Explorer and perform one of the following tasks:  
  
    -   To create a new job, right-click **Jobs** and then click **New Job**.  
  
    -   To add a step to an existing job, expand **Jobs**, right-click the job, and then click **Properties**.  
  
3.  On the **General** page, if you are creating a new job, provide a job name, select an owner and job category, and, optionally, provide a job description.  
  
4.  To make the job available for scheduling, select **Enabled**.  
  
5.  To create a job step for the package you want to schedule, click **Steps**, and then click **New**.  
  
6.  Select **Integration Services Package** for the job step type.  
  
7.  In the **Run as** list, select **SQL Server Agent Service Account** or select a proxy account that has the credentials that the job step will use. For information about creating a proxy account, see [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md).  
  
     Using a proxy account instead of the **SQL Server Agent Service Account** may resolve common issues that can occur when executing a package using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information about these issues, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base article, [An SSIS package does not run when you call the SSIS package from a SQL Server Agent job step](https://support.microsoft.com/kb/918760).  
  
    > **NOTE:** If the password changes for the credential that the proxy account uses, you need to update the credential password. Otherwise, the job step will fail.  
  
     For information about configuring the SQL Server Agent service account, see [Set the Service Startup Account for SQL Server Agent &#40;SQL Server Configuration Manager&#41;](https://msdn.microsoft.com/library/46ffe818-ebb5-43a0-840b-923f219a2472).  
  
8.  In the **Package Source** list box, click the source of the package and then configure the options for the job step.  
  
     **The following table describes the possible package sources.**  
  
    |Package Source|Description|  
    |--------------------|-----------------|  
    |**SSIS Catalog**|Packages that are stored in the SSISDB database. The packages are contained in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.|  
    |**SQL Server**|Packages that are stored in the MSDB database. You use the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service to manage these packages.|  
    |**SSIS Package Store**|Packages that are stored in the default folder on your computer. The default folder is *\<drive>*:\Program Files\Microsoft SQL Server\110\DTS\Packages. You use the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service to manage these packages.<br /><br /> Note: You can specify a different folder or specify additional folders in the file system to be managed by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, by modifying the configuration file for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. For more information, see [Integration Services Service &#40;SSIS Service&#41;](../../integration-services/service/integration-services-service-ssis-service.md).|  
    |**File System**|Packages that are stored in any folder on your local machine.|  
  
     **The following tables describe the configuration options that are available for the job step depending on the package source you select.**  
  
    > **IMPORTANT:** If the package is password-protected, when you click any of the tabs on the **General** page of the **New Job Step** dialog box, with the exception of the **Package** tab, you need to enter the password in the **Package Password** dialog box that appears. Otherwise the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job will fail to run the package.  
  
     **Package Source**: SSIS Catalog  
  
    |Tab|Options|  
    |---------|-------------|  
    |**Package**|**Server**<br /><br /> Type or select the name of the database server instance that hosts the SSISDB catalog.<br /><br /> When **SSIS Catalog** is the package source, you can log on to the server using only a Microsoft Windows user account. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication is not available.|  
    ||**Package**<br /><br /> Click the ellipsis button and select a package.<br /><br /> You are selecting a package in a folder under the **Integration Services Catalogs** node in **Object Explorer**.|  
    |**Parameters**<br /><br /> Located on the **Configuration** tab.|The **Integration Services Project Conversion Wizard** enables you to replace package configurations with parameters.<br /><br /> The **Parameters** tab displays parameters that you added when you designed the package, for example by using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The tab also displays parameters that were added to the package when you converted the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project from the package deployment model to the project deployment model. Enter new values for parameters that are contained in the package. You can enter a literal value or use the value contained in a server environment variable that you have already mapped to the parameter.<br /><br /> To enter the literal value, click the ellipsis button next to a parameter. The **Edit Literal Value for Execution** dialog box appears.<br /><br /> To use an environment variable, click **Environment** and then select the environment that contains the variable you want to use.<br /><br /> **\*\* Important \*\*** If you have mapped multiple parameters and/or connection manager properties to variables contained in multiple environments, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent displays an error message. For a given execution, a package can execute only with the values contained in a single server environment.<br /><br /> For information on how to create a server environment and map a variable to a parameter, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md).|  
    |**Connection Managers**<br /><br /> Located on the **Configuration** tab.|Change values for connection manager properties. For example, you can change the server name. Parameters are automatically generated on the SSIS server for the connection manager properties. To change a property value, you can enter a literal value or use the value contained in a server environment variable that you have already mapped to the connection manager property.<br /><br /> To enter the literal value, click the ellipsis button next to a parameter. The **Edit Literal Value for Execution** dialog box appears.<br /><br /> To use an environment variable, click **Environment** and then select the environment that contains the variable you want to use.<br /><br /> **\*\* Important \*\*** If you have mapped multiple parameters and/or connection manager properties to variables contained in multiple environments, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent displays an error message. For a given execution, a package can execute only with the values contained in a single server environment.<br /><br /> For information on how to create a server environment and map a variable to a connection manager property, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md).|  
    |**Advanced**<br /><br /> Located on the **Configuration** tab.|Configure the following additional settings for the package execution:|  
    ||**Property overrides**:<br /><br /> Click **Add** to enter a new value for a package property, specify the property path, and indicate whether the property value is sensitive. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server encrypts sensitive data. To edit or remove the settings for a property, click a row in the **Property** overrides box and then click **Edit** or **Remove**. You can find the property path by doing one of the following:<br /><br /> -Copy the property path from the XML configuration file (\*.dtsconfig) file. The path is listed in the Configuration section of the file, as a value of the Path attribute. The following is an example of the path for the MaximumErrorCount property: \Package.Properties[MaximumErrorCount]<br /><br /> -Run the **Package Configuration Wizard** and copy the property paths from the final **Completing the Wizard** page. You can then cancel the wizard.<br /><br /> <br /><br /> Note: The **Property overrides** option is intended for packages with configurations that you upgraded from a previous release of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Packages that you create using [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] and deploy to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server use parameters instead of configurations.|  
    ||**Logging level**<br /><br /> Select one of the following logging levels for the package execution. Note that selecting the **Performance** or **Verbose** logging level may impact the performance of the package execution.<br /><br /> **None**:<br />                          Logging is turned off. Only the package execution status is logged.<br /><br /> **Basic**:<br />                          All events are logged, except custom and diagnostic events. This is the default value for the logging level.<br /><br /> **Performance**:<br />                          Only performance statistics, and OnError and OnWarning events, are logged.<br /><br /> **Verbose**:<br />                          All events are logged, including custom and diagnostic events.<br /><br /> The logging level you select determines what information is displayed in SSISDB views and in reports for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information, see [Integration Services (SSIS) Logging](../../integration-services/performance/integration-services-ssis-logging.md).|  
    ||**Dump on errors**<br /><br /> Specify whether debug dump files are generated when any error occurs during the execution of the package. The files contain information about the execution of the package that can help you troubleshoot issues. When you select this option, and an error occurs during execution, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a .mdmp file (binary file) and a .tmp file (text file). By default, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] stores the files in the *\<drive>:*\Program Files\Microsoft SQL Server\110\Shared\ErrorDumps folder.|  
    ||**32-bit runtime**<br /><br /> Indicate whether to run the package using the 32-bit version of the dtexec utility on a 64-bit computer that has the 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent installed.<br /><br /> You may need to run the package using the 32-bit version of dtexec if for example your package uses a native OLE DB provider that is not available in a 64-bit version. For more information, see [64 bit Considerations for Integration Services](https://msdn.microsoft.com/library/ms141766\(SQL.105\).aspx).<br /><br /> By default, when you select the **SQL Server Integration Services Package** job step type, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs the package using the version of the dtexec utility that is automatically invoked by the system. The system invokes either the 32-bit or 64-bit version of the utility depending on the computer processor, and the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent that is running on the computer.|  
  
     **Package Source**:  SQL Server, SSIS Package Store, or File System  
  
     Many of the options that you can set for packages stored in SQL Server, the SSIS Package Store, or the file system, correspond to command-line options for the **dtexec** command prompt utility. For more information about the utility and command-line options, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).  
  
    |Tab|Options|  
    |---------|-------------|  
    |**Package**<br /><br /> These are the tab options for packages that are stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store.|**Server**<br /><br /> Type or select the name of the database server instance for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.|  
    ||**Use Windows Authentication**<br /><br /> Select this option to log on to the server using a Microsoft Windows user account.|  
    ||**Use SQL Server Authentication**<br /><br /> When a user connects with a specified login name and password from a non-trusted connection, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication by checking to see if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot find the login account, authentication fails, and the user receives an error message.|  
    ||**User Name**|  
    ||**Password**|  
    ||**Package**<br /><br /> Click the ellipsis button and select the package.<br /><br /> You are selecting a package in a folder under the **Stored Packages** node in **Object Explorer**.|  
    |**Package**<br /><br /> These are the tab options for packages that are stored in the file system.|**Package**<br /><br /> Type the full path for the package file, or click the ellipsis button to select the package.|  
    |**Configurations**|Add an XML configuration file to run the package with a specific configuration. You use a package configuration to update the values of package properties at runtime.<br /><br /> This option corresponds to the **/ConfigFile** option for **dtexec**.<br /><br /> To understand how package configurations are applied, see [Package Configurations](../../integration-services/packages/package-configurations.md). For information on how to create a package configuration, see [Create Package Configurations](../../integration-services/packages/create-package-configurations.md).|  
    |**Command files**|Specify additional options you want to run with **dtexec**, in a separate file.<br /><br /> For example, you can include a file that contains the /Dump *errorcode* option, to generate debug dump files when one or more specified events occur while the package is running.<br /><br /> You can run a package with different sets of options by creating multiple files and then specifying the appropriate file by using the **Command files** option.<br /><br /> The **Command files** option corresponds to the **/CommandFile** option for **dtexec**.|  
    |**Data Sources**|View the connection managers contained in the package. To modify a connection string, click the connection manager and then click the connection string.<br /><br /> This option corresponds to the **/Connection** option for **dtexec**.|  
    |**Execution Options**|**Fail the package on validation warnings**<br /> Indicates whether a warning message is consider an error. If you select this option and a warning occurs during validation, the package will fail during validation. This option corresponds to the **/WarnAsError** option for **dtexec**.<br /><br /> **Validate package without executing**<br /> Indicates whether the package execution is stopped after the validation phase without actually running the package. This option corresponds to the **/Validate** option for **dtexec**.<br /><br /> **Override MacConcurrentExecutables property**<br /> Specifies the number of executable files that the package can run concurrently. A value of -1 means that the package can run a maximum number of executable files equal to the total number of processors on the computer executing the package, plus two. This option corresponds to the **/MaxConcurrent** option for **dtexec**.<br /><br /> **Enable package checkpoints**<br /> Indicates whether the package will use checkpoints during package execution. For more information, see [Restart Packages by Using Checkpoints](../../integration-services/packages/restart-packages-by-using-checkpoints.md).<br /><br /> This options corresponds to the **/CheckPointing** option for **dtexec**.<br /><br /> **Override restart options**<br /> Indicates whether a new value is set for the **CheckpointUsage** property on the package. Select a value from the **Restart option** list box.<br /><br /> This option corresponds to the **/Restart** option for **dtexec**.<br /><br /> **Use 32 bit runtime**<br /> Indicate whether to run the package using the 32-bit version of the dtexec utility on a 64-bit computer that has the 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent installed.<br /><br /> You may need to run the package using the 32-bit version of dtexec if for example your package uses a native OLE DB provider that is not available in a 64-bit version. For more information, see [64 bit Considerations for Integration Services](https://msdn.microsoft.com/library/ms141766\(SQL.105\).aspx).<br /><br /> By default, when you select the **SQL Server Integration Services Package** job step type, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs the package using the version of the dtexec utility that is automatically invoked by the system. The system invokes either the 32-bit or 64-bit version of the utility depending on the computer processor, and the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent that is running on the computer.|  
    |**Logging**|Associate a log provider with the execution of the package.<br /><br /> **SSIS log provider for Text files**<br /> Writes log entries to ASCII text files<br /><br /> **SSIS log provider for SQL Server**<br /> Writes log entries to the sysssislog table in the MSDB database.<br /><br /> **SSIS log provider for SQL Server Profiler**<br /> Writes traces that you can view using SQL Server Profiler.<br /><br /> **SSIS log provider for Windows Event Log**<br /> Writes log entries to the Application log in the Windows Event log.<br /><br /> **SSIS log provider for XML files**<br /> Writes log files to an XML file.<br /><br /> For the text file, XML file, and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler log providers, you are selecting file connection managers that are contained in the package. For the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log provider, you are selecting an OLE DB connection manager that is contained in the package.<br /><br /> This option corresponds to the **/Logger** option for **dtexec**.|  
    |**Set values**|Override a package property setting. In the **Properties** box, enter values in the **Property Path** and **Value** columns. After you enter values for one property, an empty row appears in the **Properties** box to enable you to enter values for another property.<br /><br /> To remove a property from the Properties box, click the row and then click **Remove**.<br /><br /> You can find the property path by doing one of the following:<br /><br /> -Copy the property path from the XML configuration file (\*.dtsconfig) file. The path is listed in the Configuration section of the file, as a value of the Path attribute. The following is an example of the path for the MaximumErrorCount property: \Package.Properties[MaximumErrorCount]<br /><br /> -Run the **Package Configuration Wizard** and copy the property paths from the final **Completing the Wizard** page. You can then cancel the wizard.|  
    |**Verification**|**Execute only signed packages**<br /> Indicates whether the package signature is checked. If the package is not signed or the signature is not valid, the package fails. This option corresponds to the **/VerifySigned** option for **dtexec**.<br /><br /> **Verify Package build**<br /> Indicates whether the build number of the package is verified against the build number that is entered in the **Build** box next to this option. If a mismatch occurs, the package will not execute. This option corresponds to the **/VerifyBuild** option for **dtexec**.<br /><br /> **Verify package ID**<br /> Indicates whether the GUID of the package is verified, by comparing it to the package ID that is entered in the **Package ID** box next to this option. This option corresponds to the **/VerifyPackageID** option for **dtexec**.<br /><br /> **Verify version ID**<br /> Indicates whether the version GUID of the package is verified, by comparing it version ID that is entered in the **Version ID** box next to this option. This option corresponds to the **/VerifyVersionID** option for **dtexec**.|  
    |**Command line**|Modify the command line options for dtexec. For more information about the options, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).<br /><br /> **Restore the original options**<br /> Use the command-line options that you have set in the **Package**, **Configurations**, **Command files**, **Data sources**, **Execution options**, **Logging**, **Set values**, and **Verification** tabs of the **Job Set Properties** dialog box.<br /><br /> **Edit the command manually**<br /> Type additional command-line options in the **Command line** box.<br /><br /> Before you click **OK** to save your changes to the job step, you can remove all of the additional options that you've typed in the **Command line** box by clicking **Restore the original options**.<br /><br /> **\*\* Tip \*\*** You can copy the command line to a Command Prompt window, add `dtexec`, and run the package from the command line. This is an easy way to generate the command line text.|  
  
9. Click **OK** to save the settings and close the **New Job Step** dialog box.  
  
    > **NOTE:** For packages that are stored in the **SSIS Catalog**, the **OK** button is disabled when there is an unresolved parameter or connection manager property setting. An unresolved setting occurs when you are using a value contained in a server environment variable to set the parameter or property and one of the following conditions is met.:  
    >   
    >  The **Environment** checkbox on the **Configuration** tab is not selected.  
    >   
    >  The server environment that contains the variable is not selected in the list box on the **Configuration** tab.  
  
10. To create a schedule for a job step, click **Schedules** in the **Select a page** pane. For information on how to configure a schedule, see [Schedule a Job](../../ssms/agent/schedule-a-job.md).  
  
    > [!TIP]  
    >  When you name the schedule, consider using a name that is unique and descriptive so you can more easily distinguish the schedule from other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedules.  

## See Also  
 [Execution of Projects and Packages](deploy-integration-services-ssis-projects-and-packages.md)  

## External Resources  
  
-   Knowledge Base article, [An SSIS package does not run when you call the SSIS package from a SQL Server Agent job step](https://support.microsoft.com/kb/918760), on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Web site  
  
-   Video, [Troubleshooting: Package Execution Using SQL Server Agent (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=141772), in the MSDN Library  
  
-   Video, [How to: Automate Package Execution by Using the SQL Server Agent (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=141771), in the MSDN Library  
  
-   Technical article, [Checking SQL Server Agent jobs using Windows PowerShell](https://go.microsoft.com/fwlink/?LinkId=165675), on mssqltips.com  
  
-   Technical article, [Auto alert for SQL Agent jobs when they are enabled or disabled](https://go.microsoft.com/fwlink/?LinkId=165676), on mssqltips.com  
  
-   Blog entry, [Configuring SQL Agent Jobs to Write to Windows Event Log](https://go.microsoft.com/fwlink/?LinkId=220745), on mssqltips.com.  
  
  
