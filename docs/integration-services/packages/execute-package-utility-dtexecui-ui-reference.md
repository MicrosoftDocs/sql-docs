---
title: "Execute Package Utility (dtexecui) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.dtexecui.setvalues.f1"
  - "sql13.dts.dtexecui.reporting.f1"
  - "sql13.dts.dtexecui.datasources.f1"
  - "sql13.dts.dtexecui.commandfiles.f1"
  - "sql13.dts.dtexecui.logging.f1"
  - "sql13.dts.dtexecui.general.f1"
  - "sql13.dts.dtexecui.verification.f1"
  - "sql13.dts.dtexecui.executionoptions.f1"
  - "sql13.dts.dtexecui.commandline.f1"
  - "sql13.dts.dtexecui.configuration.f1"
helpviewer_keywords: 
  - "DTExecUI utility"
ms.assetid: 3d71df39-126b-4c8e-bd77-128bbd5b0887
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Execute Package Utility (dtexecui)
  Use the **Execute Package Utility** to run [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. The utility runs packages that are stored in one of three locations: [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store, and the file system. This user interface, which can be opened from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by typing **dtexecui** at a command prompt, is an alternative to running packages by using the **DTExec** command prompt tool.  
  
 Packages execute in the same process as the **dtexecui.exe** utility. Because this utility is a 32-bit tool, packages run by using **dtexecui.exe** in a 64-bit environment run in Windows on Win32 (WOW). When developing and testing commands by using the dtexecui.exe utility on a 64-bit computer, you should test the commands in 64-bit mode by using the 64-bit version of **dtexec.exe** before deploying or scheduling the commands on a production server.  
  
 The **Execute Package Utility** is a graphical user interface for the **DTExec** command prompt tool. The user interface makes it easy to configure options and it automatically assembles the command line that is passed to the **DTExec** command prompt tool when you run the package from the specified options.  
  
 The **Execute Package Utility** can also be used to assemble command lines that you use when running **DTExec** directly.  
  
### To open Execute Package Utility in SQL Server Management Studio  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], on the **View** menu, click **Object Explorer**.  
  
2.  In Object Explorer, click **Connect**, and then click **Integration Services**.  
  
3.  In the **Connect to Server** dialog box, enter the server name in the **Server name** list, and then click **Connect**.  
  
4.  Expand the **Stored Package**s folder and subfolders, right-click the package you want to run, and then click **Run Package**.  
  
### To open the Execute Package Utility at the Command Prompt  
  
-   In a command prompt window, run **dtexecui**.  
  
 The following sections describe pages of the **Execute Package Utility** dialog box.  
  
## General Page  
 Use the **General** page of the **Execute Package Utility** dialog box to specify a package name and location.  
  
 The Execute Package utility (dtexecui.exe) always runs a package on the local computer, even if the package is saved on a remote server. If the remote package uses configuration files that also are saved on the remote server, then the Execute Package utility may not locate the configurations and the package fails. To avoid this problem, the configurations must be referenced by using a Universal Naming Convention (UNC) share name like \\\myserver\myfile.  
  
### Static Options  
 **Package source**  
 Specify the location of the package to run, using the following options:  
  
|||  
|-|-|  
|Value|Description|  
|**SQL Server**|Select this option when the package resides in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Specify an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and provide a user name and password for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Each user name and password adds the **/USER** _username_ and **/PASSWORD** _password_ options to the command prompt.|  
|**File system**|Select this option when the package resides in the file system.|  
|**SSIS Package Store**|Select this option when the package resides in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store.|  
  
 Each of these selections has the following set of options.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
### Dynamic Options  
  
#### Package Source = SQL Server  
 **Server**  
 Type the name of the server where the package resides, or select a server from the list.  
  
 **Log on to the server**  
 Specify whether the package should use Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Windows Authentication is recommended for better security. With Windows Authentication you do not have to specify a user name and password.  
  
 **Use Windows Authentication**  
 Select this option to use Windows Authentication and log on using a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user account.  
  
 **Use SQL Server Authentication**  
 Select this option to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. When a user connects with a specified login name and password from a non-trusted connection, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication by checking to see if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot find the login account, authentication fails, and the user receives an error message.  
  
> [!IMPORTANT]  
>  When possible, use Windows Authentication.  
  
 **Package**  
 Type the name of the package, or click the ellipsis button **(...)** to locate a package using the **Select an SSIS Package** dialog box.  
  
#### Package Source = File System  
 **Package**  
 Type the name of the package, or click the ellipsis button **(...)** to locate a package using the Open dialog box. By default, the dialog box lists only files that have the .dtsx extension.  
  
#### Package Source = SSIS Package Store  
 **Server**  
 Type the name of the computer where the package resides, or select a computer from the list.  
  
 **Log on to the server**  
 Specify whether the package should use Microsoft Windows Authentication to connect to the package source. Windows Authentication is recommended for better security. With Windows Authentication you do not have to specify a user name and password.  
  
 **Use Windows Authentication**  
 Select this option to use Windows Authentication and log on using a Microsoft Windows user account.  
  
 **Use SQL Server Authentication**  
 This option is not available when you run a package stored in the **SSIS Package Store**.  
  
 **Package**  
 Type the name of the package, or click the ellipsis button **(...)** to locate a package using the **Select an SSIS Package** dialog box.  
  
## Configurations Page  
 Use the **Configurations** page of the **Execute Package Utility** dialog box to select the configuration files to load at run time, and to specify the order in which they load.  
  
### Options  
 **Configuration files**  
 Lists the configurations that the package uses. Each configuration file adds a **/CONFIGFILE filename** option to the command prompt.  
  
 **Arrow keys**  
 Select a configuration file in the list, and use the arrow keys at right to change the loading order. Configurations load in order starting from the top of the list.  
  
> [!NOTE]  
>  If multiple configurations modify the same property, the configuration that loads last is used.  
  
 **Add**  
 Click to add configurations using the **Open** dialog box. By default, the dialog box lists only files that have the .dtsconfig extension.  
  
 **Remove**  
 Select a configuration file in the list and then click **Remove**.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Command Files Page  
 Use the **Command Files** page of the **Execute Package Utility** dialog box to select the command files to load at run time.  
  
### Options  
 **Command files**  
 Lists the command files that the package uses. A package can use multiple files to set command-line options.  
  
 **Arrow keys**  
 Select a command file in the list, and use the arrow keys at right to change the loading order. Command files load in order, starting from the top of the list.  
  
 **Add**  
 Click to add a command file, using the **Open** dialog box.  
  
 **Remove**  
 Select a command file in the text box, and then remove it using the **Remove** button.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Connection Managers Page  
 Use the **Connection Managers** page of the **Execute Package Utility** dialog box to edit the connection strings of the connection managers that the package uses.  
  
### Options  
 **Connection Manager**  
 Select its check box to make the **Connection String** column editable.  
  
 **Description**  
 View a description for each connection manager. Descriptions cannot be edited.  
  
 **Connection String**  
 Edit the connection string for a connection manager. This field is editable only when the **Connection Manager** check box is selected.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Execution Options Page  
 Use the **Execution Options** page of the **Execute Package Utility** dialog box to specify run-time options for the package.  
  
### Options  
 **Fail package on validation warnings**  
 Indicate whether the package fails if a validation warning occurs.  
  
 **Validate package without executing**  
 Indicate whether the package is validated only.  
  
 **Maximum concurrent executables**  
 Indicate whether you want to specify the maximum number of executables that can run in the package at the same time. After you select this check box, use the spin box to specify the maximum number of executables.  
  
 **Enable package checkpoints**  
 Indicate whether to enable package checkpoints.  
  
 **Checkpoint file**  
 List the checkpoint file the package uses, if you enable package checkpoints.  
  
 **Browse**  
 Click the browse button **(...)** to locate the checkpoint file using the **Open** dialog box, if you enable package checkpoints. If a checkpoint file is already specified, it is replaced by the selected file.  
  
 **Override restart options**  
 Indicate whether to override restart options, if you enable package checkpoints.  
  
 **Restart option**  
 Select how to use checkpoints, if you override restart options.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Reporting Page  
 Use the **Reporting** page of the **Execute Package Utility** dialog box to specify the events and information about the package to log to the console when the package runs.  
  
### Options  
 **Console events**  
 Indicate the events and types of messages to report.  
  
 **None**  
 Select for no reporting.  
  
 **Errors**  
 Select to report error messages.  
  
 **Warnings**  
 Select to report warning messages.  
  
 **Custom Events**  
 Select to report custom event messages.  
  
 **Pipeline Events**  
 Select to report data flow events messages.  
  
 **Information**  
 Select to report information messages.  
  
 **Verbose**  
 Select to use verbose reporting.  
  
 **Console logging**  
 Specify the information that you want written to the log when the selected event occurs.  
  
 **Name**  
 Select to report the name of the person who created the package.  
  
 **Computer**  
 Select to report the name of the computer the package is running on.  
  
 **Operator**  
 Select to report the name of the person who started the package.  
  
 **Source name**  
 Select to report the package name.  
  
 **Source GUID**  
 Select to report the package GUID.  
  
 **Execution GUID**  
 Select to report the GUID of the package execution instance.  
  
 **Message**  
 Select to report messages.  
  
 **Start time and end time**  
 Select to report when the package began and finished.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Logging Page  
 Use the **Logging** page of the **Execute Package Utility** dialog box to make log providers available to the package at run time. Provide the package log provider type and the connection string for connecting to the log. Each log provider entry adds a **/LOGGER**_classid_ option to the command prompt.  
  
### Options  
 **Log Provider**  
 Select a log provider from the list.  
  
 **Configuration String**  
 Select the name of the connection manager from the package that points to the log location, or type the connection string for connecting to the log provider.  
  
 **Remove**  
 Select a log provider and click to remove it.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Set Values Page  
 Use the **Set Values** page of the **Execute Package Utility** dialog box to set the property values of packages, executables, connections, variables, and log providers by typing the paths of properties and the property values. Each path entry adds a **/SET**_propertypath;value_ option to the command prompt.  
  
### Options  
 **Property Path**  
 Type the path of the property. The path syntax uses a backslash (\\) to indicate that the following item is a container, the period (.) to indicate the following item is a property, and brackets to indicate a collection member. The member can be identified by its index or its name. For example, the property path of a package variable is \Package.Variables[MyVariable].Value.  
  
 **Value**  
 Type the value of the property.  
  
 **Remove**  
 Select a property path and click to remove it.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Verification Page  
 Use the **Verification** page of the **Execute Package** dialog box to set criteria for verifying the package.  
  
### Options  
 **Execute only signed packages**  
 Select to execute only packages that have been signed.  
  
 **Verify package build**  
 Select to verify the package build.  
  
 Build  
 Specify the sequential Build number associated with the build.  
  
 **Verify package ID**  
 Select to verify the package ID.  
  
 Package ID  
 Specify the package identification number.  
  
 **Verify version ID**  
 Select to verify the version ID.  
  
 Version ID  
 Specify the version identification number.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## Command Line Page  
 Use the **Command Line** node of the **Execute Package Utility** dialog box to edit the command line that has been generated by the options created by the various dialogs.  
  
### Options  
 **Restore the original options**  
 Click to restore the command line to its original state. Use this option if you have made modifications using the **Edit the command line manually** option and want to restore the original command-line options.  
  
 **Edit the command line manually**  
 Click to edit the command line in the **Command line** text box.  
  
 **Command line**  
 Displays the current command line. Editable if you selected the option to edit the command line manually.  
  
 **Execute**  
 Click to run the package.  
  
 **Close**  
 Click to close the **Execute Package Utility** dialog box.  
  
## See Also  
 [dtexec Utility](../../integration-services/packages/dtexec-utility.md)  
  
  
