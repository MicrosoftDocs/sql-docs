---
title: "Installation Wizard Help"
description: Specify whether to create a default instance or a named instance of SQL Server by using Instance Configuration in the SQL Server Installation Wizard.
ms.custom:
  - intro-installation
ms.date: 08/16/2019
ms.service: sql
ms.reviewer: ""
ms.subservice: install
ms.topic: conceptual
f1_keywords:
  - "instance configuration, Setup"
helpviewer_keywords:
  - "Instance Name page [SQL Server Installation Wizard]"
  - "SQL Server Installation Wizard, Instance Name page"
author: rwestMSFT
ms.author: randolphwest
---

# Installation Wizard help

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes some of the configuration pages in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard.

## Instance Configuration page

Use the **Instance Configuration** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to specify whether to create a default instance or a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] isn't already installed, a default instance is created unless you specify a named instance.  
  
Each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] consists of a distinct set of services that have specific settings for collations and other options. The directory structure, registry structure, and service names all reflect the instance name and a specific instance ID that were created during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
An instance is either the default instance or a named instance. The default instance name is MSSQLSERVER. The default name doesn't require a client to specify the name of the instance to make a connection. A named instance is determined by the user during Setup. You can install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a named instance without installing the default instance first. Only one installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], no matter the version, can be the default instance at one time.  
  
> [!NOTE]  
> With [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep, you can specify the instance name when you complete a prepared instance on the **Instance Configuration** page. You can configure the prepared instance you're completing as a default instance if there is no existing default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the machine.  
  
### Multiple instances
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a single server or processor, but only one instance can be the default instance. All others must be named instances. A computer can run multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] concurrently, and each instance runs independently of the other instances.  
  
 For more information, see [Maximum capacity specifications for SQL Server](../maximum-capacity-specifications-for-sql-server.md).  
  
### Options

**Failover cluster instances only**: Specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster network name. This name identifies the failover cluster instance on the network.  
  
**Decide between a default or a named instance**: Consider the following information when you decide whether to install a default or named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
* If you plan to install a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a database server, it should be a default instance.  
  
* Use a named instance for situations where you plan to have multiple instances on the same computer. A server can host only one default instance.  
* Any application that installs [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] should install it as a named instance. This practice
minimizes conflicts when multiple applications are installed on the same computer.
  
**Default instance**: Select this option to install a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A computer can host only one default instance; all other instances must be named. However, if you have a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed, you can add a default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to the same computer.  
  
**Named instance**: Select this option to create a new named instance. Be aware of the following information when you name an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
* Instance names aren't case-sensitive.  
  
* Instance names can't start or end with an underscore (_).  
  
* Instance names can't contain the term "Default" or other reserved keywords. If a reserved keyword is used in an instance name, a Setup error occurs. For more information, see [Reserved keywords &#40;Transact-SQL&#41;](../../t-sql/language-elements/reserved-keywords-transact-sql.md).  
  
* If you specify MSSQLSERVER for the instance name, a default instance is created.  
  
* An installation of [!INCLUDE[ssGeminiLong](../../includes/ssgeminilong-md.md)] is always installed as a named instance of "[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]". You can't specify a different instance name for this feature role.  
  
* Instance names are limited to 16 characters.  
  
* The first character in the instance name must be a letter. Acceptable letters are the ones defined by the Unicode Standard 2.0. These letters include Latin characters a-z, A-Z, and letter characters from other languages.  
  
* Subsequent characters can be letters defined by the Unicode Standard 2.0, decimal numbers from Basic Latin or other national scripts, the dollar sign ($), or an underscore (_).  
  
* Embedded spaces or other special characters aren't allowed in instance names. The backslash (\\), comma (,), colon (:), semicolon (;), single quotation mark ('), ampersand (&), hyphen (-), and at sign (@) also aren't allowed.  
  
  Only characters that are valid in the current Windows code page can be used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance names. If an unsupported Unicode character is used, a Setup error occurs.  
  
**Detected instances and features**: View a list of installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances and components on the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running.  
  
**Instance ID**: By default, the instance name is used as the instance ID. This ID is used to identify installation directories and registry keys for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The same behavior occurs for default instances and named instances. For a default instance, the instance name and instance ID are MSSQLSERVER. To use a nondefault instance ID, specify it in the **Instance ID** field.  
  
> [!IMPORTANT]  
>  With [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep, the instance ID displayed on the **Instance Configuration** page is the instance ID you specified during the prepare image step of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep process. You won't be able to specify a different instance ID during the complete image step.

> [!NOTE]  
>  Instance IDs that begin with an underscore (_) or that contain the number sign (#) or the dollar sign ($) aren't supported.  
  
For more information about directories, file locations, and instance ID naming, see [File locations for default and named instances of SQL Server](file-locations-for-default-and-named-instances-of-sql-server.md).  
  
All components of a given instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are managed as a unit. All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades apply to every component of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
All components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that share the same instance name must meet the following criteria:  
  
* Same version
* Same edition
* Same language settings
* Same clustered state
* Same operating system  
  
> [!NOTE]  
> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] isn't cluster-aware.  

## Analysis Services Configuration - Account Provisioning page
  
Use this page to set the server mode and to grant administrative permissions to users or services that require unrestricted access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Setup doesn't automatically add the local Windows group BUILTIN\Administrators to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server administrator role of the instance you're installing. If you want to add the local Administrators group to the server administrator role, you must explicitly specify that group.  
  
If you're installing [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], be sure to grant administrative permissions to SharePoint farm administrators or service administrators who are responsible for a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server deployment in a [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] farm.  
  
### Options

**Server Mode**: The server mode specifies the type of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases that can be deployed to the server. Server modes are determined during Setup and can't be modified later. Each mode is mutually exclusive, which means that you need two instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], each configured for a different mode, to support both classic online analytical processing (OLAP) and tabular model solutions.  
  
**Specify Administrators**: You must specify at least one server administrator for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The users or groups that you specify become members of the server administrator role of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance you're installing. These members must have Windows domain user accounts in the same domain as the computer on which you're installing the software.  
  
> [!NOTE]  
> User Account Control (UAC) is a Windows security feature that requires an administrator to specifically approve administrative actions or applications before they're allowed to run. Because UAC is on by default, you'll be prompted to allow specific operations that require elevated privileges. You can configure UAC to change the default behavior or you can customize UAC for specific programs. For more information about UAC and UAC configuration, see the [User Account Control step-by-step guide](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc709691(v=ws.10)) and [User Account Control (Wikipedia)](https://go.microsoft.com/fwlink/?linkid=196351).  
  
### See also
  
* [Configure service accounts &#40;Analysis Services&#41;](/analysis-services/instances/configure-service-accounts-analysis-services)
* [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)  

## Analysis Services Configuration - Data Directories page

The default directories in the following table are user-configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. Permission to access these files is granted to local administrators and to members of the SQLServerMSASUser$\<instance> security group that's created and provisioned during Setup.  
  
### UI element list  
  
|Description|Default directory|Recommendations|  
|-----------------|-----------------------|---------------------|  
|**Data root directory**|\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Data\ |Ensure that the \Program files\Microsoft SQL Server\ folder is protected with limited permissions. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] performance depends, in many configurations, on the performance of the storage on which the data directory is located. Place this directory on the highest-performing storage that is attached to the system. For failover cluster installations, ensure that data directories are placed on the shared disk.|  
|**Log file directory**|\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Log\ |This directory is for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files, and it includes the FlightRecorder log. If you increase the flight recorder duration, ensure that the log directory has adequate space.|  
|**Temp directory**|\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Temp\ |Place the Temp directory on a high-performance storage subsystem.|  
|**Backup directory**|\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Backup\ |This directory is for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] default backup files. For [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installations, this directory is also where the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Services caches [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data files.<br /><br /> Ensure appropriate permissions are set to prevent data loss, and that the user group for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] has adequate permissions to write to the backup directory. Using a mapped drive for backup directories isn't supported.|  
  
### Considerations  
  
* When [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances are deployed on a SharePoint farm, they store application files, data files, and properties in content databases and service-application databases.  
  
* When you add features to an existing installation, you can't change the location of a previously installed feature, nor can you specify the location for a new feature.  

* You might need to configure scanning software, such as antivirus and antispyware applications, to exclude SQL Server folders and file types. Review this support article for more information: [Antivirus software on computers running SQL Server](https://support.microsoft.com/kb/309422).
  
* If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Don't share directories in this dialog box with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Also install the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to separate directories.  
  
* Program files and data files can't be installed in the following situations:  
  
  * On a removable disk drive  
  * On a file system that uses compression  
  * To a directory where system files are located  
  
### See also

For more information about directories, file locations, and instance ID naming, see [File locations for default and named instances of SQL Server](file-locations-for-default-and-named-instances-of-sql-server.md).  
  
### Analysis Services Configuration - Data Directories page

The default directories in the following table are user-configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. Permission to access these files is granted to local administrators and to members of the SQLServerMSASUser$\<instance> security group that's created and provisioned during Setup.  
  
#### UI element list
  
|Description|Default directory|Recommendations|  
|-----------------|-----------------------|---------------------|  
|**Data root directory** |\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Data |Ensure that the \Program files\Microsoft SQL Server\ folder is protected with limited permissions. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] performance depends, in many configurations, on the performance of the storage on which the data directory is located. Place this directory on the highest-performing storage that's attached to the system. For failover cluster installations, ensure that the data directories are placed on the shared disk.|  
|**Log file directory**|\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Log |This directory is for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] log files, and it includes the FlightRecorder log. If you increase the flight-recorder duration, ensure that the log directory has adequate space.|  
|**Temp directory**|\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Temp |Place the Temp directory on a high-performance storage subsystem.|  
|**Backup directory**|\<Drive:>\Program Files\Microsoft SQL Server\MSAS*nn*.\<InstanceID>\OLAP\Backup |This directory is for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] default backup files. For [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installations, it's also where the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Services caches [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data files.<br /><br /> Ensure appropriate permissions are set to prevent data loss, and that the user group for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories isn't supported.|  
  
#### Considerations
  
* When [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances are deployed on a SharePoint farm, they store application files, data files, and properties in content databases and service-application databases.  
  
* When you add features to an existing installation, you can't change the location of a previously installed feature, nor can you specify the location for a new feature.  

* You might need to configure scanning software, such as antivirus and antispyware applications, to exclude SQL Server folders and file types. For more information, see [Antivirus software on computers running SQL Server](https://support.microsoft.com/kb/309422).
  
* If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Don't share the directories in this dialog box with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Also install the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to separate directories.  
  
* Program files and data files can't be installed in the following situations:  
  
  * On a removable disk drive  
  * On a file system that uses compression  
  * To a directory where system files are located  
  
#### See also

* For more information about directories, file locations, and instance ID naming, see [File locations for default and named instances of SQL Server](file-locations-for-default-and-named-instances-of-sql-server.md)  
* [Share and NTFS permissions on a file server](/iis/web-hosting/configuring-servers-in-the-windows-web-platform/configuring-share-and-ntfs-permissions)

## <a name="serverconfig"></a> Database Engine Configuration - Server Configuration page

Use this page to set the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security mode and to add Windows users or groups as administrators of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
### Considerations for running newer versions of SQL Server

In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the BUILTIN\Administrators group was provisioned as a login in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and members of the local Administrators group could log in by using their Administrator credentials. However, using elevated permissions isn't a best practice.

In newer versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the BUILTIN\Administrators group isn't provisioned as a login. Make sure you create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for each administrative user, and add that login to the **sysadmin** fixed server role during the installation of a new instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Do the same for Windows accounts that run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] agent jobs, including replication agent jobs.  
  
### Options

**Security Mode**: Select **Windows Authentication** or **Mixed Mode Authentication** for your installation.  
  
**Windows Principal Provisioning**: In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the Windows BUILTIN\Administrators local group was placed into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **sysadmin** server role, effectively granting Windows administrators access to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In newer versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the BUILTIN\Administrators group isn't provisioned in the **sysadmin** server role. Instead, you should explicitly provision [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators for new installations during Setup.  
  
> [!IMPORTANT]  
> You must explicitly provision [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators for new installations during Setup. Setup won't allow you to continue until you complete this step.
  
**Specify [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrators**: You must specify at least one Windows principal for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, select the **Add Current User** button. To add or remove accounts from the list of system administrators, select **Add** or **Remove**, and then edit the list of users, groups, or computers that will have administrator privileges for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
When you're finished editing the list, select **OK**, then verify the list of administrators in the configuration dialog box. If the list is complete, select **Next**.  
  
If you select **Mixed Mode Authentication**, you must provide login credentials for the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator (**sa**) account.  
  
> [!IMPORTANT]  
> [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
**Windows Authentication Mode**: When a user connects through a Windows user account, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] validates the account name and password by using the Windows principal token in the operating system. Windows authentication is the default authentication mode, and it's much more secure than mixed mode authentication. Windows authentication uses the Kerberos security protocol, provides password-policy enforcement in terms of complexity validation for strong passwords, provides support for account lockout, and supports password expiration.  
  
> [!IMPORTANT]  
> [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  

> [!IMPORTANT]  
> [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] Never set a blank or weak **sa** password.  
  
**Mixed Mode (Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication)**: Allows users to connect by using Windows authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. Users who connect through a Windows user account can use trusted connections that are validated by Windows.  
  
If you must choose mixed mode authentication and you need to use SQL logins to accommodate legacy applications, you must set strong passwords for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] accounts.  
  
> [!NOTE]  
> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication is provided for backward compatibility only. [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
**Enter Password**: Enter and confirm the system administrator (**sa**) login. Passwords are the first line of defense against intruders, so setting strong passwords is essential to the security of your system. Never set a blank or weak **sa** password.  
  
> [!NOTE]  
> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] passwords can contain from 1 to 128 characters, including any combination of letters, symbols, and numbers. If you choose mixed mode authentication, you must enter a strong **sa** password before you can continue to the next page of the Installation Wizard.  
  
#### Strong password guidelines
  
Strong passwords aren't readily guessed by a person and aren't easily hacked by using a computer program. Strong passwords can't use prohibited conditions or terms, including:  
  
* A blank or NULL condition
* "Password"
* "Admin"
* "Administrator"
* "sa"
* "sysadmin"

A strong password can't be the following terms associated with the installation computer:  
  
* The name of the user currently logged into the machine
* The computer name  
  
A strong password must be more than 8 characters in length and satisfy at least three of the following four criteria:  
  
* It must contain uppercase letters.
* It must contain lowercase letters.  
* It must contain numbers.
* It must contain non-alphanumeric characters; for example, #, %, or ^.  
  
Passwords entered on this page must meet strong-password-policy requirements. If you have any automation that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication, ensure that the password meets strong-password-policy requirements.  
  
### See also

For more information about choosing Windows authentication versus [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication, see [Choose an authentication mode](../../relational-databases/security/choose-an-authentication-mode.md).  

For more information about choosing an account to run the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

## <a name ="datadir"></a> Database Engine Configuration - Data Directories page

Use this page to specify the installation location for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] program and data files. Based on the type of installation, the supported storage may include local disk, shared storage, or an SMB file server.  
  
To specify an SMB file share as a directory, you must manually type the supported UNC path. Browsing to an SMB file share isn't supported. The following example shows the supported UNC path format of an SMB file share:

`\\<ServerName>\<ShareName>\...`

### Standalone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
  
For a standalone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the following table lists the supported storage types and the default directories that you can configure during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup:  
  
### UI element list
  
|Description|Supported storage types|Default directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**Data root directory**|Local disk, SMB file server, shared storage* |\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\ |[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup configures access control lists (ACLs) for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and breaks inheritance as part of configuration.|  
|**User database directory**|Local disk, SMB file server, shared storage*|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data |Best practices for user data directories depend on workload and performance requirements.|  
|**User database log directory**|Local disk, SMB file server, shared storage*|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data|Ensure that the log directory has adequate space.|  
|**Backup directory**|Local disk, SMB file server, shared storage*|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Backup|Set appropriate permissions to prevent data loss, and ensure that the user account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories isn't supported.|  
  
\* Although shared disks are supported, we don't recommend their use for a standalone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

For a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the following table lists the supported storage types and the default directories that you can configure during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
|Description|Supported storage types|Default directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**Data root directory**|Shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\<br /><br /> **Tip**: If you select **shared disk** on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if you don't make a selection on the **Cluster Disk Selection** page.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup configures ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and breaks inheritance as part of configuration.|  
|**User database directory**|Shared storage, SMB file server|\<Drive:>Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data<br /><br /> **Tip**: If you select **shared disk** on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if you don't make a selection on the **Cluster Disk Selection** page.|Best practices for user data directories depend on workload and performance requirements.|  
|**User database log directory**|Shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data<br /><br /> **Tip**: If you select **shared disk** on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if you don't make a selection on the **Cluster Disk Selection** page.|Ensure that the log directory has adequate space.|  
|**Backup directory**|Local disk, shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Backup<br /><br /> **Tip**: If you select **shared disk** on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if you don't make a selection on the **Cluster Disk Selection** page.|Set appropriate permissions to prevent data loss, and ensure that the user account for the SQL Server service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories isn't supported.|  
  
### Security considerations
  
Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.  
  
The following recommendations apply to SMB file servers:  
  
* The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must be a domain account if an SMB file server is used.  
  
* The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have Full Control NTFS permissions on the SMB file share folder used as the data directory.  
  
* The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be granted SeSecurityPrivilege privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the **Manage auditing and security log** policy. This setting is in the **User Rights Assignments** section under **Local Policies** in the Local Security Policy console.

### Considerations
  
* When adding features to an existing installation, you can't change the location of a previously installed feature, nor can you specify the location for a new feature.  
  
* If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Don't share the directories in this dialog box with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Also install the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to separate directories.  
  
* Program files and data files can't be installed in the following situations:  
  
  * On a removable disk drive
  * On a file system that uses compression
  * To a directory where system files are located
  * On a mapped network drive on a failover cluster instance  
  
## <a name="tempdb"><a/> Database Engine Configuration - TempDB page

Use this page to specify the **tempdb** data and log file location, size, growth settings, and number of files for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Based on the type of installation, the supported storage may include local disk, shared storage, or an SMB file server.  
  
To specify an SMB file share as a directory, you must manually type the supported UNC path. Browsing to an SMB file share isn't supported. The following example shows the supported UNC path format of an SMB file share:

`\\<ServerName>\<ShareName>\....`
  
### Data and log directories for a standalone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

For standalone instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the following table lists the supported storage types and default directories that you can configure during setup.  
  
|Description|Supported storage type|Default directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**Data directories**|Local disk, SMB file server, shared storage* |\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup configures ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and breaks inheritance as part of configuration.<br /><br /> Best practices for the **tempdb** directories depend on workload and performance requirements. To spread the data files across several volumes, specify multiple folders or drives.|  
|**Log directory**|Local disk,  SMB file server, shared storage*|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data|Ensure that the log directory has adequate space.|  
  
\* Although shared disks are supported, we don't recommend their use for a standalone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Data and log directories for a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

For a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the following table lists the supported storage types and the default directories that you can configure during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
|Description|Supported storage type|Default directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**tempdb data directory**|Local disk, shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\Data<br /><br /> **Tip**: If you select **shared disk** on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if you don't make a selection on the **Cluster Disk Selection** page.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup configures ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and breaks inheritance as part of configuration.<br /><br /> Ensure that the specified directory or directories (if multiple files are specified) are valid for all the cluster nodes. During failover, if the **tempdb** directories aren't available on the failover target node, the SQL Server resource fails to come online.|  
|**tempdb log directory**|Local disk, shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL*nn*.\<InstanceID>\MSSQL\Data<br /><br /> **Tip**: If you select **shared disk** on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if you don't make a selection on the **Cluster Disk Selection** page.|Best practices for user data directories depend on workload and performance requirements.<br /><br /> Ensure that the specified directory is valid for all the cluster nodes. During failover, if the **tempdb** directories aren't available on the failover target node, the SQL Server resource fails to come online.<br /><br /> Ensure that the log directory has adequate space.|  
  
### UI element list

Configure the settings for **tempdb** according to your workload and requirements. The following settings apply to **tempdb** data files:  
  
* **Number of files** is the total number of data files for **tempdb**. The default value is the lower of 8 or the number of logical cores detected by Setup. As a general rule, if the number of logical processors is less than or equal to 8, use the same number of data files as logical processors. If the number of logical processors is greater than 8, use 8 data files. If contention occurs, increase the number of data files by multiples of 4 (up to the number of logical processors) until contention falls to acceptable levels, or make changes to the workload or code.
  
* **Initial size (MB)** is the initial size in megabytes for each **tempdb** data file. The default value is 8 MB (or 4 MB for [!INCLUDE[ssexpress](../../includes/ssexpress_md.md)]). [!INCLUDE[sssql17](../../includes/sssql17-md.md)] introduces a maximum initial file size of 262,144 MB (256 GB). [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] has a maximum initial file size of 1024 MB. All **tempdb** data files are the same initial size. Because **tempdb** is re-created every time SQL Server starts or fails over, specify a size that's close to the size required by your workload for normal operation. To further optimize the creation of **tempdb** during startup, enable [database instant file initialization](../../relational-databases/databases/database-instant-file-initialization.md).  
  
* **Total initial size (MB)** is the cumulative size of all the **tempdb** data files.  
  
* **Autogrowth (MB)** is the amount of space in megabytes that each **tempdb** data file automatically grows by when it runs out of space. In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, all data files grow at the same time by the amount specified in this setting.  
  
* **Total autogrowth (MB)** is the cumulative size of each autogrowth event.  
* **Data directories** shows all the directories that hold **tempdb** data files. When there are multiple directories, data files are placed in directories in a round-robin manner. For example, if you create 3 directories and specify 8 data files, data files 1, 4, and 7 are created in the first directory. Data files 2, 5, and 8 are created in the second directory. Data files 3 and 6 are in the third directory.  
  
* To add directories, select **Add...**.  
  
* To remove a directory, select the directory, and select **Remove**.  
  
**Tempdb log file** is the name of the log file. This file is created automatically. The following settings apply only to **tempdb** log files:  
  
* **Initial size (MB)** is the initial size of the **tempdb** log file. The default value is 8 MB (or 4 MB for [!INCLUDE[ssexpress](../../includes/ssexpress_md.md)]). [!INCLUDE[sssql17](../../includes/sssql17-md.md)] introduces a maximum initial file size of 262,144 MB (256 GB). [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] has a maximum initial file size of 1024 MB. Because **tempdb** is re-created every time SQL Server starts or fails over, specify a size that's close to the size required by your workload for normal operation. To further optimize the creation of **tempdb** during startup, enable [database instant file initialization](../../relational-databases/databases/database-instant-file-initialization.md).  
  
  > [!NOTE]
  > **Tempdb** uses minimal logging. The **tempdb** log file can't be backed up. It is re-created every time SQL Server starts or when a cluster instance fails over.

* **Autogrowth (MB)** is the growth increment of the **tempdb** log in megabytes.  The default value of 64 MB creates the optimal number of virtual log files during initialization.  

  > [!NOTE]
  > **Tempdb** log files don't use database instant file initialization.
  
* **Log directory** is the directory where **tempdb** log files are created. There's only one **tempdb** log directory.  
  
### Security considerations
  
Setup configures ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and breaks inheritance as part of configuration.  

The following recommendations apply to the SMB file server:  
  
* The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must be a domain account if an SMB file server is used.  
  
* The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have **Full Control** NTFS permissions on the SMB file share folder used as the data directory.  
  
* The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be granted SeSecurityPrivilege privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the **Manage auditing and security log** policy. This setting is in the **User Rights Assignments** section under **Local Policies** in the Local Security Policy console.  
  
> [!NOTE]
> If you specify nondefault installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be installed to separate directories.
  
### See also

* [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
* [Share and NTFS permissions on a file server](/iis/web-hosting/configuring-servers-in-the-windows-web-platform/configuring-share-and-ntfs-permissions)  

<!--
The MaxDOP setting applies only to SQL Server 2019 and later.
-->

::: moniker range=">=sql-server-ver15"

## <a name="maxdop"><a/> Database Engine Configuration - MaxDOP page

**Max degree of parallelism (MaxDOP)** determines the maximum number of processors that a single statement can use. [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] introduces the ability to configure this option during installation. [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] also automatically detects the recommended MaxDOP setting for the server based on the number of cores.  

If this page is skipped during setup, the default MaxDOP value is the recommended value displayed in this page instead of the default [!INCLUDE[ssde_md](../../includes/ssde_md.md)] value for previous versions (0). You can also manually configure this setting on this page, and you can modify this setting after installation. 

### UI element list

* **Max degree of parallelism (MaxDOP)** is the value for the maximum number of processors to use during parallel execution of a single statement. The default value will align with the max degree of parallelism guidelines in [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#recommendations).

## <a name="memory"><a/> Database Engine Configuration - Memory page

**min server memory** determines the lower memory limit that the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will use for the buffer pool and other caches. The default value is 0 and the recommended value is also 0. For more information on the effects of **min server memory**, see the [Memory Management Architecture Guide](../../relational-databases/memory-management-architecture-guide.md#effects-of-min-and-max-server-memory).

**max server memory** determines the upper memory limit that the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will use for the buffer pool and other caches. The default value is 2,147,483,647 megabytes (MB) and the calculated recommended values will align with the memory configuration guidelines in [Server Memory Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md#manually) for a standalone [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, based on the existing system memory. For more information on the effects of **max server memory**, see the [Memory Management Architecture Guide](../../relational-databases/memory-management-architecture-guide.md#effects-of-min-and-max-server-memory).

If this page is skipped during setup, the default **max server memory** value used is the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] default value (2,147,483,647 megabytes). You can manually configure these settings on this page once you've chosen the **Recommended** radio button, and you can modify these setting after installation. For more information, see [Server Memory Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).

### UI element list
  
**Default**: This radio button is selected by default and sets the **min server memory** and **max server memory** settings to the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] default values. 

**Recommended**: This radio button must be selected to accept the calculated recommended values or to change the calculated values to user configured values.  
  
**Min Server Memory (MB)**: If changing from the calculated recommended value to a user configured value, enter the value for **min server memory**.  
  
**Max Server Memory (MB)**: If changing from the calculated recommended value to a user configured value, enter the value for **max server memory**.  

**Click here to accept the recommended memory configurations for the SQL Server Database Engine**: Select this check box to accept the calculated recommended memory configurations on this server. If the **Recommended** radio button was selected, setup cannot continue without this check box being selected.

::: moniker-end

## Database Engine Configuration - FILESTREAM page

Use this page to enable FILESTREAM for this installation of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. FILESTREAM integrates the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] with an NTFS file system by storing **varbinary(max)** binary large object (BLOB) data as files in the file system. [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can insert, update, query, search, and back up FILESTREAM data. Microsoft Win32 file-system interfaces provide streaming access to the data. 
  
### UI element list
  
**Enable FILESTREAM for Transact-SQL access**: Select to enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access. This check box must be selected before the other options will be available.  
  
**Enable FILESTREAM for file I/O streaming access**: Select to enable Win32 streaming access for FILESTREAM.  
  
**Windows share name**: Enter the name of the Windows share in which the FILESTREAM data will be stored.  
  
**Allow remote clients to have streaming access to FILESTREAM data**: Select this check box to allow remote clients to access the FILESTREAM data on this server.  
  
### See also

* [Enable and configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)
* [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  

## Database Engine Configuration - User Instance page

Use the **User Instance** page to:

* Generate a separate instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for users without administrator permissions.
* Add users to the administrator role.  
  
### Options
  
**Enable User Instances**: The default is on. To turn off the ability to enable user instances, clear the check box.  
  
The user instance, also known as a child or client instance, is an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is generated by the parent instance (the primary instance that runs as a service, like [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]) on behalf of a user. The user instance runs as a user process under the security context of that user. The user instance is isolated from the parent instance and any other user instances that are running on the computer. The user instance feature is also referred to as "run as normal user" (RANU).  
  
> [!NOTE]  
> Logins provisioned as members of the **sysadmin** fixed server role during Setup are provisioned as administrators in the template database. They're members of the **sysadmin** fixed server role on the user instance unless they're removed.  
  
**Add user to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrator role**:  Default is off. To add the current Setup user to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrator role, select the check box.  
  
[!INCLUDE[winvista](../../includes/winvista-md.md)] users that are members of BUILTIN\Administrators aren't automatically added to the **sysadmin** fixed server role when they connect to [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. Only [!INCLUDE[winvista](../../includes/winvista-md.md)] users who have been explicitly added to a server-level administrator role can administer [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. Members of the Built-In\Users group can connect to the [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] instance, but they'll have limited permissions to do database tasks. For this reason, users whose [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] privileges are inherited from BUILTIN\Administrators and Built-In\Users in previous releases of Windows must be explicitly granted administrative privileges in instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] that are running on [!INCLUDE[winvista](../../includes/winvista-md.md)].  
  
To make changes to the user roles after the installation program ends, use [SQL Server Management Studio](../../relational-databases/security/authentication-access/join-a-role.md) or [Transact-SQL](../../t-sql/statements/alter-role-transact-sql.md).
