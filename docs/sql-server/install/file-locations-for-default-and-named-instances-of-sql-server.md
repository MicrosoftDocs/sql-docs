---
title: "File Locations for Default and Named Instances of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/25/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 463c570e-9f75-4653-b3b8-4d61753b0013
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# File Locations for Default and Named Instances of SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  An installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] consists of one or more separate instances. An instance, whether default or named, has its own set of program and data files, as well as a set of common files shared between all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the computer.  
  
 For an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that includes the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], each component has a full set of data and executable files, and common files shared by all components.  
  
 To isolate install locations for each component, unique instance IDs are generated for each component within a given instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  Program files and data files cannot be installed on a removable disk drive, cannot be installed on a file system that uses compression, cannot be installed to a directory where system files are located, and cannot be installed on shared drives on a failover cluster instance.  
>  
>  You might need to configure scanning software, such as antivirus and antispyware applications, to exclude SQL Server folders and file types. Review this support article for more information: [Antivirus software on computers running SQL Server](https://support.microsoft.com/kb/309422).
> 
>  System databases (master, model, MSDB, and tempdb), and [!INCLUDE[ssDE](../../includes/ssde-md.md)] user databases can be installed with Server Message Block (SMB) file server as a storage option. This applies to both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stand-alone and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installations (FCI). For more information, see [Install SQL Server with SMB Fileshare as a Storage Option](../../database-engine/install-windows/install-sql-server-with-smb-fileshare-as-a-storage-option.md).  
>   
>  Do not delete any of the following directories or their contents: Binn, Data, Ftdata, HTML, or 1033. You can delete other directories, if necessary; however, you might not be able to retrieve any lost functionality or data without uninstalling and then reinstalling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Do not delete or modify any of the .htm files in the HTML directory. They are required for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools to function properly.  
  
## Shared Files for All Instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 Common files used by all instances on a single computer are installed in the folder [!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]. \<*drive*> is the drive letter where components are installed. The default is usually drive C.  \<*nnn*> identifies the version. The following table identifies versions for the paths. 

|\<*nnn*>|Version
|-----|-----
|140|[!INCLUDE[ssqlv14](../../includes/sssqlv14-md.md)]
|130|[!INCLUDE[ssqlv13](../../includes/sssql15-md.md)]
|120|SQL Server 2014
|110|[!INCLUDE[sssql11](../../includes/sssql11-md.md)] 
  
## File Locations and Registry Mapping  
 During [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, an instance ID is generated for each server component. The server components in this [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] release are the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 The default instance ID is constructed by using the following format:  
  
-   MSSQL for the [!INCLUDE[ssDE](../../includes/ssde-md.md)], followed by the major version number, followed by an underscore and the minor version when applicable, and a period, followed by the instance name.  
  
-   MSAS for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], followed by the major version number, followed by an underscore and the minor version when applicable, and a period, followed by the instance name.  
  
-   MSRS for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], followed by the major version number, followed by an underscore and the minor version when applicable, and a period, followed by the instance name.  
  
 Examples of default instance IDs in this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are as follows:  
  
-   MSSQL14.MSSQLSERVER for a default instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   MSAS14.MSSQLSERVER for a default instance of [!INCLUDE[ssASCurrent](../../includes/ssascurrent-md.md)].  
  
-   MSSQL14.MyInstance for a named instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] named "MyInstance."  
  
    >[!NOTE]
    >The two digit number in the instance ID path identifies the version number. In the preceding examples, version number 14 is [!INCLUDE[ssqlv14](../../includes/sssqlv14-md.md)]. 

 The directory structure for a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] named instance that includes the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], named "MyInstance", and installed to the default directories would be as follows:  
  
-   C:\Program Files\Microsoft SQL Server\MSSQL14.MyInstance\  
  
-   C:\Program Files\Microsoft SQL Server\MSAS14.MyInstance\  
  
 You can specify any value for the instance ID, but avoid special characters and reserved keywords.  
  
 You can specify a non-default instance ID during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. Instead of \<Program Files>\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a \<custom path>\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is used if the user chooses to change the default installation directory. Note that instance IDs that begin with an underscore (_) or that contain the number sign (#) or the dollar sign ($) are not supported.  
  
> [!NOTE]  
>  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and client components are not instance aware and, therefore are not assigned an instance ID. By default, non-instance-aware components are installed to a single directory: [!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]. Changing the installation path for one shared component also changes it for the other shared components. Subsequent installations install non-instance-aware components to the same directory as the original installation.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is the only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component that supports instance renaming after installation. If an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is renamed, the instance ID will not change. After instance renaming is complete, directories and registry keys will continue to use the instance ID created during installation.  
  
 The registry hive is created under HKLM\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\<*Instance_ID*> for instance-aware components. For example,  
  
-   HKLM\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.MyInstance  
  
-   HKLM\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSAS14.MyInstance  
  
-   HKLM\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSRS14.MyInstance  
  
 The registry also maintains a mapping of instance ID to instance name. Instance ID to instance name mapping is maintained as follows:  
  
-   [HKEY_LOCAL_MACHINE\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\Instance Names\SQL] "InstanceName"="MSSQL14"  
  
-   [HKEY_LOCAL_MACHINE\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\Instance Names\OLAP] "InstanceName"="MSAS14"  
  
-   [HKEY_LOCAL_MACHINE\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\Instance Names\RS] "InstanceName"="MSRS14"  
  
## Specifying File Paths  
 During Setup, you can change the installation path for the following features:  
  
 The installation path is displayed in Setup only for features with a user-configurable destination folder:  
  
|Component|Default path|Configurable or Fixed Path|  
|---------------|------------------|--------------------------------|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] server components|\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.\<InstanceID>\ |Configurable|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] data files|\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.\<InstanceID>\ |Configurable|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server|\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSAS14.\<InstanceID>\ |Configurable|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data files|\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSAS14.\<InstanceID>\ |Configurable|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server|\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSRS14.\<InstanceID>\Reporting Services\ReportServer\Bin\ |Configurable|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report manager|\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSRS14.\<InstanceID>\Reporting Services\ReportManager\ |Fixed path|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|\<Install Directory>\140\DTS\\ <sup>1</sup> |Configurable |  
|Client Components (except bcp.exe and sqlcmd.exe)|\<Install Directory>\140\Tools\\ <sup>1</sup> |Configurable |  
|Client Components (bcp.exe and sqlcmd.exe)|\<Install Directory>\Client SDK\ODBC\110\Tools\Binn|Fixed path|  
|Replication and server-side COM objects|[!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]COM\\ <sup>2</sup> |Fixed path|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component DLLs for the Data Transformation Run-time engine, the Data Transformation Pipeline engine, and the **dtexec** command prompt utility|[!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]DTS\Binn|Fixed path|  
|DLLs that provide managed connection support for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|[!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]DTS\Connections|Fixed path|  
|DLLs for each type of enumerator that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports|[!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]DTS\ForEachEnumerators|Fixed path|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service, WMI providers|[!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]Shared\ |Fixed path|  
|Components that are shared between all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]Shared\ |Fixed path|  
  
> [!WARNING]
> Ensure that the \Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\ folder is protected with limited permissions.  
  
Note that the default drive for file locations is *systemdrive*, normally drive C. Installation paths for child features are determined by the installation path of the parent feature.  
  
<sup>1</sup> A single installation path is shared between [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and client components. Changing the installation path for one component also changes it for other components. Subsequent installations install components to the same location as the original installation.  
  
<sup>2</sup> This directory is used by all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a computer. If you apply an update to any of the instances on the computer, any changes to files in this folder will affect all instances on the computer. When you add features to an existing installation, you cannot change the location of a previously installed feature, nor can you specify the location for a new feature. You must either install additional features to the directories already established by Setup, or uninstall and reinstall the product.  
  
> [!NOTE]  
>  For clustered configurations, you must select a local drive that is available on every node of the cluster.  
  
 When you specify an installation path during Setup for the server components or data files, the Setup program uses the instance ID in addition to the specified location for program and data files. Setup does not use the instance ID for tools and other shared files. Setup also does not use any instance ID for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] program and data files, although it does use the instance ID for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] repository.  
  
 If you set an installation path for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] feature, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup uses that path as the root directory for all instance-specific folders for that installation, including SQL Data Files. In this case, if you set the root to "C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.\<InstanceName>\MSSQL\\", instance-specific directories are added to the end of that path.  
  
 Customers who choose to use the USESYSDB upgrade functionality in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard (Setup UI mode) can easily lead themselves into a situation where the product gets installed into a recursive folder structure. For example, \<*SQLProgramFiles*>\MSSQL14\MSSQL\MSSQL10_50\MSSQL\Data\\. Instead, to use the USESYSDB feature, set an installation path for the SQL Data Files feature instead of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] feature.  
  
> [!NOTE]
>  Data files are always expected to be found in a child directory named Data. For example, specify C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.\<InstanceName>\ to specify the root path to the data directory of the system databases during upgrade when data files are found under C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.\<InstanceName>\MSSQL\Data.  
  
## See Also  
 [Database Engine Configuration - Data Directories](https://msdn.microsoft.com/library/9b1fa0fc-623b-479a-afc3-4f13bd850487)   
 [Analysis Services Configuration - Data Directories](https://msdn.microsoft.com/library/ef732855-b7af-4f40-a619-5573c1c354bb)  
  
  
