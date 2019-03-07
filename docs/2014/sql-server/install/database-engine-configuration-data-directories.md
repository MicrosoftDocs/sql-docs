---
title: "Database Engine Configuration - Data Directories | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 9b1fa0fc-623b-479a-afc3-4f13bd850487
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Database Engine Configuration - Data Directories
  Use this page to specify the installation location for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)] program and data files. Based on the type of installation, the supported storage may include local disk, shared storage, or an SMB file server.  
  
 To specify an SMB file share as a directory, you must manually type the supported UNC path. Browsing to an SMB file share is not supported. The following is a supported UNC path format of an SMB file share: \\\Servername\ShareName\\....  
  
## Stand-Alone Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and the default directories for a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are user configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
## UIElement List  
  
|Description|Supported Storage Type|Default directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|Data root directory|Local Disk, SMB File Server, Shared Storage <sup>1</sup>|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.|  
|User database directory|Local Disk, SMB File Server, Shared Storage <sup>1</sup>|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data|Best practices for user data directories depend on workload and performance requirements.|  
|User database log directory|Local Disk, SMB File Server, Shared Storage <sup>1</sup>|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data|Ensure that the log directory has adequate space.|  
|Temp DB directory|Local Disk, SMB File Server, Shared Storage <sup>1</sup>|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data|Best practices for the Temp directory depend on workload and performance requirements.|  
|Temp DB log directory|Local Disk, SMB File Server, Shared Storage <sup>1</sup>|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data|Ensure that the log directory has adequate space.|  
|Backup directory|Local Disk, SMB File Server, Shared Storage <sup>1</sup>|C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Backup|Set appropriate permissions to prevent data loss, and ensure that the user account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories is not supported.|  
  
 <sup>1</sup> Although shared disks are supported, it is not a recommended practice for a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Failover Cluster Instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following table lists the supported storage types and the default directories for a failover cluster instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are user configurable during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
|Description|Supported Storage Type|Default Directory|Recommendations|  
|-----------------|----------------------------|-----------------------|---------------------|  
|Data root Directory|Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.|  
|User database directory|Shared Storage, SMB File Server|\<Drive:>Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Best practices for user data directories depend on workload and performance requirements.|  
|User database log directory|Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Ensure that the log directory has adequate space.|  
|Temp DB directory|Local Disk, Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Ensure that the specified directory is valid for all the cluster nodes. During failover, if the tempdb directories are not available on the failover target node, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource will fail to come online.|  
|Temp DB log directory|Local Disk, Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Data<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Ensure that the specified directory is valid for all the cluster nodes. During failover, if the tempdb directories are not available on the failover target node, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource will fail to come online.|  
|Backup directory|Local Disk, Shared Storage, SMB File Server|\<Drive:>\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<InstanceID>\MSSQL\Backup<br /><br /> Tip: If shared disk was selected on the **Cluster Disk Selection** page, the default is the first shared disk. This field defaults to blank if no selection was made on the **Cluster Disk Selection** page.|Set appropriate permissions to prevent data loss, and ensure that the user account for the SQL Server service has adequate permissions to write to the backup directory. Using a mapped drive for backup directories is not supported.|  
  
## Security Considerations  
 Setup will configure ACLs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directories and break inheritance as part of configuration.  
  
 The following recommendations apply to the SMB file server:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must be a domain account if an SMB file server is used.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have FULL CONTROL NTFS permissions on the SMB file share folder used as the data directory.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be granted SeSecurityPrivilege privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the **Manage auditing and security log** policy. This setting is available in the **User Rights Assignments** section under **Local Policies** in the **Local Security Policy** console.  
  
## Notes  
  
-   When adding features to an existing installation, you cannot change the location of a previously installed feature, nor can you specify the location for a new feature.  
  
-   If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be installed to separate directories.  
  
-   Program files and data files cannot be installed in the following situations:  
  
    -   On a removable disk drive  
  
    -   On a file system that uses compression  
  
    -   To a directory where system files are located  
  
    -   On a mapped network drive on a failover cluster instance  
  
## See Also  
 [File Locations for Default and Named Instances of SQL Server](../../../2014/sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md)   
 [Share and NTFS Permissions on a File Server](https://go.microsoft.com/fwlink/?LinkID=206571)  
  
  
