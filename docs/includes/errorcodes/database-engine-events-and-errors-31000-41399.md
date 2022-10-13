---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    31001    |    16    |    No    |    The session '%s' already exists. Use a different session name.    |
|    31002    |    16    |    No    |    This operation can be performed only by the owner of the session.    |
|    31003    |    16    |    No    |    User does not have enough permissions to tune one or more of the databases specified.    |
|    32001    |    10    |    No    |    Log shipping backup log job for %s.    |
|    32002    |    10    |    No    |    Log shipping copy job for %s:%s.    |
|    32003    |    10    |    No    |    Log shipping restore log job for %s:%s.    |
|    32004    |    10    |    No    |    Log shipping backup log job step.    |
|    32005    |    10    |    No    |    Log shipping copy job step.    |
|    32006    |    10    |    No    |    Log shipping restore log job step.    |
|    32007    |    16    |    No    |    Database %s is not ONLINE.    |
|    32008    |    16    |    No    |    Database %s is not ONLINE. The backup job will not be performed until this database is brought online.    |
|    32009    |    16    |    No    |    A log shipping primary entry already exists for database %s.    |
|    32010    |    16    |    No    |    Database %s does not exist as log shipping primary.    |
|    32011    |    16    |    No    |    Primary Database %s has active log shipping secondary database(s). Drop the secondary database(s) first.    |
|    32012    |    16    |    No    |    Secondary %s.%s already exists for primary %s.    |
|    32013    |    16    |    No    |    A log shipping entry already exists for secondary database %s.    |
|    32014    |    16    |    No    |    Database %s does not exist as log shipping secondary.    |
|    32015    |    16    |    No    |    The primary database %s cannot have SIMPLE recovery for log shipping to work properly.    |
|    32016    |    16    |    No    |    The specified agent_id %s or agent_type %d do not form a valid pair for log shipping monitoring processing.    |
|    32017    |    16    |    No    |    Log shipping is supported on Enterprise, Developer, Standard and Workgroup editions of SQL Server. This instance has %s and is not supported.    |
|    32018    |    16    |    No    |    Log shipping is not installed on this instance.    |
|    32019    |    10    |    No    |    Log shipping alert job.    |
|    32020    |    10    |    No    |    Log shipping alert job step.    |
|    32021    |    10    |    No    |    Log shipping alert job schedule.    |
|    32022    |    16    |    No    |    Cannot add a log shipping job with name %s. A job with same name already exists in the system and this job does not belong to log shipping category.    |
|    32023    |    16    |    No    |    An entry for primary server %s, primary database %s does not exist on this secondary. Register the primary first.    |
|    32024    |    16    |    No    |    An entry for primary server %s, primary database %s already exists.    |
|    32025    |    16    |    No    |    Primary Server %s, Database %s has active log shipping secondary database(s) on the secondary. Drop the secondary database(s) first.    |
|    32026    |    10    |    No    |    Log shipping Primary Server Alert.    |
|    32027    |    10    |    No    |    Log shipping Secondary Server Alert.    |
|    32028    |    16    |    No    |    Invalid value = %d for parameter \@threshold_alert was specified.    |
|    32029    |    10    |    No    |    Log shipping backup agent [%s] has verified log backup file '%s.wrk' and renamed it as '%s.trn'. This is an informational message only. No user action is required.    |
|    32030    |    10    |    No    |    Could not query monitor information for log shipping primary %s.%s from monitor server %s.    |
|    32031    |    10    |    No    |    Could not query monitor information for log shipping secondary %s.%s from monitor server %s.    |
|    32032    |    16    |    No    |    Invalid value '%d' for update period. Update period should be between 1 and 120 minutes.    |
|    32033    |    16    |    No    |    The update job for the database mirroring monitor already exists. To change the update period, use sys.sp_dbmmonitorchangemonitoring    |
|    32034    |    16    |    No    |    An internal error occurred when setting up the database mirroring monitoring job.    |
|    32035    |    16    |    No    |    An internal error occurred when modifying the database mirroring monitoring job.    |
|    32036    |    16    |    No    |    Parameter(s) out of range.    |
|    32037    |    16    |    No    |    The units for the update period for the database mirroring monitor job have been changed.    |
|    32038    |    16    |    No    |    An internal error has occurred in the database mirroring monitor.    |
|    32039    |    16    |    No    |    The database '%s' is not being mirrored. No update of the base table was done.    |
|    [32040](../../relational-databases/errors-events/mssqlserver-32040-database-engine-error.md)    |    16    |    No    |    The alert for 'oldest unsent transaction' has been raised. The current value of '%d' surpasses the threshold '%d'.    |
|    32041    |    16    |    No    |    The database mirroring monitor base tables have not been created. Please run sys.sp_dbmmonitorupdate to create them.    |
|    [32042](../../relational-databases/errors-events/mssqlserver-32042-database-engine-error.md)    |    16    |    No    |    The alert for 'unsent log' has been raised. The current value of '%d' surpasses the threshold '%d'.    |
|    [32043](../../relational-databases/errors-events/mssqlserver-32043-database-engine-error.md)    |    16    |    No    |    The alert for 'unrestored log' has been raised. The current value of '%d' surpasses the threshold '%d'.    |
|    [32044](../../relational-databases/errors-events/mssqlserver-32044-database-engine-error.md)    |    16    |    No    |    The alert for 'mirror commit overhead' has been raised. The current value of '%d' surpasses the threshold '%d'.    |
|    32045    |    16    |    No    |    '%s' must be executed in msdb.    |
|    32046    |    16    |    No    |    Only members of the sysadmin fixed server role or the 'dbm_monitor' role in msdb can perform this operation.    |
|    32047    |    15    |    No    |    Database Mirroring Monitor Job    |
|    32048    |    15    |    No    |    Database Mirroring Monitor Schedule    |
|    32049    |    16    |    No    |    The database mirroring monitoring job does not exist. Run sp_dbmmonitoraddmonitoring to setup the job.    |
|    32050    |    16    |    No    |    Alerts cannot be created on the system databases, master, msdb, model or tempdb.    |
|    32051    |    10    |    No    |    System administrator privilege is required to update the base table. The base table was not updated.    |
|    32052    |    16    |    No    |    Parameter '%s' cannot be null or empty. Specify a value for the named parameter and retry the operation.    |
|    32053    |    16    |    No    |    The server name, given by '\@\@servername', is currently null.    |
|    32054    |    16    |    No    |    There was an error establishing a link to the remote monitor server.    |
|    32055    |    16    |    No    |    There was an error configuring the remote monitor server.    |
|    33001    |    16    |    No    |    Cannot drop the option because the option is not specified on %S_MSG.    |
|    33002    |    16    |    No    |    Access to %ls %ls is blocked because the signature is not valid.    |
|    33003    |    16    |    No    |    DDL statement is not allowed.    |
|    33004    |    16    |    No    |    The password of login '%.*ls' is invalid. A new password should be set for this login without specifying the old password.    |
|    33005    |    16    |    No    |    Cannot find the certificate or asymmetric key from the file %.*ls. ErrorCode: 0x%x.    |
|    33006    |    16    |    No    |    WITH SIGNATURE option cannot be specified on database.    |
|    33007    |    16    |    No    |    Cannot encrypt symmetric key with itself.    |
|    33008    |    16    |    No    |    Cannot grant, deny, or revoke %.*ls permission on INFORMATION_SCHEMA or SYS %S_MSG.    |
|    33009    |    16    |    No    |    The database owner SID recorded in the master database differs from the database owner SID recorded in database '%.*ls'. You should correct this situation by resetting the owner of database '%.*ls' using the ALTER AUTHORIZATION statement.    |
|    33010    |    16    |    No    |    The MUST_CHANGE option cannot be specified together with the HASHED option.    |
|    33011    |    16    |    No    |    The %S_MSG private key cannot be dropped because one or more entities are encrypted by it.    |
|    33012    |    10    |    No    |    Cannot %S_MSG signature %S_MSG %S_MSG '%.*ls'. Signature already exists or cannot be added.    |
|    33013    |    16    |    No    |    A %S_MSG by %S_MSG '%.*s' does not exist.    |
|    33014    |    16    |    No    |    Cannot countersign '%.*s'. Only modules can be countersigned.    |
|    33015    |    16    |    No    |    The database principal is referenced by a %S_MSG in the database, and cannot be dropped.    |
|    33016    |    16    |    No    |    The user cannot be remapped to a login. Remapping can only be done for users that were mapped to Windows or SQL logins.    |
|    33017    |    16    |    No    |    Cannot remap a user of one type to a login of a different type. For example, a SQL user must be mapped to a SQL login; it cannot be remapped to a Windows login.    |
|    33018    |    16    |    No    |    Cannot remap user to login '%.*s', because the login is already mapped to a user in the database.    |
|    33019    |    16    |    No    |    Cannot create implicit user for the special login '%.*s'.    |
|    33020    |    16    |    No    |    A HASHED password cannot be set for a login that has CHECK_POLICY turned on.    |
|    33021    |    16    |    Yes    |    Failed to generate a user instance of SQL Server. Only local user accounts, interactive users accounts, service accounts, or batch accounts can generate a user instance. The connection will be closed.%.*ls    |
|    33022    |    16    |    No    |    Cannot obtain cryptographic provider properties. Provider error code: %d.    |
|    33023    |    16    |    No    |    The %S_MSG is too long. Maximum allowed length is %d bytes.    |
|    33024    |    16    |    No    |    Cryptographic provider %S_MSG '%ls' in dll is different from the guid recorded in system catalog for provider with id %d.    |
|    33025    |    16    |    No    |    Invalid cryptograpihic provider property: %S_MSG.    |
|    33026    |    16    |    No    |    Cryptographic provider with guid '%ls' already exists.    |
|    [33027](../../relational-databases/errors-events/mssqlserver-33027-database-engine-error.md)    |        |        |    Failed to load cryptographic provider '%.*ls' due to an invalid Authenticode signature or invalid file path. Check previous messages for other failures.    |
|    33027    |    16    |    No    |    Cannot load library '%.*ls'. See errorlog for more information.    |
|    [33028](../../relational-databases/errors-events/mssqlserver-33028-database-engine-error.md)    |    16    |    No    |    Cannot open session for %S_MSG '%.*ls'. Provider error code: %d. (%S_MSG)    |
|    33029    |    16    |    No    |    Cannot initialize cryptographic provider. Provider error code: %d. (%S_MSG)    |
|    33030    |    16    |    No    |    Cryptographic provider is not available.    |
|    33031    |    16    |    No    |    Cryptographic provider '%.*ls' is in disabled.    |
|    33032    |    16    |    Yes    |    SQL Crypto API version '%02d.%02d' implemented by provider is not supported. Supported version is '%02d.%02d'.    |
|    33033    |    16    |    No    |    Specified key type or option '%S_MSG' is not supported by the provider.    |
|    33034    |    16    |    No    |    Cannot specify algorithm for existing key.    |
|    33035    |    16    |    No    |    Cannot create key '%.*ls' in the provider. Provider error code: %d. (%S_MSG)    |
|    33036    |    10    |    No    |    Cannot drop the key with thumbprint '%.*ls' in the provider.    |
|    33037    |    16    |    No    |    Cannot export %S_MSG from the provider. Provider error code: %d. (%S_MSG)    |
|    33038    |    16    |    No    |    Operation is not supported by cryptographic provider key.    |
|    33039    |    16    |    No    |    Invalid algorithm '%.*ls'. Provider error code: %d. (%S_MSG)    |
|    33040    |    16    |    No    |    Cryptographic provider key cannot be encrypted by password or other key.    |
|    33041    |    16    |    Yes    |    Cannot create login token for existing authenticators. If dbo is a windows user make sure that its windows account information is accessible to SQL Server.    |
|    33042    |    16    |    No    |    Cannot add %S_MSG because it is already mapped to a login.    |
|    33043    |    16    |    No    |    Cannot add %S_MSG '%.*ls' because there is already %S_MSG specified for the login.    |
|    33044    |    16    |    No    |    Cannot drop %S_MSG because there is %S_MSG referencing this provider.    |
|    33045    |    16    |    No    |    Cannot drop %S_MSG because it is not mapped to this login.    |
|    33046    |    16    |    No    |    Server principal '%.*ls' has no credential associated with %S_MSG '%.*ls'.    |
|    33047    |    16    |    No    |    Fail to obtain or decrypt secret for %S_MSG '%.*ls'.    |
|    33048    |    16    |    No    |    Cannot use %S_MSG under non-primary security context.    |
|    33049    |    16    |    No    |    Key with %S_MSG '%.*ls' does not exist in the provider or access is denied. Provider error code: %d. (%S_MSG)    |
|    33050    |    16    |    No    |    Cannot create key '%.*ls' in the provider. Provider does not allow specifying key names.    |
|    33051    |    16    |    No    |    Invalid algorithm id: %d. Provider error code: %d. (%S_MSG)    |
|    33052    |    16    |    No    |    Cryptographic provider key cannot be a temporary key.    |
|    33053    |    16    |    No    |    Extensible Key Management is disabled or not supported on this edition of SQL Server. Use sp_configure 'EKM provider enabled' to enable it.    |
|    33054    |    16    |    No    |    Extensible key management is not supported in this edition of SQL Server.    |
|    33055    |    16    |    No    |    Exception happened when calling cryptographic provider '%.*ls' in the API '%.*ls'. SQL Server is terminating process %d. Exception type: %ls; Exception code: 0x%lx.    |
|    33056    |    16    |    No    |    Cannnot impersonate login '%.*ls' to access %S_MSG '%.*ls'.    |
|    33057    |    10    |    No    |    Cryptographic provider is now disabled. However users who have an open cryptographic session with the provider can still use it. Restart the server to disable the provider for all users.    |
|    33058    |    10    |    No    |    Cryptographic provider is now dropped. However users who have an open cryptographic session with the provider can still use it. Restart the server to drop the provider for all users.    |
|    33070    |    16    |    No    |    The specified maximum size limit for the audit log file is less than the minimum value allowed. The maximum size limit must be at least 2 MB.    |
|    33071    |    16    |    No    |    This command requires %S_MSG to be disabled. Disable the %S_MSG and rerun this command.    |
|    33072    |    16    |    No    |    The audit log file path is invalid.    |
|    33073    |    16    |    No    |    Cannot find %S_MSG '%.*ls' or you do not have the required permissions.    |
|    33074    |    16    |    No    |    Cannot %S_MSG a %S_MSG %S_MSG from a user database. This operation must be performed in the master database.    |
|    33075    |    16    |    No    |    Auditing is not available in this edition of SQL Server. For more information about feature support in the editions of SQL Server, see SQL Server Books Online.    |
|    33076    |    16    |    No    |    The specified maximum size limit is greater than the maximum value allowed. The maximum size limit must be less than 16777215 TB.    |
|    33077    |    16    |    No    |    RESERVE_DISK_SPACE cannot be specified when setting MAXSIZE = UNLIMITED. Either reduce MAXSIZE or do not specify RESERVE_DISK_SPACE.    |
|    33079    |    16    |    No    |    Cannot bind a default or rule to the CLR type '%s' because an existing sparse column uses this data type. Modify the data type of the sparse column or remove the sparse designation of the column.    |
|    33080    |    10    |    No    |    Cryptographic provider library '%.*ls' loaded into memory. This is an informational message only. No user action is required.    |
|    [33081](../../relational-databases/errors-events/mssqlserver-33081-database-engine-error.md)    |    10    |    No    |    Failed to load cryptographic provider '%.*ls' due to an invalid Authenticode signature or invalid file path. Check previous messages for other failures.    |
|    33082    |    16    |    No    |    Cannot find Cryptographic provider library with guid '%ls'.    |
|    33083    |    16    |    No    |    Cannot create %S_MSG for %S_MSG '%ls' because it is not supported by the extensible key management provider '%ls'.    |
|    33084    |    16    |    No    |    The OPEN SYMMETRIC KEY statement cannot reference a symmetric key created from an Extensible Key Management (EKM) provider. Symmetric keys created from an EKM provider are opened automatically for principals who can successfully authenticate with the cryptographic provider.    |
|    [33085](../../relational-databases/errors-events/mssqlserver-33085-database-engine-error.md)    |    10    |    No    |    One or more methods cannot be found in cryptographic provider library '%.*ls'.    |
|    33086    |    10    |    No    |    SQL Server Audit failed to record %ls action.    |
|    33087    |    16    |    No    |    %S_MSG property of the key returned by EKM provider doesn't match the expected value    |
|    33088    |    16    |    No    |    The algorithm: %.*ls is not supported for EKM operations by SQL Server    |
|    33089    |    16    |    No    |    Key validation failed since an attempt to get algorithm info for that key failed. Provider error code: %d. (%S_MSG)    |
|    33090    |    10    |    No    |    Attempting to load library '%.*ls' into memory. This is an informational message only. No user action is required.    |
|    33091    |    10    |    No    |    Warning: The certificate used for encrypting the database encryption key has not been backed up. You should immediately back up the certificate and the private key associated with the certificate. If the certificate ever becomes unavailable or if you must restore or attach the database on another server, you must have backups of both the certificate and the private key or you will not be able to open the database.    |
|    33101    |    16    |    No    |    Cannot use %S_MSG '%.*ls', because its private key is not present or it is not protected by the database master key. SQL Server requires the ability to automatically access the private key of the %S_MSG used for this operation.    |
|    33102    |    16    |    No    |    Cannot encrypt a system database. Database encryption operations cannot be performed for 'master', 'model', 'tempdb', 'msdb', or 'resource' databases.    |
|    33103    |    16    |    No    |    A database encryption key already exists for this database.    |
|    33104    |    16    |    No    |    A database encryption key does not exist for this database.    |
|    33105    |    16    |    No    |    Cannot drop the database encryption key because it is currently in use. Database encryption needs to be turned off to be able to drop the database encryption key.    |
|    33106    |    16    |    No    |    Cannot change database encryption state because no database encryption key is set.    |
|    33107    |    16    |    No    |    Cannot enable database encryption because it is already enabled.    |
|    33108    |    16    |    No    |    Cannot disable database encryption because it is already disabled.    |
|    33109    |    16    |    No    |    Cannot disable database encryption while an encryption, decryption, or key change scan is in progress.    |
|    33110    |    16    |    No    |    Cannot change database encryption key while an encryption, decryption, or key change scan is in progress.    |
|    33111    |    16    |    No    |    Cannot find server %S_MSG with thumbprint '%.*ls'.    |
|    33112    |    10    |    No    |    Beginning database encryption scan for database '%.*ls'.    |
|    33113    |    10    |    No    |    Database encryption scan for database '%.*ls' is complete.    |
|    33114    |    10    |    No    |    Database encryption scan for database '%.*ls' was aborted. Reissue ALTER DB to resume the scan.    |
|    33115    |    16    |    No    |    CREATE/ALTER/DROP DATABASE ENCRYPTION KEY failed because a lock could not be placed on the database. Try again later.    |
|    33116    |    16    |    No    |    CREATE/ALTER/DROP DATABASE ENCRYPTION KEY failed because a lock could not be placed on database '%.*ls'. Try again later.    |
|    33117    |    16    |    No    |    Transparent Data Encryption is not available in the edition of this SQL Server instance. See books online for more details on feature support in different SQL Server editions.    |
|    33118    |    16    |    No    |    Cannot enable or modify database encryption on a database that is read-only, has read-only files or is not recovered.    |
|    33119    |    16    |    No    |    Cannot modify filegroup read-only/read-write state while an encryption transition is in progress.    |
|    33120    |    16    |    No    |    In order to encrypt the database encryption key with an %S_MSG, please use an %S_MSG that resides on an extensible key management provider.    |
|    33121    |    16    |    No    |    The %S_MSG '%ls' does not have a login associated with it. Create a login and credential for this key to automatically access the extensible key management provider '%ls'.    |
|    33122    |    16    |    No    |    This command requires a database encryption scan on database '%.*ls'. However, the database has changes from previous encryption scans that are pending log backup. Take a log backup and retry the command.    |
|    33123    |    16    |    No    |    Cannot drop or alter the database encryption key since it is currently in use on a mirror. Retry the command after all the previous reencryption scans have propagated to the mirror or after mirroring is disabled.    |
|    33124    |    10    |    No    |    Database encryption scan for database '%.*ls' cannot complete since one or more files are offline. Bring the files online to run the scan to completion.    |
|    [33128](../../relational-databases/errors-events/mssqlserver-33128-database-engine-error.md)    |        |        |    Encryption failed. Key uses deprecated algorithm '%.*ls' which is no longer supported.    |
|    [33129](../../relational-databases/errors-events/mssqlserver-33129-database-engine-error.md)    |        |        |    Cannot use ALTER_LOGIN with the DISABLE argument to deny access to a Windows group.    |
|    33201    |    17    |    No    |    An error occurred in reading from the audit file or file-pattern: '%s'. The SQL service account may not have Read permission on the files, or the pattern may be returning one or more corrupt files.    |
|    33202    |    17    |    No    |    SQL Server Audit could not write to file '%s'.    |
|    33203    |    17    |    No    |    SQL Server Audit could not write to the event log.    |
|    33204    |    17    |    No    |    SQL Server Audit could not write to the security log.    |
|    33205    |    10    |    No    |    Audit event: %s.    |
|    33206    |    17    |    No    |    SQL Server Audit failed to create the audit file '%s'. Make sure that the disk is not full and that the SQL service account has the required permissions to create and write to the file.    |
|    33207    |    17    |    No    |    SQL Server Audit failed to access the event log. Make sure that the SQL service account has the required permissions to the access the event log.    |
|    33208    |    17    |    No    |    SQL Server Audit failed to access the security log. Make sure that the SQL service account has the required permissions to access the security log.    |
|    33209    |    10    |    No    |    Audit '%.*ls' has been changed to ON_FAILURE=CONTINUE because the server was started by using the -m flag. because the server was started with the -m flag.    |
|    33210    |    10    |    No    |    SQL Server Audit failed to start, and the server is being shut down. To troubleshoot this issue, use the -m flag (Single User Mode) to bypass Audit-generated shutdowns when the server is starting.    |
|    33211    |    15    |    No    |    A list of subentities, such as columns, cannot be specified for entity-level audits.    |
|    33212    |    15    |    No    |    There is an invalid column list after the object name in the AUDIT SPECIFICATION statement.    |
|    33213    |    16    |    No    |    All actions in an audit specification statement must be at the same scope.    |
|    33214    |    17    |    No    |    The operation cannot be performed because SQL Server Audit has not been started.    |
|    33215    |    10    |    No    |    One or more audits failed to start. Refer to previous errors in the error log to identify the cause, and correct the problems associated with each error.    |
|    33216    |    10    |    No    |    SQL Server was started using the -f flag. SQL Server Audit is disabled. This is an informational message. No user action is required.    |
|    33217    |    10    |    No    |    SQL Server Audit is starting the audits. This is an informational message. No user action is required.    |
|    33218    |    10    |    No    |    SQL Server Audit has started the audits. This is an informational message. No user action is required.    |
|    33219    |    10    |    No    |    The server was stopped because SQL Server Audit '%.*ls' is configured to shut down on failure. To troubleshoot this issue, use the -m flag (Single User Mode) to bypass Audit-generated shutdowns when the server is starting.    |
|    33220    |    16    |    No    |    Audit actions at the server scope can only be granted when the current database is master.    |
|    33221    |    16    |    No    |    You can only create audit actions on objects in the current database.    |
|    33222    |    10    |    No    |    Audit '%.*ls' failed to %.*ls. For more information, see the SQL Server error log. You can also query sys.dm_os_ring_buffers where ring_buffer_type = 'RING_BUFFER_XE_LOG'.    |
|    33223    |    16    |    No    |    ALTER SERVER AUDIT requires the STATE option to be specified without using any other options.    |
|    33224    |    16    |    No    |    The specified pattern did not return any files or does not represent a valid file share. Verify the pattern parameter and rerun the command.    |
|    33225    |    16    |    No    |    The specified values for initial_file_name and audit_record_offset do not represent a valid location within the audit file set. Verify the file name and offset location, and then rerun the command.    |
|    33226    |    10    |    No    |    The fn_get_audit_file function is skipping records from '%.*ls' at offset %I64d.    |
|    33227    |    16    |    No    |    The specified value for QUEUE_DELAY is not valid. Specify either 0 or 1000 and higher.    |
|    33228    |    16    |    No    |    You cannot configure SQL Server Audit to shutdown the server because you do not have the permission to shut down the server. Contact your system administrator.    |
|    33229    |    16    |    No    |    Changes to an audit specification must be done while the audit specification is disabled.    |
|    33230    |    16    |    No    |    An audit specification for audit '%.*ls' already exists.    |
|    33231    |    16    |    No    |    You can only specify securable classes DATABASE, SCHEMA, or OBJECT in AUDIT SPECIFICATION statements.    |
|    33301    |    16    |    No    |    The %ls that is specified for conversation priority '%.*ls' is not valid. The value must be between 1 and %d characters long.    |
|    33302    |    16    |    No    |    The %ls that is specified for conversation priority '%.*ls' is not valid. The value must be between 1 and 10.    |
|    33303    |    16    |    No    |    A conversation priority already exists in the database with either the name '%.*ls' or the properties %ls='%ls', %ls='%ls', and %ls='%.*ls'. Either use a unique name or a unique set of properties.    |
|    34001    |    16    |    No    |    Dialog with queue 'syspolicy_event_queue' has encountered an error: %s.    |
|    34002    |    16    |    No    |    Dialog with queue 'syspolicy_event_queue' has ended.    |
|    34003    |    16    |    No    |    Error number %d was encountered while processing an event. The error message is: %s.    |
|    34004    |    16    |    No    |    Execution mode %d is not a valid execution mode.    |
|    34010    |    16    |    No    |    %s '%s' already exists in the database.    |
|    34011    |    16    |    No    |    Specified value for property %s cannot be used with execution mode %d.    |
|    34012    |    16    |    No    |    Cannot delete %s referenced by a %s.    |
|    34013    |    16    |    No    |    %s '%s' is referenced by a '%s'. Cannot add another reference.    |
|    34014    |    16    |    No    |    Facet does not exist.    |
|    34015    |    16    |    No    |    Policy group %s does not exist.    |
|    34016    |    16    |    No    |    Invalid target filer: %s. Only filters that restrict the first level below the Server node are allowed.    |
|    34017    |    16    |    No    |    Automated policies cannot reference conditions that contain script.    |
|    34018    |    16    |    No    |    Target type "%s" is not a valid target type.    |
|    34019    |    16    |    No    |    Object "%s" is invalid.    |
|    34020    |    16    |    No    |    Configuration option "%s" is unknown.    |
|    34021    |    16    |    No    |    Invalid value type for configuration option "%s". Expecting "%s".    |
|    34022    |    16    |    No    |    Policy automation is turned off.    |
|    34050    |    16    |    No    |    %ls    |
|    34051    |    16    |    No    |    %ls    |
|    34052    |    16    |    No    |    %ls    |
|    34053    |    16    |    No    |    %ls    |
|    34054    |    16    |    No    |    Policy Management cannot be enabled on this edition of SQL Server.    |
|    34101    |    20    |    Yes    |    An error was encountered during object serialization operation. Examine the state to find out more details about this error.    |
|    34102    |    16    |    Yes    |    An object in serialized stream has version %u but maximum supported version of this class is %u.    |
|    34103    |    16    |    Yes    |    Error in formatter during serialize/deserialize. Required to process %d elements but processed only %d elements.    |
|    34104    |    16    |    No    |    An error was encountered during object serialization operation. The object that failed serialization is %hs.    |
|    35001    |    16    |    No    |    Parent Server Group does not exist.    |
|    35002    |    16    |    No    |    Server type and parent Server Group type are not the same    |
|    35003    |    16    |    No    |    Cannot move node to one of its children    |
|    35004    |    16    |    No    |    Could not find server group    |
|    35005    |    16    |    No    |    An invalid value NULL was passed in for \@server_group_id.    |
|    35006    |    16    |    No    |    An invalid value NULL was passed in for \@server_id.    |
|    35007    |    16    |    No    |    Could not find shared registered server.    |
|    35008    |    16    |    No    |    Cannot delete system shared server groups.    |
|    35009    |    16    |    No    |    An invalid value NULL was passed in for \@server_type.    |
|    35010    |    16    |    No    |    An invalid value %d was passed in for parameter \@server_type.    |
|    35011    |    16    |    No    |    The \@server_name parameter cannot be a relative name.    |
|    35012    |    16    |    No    |    You cannot add a shared registered server with the same name as the Configuration Server.    |
|    [35250](../../relational-databases/errors-events/mssqlserver-35250-database-engine-error.md)    |    16    |    No    |    The connection to the primary replica is not active. The command cannot be processed.    |
|    [41030](../../relational-databases/errors-events/mssqlserver-41030-database-engine-error.md)    |        |        |    Failed to open the Windows Server Failover Clustering registry subkey '%.*ls' (Error code %d).  The parent key is the cluster root key.  The WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. If the corresponding availability group has been dropped, this error is expected. For information about this error code, see "System Error Codes" in the Windows Development documentation.    |
|    [41301](../../relational-databases/errors-events/mssqlserver-41301-database-engine-error.md)    |        |        |    A previous transaction that the current transaction took a dependency on has aborted, and the current transaction can no longer commit.   |
|    [41302](../../relational-databases/errors-events/mssqlserver-41302-database-engine-error.md)    |        |        |    The current transaction attempted to update a record that has been updated since this transaction started. The transaction was aborted.    |
|    [41305](../../relational-databases/errors-events/mssqlserver-41305-database-engine-error.md)    |        |        |    The current transaction failed to commit due to a repeatable read validation failure.    |
|    [41307](../../relational-databases/errors-events/mssqlserver-41307-database-engine-error.md)    |        |        |    The row size limit of *number* bytes for memory optimized tables has been exceeded. Please simplify the table definition.    |
|    [41325](../../relational-databases/errors-events/mssqlserver-41325-database-engine-error.md)    |        |        |    The current transaction failed to commit due to a serializable validation failure.    |
|    [41332](../../relational-databases/errors-events/mssqlserver-41332-database-engine-error.md)    |        |        |    Memory optimized tables and natively compiled stored procedures cannot be accessed or created when the session TRANSACTION ISOLATION LEVEL is set to SNAPSHOT.    |
|    [41333](../../relational-databases/errors-events/mssqlserver-41333-database-engine-error.md)    |        |        |    The following transactions must access memory optimized tables and natively compiled stored procedures under snapshot isolation: RepeatableRead transactions, Serializable transactions, and transactions that access tables that are not memory optimized in RepeatableRead or Serializable isolation.    |
|    [41342](../../relational-databases/errors-events/mssqlserver-41342-database-engine-error.md)    |        |        |    The model of the processor on the system does not support creating *construct*. This error typically occurs with older processors.|
|    [41349](../../relational-databases/errors-events/mssqlserver-41349-database-engine-error.md)    |        |        |    Warning: Encryption was enabled for a database that contains one or more memory optimized tables with durability SCHEMA_AND_DATA. The data in these memory optimized tables will not be encrypted.    |
|    [41350](../../relational-databases/errors-events/mssqlserver-41350-database-engine-error.md)    |        |        |    Warning: A memory optimized table with durability SCHEMA_AND_DATA was created in a database that is enabled for encryption. The data in the memory optimized table will not be encrypted.    |
|    [41359](../../relational-databases/errors-events/mssqlserver-41359-database-engine-error.md)    |        |        |    A query that accesses memory optimized tables using the READ COMMITTED isolation level, cannot access disk based tables when the database option READ_COMMITTED_SNAPSHOT is set to ON. Provide a supported isolation level for the memory optimized table using a table hint, such as WITH (SNAPSHOT).    |
|    [41365](../../relational-databases/errors-events/mssqlserver-41365-database-engine-error.md)    |        |        |    Merge request for transaction range '%ld, %ld' on database %.*ls was not scheduled. The checkpoint files representing the range are either not available for merge or part of an ongoing merge.    |
|    [41368](../../relational-databases/errors-events/mssqlserver-41368-database-engine-error.md)    |        |        |    Accessing memory optimized tables using the READ COMMITTED isolation level is supported only for autocommit transactions. It is not supported for explicit or implicit transactions. Provide a supported isolation level for the memory optimized table using a table hint, such as WITH (SNAPSHOT).    |
|    [41396](../../relational-databases/errors-events/mssqlserver-41396-database-engine-error.md)    |        |        |    The sort operation exceeded the buffer limit. The stored procedure execution was aborted. Consult SQL Server Books Online for more information.    |
|    [41399](../../relational-databases/errors-events/mssqlserver-41399-database-engine-error.md)    |        |        |    The sort operation is too complex. Consult SQL Server Books Online for more information.    |
