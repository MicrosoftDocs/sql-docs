---
title: "SQL Server Backup Applications - Volume Shadow Copy Service (VSS) and SQL Writer"
description: Describes the SQL writer component and its role in the VSS snapshot creation and restore process for SQL Server databases.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 04/02/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
---
# SQL Server backup applications - Volume Shadow Copy Service (VSS) and SQL Writer

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

SQL Server provides support for Volume Shadow Copy Service (VSS) by providing a writer (the SQL writer) so that a third-party backup application can use the VSS framework to back up database files. This paper describes the SQL writer component and its role in the VSS snapshot creation and restores process for SQL Server databases. It also captures details on how to configure and use the SQL writer to work with backup applications in the VSS framework.

## Infrastructure of VSS

VSS provides the system infrastructure for running VSS applications on Windows systems. Though largely transparent to both user and developer, VSS:

- Coordinates activities of providers, writers, and requestors in the creation and use of shadow copies.
- Furnishes the default system provider.
- Implements the low-level driver functionality necessary for any provider to work.

The VSS service starts on demand, therefore this service must be enabled for VSS operations to be successful.

## VSS components

VSS coordinates the activities of the following cooperating components:

- Providers own the shadow copy data and instantiate the shadow copies.

- Writers are applications that change data and participate in the shadow copy synchronization process.

- Requestors initiate the creation and destruction of shadow copies. The design is focused on the scenario where the requestor is a backup application.

VSS provides coordination between these parties:

:::image type="content" source="media/sql-server-vss-writer-backup-guide/coordinations.png" alt-text="Diagram showing how VSS provides coordination between parties.":::

This diagram shows all the components participating in a typical VSS snapshot activity. In such a scenario, SQL Server (including the SQL writer) is acting as a writer in one of the Writer boxes. Other such writers might be Exchange Server, etc.

- **Virtual Device Interface**: SQL Server provides an application programming interface called Virtual Device Interface (VDI), which helps independent software vendors to integrate SQL Server into their products by providing support for backup and restore operations. These APIs are engineered to provide maximum reliability and performance, and support the full range of SQL Server backup and restore functionality, including the full range of hot and snapshot backup capabilities. For more information, see [SQL Server 2005 Virtual Backup Device Interface Specification](https://www.microsoft.com/download/details.aspx?id=17282).

- **Requestor**: A process (either automated or GUI) that requests that one or more snapshot sets be taken of one or more original volumes. In this article, a requestor is also used to imply a backup application that is creating a snapshot of SQL Server databases.

For more information, see [Volume Shadow Copy Service](/windows/win32/vss/volume-shadow-copy-service-overview).

## SQL writer

The SQL writer is a VSS writer provided by the SQL Server instance. It handles the VSS interaction with SQL Server. The SQL writer ships with SQL Server as a standalone service and is installed as part of the SQL Server installation.

The role of the SQL writer in a VSS snapshot backup operation:

:::image type="content" source="media/sql-server-vss-writer-backup-guide/sql-vss-snapshot.png" alt-text="Screenshot of VSS snapshot.":::

<a id="configuring-the-sql-writer"></a>

### Configure the SQL writer

The SQL writer service is installed in the system as part of the SQL Server installation and is configured to start automatically when Windows starts.

### SQL writer service account

During installation, the SQL writer account is installed to use the Local System account. Since the SQL writer needs to talk to SQL Server using exclusive VDI APIs, the SQL writer account must have sufficient access rights for both SQL Server and VSS. Configuring the service as a Local System account provides sufficient rights for the service to run correctly.

> [!IMPORTANT]  
> Make sure that the SQL writer service runs under the Local System account, and that the SQL Server `NT SERVICE\SQLWriter` account is a member of the **sysadmin** role.

<a id="re-enabling-and-starting-sql-writer"></a>

### Re-enable and start SQL writer

By default, the SQL writer service is enabled and starts automatically. If this configuration has been modified, the following steps must be taken to revert to default settings:

The SQL writer service can be enabled by marking this service as **Automatic**. To open the services through control panel, select **Start**, select **Control Panel**, double-click **Administrative Tools**, and then double-click **Services**. In the **Services** pane, double-click the **SQL writer** service and modify the **Startup Type** property to **Automatic**.

Service should then be started by selecting the **Start** button under the **Service Status** property in the service property screen mentioned previously.

> [!NOTE]  
> In certain cases where an instance of SQL Server Express is installed and an application is using the User Instances feature, the SQL writer might be automatically started by SQL Server. This is done to facilitate the enumeration of these User Instances during a VSS backup operation.

## Features supported by the SQL writer

- **Full-text**: The SQL writer reports full-text catalog containers with recursive file specifications under the database components in the writer's metadata document. They're automatically included in the backup when the database component is selected.

- **Differential backup and restore**: The SQL writer supports differential backup and restore through two VSS differential mechanisms:

  - **Partial file**: The SQL writer uses the VSS Partial File mechanism for reporting changed byte ranges within its database files.

  - **Differenced file by last modify time**: The SQL writer uses the VSS Differenced File by Last Modify Time mechanism for reporting changed files in full-text catalogs.

- **Restore with move**: The SQL writer supports VSS's New Target specification during restore. The New Target specification allows for a database/log file or full-text catalog container to be relocated as part of the Restore operation.

- **Database rename**: A requestor might need to restore a SQL Server database with a new name, especially if the database is to be restored side by side with the original database. The SQL writer supports renaming a database during the restore operation, as long as the database remains within the original SQL instance.

- **Copy-only backup**: It's sometimes necessary to take a backup that is intended for a special purpose, for example when you need to make a copy of a database for testing purposes. This backup shouldn't affect the overall backup and restore procedures for the database. Using the `COPY_ONLY` option specifies that the backup is done out-of-band and shouldn't affect the normal sequence of backups. The SQL writer supports the copy-only backup type with SQL Server instances.

- **Autorecovery of database snapshot**: Typically a snapshot of a SQL Server database obtained by using the VSS framework is in a non-recovered state. Data in the snapshot can't be safely accessed before it goes through the recovery phase to roll back in-flight transactions and place the database in a consistent state. It's possible for a VSS backup application to request autorecovery of the snapshots, as part of the snapshot creation process.

These new features and their usage are described in more detail in [Backup and Restore Option Details](#backup-and-restore-option-details) in this article.

### What isn't supported

- Log backups aren't supported by the SQL writer.
- File and filegroup backup isn't supported.
- Page restore isn't supported.
- Database snapshots aren't supported, and are ignored while creating both component and non-component VSS snapshots.
- Autoclose databases, or databases with shutdown enabled.
- Linux doesn't provide a VSS framework and therefore the SQL writer isn't available on Linux.

The following table lists the kinds of snapshot backups that are supported by the SQL writer/SQL Server working with the VSS framework for all editions of SQL Server on Windows.

| Backup and restore operation | Component-based | Non-component-based |
| --- | --- | --- |
| Full data backup<br />(Including full-text catalog) | Yes | Yes |
| Full restore | Yes | Yes |
| Full restore (No recovery) | Yes | No |
| Differential backup | Yes | No |
| Differential restore | Yes | No |
| Restore with move | Yes | No |
| Database rename | Yes | No |
| Copy only backup | Yes | No |
| Auto-recovered snapshots | Yes | No |
| Log backup | No | No |
| Database snapshots | No | No |
| Autoclose databases<br />Databases with shutdown | Yes | No |
| Availability group databases | Yes | No on secondary |

## Backup operations

SQL Server (using the SQL writer) supports the following modes of VSS-based backup operations:

- Non-component-based
- Component-based

### Version support

The SQL writer is shipped as part of SQL Server and supports only SQL Server instances. The SQL writer enumerates SQL Server Express instances also, including user instances launched by SQL Server Express edition.

### Non-component-based backup operations

Non-component-based backups implicitly select databases by using the list of volumes in the snapshot set. The SQL writer checks for torn databases, raising an error if found. A torn database is one in which a subset of files is selected by the list of volumes.

In the non-component-based model, only databases with the simple recovery model are supported. Roll-forward after a restore isn't supported.

### Component-based backup operations

Component-based backups are preferred and recommended with the SQL writer, since the application (VSS backup application) explicitly selects the databases from the metadata that is returned from the SQL writer. The snapshot set should include all the volumes necessary to back up those databases. The VSS infrastructure doesn't automatically add the volumes that are required for the selected set of databases. All backing volumes should be included in the volume snapshot set. It's the responsibility of the backup application to make sure that all backing volumes are included in the snapshot set. The SQL writer detects torn databases (with backing volumes outside the snapshot set) and fails the backup.

The rest of this section assumes that component-based backups are used as part of the VSS snapshot creation process for SQL Server.

## Snapshot creation process

The VSS framework coordinates the activities of a *requestor* (a backup application) and the SQL writer during the creation of SQL Server snapshots. To enable this coordination, the VSS framework defines requestor and writer *interfaces*. These interfaces should be implemented by participating requestor applications and writers. The SQL writer implements the necessary writer interfaces. As part of the snapshot creation process, the SQL writer's interfaces are called by the VSS framework. The SQL writer interacts with SQL Server instances on the system to facilitate snapshot creation.

The VSS framework defines a set of APIs for usage by a requestor/backup application. A backup application developer needs to follow these API calling patterns to work with the VSS framework snapshot creation process. The next sections describe the snapshot creation process from the SQL writer point of view. They also detail some of the internal interactions between the requestor, VSS framework, SQL writer, and SQL Server instances.

For more information on these steps and for details of the VSS framework interfaces, see [Volume Shadow Copy Service (VSS)](/windows-server/storage/file-server/volume-shadow-copy-service).

> [!NOTE]  
> It's assumed that you're familiar with the VSS framework and backup creation process in general. These sections are provided as supplemental information about how the SQL writer participates in the VSS backup creation process.

### Snapshot creation workflow

The following image shows the dataflow diagram during a component-based snapshot creation/backup operation.

:::image type="content" source="media/sql-server-vss-writer-backup-guide/dataflow-chart.png" alt-text="Screenshot of the dataflow.":::

To more fully understand the basic tasks involved in performing a backup, it's useful to break down this overview into the following phases:

- [Backup initialization](#backup-initialization)
- [Backup discovery phase](#backup-discovery)
- [Prebackup tasks](#prebackup-tasks)
- [Actual backup of files](#actual-backup-of-files)
- [Backup termination](#backup-termination)

### Backup initialization

During this phase of the backup, the requestor (backup application) binds to the snapshot interface `IvssBackupComponents`, and initializes it in preparation for the backup. It also calls the VSS API `IVssGatherWriterMetadata` to tell the VSS framework to gather metadata from all the writers.

The VSS framework calls each of the registered writers, including the SQL writer, for the writer metadata using the `OnIdentify` event. The SQL writer queries the SQL Server instances to get the backup metadata information for each database and create the Writer Metadata Document. This phase is also referred to as *metadata enumeration*.

The writer metadata document is a document that contains information that is passed from the writer to the requestor (backup application). The writer metadata document contains the following information:

- The application ID and friendly name
- Where files and components exist
- Which files need to be included and excluded in a backup
- What options should be used at restore time

This is passed back to the requestor via the VSS framework.

### Backup discovery

In this phase, a requestor examines the writer metadata document and creates and fills a Backup Component Document with each component that needs to be backed up. It also specifies the needed backup options and parameters as part of this document. For the SQL writer, each database instance that needs to be backed up is a separate component.

#### Backup components document

This is an XML document created by a requestor (using the `IVssBackupComponents` interface) in the course of setting up a restore or backup operation. The backup components document contains a list of those explicitly included components, from one or more writers, participating in a backup or restore operation. It doesn't contain implicitly included component information. In contrast, a writer metadata document contains only writer components that might participate in a backup. Structural details of a backup component document are described in the VSS API documentation.

#### Prebackup tasks

Prebackup tasks under VSS are focused on creating a shadow copy of the volumes containing data for backup. The backup application saves data from the shadow copy, not the actual volume.

Requestors typically wait on writers during preparation for backup and while the shadow copy is being created. If the SQL writer is participating in the backup operation, it needs to configure its files and also itself to be ready for backup and shadow copy.

#### Prepare for backup

The requestor needs to set the type of backup operation that needs to be performed (`IVssBackupComponents::SetBackupState`) and then notifies writers through VSS, to prepare for a backup operation using `IVssBackupComponents::PrepareForBackup`.

The SQL writer is given access to the backup component document, which details what databases need to be backed up. All backing volumes should be included in the volume snapshot set. The SQL writer detects torn databases (with backing volumes outside the snapshot set) and fails the backup during the PostSnapshot event.

#### Actual backup of files

In this phase, the requestor can move the data to a backup media, if needed. Interactions in this stage are between the requestor and the VSS framework. The SQL writer isn't involved.

1. Get writer status. Returns the status of writers. The requestor might need to handle any failures here.
1. Perform the backup.

The requestor can move the data to back up media if needed at this time.

#### Backup complete

This event indicates that the backup was completed successfully.

This is also the time at which the SQL writer can commit the backup as a differential base, if the current backup is a full backup of the database (and not a copy-only backup).

> [!NOTE]  
> The requestor should send this event (Backup Complete event) explicitly to allow the SQL writer to commit differential base backups. If this event isn't received, the backup that is created isn't an eligible differential base backup.

#### Save writer metadata

The requestor should save the backup component document and each component backup metadata along with the data backed from the snapshot. These writer metadata are needed by the SQL writer/SQL Server for restore operations.

#### Backup termination

The requestor terminates the shadow copy by releasing the `IVssBackupComponents` interface, or by calling `IVssBackupComponents::DeleteSnapshots`.

### SQL writer metadata document

This is an XML document created by a writer (the SQL writer in this case) using the `IVssCreateWriterMetadata` interface, and containing information about the writer's state and components. The structural details of a writer metadata document are described in the VSS API documentation. Here are some of the details of the SQL writer metadata document.

- Writer Identification Information

  - **Writer name** - L"SqlServerWriter"
  - **Writer class ID** - 0xa65faa63, 0x5ea8, 0x4ebc, 0x9d, 0xbd, 0xa0, 0xc4, 0xdb, 0x26, 0x91, 0x2a
  - **Writer instance ID** - L"SQL Server:SQLWriter"
  - **VSSUsageType** - VSS_UT_USERDATA
  - **VSSSourceType** - VSS_ST_TRANSACTEDDB

- Writer Level Information - VSS_APP_BACK_END

- Restore Method Specification – VSS_RME_RESTORE_IF_CAN_REPLACE.

- Supported Backup schema (IVssCreateWriterMetadata::SetBackupSchema API)
  - VSS_BS_DIFFERENTIAL – differential backup
  - VSS_BS_TIMESTAMPED – Timestamp based – for full-text catalog files.
  - VSS_BS_LAST_MODIFY –Differential backup based on last modify time,
  - VSS_BS_WRITER_SUPPORTS_NEW_TARGET – supports new target location option.
  - VSS_BS_WRITER_SUPPORTS_RESTORE_WITH_MOVE – supports restore "with move"
  - VSS_BS_COPY – supports "copy-only" backup option.

- Component Level Information (contains component level-specific information provided by the SQL writer)

  - **Type** - VSS_CT_FILEGROUP
  - **Name** - name of the component (database name)
  - **Logical path** – of the server instance (in the form of "server\instance-name" for named instances and "server" for default instance.)
  - **Component Flags**
  - **VSS_CF_APP_ROLLBACK_RECOVERY** – indicates that SQL Server snapshots always require a "recovery" phase to make the files consistent and useable for non-backup (that is, app-rollback) scenarios.
  - Selectable - True
  - Selectable for Restore - True
  - Restore methods supported - VSS_RME_RESTORE_IF_CAN_REPLACE

The only extension of the component set structure in SQL Server is the introduction of full-text catalogs. Full-text catalogs are container directories, which can't be expressed as the VSS database or log files, given that the VSS database and log files don't have recursive specification. Therefore, the SQL writer uses a VSS filegroup component (`VSS_CT_FILEGROUP`) to represent the database level component and filegroup files to represent the database, log, and full-text catalog files.

An example writer metadata document is provided at the [end of this document](#writer-metadata-document-an-example).

#### Initiate snapshot

Requestor initiates the snapshot process by calling the VSS framework interface `DoSnapshotSet`.

#### Create snapshot

This phase involves a series of interactions between the VSS framework and the SQL writer.

1. *Prepare for snapshot*. The SQL writer calls SQL Server to prepare for snapshot creation.

1. *Freeze*. The SQL writer calls SQL Server to freeze all database I/O for each of the databases being backed up in the snapshot. Once the freeze event returns to the VSS framework, VSS creates the snapshot.

1. *Thaw*. On this event, the SQL writer calls the SQL Server instances to *thaw* or resume normal I/O operations.

The snapshot creation phase takes less than 60 seconds, to prevent blocking of all writes to the database.

#### Post-snapshot

If autorecovery is needed for the snapshot, the SQL writer does the autorecovery for each database that has been selected to be in the snapshot. For a detailed explanation, see [Auto-recovered snapshots](#auto-recovered-snapshots).

## Restore process

This section describes the restore operation workflow and various steps involved.

### Restore operation workflow

The following figure shows the dataflow diagram during a VSS restore operation.

:::image type="content" source="media/sql-server-vss-writer-backup-guide/restore-process-flow.png" alt-text="Screenshot of restore process flow.":::

To more fully understand the basic tasks involved in performing a restore, it's useful to break down this overview into the following sections:

- [Restore initialization](#restore-initialization)
- [Preparing for restore](#prepare-for-restore)
- [Actual file restoration](#restore-files)
- [Restore clean up and termination](#cleanup-and-termination)

In all VSS component-based restore scenarios, database restore is handled by the SQL writer in two distinct phases.

- **Pre-restore**: The SQL writer handles the validation, closing of file handles, etc.
- **Post-restore**: The SQL writer attaches the database and does crash recovery if needed.

Between these two phases, the backup application is responsible for moving the relevant data around underneath SQL.

### Restore initialization

During the initialization phase of a restore, the requestor needs to have access to the stored backup components documents.

The backup component document that is generated during the backup operation, is stored as part of the backup data. The backup application needs to pass this data back to the VSS framework. The SQL writer obtains access to this data at the beginning of the restore process.

#### Prepare for restore

When the requestor prepares for a restore, it uses the stored backup components document to determine what is to be restored and how. The requestor selects the components to be restored and set appropriate restore options as needed.

If a backup application intends to apply differential or log backups on top of the current restore operation (that is, *restore with norecovery* is needed), the following option should be set as part of component creation for each database that is being restored.

```cpp
IVssBackupComponents::SetAdditionalRestores(true)
```

Once all the needed details are set in the backup component document, the requestor makes the `IVssBackupComponents::PreRestore` call to generate a pre-restore event through VSS that is handled by the writers.

The SQL writer examines the supplied backup component document to identify the appropriate databases, deleting any additional files created since the backup time. It also checks disk spaces and closes any opened database file handles so that the requestor can copy the needed data during the Restore phase. This phase allows any early error conditions to be detected before the requestor does the actual file copying. SQL Server also puts the database in restoring state. From this point on, the database can't be started until a successful restore.

#### Restore files

This is purely a requestor-specific action. It's the responsibility of the requestor (backup application) to copy the needed database files (or copy relevant ranges of data for differential restores) to the appropriate places. The SQL writer isn't involved in this operation.

#### Cleanup and termination

Once all the data is restored to the right places, a call from a requestor notifying that the restore operation has been completed `IvssBackupComponents::PostRestore`, lets the SQL writer know that post-restore actions can be started. The SQL writer at this point does the *redo* phase of crash recovery. If recovery isn't requested (that is, `SetAdditionalRestores(true)` isn't specified by the requestor), the undo phase of the recovery step is also carried out during this phase.

## Backup and restore option details

This section describes in detail all backup and restore options supported by the SQL writer.

### Requestor creates a volume shadow copy

The SQL writer could be involved in the volume shadow copy creation process (outside the context of backup and restore), because the backing volumes for the database files have been added into the volume snapshot set. In this case, the SQL writer only participates in the metadata enumeration, `Freeze`, `Thaw`, `PrepareForSnapshot`, and `PostSnapshot` coordination (see the data flow diagram for detail).

### Full backup and restore

The SQL writer supports full backup and restore operations in both non-component-based mode and component-based mode.

### Non-component-based backup and restore

In a non-component-based backup and restore, the requestor specifies a volume or a folder tree to be backed up and restored. All the data in the specified volume and folder is backed up and restored.

#### Backup

In a non-component-based backup, the SQL writer implicitly selects databases by using the list of volumes in the snapshot set. The writer checks for torn databases, raising an error if found. A torn database is one in which a subset of files is selected by the list of volumes. Roll-forward (differential or log restores) after a restore isn't supported through the SQL writer.

#### Restore

The requestor restores databases that have been backed up in non-component-based mode. Such restores can't be followed up by a roll-forward restore, such as log restore or differential restore.

For non-component-based restore operations, the restore must be performed with the SQL Server instance offline or the target databases are dropped/detached to ensure that the files are offline. The files are copied in place and then the databases attached. All this happens outside the scope of the SQL writer.

### Component-based backup and restore

In a component-based backup, the requestor explicitly selects database components (from the metadata that the SQL writer returns to the client) to be backed up/restored.

#### Backup

In a component-based backup, all backing volumes for selected databases should be included in the volume snapshot set. Otherwise, the SQL writer detects torn databases (with backing volumes outside the snapshot set) and fails the backup. A full backup backs up database data and all the log files necessary to bring the database up to a transactionally consistent state at restore time.

#### Full restore without roll-forward

A full restore of the database backup is sometimes accomplished without doing any additional roll-forward. This might be because there's no metadata to facilitate the roll-forward or, in some cases, roll-forward isn't needed. This section covers these two situations briefly.

#### No metadata/no roll-forward

If no writer metadata (component-based backup metadata) is saved during the backup operation, then the restore must be performed with the SQL Server instance offline or the target databases are dropped/detached to ensure that the files are offline. The files are copied in place and then the databases attached. All this happens outside the scope of the SQL writer.

#### Metadata exist but no additional roll-forward is needed

The requestor restores databases that have been backed up in component-based mode but no roll-forward is requested. In this case SQL Server performs crash recovery on the database as part of restore.

### Full restore with additional roll-forward

The requestor can issue a restore specifying the `SetAdditionalRestores(true)` option. This option indicates that the requestor is going to follow up with more roll-forward restores (such as log restore, differential restore, etc.). This instructs SQL Server not to perform the recovery step at the end of the restore operation.

This is only possible if the writer metadata was saved during the backup and is available to the SQL writer at the time of the restore. The SQL Server service must be running before the requestor directs VSS to perform the restore activity.

The SQL writer expects the following sequence:

1. Preparation to restore each database. This activity involves closing all the file handles so as to allow database files to be copied/mounted by the requestor application.

1. Files are copied/mounted by the requestor application.

1. Finalize the restore (with `NORECOVERY`). The databases are brought online, but into a *restoring* state.

Conventional SQL Server backups, differential, or logs, can then be used to roll forward the database through the VDI or Transact-SQL, or by applying the differential restore using the VSS framework.

### Full-text support

The SQL writer reports full-text catalog containers with recursive file specifications under the database components in the writer metadata document. They're automatically included in the backup when the database component is selected.

### Differential backup and restore

A differential backup operation backs up only the data that has changed since the most recent base full backup. A differential backup contains only those parts of the database files that have changed. In order to do such a backup, the requestor (backup application) would need information about the location of the changes in the database files, so that appropriate sections of the files can be backed up. During a differential backup operation, the SQL writer provides this information in the format as specified by *VSS partial file information*. This information can be used to back up only the changed portion of the database files.

### Backup

The requestor can issue a differential backup by setting the `DIFFERENTIAL` option `VSS_BT_DIFFERENTIAL` in the backup component document `IVssBackupComponents::SetBackupState`, when initiating a backup operation with VSS. The SQL writer passes the partial file information (returned to it by SQL Server) to VSS. The requestor can obtain this file information by calling VSS API's `IVssComponent::GetPartialFile`. This partial file information allows the requestor to choose only changed byte-ranges to back up for the database files.

During the pre-backup tasks phase, the SQL writer makes sure that a single differential base for each selected database exists.

During the `PostSnapshot` event, the SQL writer obtains the partial file information from SQL Server and adds it to backup component document using `IVssComponent::AddPartialFile` call.

> [!NOTE]  
> The SQL writer supports only a single differential baseline for differential backups. Multi-baselines aren't supported.

### Partial file information format

For each database being backed up during a differential backup, the SQL writer stores the partial file information for each database file. This information is used by the requestor or backup application to copy only relevant portions of the file to the backup medium during the actual backup of the files.

For more information on the format for this partial file information, see [Volume Shadow Copy Service (VSS)](/windows-server/storage/file-server/volume-shadow-copy-service).

A requestor can determine these files by calling `IVssComponent::GetPartialFileCount` and `IVssComponent::GetPartialFile`. `IVssComponent::GetPartialFile` returns a path and a filename pointing to the file, and a ranges string indicating what needs to be backed up in the file.

For more information of the partial file information retrieval, see the [VSS documentation](/windows/win32/vss/volume-shadow-copy-service-overview).

### Back up files

During this phase, the backup application should look at the writer metadata stored in the backup component document and back up only the relevant portions of the files. (For full-text catalog files, this backup should be done based on the file timestamps. This is described later in this article).

A differential backup always relates to the latest base backup that exists for the database. At restore time, SQL Server detects mismatched base and differential backups. So, it's the responsibility of the backup application or system administrator to be sure that the differential is relative to the expected full backup. If some out of band procedure has made another full backup, the backup application might not be able to restore the differential, since it doesn't own the base backup.

Currently if the byte-range information (partial file information) is too large (exceeding 64 KB in buffer size), SQL Server throws an error instructing the user to perform a full backup.

### Troubleshooting

File add/drop/shrink/growth/logical-rename/physical-rename make interesting cases in backup troubleshooting.

#### Files newly added after the base was taken

These files are included in the partial specification because every header of the database file needs to be in the partial specification. Besides the header page, all the allocated pages need to be included in the partial specification.

#### Files dropped after base was taken

After the base was taken, data files could be dropped. Such files aren't included in the writer metadata document during differential backup. Furthermore, no partial information is associated with the dropped file.

#### Files shrunk after the base was taken

The partial information isn't collected from the files until file shrinks have been disabled in the server. This ensures that VSS never includes any partial information that corresponds to the shrunk region of a data file.

#### Files grown after the base was taken

If growth took place before the partial information is collected, then the partial information should have included the allocated pages in the grown region. If the growth took place after the partial information is collected, then the partial information doesn't include changes in the grown region. In the following sections, you see that such changes are restored by the log roll-forward.

#### File logically renamed after the base was taken

A logical rename of the file doesn't affect the backup or restore, since the file's logical name isn't referenced anywhere in the writer metadata document or in the backup component document.

For more information, see [Writer metadata document: An example](#writer-metadata-document-an-example) later in this article.

#### File physically renamed after the base was taken

A physical database file rename doesn't take effect until the database restarts. Therefore, the database configuration information or the file path information in the partial information buffer is still based on the old physical paths, which are the only valid paths to those database files on the snapshot.

### Restore

During a differential restore, the backup metadata that the requestor gives back to the SQL writer has the backup type information. Therefore, no special treatment from the SQL writer is needed. SQL Server figures out that it's a differential restore by itself. SQL Server handles such a differential restore in the same way as against a native differential restore that isn't performed through VSS.

### Pre-restore phase

During this phase SQL Server resizes all files to the appropriate size based on the differential backup's file metadata. If the file is grown, SQL Server zeroes out the grown portion. If a new file has to be created (it was created after the base was taken), SQL Server zeroes out the new file. It also closes all the file handles so that the backup application can overwrite the files with the restored data from the backup media.

### Restore files

The client should restore the files based on the partial file specification. The data should be restored to the same offset/range of the database file as specified in the partial file specification stored in the writer metadata.

Database file add/drop/growth/shrink/logical-rename/physical-rename again makes interesting troubleshooting cases at restore time.

#### If a database file has been added after the full base was taken

Such files must have been precreated by SQL Server during the restore preparation phase. They should have been extended to the right size and zeroed out. The client only needs to lay down the data as per partial specification (the partial specification includes all allocated extents).

#### If a database file has been dropped after the full base was taken

The partial information that SQL Server has provided doesn't include any tracking information for such file drops. SQL Server is responsible for detecting the files to be deleted, by comparing the restored files metadata against the existing containers, and actually deleting them. This is done before the restore as a preparation step.

#### If a database file has grown since the full base was taken

Such files must be extended to the right size by SQL Server during the restore preparation phase. The extended region must be zeroed out by SQL Server as well. Therefore, the client can safely lay down the data even in the grown region as per partial specification. If the file was grown after the partial information was taken, the changes in the grown region are restored by replaying the log that was backed up along with the differential backup.

#### If a database file has shrunk since the full base was taken

SQL Server is responsible for truncating the file to the required size as per metadata. This is done before the restore as a preparation step.

#### If a database file has been logically renamed since the full base was taken

This doesn't affect the restore, as the logical name doesn't appear in the writer metadata document or the backup component document. The logical name change is restored when the client applies the change to the primary database file, which contains the system catalog information.

#### If a database file has been physically renamed since the full base was taken

If by the time of differential backup, the rename hasn't taken effect, then the client still restores data to the old location. A database restart post-restore causes the physical rename to take effect. If by the time of differential backup, the physical file rename had already taken effect, then the partial data, if any, is backed up from the new physical path.

### Post restore

During the post restore events, the SQL writer performs the normal redo operation and recovery (if `SetAdditionalRestores()` is set to `False`) of the database.

## Differential backup and restore for full-text catalogs

SQL Server full-text catalogs are part of the database resources that need to be backed up or restored together with the rest of the database files. A differential backup is timestamp-based for full-text catalog. The SQL Server VSS differential backup and restore has a single base backup. In other words, there aren't different bases for different containers. For VSS full-text catalog backup, this means for all full-text catalog containers, the differential backup is single-timestamp based, unlike the case of native SQL Server differential backup, in which there's one timestamp base per full-text catalog container.

In VSS, this timestamp is expressed as a component-wide property that is set during the full backup, and used during a subsequent differential backup.

### OnIdentify

In `OnIdentify`, the SQL writer calls `IVssCreateWriterMetadata::SetBackupSchema()` to set the value `VSS_BS_TIMESTAMPED`. This indicates to the backup application that the SQL writer owns the management of the differential base.

<a id="setting-the-base-timestamp"></a>

### Set the base timestamp

The base timestamp is set during a full backup. In `OnPostSnapshot()`, the writer invokes `IVssComponent::SetBackupStamp()` to store the timestamp with the component in the backup document.

### Differential backup

The backup application retrieves this timestamp from the base full backup, and makes the timestamp available for the writer by calling `IVssComponent::GetBackupStamp()` to retrieve the base stamp from the previous base backup. Then it makes it available to the writer by calling `IVssBackupComponent::SetPreviousBackupStamp()`. Writer then retrieves the stamp by calling `IVssComponent::GetPreviousBackupStamp()` and translates it into a timestamp used for `IVssComponent::AddDifferencedFilesByLastModifyTime()`.

#### Backup application's responsibility during differential backup

During a differential backup, the backup application is responsible for:

- Backing up any file (the entire file) whose last modified timestamp is greater than the timestamp specified by the *last modify time* for the file set in the component.

- Tracking and detecting deleted files.

#### Backup application's responsibilities during a differential restore

During a differential restore, the backup application is responsible for:

- Restoring all files that have been backed up, either by creating a new file if it doesn't already exist, or by overwriting an existing file if it already exists.

- Growing the file before laying down the content if the restored file is larger than the existing file.

- Truncating the file to the same size as that of the restored file if the restored file is smaller than the existing file.

- Deleting all files that should be deleted; that is, those files that shouldn't exist as of the point in time of the differential backup.

### Copy-only backup

It's sometimes necessary to take a backup that is intended for a special purpose. For example, you might need to make a copy of a database for testing purposes. This backup shouldn't affect the overall backup and restore procedures for the database. Using the `COPY_ONLY` option specifies that the backup is done out-of-band and shouldn't affect the normal sequence of backups. The SQL writer supports the copy-only backup type with SQL Server instances.

During the backup discovery phase, the SQL writer indicates its capability to do a copy-only backup by setting the supported backup schema option `VSS_BS_COPY` using the `IVssCreateWriterMetadata::SetBackupSchema` call. The requestor can set the backup type as a copy-only backup by setting the `VSS_BACKUP_TYPE` option as `VSS_BT_COPY` with the call `IVssBackupComponents::SetBackupState`.

When a copy-only backup is selected, it's assumed that files on disk are copied to a backup medium (by the requestor) regardless of the state of each file's backup history. SQL Server doesn't update the backup history. This type of backup doesn't constitute a base backup for further differential backup operations, and it also doesn't disturb the history of the previous differential backups.

### Restore with move

VSS allows the requestor (backup application) to specify a new restore target using the `IVssComponent::SetNewTarget` call. In both `PreRestore()` and `PostRestore()`, the SQL writer checks if there's at least one new target specified. It's the backup application's responsibility to physically copy the files to the new location during the actual file restore/copy time.

The backup application is only allowed to specify new targets for the physical path, but not the file specification. For example, for a database file located at `c:\data\test.mdf`, the actual file name `test.mdf` can't be changed. Only the path `c:\data` can be changed. For a full-text catalog container located at `c:\ftdata\foo`, since the file specification in VSS is `"*"` and the path specification in VSS is `c:\ftdata\foo`, the entire path can be changed.

### Database rename

A requestor might need to restore a SQL Server database with a new name, especially if the database is to be restored side by side with the original database. This option can be specified by the requestor during the restore operation by setting a custom restore option as "New Component Name" = <"New Name"> using the VSS call `IVssBackupComponents::SetRestoreOptions()` (in the `wszRestoreOptions` parameter).

The SQL writer takes the entire content of New Component Name's value and uses it as the new name for the restored database. If no option is specified, SQL Server restores the database with its original name (component name).

> [!NOTE]  
> The SQL writer currently doesn't support **Rename across Instances** to move a database to a new instance.

### Auto-recovered snapshots

Typically a snapshot of SQL Server database obtained by using VSS framework is in a non-recovered state. Data in the snapshot can't be safely accessed before going through the recovery phase to roll back in-flight transactions and placing the database in a consistent state. Since the snapshot is in a read-only state, it can't be recovered by the normal process of attaching the database.

It's possible to autorecover the snapshots as part of the snapshot creation process. As part of the writer metadata document, the SQL writer specifies the component flag `VSS_CF_APP_ROLLBACK_RECOVERY` to indicate that recovery needs to be performed for the database on snapshot before the database can be accessed when specifying the snapshot set, the requestor can indicate that the snapshot should be an app-rollback snapshot (that is, all database files in a snapshot are meant to be in a consistent state for application usage) or a backup snapshot (a snapshot used for backing up data to be restored later if there's a system failure).

The requestor should set `VSS_VOLSNAP_ATTR_ROLLBACK_RECOVERY` to indicate that this component is being backed up for a non-backup purpose. VSS then correlates `VSS_CF_APP_ROLLBACK_RECOVERY` that the SQL writer specified on the selected component with `VSS_VOLSNAP_ATTR_ROLLBACK_RECOVERY`, and determines that auto recovery is happening. VSS makes the snapshot writeable for a limited period of time and automatically add the `VSS_VOLSNAP_ATTR_AUTORECOVERY` bit to the snapshot context.

In SQL Server, autorecovery should be applied only to app-rollback snapshots but not for backup snapshots. For app-rollback snapshots, an autorecovery process is initiated by the SQL writer during the `PostSnapShotevent`. This process does the following for each explicitly selected (by the requestor) SQL Server database in the snapshot set:

- Attach the snapshot database to the original SQL Server instance (that is, the instance to which the original database is attached).

- Recover the database (this happens as part of the "attach" operation).

- Shrink log files.

  This reduces the amount of unnecessary copy-on-write that needs to be done by the VSS framework, if VSS provider is a *software provider*. Shrinking the log files is the default behavior. This can be disabled by setting the value to the following registry key to `1`.

  ```output
  HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SQLWriter\Settings\DisableLogShrink
  ```

  This might be useful in scenarios where the snapshot might be used to export data from a specific page (at a specific point in time) from the log to fix a problem in the online database.

- Detach the database.

Now there is a consistent, recovered snapshot that can be attached for querying.

### Multi-database transactions

In older versions of SQL Server, the snapshot databases might sometimes contain some in-flight multi-database transactions. During recovery operation, the SQL writer attaches the database on the snapshots with the Presumed Abort option. This would roll back any multi-database transaction that isn't yet committed (including any transactions that are in a Prepared to Commit state). This might lead to some inconsistencies between databases in the snapshot set.

For example, consider two databases A and B. There's a distributed transaction between these two databases and this transaction is in Committed state in database A and in Prepared to Commit state in database B. As part of the autorecovery process, this transaction is committed in database A and rolled back in database B. This might lead to some inconsistencies in the snapshot set.

Newer versions of Windows have an improved Microsoft Distributed Transaction Coordinator (MS DTC) component that fixes this inconsistency problem for transactions spanning databases across SQL Server instances. Newer versions of SQL Server fix these inconsistencies for transactions spanning databases within a SQL Server instance.

### Security implications for autorecovered snapshots

For VSS snapshots, after the auto recovery, the files are secured using Access Control Lists (ACLs) to allow access only to the special builtin group which the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] account belongs to. This implies that members of either box admin or that special group are able to attach the database. The client requesting an attach of the database files on a snapshot either has to be a member of `Builtin/Administrators` or the SQL Server account.

#### Simple recovery model user databases

If the `master` database is restored together with user databases that are using the simple recovery model, the user databases can be restored with the same technique as the `master` database: with the instance shut down, just copy or mount the volumes. When the SQL instance is started, everything recovers.

#### Rolling forward user databases

If user databases are to be recovered and rolled forward together with `master` database recovery, the instance must not start up and recover the `master` and user databases together.

The procedure is as follows:

1. Ensure that the SQL Server instance is stopped.

1. Perform the restore in two phases.

   1. Restore the system databases and user databases that should be recovered at the same time (that is, user databases in the simple recovery model) via file copy, or volume-mount, through VSS.

      - If the user databases to be rolled forward aren't on the same volume as the system databases, then that volume shouldn't be brought back at this time. This scenario requires planning before the backup.

      - If the user databases are on the same volume as the system databases, then the user databases need to be hidden from SQL Server.

   1. Start the SQL Server instance using the `-f` parameter. (When using the `-f` startup option, only the `master` database can be restored.)

      1. Issue an `ALTER DATABASE <database> SET OFFLINE` (or detatch the database) for each database to be rolled forward.

      1. Stop the SQL Server instance.

      1. Start the SQL Server instance (the files for the user databases to roll forward aren't visible to SQL Server).

Use VSS to restore the user databases `WITH NORECOVERY`, as described in [Full restore with additional roll-forward](#full-restore-with-additional-roll-forward).

### Writer metadata document: An example

A database named `DB1`, belonging to SQL Server instance `Instance1` on machine `Server1`, contains the following database/log files:

- Database file named "primary" stored at `c:\db\DB1.mdf`
- Database file named "secondary" stored at `c:\db\DB1.ndf`
- Database log file named "log" stored at `c:\db\DB1.ldf`
- Full-text catalog named "foo" stored under the directory `c:\db\ftdata\foo`
- Full-text catalog named "bar" stored under the directory `c:\db\ftdata\bar`

Following is the database's writer metadata:

#### Database level filegroup component

**Primary database file:**

```output
ComponentType: VSS_CT_FILEGROUP
LogicalPath: "Server1\Instance1"
ComponentName: "DB1"
Caption: NULL
pbIcon: NULL
cbIcon: 0
bRestoreMetadata: FALSE
NotifyOnBackupComplete: TRUE
Selectable: TRUE
SelectableForRestore: TRUE
ComponentFlags: VSS_CF_APP_ROLLBACK_RECOVERY
```

**Secondary database file:**

```output
LogicalPath: "Server1\Instance1"
GroupName: "DB1"
Path: "c:\db"
FileSpec: "DB1.mdf"
Recursive: FALSE
AlternatePath: NULL
BackupTypeMask: VSS_FSBT_ALL_BACKUP_REQUIRED | VSS_FSBT_ALL_SNAPSHOT_REQUIRED
Filegroup file
LogicalPath: "Server1\Instance1"
GroupName: "DB1"
Path: "c:\db"
FileSpec: "DB1.ndf"
Recursive: FALSE
AlternatePath: NULL
BackupTypeMask: VSS_FSBT_ALL_BACKUP_REQUIRED | VSS_FSBT_ALL_SNAPSHOT_REQUIRED
```

**Full-text file log:**

```output
LogicalPath: "Server1\Instance1"
GroupName: "DB1"
Path: "c:\db"
FileSpec: "DB1.ldf"
Recursive: FALSE
AlternatePath: NULL
BackupTypeMask: VSS_FSBT_ALL_BACKUP_REQUIRED | VSS_FSBT_ALL_SNAPSHOT_REQUIRED
```

**Full-text file foo:**

```output
LogicalPath: "Server1\Instance1"
GroupName: "DB1"
Path: "c:\db\ftdata\foo"
FileSpec: "*"
Recursive: TRUE
AlternatePath: NULL
BackupTypeMask: VSS_FSBT_ALL_BACKUP_REQUIRED | VSS_FSBT_ALL_SNAPSHOT_REQUIRED
```

**Full-text file bar:**

```output
LogicalPath: "Server1\Instance1"
GroupName: "DB1"
Path: "c:\db\ftdata\bar"
FileSpec: "*"
Recursive: TRUE
AlternatePath: NULL
BackupTypeMask: VSS_FSBT_ALL_BACKUP_REQUIRED | VSS_FSBT_ALL_SNAPSHOT_REQUIRED
```

If the server instance is the default instance on the machine, the logical path becomes one part: `Server1`.

### Special cases

This section describes some of the special cases encountered during SQL writer-based backup and restore operations.

#### Autoclose databases

For non-component-based backups, autoclosing of databases is done, when checking for torn conditions, but the autoclosed databases aren't explicitly frozen during backup operations.

The expected scenario here is that many closed databases might exist, and you want to minimize the cost of the snapshot. Autoclosed databases are typically used in low-end configurations where resources are scarce.

#### File list

The list of files for each database is determined during an enumeration step before the Prepare for Backup event. If the list of database files changes between enumeration and freeze, then the database could be torn, unless the application rechecks the list of files. While this scenario is unlikely, it's something that vendors need to be aware of.

#### Stopped instances

If an instance of SQL Server isn't running at the time the enumeration step occurs, then none of the databases for those instances can be selected.

If an instance stops in the interval between enumeration and the Prepare for Backup event, any databases in the stopped instance are ignored.

#### System and user databases

System databases in SQL Server include the `master`, `model`, and `msdb` databases that are shipped and installed as part of SQL Server. This section describes how these databases are handled in a VSS snapshot backup process.

The `master` database can only be restored by stopping the instance, replacing the database files (done by the backup application), then restarting the instance. No roll-forward is possible.

The SQL writer supports restore of both `model` and `msdb` databases online, without shutting down the instance.

## Related content

- [SQL Server VSS Writer logging](sql-server-vss-writer-logging.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Back up and restore of SQL Server databases](back-up-and-restore-of-sql-server-databases.md)
- [Copy-only backups](copy-only-backups-sql-server.md)
- [Transaction log backups (SQL Server)](transaction-log-backups-sql-server.md)
- [Apply Transaction Log Backups (SQL Server)](apply-transaction-log-backups-sql-server.md)
- [SQL Server transaction log architecture and management guide](../sql-server-transaction-log-architecture-and-management-guide.md)
