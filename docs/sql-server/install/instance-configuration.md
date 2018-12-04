---
title: "Installation Wizard Help | Microsoft Docs"
ms.custom: ""
ms.date: "2017-04-21"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
f1_keywords: 
  - "instance configuration, Setup"
helpviewer_keywords: 
  - "Instance Name page [SQL Server Installation Wizard]"
  - "SQL Server Installation Wizard, Instance Name page"
ms.assetid: 5bf822fc-6dec-4806-a153-e200af28e9a5
author: MashaMSFT
ms.author: mathoma
manager: craigg
robots: noindex,nofollow
---
# Installation Wizard Help
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes some of the configuration pages in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard. 

## Instance Configuration
Use the **Instance Configuration** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to specify whether to create a default instance or a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not already installed, a default instance will be created unless you specify a named instance.  
  
Each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] consists of a distinct set of services that have specific settings for collations and other options. The directory structure, registry structure, and service names all reflect the instance name and a specific instance ID created during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
 An instance is either the default instance or a named instance. The default instance name is MSSQLSERVER. It does not require a client to specify the name of the instance to make a connection. A named instance is determined by the user during Setup. You can install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a named instance without installing the default instance first. Only one installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], regardless of version, can be the default instance at one time.  
  
> [!NOTE]  
> With [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep, you can specify the instance name when you complete a prepared instance on the **Instance Configuration** page. You can choose to configure the prepared instance you are completing as a default instance if there is no existing default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the machine.  
  
### Multiple Instances  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a single server or processor, but only one instance can be the default instance. All others must be named instances. A computer can run multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] concurrently, and each instance runs independently of other instances.  
  
 For more information, see [Maximum Capacity Specifications for SQL Server](../maximum-capacity-specifications-for-sql-server.md).  
  
### Options  
 Failover cluster instances only - Specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster network name. This name identifies the failover cluster instance on the network.  
  
 Default or Named instance - Consider the following information when you decide whether to install a default or named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   If you plan to install a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a database server, it should be a default instance.  
  
-   Use a named instance for situations where you plan to have multiple instances on the same computer. A server can host only one default instance.  
  
-   Any application that installs [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] should install it as a named instance. This will minimizes conflict when multiple applications are installed on the same computer.  
  
 **Default instance**  
 Select this option to install a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A computer can host only one default instance; all other instances must be named. However, if you have a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed, you can add a default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to the same computer.  
  
 **Named instance**  
 Select this option to create a new named instance. Be aware of the following when you name an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   Instance names are not case sensitive.  
  
-   Instance names cannot start or end with an underscore (_).  
  
-   Instance names cannot contain the term "Default" or other reserved keywords. If a reserved keyword is used in an instance name, a Setup error will occur. For more information, see [Reserved Keywords &#40;Transact-SQL&#41;](../../t-sql/language-elements/reserved-keywords-transact-sql.md).  
  
-   If you specify MSSQLServer for the instance name, a default instance will be created.  
  
-   An installation of [!INCLUDE[ssGeminiLong](../../includes/ssgeminilong-md.md)] is always installed as a named instance of '[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]'. You cannot specify a different instance name for this feature role.  
  
-   Instance names are limited to 16 characters.  
  
-   The first character in the instance name must be a letter. Acceptable letters are those defined by the Unicode Standard 2.0. These include Latin characters a-z, A-Z, and letter characters from other languages.  
  
-   Subsequent characters can be letters defined by the Unicode Standard 2.0, decimal numbers from Basic Latin or other national scripts, the dollar sign ($), or an underscore (_).  
  
-   Embedded spaces or other special characters are not allowed in instance names. The backslash (\\), comma (,), colon (:), semi-colon (;), single quote ('), ampersand (&), hyphen (-), and at sign (@) are also not allowed.  
  
     Only characters that are valid in the current Windows code page can be used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance names. If an unsupported Unicode character is used, a Setup error will occur.  
  
 **Detected instances and features**  
 View a list of installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances and components on the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running.  
  
 **Instance ID** - By default, the instance name is used as the Instance ID. This is used to identify installation directories and registry keys for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. For a default instance, the instance name and instance ID would be MSSQLSERVER. To use a non-default instance ID, specify it in the **Instance ID** field.  
  
> [!IMPORTANT]  
>  With [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep, the Instance ID displayed on this page is the Instance ID specified during the prepare image step of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep process. You will not be able to specify a different Instance ID during the complete image step.  
  
> [!NOTE]  
>  Instance IDs that begin with an underscore (_) or that contain the number sign (#) or the dollar sign ($) are not supported.  
  
 For more information about directories, file locations, and instance ID naming, see [File Locations for Default and Named Instances of SQL Server](file-locations-for-default-and-named-instances-of-sql-server.md).  
  
 All components of a given instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are managed as a unit. All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades will apply to every component of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 All components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that share the same instance name must meet the following criteria:  
  
-   **Same version**  
  
-   **Same edition**  
  
-   **Same language settings**  
  
-   **Same clustered state**  
  
    > [!NOTE]  
    >  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is not cluster-aware.  
  
-   **Same operating system**  
  
## Analysis Services Configuration - Account Provisioning
  Use this page to set the server mode, and to grant administrative permissions to users or services requiring unrestricted access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Setup does not automatically add the local Windows Group BUILTIN\Administrators to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server administrator role of the instance you are installing. If you want to add the local Administrators group to the server administrator role, you must explicitly specify that group.  
  
 If you are installing [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], be sure to grant administrative permissions to SharePoint farm administrators or service administrators who are responsible for a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server deployment in a [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] farm.  
  
### Options  
 **Server Mode** - The server mode specifies the type of Analysis Services databases that can be deployed to the server. Server modes are determined during Setup and cannot be modified later. Each mode is mutually exclusive, which means that you will need two instances of Analysis Services, each configured for a different mode, to support both classic OLAP and tabular model solutions.  
  
 **Specify Administrators** - You must specify at least one server administrator for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The users or groups that you specify will become members of the server administrator role of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance you are installing. These must be Windows domain user accounts in the same domain as the computer on which you are installing the software.  
  
> [!NOTE]  
>  User Account Control (UAC) is a Windows security feature that requires an administrator to specifically approve administrative actions or applications before they are allowed to run. Because UAC is on by default, you will be prompted to allow specific operations that require elevated privileges. You can configure UAC to change the default behavior or customize UAC for specific programs. For more information about UAC and UAC configuration, see [User Account Control Step by Step Guide](https://go.microsoft.com/fwlink/?linkid=196350) and [User Account Control (Wikipedia)](https://go.microsoft.com/fwlink/?linkid=196351).  
  
### See Also  
 [Configure Service Accounts &#40;Analysis Services&#41;](../../analysis-services/instances/configure-service-accounts-analysis-services.md)
 [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)  

 ## Analysis Services Configuration - Data Directories
  The default directories in the following table are user-configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. Permission to access these files is granted to local administrators and to members of the SQLServerMSASUser$\<instance> security group that is created and provisioned during Setup.  
  
### UIElement List  
  
|Description|Default directory|Recommendations|  
|-----------------|-----------------------|---------------------|  
|Data root directory|C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Data\ |Ensure that the \Program files\Microsoft SQL Server\ folder is protected with limited permissions. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] performance depends, in many configurations, on the performance of the storage on which the data directory is located. Place this directory on the highest performing storage that is attached to the system. For failover cluster installations, ensure that data directories are placed on the shared disk.|  
|Log file directory|C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Log\ |This is the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files, and it includes the FlightRecorder log. If you increase the flight recorder duration, ensure that the log directory has adequate space.|  
|Temp directory|C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Temp\ |Place the Temp directory on the high performance storage subsystem.|  
|Backup directory|C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Backup\ |This is the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] default backup files. For [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installations, it also where the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Services caches [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data files.<br /><br /> Ensure appropriate permissions are set to prevent data loss, and that the user group for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories is not supported.|  
  
### Notes  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are deployed on a SharePoint farm store application files, data files, and properties in content databases and service application databases.  
  
-   When you add features to an existing installation, you cannot change the location of a previously installed feature, nor can you specify the location for a new feature.  

-   You might need to configure scanning software, such as antivirus and antispyware applications, to exclude SQL Server folders and file types. Review this support article for more information: [Antivirus software on computers running SQL Server](https://support.microsoft.com/kb/309422).
  
-   If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be installed to separate directories.  
  
-   Program files and data files cannot be installed in the following situations:  
  
    -   On a removable disk drive  
  
    -   On a file system that uses compression  
  
    -   To a directory where system files are located  
  
### See Also  
 For more information about directories, file locations, and instance ID naming, see [File Locations for Default and Named Instances of SQL Server](file-locations-for-default-and-named-instances-of-sql-server.md).  
  
## Database Engine Configuration - Data Directories
  Use this page to specify the installation location for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)] program and data files. Based on the type of installation, the supported storage may include local disk, shared storage, or an SMB file server.  
  
 To specify an SMB file share as a directory, you must manually type the supported UNC path. Browsing to an SMB file share is not supported. The following is a supported UNC path format of an SMB file share: \\\Servername\ShareName\\....  
  
### Stand-Alone Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and the default directories for a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are user configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
### UIElement List  
  
|Description|Supported Storage Type|Default directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|Data root directory|Local Disk, SMB File Server, Shared Storage* |C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\ |[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.|  
|User database directory|Local Disk, SMB File Server, Shared Storage*|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data |Best practices for user data directories depend on workload and performance requirements.|  
|User database log directory|Local Disk, SMB File Server, Shared Storage*|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data|Ensure that the log directory has adequate space.|  
|Backup directory|Local Disk, SMB File Server, Shared Storage*|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Backup|Set appropriate permissions to prevent data loss, and ensure that the user account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories is not supported.|  
  
 *Although shared disks are supported, it is not a recommended practice for a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Failover Cluster Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and the default directories for a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are user configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
|Description|Supported Storage Type|Default Directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|Data root Directory|Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.|  
|User database directory|Shared Storage, SMB File Server|\<Drive:>Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Best practices for user data directories depend on workload and performance requirements.|  
|User database log directory|Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Ensure that the log directory has adequate space.|  
|Backup directory|Local Disk, Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Backup<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Set appropriate permissions to prevent data loss, and ensure that the user account for the SQL Server service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories is not supported.|  
  
### Security Considerations  
 Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.  
  
 The following recommendations apply to the SMB file server:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must be a domain account if an SMB file server is used.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have FULL CONTROL NTFS permissions on the SMB file share folder used as the data directory.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be granted SeSecurityPrivilege privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the **Manage auditing and security log** policy. This setting is available in the **User Rights Assignments** section under **Local Policies** in the **Local Security Policy** console.  
  
### Notes  
  
-   When adding features to an existing installation, you cannot change the location of a previously installed feature, nor can you specify the location for a new feature.  
  
-   If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be installed to separate directories.  
  
-   Program files and data files cannot be installed in the following situations:  
  
    -   On a removable disk drive  
  
    -   On a file system that uses compression  
  
    -   To a directory where system files are located  
  
    -   On a mapped network drive on a failover cluster instance  
  
### See Also  
### Analysis Services Configuration - Data Directories
  The default directories in the following table are user-configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. Permission to access these files is granted to local administrators and to members of the SQLServerMSASUser$\<instance> security group that is created and provisioned during Setup.  
  
#### UIElement List  
  
|Description|Default directory|Recommendations|  
|-----------------|-----------------------|---------------------|  
|Data root directory |C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Data |Ensure that the \Program files\Microsoft SQL Server\ folder is protected with limited permissions. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] performance depends, in many configurations, on the performance of the storage on which the data directory is located. Place this directory on the highest performing storage that is attached to the system. For failover cluster installations, ensure that data directories are placed on the shared disk.|  
|Log file directory|C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Log |This is the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files, and it includes the FlightRecorder log. If you increase the flight recorder duration, ensure that the log directory has adequate space.|  
|Temp directory|C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Temp |Place the Temp directory on the high performance storage subsystem.|  
|Backup directory|C:\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Backup |This is the directory for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] default backup files. For [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installations, it also where the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Services caches [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data files.<br /><br /> Ensure appropriate permissions are set to prevent data loss, and that the user group for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories is not supported.|  
  
#### Notes  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances that are deployed on a SharePoint farm store application files, data files, and properties in content databases and service application databases.  
  
-   When you add features to an existing installation, you cannot change the location of a previously installed feature, nor can you specify the location for a new feature.  

-   You might need to configure scanning software, such as antivirus and antispyware applications, to exclude SQL Server folders and file types. Review this support article for more information: [Antivirus software on computers running SQL Server](https://support.microsoft.com/kb/309422).
  
-   If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be installed to separate directories.  
  
-   Program files and data files cannot be installed in the following situations:  
  
    -   On a removable disk drive  
  
    -   On a file system that uses compression  
  
    -   To a directory where system files are located  
  
#### See Also  
 For more information about directories, file locations, and instance ID naming, see [File Locations for Default and Named Instances of SQL Server](file-locations-for-default-and-named-instances-of-sql-server.md).  
  
    
 [Share and NTFS Permissions on a File Server](https://go.microsoft.com/fwlink/?LinkID=206571) 

## Database Engine Configuration - Filestream
  Use this page to enable FILESTREAM for this installation of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. FILESTREAM integrates the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] with an NTFS file system by storing **varbinary(max)** binary large object (BLOB) data as files on the file system. [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can insert, update, query, search, and back up FILESTREAM data. Win32 file system interfaces provide streaming access to the data.  
  
### UIElement List  
 **Enable FILESTREAM for Transact-SQL access**  
 Select to enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access. This control must be checked before the other control options will be available.  
  
 **Enable FILESTREAM for file I/O streaming access**  
 Select to enable Win32 streaming access for FILESTREAM.  
  
 **Windows share name**  
 Use this control to enter the name of the Windows share in which the FILESTREAM data will be stored.  
  
 **Allow remote clients to have streaming access to FILESTREAM data**  
 Select this control to allow remote clients to access this FILESTREAM data on this server.  
  
### See Also  
 [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  

  
## Database Engine Configuration - Server Configuration
  Use this page to set the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security mode, and to add Windows users or groups as administrators of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
### Considerations for Running [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
 On previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **BUILTIN\Administrators** group was provisioned as a login in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and members of the local Administrators group could login using their Administrator credentials. Using elevated permissions is not a best practice. In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] the **BUILTIN\Administrators** group is not provisioned as a login. As a result, you should create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for each administrative user, and add that login to the sysadmin fixed server role during installation of a new instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. You should also do this for Windows accounts that are used to run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] agent jobs. These include replication agent jobs.  
  
### Options  
 **Security Mode** - Select Windows Authentication or Mixed Mode Authentication for your installation.  
  
 **Windows Principal Provisioning** - In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the Windows Builtin\Administrator local group was placed into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin server role, effectively granting Windows administrators access to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the Builtin\Administrator group is not provisioned in the sysadmin server role. Instead, you should explicitly provision [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators for new installations during Setup.  
  
> [!IMPORTANT]  
>  You must explicitly provision [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators for new installations during Setup. Setup will not allow you to continue until you complete this step.  
  
 **Specify [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrators** - You must specify at least one Windows principal for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, click the **Current User** button. To add or remove accounts from the list of system administrators, click **Add** or **Remove**, and then edit the list of users, groups, or computers that will have administrator privileges for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 When you are finished editing the list, click **OK**, then verify the list of administrators in the configuration dialog. When the list is complete, click **Next**.  
  
 If you select Mixed Mode Authentication, you must provide logon credentials for the builtin [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator (SA) account.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
 **Windows Authentication Mode**  
 When a user connects through a Windows user account, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] validates the account name and password using the Windows principal token in the operating system. This is the default authentication mode, and is much more secure than Mixed Mode. Windows Authentication utilizes Kerberos security protocol, provides password policy enforcement in terms of complexity validation for strong passwords, provides support for account lockout, and supports password expiration.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] Never set a blank or weak sa password.  
  
 **Mixed Mode (Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication)**  
 Allows users to connect by using Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Users who connect through a Windows user account can use trusted connections that are validated by Windows.  
  
 If you must choose Mixed Mode Authentication and you have a requirement for using SQL logins to accommodate legacy applications, you must set strong passwords for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] accounts.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is provided for backward compatibility only. [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
 **Enter Password**  
 Enter and confirm the system administrator (sa) login. Passwords are the first line of defense against intruders, so setting strong passwords is essential to the security of your system. Never set a blank or weak sa password.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] passwords can contain from 1 to 128 characters, including any combination of letters, symbols, and numbers. If you choose Mixed Mode authentication, you must enter a strong sa password before you can continue to the next page of the Installation Wizard.  
  
 **Strong Password Guidelines**  
 Strong passwords are not readily guessed by a person, and are not easily hacked using a computer program. Strong passwords cannot use prohibited conditions or terms, including:  
  
-   A blank or NULL condition  
  
-   "Password"  
  
-   "Admin"  
  
-   "Administrator"  
  
-   "sa"  
  
-   "sysadmin"  
  
 A strong password cannot be the following terms associated with the installation computer:  
  
-   The name of the user currently logged onto the machine.  
  
-   The computer name.  
  
 A strong password must be more than 8 characters in length and satisfy at least three of the following four criteria:  
  
-   It must contain uppercase letters.  
  
-   It must contain lowercase letters.  
  
-   It must contain numbers.  
  
-   It must contain non-alphanumeric characters; for example, #, %, or ^.  
  
 Passwords entered on this page must meet strong password policy requirements. If you have any automation that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, ensure that the password meets strong password policy requirements.  
  
### Related Content  
 For more information about choosing Windows Authentication vs. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, see [Choose an Authentication Mode](../../relational-databases/security/choose-an-authentication-mode.md).  
  
 For more information about choosing an account to run the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], see 
[Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).
  
## Database Engine Configuration - TempDB
  Use this page to specify **tempdb** data and log  file location, size, growth settings, and number of files for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)]. Based on the type of installation, the supported storage may include local disk, shared storage, or an SMB file server.  
  
 To specify an SMB file share as a directory, you must manually type the supported UNC path. Browsing to an SMB file share is not supported. The following is a supported UNC path format of an SMB file share: \\\Servername\ShareName\\....  
  
### Data and log directories for  a stand-alone instance of  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and default directories for stand-alone instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you can configure during setup.  
  
|Description|Supported Storage Type|Default Directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**Data directories**|Local disk, SMB file server, shared storage* |C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.<br /><br /> Best practices for the **temdb** directories depend on workload and performance requirements. Specify multiple folders/drives to spread the data files across several volumes.|  
|**Log directory**|Local disk,  SMB file server, shared storage*|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data|Ensure that the log directory has adequate space.|  
  
 *Although shared disks are supported, it is not a recommended practice for a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Data and log directories for a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and the default directories for a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are user configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
|Description|Supported Storage Type|Default Directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**tempdb** data directory|Local disk, shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.<br /><br /> Ensure that the specified directory or directories (if multiple files are specified) is valid for all the cluster nodes. During failover, if the **tempdb** directories are not available on the failover target node, the SQL Server resource will fail to come online.|  
|**tempdb** log directory|Local disk, shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Best practices for user data directories depend on workload and performance requirements.<br /><br /> Ensure that the specified directory is valid for all the cluster nodes. During failover, if the **tempdb** directories are not available on the failover target node, the SQL Server resource will fail to come online.<br /><br /> Ensure that the log directory has adequate space.|  
  
### UIElement List  
 Configure the settings for **tempdb** according to your workload and requirements. The following settings apply to **tempdb** data files:  
  
-   **Number of files** is the total number of data files for **tempdb**. The default value is the lower of 8 or the number of logical cores detected by setup. As a general rule, if the number of logical processors is less than or equal to 8, use the same number of data files as logical processors. If the number of logical processors is greater than 8, use 8 data files and then if contention continues, increase the number of data files by multiples of 4 (up to the number of logical processors) until the contention is reduced to acceptable levels or make changes to the workload/code. 
  
-   **Initial size (MB)** is the initial size in MB for each **tempdb** data file. The default value is 8 MB (or 4 MB for [!INCLUDE[ssexpress](../../includes/ssexpress_md.md)]). [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)] introduces a maximum initial file size of 262,144 MB (256 GB). [!INCLUDE[sssql15](../../includes/sssql15-md.md)] had a maximum initial file size of 1024 MB. All **tempdb** data files are the same initial size. Because **tempdb** is recreated every time SQL Server starts or fails over you should specify a size that is close to the size required by your workload for normal operation. To further optimize the creation of **tempdb** during start-up, enable [Database Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md).  
  
-   **Total initial size (MB)** is the cumulative size of all of the **tempdb** data files.  
  
-   **Autogrowth (MB)** is the amount of space in megabytes that each **tempdb** data file will automatically grow by when they run out of space. In [!INCLUDE[sssql15](../../includes/sssql15-md.md)] and later all data files will grow at the same time by the amount specified in this setting.  
  
-   **Total autogrowth (MB)** is the cumulative size of each autogrowth event.  
  
-   **Data directories** shows all of the directories that hold **tempdb** data files. When there are multiple directories, data files are placed in directories in a round-robin manner. For example, if you create 3 directories and specify 8 data files, data files number 1, 4, and 7 will be created in the first directory. Data files 2, 5, and 8 will be created in the second directory. Data files 3 and 6 will be in the third directory.  
  
-   To add directories, click **Add...**.  
  
-   To remove a directory, select the directory and click **Remove**.  
  
 **TempDB log file** is the name of the log file. It is created automatically. The following settings apply only to **tempdb** log files:  
  
-   **Initial size (MB)** is the initial size of the **tempdb** log file. The default value is 8 MB (or 4 MB for [!INCLUDE[ssexpress](../../includes/ssexpress_md.md)]). [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)] introduces a maximum initial file size of 262,144 MB (256 GB). [!INCLUDE[sssql15](../../includes/sssql15-md.md)] had a maximum initial file size of 1024 MB. Because **tempdb** is recreated every time SQL Server starts or fails over you should specify a size that is close to the size required by your workload for normal operation. To further optimize the creation of **tempdb** during start-up, enable [Database Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md).  
  
-   **Note:Tempdb** uses minimal logging. The **tempdb** log cannot be backed up. It is recreated every time SQL Server starts or when a cluster instance fails over.  
  
-   **Autogrowth (MB)** is the growth increment of the **tempdb** log in megabytes.  The default value of 64 MB creates the optimal number of virtual log files during initialization.  
  
-   **Note:Tempdb** log files do not use instant file initialization.  
  
-   **Log directory** is the directory where **tempdb** log files are created. There is only one **tempdb** log directory.  
  
### Security Considerations  
 Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.  

 The following recommendations apply to the SMB file server:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must be a domain account if an SMB file server is used.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have FULL CONTROL NTFS permissions on the SMB file share folder used as the data directory.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be granted SeSecurityPrivilege privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the **Manage auditing and security log** policy. This setting is available in the **User Rights Assignments** section under **Local Policies** in the **Local Security Policy** console.  
  
### Notes  
  
-   If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be installed to separate directories.  
  
### See Also  
 [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)   
 [Share and NTFS Permissions on a File Server](https://go.microsoft.com/fwlink/?LinkID=206571)  

## Database Engine Configuration - User Instance
Use the **User Instance** page to generate a separate instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for users without administrator permissions, and to add users to the administrator role.  
  
### Option  
 Enable User Instances  
 Default is on. To disable the functionality of enabling user instances, clear the check box.  
  
 The user instance, also known as a child or client instance, is an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is generated by the parent instance (the primary instance that runs as a service, like [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]) on behalf of a user. The user instance runs as a user process under the security context of that user. The user instance is isolated from the parent instance and any other user instances running on the computer. The user instance feature is also referred to as "Run As Normal User" (RANU).  
  
> [!NOTE]  
>  Logins provisioned as members of the **sysadmin** fixed server role during setup are provisioned as administrators in the template database. They are members **sysadmin** fixed server role on the user instance unless removed  
  
 Add User to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrator role  
 Default is not on. To add the current setup user to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrator role, select the check box.  
  
 [!INCLUDE[wiprlhext](../../includes/wiprlhext-md.md)] users that are members of BUILTIN\Administrators are not automatically added to the sysadmin fixed server role when they connect to [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. Only [!INCLUDE[wiprlhext](../../includes/wiprlhext-md.md)] users that have been explicitly added to a server-level administrator role can administer [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. Any member of the Built-In\Users group can connect to the [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] instance, but they will have limited permissions to perform database tasks. For this reason, users whose [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] privileges are inherited from BUILTIN\Administrators and Built-In\Users in previous releases of Windows must be explicitly granted administrative privileges in instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] running on [!INCLUDE[wiprlhext](../../includes/wiprlhext-md.md)].  
  
 To make any changes to the user roles after this installation program ends, use the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Surface Area Configuration Tool (SQLSAC.exe). To update the list of users in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] System Administrator role, click the **Add New Administrator** link.  
  
 Ensure that the **User to provision** field lists the DomainName\UserName of the user whose permissions should be updated. Select the role to be updated from the list of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances in the **Available privileges** pane, and then click the right arrow. To add the user to all available roles for all available instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances and all available roles, click the double right arrow.  
  
 To implement the changes when your selections are complete, [!INCLUDE[clickOK](../../includes/clickok-md.md)] To end the tool without making changes, click **Cancel**.  
  
  
