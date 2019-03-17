---
title: "Use the Maintenance Plan Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql12.ag.maintwiz.planprop.f1"
  - "sql12.ag.maintwiz.task.f1"
  - "sql12.ag.maintwiz.maintcleanup.f1"
  - "sql12.ag.maintwiz.indexdefrag.f1"
  - "sql12.ag.maintwiz.order.f1"
  - "sql12.ag.maintwiz.histcleanup.f1"
  - "sql12.ag.maintwiz.backuplog.f1"
  - "sql12.ag.maintwiz.progress.f1"
  - "sql12.ag.maintwiz.execagentjob.f1"
  - "sql12.ag.maintwiz.integrity.f1"
  - "sql12.ag.maintwiz.welcome.f1"
  - "sql12.ag.maintwiz.backupdiff.f1"
  - "sql12.ag.maintwiz.server.f1"
  - "sql12.ag.maintwiz.summary.f1"
  - "sql12.ag.maintwiz.backupfull.f1"
  - "sql12.ag.maintwiz.report.f1"
  - "sql12.ag.maintwiz.shrinkdb.f1"
  - "sql12.ag.maintwiz.reindex.f1"
  - "sql12.ag.maintwiz.updatestats.f1"
helpviewer_keywords: 
  - "Maintenance Plan Wizard"
  - "Database Maintenance Plan Wizard"
  - "Database Maintenance Plan Wizard, starting"
ms.assetid: db65c726-9892-480c-873b-3af29afcee44
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Use the Maintenance Plan Wizard
  This topic describes how to create a single server or multiserver maintenance plan using the Maintenance Plan Wizard in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. The Maintenance Plan Wizard creates a maintenance plan that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent can run on a regular basis. This allows you to perform various database administration tasks, including backups, database integrity checks, or database statistics updates, at specified intervals.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   [Creating a maintenance plan using the Maintenance Plan Wizard in SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   To create a multiserver maintenance plan, a multiserver environment containing one master server and one or more target servers must be configured. Multiserver maintenance plans must be created and maintained on the master server. These plans can be viewed, but not maintained, on target servers.  
  
-   Members of the **db_ssisadmin** and **dc_admin** roles may be able to elevate their privileges to **sysadmin**. This elevation of privilege can occur because these roles can modify [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages; these packages can be executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the **sysadmin** security context of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. To guard against this elevation of privilege when running maintenance plans, data collection sets, and other [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs that run packages to use a proxy account with limited privileges or only add **sysadmin** members to the **db_ssisadmin** and **dc_admin** roles.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To create or manage maintenance plans, you must be a member of the **sysadmin** fixed server role. Object Explorer only displays the **Maintenance Plans** node for users who are members of the **sysadmin** fixed server role.  
  
##  <a name="SSMSProcedure"></a> Using Maintenance Plan Wizard  
  
#### To start the Maintenance Plan Wizard  
  
1.  Expand the server where you want to create your management plan.  
  
2.  Expand the **Management** folder.  
  
3.  Right-click the **Maintenance Plans** folder and select **Maintenance Plan Wizard**.  
  
4.  On the **SQL Server Maintenance Plan Wizard** page, click **Next**.  
  
5.  On the **Select Plan Properties** page:  
  
    1.  In the **Name** box, enter the name of the maintenance plan you are creating.  
  
    2.  In the **Description** box, briefly describe your maintenance plan.  
  
    3.  In the **Run as** list, specify the credential that Microsoft SQL Server Agent uses when executing the maintenance plan.  
  
    4.  Select either **Separate schedules for each task** or **Single schedule for the entire plan or no schedule** to specify the recurring schedule of the maintenance plan.  
  
        > [!NOTE]  
        >  If you select **Separate schedules for each task**, you will need to follow the steps in **e.** below for each task in your maintenance plan.  
  
    5.  If you selected **Single schedule for the entire plan or no schedule**, under **Schedule**, click **Change**.  
  
        1.  In the **New Job Schedule** dialog box, in the **Name** box, enter the job schedule's name.  
  
        2.  On the **Schedule type** list, select the type of schedule:  
  
            -   **Start automatically when SQL Server Agent starts**  
  
            -   **Start whenever the CPUs become idle**  
  
            -   **Recurring**. This is the default selection.  
  
            -   **One time**  
  
        3.  Select or clear the **Enabled** check box to enable or disable the schedule.  
  
        4.  If you select **Recurring**:  
  
            1.  Under **Frequency**, on the **Occurs** list, specify the frequency of occurrence:  
  
                -   If you select **Daily**, in the **Recurs every** box, enter how often the job schedule repeats in days.  
  
                -   If you select **Weekly**, in the **Recurs every** box, enter how often the job schedule repeats in weeks. Select the day or days of the week on which the job schedule is run.  
  
                -   If you select **Monthly**, select either **Day** or **The**.  
  
                    -   If you select **Day**, enter both the date of the month you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the 15th day of the month every other month, select **Day** and enter "15" in the first box and "2" in the second box. Please note that the largest number allowed in the second box is "99".  
  
                    -   If you select **The**, select the specific day of the week within the month that you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the last weekday of the month every other month, select **Day**, select **last** from the first list and **weekday** from the second list, and then enter "2" in the last box. You can also select **first**, **second**, **third**, or **fourth**, as well as specific weekdays (for example: Sunday or Wednesday) from the first two lists. Please note that the largest number allowed in the last box is "99".  
  
            2.  Under **Daily frequency**, specify how often the job schedule repeats on the day the job schedule runs:  
  
                -   If you select **Occurs once at**, enter the specific time of day when the job schedule should run in the **Occurs once at** box. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
                -   If you select **Occurs every**, specify how often the job schedule runs during the day chosen under **Frequency**. For example, if you want the job schedule to repeat every 2 hours during the day that the job schedule is run, select **Occurs every**, enter "2" in the first box, and then select **hour(s)** from the list. From this list you can also select **minute(s)** and **second(s)**. Please note that the largest number allowed in the first box is "100".  
  
                     In the **Starting at** box, enter the time that the job schedule should start running. In the **Ending at** box, enter the time that the job schedule should stop repeating. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
            3.  Under **Duration**, in **Start date**, enter the date that you want the job schedule to start running. Select **End date** or **No end date** to indicate when the job schedule should stop running. If you select **End date**, enter the date that you want to job schedule to stop running.  
  
        5.  If you select **One Time**, under **One-time occurrence**, in the **Date** box, enter the date that the job schedule will be run. In the **Time** box, enter the time that the job schedule will be run. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
        6.  Under **Summary**, in **Description**, verify that all job schedule settings are correct.  
  
        7.  Click **OK**.  
  
    6.  Click **Next**.  
  
6.  On the **Select Target Servers** page, select the servers where you want to run the maintenance plan. This page is only visible on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances that are configured as master servers.  
  
    > [!NOTE]  
    >  To create a multiserver maintenance plan, a multiserver environment containing one master server and one or more target servers must be configured, and the local server should be configured as a master server. In multiserver environments, this page displays the **(local)** master server and all corresponding target servers.  
  
7.  On the **Select Maintenance Tasks** page, select one or more maintenance tasks to add to the plan. When you have selected all of the necessary tasks, click **Next**.  
  
    > [!NOTE]  
    >  The tasks you select here will determine which pages you will need to complete after the **Select Maintenance Task Order** page below.  
  
8.  On the **Select Maintenance Task Order** page, select a task and click either **Move Up...** or **Move Down...** to change its order of execution. When finished, or if you are satisfied with the current order of tasks, click **Next**.  
  
    > [!NOTE]  
    >  If you selected **Separate schedules for each task** on the **Select Plan Properties** page above, you will not be able to change the order of the maintenance tasks on this page.  
  
#### Define Database Check Integrity (CHECKDB) Tasks  
  
1.  On the **Define Database Check Integrity Task** page, choose the database or databases where the allocation and structural integrity of user and system tables and indexes will be checked. By running the `DBCC CHECKDB`[!INCLUDE[tsql](../../includes/tsql-md.md)] statement, this task ensures that any integrity problems with the database are reported, thereby allowing them to be addressed later by a system administrator or database owner. For more information, see [DBCC CHECKDB &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql)When complete, click **Next**.  
  
     The following options are available on this page.  
  
     **Databases** list  
     Specify the databases affected by this task.  
  
    -   **All databases**  
  
         Generate a maintenance plan that runs this task against all [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases except **tempdb**.  
  
    -   **System databases**  
  
         Generate a maintenance plan that runs this task against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases except **tempdb** and user-created databases.  
  
    -   **All user databases (excluding master, model, msdb, tempdb)**  
  
         Generate a maintenance plan that runs this task against all user-created databases. No maintenance tasks are run against the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases.  
  
    -   **These databases**  
  
         Generate a maintenance plan that runs this task against only those databases that are selected. At least one database in the list must be selected if this option is chosen.  
  
     **Include indexes** check box  
     Check the integrity of all the index pages as well as the table data pages.  
  
#### Define Database Shrink Tasks  
  
1.  On the **Define Shrink Database Task** page, create a task that attempts to reduce the size of the selected databases by using the `DBCC SHRINKDATABASE` statement, with either the `NOTRUNCATE` or `TRUNCATEONLY` option. For more information, see [DBCC SHRINKDATABASE &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql). When complete, click **Next**.  
  
    > [!WARNING]  
    >  Data that is moved to shrink a file can be scattered to any available location in the file. This causes index fragmentation and can slow the performance of queries that search a range of the index. To eliminate the fragmentation, consider rebuilding the indexes on the file after shrinking.  
  
     The following options are available on this page.  
  
     **Databases** list  
     Specify the databases affected by this task. See step 9, above, for more information on the available options on this list.  
  
     **Shrink database when it grows beyond** box  
     Specify the size in megabytes that causes the task to execute.  
  
     **Amount of free space to remain after shrink** box  
     Stop shrinking when free space in database files reaches this size (as a percentage).  
  
     **Retain freed space in database files**  
     The database is condensed to contiguous pages but the pages are not deallocated, and the database files do not shrink. Use this option if you expect the database to expand again, and you do not want to reallocate space. With this option, the database files do not shrink as much as possible. This uses the NOTRUNCATE option.  
  
     **Return freed space to operating system**  
     The database is condensed to contiguous pages and the pages are released back to the operating system for use by other programs. This database files shrink as much as possible. This uses the TRUNCATEONLY option. This is the default option.  
  
#### Define the Index Tasks  
  
1.  On the **Define Reorganize Index Task** page, select the server or servers where you'll be moving index pages into a more efficient search order. This task uses the `ALTER INDEX ... REORGANIZE` statement. For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql). When complete, click **Next**.  
  
     The following options are available on this page.  
  
     **Databases** list  
     Specify the databases affected by this task. See step 9, above, for more information on the available options on this list.  
  
     **Object** list  
     Limit the **Selection** list to display tables, views, or both. This list is only available if a single database is chosen from the **Databases** list above.  
  
     **Selection** list  
     Specify the tables or indexes affected by this task. Not available when **Tables and Views** is selected in the Object box.  
  
     **Compact large objects** check box  
     Deallocate space for tables and views when possible. This option uses `ALTER INDEX ... LOB_COMPACTION = ON`.  
  
2.  On the **Define Rebuild Index Task** page, select the database or databases where you'll be re-creating multiple indexes. This task uses the `ALTER INDEX ... REBUILD PARTITION` statement. For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql).) When complete, click **Next**.  
  
     The following options are available on this page.  
  
     **Databases** list  
     Specify the databases affected by this task. See step 9, above, for more information on the available options on this list.  
  
     **Object** list  
     Limit the **Selection** list to display tables, views, or both. This list is only available if a single database is chosen from the **Databases** list above.  
  
     **Selection** list  
     Specify the tables or indexes affected by this task. Not available when **Tables and Views** is selected in the Object box.  
  
     **Free space options** area  
     Presents options for applying fill factor to indexes and tables.  
  
     **Default free space per page**  
     Reorganizes the pages with the default amount of free space. This will drop the indexes on the tables in the database and re-create them with the fill factor that was specified when the indexes were created. This is the default option.  
  
     **Change free space per page to** box  
     Drop the indexes on the tables in the database and re-create them with a new, automatically calculated fill factor, thereby reserving the specified amount of free space on the index pages. The higher the percentage, the more free space is reserved on the index pages, and the larger the index grows. Valid values are from 0 through 100. Uses the `FILLFACTOR` option.  
  
     **Advanced options** area  
     Presents additional options for sorting indexes and reindexing.  
  
     **Sort results in tempdb** check box  
     Uses the `SORT_IN_TEMPDB` option which determines where the intermediate sort results, generated during index creation, are temporarily stored. If a sort operation is not required, or if the sort can be performed in memory, the `SORT_IN_TEMPDB` option is ignored.  
  
     **Keep index online while reindexing** check box  
     Uses the `ONLINE` option which allows users to access the underlying table or clustered index data and any associated nonclustered indexes during index operations. Selecting this option activates additional options for rebuilding indexes that do not allow for online rebuilds: **Do not rebuild indexes** and **Rebuild indexes offline**.  
  
    > [!NOTE]  
    >  Online index operations are not available in every edition of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
#### Define the Update Statistics Task  
  
1.  On the **Define Update Statistics Task** page, define the database or databases on which table and index statistics will be updated. This task uses the `UPDATE STATISTICS` statement. For more information, see [UPDATE STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/update-statistics-transact-sql) When finished, click **Next**  
  
     The following options are available on this page.  
  
     **Databases** list  
     Specify the databases affected by this task. See step 9, above, for more information on the available options on this list.  
  
     **Object** list  
     Limit the **Selection** list to display tables, views, or both. This list is only available if a single database is chosen from the **Databases** list above.  
  
     **Selection** list  
     Specify the tables or indexes affected by this task. Not available when **Tables and Views** is selected in the Object box.  
  
     **All existing statistics**  
     Update statistics for both columns and indexes.  
  
     **Column statistics only**  
     Only update column statistics. Uses the `WITH COLUMNS` option.  
  
     **Index statistics only**  
     Only update index statistics. Uses the `WITH INDEX` option.  
  
     **Scan type**  
     Type of scan used to gather updated statistics.  
  
     **Full scan**  
     Read all rows in the table or view to gather the statistics.  
  
     **Sample by**  
     Specify the percentage of the table or indexed view, or the number of rows to sample when collecting statistics for larger tables or views.  
  
#### Define the History Cleanup Task  
  
1.  On the **Define History Cleanup Task** page, define the database or databases where you want to discard old task history. This task uses the `EXEC sp_purge_jobhistory`, `EXEC sp_maintplan_delete_log`, and `EXEC sp_delete_backuphistory` statements to remove history information from the **msdb** tables. When finished, click **Next**.  
  
     The following options are available on this page.  
  
     **Select the historical data to delete**  
     Chose the type of task data to delete/  
  
     **Backup and restore history**  
     Retaining records of when recent backups were created can help [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] create a recovery plan when you want to restore a database. The retention period should be at least the frequency of full database backups.  
  
     **SQL Server Agent Job history**  
     This history can help you troubleshoot failed jobs, or determine why database actions occurred.  
  
     **Maintenance Plan history**  
     This history can help you troubleshoot failed maintenance plan jobs, or determine why database actions occurred.  
  
     **Remove historical data older than**  
     Specify age of items that you want to delete. You can specify **Hour(s)**, **Day(s)**, **Week(s)** (the default), **Month(s)**, or **Year(s)**  
  
#### Define the Execute Agent Job Task  
  
1.  On the **Define Execute Agent Job Task** page, under **Available SQL Server Agent jobs**, choose the job or jobs to run. This option will not be available if you have no SQL Agent jobs. This task uses the `EXEC sp_start_job` statement. For more information, see [sp_start_job &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-start-job-transact-sql)When finished, click **Next**.  
  
#### Define Backup Tasks  
  
1.  On the **Define Backup Database (Full) Task** page, select the database or databases on which to run a full backup. This task uses the `BACKUP DATABASE` statement. For more information, see [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql). When finished, click **Next**.  
  
     The following options are available on this page.  
  
     **Backup type** list  
     Displays the type of backup to be performed. This is read-only.  
  
     **Databases** list  
     Specify the databases affected by this task. See step 9, above, for more information on the available options on this list.  
  
     **Backup component**  
     Select **Database** to back up the entire database. Select **File and filegroups** to back up only a portion of the database. If selected, provide the file or filegroup name. When multiple databases are selected in the **Databases** box, only specify **Databases** for the **Backup components**. To perform file or filegroup backups, create a task for each database. These options are only available if a single database is chosen from the **Databases** list above.  
  
     **Backup set will expire** check box  
     Specifies when the backup set for this backup can be overwritten. Select **After** and enter a number of days to expiration, or select **On** and enter a date of expiration. This option is disabled if **URL** is selected as the backup destination.  
  
     **Back up to**  
     Specifies the medium on which to back up the database. Select **Disk**, **Tape**, or **URL**. Only tape devices attached to the computer containing the database are available.  
  
     **Back up database(s) across one or more files**  
     Click **Add** to open the **Select Backup Destination** dialog box. This option is disabled if you selected URL as the backup destination.  
  
     Click **Remove** to remove a file from the box.  
  
     Click **Contents** to read the file header and display the current backup contents of the file.  
  
     **Select Backup Destination** dialog box  
     Select the file, tape drive, or backup device for the backup destination. This option is disabled if you selected URL as the backup destination.  
  
     **If backup files exist** list  
     Specify how to handle existing backups. Select **Append** to add the new backups after any existing backups in the file or on the tape. Select **Overwrite** to remove the old content of a file or tape, and replace it with this new backup.  
  
     **Create a backup file for every database**  
     Create a backup file in the location specified in the folder box. One file is created for each database selected. This option is disabled if you selected URL as the backup destination.  
  
     **Create a sub-directory for each database** check box  
     Create a sub-directory under the specified disk directory that contains the database backup for each database being backed up as part of the maintenance plan.  
  
    > [!IMPORTANT]  
    >  The sub-directory will inherit permissions from the parent directory. Restrict permissions to avoid unauthorized access.  
  
     **Folder** box  
     Specify the folder to contain the automatically created database files. This option is disabled if you selected URL as the backup destination.  
  
     **SQL Credential**  
     Select a SQL Credential used to authenticate to Windows Azure Storage. If you do not have an existing SQL Credential you can use, click the **Create** button to create a new SQL Credential.  
  
    > [!IMPORTANT]  
    >  The dialog that opens when you click **Create** requires a management certificate or the publishing profile for the subscription. If you do not have access to the management certificate or publishing profile, you can create a SQL Credential by specifying the storage account name and access key information using Transact-SQL or SQL Server Management Studio. See the sample code in the [To create a credential](../security/authentication-access/create-a-credential.md#Credential) topic to create a credential using Transact-SQL. Alternatively, using SQL Server Management Studio, from the database engine instance, right click **Security**, select **New**, and select **Credential**. Specify the storage account name for **Identity** and the access key in the **Password** field.  
  
     **Azure storage container**  
     Specify the name of the Windows Azure storage container  
  
     **URL prefix:**  
     This is automatically generated based on the storage account information stored in the SQL Credential, and Azure storage container name you specified. We recommend that you do not edit the information in this field unless you are using a domain that uses a format other than **\<storage account>.blob.core.windows.net**.  
  
     **Backup file extension** box  
     Specify the extension to use for the backup files. The default is .bak.  
  
     **Verify backup integrity** check box  
     Verify that the backup set is complete and that all volumes are readable.  
  
     **Backup Encryption**  
     To create an encrypted backup, check the **Encrypt backup** check box. Select an encryption algorithm to use for the encryption step, and provide a Certificate or Asymmetric key from a list of existing certificates or asymmetric keys. The available algorithms for encryption are:  
  
    -   AES 128  
  
    -   AES 192  
  
    -   AES 256  
  
    -   Triple DES  
  
     The encryption option is disabled if you selected to append to existing backup set.  
  
     It is recommended practice to back up your certificate or keys and store them in a different location than the backup you encrypted.  
  
     Only keys residing in the Extensible Key Management (EKM) are supported.  
  
     **Set backup compression**  list  
     In [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] (or later versions), select one the following [backup compression](../backup-restore/backup-compression-sql-server.md) values:  
  
    |||  
    |-|-|  
    |**Use the default server setting**|Click to use the server-level default. This default is set by the **backup compression default** server-configuration option. For information about how to view the current setting of this option, see [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md).|  
    |**Compress backup**|Click to compress the backup, regardless of the server-level default.<br /><br /> **\*\* Important \*\*** By default, compression significantly increases CPU usage, and the additional CPU consumed by the compression process might adversely affect concurrent operations. Therefore, you might want to create low-priority compressed backups in a session whose CPU usage is limited by the Resource Governor. For more information, see [Use Resource Governor to Limit CPU Usage by Backup Compression &#40;Transact-SQL&#41;](../backup-restore/use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md).|  
    |**Do not compress backup**|Click to create an uncompressed backup, regardless of the server-level default.|  
  
2.  On the **Define Backup Database (Differential) Task** page, select the database or databases on which to run a partial backup. See the definition list in step 16, above, for more information about the available options on this page. This task uses the `BACKUP DATABASE ... WITH DIFFERENTIAL` statement. For more information, see [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql).  When finished, click **Next**.  
  
3.  On the **Define Backup Database (Transaction Log) Task** page, select the database or databases on which to run a backup for a transaction log. See the definition list in step 16, above, for more information about the available options on this page. This task uses the `BACKUP LOG` statement. For more information, see [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql). When finished, click **Next**.  
  
#### Define Maintenance Cleanup Tasks  
  
1.  On the **Define Maintenance Cleanup Task** page, specify the types of files to delete as part of the maintenance plan, including text reports created by maintenance plans and database backup files. This task uses the `EXEC xp_delete_file` statement. When finished, click **Next**.  
  
    > [!IMPORTANT]  
    >  This task does not automatically delete files in the subfolders of the specified directory. This precaution reduces the possibility of a malicious attack that uses the Maintenance Cleanup task to delete files. If you want to delete files in first-level subfolders, you must select **Include first-level subfolders**.  
  
     The following options are available on this page.  
  
     **Delete files of the following type**  
     Specify the type of files to be deleted.  
  
     **Backup files**  
     Delete database backup files.  
  
     **Maintenance Plan text reports**  
     Delete text reports of previously run maintenance plans.  
  
     **File location**  
     Specify path to files to be deleted.  
  
     **Delete specific file**  
     Delete the specific file provided in the **File name** text box.  
  
     **Search folder and delete files based on an extension**  
     Delete all files with the specified extension in the specified folder. Use this to delete multiple files at once, such as all backup files in the Tuesday folder with the .bak extension.  
  
     **Folder** box  
     Path and name of the folder containing the files to be deleted.  
  
     **File extension** box  
     Provide the file extension of the files to be deleted. To delete multiple files at one time, like all backup files with the .bak extension in the Tuesday folder, specify .bak.  
  
     **Include first-level subfolders** check box  
     Delete files with the extension specified for **File extension** from first-level subfolders under the folder specified in **Folder**.  
  
     **Delete files based on the age of the file at task run time** check box  
     Specify the minimum age of the files that you want to delete by providing a number, and unit of time in the **Delete files older than the following** box.  
  
     **Delete files older than the following**  
     Specify the minimum age of the files that you want to delete by providing a number, and unit of time (**Hour**, **Day**, **Week**, **Month**, or **Year**). Files older than the time frame specified will be deleted.  
  
#### Select Report Options  
  
1.  On the **Select Report Options** page, select options for saving or distributing a report of the maintenance plan actions. This task uses the `EXEC sp_notify_operator` statement. For more information, see [sp_notify_operator &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-notify-operator-transact-sql).When finished, click **Next**.  
  
     The following options are available on this page.  
  
     **Write a report to a text file** check box  
     Save the report in a file.  
  
     **Folder location** box  
     Specify the location of the file that will contain the report.  
  
     **E-mail report** check box  
     Send an e-mail when a task fails. To use this task you must have Database Mail enabled and correctly configured with MSDB as a Mail Host Database, and have a [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent operator with a valid e-mail address.  
  
     **Agent operator**  
     Specify the recipient of the e-mail.  
  
     **Mail profile**  
     Specify the profile that defines the sender of the e-mail.  
  
#### Complete the Wizard  
  
1.  On the **Complete the Wizard** page, verify the choices made on the previous pages, and click **Finish**.  
  
2.  On the **Maintenance Wizard Progress** page, monitor status information about the actions of the Maintenance Plan Wizard. Depending on the options that you selected in the wizard, the progress page might contain one or more actions. The top box displays the overall status of the wizard and the number of status, error, and warning messages that the wizard has received.  
  
     The following options are available on the **Maintenance Wizard Progress** page:  
  
     **Details**  
     Provides the action, status, and any messages that are returned from action taken by the wizard.  
  
     **Action**  
     Specifies the type and name of each action.  
  
     **Status**  
     Indicates whether the wizard action as a whole returned the value of **Success** or **Failure**.  
  
     **Message**  
     Provides any error or warning messages that are returned from the process.  
  
     **Report**  
     Creates a report that contains the results of the Create Partition Wizard. The options are **View Report**, **Save Report to File**, **Copy Report to Clipboard**, and **Send Report as Email**.  
  
     **View Report**  
     Opens the **View Report** dialog box, which contains a text report of the progress of the Create Partition Wizard.  
  
     **Save Report to File**  
     Opens the **Save Report As** dialog box.  
  
     **Copy Report to Clipboard**  
     Copies the results of the wizard's progress report to the Clipboard.  
  
     **Send Report as Email**  
     Copies the results of the wizard's progress report into an email message.  
  
  
