---
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/11/2024
ms.topic: include
---
> [!NOTE]
> This article contains the term *whitelist*, a term Microsoft considers insensitive in this context. The term appears in this article because it currently appears in the software. When the term is removed from the software, we will remove it from the article.

| Error | Severity | Event logged | Description |
| :--- | :--- | :--- | :--- |
| 31001 | 16 | No | The session '%s' already exists. Use a different session name. |
| 31002 | 16 | No | This operation can be performed only by the owner of the session. |
| 31003 | 16 | No | User does not have enough permissions to tune one or more of the databases specified. |
| 31004 | 16 | No | Could not create DTA directory for saving tuning option file when invoking DTA for auto indexing. |
| 31005 | 16 | No | Could not create DTA tuning option file when invoking DTA for auto indexing. |
| 31006 | 16 | No | Could not write DTA tuning option to file when invoking DTA for auto indexing. |
| 31007 | 16 | No | Could not create a DTA job when invoking DTA for auto indexing. |
| 31008 | 16 | No | Could not retrieve limit and job state information from DTA job object when invoking DTA for auto indexing. |
| 31009 | 16 | No | Could not set limits on DTA job object when invoking DTA for auto indexing. |
| 31010 | 16 | No | Could not create a DTA process when invoking DTA for auto indexing. |
| 31011 | 16 | No | Could not terminate DTA process after it fails to be assigned to DTA job object when invoking DTA for auto indexing. |
| 31012 | 16 | No | Could not resume DTA process to start tuning when invoking DTA for auto indexing. |
| 31013 | 16 | No | Invalid tuning option specified for auto indexing. |
| 31014 | 16 | No | Could not terminate DTA job when cancelling DTA tuning for auto indexing. |
| 31015 | 16 | No | Could not delete DTA tuning options file when cancelling DTA tuning for auto indexing. |
| 31016 | 16 | No | Could not cleanup hypothetical indexes and statistics when cancelling DTA tuning for auto indexing. |
| 31017 | 16 | No | DTA tuning for auto indexing is only supported on Azure DB. |
| 31018 | 16 | No | Could not get winfab temp directory when performing DTA tuning related task. |
| 31019 | 16 | No | Could not create GUID when performing DTA tuning related task. |
| 31020 | 16 | No | String operation failed. |
| 31021 | 16 | No | Could not get winfab log directory when performing DTA tuning related task. |
| 31022 | 16 | No | Could not get winfab data package directory when performing DTA tuning related task. |
| 31201 | 16 | No | Property-scoped full-text queries cannot be specified on the specified table because its full-text index is not configured for property searching. To support property-scoped searches, the full-text index must be associated with a search property list and repopulated. The Transact-SQL syntax for this is: ALTER FULLTEXT INDEX ON \<table_name\> SET SEARCH PROPERTY LIST \<property_list_name\>;. |
| 31202 | 10 | No | Reconfiguring the search property list of the full-text index has truncated the existing data in the index. Until the full-text index has been fully repopulated, full-text queries will return partial results. The ALTER FULLTEXT INDEX \<table_name\> SET SEARCH PROPERTY LIST ...; statement automatically issues a full population, but if the ALTER FULLTEXT INDEX statement specified the WITH NO POPULATION clause, you must run a full population on the full-text index using ALTER FULLTEXT INDEX ON \<table_name\> START FULL POPULATION;. This is a warning. |
| 31203 | 10 | No | Warning Master Merge operation was not done for dbid %d, objid %d, so querying index will be slow. Please run alter fulltext catalog reorganize. |
| 31204 | 16 | No | Operation %ls is not supported on the %ls platform. |
| 31205 | 16 | No | The import population for database %ls (id: %d), catalog id: %d is being cancelled because of a fatal error ('%ls'). Fix the errors that are logged in the full-text crawl log. Then resume the import either by detaching the database and re-attaching it, or by taking the database offline and bringing it back online. If the error is not recoverable, rebuild the full-text catalog. |
| 31206 | 16 | No | Failed to schedule asynchronous resume fulltext crawl task for database id %d with error %d. |
| 31601 | 16 | No | The size of the provided %ls is %u bytes, which exceeds the maximum allowed size of %u bytes. |
| 31602 | 10 | No | Received param '%.\*ls' successfully. |
| 31603 | 10 | No | Received param '%d' successfully. |
| 31604 | 16 | No | The value '%d' is not within range for the '%.\*ls' parameter. |
| 31605 | 16 | No | The specified method '%.\*ls' is not supported. |
| 31606 | 16 | No | Serializing payload value failed |
| 31607 | 16 | No | Outbound connection not allowed for the specified external endpoint. |
| 31608 | 16 | No | An error occurred, failed to communicate with the external rest endpoint. HRESULT: 0x%x. |
| 31609 | 16 | No | An error occurred, failed to parse url. HRESULT: 0x%x. |
| 31610 | 16 | No | Accessing the external endpoint is only allowed via HTTPS. |
| 31611 | 16 | No | The response's headers size exceeds the maximum allowed size of %u bytes. |
| 31612 | 16 | No | Connections to the domain %ls are not allowed. |
| 31613 | 16 | No | The length of the provided %ls after percent-encoding exceeds the maximum allowed length of %u bytes. |
| 31614 | 16 | No | Only alphanumeric characters, hyphens, underscores, and dots are allowed in the hostname or in the fully qualified domain name. |
| 31615 | 16 | No | The %ls cannot contain nested structures. Please check the formatting of the JSON and try again with a flat structure. |
| 31616 | 10 | No | Warning: header '%ls' was overwritten by a system-defined value. |
| 31617 | 16 | No | The %ls contains invalid characters. Only printable US ASCII characters are allowed. |
| 31618 | 16 | No | The %ls contains an empty key. |
| 31619 | 16 | No | The %ls could not be parsed. jsonError = %d. |
| 31620 | 16 | No | The %ls is not a json object datatype. |
| 31621 | 16 | No | One of the %ls values could not be parsed. |
| 31622 | 16 | No | An unexpected error occurred during validating that the connection is allowed to the hostname. See 'https://aka.ms/sqldb_httpinvoke_errordetails' for assistance. |
| 31624 | 16 | No | Connection to the external endpoint IP is not allowed. URL contains a hostname that is resolved to a blocked IP. See 'https://aka.ms/sqldb_httpinvoke_errordetails' for assistance. |
| 31625 | 16 | No | DNS resolution of the hostname has failed with windows sockets error %d. |
| 31627 | 16 | No | An out of memory exception has occurred during external endpoint invocation. |
| 31628 | 16 | No | The database scoped credential '%.\*ls' has an invalid identity '%.\*ls' for use with an external rest endpoint. |
| 31629 | 16 | No | The external rest endpoint execution exceeded the timeout value. |
| 31630 | 16 | No | The database scoped credential '%.\*ls' cannot be used to invoke an external rest endpoint. |
| 31631 | 16 | No | An error has occurred while waiting for the external rest endpoint result: %d. |
| 31632 | 16 | No | The request's headers size exceeds the maximum allowed size of %u bytes. |
| 31633 | 16 | No | The length of the provided %ls exceeds the maximum allowed length of %u bytes. |
| 31634 | 16 | No | The %ls must contain a '%ls' for use with managed identity. |
| 31635 | 16 | No | The %ls's '%ls' value must be a %ls for use with managed identity. |
| 31636 | 16 | No | Error retrieving the managed identity access token for the resource id '%ls' |
| 32001 | 10 | No | Log shipping backup log job for %s. |
| 32002 | 10 | No | Log shipping copy job for %s:%s. |
| 32003 | 10 | No | Log shipping restore log job for %s:%s. |
| 32004 | 10 | No | Log shipping backup log job step. |
| 32005 | 10 | No | Log shipping copy job step. |
| 32006 | 10 | No | Log shipping restore log job step. |
| 32007 | 16 | No | Database %s is not ONLINE. |
| 32008 | 10 | No | Database %s is not ONLINE. The backup job will not be performed until this database is brought online. |
| 32009 | 16 | No | A log shipping primary entry already exists for database %s. |
| 32010 | 16 | No | Database %s does not exist as log shipping primary. |
| 32011 | 16 | No | Primary Database %s has active log shipping secondary database(s). Drop the secondary database(s) first. |
| 32012 | 16 | No | Secondary %s.%s already exists for primary %s. |
| 32013 | 16 | No | A log shipping entry already exists for secondary database %s. |
| 32014 | 16 | No | Database %s does not exist as log shipping secondary. |
| 32015 | 16 | No | The primary database %s cannot have SIMPLE recovery for log shipping to work properly. |
| 32016 | 16 | No | The specified agent_id %s or agent_type %d do not form a valid pair for log shipping monitoring processing. |
| 32017 | 16 | No | Log shipping is supported on Enterprise, Developer and Standard editions of SQL Server. This instance has %s and is not supported. |
| 32018 | 16 | No | Log shipping is not installed on this instance. |
| 32019 | 10 | No | Log shipping alert job. |
| 32020 | 10 | No | Log shipping alert job step. |
| 32021 | 10 | No | Log shipping alert job schedule. |
| 32022 | 16 | No | Cannot add a log shipping job with name %s. A job with same name already exists in the system and this job does not belong to log shipping category. |
| 32023 | 16 | No | An entry for primary server %s, primary database %s does not exist on this secondary. Register the primary first. |
| 32024 | 16 | No | An entry for primary server %s, primary database %s already exists. |
| 32025 | 16 | No | Primary Server %s, Database %s has active log shipping secondary database(s) on the secondary. Drop the secondary database(s) first. |
| 32026 | 10 | No | Log shipping Primary Server Alert. |
| 32027 | 10 | No | Log shipping Secondary Server Alert. |
| 32028 | 16 | No | Invalid value = %d for parameter @threshold_alert was specified. |
| 32029 | 10 | No | Log shipping backup agent \[%s\] has verified log backup file '%s.wrk' and renamed it as '%s.trn'. This is an informational message only. No user action is required. |
| 32030 | 10 | No | Could not query monitor information for log shipping primary %s.%s from monitor server %s. |
| 32031 | 10 | No | Could not query monitor information for log shipping secondary %s.%s from monitor server %s. |
| 32032 | 16 | No | Invalid value '%d' for update period. Update period should be between 1 and 120 minutes. |
| 32033 | 16 | No | The update job for the database mirroring monitor already exists. To change the update period, use sys.sp_dbmmonitorchangemonitoring |
| 32034 | 16 | No | An internal error occurred when setting up the database mirroring monitoring job. |
| 32035 | 16 | No | An internal error occurred when modifying the database mirroring monitoring job. |
| 32036 | 16 | No | Parameter(s) out of range. |
| 32037 | 16 | No | The units for the update period for the database mirroring monitor job have been changed. |
| 32038 | 16 | No | An internal error has occurred in the database mirroring monitor. |
| 32039 | 16 | No | The database '%s' is not being mirrored. No update of the base table was done. |
| [32040](../mssqlserver-32040-database-engine-error.md) | 16 | No | The alert for 'oldest unsent transaction' has been raised. The current value of '%d' surpasses the threshold '%d'. |
| 32041 | 16 | No | The database mirroring monitor base tables have not been created. Please run sys.sp_dbmmonitorupdate to create them. |
| [32042](../mssqlserver-32042-database-engine-error.md) | 16 | No | The alert for 'unsent log' has been raised. The current value of '%d' surpasses the threshold '%d'. |
| [32043](../mssqlserver-32043-database-engine-error.md) | 16 | No | The alert for 'unrestored log' has been raised. The current value of '%d' surpasses the threshold '%d'. |
| [32044](../mssqlserver-32044-database-engine-error.md) | 16 | No | The alert for 'mirror commit overhead' has been raised. The current value of '%d' surpasses the threshold '%d'. |
| 32045 | 16 | No | '%s' must be executed in msdb. |
| 32046 | 16 | No | Only members of the sysadmin fixed server role or the 'dbm_monitor' role in msdb can perform this operation. |
| 32047 | 15 | No | Database Mirroring Monitor Job |
| 32048 | 15 | No | Database Mirroring Monitor Schedule |
| 32049 | 16 | No | The database mirroring monitoring job does not exist. Run sp_dbmmonitoraddmonitoring to setup the job. |
| 32050 | 16 | No | Alerts cannot be created on the system databases, master, msdb, model or tempdb. |
| 32051 | 10 | No | System administrator privilege is required to update the base table. The base table was not updated. |
| 32052 | 16 | No | Parameter '%s' cannot be null or empty. Specify a value for the named parameter and retry the operation. |
| 32053 | 16 | No | The server name, given by '@@servername', is currently null. |
| 32054 | 16 | No | There was an error establishing a link to the remote monitor server. |
| 32055 | 16 | No | There was an error configuring the remote monitor server. |
| 32056 | 17 | No | The SQL Server failed to create full-text FTData directory. This might be because FulltextDefaultPath is invalid or SQL Server service account does not have permission. Full-text may fail until this issue is resolved. Restart SQL Server after the issue is fixed. |
| 32057 | 16 | No | There was an error configuring the log shipping backup server. The primary_server_with_port_override is set to '%s'. It is only valid on a contained availability group |
| 33001 | 16 | No | Cannot drop the option because the option is not specified on %S_MSG. |
| 33002 | 16 | No | Access to %ls %ls is blocked because the signature is not valid. |
| 33003 | 16 | No | DDL statement is not allowed. |
| 33004 | 16 | No | The password of login '%.\*ls' is invalid. A new password should be set for this login without specifying the old password. |
| 33005 | 16 | No | Cannot find the certificate or asymmetric key from the file %.\*ls. ErrorCode: 0x%x. |
| 33006 | 16 | No | WITH SIGNATURE option cannot be specified on database. |
| 33007 | 16 | No | Cannot encrypt symmetric key with itself. |
| 33008 | 16 | No | Cannot grant, deny, or revoke %.\*ls permission on INFORMATION_SCHEMA or SYS %S_MSG. |
| 33009 | 16 | No | The database owner SID recorded in the master database differs from the database owner SID recorded in database '%.\*ls'. You should correct this situation by resetting the owner of database '%.\*ls' using the ALTER AUTHORIZATION statement. |
| 33010 | 16 | No | The MUST_CHANGE option cannot be specified together with the HASHED option. |
| 33011 | 16 | No | The %S_MSG private key cannot be dropped because one or more entities are encrypted by it. |
| 33012 | 10 | No | Cannot %S_MSG signature %S_MSG %S_MSG '%.\*ls'. Signature already exists or cannot be added. |
| 33013 | 16 | No | A %S_MSG by %S_MSG '%.\*s' does not exist. |
| 33014 | 16 | No | Cannot countersign '%.\*s'. Only modules can be countersigned. |
| 33015 | 16 | No | The database principal is referenced by a %S_MSG in the database, and cannot be dropped. |
| 33016 | 16 | No | The user cannot be remapped to a login. Remapping can only be done for users that were mapped to Windows or SQL logins. |
| 33017 | 16 | No | Cannot remap a user of one type to a login of a different type. For example, a SQL user must be mapped to a SQL login; it cannot be remapped to a Windows login. |
| 33018 | 16 | No | Cannot remap user to login '%.\*s', because the login is already mapped to a user in the database. |
| 33019 | 16 | No | Cannot create implicit user for the special login '%.\*s'. |
| 33020 | 16 | No | A HASHED password cannot be set for a login that has CHECK_POLICY turned on. |
| 33021 | 16 | Yes | Failed to generate a user instance of SQL Server. Only local user accounts, interactive users accounts, service accounts, or batch accounts can generate a user instance. The connection will be closed.%.\*ls |
| 33022 | 16 | No | Cannot obtain cryptographic provider properties. Provider error code: %d. |
| 33023 | 16 | No | The %S_MSG is too long. Maximum allowed length is %d bytes. |
| 33024 | 16 | No | Cryptographic provider %S_MSG '%ls' in dll is different from the guid recorded in system catalog for provider with id %d. |
| 33025 | 16 | No | Invalid cryptographic provider property: %S_MSG. |
| 33026 | 16 | No | Cryptographic provider with guid '%ls' already exists. |
| [33027](../mssqlserver-33027-database-engine-error.md) | 16 | No | Cannot load library '%.\*ls'. See errorlog for more information. |
| [33028](../mssqlserver-33028-database-engine-error.md) | 16 | No | Cannot open session for %S_MSG '%.\*ls'. Provider error code: %d. (%S_MSG) |
| 33029 | 16 | No | Cannot initialize cryptographic provider. Provider error code: %d. (%S_MSG) |
| 33030 | 16 | No | Cryptographic provider is not available. |
| 33031 | 16 | No | Cryptographic provider '%.\*ls' is in disabled. |
| 33032 | 16 | Yes | SQL Crypto API version '%02d.%02d' implemented by provider is not supported. Supported version is '%02d.%02d'. |
| 33033 | 16 | No | Specified key type or option '%S_MSG' is not supported by the provider. |
| 33034 | 16 | No | Cannot specify algorithm for existing key. |
| 33035 | 16 | No | Cannot create key '%.\*ls' in the provider. Provider error code: %d. (%S_MSG) |
| 33036 | 10 | No | Cannot drop the key with thumbprint '%.\*ls' in the provider. |
| 33037 | 16 | No | Cannot export %S_MSG from the provider. Provider error code: %d. (%S_MSG) |
| 33038 | 16 | No | Operation is not supported by cryptographic provider key. |
| 33039 | 16 | No | Invalid algorithm '%.\*ls'. Provider error code: %d. (%S_MSG) |
| 33040 | 16 | No | Cryptographic provider key cannot be encrypted by password or other key. |
| 33041 | 16 | Yes | Cannot create login token for existing authenticators. If dbo is a windows user make sure that its windows account information is accessible to SQL Server. |
| 33042 | 16 | No | Cannot add %S_MSG because it is already mapped to a login. |
| 33043 | 16 | No | Cannot add %S_MSG '%.\*ls' because there is already %S_MSG specified for the login. |
| 33044 | 16 | No | Cannot drop %S_MSG because there is %S_MSG referencing this provider. |
| 33045 | 16 | No | Cannot drop %S_MSG because it is not mapped to this login. |
| 33046 | 16 | No | Server principal '%.\*ls' has no credential associated with %S_MSG '%.\*ls'. |
| 33047 | 16 | No | Fail to obtain or decrypt secret for %S_MSG '%.\*ls'. |
| 33048 | 16 | No | Cannot use %S_MSG under non-primary security context. |
| 33049 | 16 | No | Key with %S_MSG '%.\*ls' does not exist in the provider or access is denied. Provider error code: %d. (%S_MSG) |
| 33050 | 16 | No | Cannot create key '%.\*ls' in the provider. Provider does not allow specifying key names. |
| 33051 | 16 | No | Invalid algorithm id: %d. Provider error code: %d. (%S_MSG) |
| 33052 | 16 | No | Cryptographic provider key cannot be a temporary key. |
| 33053 | 16 | No | Extensible Key Management is disabled or not supported on this edition of SQL Server. Use sp_configure 'EKM provider enabled' to enable it. |
| 33054 | 16 | No | Extensible key management is not supported in this edition of SQL Server. |
| 33055 | 16 | No | Exception happened when calling cryptographic provider '%.\*ls' in the API '%.\*ls'. SQL Server is terminating process %d. Exception type: %ls; Exception code: 0x%lx. |
| 33056 | 16 | No | Cannot impersonate login '%.\*ls' to access %S_MSG '%.\*ls'. |
| 33057 | 10 | No | Cryptographic provider is now disabled. However users who have an open cryptographic session with the provider can still use it. Restart the server to disable the provider for all users. |
| 33058 | 10 | No | Cryptographic provider is now dropped. However users who have an open cryptographic session with the provider can still use it. Restart the server to drop the provider for all users. |
| 33059 | 16 | No | An error occurred while trying to flush all running audit sessions. Some events may be lost. |
| 33060 | 16 | No | The format of supplied parameter sid is invalid. The sid might be incorrect or the sid might describe the wrong type of user. |
| 33061 | 16 | No | The specified RETENTION_DAYS value is greater than the maximum value allowed. The retention days value must be less than %d days. |
| 33062 | 16 | No | Password validation failed. The password does not meet SQL Server password policy requirements because it is too short. The password must be at least %d characters. |
| 33063 | 16 | No | Password validation failed. The password does not meet SQL Server password policy requirements because it is too long. The password must be at most %d characters. |
| 33064 | 16 | No | Password validation failed. The password does not meet SQL Server password policy requirements because it is not complex enough. The password must be at least %d characters long and contain characters from three of the following four sets: Uppercase letters, Lowercase letters, Base 10 digits, and Symbols. |
| 33065 | 10 | No | An error occurred while configuring the password policy: HRESULT 0x%x, state %d. To skip configuring the password policy, use trace flag -T4634 during startup. This will cause SQL Server to run using a minimal password policy. |
| 33066 | 10 | No | An error occurred while configuring the password policy: NTSTATUS 0x%x, state %d. To skip configuring the password policy, use trace flag -T4634 during startup. This will cause SQL Server to run using a minimal password policy. |
| 33067 | 16 | No | Secret provided can not contain more than 120 characters. Please provide a valid credential. |
| 33069 | 16 | No | CREATE CERTIFICATE statement cannot mix file and binary modes. Rewrite the statement using only the FILE or the BINARY keyword. |
| 33070 | 16 | No | The specified maximum size limit for the audit log file is less than the minimum value allowed. The maximum size limit must be at least 2 MB. |
| 33071 | 16 | No | This command requires %S_MSG to be disabled. Disable the %S_MSG and rerun this command. |
| 33072 | 16 | No | The audit log file path is invalid. |
| 33073 | 16 | No | Cannot find %S_MSG '%.\*ls' or you do not have the required permissions. |
| 33074 | 16 | No | Cannot %S_MSG a %S_MSG %S_MSG from a user database. This operation must be performed in the master database. |
| 33075 | 16 | No | Granular auditing is not available in this edition of SQL Server. For more information about feature support in the editions of SQL Server, see SQL Server Books Online. |
| 33076 | 16 | No | The specified maximum size limit is greater than the maximum value allowed. The maximum size limit must be less than 16777215 TB. |
| 33077 | 16 | No | RESERVE_DISK_SPACE cannot be specified when setting MAXSIZE = UNLIMITED. Either reduce MAXSIZE or do not specify RESERVE_DISK_SPACE. |
| 33078 | 16 | No | The containment setting in the master database does not match the property of the database files. Use ALTER DATABASE to reset the containment property. |
| 33079 | 16 | No | Cannot bind a default or rule to the CLR type '%s' because an existing sparse column uses this data type. Modify the data type of the sparse column or remove the sparse designation of the column. |
| 33080 | 10 | No | Cryptographic provider library '%.\*ls' loaded into memory. This is an informational message only. No user action is required. |
| [33081](../mssqlserver-33081-database-engine-error.md) | 10 | No | Failed to verify Authenticode signature on DLL '%.\*ls'. |
| 33082 | 16 | No | Cannot find Cryptographic provider library with guid '%ls'. |
| 33083 | 16 | No | Cannot create %S_MSG for %S_MSG '%ls' because it is not supported by the extensible key management provider '%ls'. |
| 33084 | 16 | No | The OPEN SYMMETRIC KEY statement cannot reference a symmetric key created from an Extensible Key Management (EKM) provider. Symmetric keys created from an EKM provider are opened automatically for principals who can successfully authenticate with the cryptographic provider. |
| [33085](../mssqlserver-33085-database-engine-error.md) | 10 | No | One or more methods cannot be found in cryptographic provider library '%.\*ls'. |
| 33086 | 10 | No | SQL Server Audit failed to record %ls action. |
| 33087 | 16 | No | %S_MSG property of the key returned by EKM provider doesn't match the expected value |
| 33088 | 16 | No | The algorithm: %.\*ls is not supported for EKM operations by SQL Server |
| 33089 | 16 | No | Key validation failed since an attempt to get algorithm info for that key failed. Provider error code: %d. (%S_MSG) |
| 33090 | 10 | No | Attempting to load library '%.\*ls' into memory. This is an informational message only. No user action is required. |
| 33091 | 10 | No | Warning: The certificate used for encrypting the database encryption key has not been backed up. You should immediately back up the certificate and the private key associated with the certificate. If the certificate ever becomes unavailable or if you must restore or attach the database on another server, you must have backups of both the certificate and the private key or you will not be able to open the database. |
| 33092 | 10 | No | Failed to verify the Authenticode signature of '%.\*ls'. Signature verification of SQL Server DLLs will be skipped. Genuine copies of SQL Server are signed. Failure to verify the Authenticode signature might indicate that this is not an authentic release of SQL Server. Install a genuine copy of SQL Server or contact customer support. |
| 33093 | 16 | No | The Windows user or group '%s' is local or built-in. Use a Windows domain user or domain group. |
| 33094 | 10 | No | An error occurred during Service Master Key %S_MSG |
| 33095 | 10 | No | Service Master Key could not be decrypted using one of its encryptions. See sys.key_encryptions for details. |
| 33096 | 10 | No | A generic failure occurred during Service Master Key encryption or decryption. |
| 33097 | 10 | No | Processing BrickId: %d with MasterDb.PrimaryPru.FragId %d |
| 33098 | 10 | No | Initializing SMK for brickId: %d |
| 33099 | 16 | No | You cannot add server-scoped catalog views, system stored procedures, or extended stored procedures to a database audit specification in a user database. Instead add them to a database audit specification in the master database. |
| 33101 | 16 | No | Cannot use %S_MSG '%.\*ls', because its private key is not present or it is not protected by the database master key. SQL Server requires the ability to automatically access the private key of the %S_MSG used for this operation. |
| 33102 | 16 | No | Cannot encrypt a system database. Database encryption operations cannot be performed for 'master', 'model', 'tempdb', 'msdb', or 'resource' databases. |
| 33103 | 16 | No | A database encryption key already exists for this database. |
| 33104 | 16 | No | A database encryption key does not exist for this database. |
| 33105 | 16 | No | Cannot drop the database encryption key because it is currently in use. Database encryption needs to be turned off to be able to drop the database encryption key. |
| 33106 | 16 | No | Cannot change database encryption state because no database encryption key is set. |
| 33107 | 16 | No | Cannot enable database encryption because it is already enabled. |
| 33108 | 16 | No | Cannot disable database encryption because it is already disabled. |
| 33109 | 16 | No | Cannot %S_MSG database encryption while an encryption, decryption, or key change scan is in progress. |
| 33110 | 16 | No | Cannot change database encryption key while an encryption, decryption, or key change scan is in progress. |
| 33111 | 16 | No | Cannot find server %S_MSG with thumbprint '%.\*ls'. |
| 33112 | 10 | No | Beginning database encryption scan for database '%.\*ls'. |
| 33113 | 10 | No | Database encryption scan for database '%.\*ls' is complete. |
| 33114 | 10 | No | Database encryption scan for database '%.\*ls' was aborted. Reissue ALTER DB to resume the scan. |
| 33115 | 16 | No | CREATE/ALTER/DROP DATABASE ENCRYPTION KEY failed because a lock could not be placed on the database. Try again later. |
| 33116 | 16 | No | CREATE/ALTER/DROP DATABASE ENCRYPTION KEY failed because a lock could not be placed on database '%.\*ls'. Try again later. |
| 33117 | 16 | No | Transparent Data Encryption is not available in the edition of this SQL Server instance. See books online for more details on feature support in different SQL Server editions. |
| 33118 | 16 | No | Cannot enable or modify database encryption on a database that is read-only, has read-only files or is not recovered. |
| 33119 | 16 | No | Cannot modify filegroup read-only/read-write state while an encryption transition is in progress. |
| 33120 | 16 | No | In order to encrypt the database encryption key with an %S_MSG, please use an %S_MSG that resides on an extensible key management provider. |
| 33121 | 16 | No | The %S_MSG '%ls' does not have a login associated with it. Create a login and credential for this key to automatically access the extensible key management provider '%ls'. |
| 33122 | 16 | No | This command requires a database encryption scan on database '%.\*ls'. However, the database has changes from previous encryption scans that are pending log backup. Take a log backup and retry the command. |
| 33123 | 16 | No | Cannot drop or alter the database encryption key since it is currently in use on a mirror or secondary availability replica. Retry the command after all the previous reencryption scans have propagated to the mirror or secondary availability replicas or after availability relationship has been disabled. |
| 33124 | 10 | No | Database encryption scan for database '%.\*ls' cannot complete since one or more files are offline. Bring the files online to run the scan to completion. |
| 33125 | 10 | No | Can not create login token because there are too many secondary principals. The maximum number of allowed secondary principals allowed is %lu. To fix this, remove the login from a server role. |
| 33126 | 10 | No | Database encryption key is corrupted and cannot be read. |
| 33127 | 16 | No | The %S_MSG cannot be dropped because it is used by one or more databases to encrypt a Database Encryption Key. |
| [33128](../mssqlserver-33128-database-engine-error.md) | 16 | No | Encryption failed. Key uses deprecated algorithm '%.\*ls' which is no longer supported at this db compatibility level. If you still need to use this key switch to a lower db compatibility level. |
| [33129](../mssqlserver-33129-database-engine-error.md) | 16 | No | Cannot use ALTER LOGIN with the ENABLE or DISABLE argument for a Windows group. GRANT or REVOKE the CONNECT SQL permission instead. |
| 33130 | 16 | No | Principal '%ls' could not be found or this principal type is not supported. |
| 33131 | 16 | No | Principal '%ls' has a duplicate display name. Make the display name unique in Azure Active Directory and execute this statement again. |
| 33132 | 16 | No | This principal type is not supported in Azure SQL Database. |
| 33133 | 16 | No | Your subscription is not enabled for Integrated Authentication. |
| 33134 | 16 | No | Principal '%ls' could not be resolved. Error message: '%ls' |
| 33135 | 16 | No | Altering the name of a Windows principal is not supported through this form of execution. For more information about this, see SQL Server Books Online. |
| 33136 | 16 | No | The operation could not be completed at this time. Please try again later. |
| 33137 | 16 | No | The tenant '%.\*ls' cannot be found. |
| 33139 | 16 | No | This subscription does not support Integrated Authentication. |
| 33140 | 16 | No | The login '%.\*ls' could not be created because a login is already associated with certificate '%.\*ls'. |
| 33141 | 10 | No | The credential created might not be able to decrypt the database master key. Please ensure a database master key exists for this database and is encrypted by the same password used in the stored procedure. |
| 33142 | 16 | No | Updating the tenantId for Azure SQL Server was not successful. |
| 33143 | 16 | No | The account admin of the subscription does not belong to this tenant. |
| 33144 | 16 | No | Cannot change the schema of a temporary object. |
| 33145 | 16 | No | Cannot update database encryption key protector while protector change is already in progress. |
| 33146 | 10 | No | The database '%.\*ls' is offline. The credential was created but SQL Server is unable to determine if the credential can decrypt the database master key. |
| 33147 | 20 | Yes | Federated Authentication Feature data used in login record to open a connection is structurally or semantically invalid; the connection has been closed. Please contact the vendor of the client library.%.\*ls. |
| 33148 | 16 | No | The user name for a windows login has to be identical to the login name. |
| 33149 | 16 | Yes | Dropping a credential '%.\*ls' which could still be used by Managed Backup. |
| 33150 | 16 | Yes | Warning: Can not check whether the credential '%.\*ls' is used by managed backup, because %ls. Please check whether the credential is used by managed backup. |
| 33151 | 16 | No | The user name for a user with password cannot be identical to the name of the server admin or the dbo of the database. |
| 33152 | 16 | No | Valid values of the database compatibility level are %d or %d. |
| 33153 | 16 | No | Valid values of the database compatibility level are %d, %d, or %d. |
| 33154 | 20 | Yes | Client sent an invalid token when server was expecting federated authentication token. And it was not a disconnect event. |
| 33155 | 20 | Yes | A disconnect event was raised when server is waiting for Federated Authentication token. This could be due to client close or server timeout expired. |
| 33156 | 20 | Yes | A Federated Authentication token was sent by the client when the server is not expecting it. So server will close the connection due to TDS non-conformance. |
| 33157 | 16 | No | Crypthographic providers are not supported on database credentials. |
| 33158 | 16 | No | Database credentials are not supported in master database. |
| 33159 | 16 | No | Principal '%ls' could not be created. Only connections established with Active Directory accounts can create other Active Directory users. |
| 33160 | 10 | No | Unable to verify Authenticode signatures due to error code %d. Signature verification of SQL Server DLLs will be skipped. Genuine copies of SQL Server are signed. Failure to verify the Authenticode signature might indicate that this is not an authentic release of SQL Server. Install a genuine copy of SQL Server or contact customer support. |
| 33161 | 15 | No | Database master keys without password are not supported in this version of SQL Server. |
| 33162 | 16 | No | TYPE and SID options have to be used along with each other, in this version of SQL Server. |
| 33163 | 16 | No | The value specified for TYPE option is not supported in this version of SQL Server. Allowed values are E (EXTERNAL_USER) and X (EXTERNAL_GROUP) |
| 33164 | 16 | No | Cannot drop the credential '%.\*ls' because it is used by an external data source. |
| 33165 | 16 | No | Cannot drop the external %ls '%.\*ls' because it is used by an external %ls. |
| 33166 | 16 | No | dbManager permission check failed. |
| 33167 | 16 | No | This command is not supported for Azure AD users. Execute this command as a SQL Authenticated user. |
| 33168 | 16 | No | TYPE option cannot be used along with FOR/FROM LOGIN, CERTIFICATE, ASYMMETRIC KEY, EXTERNAL PROVIDER, WITHOUT LOGIN or WITH PASSWORD, in this version of SQL Server. |
| 33169 | 16 | No | User name cannot have backslash character, when using the TYPE option. |
| 33170 | 16 | No | SID option cannot be used along with FOR/FROM LOGIN, CERTIFICATE, ASYMMETRIC KEY, EXTERNAL PROVIDER, WITHOUT LOGIN or WITH PASSWORD, in this version of SQL Server. |
| 33171 | 16 | No | Only active directory users can impersonate other active directory users. |
| 33172 | 16 | No | Encrypting Secret for Database '%.\*ls' and Credential '%.\*ls' failed. |
| 33173 | 16 | No | Assigning value for output parameter @credentialSecret failed because the parameter size is less than the required size %u. Please provide a larger size. |
| 33174 | 16 | No | Cannot get credential '%.\*ls' because it is not referenced by any external data source. |
| 33175 | 16 | No | Encryption operation failed. Key used for encryption has deprecated algorithm '%.\*ls' which is no longer supported at this db compatibility level. If you still need to use this key switch to a lower db compatibility level. |
| 33176 | 16 | No | Signing operation failed. Key uses deprecated algorithm '%.\*ls' which is no longer supported at this db compatibility level. If you still need to use this key switch to a lower db compatibility level. |
| 33177 | 16 | No | Hash operation failed. Hash Function uses deprecated algorithm '%.\*ls' which is no longer supported at this db compatibility level. If you still need to use this hash algorithm switch to a lower db compatibility level. |
| 33178 | 16 | No | Encryption key length is over the currently supported maximum length of %d. |
| 33179 | 10 | No | Searching for '%.\*ls' only searches "'%.\*ls'" for SQL Server authentication logins. |
| 33180 | 10 | No | Searching for "'%.\*ls'" only searches for Azure Active Directory users. To search for a SQL Server authentication login, add the server name at the end. For example \[%.\*ls@%.\*ls\]. |
| 33181 | 16 | No | The new owner cannot be Azure Active Directory group. |
| 33182 | 16 | No | The command must be executed on the target database '%.\*ls'. |
| 33183 | 16 | No | The Azure Key Vault client encountered an error with message '%s'. |
| 33184 | 16 | No | An error occurred while obtaining information for the Azure Key Vault client with message '%s'. |
| 33185 | 16 | No | An error ocurred while attempting to copy the encrypted DEK from the DBTable. |
| 33186 | 16 | No | Cannot alter the credential '%.\*ls' because it is being used by an active audit session ('%.\*ls'). |
| 33187 | 16 | Yes | Internal enclave error. Enclave failed to decrypt a column value or parameter - invalid authentication tag. For more information, contact Customer Support Services. |
| 33189 | 16 | Yes | Internal enclave error: Enclave is out of session resources. For more information, contact Customer Support Services. |
| 33190 | 16 | Yes | Internal enclave error. Enclave attestation information requested for an unsupported enclave type. For more information, contact Customer Support Services. |
| 33191 | 16 | Yes | Enclave host encountered a runtime error: out of priority queue space. |
| 33192 | 16 | Yes | Internal enclave error. Enclave raised an exception (major = %d, minor = %d). See error log for more information. For more information, contact Customer Support Services. |
| 33194 | 16 | Yes | Internal enclave error. The enclave bcrypt method %hs failed with status 0x%x. For more information, contact Customer Support Services. |
| 33195 | 17 | No | Internal enclave error. Enclave was provided with an invalid session handle. For more information, contact Customer Support Services. |
| 33196 | 16 | Yes | VBS enclave attestation failed due to an error in Windows Management Instrumentation (WMI). API: '%ls', Return code: '0x%08x'. Check the AzureAttestService Windows service is running on your machine. For more information, contact Customer Support Services. |
| 33197 | 16 | No | The value specified for TYPE option is not supported in this version of SQL Server. Allowed values are E (EXTERNAL_USER) and X (EXTERNAL_GROUP). |
| 33198 | 16 | No | TYPE and SID options have to be used along with each other, in this version of SQL Server. |
| 33199 | 16 | No | Only Active Directory logins can impersonate other Active Directory logins. |
| 33201 | 17 | No | An error occurred in reading from the audit file or file-pattern: '%s'. The SQL service account may not have Read permission on the files, or the pattern may be returning one or more corrupt files. |
| 33202 | 17 | No | SQL Server Audit could not write to file '%s'. |
| 33203 | 17 | No | SQL Server Audit could not write to the event log. |
| 33204 | 17 | No | SQL Server Audit could not write to the security log. |
| 33205 | 10 | No | Audit event: %s. |
| 33206 | 17 | No | SQL Server Audit failed to create the audit file '%s'. Make sure that the disk is not full and that the SQL Server service account has the required permissions to create and write to the file. |
| 33207 | 17 | No | SQL Server Audit failed to access the event log. Make sure that the SQL service account has the required permissions to the access the event log. |
| 33208 | 17 | No | SQL Server Audit failed to access the security log. Make sure that the SQL service account has the required permissions to access the security log. |
| 33209 | 10 | No | Audit '%.\*ls' has been changed to ON_FAILURE=CONTINUE because the server was started by using the -m flag. because the server was started with the -m flag. |
| 33210 | 10 | No | SQL Server Audit failed to start, and the server is being shut down. To troubleshoot this issue, use the -m flag (Single User Mode) to bypass Audit-generated shutdowns when the server is starting. |
| 33211 | 15 | No | A list of subentities, such as columns, cannot be specified for entity-level audits. |
| 33212 | 15 | No | There is an invalid column list after the object name in the AUDIT SPECIFICATION statement. |
| 33213 | 16 | No | All actions in an audit specification statement must be at the same scope. |
| 33214 | 17 | No | The operation cannot be performed because SQL Server Audit has not been started. |
| 33215 | 10 | No | One or more audits failed to start. Refer to previous errors in the error log to identify the cause, and correct the problems associated with each error. |
| 33216 | 10 | No | SQL Server was started using the -f flag. SQL Server Audit is disabled. This is an informational message. No user action is required. |
| 33217 | 10 | No | SQL Server Audit is starting the audits. This is an informational message. No user action is required. |
| 33218 | 10 | No | SQL Server Audit has started the audits. This is an informational message. No user action is required. |
| 33219 | 10 | No | The server was stopped because SQL Server Audit '%.\*ls' is configured to shut down on failure. To troubleshoot this issue, use the -m flag (Single User Mode) to bypass Audit-generated shutdowns when the server is starting. |
| 33220 | 16 | No | Audit actions at the server scope can only be granted when the current database is master. |
| 33221 | 16 | No | You can only create audit actions on objects in the current database. |
| 33222 | 10 | No | Audit '%.\*ls' failed to %S_MSG . For more information, see the SQL Server error log. You can also query sys.dm_os_ring_buffers where ring_buffer_type = 'RING_BUFFER_XE_LOG'. |
| 33223 | 16 | No | ALTER SERVER AUDIT requires the STATE option to be specified without using any other options. |
| 33224 | 16 | No | The specified pattern did not return any files or does not represent a valid file share. Verify the pattern parameter and rerun the command. |
| 33225 | 16 | No | The specified values for initial_file_name and audit_record_offset do not represent a valid location within the audit file set. Verify the file name and offset location, and then rerun the command. |
| 33226 | 10 | No | The fn_get_audit_file function is skipping records from '%.\*ls' at offset %I64d. |
| 33227 | 16 | No | The specified value for QUEUE_DELAY is not valid. Specify either 0 or 1000 and higher. |
| 33228 | 16 | No | You cannot configure SQL Server Audit to shutdown the server because you do not have the permission to shut down the server. Contact your system administrator. |
| 33229 | 16 | No | Changes to an audit specification must be done while the audit specification is disabled. |
| 33230 | 16 | No | An audit specification for audit '%.\*ls' already exists. |
| 33231 | 16 | No | You can only specify securable classes DATABASE, SCHEMA, or OBJECT in AUDIT SPECIFICATION statements. |
| 33232 | 16 | No | You may not add a role to Sysadmin. |
| 33233 | 16 | No | You can only create a user with a password in a contained database. |
| 33234 | 16 | No | The parameter %s cannot be provided for users that cannot authenticate in a database. |
| 33235 | 16 | No | The parameter %s cannot be provided for users that cannot authenticate in a database. Remove the WITHOUT LOGIN or PASSWORD clause. |
| 33236 | 16 | No | The default language parameter can only be provided for users in a contained database. |
| 33237 | 16 | No | Cannot use parameter %s for Windows users or groups. |
| 33238 | 16 | No | MAX_FILES and MAX_ROLLOVER_FILES options cannot be specified toghether. |
| 33239 | 16 | No | An error occurred while auditing this operation. Fix the error in the audit and then retry this operation. |
| 33240 | 16 | No | A failure occurred during initializing of an Audit. See the errorlog for details. |
| 33241 | 16 | Yes | Failed to configure user instance on startup. Error updating the idle timeout. |
| 33242 | 16 | No | When providing a sid, the user must be a user without login or a user with password. |
| 33243 | 16 | Yes | Failed to generate a user instance of SQL Server due to a failure in setting access control list on the user instance's process. The connection will be closed.%.\*ls |
| 33244 | 17 | No | SQL Server Audit failed to create an audit file related to the audit '%s' in the directory '%s'. Make sure that the disk is not full and that the SQL Server service account has the required permissions to create and write to the file. |
| 33245 | 17 | No | SQL Server Audit failed to write to an audit file related to the audit '%s' in the directory '%s'. Make sure that the disk is not full and that the SQL Server service account has the required permissions to create and write to the file. |
| 33246 | 17 | No | SQL Server Audit failed to access the MDS file log. Make sure that the disk is not full and that the SQL Server service account has the required permissions to create and write to the file. |
| 33247 | 17 | No | SQL Server Audit could not write to the MDS local file (Error Code: %d). |
| 33248 | 16 | No | The specified value for QUEUE_DELAY is not valid for MDS log target. Specify value higher than 0. |
| 33249 | 16 | No | Cannot drop the credential '%.\*ls' because it is being used. |
| 33250 | 16 | No | Failed to allocate memory for caching credential '%.\*ls' which is used by a database file. |
| 33251 | 16 | No | A credential conflicting with '%.\*ls' already exists in credential cache in memory. Use alter step to change the secret. Drop and re-create to change the credential name. |
| 33252 | 16 | No | This CREATE AUDIT request exceeds the maximum number of audits that can be created per database. |
| 33253 | 16 | No | Failed to modify the identity field of the credential '%.\*ls' because the credential is used by an active database file. |
| 33254 | 16 | No | This operation cannot be performed in the master database. |
| 33255 | 16 | No | Audit specification '%.\*ls' can only be bound to a %S_MSG audit. |
| 33256 | 16 | No | The audit store location or the audit store URL has been configured for this database audit. |
| 33257 | 16 | No | Cannot drop an audit store URL that is not configured for this database audit. |
| 33258 | 16 | No | Invalid value for procedure "%.\*ls", parameter "%.\*ls". |
| 33259 | 15 | No | Invalid security policy name '%.\*ls'. |
| 33260 | 16 | No | The predicate was not added to the security policy '%.\*ls' because there are no available predicate IDs. Drop and recreate the security policy. |
| 33261 | 16 | No | The security policy '%.\*ls' does not contain a predicate on table '%.\*ls'. |
| 33262 | 16 | No | A %S_MSG predicate for the same operation has already been defined on table '%.\*ls' in the security policy '%.\*ls'. |
| 33263 | 16 | No | Security predicates can only be added to user tables and schema bound views. '%.\*ls' is not a user table or a schema bound view. |
| 33264 | 16 | No | The security policy '%.\*ls' cannot be enabled with a predicate on table '%.\*ls'. Table '%.\*ls' is already referenced by the enabled security policy '%.\*ls'. |
| 33265 | 16 | No | The security policy '%.\*ls' cannot have a predicate on table '%.\*ls' because this table is referenced by the indexed view '%.\*ls'. |
| 33266 | 16 | No | The index on the view '%.\*ls' cannot be created because the view is referencing table '%.\*ls' that is referenced by a security policy. |
| 33267 | 16 | No | Security predicates cannot reference memory optimized tables. Table '%.\*ls' is memory optimized. |
| 33268 | 16 | No | Cannot find the object "%.\*ls" because it does not exist or you do not have permissions. |
| 33269 | 16 | No | Security predicates are not allowed on temporary objects. Object names that begin with '#' denote temporary objects. |
| 33270 | 16 | No | Cannot find the object "%.\*ls" or this object is not an inline table-valued function. |
| 33271 | 16 | No | Cannot alter '%.\*ls' because it is not a security policy. |
| 33272 | 16 | No | Unable to load the security predicate for table "%.\*ls". |
| 33273 | 16 | No | Security predicates cannot be added on FileTables. '%.\*ls' is a FileTable. |
| 33274 | 16 | No | The version was not created for the key '%.\*ls' because there are no available version IDs. Drop and recreate the key. |
| 33275 | 16 | No | The encrypted value for this column encryption key cannot be dropped. Each column encryption key must have at least one encrypted value. |
| 33277 | 16 | No | Encryption scheme mismatch for columns/variables %.\*ls. The encryption scheme for the columns/variables is %ls and the expression near line '%d' expects it to be %ls. |
| 33278 | 16 | No | Cannot assign the same encryption scheme to two expressions at line '%d'. The encryption scheme of the first expression is %ls and the encryption scheme of the second expression is %ls. Other columns/variables with the same encryption scheme as the first expression are: %.\*ls. Other columns/variables with the same encryption scheme as the second expression are: %.\*ls. |
| 33279 | 16 | No | Cannot remove the credential '%.\*ls/%.\*ls' because it is being used. |
| 33280 | 16 | No | Cannot create or alter encrypted column '%.\*ls' because data type '%ls' is not supported for encryption. |
| 33281 | 16 | No | Column '%.\*ls.%.\*ls' has a different encryption scheme than referencing column '%.\*ls.%.\*ls' in foreign key '%.\*ls'. |
| 33282 | 16 | No | Column '%.\*ls.%.\*ls' is encrypted using a randomized encryption type and is therefore not valid for use as a key column in a constraint, index, or statistics. |
| 33283 | 16 | No | '%S_MSG' clause is unsupported for encrypted columns. |
| 33284 | 16 | No | The encrypted column '%.\*ls.%.\*ls' cannot be used as a partition key column. |
| 33285 | 16 | No | Cannot set default constraint on encrypted column '%.\*ls.%.\*ls'. Default constraints are unsupported on encrypted columns. |
| 33286 | 16 | No | Cannot encrypt column '%.\*ls', because it is of a user-defined type. |
| 33287 | 16 | No | Cannot drop column encryption key '%.\*ls' because the key is referenced by column '%.\*ls.%.\*ls'. |
| 33288 | 16 | No | The encrypted value for the column encryption key cannot be added. There can be no more than two encrypted values for each column encryption key. Drop an exisiting encrypted value before adding the new one. |
| 33289 | 16 | No | Cannot create or alter encrypted column '%.\*ls'. Character strings that do not use a \*_BIN2 collation cannot be encrypted using deterministic encryption. |
| 33290 | 16 | No | There is no column encryption key value associated with the column master key '%.\*ls'. |
| 33291 | 16 | No | There is already a column encryption key value associated with the column master key '%.\*ls'. |
| 33292 | 16 | No | The encryption algorithm '%.\*ls' is not supported. Please specify a valid encryption algorithm. |
| 33293 | 16 | No | Cannot find the %S_MSG "%.\*ls" because it does not exist or you do not have permissions. |
| 33294 | 16 | No | Some parameters or columns of the batch require to be encrypted, but the corresponding column encryption key cannot be found. Use sp_refresh_parameter_encryption to refresh the module parameters metadata. |
| 33296 | 16 | No | Cannot create table '%.\*ls' since it references a column encryption key from a different database. |
| 33297 | 16 | No | Cannot create %S_MSG. The %S_MSG cannot be empty. |
| 33298 | 16 | Yes | Internal Query Processor Error: The encryption scheme of certain parameters was incorrectly analyzed. For more information, contact Customer Support Services. |
| 33299 | 16 | No | Encryption scheme mismatch for columns/variables %.\*ls. The encryption scheme for the columns/variables is %ls and the expression near line '%d' expects it to be %ls (or weaker). |
| 33301 | 16 | No | The %ls that is specified for conversation priority '%.\*ls' is not valid. The value must be between 1 and %d characters long. |
| 33302 | 16 | No | The %ls that is specified for conversation priority '%.\*ls' is not valid. The value must be between 1 and 10. |
| 33303 | 16 | No | A conversation priority already exists in the database with either the name '%.\*ls' or the properties %ls='%ls', %ls='%ls', and %ls='%.\*ls'. Either use a unique name or a unique set of properties. |
| 33304 | 16 | No | The transmission queue row with conversation handle '%ls' and message sequence number %d references a missing multicast message body row with reference %d. |
| 33305 | 16 | No | The multicast message body row with reference %d should have reference count value of %d. |
| 33306 | 16 | No | The unreferenced message with reference %d has been deleted from the message body table. |
| 33307 | 16 | No | The message with reference %d has been updated to a reference count of %d in the message body table. |
| 33308 | 10 | No | The queue %d in database %d has activation enabled and contains unlocked messages but no RECEIVE has been executed for %u seconds. |
| 33309 | 10 | No | Cannot start cluster endpoint because the default %S_MSG endpoint configuration has not been loaded yet. |
| 33310 | 16 | No | Local cluster name can be set only once. |
| 33311 | 10 | No | The wait for connect request completion failed. |
| 33312 | 10 | No | The wait for querying proxy routes failed or was aborted. |
| 33313 | 16 | No | An out of memory condition has occurred in the Service Broker transport layer. A service broker connection is closed due to this condition. |
| 33314 | 16 | No | The supplied whitelist is invalid. |
| 33315 | 16 | No | The redirected endpointurl is Invalid |
| 33316 | 16 | No | Failed to reset encryption while performing redirection. |
| 33317 | 16 | No | The redirect response contains invalid redirect string |
| 33318 | 16 | No | The redirect request contains invalid string or redirect handler failed to handle the request |
| 33319 | 16 | No | The redirector returned lookup failure |
| 33320 | 16 | No | Tried to send redirect request but the redirect string is empty |
| 33321 | 16 | No | The other end does not support redirection |
| 33322 | 16 | No | The redirect message is corrupted |
| 33323 | 16 | No | The endpoint is closing because the connection was redirected |
| 33324 | 16 | No | Failed to parse the redirect info string |
| 33325 | 16 | No | Failed to construct new endpoint after redirection |
| 33326 | 16 | No | Forwarder disconnected during redirection |
| 33327 | 16 | No | Failed to parse the specified connection string |
| 33328 | 16 | No | Redirector lookup failed with error code: %x |
| 33329 | 16 | No | XdbHost encountered error code: %x during socket duplication |
| 33330 | 16 | No | XdbHost returned an error during socket duplication |
| 33331 | 16 | No | DBCC CLEANUPCONVERSATIONS is not allowed on this server. |
| 33332 | 16 | No | DBCC CLEANUPCONVERSATIONS cannot be executed through MARS connection. |
| 33333 | 10 | No | The connection had a send posted for over %d seconds. The connection is suspected hung and is being closed. |
| 33334 | 10 | No | Error while setting up ssl channel. Error code: (%d, %d, %d) |
| 33335 | 10 | No | The connection has been flow controlled in the %d seconds (%d times, where threshold is %d). Min required transfer rate is %d Kbps. Actual transfer rate is %d Kbps. The connection is extensively flow controlled and is being closed. |
| 33336 | 10 | No | The connection has been flow controlled in the last %d seconds for a period of %d seconds. The connection is extensively flow controlled and is being closed. |
| 33401 | 16 | No | FILESTREAM database options cannot be set on system databases such as '%.\*ls'. |
| 33402 | 16 | No | An invalid directory name '%.\*ls' was specified. Use a valid operating system directory name. |
| 33403 | 16 | No | The case-sensitive or binary collation '%.\*ls' cannot be used with the COLLATE_FILENAME option. Change the collation to a case-insensitive collation type. |
| 33404 | 16 | No | The database default collation '%.\*ls' is case sensitive and cannot be used to create a FileTable. Specify a case insensitive collation with the COLLATE_FILENAME option. |
| 33405 | 16 | No | An error occurred during the %S_MSG %S_MSG operation on a FileTable object. (HRESULT = '0x%08x'). |
| 33406 | 16 | No | An invalid filename, '%.\*ls', caused a FileTable check constraint error. Use a valid operating system filename. |
| 33407 | 16 | No | An invalid path locator caused a FileTable check constraint error. The path locator cannot indicate a root row. Correct the path locator or use the default value. |
| 33408 | 16 | No | The operation caused a FileTable check constraint error. A directory entry cannot have a data stream associated with the row. Remove the blob data or clear the is_directory flag. |
| 33409 | 16 | No | The operation caused a FileTable check constraint error. A file entry cannot have a NULL value for the data stream associated with the row. Insert file data or use 0x to insert a zero length file. |
| 33410 | 16 | No | An invalid path locator caused a FileTable check constraint error. The parent of a row's path locator must be a directory, not a file. Correct the path locator to refer to a parent that is a directory. |
| 33411 | 16 | No | The option '%.\*ls' is only valid when used on a FileTable. Remove the option from the statement. |
| 33412 | 16 | No | The option '%.\*ls' is not valid when used with the '%.\*ls' syntax. Remove the option from the statement. |
| 33413 | 16 | No | The option '%.\*ls' can only be specified once in a statement. Remove the duplicate option from the statement. |
| 33414 | 16 | No | FileTable objects require the FILESTREAM database option DIRECTORY_NAME to be non-NULL. To create a FileTable in the database '%.\*ls', set the DIRECTORY_NAME option to a non-NULL value using ALTER DATABASE. Or, to set the DIRECTORY_NAME option to NULL, in the database '%.\*ls' disable or drop the existing FileTables. |
| 33415 | 16 | No | FILESTREAM DIRECTORY_NAME '%.\*s' attempting to be set on database '%.\*s' is not unique in this SQL Server instance. Provide a unique value for the database option FILESTREAM DIRECTORY_NAME to enable non-transacted access. |
| 33416 | 10 | No | When the FILESTREAM database option NON_TRANSACTED_ACCESS is set to FULL and the READ_COMMITTED_SNAPSHOT or the ALLOW_SNAPSHOT_ISOLATION options are on, T-SQL and transactional read access to FILESTREAM data in the context of a FILETABLE is blocked. |
| 33417 | 16 | No | An invalid path locator caused a FileTable check constraint error. The path locator has a level of %d, which is deeper than the limit of %d supported by FileTable. Reduce the depth of the directory hierarchy. |
| 33418 | 16 | No | FILETABLE_DIRECTORY '%.\*s' attempting to be set on table '%.\*s' is not unique in the database '%.\*s'. Provide a unique value for the option FILETABLE_DIRECTORY to this operation. |
| 33419 | 16 | No | Function %ls is only valid on the varbinary(max) FILESTREAM column in a FileTable. |
| 33420 | 16 | No | Unable to process object '%.\*s' because it is a three-part or four-part name. Specifying the server or database is not supported in the object identifier. |
| 33421 | 16 | No | The object name '%.\*s' is not a valid FileTable object. |
| 33422 | 16 | No | The column '%.\*s' cannot be added to table '%.\*s' as it is a FileTable. Adding columns to the fixed schema of a FileTable object is not permitted. |
| 33423 | 16 | No | Invalid FileTable path name or format. |
| 33424 | 16 | No | Invalid computer host name in the FileTable path. |
| 33425 | 16 | No | Invalid share name in the FileTable path. |
| 33426 | 16 | No | INSERT, UPDATE, DELETE, or MERGE to FileTable '%.\*ls' is not allowed inside a trigger on a FileTable. |
| 33427 | 16 | No | Function %ls is not allowed on the deleted table inside a trigger. |
| 33428 | 14 | No | User does not have permission to kill non-transacted FILESTREAM handles in database ID %d. |
| 33429 | 16 | No | The non-transacted FILESTREAM handle %d does not exist. |
| 33430 | 10 | No | Killed %d non-transactional FILESTREAM handles from database ID %d. |
| 33431 | 16 | No | An invalid path locator caused a FileTable check constraint error. The path locator has a length of %d, which is longer than the limit of %d allowed for depth %d. Reduce the length of the path locator. |
| 33433 | 10 | No | Unable to perform the Filetable lost update recovery for database id %d. |
| 33434 | 16 | No | The current state of the database '%.\*s' is not compatible with the specified FILESTREAM non-transacted access level. The database may be read only, single user or set to emergency state. |
| 33435 | 16 | No | Cannot publish the FileTable '%ls' for replication. Replication is not supported for FileTable objects. |
| 33436 | 16 | No | Cannot enable Change Data Capture on the FileTable '%ls'. Change Data Capture is not supported for FileTable objects. |
| 33437 | 16 | No | Cannot publish the logbased view '%ls' for replication. Replication is not supported for logbased views that depend on FileTable objects. |
| 33438 | 16 | No | Cannot enable Change Tracking on the FileTable '%.\*ls'. Change Tracking is not supported for FileTable objects. |
| 33439 | 16 | No | Cannot use IGNORE_CONSTRAINTS hint when inserting into FileTable '%.\*ls', unless FILETABLE_NAMESPACE is disabled. |
| 33440 | 16 | No | When inserting into FileTable '%.\*ls' using BCP or BULK INSERT, either CHECK_CONSTRAINTS option needs to be on, or FILETABLE_NAMESPACE needs to be disabled on the table. |
| 33441 | 16 | No | The FileTable '%.\*s' cannot be partitioned. Partitioning is not supported on FileTable objects. |
| 33442 | 16 | No | The handle ID %d is opened against the server root share and cannot be killed. The lifetime of the handle is controlled by the client who originally opened it. |
| 33443 | 16 | No | The database '%.\*s' cannot be enabled for both FILESTREAM non-transacted access and Database Mirroring. |
| 33444 | 16 | No | The database '%.\*s' cannot be enabled for both FILESTREAM non-transacted access and HADR. |
| 33445 | 16 | No | The database '%.\*s' is a readable secondary database in an availability group and cannot be enabled for FILESTREAM non-transacted access. |
| 33446 | 16 | No | The FILESTREAM database configuration cannot be changed for database '%.\*s'. The database is either a mirror database in Database Mirroring, or is in a secondary replica of an Always On availability group. Connect to the server instance that hosts the primary database replica, and retry the operation. |
| 33447 | 16 | No | Cannot access file_stream column in FileTable '%.\*ls', because FileTable doesn't support row versioning. Either set transaction level to something other than READ COMMITTED SNAPSHOT or SNAPSHOT, or use READCOMMITTEDLOCK table hint. |
| 33448 | 16 | No | Cannot disable clustered index '%.\*ls' on FileTable '%.\*ls' because the FILETABLE_NAMESPACE is enabled. |
| 33449 | 10 | No | FILESTREAM File I/O access is enabled, but no listener for the availability group is created. A FILESTREAM PathName will be unable to refer to a virtual network name (VNN) and, instead, will need to refer to a physical Windows Server Failover Clustering (WSFC) node. This might limit the usability of FILESTEAM File I/0 access after an availability group failover. Therefore, we recommend that you create a listener for each availability group. For information about how to create an availability group listener, see SQL Server Books Online. |
| 33450 | 10 | No | FILESTREAM File I/O access is enabled. One or more availability groups ('%ls') currently do not have a listener. A FILESTREAM PathName will be unable to refer to a virtual network name (VNN) and, instead, will need to refer to a physical Windows Server Failover Clustering (WSFC) node. This might limit the usability of FILESTEAM File I/0 access after an availability group failover. Therefore, we recommend that you create a listener for each availability group. For information about how to create an availability group listener, see SQL Server Books Online. |
| 33451 | 16 | No | Cannot enable Change Feed on the FileTable '%ls'. Change Feed is not supported for FileTable objects. |
| 33501 | 16 | No | Security predicates cannot be added on tables that contain filestream data. Column '%.\*ls' on table '%.\*ls' contains filestream data. |
| 33502 | 16 | No | The column '%.\*ls' could not be added to table '%.\*ls' because the column contains filestream data and the table is referenced by one or more security policies. Filestream columns are not allowed on tables that are referenced by security policies. |
| 33503 | 16 | No | BLOCK predicates can only be added to user tables. '%.\*ls' is not a user table. |
| 33504 | 16 | No | The attempted operation failed because the target object '%.\*ls' has a block predicate that conflicts with this operation. If the operation is performed on a view, the block predicate might be enforced on the underlying table. Modify the operation to target only the rows that are allowed by the block predicate. |
| 33505 | 16 | No | Partitioned view '%.\*ls' is not updatable because table '%.\*ls' has an enabled security policy that defines block predicates for this table. |
| 33506 | 16 | No | Only natively compiled inline table-valued functions can be used for security predicates on memory optimized tables. Table '%.\*ls' is memory optimized, but function '%.\*ls' is not natively compiled. Recreate the function using WITH NATIVE_COMPILATION. |
| 33507 | 16 | No | Function '%ls' cannot be used in the definition of BLOCK security predicates. Modify the BLOCK security predicates for this table or view to not use this function. |
| 33508 | 16 | No | Column '%.\*ls' cannot be passed as a parameter for a BLOCK security predicate because the column definition contains an expression using a window function. Modify the BLOCK security predicates for this view to not use this column. |
| 33509 | 16 | No | Column '%.\*ls' cannot be passed as a parameter for an AFTER UPDATE or AFTER INSERT BLOCK security predicate for this view because it refers to a base table that is not being modified in this statement. Modify the AFTER INSERT and AFTER UPDATE BLOCK security predicates for this view to not use this column. |
| 33510 | 16 | No | BLOCK security predicates cannot reference history tables. Table '%.\*ls' is a temporal or ledger history table. |
| 33511 | 16 | No | Table '%.\*ls' is memory optimized. Only security policies with schema binding enabled may specify security predicates for memory optimized tables. Create a new security policy with schema binding enabled. |
| 33512 | 16 | No | Binding for the non-schema bound security predicate on object '%.\*ls' failed with one or more errors, indicating the schema of the predicate function has changed. Update or drop the affected security predicates. |
| 33513 | 16 | No | Binding for the non-schema bound security predicate on object '%.\*ls' failed, because the predicate function is not an inline table-valued function. Only inline table-valued functions can be used for security predicates. |
| 33514 | 16 | No | Incorrect encryption metadata was received from the client. The error occurred during the invocation of the batch and therefore the client can refresh the encryption metadata by calling sp_describe_parameter_encryption and retry. |
| 33515 | 16 | No | The parameter "%.\*ls" does not have the same encryption information as the one it was created with. Use sp_refresh_parameter_encryption to refresh the parameter encryption information for the module. |
| 33516 | 16 | No | The object '%.\*ls' is referenced by the security policy '%.\*ls'. The currently installed edition of SQL Server does not support security policies. Either drop the security policy or upgrade the instance of SQL Server to a version that supports security policies. |
| 33517 | 16 | No | The column '%.\*ls' of the object '%.\*ls' is encrypted. The currently installed edition of SQL Server does not support encrypted columns. Either remove the encryption from the column or upgrade the instance of SQL Server to a version that supports encrypted columns. |
| 33518 | 16 | No | Valid values of the database compatibility level are %d, %d, %d, or %d. |
| 33519 | 16 | No | An external data source conflicting with '%.\*ls' already exists in EDS cache in memory. Use alter step to change the location or credential. Drop and re-create to change the EDS name. |
| 33520 | 16 | No | Failed to allocate memory for caching external data source '%.\*ls' which is used by a database file. |
| 33521 | 16 | No | Cannot drop the external data source '%.\*ls' because it is being used. |
| 33522 | 16 | No | Cannot remove the external data source '%.\*ls' because it is being used. |
| 33523 | 16 | No | Cryptographic failure due to insufficient memory. |
| 33524 | 10 | No | The fn_get_audit_file function is skipping records from '%.\*ls'. You must be connected to database '%.\*ls' to access its audit logs. |
| 33525 | 10 | No | The fn_get_audit_file function is skipping records from '%.\*ls'. You must be connected to server '%.\*ls' to access its audit logs. |
| 33526 | 10 | No | The fn_get_audit_file function is skipping records from '%.\*ls', as it does not conform to the auditing blob naming convention. |
| 33527 | 16 | No | Error occurred while initializing the security functions lookup table. This might be because the installation of SQL Server is corrupted and a required file is missing. |
| 33528 | 16 | No | Valid values of the database compatibility level are %d, %d, %d, %d, or %d. |
| 33529 | 16 | No | The audit filter predicate exceeds the maximum allowed length of %d characters. |
| 33530 | 16 | No | The combined length of audit name and blob storage container name exceeds the maximum allowed length (by %d character(s)). Please use shorter audit or container name. |
| 33531 | 16 | No | The specified value for QUEUE_DELAY is not valid with asynchronous log target. Specify value higher than 0. |
| 33532 | 16 | No | Invalid value given for parameter PATH. Please specify a valid blob container path with the following format : https://\<storageName\>.blob.core.windows.net/\<containerName\> |
| 33533 | 16 | No | SHUTDOWN on failure option is not supported in this version of SQL Server. |
| 33534 | 16 | Yes | Internal enclave error. Enclave loading failed: invalid path. For more information, contact Customer Support Services. |
| 33535 | 16 | Yes | Internal enclave error: Enclave call failed for method '%s'. For more information, contact Customer Support Services. |
| 33536 | 16 | No | VBS enclave attestation failed. Attestation status: '%ls', substatus: '%ls'. For more information, see 'https://go.microsoft.com/fwlink/?linkid=2099553'. |
| 33537 | 16 | Yes | Internal enclave error. Enclave attestation error: enclave platform returned unexpected output. For more information, contact Customer Support Services. |
| 33538 | 16 | No | The credentials of blob storage container '%.\*ls' are invalid. |
| 33539 | 16 | No | The blob storage '%.\*ls' was not found. Verify your storage account name. |
| 33540 | 16 | No | Invalid audit or database names, please use valid URL characters. |
| 33541 | 16 | No | The credentials of blob storage container '%.\*ls' was not found. |
| 33542 | 16 | No | Unsupported operation near line '%d'; operations on encrypted columns with string data types require a \*_BIN2 or a \*_UTF8 collation. However, the column or the variable uses the '%.\*ls' collation. |
| 33543 | 16 | No | Cannot alter column '%.\*ls'. The statement attempts to encrypt, decrypt or re-encrypt the column in-place using a secure enclave, but the current and/or the target column encryption key for the column is not enclave-enabled. |
| 33544 | 16 | No | Cannot alter column '%.\*ls'. The statement attempts to change the encryption scheme of the column and one or more of the following column properties: collation (to a different code page), data type. Such changes cannot be combined in a single statement. Try using multiple statements. |
| 33545 | 16 | No | The statement requires a secure enclave, but the enclave is not available for the target database - see https://go.microsoft.com/fwlink/?linkid=2005337 for more details. |
| 33546 | 16 | No | The statement triggers enclave computations, but a column encryption key, needed for the computations, has not been found inside the enclave. Check that: (1) column encryption and enclave computations are enabled on connection, (2) driver is enclave-enabled. For additional reasons see: https://go.microsoft.com/fwlink/?linkid=2086681. |
| 33547 | 16 | No | Enclave comparator cache failed to initialize during enclave load. |
| 33548 | 16 | Yes | Internal enclave error: Enclave out of memory. For more information, contact Customer Support Services. |
| 33549 | 16 | Yes | Internal enclave error: OSF Serialization error. For more information, contact Customer Support Services. |
| 33550 | 16 | Yes | Internal enclave error: Invalid data format. For more information, contact Customer Support Services. |
| 33551 | 16 | Yes | Internal enclave error: Nonce checking in secure channel failed. For more information, contact Customer Support Services. |
| 33552 | 16 | Yes | Internal enclave error. Enclave shutdown due to a critical error. For more information, contact Customer Support Services. |
| 33553 | 16 | No | A failure occurred during initialization of an Audit to External Monitor target. See the errorlog for details. |
| 33554 | 16 | Yes | Encountered error 0x%08lx while waiting for encryption scan completion consensus. See the errorlog for details. |
| 33555 | 16 | Yes | Unable to find the user-specified certificate \[Cert Hash(sha1) "%hs"\] in the certificate store of the local computer. Please verify that certificate exists. |
| 33556 | 16 | Yes | Invalid character in the thumbprint \[Cert Hash(sha1) "%hs"\]. Please provide a certificate with a valid thumbprint. |
| 33557 | 16 | Yes | Invalid thumbprint length \[Cert Hash(sha1) "%hs"\]. Please provide a certificate with a valid thumbprint. |
| 33558 | 16 | No | Encryption scan can not be resumed because no encryption scan is in progress. |
| 33559 | 16 | No | Specified workload group does not exist. Retry with a valid workload group. |
| 33560 | 10 | No | TDE scan for database id \[%d\] was initiated on %S_DATE (UTC). The encryption scan will resume soon. |
| 33561 | 16 | No | Encryption scan can not be suspended because no encryption scan is in progress. |
| 33562 | 16 | No | TDE encryption scan state cannot be updated for database id \[%d\]. |
| 33563 | 10 | No | TDE scan for database id \[%d\] was aborted on %S_DATE (UTC). The encryption scan will resume soon. |
| 33564 | 10 | No | TDE scan for database id \[%d\] was suspended on %S_DATE (UTC). To resume scan, run "ALTER DATABASE \[%.\*s\] SET ENCRYPTION RESUME". |
| 33565 | 16 | Yes | Found the certificate \[Cert Hash(sha1) "%hs"\] in the %S_MSG store but the SQL Server service account does not have access to it. |
| 33566 | 16 | Yes | Found the certificate \[Cert Hash(sha1) "%hs"\] in the %S_MSG store but it does not have a private key. Please verify and use a valid certificate. |
| 33567 | 16 | No | Column '%.\*ls.%.\*ls' is encrypted using a randomized encryption type and is therefore not valid for use as a key column in a clustered index, constraint, or statistics. |
| 33568 | 16 | Yes | Internal enclave error: Structured exception within the enclave with status %d. For more information, contact Customer Support Services. |
| 33569 | 16 | No | Column '%.\*ls.%.\*ls' is encrypted with randomized encryption using a collation that is neither \*_BIN2 nor \*_UTF8, and is therefore not valid for use as a key column in an index. |
| 33570 | 10 | No | Specified workload group does not exist. Switching to default workload group for TDE encryption scan. |
| 33571 | 16 | No | Cannot create server audit with GUID '%s' in this version of SQL Server: Please specify another GUID value. |
| 33572 | 16 | No | Encrypted index recovery: %ls %d |
| 33573 | 16 | No | Column '%.\*ls.%.\*ls' is encrypted using randomized encryption with a non enclave-enabled column encryption key and is therefore not valid for use as a key column in a constraint, index, or statistics. |
| 33574 | 16 | No | Specifying a private key file or binary is not allowed when importing or exporting a certificate using the PFX format. |
| 33575 | 16 | No | WITH PRIVATE KEY must be specified when importing or exporting a certificate using the PFX format. |
| 33576 | 16 | No | Invalid certificate encoding FORMAT value provided. The supported values are 'DER' and 'PFX'. |
| 33577 | 16 | No | The specified PFX file or the binary literal cannot be imported as it contains multiple certificates. |
| 33578 | 16 | No | The specified file or the binary literal is not supported as it does not contain a private key. |
| 33579 | 16 | No | Cannot drop a value for the column encryption key '%.\*ls'; dropping the value makes the key enclave disabled, and one or more objects that are schema-bound to column '%.\*ls' depend on it. |
| 33580 | 16 | No | Operation supported by enclaves invoked on data encrypted with randomized encryption where the keys are not enclave-enabled. Please clear the proc cache. |
| 33581 | 16 | No | Cannot alter column '%.\*ls'. The statement attempts to encrypt, decrypt or re-encrypt the column in-place using a secure enclave, but this is not supported for memory-optimized tables. |
| 33582 | 16 | No | Cannot create the module. Executing the module requires keys in a secure enclave, but this is not supported for memory-optimized tables. |
| 33583 | 16 | No | Could not find Azure AD principal mapping to Windows principal '%ls'. |
| 33584 | 16 | No | Found multiple Azure AD principals mapping to Windows principal '%ls'. |
| 33585 | 16 | No | Windows principal '%ls' does not map to provided Azure AD principal '%ls', please use a different Azure AD principal. |
| 33586 | 10 | No | Warning: Index or statistic '%.\*ls.%.\*ls' has an enclave-enabled key column, however Accelerated Database Recovery is not enabled for database '%.\*ls'. Enabling it is strongly recommended to increase the database availability during recovery. |
| 33587 | 16 | No | Security predicates cannot be added to external table '%.\*ls' because it does not contain a credential. |
| 33588 | 16 | No | Invalid encryption algorithm. Always Encrypted with secure enclaves requires data to be encrypted with the AEAD_AES_256_CBC_HMAC_SHA_256 algorithm. |
| 33589 | 17 | No | Internal enclave error. Enclave was provided with an invalid expression handle. For more information, contact Customer Support Services. |
| 33590 | 10 | No | Database encryption scan for database '%.\*ls' was suspended. Reissue ALTER DB to resume the scan. |
| 33591 | 16 | No | ALGORITHM has an invalid value. The allowed values are '%ls', '%ls'. |
| 33592 | 16 | No | The underlying operating system does not support encrypting private keys using '%ls'. Consider using '%ls'. |
| 33593 | 16 | No | Specifying ALGORITHM is not allowed when exporting a certificate using the DER format. |
| 33594 | 16 | No | ALGORITHM must be specified when exporting a certificate using the PFX format. The supported algorithms are '%ls', '%ls'. |
| 33595 | 16 | No | The session context key '%s' is too long. Maximum allowed length of session context key is %d characters. |
| 33596 | 16 | No | Maximum number of session context keys used for audit specification has been exceeded. The maximum allowed number of keys is %d. |
| 33597 | 16 | No | Operator Audit option is not supported for this target type, use EXTERNAL_MONITOR target. |
| 33598 | 16 | No | The private key password is invalid, or the file was encrypted with an algorithm not supported by the underlying operating system. |
| 33599 | 16 | No | Valid values of the database compatibility level are %d, %d, %d, %d, %d, or %d. |
| 33801 | 16 | No | Cannot bulk load file split because either secret or file_properties option is missing. |
| 34001 | 16 | No | Dialog with queue 'syspolicy_event_queue' has encountered an error: %s. |
| 34002 | 16 | No | Dialog with queue 'syspolicy_event_queue' has ended. |
| 34003 | 16 | No | Error number %d was encountered while processing an event. The error message is: %s. |
| 34004 | 16 | No | Execution mode %d is not a valid execution mode. |
| 34010 | 16 | No | %s '%s' already exists in the database. |
| 34011 | 16 | No | Specified value for property %s cannot be used with execution mode %d. |
| 34012 | 16 | No | Cannot delete %s referenced by a %s. |
| 34013 | 16 | No | %s '%s' is referenced by a '%s'. Cannot add another reference. |
| 34014 | 16 | No | Facet does not exist. |
| 34015 | 16 | No | Policy group %s does not exist. |
| 34016 | 16 | No | Invalid target filer: %s. Only filters that restrict the first level below the Server node are allowed. |
| 34017 | 16 | No | Automated policies cannot reference conditions that contain script. |
| 34018 | 16 | No | Target type "%s" is not a valid target type. |
| 34019 | 16 | No | Object "%s" is invalid. |
| 34020 | 16 | No | Configuration option "%s" is unknown. |
| 34021 | 16 | No | Invalid value type for configuration option "%s". Expecting "%s". |
| 34022 | 16 | No | Policy automation is turned off. |
| 34050 | 16 | No | %ls |
| 34051 | 16 | No | %ls |
| 34052 | 16 | No | %ls |
| 34053 | 16 | No | %ls |
| 34054 | 16 | No | Policy Management cannot be enabled on this edition of SQL Server. |
| 34101 | 20 | Yes | An error was encountered during object serialization operation. Examine the state to find out more details about this error. |
| 34102 | 16 | Yes | An object in serialized stream has version %u but maximum supported version of this class is %u. |
| 34103 | 16 | Yes | Error in formatter during serialize/deserialize. Required to process %d elements but processed only %d elements. |
| 34104 | 16 | No | An error was encountered during object serialization operation. The object that failed serialization is %hs. |
| 34901 | 16 | Yes | The global lock manager encountered a severe failure. |
| 35001 | 16 | No | Parent Server Group does not exist. |
| 35002 | 16 | No | Server type and parent Server Group type are not the same |
| 35003 | 16 | No | Cannot move node to one of its children |
| 35004 | 16 | No | Could not find server group |
| 35005 | 16 | No | An invalid value NULL was passed in for @server_group_id. |
| 35006 | 16 | No | An invalid value NULL was passed in for @server_id. |
| 35007 | 16 | No | Could not find shared registered server. |
| 35008 | 16 | No | Cannot delete system shared server groups. |
| 35009 | 16 | No | An invalid value NULL was passed in for @server_type. |
| 35010 | 16 | No | An invalid value %d was passed in for parameter @server_type. |
| 35011 | 16 | No | The @server_name parameter cannot be a relative name. |
| 35012 | 16 | No | You cannot add a shared registered server with the same name as the Configuration Server. |
| 35100 | 16 | No | Error number %d in the THROW statement is outside the valid range. Specify an error number in the valid range of 50000 to 2147483647. |
| 35101 | 16 | No | One of the SET options FMTONLY or NOEXEC was changes from ON to OFF in a TRY...CATCH block. |
| 35102 | 16 | No | Error related to '%ls' where one of participants is Azure SQL Managed Instance. |
| 35103 | 16 | No | '%ls' feature where one of participants is Azure SQL Managed Instance is disabled. |
| 35104 | 16 | No | Deprecated feature '%ls' with Azure SQL Managed Instance as a participant is not supported in this version of SQL Server. |
| 35200 | 16 | No | Failed to add database '%.\*ls' to availability group '%.\*ls'. The specified availability group is created with basic functionality and supports %d database. |
| 35201 | 10 | No | A connection timeout has occurred while attempting to establish a connection to availability replica '%ls' with id \[%ls\]. Either a networking or firewall issue exists, or the endpoint address provided for the replica is not the database mirroring endpoint of the host server instance. |
| 35202 | 10 | No | A connection for availability group '%ls' from availability replica '%ls' with id \[%ls\] to '%ls' with id \[%ls\] has been successfully established. This is an informational message only. No user action is required. |
| 35203 | 16 | No | Unable to establish a connection between instance '%ls' with id \[%ls\] and '%ls' with id \[%ls\] due to a transport version mismatch. |
| 35204 | 10 | No | The connection between server instances '%ls' with id \[%ls\] and '%ls' with id \[%ls\] has been disabled because the database mirroring endpoint was either disabled or stopped. Restart the endpoint by using the ALTER ENDPOINT Transact-SQL statement with STATE = STARTED. |
| 35205 | 16 | No | Could not start the Always On Availability Groups transport manager. This failure probably occurred because a low memory condition existed when the message dispatcher started up. If so, other internal tasks might also have experienced errors. Check the SQL Server error log and the Windows error log for additional error messages. If a low memory condition exists, investigate and correct its cause. |
| 35206 | 10 | No | A connection timeout has occurred on a previously established connection to availability replica '%ls' with id \[%ls\]. Either a networking or a firewall issue exists or the availability replica has transitioned to the resolving role. |
| 35207 | 16 | No | Connection attempt on availability group id '%ls' from replica id '%ls' to replica id '%ls' failed because of error %d, severity %d, state %d. |
| 35208 | 16 | No | Availability group DDL operations are permitted only when you are using the master database. Run the 'USE master' statement, and retry your availability group DDL statement. |
| 35209 | 16 | No | The %ls operation failed for availability replica '%.\*ls', because the backup priority value is outside the valid range. The valid range is between %d and %d, inclusive. Set the backup priority to a value within this range, and retry the operation. |
| 35210 | 16 | No | Failed to modify options for availability replica '%.\*ls' in availability group '%.\*ls'. The specified availability group does not contain an availability replica with specified name. Verify that availability group name and availability replica name are correct, then retry the operation. |
| 35211 | 16 | No | The %ls operation is not allowed. The operation attempted to change the configuration of availability replica '%.\*ls' to the asynchronous-commit availability mode with automatic failover, which is an invalid configuration. Either change the failover mode to manual or the availability mode to synchronous commit, and retry the operation. |
| 35212 | 16 | No | The %ls operation is not allowed by the current availability group configuration. This operation would exceed the maximum number of %d synchronous-commit availability replicas in availability group '%.\*ls'. Change one of the existing synchronous-commit replicas to the asynchronous-commit availability mode, and retry the operation. |
| 35213 | 16 | No | The %ls operation is not allowed by the current availability group configuration. This operation would exceed the maximum number of %d automatic failover targets in availability group '%.\*ls'. Change one of the existing synchronous-commit replicas to the manual failover mode, and retry the operation. |
| 35214 | 16 | No | The %ls operation failed for availability replica '%.\*ls'. The minimum value for session timeout is %d. Retry the operation specifying a valid session-timeout value. |
| 35215 | 16 | No | The %ls operation is not allowed on availability replica '%.\*ls', because automatic failover mode is an invalid configuration on a SQL Server Failover Cluster Instance. Retry the operation by specifying manual failover mode. |
| 35216 | 16 | No | An error occurred while adding or removing a log truncation holdup to build secondary replica from primary availability database '%.\*ls'. Primary database is temporarily offline due to restart or other transient condition. Retry the operation. |
| 35217 | 16 | No | The thread pool for Always On Availability Groups was unable to start a new worker thread because there are not enough available worker threads. This may degrade Always On Availability Groups performance. Use the "max worker threads" configuration option to increase number of allowable threads. |
| 35218 | 16 | No | An error occurred while trying to set the initial Backup LSN of database '%.\*ls'. Primary database is temporarily offline due to restart or other transient condition. Retry the operation. |
| 35220 | 16 | No | Could not process the operation. Always On Availability Groups replica manager is waiting for the host computer to start a Windows Server Failover Clustering (WSFC) cluster and join it. Either the local computer is not a cluster node, or the local cluster node is not online. If the computer is a cluster node, wait for it to join the cluster. If the computer is not a cluster node, add the computer to a WSFC cluster. Then, retry the operation. |
| 35221 | 16 | No | Could not process the operation. Always On Availability Groups replica manager is disabled on this instance of SQL Server. Enable Always On Availability Groups, by using the SQL Server Configuration Manager. Then, restart the SQL Server service, and retry the currently operation. For information about how to enable and disable Always On Availability Groups, see SQL Server Books Online. |
| 35222 | 16 | No | Could not process the operation. Always On Availability Groups does not have permissions to access the Windows Server Failover Clustering (WSFC) cluster. Disable and re-enable Always On Availability Groups by using the SQL Server Configuration Manager. Then, restart the SQL Server service, and retry the currently operation. For information about how to enable and disable Always On Availability Groups, see SQL Server Books Online. |
| 35223 | 16 | No | Cannot add %d availability replica(s) to availability group '%.\*ls'. The availability group already contains %d replica(s), and the maximum number of replicas supported in an availability group is %d. |
| 35224 | 16 | No | Could not process the operation. Always On Availability Groups failed to load the required Windows Server Failover Clustering (WSFC) library. Verify that the computer is a node in a WSFC cluster. You will need to restart the SQL Server instance to reload the required library functions. |
| 35225 | 16 | No | Could not process the operation. The instance of SQL Server is running under WOW64 (Windows 32-bit on Windows 64-bit), which does not support Always On Availability Groups. Reinstall SQL Server in the native 64-bit edition, and re-enable Always On Availability Groups. Then, restart the SQL Server service, and retry the operation. For information about how to enable and disable Always On Availability Groups, see SQL Server Books Online. |
| 35226 | 16 | No | Could not process the operation. Always On Availability Groups has not started because the instance of SQL Server is not running as a service. Restart the server instance as a service, and retry the operation. |
| 35228 | 16 | No | The attempt to set the failure condition level for availability group '%.\*ls' failed. The specified level value is out of the valid range \[%u, %u\]. Reenter the command specifying a valid failure condition level value. |
| 35229 | 16 | No | The attempt to set the health check timeout value for availability group '%.\*ls' failed. The specified timeout value is less than %u milliseconds. Reenter the command specifying a valid health check timeout value. |
| 35230 | 16 | No | The specified computer name is either an empty string or is longer than %d Unicode characters. Reenter the command specifying a valid computer name. |
| 35231 | 16 | No | The specified server instance name, '%ls', is invalid. Reenter the command specifying a valid instance name. |
| 35232 | 16 | No | The specified endpoint URL '%.\*ls' is invalid. Reenter the command specifying the correct URL. For information about specifying the endpoint URL for an availability replica, see SQL Server Books Online. |
| 35233 | 16 | No | Cannot create an availability group containing %d availability replica(s). The maximum number of availability replicas in an availability group %ls is %d. Reenter your CREATE AVAILABILITY GROUP command specifying fewer availability replicas. |
| 35234 | 16 | No | Database name '%ls' was specified more than once. Reenter the command, specifying each database name only once. |
| 35235 | 16 | No | The system name '%ls' was specified more than once in the REPLICA ON clause of this command. Reenter the command, specifying a different instance of SQL Server for each replica. |
| 35236 | 15 | No | The endpoint URL was not specified for the availability replica hosted by server instance '%.\*ls'. Reenter the command, specifying the endpoint URL of this instance of SQL Server. |
| 35237 | 16 | No | None of the specified replicas for availability group %.\*ls maps to the instance of SQL Server to which you are connected. Reenter the command, specifying this server instance to host one of the replicas. This replica will be the initial primary replica. |
| 35238 | 16 | No | Database '%.\*ls' cannot be added to availability group '%.\*ls'. The database does not exist on this SQL Server instance. Verify that the database name is correct, then retry the operation. |
| 35239 | 16 | No | The ALTER DATABASE \<database-name\> SET HADR SUSPEND (or SET HADR RESUME) statement failed on database '%.\*ls' of availability group '%.\*ls''. Either the availability group does not contain the specified database, or the database has not joined the availability group, or the database has not yet started. Reenter the command after the database is online and has joined the availability group. |
| 35240 | 16 | No | Database '%.\*ls' cannot be joined to or unjoined from availability group '%.\*ls'. This operation is not supported on the primary replica of the availability group. |
| 35242 | 16 | No | Cannot complete this ALTER DATABASE \<database-name\> SET HADR operation on database '%.\*ls'. The database is not joined to an availability group. After the database has joined the availability group, retry the command. |
| 35243 | 16 | No | Failed to set resource property '%.\*ls' for availability group '%.\*ls'. The operation encountered SQL Server error %d. When the cause of the error has been resolved, retry the ALTER AVAILABILITY GROUP command later. |
| 35244 | 16 | No | Database '%.\*ls' cannot be added to availability group '%.\*ls'. The database is currently joined to another availability group. Verify that the database name is correct and that the database is not joined to an availability group, then retry the operation. |
| 35246 | 16 | No | Failed to create the availability group. A SQL Server instance name could not be validated because the dynamic link library (DLL) file '%ls' could not be located (Windows System Error %d). Verify that the specified server instance exists. If it exists, the DLL file might be missing from the server instance. |
| 35247 | 16 | No | Failed to create the availability group. A SQL Server instance name could not be validated because the dynamic link library (DLL) file '%ls' could not be loaded (Windows System Error %d). |
| 35248 | 16 | No | The %ls operation is not allowed by the current availability group configuration. The required_synchronized_secondaries_to_commit %d is greater than the %d possible secondary synchronous-commit availability replicas in availability group '%.\*ls'. Change one of the existing asynchronous-commit replicas to the synchronous-commit availability mode, and retry the operation. |
| 35249 | 16 | No | An attempt to add or join a system database, '%.\*ls', to an availability group failed. Specify only user databases for this operation. |
| [35250](../mssqlserver-35250-database-engine-error.md) | 16 | No | The connection to the primary replica is not active. The command cannot be processed. |
| 35251 | 16 | No | This command can be run only on the primary replica. Connect to the primary replica, and retry the command. |
| 35252 | 16 | No | The command can only be run on a secondary database. Connect to the correct secondary replica, and retry the command. |
| 35253 | 16 | No | Database "%.\*ls" is not in the correct state to become the primary database. The log must be restored from the previous primary replica to bring the database out of the reinitializing state. |
| 35254 | 16 | Yes | An error occurred while accessing the availability group metadata. Remove this database or replica from the availability group, and reconfigure the availability group to add the database or replica again. For more information, see the ALTER AVAILABILITY GROUP Transact-SQL statement in SQL Server Books Online. |
| 35255 | 16 | No | An attempt to start database '%.\*ls' failed because the database is already started and online. |
| 35256 | 16 | No | The session timeout value was exceeded while waiting for a response from the other availability replica in the session. That replica or the network might be down, or the command might be misconfigured. If the partner is running and visible over the network, retry the command using correctly configured partner-connection parameters. |
| 35257 | 16 | No | Always On Availability Groups Send Error (Error code 0x%X, "NOT OK") was returned when sending a message for database ID %d. If the partner is running and visible over the network, retry the command using correctly configured partner-connection parameters. |
| 35258 | 16 | No | Error in the hadron simulator. |
| 35259 | 16 | No | Database '%.\*ls' is already participating in a different availability group. |
| 35260 | 16 | No | During an attempted database recovery, an availability database manager was not found for database id %d with availability group ID %d and group database ID %ls. Recovery was terminated. The most likely cause of this error is that the availability group manager is not running, but the cause could be a metadata error. Ensure that the availability group manager and the WSFC cluster are started, and retry the recovery operation. |
| 35261 | 16 | No | Attempt to perform an Always On Availability Groups operation on a system database, '%ls', failed. System databases are not supported by Always On Availability Groups. |
| 35262 | 10 | Yes | Skipping the default startup of database '%.\*ls' because the database belongs to an availability group (Group ID: %d). The database will be started by the availability group. This is an informational message only. No user action is required. |
| 35263 | 16 | No | During the undo phase, a function call (%ls) to the primary replica returned an unexpected status (Code: %d). Check for a possible cause in the SQL Server error log for the primary replica. If an error occurred on the primary database, you might need to suspend the secondary database, fix the issue on the primary database, and then resume the database. |
| 35264 | 10 | No | Always On Availability Groups data movement for database '%.\*ls' has been suspended for the following reason: "%S_MSG" (Source ID %d; Source string: '%.\*ls'). To resume data movement on the database, you will need to resume the database manually. For information about how to resume an availability database, see SQL Server Books Online. |
| 35265 | 10 | No | Always On Availability Groups data movement for database '%.\*ls' has been resumed. This is an informational message only. No user action is required. |
| 35266 | 10 | No | Always On Availability Groups connection with %S_MSG database established for %S_MSG database '%.\*ls' on the availability replica '%.\*ls' with Replica ID: {%.8x-%.4x-%.4x-%.2x%.2x-%.2x%.2x%.2x%.2x%.2x%.2x}. This is an informational message only. No user action is required. |
| [35267](../mssqlserver-35267-database-engine-error.md) | 10 | No | Always On Availability Groups connection with %S_MSG database terminated for %S_MSG database '%.\*ls' on the availability replica '%.\*ls' with Replica ID: {%.8x-%.4x-%.4x-%.2x%.2x-%.2x%.2x%.2x%.2x%.2x%.2x}. This is an informational message only. No user action is required. |
| 35268 | 16 | Yes | Synchronization of a secondary database, '%.\*ls', was interrupted, leaving the database in an inconsistent state. The database will enter the RESTORING state. To complete recovery and bring the database online, use current log backups from the primary database to restore the log records past LSN %S_LSN. Alternatively, drop this secondary database, and prepare a new one by restoring a full database backup of the primary database followed by all subsequent log backups. |
| 35269 | 21 | Yes | Synchronization of a secondary database, '%.\*ls', was interrupted, leaving the database in an inconsistent state. The database will be marked SUSPECT. To return the database to a consistent state, restore it from a clean database backup followed by all subsequent log backups. |
| 35270 | 10 | No | Received a corrupt FileStream transport message. The '%ls' message section is invalid. |
| 35271 | 16 | No | The availability database %ls in availability group %ls failed to complete a reconfiguration. Refer to the error code for more details. If this condition persists, contact the system administrator. |
| 35272 | 16 | No | Either invalid parameters were supplied for sys.sp_availability_group_command_internal or user does not have permissions to execute this procedure. |
| 35273 | 10 | Yes | Bypassing recovery for database '%ls' because it is marked as an inaccessible availability database. The session with the primary replica was interrupted while reverting the database to the common recovery point. Either the WSFC node lacks quorum or the communications links are broken because of problems with links, endpoint configuration, or permissions (for the server account or security certificate). To gain access to the database, you need to determine what has changed in the session configuration and undo the change. |
| 35274 | 10 | Yes | Recovery for availability database '%ls' is pending until the secondary replica receives additional transaction log from the primary before it complete and come online. Ensure that the server instance that hosts the primary replica is running. |
| 35275 | 16 | Yes | A previous RESTORE WITH CONTINUE_AFTER_ERROR operation or being removed while in the SUSPECT state from an availability group left the '%.\*ls' database in a potentially damaged state. The database cannot be joined while in this state. Restore the database, and retry the join operation. |
| 35276 | 17 | Yes | Failed to allocate and schedule an Always On Availability Groups task for database '%ls'. Manual intervention may be required to resume synchronization of the database. If the problem persists, you might need to restart the local instance of SQL Server. |
| 35277 | 16 | No | Automatic failover is not supported for distributed availability group replica. |
| 35278 | 16 | No | Availability database '%.\*ls', which is in the secondary role, is being restarted to resynchronize with the current primary database. This is an informational message only. No user action is required. |
| 35279 | 16 | Yes | The attempt to join database '%.\*ls' to the availability group was rejected by the primary database with error '%d'. For more information, see the SQL Server error log for the primary replica. |
| 35280 | 16 | No | Database '%.\*ls' cannot be added to availability group '%.\*ls'. The database is already joined to the specified availability group. Verify that the database name is correct and that the database is not joined to an availability group, then retry the operation. |
| 35281 | 16 | No | Database '%.\*ls' cannot be removed from availability group '%.\*ls'. The database is not joined to the specified availability group. Verify that the database name and the availability group name are correct, then retry the operation. |
| 35282 | 16 | No | Availability replica '%.\*ls' cannot be added to availability group '%.\*ls'. The availability group already contains an availability replica with the specified name. Verify that the availability replica name and the availability group name are correct, then retry the operation. |
| 35283 | 16 | No | Availability replica '%.\*ls' cannot be removed from availability group '%.\*ls'. The availability group does not contain an availability replica with the specified name. Verify that the availability replica name is correct, then retry the operation. |
| 35284 | 16 | No | Availability replica '%.\*ls' cannot be removed from availability group '%.\*ls', because this replica is on the local instance of SQL Server. If the local availability replica is a secondary replica, connect to the server instance that is currently hosting the primary replica, and re-run the command. |
| 35285 | 10 | No | The recovery LSN %S_LSN was identified for the database with ID %d. This is an informational message only. No user action is required. |
| 35286 | 16 | No | Using the recovery LSN %S_LSN stored in the metadata for the database with ID %d. This is an informational message only. No user action is required. |
| 35287 | 16 | No | Always On Availability Groups transport for availability database "%.\*ls" failed to decompress the log block whose LSN is %S_LSN. This error can be caused by a corrupt network packet or a compression version mismatch. The database replica has been put into the SUSPENDED state. Resume the availability database. If the error keeps reoccurring, investigate the root cause. |
| 35288 | 16 | No | Always On Availability Groups log apply for availability database "%.\*ls" has received an out-of-order log block. The expected LSN was %S_LSN. The received LSN was %S_LSN. The database replica has been put into the SUSPENDED state. Resume the availability database. If the error reoccurs, contact Customer Support Services. |
| 35289 | 16 | No | Failed to send request for file '%.\*ls' to the '%.\*ls' primary database for the local secondary database. Resuming the database will be retried automatically. |
| 35290 | 16 | No | Failed to wait for completion of file requests from the '%.\*ls' primary database for the local secondary database. Resuming the database will be retried automatically. |
| 35291 | 10 | No | Failed to acquire exclusive access to the extended recovery fork stack (error %d). If the problem persists, you might need to restart the instance of SQL Server. |
| 35292 | 16 | No | An internal error occurred when performing an operation on extended recovery forks. This is an informational message only. No user action is required. |
| 35293 | 16 | No | Error in retrieving extended recovery forks from the primary replica. The extended-recovery-fork stack changed while being retrieved by the secondary replica. Retry the operation. |
| 35294 | 16 | No | %ls backup for database "%.\*ls" on a secondary replica failed because a synchronization point could not be established on the primary database. Either locks could not be acquired on the primary database or the database is not operating as part of the availability replica. Check the database status in the SQL Server error log of the server instance that is hosting the current primary replica. If the primary database is participating in the availability group, retry the operation. |
| 35295 | 16 | No | Log backup for database "%.\*ls" on a secondary replica failed because the last backup LSN (0x%ls) from the primary database is greater than the current local redo LSN (0x%ls). No log records need to be backed up at this time. Retry the log-backup operation later. |
| 35296 | 16 | No | Log backup for database "%.\*ls" on secondary replica failed because the new backup information could not be committed on primary database. Check the database status in the SQL Server error log of the server instance that is hosting the current primary replica. If the primary database is participating in the availability group, retry the operation. |
| 35297 | 10 | No | %ls backup for database "%.\*ls" on secondary replica created backup files successfully but could not ensure that a backup point has been committed on the primary. This is an informational message only. Preserve this log backup along with the other log backups of this database. |
| 35298 | 10 | No | The backup on the secondary database "%.\*ls" was terminated, but a terminate backup message could not be sent to the primary replica. This is an informational message only. The primary replica should detect this error and clean up its backup history accordingly. |
| 35299 | 10 | Yes | Nonqualified transactions are being rolled back in database %.\*ls for an Always On Availability Groups state change. Estimated rollback completion: %d%%. This is an informational message only. No user action is required. |
| 35301 | 15 | No | The statement failed because a columnstore index cannot be unique. Create the columnstore index without the UNIQUE keyword or create a unique index without the COLUMNSTORE keyword. |
| 35302 | 15 | No | The statement failed because specifying sort order (ASC or DESC) is not allowed when creating a columnstore index. Create the columnstore index without specifying a sort order. |
| 35303 | 15 | No | The statement failed because a nonclustered index cannot be created on a table that has a clustered columnstore index. Consider replacing the clustered columnstore index with a nonclustered columnstore index. |
| 35304 | 15 | No | The statement failed because a clustered columnstore index cannot be created on a table that has a nonclustered index. Consider dropping all nonclustered indexes and trying again. |
| 35305 | 15 | No | The statement failed because a clustered columnstore index cannot be created on a view. Consider creating a nonclustered columnstore index on the view, creating a clustered columnstore index on the base table or creating an index without the COLUMNSTORE keyword on the view. |
| 35306 | 15 | No | The statement failed because a columnstore index cannot be specified using INDEX specification at the column level. |
| 35307 | 15 | No | The statement failed because column '%.\*ls' on table '%.\*ls' is a computed column. Columnstore index cannot include a computed column implicitly or explicitly. |
| 35308 | 15 | No | The statement failed because a columnstore index cannot be a filtered index. Consider creating a columnstore index without the predicate filter. |
| 35309 | 15 | No | The statement failed because a columnstore index cannot be created on a sparse column. Consider creating a nonclustered columnstore index on a subset of columns that does not include any sparse columns. |
| 35310 | 15 | No | The statement failed because columnstore indexes are not allowed on table types and table variables. Remove the column store index specification from the table type or table variable declaration. |
| 35311 | 15 | No | The statement failed because a columnstore index cannot have included columns. Create the columnstore index on the desired columns without specifying any included columns. |
| 35312 | 15 | No | The statement failed because a columnstore index cannot be created on a column with filestream data. Consider creating a nonclustered columnstore index on a subset of columns that does not include any columns with filestream data. |
| 35313 | 15 | No | The statement failed because specifying FILESTREAM_ON is not allowed when creating a columnstore index. Consider creating a columnstore index on columns without filestream data and omit the FILESTREAM_ON specification. |
| 35314 | 15 | No | The statement failed because a columnstore index cannot be created on a column set. Consider creating a nonclustered columnstore index on a subset of columns in the table that does not contain a column set or any sparse columns. |
| 35315 | 15 | No | The statement failed because a columnstore index cannot be created in this edition of SQL Server. See Books Online for more details on feature support in different SQL Server editions. |
| 35316 | 15 | No | The statement failed because a columnstore index must be partition-aligned with the base table. Create the columnstore index using the same partition function and same (or equivalent) partition scheme as the base table. If the base table is not partitioned, create a nonpartitioned columnstore index. |
| 35317 | 15 | No | The statement failed because specifying %S_MSG is not allowed when creating a columnstore index. Consider creating a columnstore index without specifying %S_MSG. |
| 35318 | 15 | No | The statement failed because the %S_MSG option is not allowed when creating a columnstore index. Create the columnstore index without specifying the %S_MSG option. |
| 35319 | 15 | No | The table option DATA_COMPRESSION is not allowed when a table specifies a clustered column store index. |
| 35320 | 15 | No | Column store indexes are not allowed on tables for which the durability option SCHEMA_ONLY is specified. |
| 35321 | 16 | No | Table '%.\*ls' uses a clustered columnstore index. Columnstore indexes are not supported in this service tier of the database. See Books Online for more details on feature support in different service tiers of Windows Azure SQL Database. |
| 35322 | 16 | No | The statement failed because a nonclustered index and the underlying clustered columnstore index must be partition-aligned. Consider creating the nonclustered index using the same partition function and same (or equivalent) partition scheme as the clustered columnstore index. |
| 35323 | 16 | No | The statement failed because adding multiple generated value type columns (such as identity and uniqueidentifier types) at once is not supported for columnstore index. Consider adding the generated columns separately. |
| 35324 | 15 | No | The statement failed because specifying SORT_IN_TEMPDB is not allowed when creating a columnstore index without ORDER. Consider creating a columnstore index without specifying SORT_IN_TEMPDB, or use it with ORDER clause. |
| 35325 | 15 | No | The statement failed because the definition of a column belonging to clustered columnstore index cannot be changed. Consider dropping the columnstore index, altering the column, then creating a new columnstore index. |
| 35326 | 15 | No | The statement failed because a nonclustered columnstore index cannot be reorganized. Reorganization of a nonclustered columnstore index is not necessary. |
| 35327 | 15 | No | ALTER INDEX REBUILD statement failed because specifying %S_MSG is not allowed when rebuilding a columnstore index. Rebuild the columnstore index without specifying %S_MSG. |
| 35328 | 15 | No | ALTER INDEX REBUILD statement failed because the %S_MSG option is not allowed when rebuilding a columnstore index. Rebuild the columnstore index without specifying the %S_MSG option. |
| 35329 | 15 | No | The statement failed because specifying ORDER is not allowed when creating the rowstore index '%.\*ls' on table '%.\*ls'. Consider creating the rowstore index without specifying ORDER. |
| 35330 | 16 | No | %S_MSG statement failed because data cannot be updated in a table that has a nonclustered columnstore index. Consider disabling the columnstore index before issuing the %S_MSG statement, and then rebuilding the columnstore index after %S_MSG has completed. |
| 35331 | 16 | No | Cannot use duplicate column names in the ORDER column list. Column name '%.\*ls' appears more than once. |
| 35332 | 16 | No | Cannot ORDER on more than %d columns. |
| 35334 | 15 | No | DBCC DBREINDEX failed because specifying FILLFACTOR is not allowed when creating or rebuilding a columnstore index. Rebuild the columnstore index without specifying FILLFACTOR. |
| 35335 | 15 | No | The statement failed because specifying a key list is not allowed when creating a clustered columnstore index. Create the clustered columnstore index without specifying a key list. |
| 35336 | 15 | No | The statement failed because specifying key list is missing when creating an index. Create the index with specifying key list . |
| 35337 | 16 | No | UPDATE STATISTICS failed because statistics cannot be updated on a columnstore index. UPDATE STATISTICS is valid only when used with the STATS_STREAM option. |
| 35338 | 16 | No | Clustered columnstore index is not supported. |
| 35339 | 16 | No | Multiple columnstore indexes are not supported. |
| 35340 | 16 | No | LOB columns are disabled in columnstore. |
| 35341 | 16 | No | The statement failed. A columnstore index cannot include a decimal or numeric data type with a precision greater than 18. Reduce the precision or omit column '%.\*ls'. |
| 35342 | 15 | No | The statement failed because specifying ORDER is not a valid option for creating the non-clustered columnstore index '%.\*ls' on table '%.\*ls'. Consider creating the non-clustered columnstore index without specifying ORDER, or create a clustered columnstore index. |
| 35343 | 15 | No | The statement failed. Column '%.\*ls' has a data type that cannot participate in a columnstore index. |
| 35344 | 16 | No | MERGE clause of ALTER PARTITION statement failed because two nonempty partitions containing a columnstore index cannot be merged. Consider an ALTER TABLE SWITCH operation from one of the nonempty partitions on table '%.\*ls' to a temporary staging table and then re-attempt the ALTER PARTITION MERGE operation. Once completed, use ALTER TABLE SWITCH to move the staging table partition back to the original source table. |
| 35345 | 16 | No | MERGE clause of ALTER PARTITION statement failed because two partitions on different filegroups cannot be merged if either partition contains columnstore index data. Consider dropping the columnstore index on table '%.\*ls' before issuing the ALTER PARTITION statement, then rebuilding the columnstore index after ALTER PARTITION is complete. |
| 35346 | 16 | No | SPLIT clause of ALTER PARTITION statement failed because the partition is not empty. Only empty partitions can be split in when a columnstore index exists on the table. Consider an ALTER TABLE SWITCH operation from one of the nonempty partitions on table '%.\*ls' to a temporary staging table and then re-attempt the ALTER PARTITION SPLIT operation. Once completed, use ALTER TABLE SWITCH to move the staging table partition back to the original source table. |
| 35347 | 16 | No | The stored procedure 'sp_tableoption' failed because a table with a columnstore index cannot be altered to use vardecimal storage format. Consider dropping the columnstore index. |
| 35348 | 16 | No | The statement failed because table '%.\*ls' uses vardecimal storage format. A columnstore index cannot be created on a table using vardecimal storage. Consider rebuilding the table without vardecimal storage. |
| 35349 | 16 | No | TRUNCATE TABLE statement failed because table '%.\*ls' has a columnstore index on it. A table with a columnstore index cannot be truncated. Consider dropping the columnstore index then truncating the table. |
| 35350 | 16 | No | The statement failed because a columnstore index on a partitioned table must be partition-aligned with the base table. Consider dropping the columnstore index before creating a new clustered index. |
| 35351 | 16 | No | DROP INDEX statement failed because a columnstore index on a partitioned table must be partition-aligned with the base table (heap). Consider dropping the columnstore index before dropping a clustered index. |
| 35352 | 16 | No | %S_MSG statement failed because the operation cannot be performed online on a table with a columnstore index. Perform the operation without specifying the ONLINE option or drop (or disable) the columnstore index before performing the operation using the ONLINE option. |
| 35353 | 16 | No | %s cannot be enabled on a table with a clustered columnstore index. Consider dropping clustered columnstore index '%s' on table '%s'. |
| 35354 | 16 | No | The statement failed because a clustered columnstore index cannot be created on a table enabled for %S_MSG. Consider disabling %S_MSG and then creating the clustered columnstore index. |
| 35355 | 16 | No | The statement failed. Column '%.\*ls' is either a primary key or a partitioning key that must be included, but a columnstore index cannot include a decimal or numeric data type with a precision greater than 18. Consider reducing the precision to 18. |
| 35356 | 16 | No | This operation is not supported on nonclustered columnstore indexes built in earlier versions of SQL Server. Consider rebuilding the index. |
| 35357 | 16 | No | The statement failed because a secondary dictionary reached the maximum size limit. Consider dropping the columnstore index, altering the column, then creating a new columnstore index. |
| 35358 | 16 | No | CREATE TRIGGER on table '%.\*ls' failed because you cannot create a trigger on a table with a clustered columnstore index. Consider enforcing the logic of the trigger in some other way, or if you must use a trigger, use a heap or B-tree index instead. |
| 35359 | 16 | No | The statement failed because a table with a clustered columnstore index cannot have triggers. Consider removing all triggers from the table and then creating the clustered columnstore index. |
| 35360 | 16 | No | Referential constraint '%.\*ls' cannot be created because the %S_MSG table '%.\*ls' has a clustered columnstore index. |
| 35361 | 16 | No | The statement failed. A clustered columnstore index cannot be created over referencing column '%.\*ls' on table '%.\*ls'. |
| 35362 | 16 | No | Tuple mover stvf skipped rowset in DW mode |
| 35363 | 16 | No | The statement failed because clustered columnstore indexes are not supported in system databases. |
| 35364 | 16 | No | ALTER INDEX statement option COMPRESSION_DELAY can only be used with columnstore indexes. |
| 35365 | 22 | No | The compression block header at offset %ld is invalid. |
| 35366 | 22 | No | The columnstore blob Xpress header is invalid. |
| 35367 | 22 | No | The columnstore blob Xpress object header is invalid. |
| 35368 | 17 | No | Internal DDL Operation Error: The DDL encountered an unexpected error %d during execution (HRESULT = 0x%x). |
| 35369 | 16 | No | GETCHECKSUM can not be used for a table which has a clustered columnstore index. |
| 35370 | 16 | No | Cursors are not supported on a table which has a clustered columnstore index. |
| 35371 | 16 | No | SNAPSHOT isolation level is not supported on a table which has a clustered columnstore index. |
| 35372 | 16 | No | You cannot create more than one clustered index on %S_MSG '%.\*ls'. Consider creating a new clustered index using 'with (drop_existing = on)' option. |
| 35373 | 16 | No | ALTER INDEX REORGANIZE statement failed on a clustered columnstore index with error %d. See previous error messages for more details. |
| 35374 | 16 | No | Columnstore archive decompression failed with error %d. |
| 35375 | 16 | No | ALTER INDEX REORGANIZE statement option COMPRESS_ALL_ROW_GROUPS can only be used with clustered columnstore indexes. |
| 35376 | 16 | No | Tuple mover stvf got passed invalid arguments |
| 35377 | 22 | No | During migration to rowgroup consolidation, distribution type extended property must be set on the CCI table. |
| 35378 | 16 | No | Row groups stvf got passed invalid arguments |
| 35379 | 16 | No | Internal error occurred while flushing delete buffer database id %d, table id %d, index id %d, partition number %d. Additional messages in the SQL Server error log may provide more details. |
| 35380 | 16 | No | The non-clustered columnstore index '%.\*ls' on table '%.\*ls' cannot be reorganized due to an active snapshot transaction. |
| 35381 | 22 | No | The columnstore blob dictionary header is invalid. |
| 35382 | 16 | No | The specified COMPRESSION_DELAY option value %d is invalid. The valid range for disk-based table is between (0, 10080) minutes and for memory-optimized table is 0 or between (60, 10080) minutes. |
| 35383 | 16 | No | The use of user-defined functions is not allowed in default constraints when adding columns to a columnstore index. |
| 35384 | 16 | No | The statement failed because column '%.\*ls' on table '%.\*ls' is a lob column. Non clustered index with lob column as included column cannot co-exist with clustered columnstore index. |
| 35386 | 17 | No | Unable to allocate %I64d KB for columnstore compression because it exceeds the remaining memory from total allocated for current resource class and DWU. Please rerun query at a higher resource class, and also consider increasing DWU. See 'https://aka.ms/sqldw_columnstore_memory' for assistance. |
| 35387 | 17 | No | TEMPDB ran out of space during spilling. Verify that data is evenly distributed and/or rewrite the query to consume fewer rows. If the issue still persists, consider upgrading to a higher service level objective. |
| 35388 | 15 | No | The statement failed because column '%.\*ls' on table '%.\*ls' is a persisted computed column. Adding persisted computed columns to an existing clustered columnstore index is not supported. |
| 35389 | 15 | No | The statement failed because column '%.\*ls' on table '%.\*ls' is a computed column of a LOB type. Columnstore indexes cannot include computed columns of LOB types. |
| 35390 | 16 | No | Computed columns in columnstore indexes are temporarily disabled. Please rewrite your query to exclude computed columns. |
| 35391 | 16 | No | Could not build or rebuild clustered columnstore index '%.\*ls' online, because the the table has secondary indexes. Please perform the operation offline, or remove the secondary indexes and try again. |
| 35392 | 15 | No | The statement failed because column '%.\*ls' on table '%.\*ls' is a computed column. Nonclustered index with computed column as key or included column cannot be created on a table that has a clustered columnstore index. |
| 35393 | 16 | No | Columnstore tuple mover unit tests can't be run under master database. |
| 35394 | 16 | No | Transaction %d reached %I64u log used bytes and %I64d reserved log bytes. In order to minimize log usage %d suboptimal rowgroups were created, which is not allowed since it compromises index quality. Please re-run query at a higher resource class, and also consider increasing DWU. See 'https://aka.ms/sqldw_columnstore_memory' for assistance. |
| 35395 | 16 | No | Transaction %d reached %I64u log used bytes and %I64d reserved log bytes. In order to minimize log usage, %d suboptimal rowgroups were created, which is not allowed since it compromises index quality. Please re-run the query with more memory (change your Resource Governor settings or upgrade to a higher SLO) or target fewer partitions in the load if your target table is partitioned. |
| 35396 | 17 | No | Effective DOP of %u is too high for columnstore compression on table id %d because each thread only has %I64d KB for compression. Please retry using the MAXDOP hint to use a lower DOP. |
| 35397 | 16 | No | %S_MSG statement failed because the operation cannot be performed resumably on a table with a columnstore index. Perform the operation without specifying the RESUMABLE option or drop (or disable) the columnstore index before performing the operation using the RESUMABLE option. |
| 35398 | 16 | No | Columnstore index operation failed. Please contact Microsoft customer support. |
| 35399 | 16 | No | Could not create columnstore index '%.\*ls' on table '%.\*ls' since data type of column '%.\*ls' is not supported in the ORDER list. |
| 35400 | 16 | No | Columnstore tuple mover is unable to acquire lock. |
| 35401 | 10 | No | ONLINE |
| 35402 | 10 | No | ALLOW_ROW_LOCKS |
| 35403 | 10 | No | ALLOW_PAGE_LOCKS |
| 35404 | 16 | No | sequence |
| 35405 | 10 | No | decryption |
| 35406 | 10 | No | creation |
| 35409 | 10 | No | Replication |
| 35410 | 10 | No | Change Tracking |
| 35411 | 10 | No | Change Data Capture |
| 35412 | 16 | No | CloudDB Async Transport |
| 35413 | 16 | No | CloudDB Async Transport Forwarder |
| 35415 | 10 | No | mirrored |
| 35417 | 10 | No | primary |
| 35418 | 10 | No | secondary |
| 35419 | 16 | No | feature |
| 35420 | 16 | No | operation |
| 35421 | 16 | No | statement |
| 35422 | 16 | No | index option |
| 35423 | 16 | No | table option |
| 35424 | 16 | No | operator |
| 35425 | 16 | No | value |
| 35426 | 16 | No | system column |
| 35427 | 16 | No | set option |
| 35428 | 16 | No | query hint |
| 35429 | 16 | No | transaction isolation level |
| 35430 | 16 | No | in-memory index |
| 35431 | 10 | No | SCHEDULER |
| 35432 | 10 | No | NUMANODE |
| 35435 | 16 | No | system database |
| 35439 | 10 | No | it is a system database. |
| 35440 | 10 | No | it acts as a distribution database |
| 35441 | 10 | No | an internal error occurred |
| 35442 | 10 | No | it is involved in a mirroring session. Turn off the mirroring session and try again |
| 35443 | 10 | No | it is not in SIMPLE recovery mode. Change the recovery model to SIMPLE and try again |
| 35444 | 10 | No | it is a database snapshot |
| 35445 | 10 | No | it is a not currently enabled |
| 35446 | 16 | No | clause |
| 35447 | 10 | No | Cluster Proxy |
| 35448 | 10 | No | partition |
| 35449 | 16 | No | signing algorithm |
| 35450 | 16 | No | table |
| 35451 | 16 | No | join hint |
| 35452 | 10 | No | Database unavailable |
| 35453 | 10 | No | it is a system database. Make sure to target a user database. If you want temporary clustered columnstore tables, consider creating a regular user database for them |
| 35454 | 10 | No | a transaction is currently active. Execute sp_db_enable_clustered_columnstores by itself |
| 35455 | 10 | No | an internal error occurred |
| 35456 | 10 | No | it is involved in a mirroring pair. Consider disabling mirroring, changing the setting, then re-establishing mirroring |
| 35457 | 10 | No | it is a database snapshot |
| 35458 | 10 | No | it is part of an Always On availability group. Consider removing the database from the availability group, changing the setting, and then adding the database back to the availability group |
| 35459 | 10 | No | it is a replication distribution database. Make sure to target a regular user database |
| 35460 | 10 | No | it is not using the SIMPLE recovery model. Consider temporarily changing to the simple recovery model, then downgrading, then switching back |
| 35461 | 10 | No | it contains one or more clustered columnstore indexes. Consider dropping these indexes or creating clustered B-tree indexes instead, and trying again |
| 35462 | 10 | No | referencing |
| 35463 | 10 | No | referenced |
| 35464 | 10 | No | columnstore indexes are not supported in the current SQL Server edition. See SQL Server Books Online for supported editions |
| 35465 | 17 | No | Unable to allocate the recovery thread |
| 35466 | 16 | No | Exception in the recovery thread |
| 35467 | 16 | No | Already linked to a partition host |
| 35468 | 16 | No | Partition DB marked as suspect |
| 35469 | 16 | No | Failed to get Partition DB attributes |
| 35470 | 16 | No | Partition Host |
| 35471 | 10 | No | Partition metadata not found |
| 35472 | 15 | No | memory optimized tables |
| 35473 | 15 | No | natively compiled modules |
| 35474 | 15 | No | indexes on memory optimized tables |
| 35475 | 15 | No | hash indexes |
| 35476 | 15 | No | transactions that access memory optimized tables or natively compiled modules |
| 35477 | 15 | No | databases that have a MEMORY_OPTIMIZED_DATA filegroup |
| 35478 | 16 | No | truncate |
| 35479 | 16 | No | start |
| 35480 | 16 | No | stop |
| 35481 | 15 | No | memory optimized table types |
| 35482 | 16 | No | clustered |
| 35483 | 16 | No | nonclustered |
| 35484 | 16 | No | BUCKET_COUNT |
| 35485 | 16 | No | security policy |
| 35486 | 15 | No | memory optimized tables that have a column store index |
| 35487 | 10 | No | Global Transactions |
| 35488 | 15 | No | natively compiled triggers |
| 35489 | 16 | No | Upgrade of Hekaton database |
| 35490 | 16 | No | memory optimized tables that have max length columns |
| 35491 | 16 | No | max length columns in natively compiled modules |
| 35492 | 16 | No | column definition |
| 35493 | 16 | No | enabled |
| 35494 | 16 | No | disabled |
| 35495 | 15 | No | memory optimized table |
| 35496 | 15 | No | natively compiled module |
| 35497 | 15 | No | module |
| 35498 | 16 | No | external data source |
| 35499 | 16 | No | OUTPUT clauses in natively compiled modules |
| 35501 | 15 | No | schemas that contain natively compiled modules |
| 35502 | 15 | No | change_tracking_hardened_cleanup_version() |
| 35503 | 15 | No | safe_cleanup_version() |
| 35504 | 16 | No | server encryption protector |
| 35505 | 15 | No | RESUMABLE |
| 35506 | 15 | No | MAX_DURATION |
| 35507 | 10 | No | Storage |
| 35508 | 10 | No | RESUME |
| 35509 | 10 | No | PAUSE |
| 35510 | 10 | No | ABORT |
| 35511 | 10 | No | ABORT |
| 35512 | 10 | No | allow_enclave_computations |
| 35513 | 10 | No | compute |
| 35514 | 10 | No | local computer |
| 35515 | 10 | No | current user |
| 35516 | 10 | No | query_capture_policy |
| 35517 | 10 | No | execution_count |
| 35518 | 10 | No | total_compile_cpu_time_ms |
| 35519 | 10 | No | total_execution_cpu_time_ms |
| 35520 | 10 | No | OPTIMIZE_FOR_SEQUENTIAL_KEY |
| 35521 | 10 | No | stale_capture_policy_threshold |
| 35522 | 10 | No | day |
| 35523 | 10 | No | TRIPLE_DES_3KEY |
| 35524 | 10 | No | AES_256 |
| 35525 | 10 | No | STATISTICS_ONLY |
| 35526 | 10 | No | striped_metadata |
| 35527 | 10 | No | rbio_connection |
| 35528 | 10 | No | unable to read page due to invalid FCB |
| 35529 | 10 | No | certificate id |
| 35532 | 10 | No | certificate name |
| 36001 | 16 | No | %s '%s' already exists in the database. |
| 36002 | 16 | No | instance_id already exists in the database. |
| 36003 | 16 | No | %s '%s' already exists for the given DAC instance. |
| 36004 | 16 | No | DacInstance with the specified instance_id does not exist. |
| 36005 | 16 | No | Dac root - database %s does not exist. |
| 36006 | 16 | No | Dac Policy with the specified policy id already exists in the parts table. |
| 36007 | 16 | No | Dac Part the policy refers to, does not exist. |
| 36008 | 16 | No | Dac Policy refers to a non-existent Policy. |
| 36009 | 16 | No | %s '%s' already exists in the Dac Parts. |
| 36010 | 16 | No | The caller must be a member of the dbcreator fixed server role to perform this action. |
| 36011 | 16 | No | The caller must be sysadmin or the creator of the history entry being updated. |
| 36012 | 10 | No | Unable to execute T-SQL within procedure due to SQL Server limitation.\nPlease execute following T-SQL in database '%s' context after this procedure finishes:\n%s |
| 36101 | 16 | No | Process ID %d is not an active process ID. |
| 37001 | 16 | No | This operation is not allowed since there are dependent objects pending installation. |
| 37002 | 16 | No | Cannot find the database '%s', because it does not exist or you do not have permission to access it. |
| 37003 | 16 | No | This operation is not allowed because a utility control point already exists on this instance of SQL Server. |
| 37004 | 16 | No | The specified instance of SQL Server cannot be used as a utility control point because the feature is not enabled in SQL Server '%s'. |
| 37005 | 16 | No | The specified instance of SQL Server cannot be managed by a utility control point because the feature is not enabled in SQL Server '%s'. |
| 37006 | 16 | No | Cannot perform the operation because the specified instance of SQL Server is not enrolled in a SQL Server utility. |
| 37007 | 16 | No | An error occurred during upload to the SQL Server utility control point. |
| 37008 | 16 | No | The operation cannot continue. To remove the SQL Server utility control point, the user must be a member of the sysadmin role. |
| 37009 | 16 | No | The operation cannot continue. The specified instance of SQL Server is not a SQL Server utility control point. |
| 37010 | 16 | No | The operation cannot continue. The SQL Server utility control point has managed instances of SQL Server enrolled. |
| 37101 | 16 | No | Server '%.\*ls' does not contain elastic job account: '%.\*ls'. |
| 37102 | 16 | No | Elastic job account '%.\*ls' does not contain the resource of type '%.\*ls' named '%.\*ls'. |
| 37103 | 16 | No | Internal job account error occurred : '%.\*ls'. |
| 37104 | 16 | No | A job account already exists for subscription '%.\*ls' for the selected region. |
| 37105 | 16 | No | The job account '%.\*ls' on server '%.\*ls' already exists. |
| 37106 | 16 | No | The database '%.\*ls' on server '%.\*ls' is in use by job account '%.\*ls'. The database cannot be deleted or renamed while associated with a job account. |
| 37107 | 16 | No | The database '%.\*ls' on server '%.\*ls' has service level objective '%.\*ls' which is not supported for use as a job account database. |
| 37108 | 16 | No | A job account could not be linked to database '%.\*ls' on server '%.\*ls' because it is a geo-secondary database. |
| 37109 | 16 | No | Database '%ls' on server '%ls' is already linked to another job account. |
| 37201 | 16 | No | An instance pool could not be found with name '%.\*ls'. |
| 37202 | 16 | No | An instance pool with name '%.\*ls' is busy with another ongoing operation. |
| 37203 | 16 | No | An instance pool with name '%.\*ls' is not empty. |
| 37204 | 16 | No | An instance pool with name '%.\*ls' does not have enough vCore capacity for given request. |
| 37301 | 16 | Yes | Internal enclave error. The enclave symcrypt method %ls failed with status 0x%08x. For more information, contact Customer Support Services. |
| 37302 | 16 | Yes | %ls. Internal error. API: '%ls'. Return code: '0x%08x'. For more information, contact Customer Support Services. |
| 37303 | 16 | Yes | Internal error occurred while obtaining an authentication token for external service. Authentication method: %ls, status: 0x%08x. |
| 37304 | 16 | No | Cannot initialize %ls enclave for column encryption - the operating system does not support the enclave type. |
| 37305 | 16 | Yes | Invalid enclave configuration: %ls. |
| 37306 | 16 | Yes | Internal enclave error: Cannot initialize the %ls enclave. Error: 0x%08x. For more information, contact Customer Support Services. |
| 37307 | 16 | Yes | Internal enclave error: Cannot initialize the %ls enclave. Load method: %ls. For more information, contact Customer Support Services. |
| 37308 | 10 | Yes | Loaded %ls enclave for Always Encrypted. |
| 37309 | 16 | Yes | Enclave attestation failed due to an error in Intel Data Center Attestation Primitives (DCAP) SGX API: '%ls'. Return code: '0x%08x'. For more information, contact Customer Support Services. |
| 37310 | 16 | Yes | %ls. Error while parsing URL. URL: '%ls'. Return code: '0x%08x'. |
| 37311 | 16 | Yes | %ls. The URL has an invalid scheme name. URL: '%ls'. The supported schemes are: '%ls'. |
| 37312 | 16 | Yes | %ls. The service URL specified by the client is not reachable. URL: '%ls'. Return code: '0x%08x'. Check your networking configuration. |
| 37313 | 16 | Yes | Enclave attestation failed. The attestation service returned an empty response. Attestation URL: '%ls'. Verify the attestation policy. If the policy is correct, contact Customer Support Services. |
| 37314 | 16 | Yes | VBS enclave attestation failed due to an error in Windows Management Instrumentation (WMI). API: '%ls', Return code: '0x%08x'. Check the Host Gurdian Service is running. For more information, contact Customer Support Services. |
| 37315 | 16 | Yes | Credential object named '0x%08x', configured for external authentication, does not exist. |
| 37316 | 16 | Yes | Authentication bearer service failed to return a valid challenge. Verify URL '%ls' is correct. Return code: '0x%08x'. |
| 37317 | 16 | Yes | Failed to look up '%ls' for '%ls'. |
| 37318 | 16 | Yes | Unable to acquire an authentication token for resource_id '%ls' with URL '%ls'. Return code: '0x%08x' Correlation Id: '%ls'. For more information, contact Customer Support Services. |
| 37319 | 16 | Yes | Failed to obtain an authentication token for URL '%ls', Return code: '0x%08x', CorrelationId: %ls. |
| 37320 | 16 | Yes | Authentication for URL '%ls' failed due to an invalid client id or an invalid secret. Return code: '0x%08x' Correlation Id: '%ls' |
| 37321 | 16 | Yes | Failed to acquire a token using a managed service identity. Make sure managed identities are enabled on the machine hosting SQL Server. Return code: '0x%08x'. |
| 37322 | 16 | Yes | Failed to acquire a token using a managed service identity. Make sure managed identities are enabled on the machine hosting SQL Server. Error code: '%ls'. Error message: '%ls'. Return code: '0x%08x'. |
| 37323 | 16 | Yes | Authentication bearer service failed to return a valid challenge. Verify URL '%ls' is correct. Error code: '%ls'. Error message: '%ls'. Return code: '0x%08x'. |
| 37324 | 16 | Yes | %ls. Service returned Error code: '%ls'. Error message: '%ls'. Service URL: '%ls'. Return code: '0x%08x'. Verify the service policy. For more information see: '%ls'. |
| 37325 | 16 | Yes | Enclave attestation failed due to an error in Azure Data Center Attestation Primitives (DCAP) Client. Validate Azure DCAP Client is installed and configured properly. Return code: '0x%08x'. For more information, contact Customer Support Services. |
| 37326 | 16 | Yes | Service failed due to an authorization failure. Verify the server or instance has the required permissions to access service at URL '%ls'. |
| 37327 | 16 | Yes | Maximum number of concurrent DBCC commands running in the enclave has been reached. The maximum number of concurrent DBCC queries is %d. Try rerunning the query. |
| 37328 | 10 | Yes | Configuring Always Encrypted enclave in %ls mode. |
| 37329 | 10 | Yes | %ls enclave lost due to internal error. For more information, contact Customer Support Services. |
| 37330 | 16 | Yes | Failed to acquire a token using a managed service identity. Make sure managed identities are enabled on the machine hosting SQL Server. Authentication method: %ls, Return code: '0x%08x'. Validate that Azure Active Directory Identity as been assigned to this server. For more information, contact Customer Support Services |
| 37331 | 16 | No | DevOps login, '%s', can not be created as another non-devops server principal with name or object ID already exists. |
| 37332 | 16 | No | DevOps user, '%.\*ls', can not be created as another non-devops database principal with name or object ID already exists. |
| 37333 | 16 | Yes | The server encountered an unexpected exception. API:'%ls' Return code: '0x%08x'. For more information, contact Customer Support Services. |
| 37334 | 16 | Yes | Internal error occurred while obtaining an authentication token for an external service. Authentication method: %ls, Status: 0x%08x, CorrelationId: %ls. |
| 37335 | 16 | Yes | Failed to acquire a token using a managed service identity. Make sure managed identities are enabled on the machine hosting SQL Server. Authentication method: %ls, Return code: '0x%08x', CorrelationId: %ls. Validate that Azure Active Directory Identity as been assigned to this server. For more information, contact Customer Support Services |
| 37336 | 17 | Yes | Failed to flush the Ledger Transactions table to disk in database with ID %d due to error %d. Check the errorlog for more information. |
| 37337 | 16 | Yes | Failed to retrieve the information about the latest transaction persisted in sys.db_ledger_transactions in database with ID %d due to error %d. Check the errorlog for more information. |
| 37338 | 16 | No | Error occurred while generating the hash for a ledger transaction. Return code: '%d'. |
| 37339 | 16 | No | Error occurred while generating a hash for the MERKLE_TREE_AGG aggregate function. Return code: '%d'. |
| 37340 | 16 | Yes | Failed to generate the Ledger Blocks in the database with ID %d due to error %d. Check the errorlog for more information. |
| 37341 | 16 | No | Azure Active Directory Administrator is not enabled for this server. Please set up the AAD Admin on this server and try again. |
| 37342 | 16 | No | The logical server AAD tenant ID does not match the given devops AAD tenant ID. |
| 37343 | 16 | No | Unexpected data type for encrypted column while generating ledger hash. |
| 37344 | 16 | No | CREATE TABLE failed because column '%.\*ls' in table '%.\*ls' exceeds the maximum of %d columns allowed for ledger tables. |
| 37345 | 16 | No | Table cannot have more than one 'GENERATED ALWAYS AS %ls' column. |
| 37346 | 16 | No | 'GENERATED ALWAYS AS %ls' column '%.\*ls' has invalid data type. |
| 37347 | 16 | No | 'GENERATED ALWAYS AS %ls' column '%.\*ls' cannot be nullable. |
| 37348 | 16 | No | 'GENERATED ALWAYS AS %ls' column '%.\*ls' can only be nullable. |
| 37349 | 16 | No | LEDGER = ON cannot be specified with SYSTEM_VERSIONING = OFF and APPEND_ONLY = OFF. |
| 37350 | 16 | No | LEDGER = ON cannot be specified with SYSTEM_VERSIONING = ON and APPEND_ONLY = ON. |
| 37351 | 16 | No | LEDGER = ON cannot be specified with PERIOD FOR SYSTEM_TIME and APPEND_ONLY = ON. |
| 37352 | 16 | No | APPEND_ONLY = ON cannot be specified with generated always end columns. |
| 37353 | 16 | No | Server identity does not have Azure Active Directory Readers permission. Please follow the steps here : [https://learn.microsoft.com/azure/azure-sql/database/authentication-aad-service-principal](/azure/azure-sql/database/authentication-aad-service-principal) |
| 37354 | 16 | No | LEDGER = ON cannot be specified with system versioning retention period. |
| 37355 | 16 | No | An existing History Table cannot be specified with LEDGER=ON. |
| 37356 | 16 | No | System Versioning cannot be altered for Ledger Tables. |
| 37357 | 17 | No | SQL Audit could not write to '%s'. Make sure that SAS key is valid or Managed Identity has permissions to access the storage. This is an informative message. This error doesn't affect SQL availability. |
| 37358 | 17 | No | SQL Audit failed to create the audit file for the audit '%s' at '%s'. Make sure that SAS key is valid or Managed Identity has permissions to access the storage. This is an informative message. This error doesn't affect SQL availability. |
| 37359 | 16 | No | Updates are not allowed for the append only Ledger table '%.\*ls'. |
| 37360 | 16 | No | %S_MSG is not allowed on Ledger tables. |
| 37361 | 16 | No | Cannot update ledger history table '%.\*ls'. |
| 37362 | 16 | No | LEDGER = ON cannot be specified on a table with a column set column. |
| 37363 | 16 | No | Copy operation for the database '%.\*ls' on the server '%.\*ls' cannot be started because both source and target SLOs need to be DC series. Source DB SLO: '%.\*ls' , Partner DB SLO: '%.\*ls' |
| 37364 | 16 | No | A transaction cannot update more than %d ledger tables. |
| 37365 | 16 | No | The '%s' parameter provided for ledger verification cannot be null. |
| 37366 | 16 | No | The hash computed from sys.database_ledger_transactions for block %I64d does not match the hash persisted in sys.database_ledger_blocks. |
| 37367 | 16 | No | The computed hash does not match the previous block hash persisted in sys.database_ledger_blocks for block %I64d. |
| 37368 | 16 | No | The hash of block %I64d in the database ledger does not match the hash provided in the digest for this block. |
| 37369 | 16 | No | Invalid input for parameter '@digests' provided for ledger verification. The value should be a valid JSON document that contains values for the 'block_id' and 'hash' of each digest. |
| 37370 | 10 | No | Ledger verification successfully verified up to block %I64d. |
| 37371 | 16 | No | The computed hash from '%s' and the associated history table does not match the hash persisted in sys.database_ledger_transactions for transaction %I64d. |
| 37372 | 16 | No | Invalid table ID '%d' is present in database ledger table sys.database_ledger_transactions. |
| 37373 | 16 | No | Error occurred while initializing hash algorithm at startup. Return code: '%d'. |
| 37374 | 16 | No | Creation of Ledger view failed due to error %d. |
| 37375 | 16 | No | Cannot add PRIMARY KEY, UNIQUE KEY or CHECK constraint to the ledger history table '%.\*ls'. |
| 37376 | 16 | No | Foreign key '%.\*ls' is not valid. A ledger history table cannot be used in a foreign key definition. |
| 37377 | 16 | No | Foreign key '%.\*ls' is not valid. An append only ledger table cannot be the referencing table of a foreign key constraint with cascading actions. |
| 37378 | 16 | No | Ledger tables cannot be created on system databases. |
| 37379 | 16 | No | Ledger tables cannot contain columns of XML, sql_variant, filestream or user-defined types. Any computed columns cannot be timestamp columns. |
| 37380 | 16 | No | Cannot create UNIQUE index on ledger history table '%.\*ls'. |
| 37381 | 16 | No | Cannot create a trigger on a system-versioned ledger table '%.\*ls'. |
| 37382 | 16 | No | Switching partition failed on table '%.\*ls' because it is not a supported operation on ledger tables or their corresponding history table. |
| 37383 | 16 | No | Enabling Change Tracking for ledger history table '%.\*ls' is not allowed. |
| 37384 | 16 | No | Table '%.\*ls' is a FileTable. LEDGER = ON cannot be used on FileTables. |
| 37385 | 16 | No | LEDGER = ON is not allowed for table variables. |
| 37386 | 16 | No | Cannot drop object '%.\*ls' because it is a ledger history table or a ledger view. |
| 37387 | 16 | No | Only nullable columns without a default value WITH VALUES can be added to ledger tables. |
| 37388 | 16 | No | Column '%.\*ls' in table '%.\*ls' cannot be dropped because it is already a dropped ledger column. |
| 37389 | 16 | No | Cannot ALTER VIEW '%.\*ls' because it is a ledger view. |
| 37390 | 16 | No | System-time PERIOD cannot be added to table '%.\*ls' because it is a ledger table. |
| 37391 | 16 | No | ALTER TABLE ALTER COLUMN failed for table '%.\*ls' because it is a ledger table and the operation would need to modify existing data that is immutable. |
| 37392 | 16 | No | Ledger verification failed. |
| 37393 | 16 | No | The default ledger view name is constructed as \<table_name\>_Ledger, which exceeds the maximum length of %d characters. Specify a ledger table name that is %d characters or less, or specify a user-defined ledger view name that is %d characters or less. |
| 37394 | 16 | No | Cannot create index on view '%.\*ls' because it is a ledger view. The indexes should be created on the ledger table or its history table. |
| 37395 | 16 | No | View '%.\*ls' is not updatable because it is a ledger view. |
| 37396 | 16 | No | Query cannot be executed, access token is expired. Please login with a new access token and execute query again. |
| 37397 | 16 | No | Error storing Babylon policy for DBID %d. Major error: %d, Minor error: %d, State: %d |
| 37398 | 21 | No | Fatal error: Unable to pull policies for %d consecutive attempts. |
| 37399 | 16 | No | TYPE, SID, Default Database, Default Language, Password option not allowed for External logins |
| 37401 | 16 | Yes | Error refreshing the external access control policy from metadata. Error code %d. |
| 37402 | 16 | No | Setting LEDGER to ON failed because ledger view '%.\*ls' is not specified in two-part name format. |
| 37403 | 16 | No | Error in parsing Babylon json policy element/event with error message %s. |
| 37404 | 16 | Yes | Service returned an empty response. URL: '%ls'. |
| 37405 | 16 | Yes | Exception in fetching the external policy. URL: '%ls'. Major error code: %d, Minor error code: %d, State: %d |
| 37406 | 16 | Yes | Error occurred while obtaining an authentication token for external service %ls; no authentication method is configured. |
| 37407 | 16 | No | Error in Babylon SDK with error message %s. |
| 37408 | 16 | No | The ledger digest storage cannot be configured on secondary databases. |
| 37409 | 16 | No | The provided Ledger storage target type '%s' is invalid. |
| 37410 | 16 | No | The provided ledger storage path '%s' is invalid. |
| 37411 | 16 | No | Uploading ledger digests is being enabled or disabled for database '%s'. Please wait for the previous request to complete. |
| 37412 | 16 | Yes | %ls operation failed for %ls. Error code: %d. |
| 37413 | 16 | Yes | Failed to generate a digest in the database with ID %d due to error %d. Check the errorlog for more information. |
| 37414 | 16 | No | Failed to retrieve digest information from path '%ls' due to error %d. Verify that the provided path exists and the required permissions have been granted to the SQL service. |
| 37415 | 16 | No | Invalid path specified for a ledger digest URL. Verify that the provided paths are valid. |
| 37416 | 16 | No | Fulltext indexes are not allowed on ledger tables. |
| 37417 | 16 | No | Uploading ledger digest failed for database with id %d due to error %d. |
| 37418 | 16 | No | No digests were found based on the input locations and block IDs. |
| 37419 | 16 | No | Unable to %S_MSG audit because %S_MSG contains question mark. |
| 37420 | 16 | No | LEDGER = OFF cannot be specified for tables in databases that were created with LEDGER = ON. |
| 37421 | 16 | Yes | Error occurred while reading external policy endpoint. |
| 37422 | 16 | Yes | Error occurred while reading STS Url. |
| 37423 | 16 | Yes | Error occurred while fetching the external access policies. Please check if Managed System Identity is enabled on the server. |
| 37424 | 16 | Yes | Error fetching the external policy resource endpoint. |
| 37425 | 16 | No | Error occurred while trying to record the ledger table history operation of the ledger table '%.\*ls'. |
| 37426 | 16 | No | Error occurred while trying to record the ledger column history operation of the column '%.\*ls' in ledger table '%.\*ls'. |
| 37427 | 16 | No | Cannot alter, drop, or rename object '%.\*ls' because it is a ledger dropped object. |
| 37428 | 16 | No | Cannot rename ledger object '%.\*ls' because it is not a supported operation. |
| 37429 | 16 | No | Cannot rename column '%.\*ls' of ledger object '%.\*ls' because it is not a supported operation. |
| 37430 | 16 | No | Cannot alter schema of dropped ledger object '%.\*ls' because it is not a supported operation. |
| 37431 | 16 | No | Encountered failure while initializing Confidential Ledger Adapter with URL %ls and error code %ld. |
| 37432 | 16 | No | Encountered failure while initializing Confidential Ledger Data Iterator with URL %ls and error code %ld. |
| 37433 | 16 | Yes | %ls. The attestation URL specified by the client is not reachable. URL: '%ls'. Return code: '0x%08x'. Check your networking configuration. |
| 37434 | 16 | No | Invalid input for parameter '@locations' provided for ledger verification. The value should be a valid JSON document that contains values for the 'path', 'last_digest_block_id' and 'is_current' of each location. |
| 37435 | 16 | No | Invalid input for parameter '@table_name' provided for ledger verification. The specified value cannot contain a server or database name and must point to an existing ledger table. |
| 37436 | 16 | Yes | Ledger verification failed due to error %d. |
| 37437 | 16 | No | Uploading ledger digests is currently not supported for this resource type. |
| 37438 | 16 | No | Ledger verification for table '%.\*ls' failed because its clustered index is disabled. |
| 37439 | 16 | No | The statement failed because a nonclustered index cannot be created on table '%.\*ls' that is a ledger table and has a clustered columnstore index. |
| 37440 | 16 | No | Cannot update dropped ledger table '%.\*ls'. |
| 37441 | 16 | No | The statement failed because a clustered columnstore index cannot be created on table '%.\*ls' that is a ledger table and has a nonclustered index. |
| 37442 | 16 | No | Ledger verification was aborted by the user. |
| 37443 | 16 | No | The use of replication is not supported with ledger table '%s' |
| 37444 | 16 | No | Enabling Change Data Capture for ledger history table '%ls' is not allowed. |
| 37445 | 16 | No | The specified digestStorageEndpoint is invalid. It must be an Azure blob storage or Azure Confidential Ledger endpoint. |
| 37446 | 16 | No | Changing the ledger property is not supported for this resource type. |
| 37447 | 16 | No | The certificate '%.\*ls' has been already added as a trusted issuer for DNS name '%.\*ls' |
| 37448 | 16 | No | The certificate '%.\*ls' cannot be removed from the trusted issuers for the DNS name '%.\*ls' because it hasn't been added as a trusted issuer for the DNS name '%.\*ls'. |
| 37449 | 10 | No | Warning: The certificate chain of the certificate specified could not be validated at this time. This may cause issues with the certificate-based authentication in the future. Please check your organization's policy and settings for online and/or offline validation of a certificate chain. |
| 37450 | 16 | No | The certificate '%.\*ls' cannot be added as a trusted issuer for a DNS name because it is not allowed to sign certificates |
| 37451 | 16 | No | DNS name provided exceeds maximum length of %d characters. |
| 37452 | 16 | No | Error while trying to invoke external policy pull task. |
| 37453 | 16 | No | '%ls' is not a valid option for the @type parameter. Enter 'update' or 'reload'. |
| 37454 | 10 | No | Encountered Internal Error while calling %ls. Error code %ld, State %ld. |
| 37455 | 16 | No | Server identity does not have permissions to access MS Graph. Please follow the steps here: https://aka.ms/SQLServer-AAD-Permissions. |
| 37457 | 16 | No | DDL statement executed on the database is not allowed because Azure Active Directory only authentication is enabled on the server. |
| 37458 | 16 | No | Posted digest is not globally committed in Azure Confidential Ledger. Ledger URL '%ls' and error code %ld. |
| 37459 | 16 | No | Encountered Internal Error while calling Azure Confidential Ledger. Ledger URL '%ls' and error code %ld. |
| 37460 | 16 | No | Failed to parse response from Identity Service. Ledger URL '%ls' and error code %ld. |
| 37461 | 16 | No | Encountered error while trying to retrieve AAD Token to call Azure Confidential Ledger. Ledger URL '%ls' and error code %ld. |
| 37462 | 16 | No | Encountered error while trying to retrieve Network Certificate from Identity Service. Ledger URL '%ls' and error code %ld. |
| 37463 | 16 | No | Service Principal or Managed Identity is not authorized to call Azure Confidential Ledger. Ledger URL '%ls' and error code %ld. |
| 37464 | 10 | Yes | AAD Authentication is enabled. This is an informational message only; no user action is required. |
| 37465 | 16 | No | Server identity does not have the required permissions to access the MS graph. Please follow the steps here : https://aka.ms/UMI-AzureSQL-permissions |
| 37466 | 16 | No | Endpoint is required. |
| 37467 | 20 | No | The server encountered an unexpected exception. |
| 37468 | 16 | No | The source and target table names provided as parameters to 'sp_copy_data_in_batches' must be valid table names and cannot be null or empty. |
| 37469 | 16 | No | The source and target table names provided as parameters to 'sp_copy_data_in_batches' cannot contain a database or server name. |
| 37470 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' must be user tables. |
| 37471 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' cannot be memory optimized tables. |
| 37472 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot be the source of merge or transactional replication or have change tracking or change data capture enabled. |
| 37473 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot have an XML, spatial or fulltext index. |
| 37474 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot have a disabled clustered index. |
| 37475 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot have any non-clustered indexes or statistics other than the ones for the clustered index. |
| 37476 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' cannot have a column set column. |
| 37477 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' cannot have any security policies. |
| 37478 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' cannot be graph node or edge tables. |
| 37479 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot have any indexed views referencing it. |
| 37480 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' must be empty. |
| 37481 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot have any INSERT trigger. |
| 37482 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' must be using the same partition function and partitioning columns. |
| 37483 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' must have the same number of columns when excluding the TRANSACTION ID and SEQUENCE NUMBER generated always columns. |
| 37484 | 16 | No | 'sp_copy_data_in_batches' failed because column '%.\*ls' at ordinal %d in table '%.\*ls' has a different name than the column '%.\*ls' at the same ordinal in table '%.\*ls' (excluding the TRANSACTION ID and SEQUENCE NUMBER generated always columns). |
| 37485 | 16 | No | 'sp_copy_data_in_batches' failed because column '%.\*ls' has data type %s in source table '%.\*ls' which is different from its type %s in target table '%.\*ls'. |
| 37486 | 16 | No | 'sp_copy_data_in_batches' failed because column '%.\*ls' does not have the same collation, nullability, sparse, ANSI_PADDING, vardecimal, identity or generated always attribute, CLR type or schema collection in tables '%.\*ls' and '%.\*ls'. |
| 37487 | 16 | No | 'sp_copy_data_in_batches' failed because column '%.\*ls' in table '%.\*ls' is computed column but the same column in '%.\*ls' is not computed or they do not have the same persistent attribute . |
| 37488 | 16 | No | The source and target tables provided to 'sp_copy_data_in_batches' cannot contain columns of data type text, ntext, image or with the ROWGUIDCOL or FILESTREAM attributes. |
| 37489 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot have any RULE constraints. |
| 37490 | 16 | No | 'sp_copy_data_in_batches' failed because computed column '%.\*ls' defined as '%.\*ls' in table '%.\*ls' is different from the same column in table '%.\*ls' defined as '%.\*ls'. |
| 37491 | 16 | No | The target table '%.\*ls' provided to 'sp_copy_data_in_batches' cannot have clustered columnstore index. |
| 37492 | 16 | No | The source and target tables provided as parameters to 'sp_copy_data_in_batches' cannot be temporary tables. |
| 37493 | 16 | Yes | Internal enclave error: Cannot initialize the %ls enclave. Load method: %ls. Last Error: 0x%08x. For more information, contact Customer Support Services. |
| 37494 | 16 | Yes | Authentication bearer service failed to return a valid challenge. Verify URL '%ls' is correct and reachable. Return code: '0x%08x'. Please check network connectivity and firewall setup, then retry the operation |
| 37495 | 10 | Yes | Ledger verification to detect inconsistencies for index '%.\*ls' (database ID %d) failed due to exception %d, state %d. |
| 37496 | 10 | Yes | Ledger verification detected inconsistencies for index '%.\*ls' (database ID %d). There are %ld rows that are inconsistent between the table and the index. |
| 37497 | 16 | Yes | Internal parsing error: Fail to parse the service URL '%ls'. Failure method: %ls. Return code: '0x%08x'. For more information, contact Customer Support Services. |
| 37498 | 16 | No | Snapshot isolation must be enabled on the database when sp_verify_database_ledger is executed. |
| 37499 | 10 | Yes | Ledger verification detected an inconsistency in the definition of ledger view '%.\*ls' for the ledger table '%.\*ls'. |
| 37501 | 16 | No | A value cannot be given for the column '%.\*ls' on ledger table '%.\*ls' because it is a dropped ledger column. |
| 37502 | 16 | No | The column '%.\*ls' on ledger table '%.\*ls' cannot be dropped because it is a TRANSACTION_ID or SEQUENCE_NUMBER GENERATED ALWAYS column. |
| 37503 | 16 | No | The column '%.\*ls' on ledger table '%.\*ls' cannot be dropped because it is an identity or computed column. |
| 37504 | 10 | No | Failed to convert string to guid when fetching tenantID fabric property %.\*ls' |
| 37505 | 20 | No | A secondary of secondary database (chaining) cannot be created because automatic upload of ledger digests has been configured for this database. |
| 37506 | 20 | No | Automatic upload of ledger digests cannot be enabled because this database is configured with secondaries of secondaries (chaining). |
| 37507 | 16 | No | CertPFX using AES encryption is not supported on the current operating system. |
| 37508 | 16 | No | Unable to locate PFXExportCertStoreEx API in crypt32.dll, needed for CertPFX using AES encryption ErrorCode: %d. |
| 37509 | 16 | No | Unable to locate PFXExportCertStoreEx API in securityapi.dll, needed for CertPFX using AES encryption ErrorCode: %d. |
| 37510 | 16 | No | Unable to determine the blob length needed to export the PFX certificate. ErrorCode: %d. |
| 37511 | 16 | No | Unable to export the PFX certificate. ErrorCode: %d. |
| 37512 | 16 | No | The column '%.\*ls' on ledger table '%.\*ls' is a sequence number generated always column and cannot be referenced by computed columns, check constraints, filtered index or statistics expressions. |
| 37513 | 16 | No | Cannot create %S_MSG on view '%.\*ls'. It contains references to sequence number generated always columns. |
| 37514 | 16 | No | Cannot bind a rule to a temporal or ledger history table. |
| 37515 | 16 | No | Cannot bind a rule to a sequence number generated always column. |
| 37516 | 16 | No | Server response exceeded the buffer size. Ledger URL '%ls' and error code %ld. |
| 37517 | 16 | Yes | Internal error occurred while obtaining ARC resource information from IMDS endpoint. Substate: '%ls', status: 0x%08x. |
| 37518 | 16 | Yes | Internal error occurred while obtaining ARC resource information from IMDS endpoint. Error code: '%ls'. Error message: '%ls'. Return code: '0x%08x'. |
| 37519 | 16 | Yes | Internal error occurred while obtaining ARC resource information from IMDS endpoint. Return code: '0x%08x'. |
| 37520 | 16 | Yes | Error retrieving IMDS endpoint. |
| 37521 | 16 | No | Geo-replication cannot be stopped because the database has Ledger Digest Uploads enabled. Disable Ledger Digest Uploads and retry the operation. |
| 37522 | 16 | No | The Geo-primary database cannot be dropped because the database has Ledger Digest Uploads enabled. Disable Ledger Digest Uploads and retry the operation. |
| 37523 | 16 | No | The Azure Confidential Ledger server identity check failed. Ledger URL '%ls' and error code %ld. |
| 37524 | 10 | Yes | IMDS resource information. Subscription ID: %ls, Resource Group: %ls, Name: %ls. |
| 37525 | 10 | Yes | Command '%.\*ls' is not supported as Azure Active Directory is not configured for this instance. |
| 37526 | 16 | No | A ledger table cannot be created while database mirroring is enabled. |
| 37527 | 16 | Yes | Database '%.\*ls'cannot be started as a primary replica because it might be missing ledger transactions for which digests have already been generated. Failover back to the original primary or restore more log from the primary to apply the latest transactions. |
| 37528 | 16 | No | Vardecimal cannot be enabled on table '%.\*ls' because it is a ledger table or a ledger history table. |
| 37529 | 16 | No | Bound transactions are not supported with ledger tables. |
| 37530 | 16 | No | Failed to set the ledger digest storage endpoint to '%ls' due to error %d. Verify that the provided Azure Storage account exists, it contains a container named '%ls' and is accessible from the machine hosting this instance of SQL Server. Confirm that there is a credential object with a valid Shared Access Signature for this container. |
| 37531 | 16 | No | Failed to set the ledger digest storage endpoint to '%ls'. Verify that you have created a credential object to provide SQL Server access to the '%ls' container in this Azure Storage account. |
| 37532 | 16 | No | Ledger table '%.\*ls' cannot contain check constraints, filtered indexes and statistics or computed columns that reference columns using a multi-part identifier. |
| 37533 | 16 | No | Computed column '%.\*ls' cannot be added to ledger table '%.\*ls'. Ledger tables do not support computed columns that use CLR functions or types. |
| 37534 | 16 | No | Error while trying to read database information from master metadata. Transaction is not active anymore. |
| 37535 | 16 | No | Principal name with object id '%ls' must begin with original principal name followed by a user-defined suffix to differentiate between the names. (https://aka.ms/AADUserNonUniqueDisplayName) |
| 37536 | 16 | Yes | Internal error occurred while obtaining version information from IMDS endpoint. Substate: '%ls', status: 0x%08x. |
| 37537 | 16 | Yes | Internal error occurred while obtaining version information from IMDS endpoint. Error code: '%ls'. Error message: '%ls'. Return code: '0x%08x'. |
| 37538 | 16 | Yes | Internal error occurred while obtaining version information from IMDS endpoint. Return code: '0x%08x'. |
| 37539 | 16 | No | Uploading ledger digest failed for database with id %d due to outbound firewall rules. |
| 37540 | 16 | No | The provided ledger digest storage location is not in the list of Outbound Firewall Rules on the server. Please add this to the list of Outbound Firewall Rules on your server and retry the operation. |
| 37541 | 16 | No | Provider '%ls' is no longer supported. Please use MSOLEDBSQL provider instead. (https://go.microsoft.com/fwlink/?linkid=2206241&clcid=0x409) |
| 37542 | 16 | Yes | The operation could not be completed because attempts to connect to Azure Key Vault (AKV) were denied by the configured Network Security Perimeter. Key vault uri: '%ls'. |
| 37543 | 10 | No | The trust of the certificate imported could not be verified with the Certificate Authority (CA) or verified through the Internet. Most likely the host operating system where SQL Server is hosted is missing the latest root certificate updates, or access to the Internet. In case this certificate is used for authentication when partner server rotates the certificate, the authentication will fail unless the issue has been resolved on the host OS. Please ensure that the host OS has the latest root certificate updates, or access to the Internet to reach out to remote CA. Error code: 0x%lx. |
| 37545 | 16 | No | '%ls' is not a valid object id for '%ls' or you do not have permission. |
| 37546 | 16 | No | Can only specify object_id when creating user from external provider. |
| 37549 | 16 | No | Cannot open session for %S_MSG '%.\*ls'. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37550 | 16 | No | Cannot initialize cryptographic provider. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37551 | 16 | No | Cannot create key '%.\*ls' in the provider. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37552 | 16 | No | Cannot export %S_MSG from the provider. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37553 | 16 | No | Invalid algorithm '%.\*ls'. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37554 | 16 | No | Key with %S_MSG '%.\*ls' does not exist in the provider or access is denied. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37555 | 16 | No | Invalid algorithm id: %d. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37556 | 16 | No | Key validation failed since an attempt to get algorithm info for that key failed. Provider error code: %d. (%.\*ls). For more information, see https://aka.ms/sql-ekm-connector-troubleshooting |
| 37559 | 16 | No | Object_id is not a valid option |
| 38001 | 16 | No | Cannot find the file id %d in the database '%s'. |
| 38002 | 16 | No | Only users having %s permission can execute this stored procedure. |
| 39001 | 16 | No | Only SELECT statement is supported for input data query to 'sp_execute_external_script' stored procedure. |
| 39002 | 16 | No | SQL failed to boot extensibility for error code 0x%lx. |
| 39003 | 10 | No | SQL successfully boots extensibility. |
| 39004 | 16 | No | A '%.\*s' script error occurred during execution of 'sp_execute_external_script' with HRESULT 0x%x. External script request id is %ls. |
| 39005 | 10 | No | STDOUT message(s) from external script: %.\*ls%.\*ls |
| 39006 | 10 | No | External script execution status: %.\*ls. |
| 39007 | 16 | No | The specified language '%.\*ls' does not exist or you do not have permission. |
| 39008 | 16 | No | Invlid Parameter name '%ls' specified for Procedure. This clashes with internal parameters. |
| 39009 | 16 | No | Output parameter in external script execution is not yet supported. |
| 39010 | 16 | No | External script execution for '%.\*s' script encountered an unexpected error (HRESULT = 0x%x) for request id: %ls. |
| 39011 | 16 | No | SQL Server was unable to communicate with the LaunchPad service for request id: %ls. Please verify the configuration of the service. |
| 39012 | 16 | No | Unable to communicate with the runtime for '%.\*s' script for request id: %ls. Please check the requirements of '%.\*s' runtime. |
| 39013 | 16 | No | SQL Server encountered error 0x%x while communicating with the '%.\*s' runtime for request id: %ls. Please check the configuration of the '%.\*s' runtime. |
| 39014 | 16 | No | Parallelism in external script execution is not yet supported. |
| 39015 | 16 | No | SELECT INTO statement is not supported for input data query to 'sp_execute_external_script' stored procedure. |
| 39016 | 16 | No | The parameterized external script expects the parameter '%.\*ls', which was not supplied. |
| 39017 | 16 | No | Input data query returns column #%d of type '%ls' which is not supported by the runtime for '%.\*s' script. Unsupported types are binary, varbinary, timestamp, datetime2, datetimeoffset, time, text, ntext, image, hierarchyid, xml, sql_variant and user-defined type. External script request id is %ls. |
| 39018 | 16 | No | Parameter '%.\*ls' uses a data type that is not supported by the runtime for '%.\*s' script. Unsupported types are timestamp, datetime2, datetimeoffset, time, text, ntext, image, hierarchyid, xml, sql_variant and user-defined type. External script request id is %ls. |
| 39019 | 10 | No | An external script error occurred: %.\*ls%.\*ls |
| 39020 | 16 | No | Feature 'Advanced Analytics Extensions' is not installed. Please consult Books Online for more information on this feature. |
| 39021 | 16 | No | Unable to launch runtime for '%.\*s' script for request id: %ls. Please check the configuration of the '%.\*s' runtime. See '[https://learn.microsoft.com/sql/machine-learning/install/sql-machine-learning-services-windows-install-sql-2022](../../../machine-learning/install/sql-machine-learning-services-windows-install-sql-2022.md)' for setup instructions. |
| 39022 | 10 | No | STDERR message(s) from external script: %.\*ls%.\*ls |
| 39023 | 16 | No | 'sp_execute_external_script' is disabled on this instance of SQL Server. Use sp_configure 'external scripts enabled' to enable it. |
| 39024 | 16 | No | Parallel execution of 'sp_execute_external_script' failed. Specify WITH RESULT SETS clause with output schema. |
| 39025 | 16 | No | External script execution failed as extensibility environment is not ready yet. Retry the operation when the server is fully started. |
| 39026 | 16 | No | The parameter name 'r_rowsPerRead' is specified multiple times in 'sp_execute_external_script' call. The name 'r_rowsPerRead' is reserved for specifying streaming behavior only. External script request id is %ls. |
| 39027 | 16 | No | Parameter '%.\*ls' was specified multiple times to sp_execute_external_script stored procedure. |
| 39029 | 16 | No | The external language '%.\*ls' returned error messages: '%.\*ls'. |
| 39030 | 16 | No | Variable '%.\*ls' has unsupported data type for parameter exchange in EXEC EXTERNAL LANGUAGE statement. |
| 39031 | 16 | No | Cannot parse the output schema of the builtin function '%ls'. |
| 39032 | 16 | No | The function %ls expects parameters in the form of 'name = value'. |
| 39033 | 16 | No | The parameter name '%.\*ls' has already been declared. Parameter names must be unique in %ls function call. |
| 39034 | 16 | No | The parameter '%.\*ls' in %ls contains a definition that doesn't match the supplied arguments. |
| 39035 | 16 | No | The function %ls has too many arguments supplied. |
| 39036 | 16 | No | The function %ls expects parameter '%.\*ls' which was not supplied. |
| 39037 | 16 | No | The function %ls contains a parameter '%.\*ls' that has an invalid type. |
| 39038 | 16 | No | The function %ls expects parameter '%.\*ls' of type ntext/nchar/nvarchar. |
| 39039 | 16 | No | Error converting the parameter value for '%.\*ls' to '%.\*ls'. |
| 39040 | 16 | No | The function '%ls' does not support SQL identifier or variable for '%.\*ls'. |
| 39041 | 16 | No | The parameter '%.\*ls' has an invalid definition. |
| 39042 | 16 | No | %s EXTERNAL LIBRARY failed because the library source parameter %d is not a valid expression. |
| 39043 | 16 | No | %s EXTERNAL LIBRARY failed because filename '%.\*ls' is too long. |
| 39044 | 16 | No | %s EXTERNAL LIBRARY failed because it could not open the physical file '%.\*ls': %ls. |
| 39045 | 16 | No | %s EXTERNAL LIBRARY failed because it could not read from the physical file '%.\*ls': %ls. |
| 39046 | 16 | No | CREATE EXTERNAL LIBRARY failed because the user "%.\*ls" specified in the authorization clause does not exist. |
| 39047 | 16 | No | External library '%.\*ls' already exists for owner '%.\*ls' in database '%.\*ls'. |
| 39048 | 16 | No | Failed to %s external library '%ls': %ls. |
| 39049 | 10 | No | Message(s) from 'PREDICT' engine: %.\*ls%.\*ls |
| 39050 | 16 | No | Error occurred during execution of the builtin function 'PREDICT' with HRESULT 0x%x. Out of memory. |
| 39051 | 16 | No | Error occurred during execution of the builtin function 'PREDICT' with HRESULT 0x%x. Model is corrupt or invalid. |
| 39052 | 16 | No | Error occurred during execution of the builtin function 'PREDICT' with HRESULT 0x%x. Model type is unsupported. |
| 39053 | 16 | No | The function PREDICT expects parameter 'RUNTIME' of type ntext/nchar/nvarchar. |
| 39054 | 16 | No | The 'RUNTIME' parameter is invalid. |
| 39055 | 16 | No | %ls can only be called in combination with the OUTER APPLY clause. |
| 39056 | 16 | No | Could not parse the list of columns to be passed to %ls. Only simple column references are accepted. |
| 39057 | 16 | No | The value provided for the '%.\*ls' parameter is too large. |
| 39058 | 16 | No | The parameter '%.\*ls' has a type that is not supported. |
| 39092 | 16 | No | Initialization of native scoring libraries failed with HRESULT 0x%x. |
| 39093 | 16 | No | 'PREDICT' function does not take parameters of varchar(max), nvarchar(max) or varbinary(max) type except for 'MODEL' parameter. |
| 39094 | 16 | No | 'PREDICT' function only supports models smaller than 100 MB. |
| 39096 | 16 | No | Execution failed because its WITH clause specified different output columns from what 'PREDICT' function tries to return. The schema returned by 'PREDICT' function is '%ls'. |
| 39097 | 16 | No | Input data column #%d is of type '%ls' which is not supported by '%ls' function. Unsupported types are binary, varbinary, timestamp, datetime2, datetimeoffset, time, text, ntext, image, hierarchyid, xml, sql_variant and user-defined type. |
| 39098 | 16 | No | Error occurred during execution of the builtin function 'PREDICT' with HRESULT 0x%x. |
| 39099 | 16 | No | Feature or option 'PREDICT' is not yet implemented. Please consult Books Online for more information on this feature or option. |
| 39101 | 16 | No | '%.\*ls' failed because it is not supported in the edition of this SQL Server instance. |
| 39102 | 16 | No | Duplicate column names are not allowed in '%.\*ls'. Column name '%.\*ls' is a duplicate. |
| 39103 | 16 | No | Parameter '@input_data_1_order_by_columns' is not allowed without parameter '@input_data_1_partition_by_columns'. |
| 39104 | 16 | No | Column '%.\*ls' in '%.\*ls' is not defined in the SELECT clause of '@input_data_1' parameter. |
| 39105 | 16 | No | Invalid syntax for parameter '@input_data_1_partition_by_columns'. Specify a list of comma separated columns. |
| 39106 | 16 | No | Invalid syntax for parameter '@input_data_1_order_by_columns'. Specify a list of comma separated columns and an optional argument for sorting order \<ASC\|DESC\>. |
| 39107 | 16 | No | Columns in parameters '@input_data_1_partition_by_columns' and '@input_data_1_order_by_columns' must be unique. Column name '%.\*ls' appears in both parameters. |
| 39108 | 16 | No | Maximum number of concurrent external script users has been reached. Limit is %d. Please retry the operation. External script request id is %ls. |
| 39109 | 16 | No | The combined total number of columns given in parameters '@input_data_1_partition_by_columns' and '@input_data_1_order_by_columns' exceeds the maximum %d. |
| 39110 | 16 | No | Maximum number of concurrent external script queries for this user has been reached. Limit is %d. Please retry the operation. External script request id is %ls. |
| 39111 | 16 | No | The SQL Server Machine Learning Services End-User License Agreement (EULA) has not been accepted. |
| 39112 | 15 | No | Duplicate file specification supplied for platform '%.\*ls'. |
| 39113 | 15 | No | Number of file specifications exceeds the maximum of %d. |
| 39114 | 15 | No | CREATE EXTERNAL LIBRARY statement failed because of duplicate file specification. Only one file specification per platform is allowed in a CREATE EXTERNAL LIBRARY statement. |
| 39115 | 16 | No | ALTER EXTERNAL LIBRARY statement failed because the content for platform '%.\*ls' does not exist or is not correctly defined in the external library. |
| 39116 | 16 | No | ALTER EXTERNAL LIBRARY REMOVE PLATFORM statement failed because an external library requires at least one file specification to be defined. |
| 39117 | 16 | No | %.\*ls EXTERNAL LANGUAGE statement failed because the parameter '%s' is not a valid expression. |
| 39118 | 16 | No | %s EXTERNAL LANGUAGE statement failed because the specified file path '%.\*ls' is too long. Maximum length of file path is %d characters. |
| 39119 | 16 | No | %s EXTERNAL LANGUAGE failed because it could not open the physical file '%.\*ls': %ls. |
| 39120 | 16 | No | %s EXTERNAL LANGUAGE statement failed because it could not read from the physical file '%.\*ls': %ls. |
| 39121 | 16 | No | CREATE EXTERNAL LANGUAGE statement failed because the user "%.\*ls" specified in the authorization clause does not exist or have permission. |
| 39122 | 16 | No | CREATE statement failed. External language '%.\*ls' already exists. |
| 39123 | 16 | No | %.\*ls EXTERNAL LANGUAGE statement failed because the parameter '%s' is not specified. |
| 39124 | 16 | No | '%.\*ls' platform information doesn't exist for the language '%.\*ls'. |
| 39125 | 16 | No | ALTER EXTERNAL LANGUAGE statement failed because of duplicate file specification. Only one file specification per platform is allowed. |
| 39126 | 16 | No | ALTER EXTERNAL LANGUAGE REMOVE PLATFORM statement failed because an external language requires at least one file specification to be defined. |
| 39127 | 16 | No | External script execution for '%.\*s' script ran out of resources. External script request id is: %s. |
| 39128 | 16 | No | External language runtime for '%s' could not be provisioned. Error code 0x%08x. |
| 39129 | 16 | No | Cannot drop external language '%.\*ls' because it is being referenced by external library '%.\*ls'. |
| 39130 | 16 | No | %.\*ls statement failed. Language '%s' already exists. |
| 39131 | 16 | No | %.\*ls statement failed. System language '%s' can't be altered or removed. |
| 39132 | 16 | No | The parameter '@r_rowsPerRead' is invalid. Parameter '@r_rowsPerRead' must be a positive integer. |
| 39133 | 16 | No | CREATE/ALTER EXTERNAL LANGUAGE statement failed. The environment variables string is invalid. |
| 39134 | 16 | No | The procedure '%s' cannot be executed because it is not supported in Azure SQL Edge. |
| 39135 | 16 | No | Service environment '%s' is not ready. |
| 39136 | 10 | No | Error sending a message to the service. |
| 39137 | 10 | No | Error initialization external services framework. |
| 39138 | 16 | No | Request to drop streaming job '%s' failed because the job is currently running. Stop the job before dropping. |
| 39139 | 16 | No | Request to stop streaming job '%s' failed because the job is not currently running. |
| 39140 | 16 | No | Request to create streaming job '%s' failed. The Streaming job query cannot be null or empty. |
| 39141 | 16 | No | External data source '%s' with location prefix '%s' is not supported as input for streaming. |
| 39142 | 16 | No | External data source '%s' with location prefix '%s' is not supported as output for streaming. |
| 39143 | 16 | No | External data source '%s' with location prefix '%s' is not supported for streaming. |
| 39144 | 16 | No | External data source '%s' has an invalid location for streaming. |
| 39145 | 16 | No | External stream '%s' has an invalid location. |
| 39146 | 16 | No | External stream '%s' has invalid properties. |
| 39147 | 16 | No | Kafka datasource '%s' should specify a value for PARTITIONS in the properties field. |
| 39148 | 16 | No | External file format '%s' type is not supported for streaming. |
| 39149 | 16 | No | External file format '%s' encoding is not supported for streaming. |
| 39150 | 16 | No | Error while processing streaming job: '%s'. |
| 39151 | 16 | No | Transient error communicating with the streaming runtime. Please retry the operation. |
| 39152 | 16 | No | Transient error communicating with the streaming runtime due to unfinished stop streaming job operation. Please retry the operation. |
| 39153 | 16 | No | Error while launching mssql-flight-server with HRESULT 0x%x |
| 39154 | 16 | No | Error while stopping mssql-flight-server with HRESULT 0x%x |
| 40000 | 16 | No | Replicated tables support only local (non-DTC) two-phase commit involving the master database. |
| 40001 | 16 | No | Secondary kill was initiated during commit. |
| 40002 | 16 | No | Replicated row is not found. |
| 40003 | 16 | No | Unexpected operation in replicated message. |
| 40004 | 16 | No | Column count does not match. |
| 40005 | 16 | No | Duplicated transaction id. |
| 40006 | 16 | No | Unknown transaction id. |
| 40007 | 16 | No | Invalid nesting level. |
| 40008 | 16 | No | Replication target database is not found. |
| 40009 | 16 | No | Transaction state locally does not match the expected state. |
| 40010 | 16 | No | Replicated transactions across databases are not allowed. |
| 40011 | 16 | No | Replicated target table %ld is not found. |
| 40012 | 16 | No | Replicated target index %ld on table %ld is not found. |
| 40013 | 16 | No | Replicated target schema %.\*ls is not found. |
| 40014 | 16 | No | Multiple databases can not be used in the same transaction. |
| 40015 | 16 | No | This functionality is not supported on replicated tables. |
| 40016 | 16 | No | The partitioning key column '%.\*ls' must be one of the keys of '%.\*ls.%.\*ls.%.\*ls' index. |
| 40017 | 16 | No | Partition key can not be changed. |
| 40018 | 16 | No | Partition key value is outside of the valid partition key range. |
| 40019 | 16 | No | The partition key column '%.\*ls' of table '%.\*ls.%.\*ls' is nullable or does not match the partition key type defined in the table group. |
| 40020 | 16 | No | The database is in transition and transactions are being terminated. |
| 40021 | 16 | No | The low and high keys specified for the partition are invalid. Low must be less than high. |
| 40022 | 16 | No | A partition with overlapping key ranges already exists. |
| 40023 | 16 | No | The name %s is too long. |
| 40024 | 16 | No | The last committed CSN (%d, %I64d) was not found in the log. The last seen CSN was (%d, %I64d). |
| 40025 | 16 | No | The transaction was aborted during commit due to a database state transition. |
| 40028 | 16 | No | The tablegroup name '%.\*ls.%.\*ls' is not valid. |
| 40029 | 16 | No | Replicated tables can have at most %d columns. |
| 40030 | 16 | No | Can not perform replica operation because the replica does not exist in local partition map. |
| 40031 | 16 | No | The partition key column for table '%.\*ls.%.\*ls' is undefined. |
| 40032 | 16 | No | Unsupported use of LOB in online index build. |
| 40033 | 16 | No | Attempted CSN epoch switch is not allowed. The new CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40034 | 16 | No | CSN being added must be equal last CSN+1. The new CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40035 | 16 | No | CSN being added must be equal or greater than the last CSN. The new CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40036 | 16 | No | Can not perform replica operation because this node is not the secondary for this partition. |
| 40037 | 16 | No | The epoch being started must not have been used. The new CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40038 | 16 | No | Can not get ack to roll back replication message. |
| 40039 | 16 | No | Can not get ack to commit replication message. |
| 40040 | 16 | No | Failed to initiate VDI Client for physical seeding. |
| 40041 | 16 | No | Corrupted column status. |
| 40042 | 16 | No | Corrupted column length. |
| 40043 | 16 | No | Corrupted variable data. Actual remaining bytes is %d, expected %d bytes. |
| 40044 | 16 | No | Corrupted fixed size data. Actual remaining bytes %d, expected %d bytes. |
| 40045 | 16 | No | Message version mismatch. Actual version is %d and the expected is %d. |
| 40046 | 16 | No | The minimum required message version %d for message type %d is unsupported. |
| 40047 | 16 | No | Invalid use of parent transaction. |
| 40048 | 16 | No | Corrupted fragmented row flow sequence. |
| 40049 | 16 | No | Corrupted fragmented row. |
| 40050 | 16 | No | Corrupted LOB row. |
| 40051 | 16 | No | Use of UPDATETEXT on replicated tables is not supported. |
| 40052 | 16 | No | Parallel queries are not supported on replicated tables. |
| 40053 | 16 | No | Attempt to replicate a non-replicated system table %ld. |
| 40054 | 16 | No | Tables without a clustered index are not supported in this version of SQL Server. Please create a clustered index and try again. |
| 40056 | 16 | No | Master, tempdb, model and mssqlsystemresource databases can not be replicated. |
| 40057 | 16 | No | Table is not enabled for replication. |
| 40058 | 16 | No | Unsupported replicated table usage option. Refer to the state to identify the cause. |
| 40060 | 16 | No | Attempt to replicate out of partition already locked for internal use. |
| 40061 | 16 | No | Unknown rowset id. |
| 40062 | 16 | No | Incorrect replica role transition. |
| 40063 | 16 | No | Replica is not found. |
| 40064 | 16 | No | Attempt to add a CSN to an invalid CSN vector. The new CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40065 | 16 | No | CSN vector can be reinitialized only with initial or invalid CSN. The new CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40066 | 16 | No | Transport destination is not found. |
| 40067 | 16 | No | Corrupted row sequence. |
| 40068 | 16 | No | Idempotent mode has been used on an unknown transaction. |
| 40069 | 16 | No | Could not obtain rowset interface. |
| 40070 | 16 | No | CSN mismatch detected. The local CSN is (%d,%I64d), the remote CSN is (%d,%I64d). |
| 40071 | 16 | No | This partition does not have enough valid secondaries to start a DML transaction. The needed count is %ld, the current counts are %ld (main quorum) and %ld (transient quorum). |
| 40072 | 16 | No | Corrupted rowset metadata sequence. |
| 40073 | 16 | No | Partitioned tables are not supported. |
| 40074 | 16 | No | Partition key is not found is the target rowset or is nullable or not part of index keys. |
| 40075 | 16 | No | Column schema mismatch for rowset %ls.%ls.%ls column %ld. |
| 40076 | 16 | No | Too few columns from remote rowset %ls.%ls.%ls. |
| 40077 | 16 | No | Remote rowset %ls.%ls.%ls column %ld is not found locally. |
| 40078 | 16 | No | The persisted queue logging has failed. |
| 40079 | 16 | No | A non-null variable length value is received for a column that is shorter locally. |
| 40080 | 16 | No | Corrupted (too long) packed row. |
| 40081 | 16 | No | Cardinality of index should not be less then zero. |
| 40082 | 16 | No | Induced exception for testing purposes. |
| 40083 | 16 | No | Corrupted CSN vector. |
| 40084 | 16 | No | Multiple modifications to CSN vector in the same transaction are not supported. |
| 40085 | 16 | No | The primary partition has lost the quorum. New transactions can not start. |
| 40086 | 16 | No | Primary hit an error with this secondary. |
| 40087 | 16 | No | Replica with the specified version is not found. |
| 40088 | 16 | No | CSN being set is outside the CSN epoch range. The new CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40089 | 16 | No | The index configuration for table %ld index %ld does not match the source. |
| 40090 | 16 | No | The primary partition is in transition and the transaction can not commit. |
| 40091 | 16 | No | Truncation CSN is mismatched. The truncation CSN is (%d,%I64d), the current CSN is (%d,%I64d). |
| 40092 | 16 | No | This index %ld state for table %ld does not match the source. |
| 40093 | 16 | No | Replication background task hit a lock timeout. User transactions will be killed and the operation will be retried. |
| 40094 | 16 | No | Incompatible key metadata change. The scan can not be resumed. |
| 40095 | 13 | No | Replication transaction (Process ID %d) was deadlocked on %.\*ls resources with another process and has been chosen as the deadlock victim. The operation will be retried. |
| 40096 | 16 | No | Critical replication task could not start. State is %d. |
| 40097 | 16 | No | The begin transaction message was not found when scanning persisted replication queue. |
| 40098 | 16 | No | Mismatched partition id found in the transaction log. |
| 40099 | 16 | No | Invalid nested transaction count found in the transaction log. |
| 40101 | 16 | No | The partition does not have persisted queues enabled. |
| 40102 | 16 | No | The partition can not have persisted queues modified in this state. |
| 40103 | 16 | No | The partition can not be changed inside a persisted object. |
| 40104 | 16 | No | Only sysadmin can execute this stored procedure '%.\*ls'. |
| 40105 | 16 | No | The local partition map for database %.\*ls is starting up in reduced functionality mode because of log full. Pending partition deletes will not be processed until the log is truncated. |
| 40106 | 16 | No | The schema scope set in the session is not the current schema scope for the current partition. Please rerun your query. |
| 40108 | 16 | No | The filtered replica is not a subset of the primary replica. This is only possible for table groups without a partition key. |
| 40109 | 16 | No | Number of parameters specified for procedure or function %.\*ls is incorrect. |
| 40110 | 16 | Yes | Cannot scope database %s for sp_cloud_scope_database spec proc because it is already set up as a partition database. |
| 40111 | 16 | Yes | Unable to delete partition DB id %d. |
| 40126 | 16 | Yes | The partition database was not found during pending schema scope cleanup, deleting partition metadata only (%d, %s). |
| 40127 | 16 | Yes | %S_MSG database '%.\*ls' link up with the %S_MSG database %d encountered the error: %S_MSG. |
| 40128 | 16 | Yes | Cannot pair database '%.\*ls' with fabric because it is not a partition host. |
| 40129 | 16 | Yes | %S_MSG database link up with the %S_MSG database '%.\*ls' encountered the error: %ls. |
| 40130 | 16 | No | Replication quorum parameter is %d. It should be \>= 1 and \<= 32. |
| 40131 | 16 | No | Partition key type '%.\*ls' is not supported. Only BIGINT, UNIQUEIDENTIFIER, and VARBINARY(n) (0\<n\<=512) datatypes are supported currently. |
| 40132 | 16 | No | Before dropping a table group, you have to delete all the partitions. |
| 40133 | 15 | No | This operation is not supported in this version of SQL Server. |
| 40134 | 16 | No | get_new_rowversion() can only be used in an active transaction. |
| 40135 | 15 | No | This system metadata view is not supported. |
| 40136 | 15 | No | Could not disable versioning because the database is not in single user mode. |
| 40137 | 15 | No | Could not refresh options for all scoped databases. |
| 40138 | 16 | No | Query references entities from multiple partitions. |
| 40139 | 16 | No | The data node does not host a replica of the requested partition. |
| 40140 | 16 | No | Set partition failed since a different partition already was set in the current transaction. Cross partition operations within a node are not reliable or supported. |
| 40141 | 16 | No | Partition has to be set using sp_set_partition before executing this query. |
| 40142 | 16 | No | Accessing a different partition in the same transaction is not allowed. |
| 40143 | 16 | No | The replica that the data node hosts for the requested partition is not primary. |
| 40144 | 16 | No | Cannot find the object "%.\*ls" because it does not exist or you do not have permissions. |
| 40145 | 16 | No | Database is not found. |
| 40146 | 16 | No | Table group object is not found. |
| 40147 | 16 | No | Stored procedure '%.\*ls' is only for CloudDB. |
| 40148 | 16 | No | The existing persisted queue snapshot CSN (%d, %I64d) at %S_LSN is greater than the requested snapshot CSN (%d, %I64d) at %S_LSN. |
| 40149 | 16 | No | The database does not host any partitions. |
| 40150 | 16 | No | Downgrading the severity of error %d, severity %d, state %d because it would cause the server to be shutdown on a non-critical error. |
| 40151 | 16 | No | Partition is in transactionally inconsistent state. |
| 40152 | 16 | No | Partition delete expects no context transaction. |
| 40153 | 16 | No | The current database has been shutdown. The current database has been switched to master. |
| 40154 | 16 | No | Cannot use sp_cloud_add_partition or sp_cloud_delete_partition within a transaction when Partition DB are enabled. |
| 40155 | 16 | Yes | Invalid arguments provided to sp_cloud_add_partition spec proc (%s). |
| 40156 | 16 | Yes | Drop is not allowed on the %S_MSG database '%.\*ls' as it contains partitions. Drop the partition before the operation. |
| 40157 | 16 | No | Too many secondaries. At most 32 are currently supported. |
| 40158 | 16 | No | Could not change database collation for database id %d. |
| 40159 | 16 | No | Database scoping cannot be run inside a transaction. |
| 40160 | 16 | No | Heartbeat message version mismatch. Actual version is %d and the expected is %d. |
| 40161 | 16 | No | Invalid partition type. Only 1 or 2 is supported. |
| 40162 | 16 | No | The replica that the data node hosts for the requested partition is not transactionally consistent. |
| 40163 | 16 | No | Become nothing expects no context transaction. |
| 40164 | 16 | No | Idempotent flush expects no context transaction. |
| 40165 | 16 | No | Prepare for full commit expects no context transaction. |
| 40166 | 16 | No | A CloudDB reconfiguration is going on and all new user transactions are aborted. |
| 40167 | 21 | Yes | A paired CloudDB fabric node failed and database %d must be shutdown. |
| 40168 | 16 | No | SILO_TO_PDB: Partition copy is disabled in M1. |
| 40169 | 16 | No | Waiting for database copy sync with %s.%s has failed. Please make sure the database is in the CATCH_UP state and try again later. |
| 40170 | 16 | No | Catchup of secondary at %s:%s:%s:%d has failed. |
| 40171 | 16 | No | Table group name (single part name) should not be longer than nvarchar(64). |
| 40172 | 16 | No | The partition is not in a state that allows deletion. |
| 40173 | 16 | No | This requested operation can not performed as this partition is in delete process. |
| 40174 | 16 | No | The partition is in transition and transactions are being terminated. |
| 40175 | 16 | No | Add a secondary with wait can not be used in a transaction. |
| 40176 | 16 | No | Rename a partition can not be used in a transaction. |
| 40177 | 16 | No | The new table group does not match the existing table group for the renaming partition. |
| 40178 | 16 | No | A partition with same name already exists. |
| 40179 | 16 | Yes | Fabric-database ('%.\*ls') cannot be paired, the server is not ready to pair. |
| 40180 | 16 | Yes | Fabric-database ('%.\*ls') cannot be paired, the database is already paired. |
| 40181 | 16 | Yes | Fabric-database ('%.\*ls') cannot be paired, the supplied mutex ('%.\*ls') could not be opened. Error code: %d |
| 40182 | 16 | Yes | The schema scope %ld being created is not empty. |
| 40183 | 16 | Yes | Could not create database side pairing mutex for database ('%.\*ls'). Error code: %d |
| 40184 | 16 | No | Login failed. A system operation is in progress, and the database is not accepting user connections. |
| 40185 | 16 | No | Secondary failure report expects no context transaction. |
| 40186 | 16 | No | The data node does not host a replica of the requested partition with the requested version. |
| 40187 | 16 | No | The metadata record for the partition does not exist. |
| 40188 | 16 | No | Failed to update database "%.\*ls" because it is switched to read-only to enforce disaster recovery RPO. |
| 40189 | 16 | No | The resource quota for the current database has been exceeded and this request has been aborted. Please rerun your request in the next quota window. %s |
| 40190 | 16 | No | A context transaction is required. |
| 40191 | 16 | Yes | Cannot create partition DB (%s). |
| 40192 | 16 | No | Catchup of secondary at %s:%s:%s:%d has been cancelled. |
| 40193 | 16 | No | The maximum allowed number of database is already paired. |
| 40194 | 16 | No | Table %s has a partition key already. Explicitly specifying a new partition key is not allowed. Please use "Alter table". |
| 40195 | 16 | No | %ls FOR SID command is not supported in this version of SQL Server. |
| 40196 | 16 | No | A varbinary can not be longer than max length specified. |
| 40197 | 16 | No | The service has encountered an error processing your request. Please try again. Error code %d. |
| 40198 | 16 | No | Only a primary replica can be configured as a forwarder. |
| 40199 | 16 | No | There should be no context transaction when entering the forwarder pending state. |
| 40201 | 16 | No | Destination server name is too long. |
| 40202 | 16 | No | Corrupted composite message. |
| 40203 | 16 | No | Could not register AsyncTransport endpoint. |
| 40204 | 16 | No | Dispatch sequence number maintenance failure |
| 40205 | 16 | No | Maximum transport queue size reached. |
| 40206 | 16 | No | Heartbeat lease time is less than the heartbeat worker interval. |
| 40207 | 16 | No | Async transport test failed. |
| 40208 | 16 | No | Corrupted received message format. |
| 40209 | 17 | No | Duplicate destination id. |
| 40210 | 16 | No | Cluster name has not been set. |
| 40301 | 16 | No | Invalid lock mode or resource received in lock request. |
| 40302 | 16 | No | Invalid identity value. |
| 40303 | 16 | No | Enter idempotent sequence. |
| 40304 | 16 | No | Target object %ld is not found. |
| 40305 | 16 | No | The current object is not a relation. |
| 40306 | 16 | No | Cannot create an index %ld on object %ld. |
| 40307 | 16 | No | Cannot find index %ld on object %ld. |
| 40308 | 16 | No | Metadata replication protocol error. |
| 40309 | 16 | No | Partition %ld is not found |
| 40310 | 16 | No | Converting a clustered index into a heap is not supported. |
| 40311 | 16 | No | Switching out partitions not supported. |
| 40312 | 16 | No | Inconsistent index build state. |
| 40313 | 16 | No | Not all rowsets could be deleted. |
| 40314 | 16 | No | Identity column not found on object %ld. |
| 40401 | 16 | No | Requires AAD login for managing T-SQL streaming jobs. |
| 40402 | 16 | No | Error creating the streaming job request with service auth issue. |
| 40403 | 16 | No | Error creating the streaming job request. |
| 40404 | 16 | No | Error sending the streaming job request. |
| 40405 | 16 | No | T-SQL streaming is not supported. |
| 40406 | 16 | No | The sysadmin role is required for operations on T-SQL streaming jobs. |
| 40407 | 16 | No | The database scoped credential '%.\*ls' has a secret with more than 200 characters. Long secret value is not supported in Synapse T-SQL Streaming. |
| 40408 | 16 | No | '%.\*ls' is not a supported Azure region for Synapse T-SQL streaming. |
| 40409 | 16 | No | Request to update streaming job '%s' failed. The query statement cannot be updated when the job is running. |
| 40410 | 16 | No | Request to update streaming job '%s' failed. Query statement update and online scaling cannot be done at the same time or both not set. |
| 40411 | 16 | No | Request to scale streaming job '%s' failed. The job cannot be scaled when not running. |
| 40412 | 16 | No | Request to stop streaming job '%s' failed. The job is in Created state and not running. |
| 40501 | 20 | No | The service is currently busy. Retry the request after 10 seconds. Incident ID: %ls. Code: %d |
| 40502 | 16 | No | Duplicate group id settings specified. |
| 40503 | 16 | No | Database field %ls contains invalid value '%.\*ls'. Expected data type %ls. |
| 40504 | 16 | No | Switching Databases is not supported. Use a new connection to connect to a different Database. |
| 40505 | 16 | No | Incorrect user credentials. |
| 40506 | 16 | No | Specified SID is invalid for this version of SQL Server. |
| 40507 | 16 | No | '%.\*ls' cannot be invoked with parameters in this version of SQL Server. |
| 40508 | 16 | No | USE statement is not supported to switch between databases. Use a new connection to connect to a different database. |
| 40509 | 16 | No | Upgrade of SAWA v1 database fails. Check the metadata of the database before re-running the upgrade. |
| 40510 | 16 | No | Statement '%.\*ls' is not supported in this version of SQL Server. |
| 40511 | 16 | No | Built-in function '%.\*ls' is not supported in this version of SQL Server. |
| 40512 | 16 | No | Deprecated feature '%ls' is not supported in this version of SQL Server. |
| 40513 | 16 | No | Server variable '%.\*ls' is not supported in this version of SQL Server. |
| 40514 | 16 | No | '%ls' is not supported in this version of SQL Server. |
| 40515 | 16 | No | Reference to database and/or server name in '%.\*ls' is not supported in this version of SQL Server. |
| 40516 | 16 | No | Global temp objects are not supported in this version of SQL Server. |
| 40517 | 16 | No | Keyword or statement option '%.\*ls' is not supported in this version of SQL Server. |
| 40518 | 16 | No | DBCC command '%.\*ls' is not supported in this version of SQL Server. |
| 40519 | 16 | No | Invalid value '%.\*ls' of login field in gateway magic syntax. |
| 40520 | 16 | No | Securable class '%S_MSG' not supported in this version of SQL Server. |
| 40521 | 16 | No | Securable class '%S_MSG' not supported in the server scope in this version of SQL Server. |
| 40522 | 16 | No | Database principal '%.\*ls' type is not supported in this version of SQL Server. |
| 40523 | 16 | No | Implicit user '%.\*ls' creation is not supported in this version of SQL Server. Explicitly create the user before using it. |
| 40524 | 16 | No | Data type '%.\*ls' is not supported in this version of SQL Server. |
| 40525 | 16 | No | 'WITH %ls' is not supported in this version of SQL Server. |
| 40526 | 16 | No | '%.\*ls' rowset provider not supported in this version of SQL Server. |
| 40527 | 16 | No | Linked servers are not supported in this version of SQL Server. |
| 40528 | 16 | No | Users cannot be mapped to Windows logins in this version of SQL Server. |
| 40529 | 16 | No | Built-in function '%.\*ls' in impersonation context is not supported in this version of SQL Server. |
| 40530 | 16 | No | The %.\*ls statement must be the only statement in the batch. |
| 40531 | 11 | No | Server name cannot be determined. It must appear as the first segment of the server's dns name (servername.%.\*ls). Some libraries do not send the server name, in which case the server name must be included as part of the user name (username@servername). In addition, if both formats are used, the server names must match. |
| 40532 | 11 | No | Cannot open server "%.\*ls" requested by the login. The login failed. |
| 40533 | 16 | No | Server '%.\*ls' already exists. |
| 40534 | 16 | No | A valid SID is already associated with the database owner. |
| 40535 | 16 | No | Properties for schema scope '%.\*ls' already exist. |
| 40536 | 16 | No | '%ls' is not supported in this service tier of the database. See Books Online for more details on feature support in different service tiers of Windows Azure SQL Database. |
| 40537 | 16 | No | User '%.\*ls' not found in the database. |
| 40538 | 16 | No | A valid URL beginning with 'https://' is required as value for any filepath specified. |
| 40539 | 16 | No | Windows Azure Storage credential '%.\*ls' was not found. |
| 40540 | 16 | No | Transaction was aborted as database is moved to read-only mode. This is a temporary situation and please retry the operation. |
| 40541 | 16 | No | Procedure cannot be called from inside a partition. |
| 40542 | 16 | No | Incorrect number of parameters specified for procedure. |
| 40543 | 16 | No | Invalid %S_MSG name specified. Length should be between 1 and %d. |
| 40544 | 20 | No | The database '%.\*ls' has reached its size quota. Partition or delete data, drop indexes, or consult the documentation for possible resolutions. |
| 40545 | 20 | No | The service is experiencing a problem that is currently under investigation. Incident ID: %ls. Code: %d |
| 40546 | 16 | No | Cannot create UCS task pool |
| 40548 | 16 | No | Granting CONNECT permission to the guest user in database '%.\*ls' is not permitted. |
| 40549 | 16 | No | Session is terminated because you have a long running transaction. Try shortening your transaction. |
| 40550 | 16 | No | The session has been terminated because it has acquired too many locks. Try reading or modifying fewer rows in a single transaction. |
| 40551 | 16 | No | The session has been terminated because of excessive TEMPDB usage. Try modifying your query to reduce temporary table space usage. |
| 40552 | 16 | No | The session has been terminated because of excessive transaction log space usage. Try modifying fewer rows in a single transaction. |
| 40553 | 16 | No | The session has been terminated because of excessive memory usage. Try modifying your query to process fewer rows. |
| 40554 | 10 | No | Exiting because of XEvent %ls. |
| 40555 | 16 | No | '%ls' is not supported for this database. See Books Online for more details on feature support in different service tiers of Windows Azure SQL Database. |
| 40558 | 16 | No | Error - cannot perform checkpoint on a partition database before loading partition information. |
| 40559 | 16 | No | File based statement options are not supported in this version of SQL Server. |
| 40560 | 16 | No | Specified invalid LSN: '%.\*ls' for database '%.\*ls' while publishing external actor LSN progress. |
| 40561 | 16 | No | Database copy failed. Either the source or target database does not exist. |
| 40562 | 16 | No | Database copy failed. The source database has been dropped. |
| 40563 | 16 | No | Database copy failed. The target database has been dropped. |
| 40564 | 16 | No | Database copy failed. Database copy failed due to an internal error. Please drop target database and try again.\</value\> |
| 40565 | 16 | No | Database copy failed. No more than 1 concurrent database copy from the same source is allowed. Please drop target database and try again later. |
| 40566 | 16 | No | Database copy failed due to an internal error. Please drop target database and try again. |
| 40567 | 16 | No | Database copy failed due to an internal error. Please drop target database and try again. |
| 40568 | 16 | No | Database copy failed. Either the source or the target database has become unavailable. Please drop target database and try again. |
| 40569 | 16 | No | Database copy failed. Target database has become unavailable. Please drop target database and try again. |
| 40570 | 16 | No | Database copy failed due to an internal error. Please drop target database and try again later. |
| 40571 | 16 | No | Database copy failed due to an internal error. Please drop target database and try again later. |
| 40572 | 16 | No | Cannot obtain primary partition lock for CloudDB auto partition upgrade. |
| 40573 | 16 | No | Cannot upgrade CloudDB auto partitions in database '%.\*ls' because the physical database is read only. |
| 40574 | 16 | No | Permissions for system stored procedures, server scoped catalog views, and extended stored procedures cannot be changed in this version of SQL Server. |
| 40576 | 16 | No | Table DDL on non-temporary tables is not supported in a filtered connection. |
| 40578 | 16 | No | Statement '%.\*ls' is not supported in a filtered connection. |
| 40581 | 16 | No | Logically filtered secondaries are only supported if the secondary is a forwarder. |
| 40584 | 16 | No | Value '%.\*ls' for option '%.\*ls' is not supported in this version of SQL Server. |
| 40585 | 16 | No | Can not perform replica operation because this node is not the forwarder for this partition. |
| 40586 | 16 | No | Replicas with deferred commit enabled cannot be a member of a quorum. |
| 40587 | 16 | No | Deferred commit is only supported with forwarder replicas. |
| 40588 | 16 | No | Cannot create partition worker pool |
| 40589 | 16 | No | Replicas that are not enabled for deferred commit cannot specify RPO. |
| 40590 | 16 | No | The Gpm is in rebuild and cannot be accessed as it is not yet consistent. |
| 40591 | 16 | No | Extended event configuration could not be initialized. The error is %ls. |
| 40592 | 16 | No | Extended event session '%ls' could not be created or altered. XE Error %d.%d state:%d. |
| 40593 | 16 | No | Extended event session '%ls' returned error '%ls'. |
| 40594 | 16 | No | Extended event session '%ls' has been started. |
| 40595 | 16 | No | Extended event session '%ls' has been altered. |
| 40596 | 16 | No | Extended event session '%ls' has been stopped. |
| 40597 | 16 | No | Keyword or statement option '%.\*ls' is not supported for External Logins. |
| 40599 | 16 | No | This type of KILL is not supported in Windows Azure SQL Database; Only 'KILL session ID \[WITH STATUSONLY\]' and 'KILL UOW' are supported. |
| 40601 | 16 | No | Server Admin user already exists. |
| 40602 | 16 | No | Could not create login. Please try again later. |
| 40603 | 16 | No | Cannot execute procedure because current user is not Gateway. |
| 40604 | 16 | No | Could not %.\*ls because it would exceed the quota of the server. |
| 40605 | 16 | No | There is no route from the source cluster '%ls' to the target cluster '%ls'. |
| 40606 | 16 | No | Databases cannot be attached in this version of SQL Server. |
| 40607 | 16 | No | Windows logins are not supported in this version of SQL Server. |
| 40608 | 10 | No | This session has been assigned a tracing ID of '%.\*ls'. Provide this tracing ID to customer support when you need assistance. |
| 40609 | 16 | No | '%.\*ls' is not a valid IPv4 address. |
| 40610 | 16 | No | The IP address that starts with '%.\*ls' is too long. Maximum length is %d. |
| 40611 | 16 | No | Windows Azure SQL Database supports a maximum of 128 firewall rules. |
| 40612 | 16 | No | Spec proc was executed against a silo that cannot be upgraded to include firewall objects. |
| 40613 | 17 | No | Database '%.\*ls' on server '%.\*ls' is not currently available. Please retry the connection later. If the problem persists, contact customer support, and provide them the session tracing ID of '%.\*ls'. |
| 40614 | 16 | No | Start IP address of firewall rule cannot exceed End IP address. |
| 40615 | 16 | No | Cannot open server '%.\*ls' requested by the login. Client with IP address '%.\*ls' is not allowed to access the server. To enable access, use the Windows Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall rule for this IP address or address range. It may take up to five minutes for this change to take effect. |
| 40616 | 16 | No | '%.\*ls' is not a valid login name in this version of SQL Server. |
| 40617 | 16 | No | The firewall rule name that starts with '%.\*ls' is too long. Maximum length is %d. |
| 40618 | 16 | No | The firewall rule name cannot be empty. |
| 40619 | 16 | No | The edition '%.\*ls' does not support the database data max size '%.\*ls'. |
| 40620 | 16 | No | The login failed for user "%.\*ls". The password change failed. Password change during login is not supported in this version of SQL Server. |
| 40621 | 16 | No | metric type |
| 40622 | 16 | No | metric data |
| 40623 | 20 | No | Reauthentication failed for login "%.\*ls". Within the past reauthentification interval, the login has become invalid due to a password change, a dropped login, or other cause. Please retry login. |
| 40624 | 16 | No | Operation is not allowed because server '%.\*ls' is disabled. |
| 40625 | 17 | No | Provisioning (creating, altering, or dropping) Windows Azure SQL Database servers and databases is currently disabled. This most frequently occurs for brief periods during system maintenance. |
| 40626 | 20 | No | The ALTER DATABASE command is in process. Please wait at least five minutes before logging into database '%.\*ls', in order for the command to complete. Some system catalogs may be out of date until the command completes. If you have altered the database name, use the NEW database name for future activity. |
| 40627 | 20 | No | Operation on server '%.\*ls' and database '%.\*ls' is in progress. Please wait a few minutes before trying again. |
| 40628 | 16 | No | Failed to update database '%.\*ls' because the database is read-only. Please contact your Windows Azure service owner. There may be billing related issues with your Windows Azure account. |
| 40629 | 16 | No | An edition could not be determined from maxsize '%.\*ls'. Specify a valid maxsize value. |
| 40630 | 16 | No | Password validation failed. The password does not meet policy requirements because it is too short. |
| 40631 | 16 | No | The password that you specified is too long. The password should have no more than %d characters. |
| 40632 | 16 | No | Password validation failed. The password does not meet policy requirements because it is not complex enough. |
| 40633 | 16 | No | '%.\*ls' is not a valid database edition in this version of SQL Server. |
| 40634 | 16 | No | This stored procedure can only be executed in the master database. |
| 40635 | 16 | No | Client with IP address "%.\*ls" is temporarily blocked. |
| 40636 | 16 | No | Cannot use reserved database name '%.\*ls' in this operation. |
| 40637 | 17 | No | Database copy is currently disabled. |
| 40638 | 16 | No | Invalid subscription id '%.\*ls'. Subscription does not exist. |
| 40639 | 16 | No | Request does not conform to schema: %.\*ls. |
| 40640 | 20 | No | The server encountered an unexpected exception. |
| 40641 | 16 | No | Location '%.\*ls' cannot be found. |
| 40642 | 17 | No | The server is currently too busy. Please try again later. |
| 40643 | 16 | No | The specified x-ms-version header value is invalid. |
| 40644 | 14 | No | Failed to authorize access to the specified subscription. |
| 40645 | 16 | No | Servername "%.\*ls" cannot be empty or null. It can only be made up of lowercase letters 'a'-'z', the numbers 0-9 and the hyphen. The hyphen may not lead or trail in the name. |
| 40646 | 16 | No | Subscription ID cannot be empty. |
| 40647 | 16 | No | Subscription '%.\*ls' does not have the server '%.\*ls'. |
| 40648 | 17 | No | Too many requests have been performed. Please retry later. |
| 40649 | 16 | No | Invalid content-type is specified. Only application/xml is supported. |
| 40650 | 16 | No | Subscription '%.\*ls' is not ready for the operation because another operation is currently in progress. Please wait a few minutes and then try the operation again. |
| 40651 | 16 | No | Failed to create server because the subscription '%.\*ls' is disabled. |
| 40652 | 16 | No | Cannot move or create server. Subscription '%.\*ls' will exceed server quota. |
| 40653 | 16 | No | Could not find database '%.\*ls' at time '%.\*ls' that can be restored. |
| 40654 | 16 | No | Specified subregion '%.\*ls' is invalid. |
| 40655 | 16 | No | Database 'master' cannot be restored. |
| 40656 | 16 | No | Quota for maximum number of concurrent restores has been exceeded. |
| 40657 | 16 | No | Restore is not enabled on the server. |
| 40658 | 16 | No | Quota for number of restores has been exceeded. |
| 40659 | 16 | No | Could not successfully restore database because the maximum duration for processing a restore has elapsed. |
| 40660 | 16 | No | Could not successfully restore database. This request has been assigned a tracing ID of '%.\*ls'. Provide this tracing ID to customer support when you need assistance. |
| 40661 | 16 | No | Restore has been cancelled by a system administrator. |
| 40662 | 16 | No | An internal error was encountered when processing the restore request. This request has been assigned a tracing ID of '%.\*ls'. Provide this tracing ID to customer support when you need assistance. |
| 40663 | 16 | No | Database '%.\*ls' is currently being restored and cannot be dropped. Please wait for restore to complete. |
| 40664 | 16 | No | Database 'master' cannot be copied. |
| 40665 | 16 | No | '%.\*ls' is not a supported collation. |
| 40666 | 16 | No | '%.\*ls' is a unicode-only collation and cannot be a default collation for a database. |
| 40667 | 15 | No | Specifying a LOGIN is not allowed in a federation member. |
| 40668 | 16 | No | '%.\*ls' is not a valid user name or you do not have permission. |
| 40669 | 17 | No | Location '%.\*ls' is not accepting creation of new Windows Azure SQL Database servers at this time. |
| 40670 | 16 | No | The http header 'ocp-resourceprovider-registered-uri' is missing from the request or is invalid. To continue, provide a valid value for the header. |
| 40671 | 17 | No | Unable to '%.\*ls' '%.\*ls' on server '%.\*ls'. Please retry the connection later. |
| 40672 | 16 | No | The service objective assignment for a database cannot be changed more than once per %d hour(s). Please retry the operation %d hour(s) after the last service objective assignment completed for this database. |
| 40673 | 16 | No | The service objective assignment for the database has failed. Please contact Microsoft customer support and provide the server name, database name and activity ID. |
| 40674 | 16 | No | Service objective creation or assignment is not permitted for this subscription. |
| 40675 | 16 | No | The service is currently too busy. Please try again later. |
| 40676 | 16 | No | System databases cannot be %ls. |
| 40677 | 16 | No | The operation for the request uri '%.\*ls' was not found. To continue, please provide a valid request uri. |
| 40678 | 16 | No | Invalid value for header '%.\*ls'. The header must contain a single valid GUID. |
| 40679 | 16 | No | The operation cannot be performed since the database '%ls' is not in a replication relationship. |
| 40680 | 16 | No | The operation cannot be performed since the database '%ls' is in a replication relationship. |
| 40681 | 16 | No | The operation cannot be performed since the database '%ls' is a replication target. |
| 40682 | 16 | No | Failed to update database '%.\*ls' because the database is a replication target. |
| 40683 | 16 | No | The operation cannot be performed since the database '%ls' is not a replication target. |
| 40684 | 16 | No | A seeding operation is already in progress for database '%ls'. |
| 40685 | 16 | No | A terminate geo relationship operation is already in progress for database '%ls'. Please wait for this operation to finish. |
| 40686 | 16 | No | The operation is currently not supported. |
| 40687 | 16 | No | The operation cannot be performed on the database '%.\*ls' in its current state. |
| 40688 | 16 | No | The databases '%ls' in server '%ls' and '%ls' in server '%ls' are already in a replication relation. |
| 40689 | 16 | No | Replication limit reached. The database '%ls' cannot have more than %d replication relationships. |
| 40690 | 16 | No | The operation cannot be performed since the replication source and target databases have different names. The source and target databases must have the same name. |
| 40691 | 16 | No | Replication target cannot be created in the same server as source. |
| 40692 | 16 | No | The alter database '%ls' failed to initiate because there are operations pending on the database. After the pending operations are complete, try again. |
| 40693 | 16 | No | The current operation cannot be initiated while a replication operation is in progress. You can rename the database only after the replication operation has stopped. |
| 40694 | 16 | No | The seeding operation cannot be initiated on a replication target database. |
| 40695 | 16 | No | The operation cannot be performed since the database is currently a Federation root or member database. |
| 40696 | 16 | No | sp_wait_for_database_copy_sync failed because the current database is not the primary database involved in a replication relationship with the specified target server '%s' and database '%s'. |
| 40697 | 16 | No | Login failed for user '%.\*ls'. |
| 40698 | 16 | No | '%.\*ls' cannot be performed on a free database. |
| 40699 | 16 | No | You cannot create a user with password in this version of SQL Server |
| 40700 | 16 | No | Hardware generation '%.\*ls' is currently unavailable in Azure SQL Database. Please create a SQL Database support request to resolve. |
| 40701 | 16 | No | XML format used for specifying rules is invalid. %.\*ls. |
| 40702 | 16 | No | Failed to parse XML rules. |
| 40703 | 16 | No | Invalid attribute name '%.\*ls' in %.\*ls. |
| 40704 | 16 | No | Invalid element name '%.\*ls' in %.\*ls. |
| 40705 | 16 | No | Invalid Feature type '%.\*ls' in %.\*ls. |
| 40706 | 16 | No | Feature name '%.\*ls' does not exist. |
| 40707 | 16 | No | Invalid index value '%.\*ls' in %.\*ls. |
| 40708 | 16 | No | Invalid param count '%.\*ls' in %.\*ls. |
| 40709 | 16 | No | operator attribute is missing in %.\*ls. |
| 40711 | 16 | No | Rule name '%.\*ls' does not exist. |
| 40712 | 16 | No | Invalid usage of %.\*ls. |
| 40713 | 16 | No | Invalid values supplied for \<parameter\> element in %.\*ls. |
| 40714 | 16 | No | Out of memory. |
| 40715 | 16 | No | Invalid operator type %.\*ls in %.\*ls. |
| 40716 | 16 | No | Invalid input type %.\*ls in %.\*ls. |
| 40717 | 16 | No | index attribute is missing in %.\*ls. |
| 40718 | 16 | No | one of inputtype, isnull and format attributes is required in %.\*ls. |
| 40719 | 16 | No | Failed to get %s lock on %s rules. |
| 40720 | 16 | No | Rule name '%.\*ls' already exists. |
| 40721 | 16 | No | Only one of inputtype, isnull and format attributes is required in %.\*ls. |
| 40722 | 16 | No | Failed to clear proc cache. |
| 40723 | 16 | No | Rule name cannot exceed more than %d characters. |
| 40724 | 16 | No | Unexpected Operator attribute in %.\*ls. |
| 40801 | 16 | No | Operation ALTER USER WITH LOGIN failed. User provided login does not match the login in the federation root database for the user provided username. |
| 40802 | 16 | No | A service objective assignment on server '%.\*ls' and database '%.\*ls' is already in progress. Please wait until the service objective assignment state for the database is marked as 'Completed'. |
| 40803 | 16 | No | The server '%.\*ls' has reached its quota of (%d) premium databases. |
| 40804 | 16 | No | The specified service objective '%.\*ls' is invalid. |
| 40805 | 16 | No | The service objective assignment for the database has failed. Please contact Microsoft customer support and provide the activity ID. |
| 40806 | 16 | No | The request to retrieve subscription information has timed out. Please try again later. |
| 40807 | 16 | No | Could not retrieve subscription information for subscription id: %.\*ls, after %d attempts. Please try again later. |
| 40808 | 16 | No | The edition '%.\*ls' does not support the service objective '%.\*ls'. |
| 40809 | 16 | No | No rows found in sys.dm_operation_status table for database '%.\*ls' and operation '%.\*ls'. |
| 40810 | 16 | No | More than one row found in sys.dm_operation_status table for database '%.\*ls' and operation '%.\*ls'. |
| 40811 | 16 | No | Operation '%.\*ls' for database '%.\*ls' cannot be cancelled as it has already completed. |
| 40812 | 16 | No | Database '%.\*ls' cannot be dropped as create operation is in progress. The create operation will be cancelled. |
| 40813 | 16 | No | Could not set database as writable because the database is Premium (suspended). |
| 40814 | 16 | No | Could not change database edition to or from Premium for a Federation root. |
| 40815 | 16 | No | Could not change database edition to or from Premium for a Federation member. |
| 40816 | 16 | No | Could not change database edition to Premium for a database in a replication relationship. |
| 40817 | 16 | No | Could not change database edition from Premium for a database in a replication relationship. |
| 40818 | 16 | No | The replication operation on database '%ls' failed because there are alter operations pending on the database. Try again after the pending operations have completed. |
| 40820 | 16 | No | The server has reached its quota of (%d) premium databases. |
| 40821 | 16 | No | Federations are not supported on a Premium database. |
| 40822 | 16 | No | This feature is not available for the selected database's edition (%ls). |
| 40823 | 16 | No | Invalid proxy override option supplied. |
| 40824 | 16 | No | ProxyOverrideSupport feature switch is not turned ON. |
| 40825 | 16 | No | Unable to complete request now. Please try again later. |
| 40827 | 16 | No | The operation is not supported for your subscription offer type. |
| 40838 | 16 | No | Replication relationship limit reached. The database '%ls' cannot have more than one non-readable secondary. |
| 40839 | 16 | No | Connection to a non-readable secondary database is not allowed. See 'http://go.microsoft.com/fwlink/?LinkID=402429&clcid=0x409' for more information. |
| 40840 | 16 | No | Target region '%ls' is not a DR paired Azure region. See 'http://go.microsoft.com/fwlink/?LinkID=402430&clcid=0x409' for more information. |
| 40841 | 16 | No | Friendly termination of a non-readable replication relationship is not supported. |
| 40842 | 16 | No | Termination of the non-readable replication relationship for database '%ls' is currently not allowed. See 'http://go.microsoft.com/fwlink/?LinkID=402431&clcid=0x409' for more information |
| 40843 | 16 | No | Non-readable secondary is not supported for database copy. |
| 40844 | 16 | No | Database '%ls' on Server '%ls' is a '%ls' edition database in an elastic pool and cannot have a replication relationship. |
| 40845 | 16 | No | Server DNS Alias name '%.\*ls' may only contain a single '@' when used to denote a SQL Server alias. |
| 40846 | 16 | No | DNS alias '%.\*ls' cannot be moved between DNS zones. Target server '%.\*ls' is in a DNS zone ('%.\*ls') that is different from the current server's '%.\*ls' DNS zone ('%.\*ls'). |
| 40847 | 16 | No | Could not perform the operation because server would exceed the allowed Database Throughput Unit quota of %d. |
| 40848 | 16 | No | The source database '%ls'.'%ls' cannot have higher performance level than the target database '%ls'.'%ls'. Upgrade the performance level on the target before upgrading source. |
| 40849 | 16 | No | The target database '%ls'.'%ls' cannot have lower performance level than the source database '%ls'.'%ls'. Downgrade the performance level on the source before downgrading target. |
| 40850 | 16 | No | Could not change database edition from '%ls' to Standard for database '%ls' in a replication relationship. |
| 40851 | 16 | No | Could not change database edition from '%ls' to Basic for database '%ls' in a replication relationship. |
| 40852 | 16 | No | Cannot open database '%.\*ls' on server '%.\*ls' requested by the login. Access to the database is only allowed using a security-enabled connection string. |
| 40853 | 16 | No | A recovery request for a database that is currently in an elastic pool must include either a target elastic pool name or a target service objective. |
| 40854 | 16 | No | Partner server '%ls' is not compatible with server '%ls.' |
| 40855 | 16 | No | The operation cannot be performed since the database '%ls' is not in '%ls' state on the replication relationship. |
| 40856 | 16 | No | Could not change database edition for database '%ls' in a replication relationship. |
| 40857 | 16 | No | Elastic pool not found for server: '%ls', elastic pool name: '%ls'. |
| 40858 | 16 | No | Elastic pool '%ls' already exists in server: '%ls' |
| 40859 | 16 | No | Elastic pool does not support database edition '%ls'. |
| 40860 | 16 | No | Elastic pool '%ls' and service level objective '%ls' combination is invalid. |
| 40861 | 16 | No | The database edition '%ls' cannot be different than the elastic pool service tier which is '%ls'. |
| 40862 | 16 | No | Elastic pool name must be specified if the elastic pool service objective is specified. |
| 40863 | 16 | No | Connections to this database are no longer allowed. |
| 40864 | 16 | No | The DTUs for the elastic pool must be at least (%d) DTUs for service tier '%.\*ls'. |
| 40865 | 16 | No | The DTUs for the elastic pool cannot exceed (%d) DTUs for service tier '%.\*ls'. |
| 40866 | 16 | No | Max size (%d) is not valid. Please specify a valid max size. |
| 40867 | 16 | No | The DTU max per database must be at least (%d) for service tier '%.\*ls'. |
| 40868 | 16 | No | The DTU max per database cannot exceed (%d) for service tier '%.\*ls'. |
| 40869 | 16 | No | The DTU max per database (%d) for the elastic pool does not belong to the specified values for service tier '%.\*ls'. |
| 40870 | 16 | No | The DTU min per database cannot exceed (%d) for service tier '%.\*ls'. |
| 40871 | 16 | No | The DTU min per database (%d) for the elastic pool does not belong to the allowed values for service tier '%.\*ls'. |
| 40872 | 16 | No | DTU value (%d) is not valid. Please specify a valid dtu value. |
| 40873 | 16 | No | The number of databases (%d) and DTU min per database (%d) cannot exceed the DTUs of the elastic pool (%d). |
| 40874 | 16 | No | The DTUs (%d) for the elastic pool does not belong to the specified values for service tier '%.\*ls'. |
| 40875 | 16 | No | The elastic pool storage limit in gigabytes cannot exceed (%d) in service tier '%.\*ls'. |
| 40876 | 16 | No | Elastic pools are not available in this region. |
| 40877 | 16 | No | The elastic pool is not empty. |
| 40878 | 16 | No | The elastic pool storage limit in gigabytes must be at least (%d) for service tier '%.\*ls'. |
| 40879 | 16 | No | The elastic pool storage limit in gigabytes (%d) does not belong to the allowed values for service tier '%.\*ls'. |
| 40880 | 16 | No | The DTUs (%d) for the elastic pool and the storage limit in gigabytes (%d) are inconsistent for service tier '%.\*ls'. |
| 40881 | 16 | No | The elastic pool '%.\*ls' has reached its database count limit. The database count for the elastic pool cannot exceed (%d) for service tier '%.\*ls'. |
| 40882 | 16 | No | Can not change SLO from DataWarehouse edition to other SQL DB editions and vice versa. |
| 40883 | 16 | No | The service level objective '%.\*ls' specified is invalid. It must be a slo supported by DataWarehouse edition. |
| 40884 | 16 | No | New service level objective '%.\*ls' has (%d) physical databases and it is not compatible with current service level objective which has (%d) physical databases. |
| 40885 | 16 | No | Failed to deactivate database. |
| 40886 | 16 | No | Failed to change service level objective for database. |
| 40887 | 16 | No | Failed to activate database. |
| 40888 | 16 | No | Update service level objective for database feature is disabled. |
| 40889 | 16 | No | The storage limit for the elastic pool '%.\*ls' does not provide sufficient storage space for its databases. Please increase the storage limit or move databases out of the pool. |
| 40890 | 16 | No | The elastic pool is busy with another operation. |
| 40891 | 16 | No | The DTU min per database (%d) cannot exceed the DTU max per database (%d). |
| 40892 | 16 | No | Cannot connect to database when it is paused. |
| 40893 | 16 | No | The database copy link from '%s.%s' to '%s.%s' was not successfully created or was deleted before the data copy link operation completed. |
| 40894 | 16 | No | The database copy link from '%s.%s' to '%s.%s' is not in the catchup state after the data copy link operation completed. |
| 40895 | 16 | No | The database copy link from '%s.%s' to '%s.%s' was not successfully deleted before completion of copy. |
| 40896 | 16 | No | The database copy link from '%s.%s' to ID %s was not successfully dropped. |
| 40897 | 16 | No | The elastic pool storage limit in megabytes must be at least (%d) for service tier '%.\*ls'. |
| 40898 | 16 | No | The elastic pool storage limit in megabytes (%d) does not belong to the allowed values for service tier '%.\*ls'. |
| 40899 | 16 | No | The DTUs (%d) for the elastic pool and the storage limit in megabytes (%d) are inconsistent for service tier '%.\*ls'. |
| 40900 | 16 | No | The service tier for an elastic pool cannot be changed. |
| 40901 | 16 | No | The elastic pool storage limit in megabytes cannot exceed (%d) in service tier '%.\*ls'. |
| 40902 | 16 | No | Amount of pool storage cannot be specified when creating a Premium elastic pool. |
| 40903 | 20 | No | The server '%.\*ls' is currently busy. Please wait a few minutes before trying again. |
| 40904 | 16 | No | Could not perform the operation because server would exceed the allowed Database Edition %s quota of %d. |
| 40905 | 16 | No | Location '%.\*ls' is not accepting creation of new Azure SQL Database Servers of version '%.\*ls' at this time. This location only supports the following server versions: '%.\*ls'. Please retry using a supported server version. |
| 40906 | 16 | No | A service objective change cannot start for database %s on server %s while it is also running for database %s on server %s. |
| 40907 | 16 | No | Servers involved in a Disaster Recovery Configuration cannot reside in the same location |
| 40908 | 16 | No | A Disaster Recovery Configuration already exists for '%.\*ls' and '%.\*ls' |
| 40909 | 16 | No | A Disaster Recovery Configuration does not exist for '%.\*ls' and '%.\*ls' |
| 40910 | 16 | No | A Disaster Recovery Configuration does not exist for server '%.\*ls' and virtual endpoint '%.\*ls' |
| 40911 | 16 | No | Server '%.\*ls' is not the secondary in the Disaster Recovery Configuration and cannot initiate a failover |
| 40912 | 16 | No | The value for custom backup retention in days must be between %d and %d |
| 40913 | 16 | No | Windows Azure SQL Database supports a maximum of %d Virtual Network firewall rules. |
| 40914 | 16 | No | Cannot open server '%.\*ls' requested by the login. Client is not allowed to access the server. |
| 40915 | 16 | No | Secondary server for a Failover Group cannot reside in the same region as primary server. Please provide a name of server that is in different Azure region than primary server. |
| 40916 | 16 | No | The Failover Group '%.\*ls' already exists on server '%.\*ls' |
| 40917 | 16 | No | The Failover Group '%.\*ls' does not exist on server '%.\*ls' |
| 40918 | 16 | No | The Failover Group '%.\*ls' is busy with another operation and cannot perform the '%.\*ls' operation. Please try again later |
| 40919 | 16 | No | The server '%.\*ls' is currently the primary server in the Failover Group and cannot initate failover |
| 40920 | 16 | No | The database '%.\*ls' is already included in another Failover Group |
| 40921 | 16 | No | The operation to add database '%.\*ls' to Failover Group is in progress, please wait for this operation to finish |
| 40922 | 16 | No | The operation to remove database '%.\*ls' from Failover Group is in progress, please wait for this operation to finish |
| 40923 | 16 | No | The database '%.\*ls' is a secondary in an existing geo-replication relationship and cannot be added to the Failover Group |
| 40924 | 16 | No | The operation cannot be performed due to multiple errors: '%.\*ls' Please correct them and retry operation. |
| 40925 | 16 | No | Can not connect to the database in its current state. |
| 40926 | 16 | No | The operation cannot be performed because the geo-replication link is part of a Failover Group. You must remove the database from the group in order to individually terminate or failover. |
| 40927 | 16 | No | The endpoint '%.\*ls' is already in use. Use a different Failover Group name. |
| 40928 | 16 | No | Create or update Failover Group operation successfully completed; however, some of the databases could not be added to or removed from Failover Group: '%.\*ls' |
| 40929 | 16 | No | The source database '%ls.%ls' cannot have higher edition than the target database '%ls.%ls'. Upgrade the edition on the target before upgrading source. |
| 40930 | 16 | No | The target database '%ls.%ls' cannot have lower edition than the source database '%ls.%ls'. Downgrade the edition on the source before downgrading target. |
| 40931 | 16 | No | Failover Group name '%.\*ls' cannot be empty or null. It can only be made up of lowercase letters 'a'-'z', the numbers 0-9 and the hyphen. The hyphen may not lead or trail in the name. |
| 40932 | 16 | No | The elastic pool cannot change its service tier since one or more of its databases use memory-optimized objects. |
| 40933 | 16 | No | The edition '%.\*ls' does not support the database tempdb max size '%.\*ls'. |
| 40934 | 16 | No | Server DNS Alias name '%.\*ls' cannot be empty or null. It can only be made up of lowercase letters 'a'-'z', the numbers 0-9 and the hyphen. The hyphen may not lead or trail in the name. |
| 40935 | 16 | No | The endpoint '%.\*ls' is already in use. Use a different Server DNS Alias name. |
| 40936 | 16 | No | The Server DNS Alias '%.\*ls' already exists for the server '%.\*ls'. Please use different Server DNS Alias name. |
| 40937 | 16 | No | The Server DNS Alias '%.\*ls' does not exist for the server '%.\*ls'. Please provide valid Server DNS Alias name. |
| 40938 | 16 | No | The Server DNS Alias '%.\*ls' is busy with another operation and cannot perform the '%.\*ls' operation. Please try again later. |
| 40939 | 16 | No | The scale operation from service level objective '%.\*ls' to new service level objective '%.\*ls' is not supported. Please file a support ticket. |
| 40940 | 16 | No | The elastic pool '%.\*ls' cannot be updated because one of its databases is performing a copy or geo-replication failover operation. |
| 40941 | 16 | No | Copy operation for the database '%.\*ls' on the server '%.\*ls' cannot be started because the elastic pool '%.\*ls' is currently being updated. |
| 40942 | 16 | No | A service objective assignment operation cannot be performed because copy or failover operation for the database '%.\*ls' on the server '%.\*ls' is in progress. |
| 40943 | 16 | No | A Failover Group cannot be created on the server '%.\*ls' because the Table Auditing feature is enabled for this server. |
| 40944 | 16 | No | The database '%.\*ls' on the server '%.\*ls' cannot be added to a Failover Group because the Table Auditing or Security Enabled Access feature is turned on for this database. |
| 40945 | 16 | No | The Table Auditing feature cannot be turned on for a server that contains Failover Groups. Please try Blob Auditing instead. |
| 40946 | 16 | No | The Table Auditing or Security Enabled Access feature cannot be turned on for a database that is part of a Failover Group. |
| 40947 | 16 | No | The Table Auditing or Security Enabled Access feature cannot be turned on for a database that is located on a server with a Server DNS Alias. |
| 40948 | 16 | No | A Server DNS Alias cannot be created for the server '%.\*ls' because the Table Auditing feature is enabled for this server. |
| 40949 | 16 | No | A Server DNS Alias cannot be created for the server '%.\*ls' because the database '%.\*ls' has the Table Auditing or Security Enabled Access feature enabled. |
| 40950 | 16 | No | The Dns Alias '%.\*ls' already exists for the elastic pool '%.\*ls' on server '%.\*ls'. |
| 40951 | 16 | No | The Dns Alias '%.\*ls' does not exist for the elastic pool '%.\*ls' on server '%.\*ls'. |
| 40952 | 16 | No | A Server DNS Alias cannot be created because server '%.\*ls' would exceed the allowed Server DNS Aliases quota of %d. |
| 40953 | 16 | No | A Failover Group cannot be created because server '%.\*ls' would exceed the allowed Failover Groups quota of %d. |
| 40954 | 16 | No | The Table Auditing feature cannot be turned on for a server that contains Server Dns Aliases. Please try Blob Auditing instead. |
| 40955 | 16 | No | The operation cannot be performed due to insufficient file space in the elastic pool. The operation requires (%d) MBs file space and there are (%d) MBs file space available. Unused file space must be reclaimed before retrying the operation. Please refer to the following article for details on reclaiming unused file space: https://go.microsoft.com/fwlink/?linkid=864775. |
| 40956 | 16 | No | The Server Disaster Recovery Configuration feature is deprecated. Please use Failover Group instead. |
| 40957 | 16 | No | The DTU min per database must be at least (%d) for service tier '%.\*ls'. |
| 40958 | 16 | No | The VCore max per database must be at least (%d) for service tier '%.\*ls'. |
| 40959 | 16 | No | The VCore max per database cannot exceed (%d) for service tier '%.\*ls'. |
| 40960 | 16 | No | The VCore max per database (%d) for the elastic pool does not belong to the specified values for service tier '%.\*ls'. |
| 40961 | 16 | No | The VCore min per database cannot exceed (%d) for service tier '%.\*ls'. |
| 40962 | 16 | No | The VCore min per database must be at least (%d) for service tier '%.\*ls'. |
| 40963 | 16 | No | The VCore min per database (%d) for the elastic pool does not belong to the allowed values for service tier '%.\*ls'. |
| 40964 | 16 | No | The VCore min per database (%d) cannot exceed the VCore max per database (%d). |
| 40965 | 16 | No | The service level objective '%.\*ls' does not support the license type '%.\*ls'. |
| 40966 | 16 | No | No service objective was found for capacity '%d' in edition '%.\*ls' |
| 40967 | 16 | No | More than one service objective was found for capacity '%d' in edition '%.\*ls' |
| 40968 | 16 | No | Operation results in exceeding quota limits of %d. Maximum allowed: %d. |
| 40969 | 16 | No | Cannot open server '%.\*ls' requested by the login. Client is not allowed to access the server. |
| 40970 | 16 | No | Can not change from '%.\*ls' edition to '%.\*ls' edition. |
| 40971 | 16 | No | Cannot open Failover Group "%.\*ls" requested by the login. The login failed. |
| 40972 | 16 | No | Database copy limit per database reached. The database '%ls' cannot have more than %d concurrent database copies. |
| 40973 | 16 | No | Can not drop database because a failover operation is in progress on the failover group. |
| 40974 | 16 | No | Partner region specified does not match region of partner managed instance. |
| 40975 | 16 | No | Partner managed instance's DNS Zone does not match the DNS Zone of the source managed instance. |
| 40976 | 16 | No | Instance Failover Group cannot be created because partner managed server "%.\*ls" is not empty or does not have secondaries for all databases in the primary instance "%.\*ls" . |
| 40977 | 16 | No | '%.\*ls' is not a supported timezone. |
| 40978 | 16 | No | Instance failover group cannot be created because the reserved storage size on the secondary instance is different from the reserved storage size on the primary instance. |
| 40979 | 16 | No | Database is unavailable because it is paused. Server '%.\*ls', Database '%.\*ls'. Please resume before trying to access the database again. |
| 40980 | 16 | No | Partner managed server's managed server/failover group has been dropped. |
| 40981 | 16 | No | Database '%.\*ls' is not accessible due to Azure Key Vault critical error. |
| 40982 | 16 | No | Instance failover group cannot be created because the secondary instance has user databases. |
| 40983 | 16 | No | Replication to the partner managed instance could not be established. Verify that connectivity between the Virtual Networks of the primary and secondary managed servers has been established correctly according to guidelines in https://aka.ms/instanceFailoverGroups. |
| 40984 | 16 | No | Restore and GeoRestore target cannot be free database. |
| 40985 | 16 | No | Restore and GeoRestore are not supported for free database. |
| 40986 | 16 | No | Source database '%.\*ls' dropped on '%.\*ls' does not exist on server '%.\*ls' within the supported recovery period. |
| 40987 | 16 | No | Source database '%.\*ls' does not exist on server '%.\*ls' within the supported recovery period. If restoring a dropped database, please specify its deletion date. |
| 40988 | 16 | No | The Hyperscale edition recovery requires both source and target databases to use Hyperscale service level objective. |
| 40989 | 16 | No | The Hyperscale edition Point-In-Time restore requires both source and target databases to use Hyperscale service level objective. |
| 40990 | 16 | No | Specifying an elastic pool and/or changing the service level objective or edition is not supported for '%.\*ls' edition. |
| 40991 | 16 | No | Changing geo-backup policy is not supported for %s tier of SQL Data Warehouse. |
| 40992 | 16 | No | Cannot perform this operation when the database is paused. |
| 40993 | 16 | No | Enabling long-term retention (LTR) for a serverless database is not supported if auto-pause is enabled. |
| 40994 | 16 | No | Enabling auto-pause for a serverless database is not supported if long-term retention (LTR) is enabled. |
| 40995 | 16 | No | Enabling geo-replication for a serverless database is not supported if auto-pause is enabled. |
| 40996 | 16 | No | Enabling auto-pause for a serverless database is not supported if geo-replication is enabled. |
| 40997 | 16 | No | Elastic pool '%ls' does not exist in server '%ls'. Please make sure that the elastic pool name is entered correctly. Please contact Microsoft support if further assistance is needed. |
| 40998 | 16 | No | The edition '%.\*ls' does not support '%.\*ls' bytes as the database data max size. For more details, see \<\>. |
| 40999 | 16 | No | '%ls' is not a valid service level objective for elastic pool '%ls'. |
| 41000 | 16 | No | Failed to obtain the local Windows Server Failover Clustering (WSFC) handle (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41001 | 16 | No | Failed to obtain local computer name (Error code %d). The supplied buffer may be too small, or there is a system error. For information about this error code, see "System Error Codes" in the operating system documentation. |
| 41002 | 16 | No | Failed to obtain the local Windows Server Failover Clustering (WSFC) node handle (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41003 | 16 | No | Failed to obtain the local Windows Server Failover Clustering (WSFC) node ID (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41004 | 16 | No | Failed to obtain the Windows Server Failover Clustering (WSFC) group handle for cluster group with name or ID '%s' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster group name or ID is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41005 | 16 | No | Failed to obtain the Windows Server Failover Clustering (WSFC) resource handle for cluster resource with name or ID '%s' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster resource name or ID is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41006 | 16 | No | Failed to create the Windows Server Failover Clustering (WSFC) group with name '%s' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster group name is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41007 | 16 | No | The Windows Server Failover Clustering (WSFC) group control API returned error code %d. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41008 | 16 | No | Failed to create the Windows Server Failover Clustering (WSFC) resource with name '%s' and type '%s' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster resource name or type is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41009 | 16 | No | The Windows Server Failover Clustering (WSFC) resource control API returned error code %d. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41010 | 16 | No | Failed to bring the Windows Server Failover Clustering (WSFC) group online (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster group name is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41011 | 16 | No | Failed to take the Windows Server Failover Clustering (WSFC) group offline (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster group name is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41012 | 16 | No | The Windows Server Failover Clustering (WSFC) node control API returned error code %d. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41013 | 16 | No | Failed to obtain the Windows Server Failover Clustering (WSFC) resource enumeration handle (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster resource handle is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41014 | 16 | No | Failed to enumerate the Windows Server Failover Clustering (WSFC) resources (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster resource enumeration handle is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41015 | 16 | No | Failed to obtain the Windows Server Failover Clustering (WSFC) node handle (Error code %d) for node '%.\*ls'. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster node name is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41016 | 16 | No | Failed to remove a node from the possible owner list of a Windows Server Failover Clustering (WSFC) resource (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster resource or node handle is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41017 | 16 | No | Failed to add a node to the possible owner list of a Windows Server Failover Clustering (WSFC) resource (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster resource or node handle is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41018 | 16 | No | Failed to move a Windows Server Failover Clustering (WSFC) group to the local node (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster group or node handle is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41019 | 16 | No | Failed to drop a Windows Server Failover Clustering (WSFC) group with name or ID '%.\*ls' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster group name or ID is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41020 | 16 | No | Failed to find a String property (property name '%s') of the Windows Server Failover Clustering (WSFC) resource with name or ID '%.\*ls' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41021 | 16 | No | Failed to find a DWORD property (property name '%s') of the Windows Server Failover Clustering (WSFC) resource with ID '%.\*ls' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41022 | 16 | No | Failed to create a Windows Server Failover Clustering (WSFC) notification port with notification filter %d and notification key %d (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41023 | 16 | No | The Windows Server Failover Clustering (WSFC) change handle is invalid because a WSFC notification port has not been created or has been closed. Create a new WSFC notification port and retry the operation. |
| 41024 | 16 | No | Failed to register additional Windows Server Failover Clustering (WSFC) change notifications with notification filter %d and notification key %d (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41025 | 16 | No | Failed to receive Windows Server Failover Clustering (WSFC) change notifications (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41026 | 10 | No | Failed to create the Windows Server Failover Clustering (WSFC) group with name '%ls'. The WSFC group with the specified name already exists. Retry the operation with a group name that is unique in the cluster. |
| 41027 | 16 | No | Failed to start the Windows Server Failover Clustering (WSFC) change listener (SQLOS error code %d). SQL Server may not have sufficient resources to start the WSFC change listener. If the condition persists, the SQL Server instance may need to be restarted. |
| 41028 | 16 | No | Failed to open Windows Server Failover Clustering (WSFC) registry root key (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41029 | 16 | No | Failed to open the Windows Server Failover Clustering (WSFC) resource registry key '%.\*ls' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| [41030](../mssqlserver-41030-database-engine-error.md) | 16 | No | Failed to open the Windows Server Failover Clustering registry subkey '%.\*ls' (Error code %d). The parent key is %sthe cluster root key. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. If the corresponding availability group has been dropped, this error is expected. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41031 | 16 | No | Failed to create the Windows Server Failover Clustering (WSFC) registry subkey '%.\*ls' (Error code %d). The parent key is %sthe cluster root key. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41032 | 16 | No | Failed to delete the Windows Server Failover Clustering (WSFC) registry subkey '%.\*ls' (Error code %d). The parent key is %sthe cluster root key. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41033 | 16 | No | Failed to retrieve the Windows Server Failover Clustering (WSFC) registry value corresponding to name '%.\*ls' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41034 | 16 | No | Failed to set the Windows Server Failover Clustering (WSFC) registry value corresponding to name '%.\*ls' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41035 | 16 | No | Failed to enumerate Windows Server Failover Clustering (WSFC) registry value (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41036 | 16 | No | Failed to delete the Windows Server Failover Clustering (WSFC) registry value corresponding to name '%.\*ls' (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41037 | 16 | No | Failed to obtain a Windows Server Failover Clustering (WSFC) object enumeration handle for objects of type %d (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41038 | 16 | No | Failed to enumerate Windows Server Failover Clustering (WSFC) objects (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified cluster object enumeration handle is invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41039 | 16 | No | An availability group replica already exists on the node '%.\*ls'. Each node can contain only one replica of an availability group. Please choose another node to host the new replica. |
| 41040 | 16 | No | Failed to remove the availability group replica '%.\*ls' from availability group '%.\*ls'. The availability group does not contain a replica with the specified name. Verify the availability group and replica names and then retry the operation. |
| 41041 | 16 | No | SQL Server instance to cluster node map entry cannot be found for the SQL Server instance '%.\*ls' and group ID '%.\*ls'. The specified SQL Server instance name is invalid, or the corresponding registry entry does not exist. Verify the SQL Server instance name and retry the operation. |
| 41042 | 16 | No | The availability group '%.\*ls' already exists. This error could be caused by a previous failed CREATE AVAILABILITY GROUP or DROP AVAILABILITY GROUP operation. If the availability group name you specified is correct, try dropping the availability group and then retry CREATE AVAILABILITY GROUP operation. |
| 41043 | 16 | No | For availability group '%.\*ls', the value of the name-to-ID map entry is invalid. The binary value should contain a resource ID, a group ID, and their corresponding lengths in characters. The availability group name may be incorrect, or the availability group configuration data may be corrupt. If this error persists, you may need to drop and recreate the availability group. |
| 41044 | 16 | No | Availability group name to ID map entry for availability group '%.\*ls' cannot be found. The availability group name may be incorrect. If this is a WSFC availability group, the availability group may not exist in this Windows Server Failover Cluster. Verify the availability group exists and that the availability group name is correct and then retry the operation. |
| 41045 | 16 | No | Cannot add database '%.\*ls' to the availability group '%.\*ls', because there is already a database with the same name in the availability group. Please verify that the database and availability group names specified are correct. |
| 41046 | 16 | No | Cannot add replica '%.\*ls' to the availability group '%.\*ls', because there is already a replica with the same name in the availability group. Please verify the replica and availability group names specified are correct. |
| 41047 | 16 | No | Failed to obtain the Windows Server Failover Clustering (WSFC) node state for the local WSFC node (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41048 | 10 | Yes | Always On Availability Groups: Local Windows Server Failover Clustering service has become unavailable. This is an informational message only. No user action is required. |
| 41049 | 10 | Yes | Always On Availability Groups: Local Windows Server Failover Clustering node is no longer online. This is an informational message only. No user action is required. |
| 41050 | 10 | Yes | Always On Availability Groups: Waiting for local Windows Server Failover Clustering service to start. This is an informational message only. No user action is required. |
| 41051 | 10 | Yes | Always On Availability Groups: Local Windows Server Failover Clustering service started. This is an informational message only. No user action is required. |
| 41052 | 10 | Yes | Always On Availability Groups: Waiting for local Windows Server Failover Clustering node to start. This is an informational message only. No user action is required. |
| 41053 | 10 | Yes | Always On Availability Groups: Local Windows Server Failover Clustering node started. This is an informational message only. No user action is required. |
| 41054 | 10 | Yes | Always On Availability Groups: Waiting for local Windows Server Failover Clustering node to come online. This is an informational message only. No user action is required. |
| 41055 | 10 | Yes | Always On Availability Groups: Local Windows Server Failover Clustering node is online. This is an informational message only. No user action is required. |
| 41056 | 16 | No | Availability replica '%.\*ls' of availability group '%.\*ls' cannot be brought online on this SQL Server instance. Another replica of the same availability group is already online on the node. Each node can host only one replica of an availability group, regardless of the number of SQL Server instances on the node. Use the ALTER AVAILABILITY GROUP command to correct the availability group configuration. Then, if the other replica is no longer being hosted on this node, restart this instance of SQL Server to bring the local replica of the availability group online. |
| 41057 | 16 | No | Failed to create the Windows Server Failover Clustering (WSFC) resource with name '%ls'. The WSFC resource with the specified name already exists. Retry the operation with a resource name that is unique in the cluster. |
| 41058 | 10 | No | Always On: The local replica of availability group '%.\*ls' is starting. This is an informational message only. No user action is required. |
| 41059 | 10 | No | Always On: Availability group '%.\*ls' was removed while the availability replica on this instance of SQL Server was offline. The local replica will be removed now. This is an informational message only. No user action is required. |
| 41060 | 16 | No | The Cyclic Redundancy Check (CRC) value generated for the retrieved availability group configuration data does not match that stored with the data for the availability group with ID '%.\*ls'. If this is a WSFC availability group, the availability group data in the WSFC store may have been modified outside SQL Server, or the data is corrupt. If the error persists, you may need to drop and recreate the availability group. |
| 41061 | 10 | No | Always On: The local replica of availability group '%.\*ls' is stopping. This is an informational message only. No user action is required. |
| 41062 | 16 | No | The ID of availability group '%.\*ls' in local data store is inconsistent with that in the Windows Server Failover Clustering (WSFC) data store. The availability group may have been dropped and recreated while the SQL Server instance was offline, or while the WSFC node was down. To resolve this error, drop the availability group and then recreate it. |
| 41063 | 16 | No | Windows Server Failover Clustering (WSFC) detected that the availability group resource with ID '%.\*ls' was online when the availability group was not actually online. The attempt to synchronize the WSFC resource state with the availability group state failed (Error code: %d). For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41064 | 16 | No | Failed to set local node as sole preferred owner for the Windows Server Failover Clustering (WSFC) group with ID '%.\*ls' (Error code: %d). The WSFC group might be in state that cannot accept the request. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41065 | 16 | No | Cannot bring the Windows Server Failover Clustering (WSFC) resource (ID: '%.\*ls') online at this time. The WSFC resource is not in a state that can accept the request. Wait for the WSFC resource to enter a terminal state, and retry the operation. For information about this error, see error code 5023 in "System Error Codes" in the Windows Development documentation. |
| 41066 | 16 | No | Cannot bring the Windows Server Failover Clustering (WSFC) resource (ID '%.\*ls') online (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the WSFC resource may not be in a state that could accept the request. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41067 | 16 | No | Cannot drop the Windows Server Failover Clustering (WSFC) group (ID or name '%.\*ls') at this time. The WSFC group is not in a state that could accept the request. Please wait for the WSFC group to enter a terminal state and then retry the operation. For information about this error, see error code 5023 in "System Error Codes" in the Windows Development documentation. |
| 41068 | 16 | No | Failed to enumerate the Windows Server Failover Clustering (WSFC) registry key (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41069 | 16 | No | The existence of availability group data for the availability group '%.\*ls' in the Windows Server Failover Clustering (WSFC) store could not be determined. The local WSFC node may be down, or a previous CREATE AVAILABILITY GROUP or DROP AVAILABILITY GROUP operation has failed. Please use DROP AVAILABILITY GROUP command to clean up previously failed operations. Verify that the local WSFC node is up before retrying the operation. |
| 41070 | 16 | No | Configuration data for the availability group with Windows Server Failover Clustering (WSFC) resource ID '%.\*ls' is not found in the WSFC data store. The availability group may have been dropped, or a previous CREATE AVAILABILITY GROUP or DROP AVAILABILITY GROUP operation has failed. Please use DROP AVAILABILITY GROUP command to clean up previously failed operations before retrying the current operation. |
| 41071 | 16 | No | Cannot read the persisted configuration of Always On availability group with corresponding resource ID '%.\*ls'. The persisted configuration is written by a higher-version SQL Server that hosts the primary availability replica. Upgrade the local SQL Server instance to allow the local availability replica to become a secondary replica. |
| 41072 | 16 | No | The ID of availability group '%.\*ls' in local data store does not exist in the Windows Server Failover Clustering (WSFC) data store. The availability group may have been dropped but the current WSFC node was not notified. To resolve this error, try to recreate the availability group. |
| 41073 | 16 | No | The database '%.\*ls' cannot be removed from availability group '%.\*ls'. This database does not belong to the availability group. |
| 41074 | 10 | No | Always On: The local replica of availability group '%.\*ls' is preparing to transition to the primary role. This is an informational message only. No user action is required. |
| 41075 | 10 | No | Always On: The local replica of availability group '%.\*ls' is preparing to transition to the resolving role. This is an informational message only. No user action is required. |
| 41076 | 10 | No | Always On: Availability group '%.\*ls' is going offline because it is being removed. This is an informational message only. No user action is required. |
| 41077 | 16 | No | Cannot bring the Windows Server Failover Clustering (WSFC) group (ID '%.\*ls') online at this time. The WSFC group is not in a state that could accept the request. Please wait for the WSFC group to enter a terminal state and then retry the operation. For information about this error, see error code 5023 in "System Error Codes" in the Windows Development documentation. |
| 41078 | 16 | No | Failed to delete the Windows Server Failover Clustering (WSFC) registry value corresponding to name '%.\*ls', because a registry entry with the specified name does not exist. Check that the registry value name is correct, and retry the operation. |
| 41079 | 16 | No | Cannot drop the Windows Server Failover Clustering (WSFC) group (ID or name '%.\*ls'), because the WSFC group does not exist. Specify a valid WSFC group ID or name and retry the operation. For information about this error, see error code 5013 in "System Error Codes" in the Windows Development documentation. |
| 41080 | 16 | No | Failed to delete SQL Server instance name to Windows Server Failover Clustering node name map entry for the local availability replica of availability group '%.\*ls'. The operation encountered SQL Server error %d and has been terminated. Refer to the SQL Server error log for details about this SQL Server error and corrective actions. |
| 41081 | 16 | No | Failed to destroy the Windows Server Failover Clustering group corresponding to availability group '%.\*ls'. The operation encountered SQL Server error %d and has been terminated. Refer to the SQL Server error log for details about this SQL Server error and corrective actions. |
| 41082 | 16 | No | Failed to obtain the name of local Windows Server Failover Cluster (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41083 | 16 | No | Failed to obtain the cluster quorum resource (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41084 | 16 | No | The Windows Server Failover Clustering (WSFC) cluster control API returned error code %d. If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41085 | 16 | No | Failed to find a DWORD property (property name '%s') of the Windows Server Failover Clustering (WSFC) (Error code %d). If this is a WSFC availability group, the WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. Otherwise, contact your primary support provider. For information about this error code, see "System Error Codes" in the Windows Development documentation. |
| 41086 | 16 | No | Failed to retrieve the Paxos tag from the Windows Server Failover Clustering (WSFC) registry hive. The WSFC registry hive might be corrupt. Verify whether the 'HKLM\Cluster\PaxosTag' registry value exists in the WSFC registry hive. |
| 41087 | 16 | No | Error in parsing the Paxos tag from the Windows Server Failover Clustering (WSFC) registry hive. The WSFC registry hive might be corrupt. Verify whether the 'HKLM\Cluster\PaxosTag' is in the format outlined in the Microsoft Knowledge Base article KB 947713 ("The implications of using the /forcequorum switch to start the Cluster service in Windows Server 2008"). |
| 41088 | 16 | No | Failed to determine if the Windows Server Failover Clustering (WSFC) service is in Force Quorum state. The prerequisite hotfix, KB 2494036, might not yet be installed on your Windows Server 2008/Windows Server 2008 R2 systems. For more information, see Microsoft Knowledge Base article KB 2494036 ("A hotfix is available to let you configure a cluster node that does not have quorum votes in Windows Server 2008 and in Windows Server 2008 R2"). |
| 41089 | 10 | Yes | Always On Availability Groups startup has been cancelled, because SQL Server is shutting down. This is an informational message only. No user action is required. |
| 41090 | 10 | No | Failed to update Replica status due to exception %d. |
| 41091 | 10 | No | Always On: The local replica of availability group '%.\*ls' is going offline because either the lease expired or lease renewal failed. This is an informational message only. No user action is required. |
| 41092 | 10 | No | Always On: The availability replica manager is going offline because %ls. This is an informational message only. No user action is required. |
| 41093 | 10 | No | Always On: The local replica of availability group '%.\*ls' is going offline because the corresponding resource in the Windows Server Failover Clustering (WSFC) cluster is no longer online. This is an informational message only. No user action is required. |
| 41094 | 10 | No | Always On: The local replica of availability group '%.\*ls' is restarting because the existing primary replica restarted or the availability group failed over to a new primary replica. This is an informational message only. No user action is required. |
| 41095 | 10 | No | Always On: Explicitly transitioning the state of the Windows Server Failover Clustering (WSFC) resource that corresponds to availability group '%.\*ls' to Failed. The resource state is not consistent with the availability group state in the instance of SQL Server. The WSFC resource state indicates that the local availability replica is the primary replica, but the local replica is not in the primary role. This is an informational message only. No user action is required. |
| 41096 | 10 | No | Always On: The local replica of availability group '%.\*ls' is being removed. The instance of SQL Server failed to validate the integrity of the availability group configuration in the Windows Server Failover Clustering (WSFC) store. This is expected if the availability group has been removed from another instance of SQL Server. This is an informational message only. No user action is required. |
| 41097 | 10 | No | Always On: The local replica of availability group '%.\*ls' is going offline. This replica failed to read the persisted configuration because of a version mismatch. This is an informational message only. No user action is required. |
| 41098 | 10 | No | Always On: The local replica of availability group '%.\*ls' is restarting, because it failed to read the persisted configuration. This is an informational message only. No user action is required. |
| 41099 | 10 | No | Always On: The local replica of availability group '%.\*ls' is going offline. This replica failed to read the persisted configuration, and it has exhausted the maximum for restart attempts. This is an informational message only. No user action is required. |
| 41100 | 16 | No | The availability group '%.\*ls' and/or its local availability replica does not exist. Verify that the specified availability group name is correct, and that the local availability replica has joined the availability group, then retry the operation. |
| 41101 | 16 | No | The availability group with Windows Server Failover Clustering resource ID '%.\*ls' and/or its local availability replica does not exist. Verify that the specified availability resource ID is correct, and that the local availability replica has joined the availability group, then retry the operation. |
| 41102 | 10 | No | Failed to persist configuration data of availability group '%.\*ls'. The local availability replica either is not the primary replica or is shutting down. |
| 41103 | 10 | No | Startup of the Always On Availability Replica Manager has been terminated, because the 'FixQuorum' property of Windows Server Failover Clustering (WSFC) is not present. The prerequisite Hotfix, KB 2494036, might not yet be installed on your Windows Server 2008/Windows Server 2008 R2 systems. For more information, see Microsoft Knowledge Base article KB 2494036 ("A hotfix is available to let you configure a cluster node that does not have quorum votes in Windows Server 2008 and in Windows Server 2008 R2"). |
| 41104 | 16 | No | Failover of the availability group '%.\*ls' to the local replica failed because the availability group resource did not come online due to a previous error. To identify that error, check the SQL Server error log, cluster logs and system event logs. For information about how to view events and logs for a Windows Server Failover Clustering (WSFC) cluster, see Windows Server documentation. |
| 41105 | 16 | No | Failed to create the Windows Server Failover Clustering (WSFC) resource with name '%s' and type '%s'. The resource type is not registered in the WSFC cluster. The WSFC cluster many have been destroyed and created again. To register the resource type in the WSFC cluster, disable and then enable Always On in the SQL Server Configuration Manager. |
| 41106 | 16 | No | Cannot create an availability replica for availability group '%.\*ls'. An availability replica of the specified availability group already exists on this instance of SQL Server. Verify that the specified availability group name is correct and unique, then retry the operation. To remove the existing availability replica, run DROP AVAILABILITY GROUP command. |
| 41107 | 16 | No | Availability group '%.\*ls' failed to create necessary events for the WSFC Lease mechanism. Windows returned error code (%d) when obtaining handles for Lease events. Resolve the windows error and retry the availability group operation. |
| 41108 | 16 | No | An error occurred while removing availability group '%.\*ls'. The DROP AVAILABILITY GROUP command removed the availability group configuration from the local metadata. However, the attempt to remove this configuration from the Windows Server Failover Clustering (WSFC) cluster failed because the Always On Availability Groups manager is not online (SQL Server error: %d). To remove the availability group configuration from the WSFC cluster, re-enter the command. |
| 41109 | 17 | No | Could not enqueue a task (SQL OS error: 0x%x) for process actions for the availability group '%.\*ls'. Most likely, the instance of SQL Server is low on resources. Check the SQL Server error log to determine the cause of the failure. Retry the operation later, and if this condition persists, contact your database administrator. |
| 41110 | 10 | No | Always On: The availability replica manager is starting. This is an informational message only. No user action is required. |
| 41111 | 10 | No | Always On: The availability replica manager is waiting for the instance of SQL Server to allow client connections. This is an informational message only. No user action is required. |
| 41112 | 16 | No | A Windows Server Failover Clustering (WSFC) API required by availability groups has not been loaded. Always On Availability Groups replica manager is not enabled on the local instance SQL Server. If the server instance is running an edition of SQL Server that supports Always On Availability Groups, you can enable the it by using the SQL Server Configuration Manager. |
| 41113 | 16 | No | Cannot failover availability group '%.\*ls' to this instance of SQL Server because a failover command is already pending on the local replica of this availability group. Wait for the pending failover command to complete before issuing another command on the local replica of this availability group. |
| 41114 | 16 | No | Cannot create an availability group named '%.\*ls' because it already exists in a system table. |
| 41115 | 16 | No | Cannot create the availability group named '%.\*ls' because its availability group ID (ID: '%.\*ls') already exists in a system table. |
| 41116 | 16 | No | Cannot create an availability group named '%.\*ls' with replica ID '%.\*ls' because this ID already exists in a system table. |
| 41117 | 16 | No | Cannot map local database ID %d to the availability database ID '%.\*ls' within availability group '%.\*ls'. This database is already mapped to an availability group. |
| 41118 | 16 | No | Cannot map database ID %d to the availability database ID '%.\*ls' within availability group '%.\*ls'. Another local database, (ID %d). is already mapped to this availability database. |
| 41119 | 16 | No | Could not find the availability group ID %d in the system table. |
| 41120 | 16 | No | Failed to start task to process a down notification for the local Windows Server Failover Clustering (WSFC) node (SQL OS error: %d). Possible causes are no worker threads are available or there is insufficient memory. Check the state of the local WSFC node. If this problem persists, you might need to restart the instance of SQL Server. |
| 41121 | 10 | No | The local availability replica of availability group '%.\*ls' cannot accept signal '%s' in its current replica role, '%s', and state (configuration is %s in Windows Server Failover Clustering store, local availability replica has %s joined). The availability replica signal is invalid given the current replica role. When the signal is permitted based on the current role of the local availability replica, retry the operation. |
| 41122 | 16 | No | Cannot failover availability group '%.\*ls' to this instance of SQL Server. The local availability replica is already the primary replica of the availability group. To failover this availability group to another instance of SQL Server, run the failover command on that instance of SQL Server. If local instance of SQL Server is intended to host the primary replica of the availability group, then no action is required. |
| 41123 | 16 | No | Cannot bring the Windows Server Failover Clustering (WSFC) group (ID '%.\*ls') online at this time. The WSFC group is moving to another node. Please wait for the WSFC group to complete the move operation and then retry the command. For information about this error, see error code 5908 in "System Error Codes" in the Windows Development documentation. |
| 41124 | 16 | No | The availability replica for availability group '%.\*ls' on this instance of SQL Server cannot become the primary replica because the availability group is being dropped. |
| 41125 | 16 | No | The availability replica for availability group '%.\*ls' on this instance of SQL Server cannot become the primary replica because the WSFC cluster was started in Force Quorum mode. Consider performing a forced manual failover (with possible data loss). |
| 41126 | 16 | No | Operation on the local availability replica of availability group '%.\*ls' failed. The local copy of the availability group configuration does not exist or has not been initialized. Verify that the availability group exists and that the local copy of the configuration is initialized, and retry the operation. |
| 41127 | 16 | No | Attempt to set database mapping state where the local database id %d is not mapped to any availability group. |
| 41128 | 16 | No | Failed to perform database operation '%s' on database '%.\*ls' (ID %d) in availability group '%.\*ls'. The database might be in an incorrect state for the operation. If the problem persists, you may need to restart the SQL Server instance. |
| 41129 | 16 | No | Failed to schedule or execute database operation '%s' on database '%.\*ls' (Database ID: %d) in availability group '%.\*ls' (SQL OS error: %d). The instance of SQL Server may have insufficient resources to carry out the database operation. If the problem persists, you might need to restart the server instance. |
| 41130 | 16 | No | Operation '%s' on a database '%.\*ls' (Database ID: %d) in availability group '%.\*ls' failed with SQL Server error %d (Error details: "%.\*ls"). The operation has been rolled back. See previous error messages in the SQL Server error log for more details. If the problem persists, you might need to restart the instance of SQL Server. |
| 41131 | 10 | No | Failed to bring availability group '%.\*ls' online. The operation timed out. If this is a Windows Server Failover Clustering (WSFC) availability group, verify that the local WSFC node is online. Then verify that the availability group resource exists in the WSFC cluster. If the problem persists, you might need to drop the availability group and create it again. |
| 41132 | 16 | No | Cannot join database '%.\*ls' to availability group '%.\*ls'. The specified database does not belong to the availability group. Verify the names of the database and the availability group, and retry the command specifying the correct names. |
| 41133 | 10 | No | Cannot remove database '%.\*ls' from availability group '%.\*ls'. Either the database does not belong to the availability group, or the database has not joined the group. Verify the database and availability group names, and retry the command. |
| 41134 | 16 | No | Cannot bring the availability group '%.\*ls' online. The local instance was not the previous primary replica when the availability group went offline, not all databases are synchronized, and no force failover command was issued on the local availability replica. To designate the local availability replica as the primary replica of the availability group, run the force failover command on this instance of SQL Server. |
| 41135 | 10 | No | Startup of Always On Availability Groups replica manager failed due to SQL Server error %d. To determine the cause of this error, check the SQL Server error log for the preceding error. |
| 41136 | 16 | No | Failed to join the availability replica to availability group '%.\*ls' because the group is not online. Either bring the availability group online, or drop and recreate it. Then retry the join operation. |
| 41137 | 10 | No | Abandoning a database operation '%ls' on availability database '%.\*ls' of availability group '%.\*ls'. The sequence number of local availability replica has changed (Previous sequence number: %u, current sequence number: %u). This is an informational message only. No user action is required. |
| 41138 | 17 | No | Cannot accept Always On Availability Groups operation operation on database '%.\*ls' of availability group '%.\*ls'. The database is currently processing another operation that might change the database state. Retry the operation later. If the condition persists, contact the database administrator. |
| 41139 | 10 | No | Failed to set database information for availability group %.\*ls. The local availability replica is not the primary replica, or it is shutting down. This is an informational message only. No user action is required. |
| 41140 | 16 | No | Availability group '%.\*ls' cannot process the ALTER AVAILABILITY GROUP command, because the local availability replica is not the primary replica. Connect to the server instance that is currently hosting the primary replica of this availability group, and rerun the command. |
| 41141 | 16 | No | Failed to set availability group database information for availability group %.\*ls. The local availability replica is not the primary, or is shutting down. This is an informational message only. No user action is required. |
| 41142 | 16 | No | The availability replica for availability group '%.\*ls' on this instance of SQL Server cannot become the primary replica. One or more databases are not synchronized or have not joined the availability group. If the availability replica uses the asynchronous-commit mode, consider performing a forced manual failover (with possible data loss). Otherwise, once all local secondary databases are joined and synchronized, you can perform a planned manual failover to this secondary replica (without data loss). For more information, see SQL Server Books Online. |
| 41143 | 16 | No | Cannot process the operation. The local replica of availability Group '%.\*ls' is in a failed state. A previous operation to read or update persisted configuration data for the availability group has failed. To recover from this failure, either restart the local Windows Server Failover Clustering (WSFC) service or restart the local instance of SQL Server. |
| 41144 | 16 | No | The local availability replica of availability group '%.\*ls' is in a failed state. The replica failed to read or update the persisted configuration data (SQL Server error: %d). To recover from this failure, either restart the local Windows Server Failover Clustering (WSFC) service or restart the local instance of SQL Server. |
| 41145 | 10 | No | Cannot join database '%.\*ls' to availability group '%.\*ls'. The database has already joined the availability group. This is an informational message. No user action is required. |
| 41146 | 16 | No | Failed to bring Availability Group '%.\*ls' online. If this is a WSFC availability group, the Windows Server Failover Clustering (WSFC) service may not be running, or it may not be accessible in its current state. Please verify the local WSFC node is up and then retry the operation. Otherwise, contact your primary source provider. |
| 41147 | 10 | No | Always On Availability Groups was not started because %ls. This is an informational message. No user action is required. |
| 41148 | 16 | No | Cannot add or join database '%.\*ls' to availability group '%.\*ls'. The database does not exist on this instance of SQL Server. Verify the database name and that the database exists on the server instance. Then retry the operation, specifying the correct database name. |
| 41149 | 16 | No | Operation on the availability group '%.\*ls' has been cancelled or terminated, either because of a connection timeout or cancellation by user. This is an informational message. No user action is required. |
| 41150 | 16 | No | Failed to take availability group '%.\*ls' offline. If this is a WSFC availability group, the Windows Server Failover Clustering (WSFC) service may not be running, or it may not be accessible in its current state. Verify the local WSFC node is up and then retry the operation. Otherwise, contact your primary support provider. |
| 41151 | 16 | No | Error accessing the Availability Groups manager. The local Availability Groups manager has not been initialized. Wait until the Availability Groups manager is in a state that allows access, and retry the operation. |
| 41152 | 16 | No | Failed to create availability group '%.\*ls'. The operation encountered SQL Server error %d and has been rolled back. Check the SQL Server error log for more details. When the cause of the error has been resolved, retry CREATE AVAILABILITY GROUP command. |
| 41153 | 16 | No | Failed to create availability group '%.\*ls'. The operation encountered SQL Server error %d. An attempt to roll back the operation failed. Check the SQL Server error log for more details. Then execute the DROP AVAILABILITY GROUP command to clean up any metadata that might remain from the failed attempt to create the availability group. |
| 41154 | 16 | No | Cannot failover availability group '%.\*ls' to this SQL Server instance. The availability group is still being created. Verify that the specified availability group name is correct. Wait for CREATE AVAILABILITY GROUP command to finish, then retry the operation. |
| 41155 | 16 | No | Cannot failover availability group '%.\*ls' to this instance of SQL Server. The availability group is being dropped. Verify that the specified availability group name is correct. The availability group may need to be recreated if the drop operation was unintentional. |
| 41156 | 16 | No | Cannot drop availability group '%.\*ls' from this instance of SQL Server. The availability group is either being dropped, or the local availability replica is being removed from the availability group. Verify that the specified availability group name is correct. Wait for the current operation to complete, then retry the command if necessary. |
| 41157 | 16 | No | Cannot remove the local availability replica from availability group '%.\*ls' from this instance of SQL Server. The availability group is either being dropped, or the local availability replica is being disjoined. Verify that the specified availability group name is correct. Wait for the current operation to complete, then retry the command if necessary. |
| 41158 | 16 | No | Failed to join local availability replica to availability group '%.\*ls'. The operation encountered SQL Server error %d and has been rolled back. Check the SQL Server error log for more details. When the cause of the error has been resolved, retry the ALTER AVAILABILITY GROUP JOIN command. |
| 41159 | 16 | No | Failed to join local availability replica to availability group '%.\*ls'. The operation encountered SQL Server error %d. An attempt to roll back the operation failed. Check SQL Server error log for more details. Run DROP AVAILABILITY GROUP command to clean up any metadata that might remain from the availability group. |
| 41160 | 16 | No | Failed to designate the local availability replica of availability group '%.\*ls' as the primary replica. The operation encountered SQL Server error %d and has been terminated. Check the preceding error and the SQL Server error log for more details about the error and corrective actions. |
| 41161 | 16 | No | Failed to validate the Cyclic Redundancy Check (CRC) of the configuration of availability group '%.\*ls'. The operation encountered SQL Server error %d, and the availability group has been taken offline to protect its configuration and the consistency of its joined databases. Check the SQL Server error log for more details. If configuration data corruption occurred, the availability group might need to be dropped and recreated. |
| 41162 | 16 | No | Failed to validate sequence number of the configuration of availability group '%.\*ls'. The in-memory sequence number does not match the persisted sequence number. The availability group and/or the local availability replica will be restarted automatically. No user action is required at this time. |
| 41163 | 16 | No | An error occurred while waiting for the local availability replica of availability group '%.\*ls' to transition to the primary role. The operation encountered SQL OS error %d and has been terminated. Verify that the availability group is in the correct state for the command. If this is a Windows Server Failover Clustering (WSFC) availability group, also verify that the WSFC cluster is in the correct state for the command. Then retry the command. |
| 41164 | 16 | No | An error occurred while waiting for the local availability replica of availability group '%.\*ls' to transition to the resolving role. The operation encountered SQL OS error %d and has been terminated. Verify that the availability group is in the correct state for the command. If this is a Windows Server Failover Clustering (WSFC) availability group, also verify that the WSFC cluster is in the correct state for the command. Then retry the command. |
| 41165 | 16 | No | A timeout error occurred while waiting to access the local availability replica of availability group '%.\*ls'. The availability replica is currently being accessed by another operation. Wait for the in-progress operation to complete, and then retry the command. |
| 41166 | 16 | No | An error occurred while waiting to access the local availability replica of availability group '%.\*ls'. The operation encountered SQL OS error %d, and has been terminated. Verify that the local availability replica is in the correct state, and then retry the command. |
| 41167 | 16 | No | An error occurred while attempting to access availability replica '%.\*ls' in availability group '%.\*ls'. The availability replica is not found in the availability group configuration. Verify that the availability group and availability replica names are correct, then retry the command. |
| 41168 | 16 | No | An error occurred while attempting to access availability replica with ID '%.\*ls' in availability group '%.\*ls'. The availability replica is not found in the availability group configuration. Verify that the availability group name and availability replica ID are correct, then retry the command. |
| 41169 | 16 | No | An error occurred while attempting to access the availability group database with ID '%.\*ls' in availability group '%.\*ls'. The availability database is not found in the availability group configuration. Verify that the availability group name and availability database ID are correct, then retry the command. |
| 41170 | 10 | No | Post-online processing for availability group '%.\*ls' has been terminated. Either post-online processing has already completed, the local availability replica is no longer the primary replica, or the availability group is being dropped. This is an informational message. No user action is required. |
| 41171 | 16 | No | Failed to create availability group '%.\*ls', because a Windows Server Failover Cluster (WSFC) group with the specified name already exists. The operation has been rolled back successfully. To retry creating an availability group, either remove or rename the existing WSFC group, or retry the operation specifying a different availability group name. |
| 41172 | 16 | No | An error occurred while dropping availability group '%.\*ls'. The operation encountered SQL OS error %d, and has been terminated. Verify that the specified availability group name is correct, and then retry the command. |
| 41173 | 16 | No | An error occurred while removing the local availability replica from availability group '%.\*ls'. The operation encountered SQL OS error %d, and has been terminated. Verify that the specified availability group name is correct, and then retry the command. |
| 41174 | 10 | No | Failed to start the task of the Windows Server Failover Clustering (WSFC) event notification worker (SQL OS error: %d). If the problem persists, you might need to restart the instance of SQL Server. |
| 41175 | 10 | No | Failed to stop the task of WSFC event notification worker (SQL OS error: %d). If the problem persists, you might need to restart the instance of SQL Server. |
| 41176 | 10 | No | Failed to acquire exclusive access to local availability group configuration data (SQL OS error: %d). If the problem persists, you might need to restart the instance of SQL Server. |
| 41177 | 16 | No | The availability replica of the specified availability group '%.\*ls' is being dropped. Wait for the completion of the drop command and retry the operation later. |
| 41178 | 16 | No | Cannot drop availability group '%.\*ls' from this SQL Server instance. The availability group is currently being created. Verify that the specified availability group name is correct. Wait for the current operation to complete, then retry the command if necessary. |
| 41179 | 16 | No | Cannot remove the local availability replica from availability group '%.\*ls' from this instance of SQL Server. The availability group is currently being created. Verify that the specified availability group name is correct. Wait for the current operation to complete, and then retry the command if necessary. |
| 41180 | 16 | No | Attempt to access non-existent or uninitialized availability group with ID '%.\*ls'. This is usually an internal condition, such as the availability group is being dropped or the local WSFC node has lost quorum. In such cases, and no user action is required. |
| 41181 | 16 | No | The local availability replica of availability group '%.\*ls' did not become primary. A concurrent operation may have changed the state of the availability group in Windows Server Failover Cluster. Verify that the availability group state in Windows Server Failover Cluster is correct, then retry the operation. |
| 41182 | 16 | No | Failed to set the local availability replica of availability group '%.\*ls' as joined in Windows Server Failover Clustering (WSFC) database. Either the local availability replica is no longer the primary, or the WSFC service is not accessible. Verify that the local WSFC node is online, and that the local availability replica is the primary replica. Then retry the operation. |
| 41183 | 16 | No | Failed to modify availability replica options for availability group '%.\*ls'. Before the availability group configuration could be updated, the operation encountered SQL Server error %d. The operation has been rolled back. Refer to the SQL Server error log for more information. If this is a Windows Server Failover Clustering (WSFC) availability group, verify that the local WSFC node is online, and retry the command. Otherwise, contact your primary support provider. |
| 41184 | 16 | No | Failed to modify availability replica options for availability group '%.\*ls'. The availability group configuration has been updated. However, the operation encountered SQL Server error %d while applying the new configuration to the local availability replica. The operation has been terminated. Refer to the SQL Server error log for more information. If this is a Windows Server Failover Clustering (WSFC) availability group, verify that the local WSFC node is online. Use the ALTER AVAILABILITY GROUP command to undo the changes made to the availability group configuration. |
| 41185 | 10 | No | Replica option specified in ALTER AVAILABILITY GROUP '%.\*ls' MODIFY DDL is same is cached availability group configuration. |
| 41186 | 16 | No | Availability group '%.\*ls' cannot process an ALTER AVAILABILITY GROUP command at this time. The availability group is still being created. Verify that the specified availability group name is correct. Wait for CREATE AVAILABILITY GROUP command to finish, and then retry the operation. |
| 41187 | 16 | No | Availability group '%.\*ls' cannot process an ALTER AVAILABILITY GROUP command at this time. The availability group is being dropped. Verify that the specified availability group name is correct. The availability group may need to be recreated if it was dropped unintentionally. |
| 41188 | 16 | No | Availability group '%.\*ls' failed to process %s-%s command. The operation encountered SQL Server error %d before the availability group configuration could be updated, and has been rolled back. Refer to the SQL Server error log for details. If this is a Windows Server Failover Clustering (WSFC) availability group, verify that the local WSFC node is online, and then retry the command. Otherwise, contact your primary support provider. |
| 41189 | 16 | No | Availability group '%.\*ls' failed to process the %s-%s command. The availability group configuration has been updated. However, the operation encountered SQL Server error %d while applying the new configuration to the local availability replica, and has been terminated. Refer to the SQL Server error log for details . If this is a Windows Server Failover Clustering (WSFC) availability group, verify that the local WSFC node is online. Use an ALTER AVAILABILITY GROUP command to undo the changes to the availability group configuration. |
| 41190 | 16 | No | Availability group '%.\*ls' failed to process %s-%s command. The local availability replica is not in a state that could process the command. Verify that the availability group is online and that the local availability replica is the primary replica, then retry the command. |
| 41191 | 16 | No | The local availability replica of availability group '%.\*ls' cannot become the primary replica. The last-known primary availability replica is of a higher version than the local availability replica. Upgrade the local instance of SQL Server to the same or later version as the server instance that is hosting the current primary availability replica, and then retry the command. |
| 41192 | 17 | No | Creating and scheduling a worker task for Always On Availability Groups failed due to lack of resources (SQL OS error %d). Processing of new actions might be delayed or stalled until the resource limits are resolved. Reduce the memory or thread count on the instance of SQL Server to allow new threads to get scheduled. If new tasks are scheduled the problem might resolve itself. However, if the problem persists, you might need to restart the local instance of SQL Server. |
| 41193 | 10 | No | Cannot join database '%.\*ls' to availability group '%.\*ls'. The database is in the process of being removed from the availability group. When the remove-database operation completes, the database will no longer be joined to the availability group. Then retry the join-database command. |
| 41194 | 16 | No | An error occurred while waiting for the local availability replica for availability group '%.\*ls' to complete post-online work. The operation encountered SQL OS error %d and has been terminated. Verify that the availability group is in the correct state for the command. If this is a Windows Server Failover Clustering (WSFC) availability group, also verify that the WSFC cluster is in the correct states for the command. Then retry the command. |
| 41195 | 16 | No | Cannot failover availability group '%.\*ls' from instance of SQL Server. The local availability replica is already the secondary replica of the availability group. Run the failaway command on primary instance of SQL Server. If local instance of SQL Server is intended to host the secondary replica of the availability group, then no action is required. |
| 41196 | 16 | No | Failed to create availability group '%.\*ls', because a Windows Server Failover Cluster (WSFC) group with the specified name already exists. An attempt to roll back the operation failed. Check the SQL Server error log for more details. To manually clean up the partially created availability group, run the DROP AVAILABILITY GROUP command. Reenter your CREATE AVAILABILITY GROUP command specifying a unique availability group name. |
| 41197 | 15 | No | The FAILOVER_MODE option has not been specified for the replica '%.\*ls'. Reenter the command, specifying a failover mode for the replica. |
| 41198 | 15 | No | The AVAILABILITY_MODE option has not been specified for the replica '%.\*ls'. Reenter the command, specifying an availability mode for the replica. |
| 41199 | 16 | No | The specified command is invalid because the Always On Availability Groups %ls feature is not supported by this edition of SQL Server. For information about features supported by the editions of SQL Server, see SQL Server Books Online. |
| 41201 | 16 | No | The SEMANTICSIMILARITYTABLE, SEMANTICKEYPHRASETABLE and SEMANTICSIMILARITYDETAILSTABLE functions do not support remote data sources. |
| 41202 | 16 | No | The source table '%.\*ls' specified in the SEMANTICSIMILARITYTABLE, SEMANTICKEYPHRASETABLE or SEMANTICSIMILARITYDETAILSTABLE function doesn't have a full-text index that uses the STATISTICAL_SEMANTICS option. A full-text index using the STATISTICAL_SEMANTICS option is required to use this function. |
| 41203 | 16 | No | The column '%.\*ls' specified in the SEMANTICSIMILARITYTABLE, SEMANTICKEYPHRASETABLE or SEMANTICSIMILARITYDETAILSTABLE function is not full-text indexed with the STATISTICAL_SEMANTICS option. The column must be full-text indexed using the STATISTICAL_SEMANTICS option to be used in this function. |
| 41204 | 16 | No | The source_key parameter is required in the SEMANTICSIMILARITYTABLE function. |
| 41205 | 10 | No | Error %d occurred during semantic index population for table or indexed view '%.\*ls' (table or indexed view ID %d, database ID %d, document ID %d) |
| 41206 | 10 | No | An ALTER FULLTEXT INDEX statement cannot remove the 'STATISTICAL_SEMANTICS' option from the last column in the index that has the option set when a "WITH NO POPULATION" clause is specified. Remove the "WITH NO POPULATION" clause. |
| 41207 | 10 | No | A locale ID that is not supported was specified for a column with 'STATISTICAL_SEMANTICS'. Please verify that the locale ID is correct and that the corresponding language statistics has been installed. |
| 41208 | 10 | No | Warning: The population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d') encountered a document with full-text key value '%ls' that specifies a language not supported for semantic indexing. Some columns of the row will not be part of the semantic index. |
| 41209 | 10 | No | A semantic language statistics database is not registered. Full-text indexes using 'STATISTICAL_SEMANTICS' cannot be created or populated. |
| 41210 | 10 | No | The semantic language statistics database is not accessible or not valid. Full-text indexes using 'STATISTICAL_SEMANTICS' cannot be created or populated. |
| 41211 | 16 | No | A semantic language statistics database is already registered. |
| 41212 | 16 | No | No semantic language statistics database is registered. |
| 41213 | 16 | No | The database '%.\*ls' does not exist or the database format is not valid. Provide a valid semantic language statistics database name. |
| 41214 | 16 | No | An error occurred while trying to register the semantic language statistics database. |
| 41215 | 16 | No | The SEMANTICSIMILARITYTABLE, SEMANTICKEYPHRASETABLE and SEMANTICSIMILARITYDETAILSTABLE functions do not support update or insert. |
| 41216 | 16 | No | The SEMANTICSIMILARITYTABLE, SEMANTICKEYPHRASETABLE and SEMANTICSIMILARITYDETAILSTABLE functions do not support common table expressions. |
| 41300 | 16 | No | The current transaction cannot be committed and cannot support read or write operations. Roll back the transaction. |
| [41301](../mssqlserver-41301-database-engine-error.md) | 17 | No | A transaction dependency failure occurred, and the current transaction can no longer commit. Please retry the transaction. |
| [41302](../mssqlserver-41302-database-engine-error.md) | 16 | No | The current transaction attempted to update a record that has been updated since this transaction started. The transaction was aborted. |
| 41303 | 16 | No | The bucket count for a hash index must be a positive integer not exceeding %d. |
| 41304 | 10 | No | The current value of option '%.\*ls' for table '%.\*ls', index '%.\*ls' is %d. |
| [41305](../mssqlserver-41305-database-engine-error.md) | 17 | No | The current transaction failed to commit due to a repeatable read validation failure. |
| 41306 | 16 | No | The nesting limit of %d for conditional blocks and exception blocks, for natively compiled modules, has been exceeded. Please simplify the module. |
| [41307](../mssqlserver-41307-database-engine-error.md) | 10 | No | Warning: The row size limit of %d bytes for memory optimized tables has been exceeded and will not work on subscribers running SQL Server 2014 or earlier. Please simplify the table definition. |
| 41308 | 21 | No | The database ID %d already exists. |
| 41309 | 16 | No | Unable to load the compiled DLL for database ID %d. |
| 41310 | 16 | No | A file with an invalid format was detected. Check the SQL Server error log for more details. |
| 41311 | 16 | No | File error during C code generation. The error code was %d. |
| 41312 | 16 | No | Unable to call into the C compiler. GetLastError = %d. |
| 41313 | 16 | No | The C compiler encountered a failure. The exit code was %d. |
| 41314 | 16 | No | Conversion of default value for parameter '%.\*ls' failed. Unable to create stored procedure '%.\*ls'. |
| 41315 | 16 | No | Checkpoint operation failed in database '%.\*ls'. |
| 41316 | 16 | No | Restore operation failed for database '%.\*ls' with internal error code '0x%08lx'. |
| 41317 | 16 | No | A user transaction that accesses memory optimized tables or natively compiled modules cannot access more than one user database or databases model and msdb, and it cannot write to master. |
| 41318 | 16 | No | Memory optimized tables and natively compiled modules cannot be accessed from within SQLCLR stored procedures. |
| 41319 | 16 | No | A maximum of %d predicates are allowed in the WHERE clauses of queries in natively compiled modules. |
| 41320 | 16 | No | EXECUTE AS clause is required, and EXECUTE AS CALLER is not supported, with natively compiled modules. |
| 41321 | 16 | No | The memory optimized table '%.\*ls' with DURABILITY=SCHEMA_AND_DATA must have a primary key. |
| 41322 | 16 | No | MAT/PIT export/import encountered a failure for memory optimized table or natively compiled module with object ID %d in database ID %d. The error code was 0x%x. |
| 41323 | 16 | No | The table type '%ls' is not a memory optimized table type and cannot be used in a natively compiled module. |
| 41324 | 16 | No | The memory optimized table variable '%.\*ls' cannot be used in a batch with a 'USE' statement. |
| [41325](../mssqlserver-41325-database-engine-error.md) | 17 | No | The current transaction failed to commit due to a serializable validation failure. |
| 41326 | 16 | No | Memory optimized tables cannot be created in system databases. |
| 41327 | 16 | No | The memory optimized table '%.\*ls' must have at least one index or a primary key. |
| 41328 | 16 | No | A floating point operation has overflowed. |
| 41329 | 16 | No | Unsupported operation following a transaction dependency failure. Parameters and variables cannot be accessed in the CATCH block and the CATCH block must raise an exception following a dependency failure. |
| 41330 | 16 | No | Create database operation failed for database '%.\*ls'. |
| 41331 | 17 | No | The transaction encountered an out-of-memory condition while rolling back to a savepoint, and therefore cannot be committed. Roll back the transaction. |
| [41332](../mssqlserver-41332-database-engine-error.md) | 16 | No | Memory optimized tables and natively compiled modules cannot be accessed or created when the session TRANSACTION ISOLATION LEVEL is set to SNAPSHOT. |
| [41333](../mssqlserver-41333-database-engine-error.md) | 16 | No | The following transactions must access memory optimized tables and natively compiled modules under snapshot isolation: RepeatableRead transactions, Serializable transactions, and transactions that access tables that are not memory optimized in RepeatableRead or Serializable isolation. |
| 41334 | 16 | No | The code generation directory cannot be created or set up correctly. |
| 41335 | 16 | No | Modifying the collation of a database is not allowed when the database contains memory optimized tables or natively compiled modules. |
| 41336 | 16 | No | Unable to clean up all files that are needed for compilation. |
| 41337 | 16 | No | Cannot create %S_MSG. To create %S_MSG, the database must have a MEMORY_OPTIMIZED_FILEGROUP that is online and has at least one container. |
| 41338 | 16 | No | An invalid version of a data file was detected. Check the SQL Server error log for more details. |
| 41339 | 16 | No | The table '%.\*ls' has been created or altered after the start of the current transaction. The transaction was aborted. Please retry the transaction. |
| 41340 | 16 | No | The transaction executed too many insert, update, or delete statements in memory optimized tables. The transaction was terminated. |
| 41341 | 16 | No | Table '%.\*ls' is not yet available on the secondary replica. |
| 41343 | 16 | No | In-Memory OLTP hot switch to primary replica for database '%.\*ls' failed. |
| 41344 | 16 | No | RESTORE DATABASE WITH PARTIAL requires the MEMORY_OPTIMIZED_DATA filegroup if the backup contains a MEMORY_OPTIMIZED_DATA filegroup. |
| 41345 | 16 | No | The key size limit of %d bytes for nonclustered indexes on memory optimized tables has been exceeded. Please simplify the index definition. |
| 41346 | 16 | No | CREATE and UPDATE STATISTICS for memory optimized tables do not support the WHERE clause. |
| 41347 | 16 | No | A BACKUP or RESTORE DATABASE statement that includes the primary filegroup must include the MEMORY_OPTIMIZED_DATA filegroup, and vice versa. |
| 41348 | 16 | No | MEMORY_OPTIMIZED_DATA filegroups cannot be used with the FILESTREAM_ON clause. Specify a FILESTREAM filegroup. |
| [41349](../mssqlserver-41349-database-engine-error.md) | 10 | No | Warning: Encryption was enabled for a database that contains one or more memory optimized tables with durability SCHEMA_AND_DATA. The data in these memory optimized tables will not be encrypted. |
| [41350](../mssqlserver-41350-database-engine-error.md) | 10 | No | Warning: A memory optimized table with durability SCHEMA_AND_DATA was created in a database that is enabled for encryption. The data in the memory optimized table will not be encrypted. |
| 41351 | 16 | No | An error occurred while processing the log. The log version is unsupported. |
| 41352 | 16 | No | The operation on database ID %d cannot complete because the database is in use. |
| 41353 | 16 | No | The operation on database ID %d cannot complete because the database is being dropped or being taken offline. |
| 41354 | 21 | Yes | The XTP background checkpoint thread encountered an unrecoverable error ('%ls') for database '%.\*ls'. The checkpoint process is terminating so that the thread can clean up its resources. This is an informational message only. No user action is required. |
| 41355 | 21 | Yes | An XTP checkpoint operation encountered an error ('%ls') while processing log record ID %S_LSN for database '%.\*ls'. Checkpoint processing has terminated. |
| 41356 | 16 | No | Filegroups with MEMORY_OPTIMIZED_DATA can only be created in 64-bit installations of SQL Server. |
| 41357 | 16 | No | Tables with MEMORY_OPTIMIZED=ON can only be created in 64-bit installations of SQL Server. |
| 41358 | 16 | No | Stored procedures with NATIVE_COMPILATION can only be created in 64-bit installations of SQL Server. |
| [41359](../mssqlserver-41359-database-engine-error.md) | 16 | No | A query that accesses memory optimized tables using the READ COMMITTED isolation level, cannot access disk based tables when the database option READ_COMMITTED_SNAPSHOT is set to ON. Provide a supported isolation level for the memory optimized table using a table hint, such as WITH (SNAPSHOT). |
| 41360 | 16 | No | Default MEMORY_OPTIMIZED_DATA filegroup is not available in database '%.\*ls'. |
| 41361 | 16 | No | The READ_ONLY property of a MEMORY_OPTIMIZED_DATA filegroup cannot be modified. |
| 41362 | 16 | No | FILESTREAM container '%.\*ls' cannot be added. Either the server is out of memory or the container path is too long. |
| 41363 | 16 | No | Only the owner of database '%.\*ls' or users with valid permission can execute the stored procedure '%.\*ls'. |
| 41364 | 16 | No | Stored procedure '%.\*ls' can only be executed on a database that has an online MEMORY_OPTIMIZED_DATA filegroup. |
| [41365](../mssqlserver-41365-database-engine-error.md) | 16 | No | Merge request for transaction range \[%ld, %ld\] on database '%.\*ls' was not scheduled. The checkpoint files represented by the range either cannot be merged, or the checkpoint files are already part of an existing merge operation. |
| 41366 | 16 | No | Merge operation failed because the requested transaction id range is invalid. |
| 41367 | 16 | No | Failed to complete merge operation for transaction range \[%ld, %ld\] for database '%.\*ls'. Error was 0x%08x. |
| [41368](../mssqlserver-41368-database-engine-error.md) | 16 | No | Accessing memory optimized tables using the READ COMMITTED isolation level is supported only for autocommit transactions. It is not supported for explicit or implicit transactions. Provide a supported isolation level for the memory optimized table using a table hint, such as WITH (SNAPSHOT). |
| 41369 | 16 | No | Merge operations cannot be requested on a secondary replica. |
| 41370 | 16 | No | Resource pool '%.\*ls' does not exist or resource governor has not been reconfigured. |
| 41371 | 16 | No | Binding to a resource pool is not supported for system database '%.\*ls'. This operation can only be performed on a user database. |
| 41372 | 16 | No | Database '%.\*ls' is currently bound to a resource pool. A database must be unbound before creating a new binding. |
| 41373 | 16 | No | Database '%.\*ls' cannot be explicitly bound to the resource pool '%.\*ls'. A database can only be bound to a user resource pool. |
| 41374 | 16 | No | Database '%.\*ls' does not have a binding to a resource pool. |
| 41375 | 16 | No | A binding has been created. Take database '%.\*ls' offline and then bring it back online to begin using resource pool '%.\*ls'. |
| 41376 | 16 | No | Only members of the fixed sysadmin role can execute the stored procedure '%ls'. |
| 41377 | 16 | No | The natively compiled module with database ID %ld and object ID %ld has not been executed. Query execution statistics collection can only be enabled if the module has been executed at least once since creation or last database restart. |
| 41378 | 16 | No | Both @database_id and @xtp_object_id should be specified to enable or retrieve status of query level statistics collection for a procedure. |
| 41379 | 16 | No | Restore operation failed for database '%.\*ls' due to insufficient memory in the resource pool '%ls'. Close other applications to increase the available memory, ensure that both SQL Server memory configuration and resource pool memory limits are correct and try again. See 'http://go.microsoft.com/fwlink/?LinkID=507574' for more information. |
| 41380 | 21 | No | Databases with a MEMORY_OPTIMIZED_DATA filegroup are only supported in 64-bit editions of SQL Server. |
| 41381 | 21 | No | The database cannot be started in this edition of SQL Server because it contains a MEMORY_OPTIMIZED_DATA filegroup. See Books Online for more details on feature support in different SQL Server editions. |
| 41382 | 16 | No | Failure adding a container to a MEMORY_OPTIMIZED_DATA filegroup. Possible causes include the server being out of memory and the container path being too long. |
| 41383 | 16 | No | An internal error occurred while running the DMV query. This was likely caused by concurrent DDL operations. Please retry the query. |
| 41384 | 16 | No | Database '%.\*ls' has exceeded the maximum number of XTP checkpoint files, and can no longer support write operations in durable memory-optimized tables. For more information, contact Customer Support Services. |
| 41385 | 16 | No | A memory-optimized table cannot be enabled for Change Data Capture (CDC). |
| 41386 | 16 | No | Filegroups with MEMORY_OPTIMIZED_DATA, memory-optimized tables and natively compiled modules are not supported when lightweight pooling is enabled. Disable lightweight pooling in order to use memory-optimized features. |
| 41387 | 10 | No | Disallowing XTP page allocations for database '%.\*ls' due to insufficient memory in the resource pool '%ls'. See 'http://go.microsoft.com/fwlink/?LinkId=510837' for more information. |
| 41388 | 16 | No | An upgrade operation failed for database '%.\*ls'. Check the error log for additional details. |
| 41389 | 16 | No | Failed to create a backup file collection snapshot necessary for backup of database '%.\*ls'. Check the error log for additional details. |
| 41390 | 16 | No | Failed to get checkpoint file information necessary for backup of database '%.\*ls'. Check the error log for additional details. |
| 41391 | 23 | No | Backup of database '%.\*ls' detected a missing MEMORY_OPTIMIZED_DATA file with ID '%.\*ls'. |
| 41392 | 16 | No | Cannot ALTER a natively compiled module into a non-native module. Include WITH NATIVE_COMPILATION or drop and recreate the module. |
| 41393 | 16 | No | Cannot ALTER a non-native module into a natively compiled module. Omit WITH NATIVE_COMPILATION or drop and recreate the module. |
| 41394 | 16 | No | TEXTSIZE must be a number between -1 and 2147483647. |
| 41395 | 10 | No | Rebuilding the log file is not supported for databases containing memory-optimized tables. |
| [41396](../mssqlserver-41396-database-engine-error.md) | 16 | No | The sort operation exceeded the buffer limit. The stored procedure execution was aborted. Consult SQL Server Books Online for more information. |
| 41397 | 16 | No | The TOP operator can return a maximum of %d sorted rows. %d was requested. Reduce the number of rows selected or simplify the query. |
| 41398 | 16 | No | The TOP operator can return a maximum of %d rows; %d was requested. |
| [41399](../mssqlserver-41399-database-engine-error.md) | 16 | No | The sort operation is too complex. Consult SQL Server Books Online for more information. |
