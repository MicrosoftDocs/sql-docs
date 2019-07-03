---
title: "Integration Services Service (SSIS Service) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssiseditserverregistration.connectionproperties.f1"
  - "sql13.swb.connecttodts.connectionproperties.f1"
  - "sql13.swb.connection.login.dtsserver.f1"
  - "sql13.swb.connecttodts.login.f1"
  - "sql13.swb.connecttodtsserver.login.f1"
helpviewer_keywords: 
  - "Integration Services service, about Integration Services service"
  - "SQL Server Integration Services service"
  - "service [Integration Services],about Integration Services service"
  - "service [Integration Services]"
  - "SQL Server Integration Services, service"
ms.assetid: 2c785b3b-4a0c-4df7-b5cd-23756dc87842
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services Service (SSIS Service)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The topics in this section discuss the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, a Windows service for managing [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. This service is not required to create, save, and run Integration Services packages. [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] supports the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service for backward compatibility with earlier releases of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
 Starting in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] stores objects, settings, and operational data in the **SSISDB** database for projects that you've deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server using the project deployment model. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, which is an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine, hosts the database. For more information about the database, see [SSIS Catalog](../../integration-services/catalog/ssis-catalog.md). For more information about deploying projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
## Management capabilities  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is a Windows service for managing [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is available only in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 Running the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service provides the following management capabilities:  
  
-   Starting remote and locally stored packages  
  
-   Stopping remote and locally running packages  
  
-   Monitoring remote and locally running packages  
  
-   Importing and exporting packages  
  
-   Managing package storage  
  
-   Customizing storage folders  
  
-   Stopping running packages when the service is stopped  
  
-   Viewing the Windows Event log  
  
-   Connecting to multiple [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] servers  
  
## Startup type
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is installed when you install the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is started and the startup type of the service is set to automatic. The service must be running to monitor the packages that are stored in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store. The [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Store can be either the msdb database in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the designated folders in the file system.  
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is not required if you only want to design and execute [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. However, the service is required to list and monitor packages using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  

## Manage the service
  
 When you install the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is also installed. By default, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is started and the startup type of the service is set to automatic. However, you must also install [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to use the service to manage stored and running [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.  
  
> [!NOTE]
> To connect directly to an instance of the legacy Integration Services Service, you have to use the version of SQL Server Management Studio (SSMS) aligned with the version of SQL Server on which the Integration Services Service is running. For example, to connect to the legacy Integration Services Service running on an instance of SQL Server 2016, you have to use the version of SSMS released for SQL Server 2016. [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).
>
>   In the SSMS **Connect to Server** dialog box, you cannot enter the name of a server on which an earlier version of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is running. However, to manage packages that are stored on a remote server, you do not have to connect to the instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on that remote server. Instead, edit the configuration file for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service so that [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] displays the packages that are stored on the remote server.   
  
 You can only install a single instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on a computer. The service is not specific to a particular instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. You connect to the service by using the name of the computer on which it is running.  
  
 You can manage the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service by using one of the following Microsoft Management Console (MMC) snap-ins: SQL Server Configuration Manager or Services. Before you can manage packages in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must make sure that the service is started.  
  
 By default, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured to manage packages in the msdb database of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that is installed at the same time as [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. If an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is not installed at the same time, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured to manage packages in the msdb database of the local, default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. To manage packages that are stored in a named or remote instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or in multiple instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], you have to modify the configuration file for the service.
  
 By default, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured to stop running packages when the service is stopped. However, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service does not wait for packages to stop and some packages may continue running after the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is stopped.  
  
 If the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is stopped, you can continue to run packages using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard, the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the Execute Package Utility, and the **dtexec** command prompt utility (dtexec.exe). However, you cannot monitor the running packages.  
  
 By default, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service runs in the context of the NETWORK SERVICE account.  
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service writes to the Windows event log. You can view service events in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can also view service events by using the Windows Event Viewer.  
  
## Set the properties of the service
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service manages and monitors packages in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. When you first install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is started and the startup type of the service is set to automatic.  
  
 After the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service has been installed, you can set the properties of the service by using either SQL Server Configuration Manager or the Services MMC snap-in.  
  
 To configure other important features of the service, including the locations where it stores and manages packages, you must modify the configuration file of the service.
  
### To set properties of the Integration Services service by using SQL Server Configuration Manager  
  
1.  On the **Start** menu, point to **All Programs**, point to **Microsoft SQL Server**, point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  In the **SQL Server Configuration Manager** snap-in, locate **SQL Server Integration Services** in the list of services, right-click **SQL Server Integration Services**, and then click **Properties**.  
  
3.  In the **SQL Server Integration Services Properties** dialog box you can do the following:  
  
    -   Click the **Log On** tab to view the logon information such as the account name.  
  
    -   Click the **Service** tab to view information about the service such as the name of the host computer and to specify the start mode of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
        > [!NOTE]  
        >  The **Advanced** tab contains no information for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
4.  Click **OK**.  
  
5.  On the **File** menu, click **Exit** to close the **SQL Server Configuration Manager** snap-in.  
  
### To set properties of the Integration Services service by using Services  
  
1.  In **Control Panel**, if you are using Classic View, click **Administrative Tools**, or, if you are using Category View, click **Performance and Maintenance** and then click **Administrative Tools**.  
  
2.  Click **Services**.  
  
3.  In the **Services** snap-in, locate **SQL Server Integration Services** in the list of services, right-click **SQL Server Integration Services**, and then click **Properties**.  
  
4.  In the **SQL Server Integration Services Properties** dialog box, you can do the following:  
  
    -   Click the **General** tab. To enable the service, select either the manual or automatic startup type. To disable the service, select Disable in the **Startup type** box. Selecting Disable does not stop the service if it is currently running.  
  
         If the service is already enabled, you can click **Stop** to stop the service, or click **Start** to start the service.  
  
    -   Click the **Log On** tab to view or edit the logon information.  
  
    -   Click the **Recovery** tab to view the default computer responses to service failure. You can modify these options to suit your environment.  
  
    -   Click the **Dependencies** tab to view a list of dependent services. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service has no dependencies.  
  
5.  Click **OK**.  
  
6.  Optionally, if the startup type is Manual or Automatic, you can right-click **SQL Server Integration Services** and click **Start, Stop, or Restart**.  
  
7.  On the **File** menu, click **Exit** to close the **Services** snap-in.  

## Grant permissions to the service
  In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], by default when you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] all users in the Users group had access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. When you install the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], users do not have access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. The service is secure by default. After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, the administrator must grant access to the service.  
  
### To grant access to the Integration Services service  
  
1.  Run Dcomcnfg.exe. Dcomcnfg.exe provides a user interface for modifying certain settings in the registry.  
  
2.  In the **Component Services** dialog, expand the Component Services > Computers > My Computer > DCOM Config node.  
  
3.  Right-click **Microsoft SQL Server Integration Services 13.0**, and then click **Properties**.  
  
4.  On the **Security** tab, click **Edit** in the **Launch and Activation Permissions** area.  
  
5.  Add users and assign appropriate permissions, and then click Ok.  
  
6.  Repeat steps 4 - 5 for Access Permissions.  
  
7.  Restart SQL Server Management Studio.  
  
8.  Restart the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Service.  

### Event logged when permissions are missing

If the service account of the SQL Server Agent doesn't have the Integration Services DCOM **[Launch and Activation Permissions]**, the following event is added to the system event logs when the SQL Server Agent executes the SSIS package jobs:

```
Log Name: System
Source: **Microsoft-Windows-DistributedCOM**
Date: 1/9/2019 5:42:13 PM
Event ID: **10016**
Task Category: None
Level: Error
Keywords: Classic
User: NT SERVICE\SQLSERVERAGENT
Computer: testmachine
Description:
The application-specific permission settings do not grant Local Activation permission for the COM Server application with CLSID
{xxxxxxxxxxxxxxxxxxxxxxxxxxxxx}
and APPID
{xxxxxxxxxxxxxxxxxxxxxxxxxxxxx}
to the user NT SERVICE\SQLSERVERAGENT SID (S-1-5-80-344959196-2060754871-2302487193-2804545603-1466107430) from address LocalHost (Using LRPC) running in the application container Unavailable SID (Unavailable). This security permission can be modified using the Component Services administrative tool.
```

## Configure the service
 
When you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the setup process creates and installs the configuration file for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. This configuration file contains the following settings:  
  
-   Packages are sent a stop command when the service stops.  
  
-   The root folders to display for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] in Object Explorer of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] are the MSDB and File System folders.  
  
-   The packages in the file system that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service manages are located in %ProgramFiles%\Microsoft SQL Server\130\DTS\Packages.  
  
 This configuration file also specifies which msdb database contains the packages that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service will manage. By default, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured to manage packages in the msdb database of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that is installed at the same time as [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. If an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is not installed at the same time, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured to manage packages in the msdb database of the local, default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
### Default Configuration File Example  
 The following example shows a default configuration file that specifies the following settings:  
  
-   Packages stop running when the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service stops.  
  
-   The root folders for package storage in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] are MSDB and File System.  
  
-   The service manages packages that are stored in the msdb database of the local, default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   The service manages packages that are stored in the file system in the Packages folder.  
  
 **Example of a Default Configuration File**  
  
```xml
\<?xml version="1.0" encoding="utf-8"?>  
\<DtsServiceConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
  <StopExecutingPackagesOnShutdown>true</StopExecutingPackagesOnShutdown>  
  <TopLevelFolders>  
    \<Folder xsi:type="SqlServerFolder">  
      <Name>MSDB</Name>  
      <ServerName>.</ServerName>  
    </Folder>  
    \<Folder xsi:type="FileSystemFolder">  
      <Name>File System</Name>  
      <StorePath>..\Packages</StorePath>  
    </Folder>  
  </TopLevelFolders>    
</DtsServiceConfiguration>  
```  
  
### Modify the configuration file  
 You can modify the configuration file to allow packages to continue running if the service stops, to display additional root folders in Object Explorer, or to specify a different folder or additional folders in the file system to be managed by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. For example, you can create additional root folders of type, **SqlServerFolder**, to manage packages in the msdb databases of additional instances of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
> [!NOTE]  
>  Some characters are not valid in folder names. Valid characters for folder names are determined by the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] class **System.IO.Path** and the **GetInvalidFilenameChars** field. The **GetInvalidFilenameChars** field provides a platform-specific array of characters that cannot be specified in path string arguments passed to members of the **Path** class. The set of invalid characters can vary by file system. Typically, invalid characters are the quotation mark ("), less than (<) character, and pipe (|) character.  
  
 However, you will have to modify the configuration file to manage packages that are stored in a named instance or a remote instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)]. If you do not update the configuration file, you cannot use **Object Explorer** in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view packages that are stored in the msdb database on the named instance or the remote instance. If you try to use **Object Explorer** to view these packages, you receive the following error message:  
  
 `Failed to retrieve data for this request. (Microsoft.SqlServer.SmoEnum)`  
  
 `The SQL Server specified in Integration Services service configuration is not present or is not available. This might occur when there is no default instance of SQL Server on the computer. For more information, see the topic "Configuring the Integration Services Service" in SQL Server 2008 Books Online.`  
  
 `Login Timeout Expired`  
  
 `An error has occurred while establishing a connection to the server. When connecting to SQL Server 2008, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections.`  
  
 `Named Pipes Provider: Could not open a connection to SQL Server [2]. (MsDtsSvr).`  
  
 To modify the configuration file for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, you use a text editor.  
  
> [!IMPORTANT]  
>  After you modify the service configuration file, you must restart the service to use the updated service configuration.  
  
### Modified Configuration File Example  
 The following example shows a modified configuration file for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. This file is for a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] called `InstanceName` on a server named `ServerName`.  
  
 **Example of a Modified Configuration File for a Named Instance of SQL Server**  
  
```xml
\<?xml version="1.0" encoding="utf-8"?>  
\<DtsServiceConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
  <StopExecutingPackagesOnShutdown>true</StopExecutingPackagesOnShutdown>  
  <TopLevelFolders>  
    \<Folder xsi:type="SqlServerFolder">  
      <Name>MSDB</Name>  
      <ServerName>ServerName\InstanceName</ServerName>  
    </Folder>  
    \<Folder xsi:type="FileSystemFolder">  
      <Name>File System</Name>  
      <StorePath>..\Packages</StorePath>  
    </Folder>  
  </TopLevelFolders>    
</DtsServiceConfiguration>  
```  
  
### Modify the Configuration File Location  
 The Registry key **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\SSIS\ServiceConfigFile** specifies the location and name for the configuration file that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service uses. The default value of the Registry key is **C:\Program Files\Microsoft SQL Server\130\DTS\Binn\MsDtsSrvr.ini.xml**. You can update the value of the Registry key to use a different name and location for the configuration file. Note that the version number in the path (120 for SQL Server  [!INCLUDE[ssSQL14_md](../../includes/sssql14-md.md)], 130 for  [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], etc.) will vary depending on the SQL Server version.
  
> [!CAUTION]  
>  Incorrectly editing the Registry can cause serious problems that may require you to reinstall your operating system. [!INCLUDE[msCoName](../../includes/msconame-md.md)] cannot guarantee that problems resulting from editing the Registry incorrectly can be resolved. Before editing the Registry, back up any valuable data. For information about how to back up, restore, and edit the Registry, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base article, [Description of the Microsoft Windows registry](https://support.microsoft.com/kb/256986).  
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service loads the configuration file when the service is started. Any changes to the Registry entry require that the service be restarted.  

## Connect to the local service
  Before you connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, the administrator must grant you access to the service. 
  
### To connect to the Integration Services Service  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
2.  Click **Object Explorer** on the **View** menu.  
  
3.  On the Object Explorer toolbar, click **Connect**, and then click **Integration Services**.  
  
4.  In the **Connect to Server** dialog box, provide a server name. You can use a period (.), (local), or **localhost** to indicate the local server.  
  
5.  Click **Connect**.  

## Connect to a remote SSIS server
  
 Connecting to an instance of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on a remote server, from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or another management application, requires a specific set of rights on the server for the users of the application.  
  
> [!IMPORTANT]
> To connect directly to an instance of the legacy Integration Services Service, you have to use the version of SQL Server Management Studio (SSMS) aligned with the version of SQL Server on which the Integration Services Service is running. For example, to connect to the legacy Integration Services Service running on an instance of SQL Server 2016, you have to use the version of SSMS released for SQL Server 2016. [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).
>
>  To manage packages that are stored on a remote server, you do not have to connect to the instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on that remote server. Instead, edit the configuration file for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service so that [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] displays the packages that are stored on the remote server.
  
### Connecting to Integration Services on a Remote Server  
  
#### To connect to Integration Services on a Remote Server  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
2.  Select **File**, **Connect Object Explorer** to display the **Connect to Server** dialog box.  
  
3.  Select **Integration Services** in the **Server type** list.  
  
4.  Type the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server in the **Server name** text box.  
  
    > [!NOTE]  
    >  The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is not instance-specific. You connect to the service by using the name of the computer on which the Integration Services service is running.  
  
5.  Click **Connect**.  
  
> [!NOTE]  
>  The **Browse for Servers** dialog box does not display remote instances of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. In addition, the options available on the **Connection Options** tab of the **Connect to Server** dialog box, which is displayed by clicking the **Options** button, are not applicable to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] connections.  
  
### Eliminating the "Access Is Denied" Error  
 When a user without sufficient rights attempts to connect to an instance of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on a remote server, the server responds with an "Access is denied" error message. You can avoid this error message by ensuring that users have the required DCOM permissions.  
  
#### To configure rights for remote users on Windows Server 2003 or Windows XP  
  
1.  If the user is not a member of the local Administrators group, add the user to the Distributed COM Users group. You can do this in the Computer Management MMC snap-in accessed from the **Administrative Tools** menu.  
  
2.  Open Control Panel, double-click **Administrative Tools,** and then double-click **Component Services** to start the Component Services MMC snap-in.  
  
3.  Expand the **Component Services** node in the left pane of the console. Expand the **Computers** node, expand **My Computer**, and then click the **DCOM Config** node.  
  
4.  Select the **DCOM Config** node, and then select SQL Server Integration Services 11.0 in the list of applications that can be configured.  
  
5.  Right-click on SQL Server Integration Services 11.0 and select **Properties**.  
  
6.  In the **SQL Server Integration Services 11.0 Properties** dialog box, select the **Security** tab.  
  
7.  Under **Launch and Activation Permissions**, select **Customize**, then click **Edit** to open the **Launch Permission** dialog box.  
  
8.  In the **Launch Permission** dialog box, add or delete users, and assign the appropriate permissions to the appropriate users and groups. The available permissions are Local Launch, Remote Launch, Local Activation, and Remote Activation. The Launch rights grant or deny permission to start and stop the service; the Activation rights grant or deny permission to connect to the service.  
  
9. Click OK to close the dialog box.  
  
10. Under **Access Permissions**, repeat steps 7 and 8 to assign the appropriate permissions to the appropriate users and groups.  
  
11. Close the MMC snap-in.  
  
12. Restart the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
#### To configure rights for remote users on Windows 2000 with the latest service packs  
  
1.  Run **dcomcnfg.exe** at the command prompt.  
  
2.  On the **Applications** page of the **Distributed COM Configuration Properties** dialog box, select SQL Server Integration Services 11.0 and then click **Properties**.  
  
3.  Select the **Security** page.  
  
4.  Use the two separate dialog boxes to configure **Access Permissions** and **Launch Permissions**. You cannot distinguish between remote and local access - Access permissions include local and remote access, and Launch permissions include local and remote launch.  
  
5.  Close the dialog boxes and **dcomcnfg.exe**.  
  
6.  Restart the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
### Connecting by using a Local Account  
 If you are working in a local Windows account on a client computer, you can connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on a remote computer only if a local account that has the same name and password and the appropriate rights exists on the remote computer.  
  
### By default the SSIS service does not support delegation  
By default the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service does not support the delegation of credentials, or what is sometimes referred to as a double hop. In this scenario, you are working on a client computer, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is running on a second computer, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running on a third computer. First, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] successfully passes your credentials from the client computer to the second computer on which the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is running. Then, however, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service cannot delegate your credentials from the second computer to the third computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.

You can enable delegation of credentials by granting the **Trust this user for delegation to any service (Kerberos Only)** right to the SQL Server service account, which launches the Integration Services service (ISServerExec.exe) as a child process. Before you grant this right, consider whether it meets the security requirements of your organization.

For more info, see [Getting Cross Domain Kerberos and Delegation working with SSIS Package](https://blogs.msdn.microsoft.com/psssql/2014/06/26/getting-cross-domain-kerberos-and-delegation-working-with-ssis-package/).
 
## Configure the firewall
  
 The Windows firewall system helps prevent unauthorized access to computer resources over a network connection. To access [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] through this firewall, you have to configure the firewall to enable access.  
  
> [!IMPORTANT]  
>  To manage packages that are stored on a remote server, you do not have to connect to the instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on that remote server. Instead, edit the configuration file for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service so that [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] displays the packages that are stored on the remote server.
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service uses the DCOM protocol.
  
 There are many firewall systems available. If you are running a firewall other than Windows firewall, see your firewall documentation for information that is specific to the system you are using.  
  
 If the firewall supports application-level filtering, you can use the user interface that Windows provides to specify the exceptions that are allowed through the firewall, such as programs and services. Otherwise, you have to configure DCOM to use a limited set of TCP ports. The Microsoft website link previously provided includes information about how to specify the TCP ports to use.  
  
 The Integration Services service uses port 135, and the port cannot be changed. You have to open TCP port 135 for access to the service control manager (SCM). SCM performs tasks such as starting and stopping [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] services and transmitting control requests to the running service.  
  
 The information in the following section is specific to Windows firewall. You can configure the Windows firewall system by running a command at the command prompt, or by setting properties in the Windows firewall dialog box.  
  
 For more information about the default Windows firewall settings, and a description of the TCP ports that affect the Database Engine, Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
### Configuring a Windows firewall  
 You can use the following commands to open TCP port 135, add MsDtsSrvr.exe to the exception list, and specify the scope of unblocking for the firewall.  
  
#### To configure a Windows firewall using the Command Prompt window  
  
1.  Run the following command:

    ```dos
    netsh firewall add portopening protocol=TCP port=135 name="RPC (TCP/135)" mode=ENABLE scope=SUBNET
    ```
  
2.  Run the following command:

    ```dos
    netsh firewall add allowedprogram program="%ProgramFiles%\Microsoft SQL Server\100\DTS\Binn\MsDtsSrvr.exe" name="SSIS Service" scope=SUBNET
    ```
  
    > [!NOTE]  
    >  To open the firewall for all computers, and also for computers on the Internet, replace scope=SUBNET with scope=ALL.  
  
 The following procedure describes how to use the Windows user interface to open TCP port 135, add MsDtsSrvr.exe to the exception list, and specify the scope of unblocking for the firewall.  
  
#### To configure a firewall using the Windows firewall dialog box  
  
1.  In the Control Panel, double-click **Windows Firewall**.  
  
2.  In the **Windows Firewall** dialog box, click the **Exceptions** tab and then click **Add Program.**  
  
3.  In the **Add a Program** dialog box, **click Browse**, navigate to the Program Files\Microsoft SQL Server\100\DTS\Binn folder, click MsDtsSrvr.exe, and then click **Open**. Click **OK** to close the **Add a Program** dialog box.  
  
4.  On the **Exceptions** tab, click **Add Port.**  
  
5.  In the **Add a Port** dialog box, type **RPC(TCP/135)** or another descriptive name in the **Nam**e box, type **135** in the **Port Number** box, and then select **TCP**.  
  
    > [!IMPORTANT]  
    >  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service always uses port 135. You cannot specify a different port.  
  
6.  In the **Add a Port** dialog box, you can optionally click **Change Scope** to modify the default scope.  
  
7.  In the **Change Scope** dialog box, select **My network (subnet only)** or type a custom list, and then click **OK**.  
  
8.  To close the **Add a Port** dialog box, click **OK**.  
  
9. To close the **Windows Firewall** dialog box, click **OK**.  
  
    > [!NOTE]  
    >  To configure the Windows firewall, this procedure uses the **Windows Firewall** item in Control Panel. The **Windows Firewall** item only configures the firewall for the current network location profile. However, you can also configure the Windows firewall by using the **netsh** command line tool or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (MMC) snap-in named Windows firewall with Advanced Security. For more information about these tools, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
