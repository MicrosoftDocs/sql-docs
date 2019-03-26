---
title: "Backup and Restore of Analysis Services Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/25/2019"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.ssmsimbi.Backup.f1"
  - "sql12.asvs.ssmsimbi.Restore.f1"
helpviewer_keywords: 
  - "backing up databases [Analysis Services]"
  - "encryption [Analysis Services]"
  - "databases [Analysis Services], restoring"
  - "cryptography [Analysis Services]"
  - "databases [Analysis Services], backing up"
  - "restoring databases [Analysis Services]"
  - "recovery [Analysis Services]"
ms.assetid: 947eebd2-3622-479e-8aa6-57c11836e4ec
author: minewiskan
ms.author: owend
manager: craigg
---
# Backup and Restore of Analysis Services Databases
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] includes backup and restore so that you can recover a database and its objects from a particular point in time. Backup and restore is also a valid technique for migrating databases to upgraded servers, moving databases between servers, or deploying a database to a production server. For the purposes of data recovery, if you do not already have a backup plan and your data is valuable, you should design and implement a plan as soon as possible.  
  
 The backup and restore commands are performed on a deployed Analysis Services database. For your projects and solutions in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you should use source control to ensure you can recover specific versions of your source files, and then create a data recovery plan for the repository of the source control system you are using.  
  
 For a full backup that includes source data, you have to back up the database which contains detail data. Specifically, if you are using ROLAP or DirectQuery database storage, detail data is stored in an external SQL Server relational database that is distinct from the Analysis Services database. Otherwise, if all objects are tabular or multidimensional, the Analysis Services backup will include both the metadata and source data.  
  
 One clear benefit of automating backup is that the data snapshot will always be as up-to-date as the automated frequency of backup specifies. Automated schedulers ensure that backups are not forgotten. Restoring a database can also be automated, and can be a good way to replicate data, but be sure to back up the encryption key file on the instance you replicate to. The synchronization feature is dedicated to replication of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases, but only for the data that is out of date. All of the features mentioned here can be implemented through the user interface, by way of XML/A commands or programmatically run through AMO.  
  
 This topic includes the following sections:  
  
-   [Preparing for Backup](#bkmk_prep)  
  
-   [Backing Up a Multidimensional or a Tabular Database](#bkmk_cube)  
  
-   [Restoring an Analysis Services Database](#bkmk_restore)  
  
##  <a name="bkmk_prereq"></a> Prerequisites  
 You must have administrative permissions on the Analysis Services instance or Full Control (Administrator) permissions on the database you are backing up.  
  
 Restore location must be an Analysis Services instance that is the same version, or a newer version, as the instance from which the backup was taken. Although you cannot restore a database from a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance to an earlier version of Analysis Services, it is common practice to restore an older version database, such as SQL Server 2012, on a newer [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance.  
  
 Restore location must be the same server type. Tabular databases can only be restored to Analysis Services running in tabular mode. Multidimensional databases require an instance running in multidimensional mode.  
  
##  <a name="bkmk_prep"></a> Preparing for Backup  
 Use the following checklist to prepare for backup:  
  
-   Check the location where the backup file will be stored. If you are using a remote location, you must specify it as a UNC folder. Verify that you can access the UNC path.  
  
-   Check the permissions on the folder to ensure that the Analysis Services service account has Read/Write permissions on the folder.  
  
-   Check for sufficient disk space on the target server.  
  
-   Check for existing files of the same name. If a file of the same name already exists, backup will fail unless you specify options to overwrite the file.  
  
##  <a name="bkmk_cube"></a> Backing Up a Multidimensional or a Tabular Database  
 Administrators can back up an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database to a single [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] backup file (.abf), regardless of size of the database. For step by step instructions, see [How to Backup an Analysis Services Database (TechMantra)](http://www.mytechmantra.com/LearnSQLServer/Backup_an_Analysis_Services_Database.html) and [Automate Backup an Analysis Services Database (TechMantra)](http://www.mytechmantra.com/LearnSQLServer/Automate_Backup_of_Analysis_Services_Database.html).  
  
> [!NOTE]  
>  [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], used for loading and querying PowerPivot data models in a SharePoint environment, loads its models from SharePoint content databases. These content databases are relational and run on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database engine. As such, there is no [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] backup and restore strategy for PowerPivot data models. If you have a disaster recovery plan in place for SharePoint content, that plan encompasses the PowerPivot data models stored in the content databases.  
  
 **Remote Partitions**  
  
 If the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database contains remote partitions, the remote partitions should also be backed up. When you back up a database with remote partitions, all the remote partitions on each remote server are backed up to a single file on each of those remote servers respectively. Therefore, if you want to create those remote backups off their respective host computers, you will have to manually copy those files to the designated storage areas.  
  
 **Contents of a backup file**  
  
 Backing up an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database produces a backup file whose contents vary depending upon the storage mode used by the database objects. This difference in backup content results from the fact that each storage mode actually stores a different set of information within an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. For example, multidimensional hybrid OLAP (HOLAP) partitions and dimensions store aggregations and metadata in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, while relational OLAP (ROLAP) partitions and dimensions only store metadata in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. Because the actual contents of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database vary based on the storage mode of each partition, the contents of the backup file also vary. The following table associates the contents of the backup file to the storage mode used by the objects.  
  
|Storage Mode|Contents of backup file|  
|------------------|-----------------------------|  
|Multidimensional MOLAP partitions and dimensions|Metadata, source data, and aggregations|  
|Multidimensional HOLAP partitions and dimensions|Metadata and aggregations|  
|Multidimensional ROLAP partitions and dimensions|Metadata|  
|Tabular In-Memory Models|Metadata and source data|  
|Tabular DirectQuery Models|Metadata only|  
  
> [!NOTE]  
>  Backing up an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database does not back up the data in any underlying data sources, such as a relational database. Only the contents of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database are backed up.  
  
 When you back up an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, you can choose from the following options:  
  
-   Whether to compress all database backups. The default is to compress backups.  
  
-   Whether to encrypt the contents of the backup files and require a password before the file can be unencrypted and restored. By default, the backed up data is not encrypted.  
  
    > [!IMPORTANT]  
    >  For each backup file, the user who runs the backup command must have permission to write to the backup location specified for each file. Also, the user must have one of the following roles: a member of a server role for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be backed up.  
  
 For more information about backing up an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, see [Backup Options](backup-options.md).  
  
##  <a name="bkmk_restore"></a> Restoring an Analysis Services Database  
 Administrators can restore an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database from one or more backup files.  
  
> [!NOTE]  
>  If a backup file is encrypted, you must provide the password specified during backup before you can use that file to restore an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database.  
  
 During restoration, you have the following options:  
  
-   You can restore the database using the original database name, or you can specify a new database name.  
  
-   You can overwrite an existing database. If you choose to overwrite the database, you must expressly specify that you want to overwrite the existing database.  
  
-   You can choose whether to restore existing security information or skip security membership information.  
  
-   You can choose to have the restore command change the restoration folder for each partition being restored. Local partitions can be restored to any folder location that is local to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance to which the database is being restored. Remote partitions can be restored to any folder on any server, other than the local server; remote partitions cannot become local.  
  
    > [!IMPORTANT]  
    >  For each backup file, the user who runs the restore command must have permission to read from the backup location specified for each file. To restore an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that is not installed on the server, the user must also be a member of the server role for that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. To overwrite an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, the user must have one of the following roles: a member of the server role for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be restored.  
  
    > [!NOTE]  
    >  After restoring an existing database, the user who restored the database might lose access to the restored database. This loss of access can occur if, at the time that the backup was performed, the user was not a member of the server role or was not a member of the database role with Full Control (Administrator) permissions.  
  
 For more information about restoring an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, see [Restore Options](restore-options.md).  
  
## See Also  
 [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md)   
 [Analysis Services PowerShell](../analysis-services-powershell.md)  
  
  
