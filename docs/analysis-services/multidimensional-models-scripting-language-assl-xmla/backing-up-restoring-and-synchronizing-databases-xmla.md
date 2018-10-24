---
title: "Backing Up, Restoring, and Synchronizing Databases (XMLA) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Backing Up, Restoring, and Synchronizing Databases (XMLA)
  In XML for Analysis, there are three commands that back up, restore, and synchronize databases:  
  
-   The [Backup](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/backup-element-xmla) command backs up a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database using an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] backup file (.abf), as described in the section, [Backing Up Databases](#backing_up_databases).  
  
-   The [Restore](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/restore-element-xmla) command restores an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database from an .abf file, as described in the section, [Restoring Databases](#restoring_databases).  
  
-   The [Synchronize](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/synchronize-element-xmla) command synchronizes one [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database with the data and metadata of another database, as described in the section, [Synchronizing Databases](#synchronizing_databases).  
  
##  <a name="backing_up_databases"></a> Backing Up Databases  
 As mentioned earlier, the **Backup** command backs up a specified [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database to a backup file. The **Backup** command has various properties that let you specify the database to be backed up, the backup file to use, how to back up security definitions, and the remote partitions to be backed up.  
  
> [!IMPORTANT]  
>  The Analysis Services service account must have permission to write to the backup location specified for each file. Also, the user must have one of the following roles: administrator role on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be backed up.  
  
### Specifying the Database and Backup File  
 To specify the database to be backed up, you set the [Object](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/object-element-xmla) property of the **Backup** command. The **Object** property must contain an object identifier for a database, or an error occurs.  
  
 To specify the file that is to be created and used by the backup process, you set the [File](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/file-element-xmla) property of the **Backup** command. The **File** property should be set to a UNC path and file name for the backup file to be created.  
  
 Besides specifying which file to use for backup, you can set the following options for the specified backup file:  
  
-   If you set the [AllowOverwrite](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/allowoverwrite-element-xmla) property to true, the **Backup** command overwrites the backup file if the specified file already exists. If you set the **AllowOverwrite** property to false, an error occurs if the specified backup file already exists.  
  
-   If you set the [ApplyCompression](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/applycompression-element-xmla) property to true, the backup file is compressed after the file is created.  
  
-   If you set the [Password](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/password-element-xmla) property to any non-blank value, the backup file is encrypted by using the specified password.  
  
    > [!IMPORTANT]  
    >  If **ApplyCompression** and **Password** properties are not specified, the backup file stores user names and passwords that are contained in connection strings in clear text. Data that is stored in clear text may be retrieved. For increased security, use the **ApplyCompression** and **Password** settings to both compress and encrypt the backup file.  
  
### Backing Up Security Settings  
 The [Security](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/security-element-xmla) property determines whether the **Backup** command backs up the security definitions, such as roles and permissions, defined on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. The **Security** property also determines whether the backup file includes the Windows user accounts and groups defined as members of the security definitions.  
  
 The value of the **Security** property is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*SkipMembership*|Include security definitions, but exclude membership information, in the backup file.|  
|*CopyAll*|Include security definitions and membership information in the backup file.|  
|*IgnoreSecurity*|Exclude security definitions from the backup file.|  
  
### Backing Up Remote Partitions  
 To back up remote partitions in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, you set the [BackupRemotePartitions](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/backupremotepartitions-element-xmla) property of the **Backup** command to true. This setting causes the **Backup** command to create a remote backup file for each remote data source that is used to store remote partitions for the database.  
  
 For each remote data source to be backed up, you can specify its corresponding backup file by including a [Location](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/location-element-xmla) element in the [Locations](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/locations-element-xmla) property of the **Backup** command. The **Location** element should have its **File** property set to the UNC path and file name of the remote backup file, and its [DataSourceID](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/datasourceid-element-xmla) property set to the identifier of the remote data source defined in the database.  
  
##  <a name="restoring_databases"></a> Restoring Databases  
 The **Restore** command restores a specified [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database from a backup file. The **Restore** command has various properties that let you specify the database to restore, the backup file to use, how to restore security definitions, the remote partitions to be stored, and the relocation relational OLAP (ROLAP) objects.  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the restore command must have permission to read from the backup location specified for each file. To restore an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that is not installed on the server, the user must also be a member of the server role for that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. To overwrite an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, the user must have one of the following roles: a member of the server role for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance or a member of a database role with Full Control (Administrator) permissions on the database to be restored.  
  
> [!NOTE]  
>  After restoring an existing database, the user who restored the database might lose access to the restored database. This loss of access can occur if, at the time that the backup was performed, the user was not a member of the server role or was not a member of the database role with Full Control (Administrator) permissions.  
  
### Specifying the Database and Backup File  
 The **DatabaseName** property of the **Restore** command must contain an object identifier for a database, or an error occurs. If the specified database already exists, the **AllowOverwrite** property determines whether the existing database is overwritten. If the **AllowOverwrite** property is set to false and the specified database already exists, an error occurs.  
  
 You should set the **File** property of the **Restore** command to a UNC path and file name for the backup file to be restored to the specified database. You can also set the **Password** property for the specified backup file. If the **Password** property is set to any non-blank value, the backup file is decrypted by using the specified password. If the backup file was not encrypted, or if the specified password does not match the password used to encrypt the backup file, an error occurs.  
  
### Restoring Security Settings  
 The **Security** property determines whether the **Restore** command restores the security definitions, such as roles and permissions, defined on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. The **Security** property also determines whether the **Restore** command includes the Windows user accounts and groups defined as members of the security definitions as part of the restore process.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*SkipMembership*|Include security definitions, but exclude membership information, in the database.|  
|*CopyAll*|Include security definitions and membership information in the database.|  
|*IgnoreSecurity*|Exclude security definitions from the database.|  
  
### Restoring Remote Partitions  
 For each remote backup file created during a previous **Backup** command, you can restore its associated remote partition by including a **Location** element in the **Locations** property of the **Restore** command. The [DataSourceType](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/datasourcetype-element-xmla) property for each **Location** element must be excluded or explicitly set to *Remote*.  
  
 For each specified **Location** element, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance contacts the remote data source specified in the **DataSourceID** property to restore the partitions defined in the remote backup file specified in the **File** property. Besides the **DataSourceID** and **File** properties, the following properties are available for each **Location** element used to restore a remote partition:  
  
-   To override the connection string for the remote data source specified in **DataSourceID**, you can set the **ConnectionString** property of the **Location** element to a different connection string. The **Restore** command will then use the connection string that is contained in the **ConnectionString** property. If **ConnectionString** is not specified, the **Restore** command uses the connection string stored in the backup file for the specified remote data source. You can use the **ConnectionString** setting to move a remote partition to a different remote instance. However, you cannot use the **ConnectionString** setting to restore a remote partition to the same instance that contains the restored database. In other words, you cannot use the **ConnectionString** property to make a remote partition into a local partition.  
  
-   For each original folder used to store the remote partitions on the remote data source, you can specify a [Folder](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/folder-element-xmla) element to indicate the new folder in which to restore all the remote partitions stored in the original folder. If a **Folder** element is not specified, the **Restore** command uses the original folders specified for the remote partitions that are contained in the remote backup file.  
  
### Relocating ROLAP Objects  
 The **Restore** command cannot restore aggregations or data for objects that use ROLAP storage because such information is stored in tables on an underlying relational data source. However, the metadata for ROLAP objects can be restored. To restore the metadata for ROLAP object, the **Restore** command re-creates the table structure on a relational data source.  
  
 You can use the **Location** element in a **Restore** command to relocate ROLAP objects. For each **Location** element used to relocate a data source, the **DataSourceType** property must be explicitly set to *Local*. You also have to set the **ConnectionString** property of the **Location** element to the connection string of the new location. During the restore, the **Restore** command will replace the connection string for the data source identified by the **DataSourceID** property of the **Location** element with the value of the **ConnectionString** property of the **Location** element.  
  
##  <a name="synchronizing_databases"></a> Synchronizing Databases  
 The **Synchronize** command synchronizes the data and metadata of a specified [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database with another database. The **Synchronize** command has various properties that let you specify the source database, how to synchronize security definitions, the remote partitions to be synchronized, and the synchronization of ROLAP objects.  
  
> [!NOTE]  
>  The **Synchronize** command can be executed only by server administrators and database administrators. Both the source and destination database must have the same database compatibility level.  
  
### Specifying the Source Database  
 The [Source](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/source-element-xmla) property of the **Synchronize** command contains two properties, **ConnectionString** and **Object**. The **ConnectionString** property contains the connection string of the instance that contains the source database, and the **Object** property contains the object identifier for the source database.  
  
 The destination database is the current database for the session in which the **Synchronize** command runs.  
  
 If the **ApplyCompression** property of the **Synchronize** command is set to true, the information sent from the source database to the destination database is compressed before being sent.  
  
### Synchronizing Security Settings  
 The [SynchronizeSecurity](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/security-element-xmla) property determines whether the **Synchronize** command synchronizes the security definitions, such as roles and permissions, defined on the source database. The **SynchronizeSecurity** property also determines whether the **Sychronize** command includes the Windows user accounts and groups defined as members of the security definitions.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*SkipMembership*|Include security definitions, but exclude membership information, in the destination database.|  
|*CopyAll*|Include security definitions and membership information in the destination database.|  
|*IgnoreSecurity*|Exclude security definitions from the destination database.|  
  
### Synchronizing Remote Partitions  
 For each remote data source that exists on the source database, you can synchronize each associated remote partition by including a **Location** element in the **Locations** property of the **Synchronize** command. For each **Location** element, the **DataSourceType** property must be excluded or explicitly set to *Remote*.  
  
 To define and connect to a remote data source in the destination database, the **Synchronize** command uses the connection string defined in the **ConnectionString** property of the **Location** element. The **Synchronize** command then uses the **DataSourceID** property of the **Location** element to identify which remote partitions to synchronize. The **Synchronize**command synchronizes the remote partitions on the remote data source specified in the **DataSourceID** property on the source database with the remote data source specified in the **DataSourceID** property on the destination database.  
  
 For each original folder used to store the remote partitions on the remote data source on the source database, you can also specify a **Folder** element in the **Location** element. The **Folder** element indicates the new folder for the destination database in which to synchronize all the remote partitions stored in the original folder on the remote data source. If a **Folder** element is not specified, the Synchronize command uses the original folders specified for remote partitions that are contained in the source database.  
  
### Synchronizing ROLAP Objects  
 The **Synchronize** command cannot synchronize aggregations or data for objects that use ROLAP storage because such information is stored in tables on an underlying relational data source. However, the metadata for ROLAP objects can be synchronized. To synchronize the metadata, the **Synchronize** command recreates the table structure on a relational data source.  
  
 You can use the **Location** element in a Synchronize command to synchronize ROLAP objects. For each **Location** element used to relocate a data source, the **DataSourceType** property must be explicitly set to *Local*. . You also have to set the **ConnectionString** property of the **Location** element to the connection string of the new location. During synchronization, the **Synchronize** command will replace the connection string for the data source identified by the **DataSourceID** property of the **Location** element with the value of the **ConnectionString** property of the **Location** element.  
  
## See Also  
 [Backup Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/backup-element-xmla)   
 [Restore Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/restore-element-xmla)   
 [Synchronize Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/synchronize-element-xmla)   
 [Backup and Restore of Analysis Services Databases](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
  
