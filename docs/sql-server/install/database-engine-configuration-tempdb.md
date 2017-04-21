---
title: "Database Engine Configuration - TempDB | Microsoft Docs"
ms.custom: ""
ms.date: "2016-02-12"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 7aabd304-f3c9-4c2d-bf9d-5479ee2498da
caps.latest.revision: 13
ms.author: "mikeray"
manager: "jhubbard"
robots: noindex,nofollow
---
# Database Engine Configuration - TempDB
  Use this page to specify **tempdb** data and log  file location, size, growth settings, and number of files for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)]. Based on the type of installation, the supported storage may include local disk, shared storage, or an SMB file server.  
  
 To specify an SMB file share as a directory, you must manually type the supported UNC path. Browsing to an SMB file share is not supported. The following is a supported UNC path format of an SMB file share: \\\Servername\ShareName\\....  
  
## Data and log directories for  a stand-alone instance of  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and default directories for stand-alone instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you can configure during setup.  
  
|Description|Supported Storage Type|Default Directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**Data directories**|Local disk, SMB file server, shared storage*|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL13.\<InstanceID>\MSSQL\Data|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.<br /><br /> Best practices for the **temdb** directories depend on workload and performance requirements. Specify multiple folders/drives to spread the data files across several volumes.|  
|**Log directory**|Local disk,  SMB file server, shared storage*|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL13.\<InstanceID>\MSSQL\Data|Ensure that the log directory has adequate space.|  
  
 *Although shared disks are supported, it is not a recommended practice for a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Data and log directories for a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and the default directories for a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are user configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
|Description|Supported Storage Type|Default Directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|**tempdb** data directory|Local disk, shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL13.\<InstanceID>\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.<br /><br /> Ensure that the specified directory or directories (if multiple files are specified) is valid for all the cluster nodes. During failover, if the **tempdb** directories are not available on the failover target node, the SQL Server resource will fail to come online.|  
|**tempdb** log directory|Local disk, shared storage, SMB file server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL13.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Best practices for user data directories depend on workload and performance requirements.<br /><br /> Ensure that the specified directory is valid for all the cluster nodes. During failover, if the **tempdb** directories are not available on the failover target node, the SQL Server resource will fail to come online.<br /><br /> Ensure that the log directory has adequate space.|  
  
## UIElement List  
 Configure the settings for **tempdb** according to your workload and requirements. The following settings apply to **tempdb** data files:  
  
-   **Number of files** is the total number of data files for **tempdb**. The default value is the lower of 8 or the number of logical cores detected by setup.    As a general rule, if the number of logical processors is less than or equal to 8, use the same number of data files as logical processors. If the number of logical processors is greater than 8, use 8 data files and then if contention continues, increase the number of data files by multiples of 4 (up to the number of logical processors) until the contention is reduced to acceptable levels or make changes to the workload/code.  
  
-   **Initial size (MB)** is the initial size in MB for each **tempdb** data file. All **tempdb** data files are the same initial size. Because **tempdb** is recreated every time SQL Server starts or fails over you should specify a size that is close to the size required by your workload for normal operation.   To further optimize the creation of **tempdb** during start-up, enable [Database Instant File Initialization](../Topic/Database%20Instant%20File%20Initialization.md).  
  
-   **Total initial size (MB)** is the cumulative size of all of the **tempdb** data files.  
  
-   **Autogrowth (MB)** is the amount of space in megabytes that each **tempdb** data file will automatically grow by when they run out of space. In SQL Server 2016 and later all data files will grow at the same time by the amount specified in this setting.  
  
-   **Total autogrowth (MB)** is the cumulative size of each autogrowth event.  
  
-   **Data directories** shows all of the directories that hold **tempdb** data files. When there are multiple directories, data files are placed in directories in a round-robin manner. For example, if you create 3 directories and specify 8 data files, data files number 1, 4, and 7 will be created in the first directory. Data files 2, 5, and 8 will be created in the second directory. Data files 3 and 6 will be in the third directory.  
  
-   To add directories, click **Add...**.  
  
-   To remove a directory, select the directory and click **Remove**.  
  
 **TempDB log file** is the name of the log file. It is created automatically. The following settings apply only to **tempdb** log files:  
  
-   **Initial size (MB)** is the initial size of the **tempdb** log file.  
  
-   **Note:Tempdb** uses minimal logging. The **tempdb** log cannot be backed up. It is recreated every time SQL Server starts or when a cluster instance fails over.  
  
-   **Autogrowth (MB)** is the growth increment of the **tempdb** log in megabytes.  The default value of 64 MB creates the optimal number of virtual log files during initialization.  
  
-   **Note:Tempdb** log files do not use instant file initialization.  
  
-   **Log directory** is the directory where **tempdb** log files are created. There is only one **tempdb** log directory.  
  
## Security Considerations  
 Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.  
  
 The following recommendations apply to the SMB file server:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must be a domain account if an SMB file server is used.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have FULL CONTROL NTFS permissions on the SMB file share folder used as the data directory.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be granted SeSecurityPrivilege privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the **Manage auditing and security log** policy. This setting is available in the **User Rights Assignments** section under **Local Policies** in the **Local Security Policy** console.  
  
## Notes  
  
-   If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be installed to separate directories.  
  
## See Also  
 [File Locations for Default and Named Instances of SQL Server](../Topic/File%20Locations%20for%20Default%20and%20Named%20Instances%20of%20SQL%20Server.md)   
 [Share and NTFS Permissions on a File Server](http://go.microsoft.com/fwlink/?LinkID=206571)  
  
  