---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    18002    |    20    |    Yes    |    Exception happened when running extended stored procedure '%.*ls' in the library '%.*ls'. SQL Server is terminating process %d. Exception type: %ls; Exception code: 0x%lx.    |
|    18052    |    16    |    No    |    Error: %d, Severity: %d, State: %d.    |
|    18053    |    16    |    No    |    Error: %d, Severity: %d, State: %d. (Params:%ls). The error is printed in terse mode because there was error during formatting. Tracing, ETW, notifications etc are skipped.    |
|    18054    |    16    |    Yes    |    Error %d, severity %d, state %d was raised, but no message with that error number was found in sys.messages. If error is larger than 50000, make sure the user-defined message is added using sp_addmessage.    |
|    18055    |    20    |    Yes    |    Exception %d, %d occurred when the server tried to reset connection %d. Because the server cannot recover from the failure to reset the connection, the connection has been dropped. Please contact Microsoft technical support.    |
|    18056    |    20    |    Yes    |    The client was unable to reuse a session with SPID %d, which had been reset for connection pooling. The failure ID is %d. This error may have been caused by an earlier operation failing. Check the error logs for failed operations immediately before this error message.    |
|    18057    |    20    |    Yes    |    Error: Failed to set up execution context.    |
|    18058    |    17    |    Yes    |    Failed to load format string for error %d, language id %d. Operating system error: %s. Check that the resource file matches SQL Server executable, and resource file in localized directory matches the file under English directory. Also check memory usage.    |
|    18059    |    16    |    Yes    |    The connection has been dropped because the principal that opened it subsequently assumed a new security context, and then tried to reset the connection under its impersonated security context. This scenario is not supported. See "Impersonation Overview" in Books Online.    |
|    18060    |    16    |    Yes    |    Failed to format string for error %d, language id %d. This may be caused by low memory in server, or error happening while formatting the message.    |
|    18061    |    20    |    Yes    |    The client was unable to join a session with SPID %d. This error may have been caused by an earlier operation failing or a change in permissions since the session was established. Check the error logs for failed operations immediately before this error message.    |
|    18100    |    10    |    Yes    |    Process ID %d was killed by hostname %.*ls, host process ID %d.    |
|    18113    |    10    |    Yes    |    SQL Server shutdown after verifying system indexes.    |
|    18124    |    10    |    Yes    |    The default collation was successfully changed.    |
|    18204    |    16    |    Yes    |    %s: Backup device '%s' failed to %s. Operating system error %s.    |
|    18210    |    16    |    Yes    |    %s: %s failure on backup device '%s'. Operating system error %s.    |
|    18225    |    10    |    Yes    |    Tape '%s' (family ID %d, sequence %d, media_set_guid %s) is mounted on tape drive '%s'. This is an informational message only. No user action required    |
|    18226    |    10    |    Yes    |    Tape mount is being requested on drive '%s'. Expected volume has (Family ID %d, sequence %d).    |
|    18227    |    10    |    Yes    |    Unnamed tape (family ID %d, sequence %d, media_set_guid %s) is mounted on tape drive '%s'. This is an informational message only. No user action required.    |
|    18228    |    10    |    Yes    |    Tape mount request on drive '%s' is cancelled. This is an informational message only. No user action is required.    |
|    18257    |    10    |    Yes    |    %s: Device or media does not support %s. To access this feature, use a different device or media.    |
|    [18264](../../relational-databases/errors-events/mssqlserver-18264-database-engine-error.md)    |    10    |    Yes    |    Database backed up. Database: %s, creation date(time): %s(%s), pages dumped: %I64d, first LSN: %s, last LSN: %s, number of dump devices: %d, device information: (%s). This is an informational message only. No user action is required.    |
|    18265    |    10    |    Yes    |    Log was backed up. Database: %s, creation date(time): %s(%s), first LSN: %s, last LSN: %s, number of dump devices: %d, device information: (%s). This is an informational message only. No user action is required.    |
|    18266    |    10    |    Yes    |    Database file was backed up. Database: %s, creation date(time): %s(%s), file list: (%s), pages dumped: %I64d, number of dump devices: %d, device information: (%s). This is an informational message only. No user action is required.    |
|    18267    |    10    |    Yes    |    Database was restored: Database: %s, creation date(time): %s(%s), first LSN: %s, last LSN: %s, number of dump devices: %d, device information: (%s). Informational message. No user action required.    |
|    18268    |    10    |    Yes    |    Log was restored. Database: %s, creation date(time): %s(%s), first LSN: %s, last LSN: %s, number of dump devices: %d, device information: (%s). This is an informational message. No user action is required.    |
|    18269    |    10    |    Yes    |    Database file was restored. Database: %s, creation date(time): %s(%s), file list: (%s), number of dump devices: %d, device information: (%s). This is an informational message. No user action is required.    |
|    18270    |    10    |    Yes    |    Database differential changes were backed up. Database: %s, creation date(time): %s(%s), pages dumped: %I64d, first LSN: %s, last LSN: %s, full backup LSN: %s, number of dump devices: %d, device information: (%s). This is an informational message. No user action is required.    |
|    18271    |    10    |    Yes    |    Database changes were restored. Database: %s, creation date(time): %s(%s), first LSN: %s, last LSN: %s, number of dump devices: %d, device information: (%s). This is an informational message. No user action is required.    |
|    18272    |    16    |    Yes    |    During restore restart, an I/O error occurred on checkpoint file '%s' (operating system error %s). The statement is proceeding but cannot be restarted. Ensure that a valid storage location exists for the checkpoint file.    |
|    18273    |    16    |    Yes    |    Could not clear '%s' bitmap in database '%s' because of error %d. As a result, the differential or bulk-logged bitmap overstates the amount of change that will occur with the next differential or log backup. This discrepancy might slow down later differential or log backup operations and cause the backup sets to be larger than necessary. Typically, the cause of this error is insufficient resources. Investigate the failure and resolve the cause. If the error occurred on a data backup, consider taking a data backup to create a new base for future differential backups.    |
|    18274    |    10    |    Yes    |    Tape '%s' was dismounted from drive '%s'. This is an informational message. No user action is required.    |
|    18275    |    10    |    Yes    |    Unnamed tape was dismounted from drive '%s'. This is an informational message. No user action is required.    |
|    18276    |    10    |    Yes    |    Database file differential changes were backed up. Database: %s, creation date(time): %s(%s), file list: (%s), pages dumped: %I64d, number of dump devices: %d, device information: (%s). This is an informational message only. No user action is required.    |
|    18277    |    10    |    Yes    |    Database file changes were restored. Database: %s, creation date(time): %s(%s), file list: (%s), number of dump devices: %d, device information: (%s). This is an informational message only. No user action is required.    |
|    18300    |    10    |    No    |    Reason: Infrastructure error occurred. Check for previous errors.    |
|    18301    |    10    |    No    |    Reason: Could not find a login matching the name provided.    |
|    18302    |    10    |    No    |    Reason: Failed to unprotect memory containing sensitive information.    |
|    18303    |    10    |    No    |    Reason: Failed to unprotect memory containing sensitive information.    |
|    18304    |    10    |    No    |    Reason: Could not find a login matching the name provided.    |
|    18305    |    10    |    No    |    Reason: Attempting to use an NT account name with SQL Server Authentication.    |
|    18306    |    10    |    No    |    Reason: An error occurred while evaluating the password.    |
|    18307    |    10    |    No    |    Reason: Password did not match that for the login provided.    |
|    18308    |    10    |    No    |    Reason: Invalid password provided.    |
|    18309    |    10    |    No    |    Reason: Password validation failed with an infrastructure error. Check for previous errors.    |
|    18310    |    10    |    No    |    Reason: Token-based server access validation failed with an infrastructure error. Check for previous errors.    |
|    18311    |    10    |    No    |    Reason: Login-based server access validation failed with an infrastructure error. Check for previous errors.    |
|    18312    |    10    |    No    |    Reason: SQL Server service is paused. No new connections can be accepted at this time.    |
|    18313    |    10    |    No    |    Reason: Interface for login to SQL Server is not supported.    |
|    18314    |    10    |    No    |    Reason: Failed to open the specified database.    |
|    18315    |    10    |    No    |    Reason: Failed to open the database for this login.    |
|    18316    |    10    |    No    |    Reason: Unable to determine the initial language and date format.    |
|    18317    |    10    |    No    |    Reason: The user must change the password, but it cannot be changed with the current connection settings.    |
|    18318    |    10    |    No    |    Reason: Failed to unprotect memory containing sensitive information.    |
|    18319    |    10    |    No    |    Reason: Simulation of a failure while redoing login on the connection.    |
|    18320    |    10    |    No    |    Reason: SQL Server service is paused. Login could not be revalidated at this time.    |
|    18321    |    10    |    No    |    Reason: Reinitialization of security context failed while revalidating the login on the connection.    |
|    18322    |    10    |    No    |    Reason: Access to server validation failed while revalidating the login on the connection.    |
|    18323    |    10    |    No    |    Reason: Failed to open the specified database while revalidating the login on the connection.    |
|    18324    |    10    |    No    |    Reason: Failed to create the user instance while revalidating the login on the connection.    |
|    18325    |    10    |    No    |    Reason: Failed to attach the specified database while revalidating the login on the connection.    |
|    18326    |    10    |    No    |    Reason: Failed to open the database for this login while revalidating the login on the connection.    |
|    18327    |    10    |    No    |    Reason: Failed to determine the language and date format while revalidating the login on the connection.    |
|    18328    |    10    |    No    |    Reason: An exception was raised while revalidating the login on the connection. Check for previous errors.    |
|    18329    |    10    |    No    |    Reason: Simulation of a failure while reauthenticating login.    |
|    18330    |    10    |    No    |    Reason: SQL Server service is paused. Login cannot be reauthenticated at this time.    |
|    18331    |    10    |    No    |    Reason: Failed to reinitialize security context while reauthenticating login.    |
|    18332    |    10    |    No    |    Reason: Failed to access server for validation while reauthenticating login.    |
|    18333    |    10    |    No    |    Reason: Failed to open the specified database while reauthenticating login.    |
|    18334    |    10    |    No    |    Reason: An error occurred while reauthenticating login. Check for previous errors.    |
|    18335    |    10    |    No    |    Reason: Could not retrieve database name or map database to an item.    |
|    18336    |    10    |    No    |    Reason: Cannot connect with a login that does not specify a share.    |
|    18337    |    10    |    No    |    Reason: Failed to open the explicitly specified database.    |
|    18338    |    10    |    No    |    Reason: Unable to determine the database name from the specified file name.    |
|    18339    |    10    |    No    |    Reason: Failed to open the database specified in the login properties.    |
|    18340    |    10    |    No    |    Reason: Failed to store database name and collation. Check for previous errors.    |
|    18341    |    10    |    No    |    . Reason: Current collation did not match the database's collation during connection reset.    |
|    18342    |    10    |    No    |    Reason: Failed to send an environment change notification to a log shipping partner node.    |
|    18343    |    10    |    No    |    Reason: Failed to retrieve database name or map database to an item while revalidating the login on the connection.    |
|    18344    |    10    |    No    |    Reason: Connection with a login which does not specify a share is not allowed while revalidating the login on the connection.    |
|    18345    |    10    |    No    |    Reason: Failed to open the database configured in the login object while revalidating the login on the connection.    |
|    18346    |    10    |    No    |    Reason: Failed to determine database name from a given file name while revalidating the login on the connection.    |
|    18347    |    10    |    No    |    Reason: Failed to open the database specified in the login properties while revalidating the login on the connection.    |
|    18348    |    10    |    No    |    Reason: Failed to store database name and collation while revalidating the login on the connection. Check for previous errors.    |
|    18349    |    10    |    No    |    Reason: Current collation did not match the database's collation during connection reset.    |
|    18350    |    10    |    No    |    Reason: Failed to send an environment change notification to a log shipping partner node while revalidating the login.    |
|    18351    |    10    |    No    |    Reason: Client impersonation failed.    |
|    18352    |    10    |    No    |    Reason: Failed to revert impersonation to self.    |
|    18353    |    10    |    No    |    Reason: Failed to get security token information.    |
|    18354    |    10    |    No    |    Reason: Failed to duplicate of security token.    |
|    18355    |    10    |    No    |    Reason: Failed attempted retry of a process token validation.    |
|    18356    |    10    |    No    |    Reason: An error occurred while attempting to change password.    |
|    18357    |    10    |    No    |    Reason: An attempt to login using SQL Server Authentication failed. Server is configured for Windows Authentication only.    |
|    18400    |    16    |    Yes    |    The background checkpoint thread has encountered an unrecoverable error. The checkpoint process is terminating so that the thread can clean up its resources. This is an informational message only. No user action is required.    |
|    18401    |    14    |    Yes    |    Login failed for user '%.*ls'. Reason: Server is in script upgrade mode. Only administrator can connect at this time.%.*ls    |
|    18451    |    14    |    Yes    |    Login failed for user '%.*ls'. Only administrators may connect at this time.%.*ls    |
|    [18452](../../relational-databases/errors-events/mssqlserver-18452-database-engine-error.md)    |    14    |    Yes    |    Login failed. The login is from an untrusted domain and cannot be used with Windows Authentication.%.*ls    |
|    18453    |    10    |    Yes    |    Login succeeded for user '%.*ls'. Connection made using Windows Authentication.%.*ls    |
|    18454    |    10    |    Yes    |    Login succeeded for user '%.*ls'. Connection made using SQL Server Authentication.%.*ls    |
|    18455    |    10    |    Yes    |    Login succeeded for user '%.*ls'.%.*ls    |
|    [18456](../../relational-databases/errors-events/mssqlserver-18456-database-engine-error.md)    |    14    |    Yes    |    Login failed for user '%.*ls'.%.*ls%.*ls    |
|    18458    |    14    |    Yes    |    Login failed. The number of simultaneous users already equals the %d registered licenses for this server. To increase the maximum number of simultaneous users, obtain additional licenses and then register them through the Licensing item in Control Panel.%.*ls    |
|    18459    |    14    |    Yes    |    Login failed. The workstation licensing limit for SQL Server access has already been reached.%.*ls    |
|    18460    |    14    |    Yes    |    Login failed. The number of simultaneous users has already reached the limit of %d licenses for this '%ls' server. Additional licenses should be obtained and installed or you should upgrade to a full version.%.*ls    |
|    18461    |    14    |    Yes    |    Login failed for user '%.*ls'. Reason: Server is in single user mode. Only one administrator can connect at this time.%.*ls    |
|    18462    |    14    |    No    |    The login failed for user "%.*ls". The password change failed. The password for the user is too recent to change. %.*ls    |
|    18463    |    14    |    No    |    The login failed for user "%.*ls". The password change failed. The password cannot be used at this time. %.*ls    |
|    18464    |    14    |    No    |    Login failed for user '%.*ls'. Reason: Password change failed. The password does not meet Windows policy requirements because it is too short.%.*ls    |
|    18465    |    14    |    No    |    Login failed for user '%.*ls'. Reason: Password change failed. The password does not meet Windows policy requirements because it is too long.%.*ls    |
|    18466    |    14    |    No    |    Login failed for user '%.*ls'. Reason: Password change failed. The password does not meet Windows policy requirements because it is not complex enough.%.*ls    |
|    18467    |    14    |    No    |    The login failed for user "%.*ls". The password change failed. The password does not meet the requirements of the password filter DLL. %.*ls    |
|    18468    |    14    |    No    |    The login failed for user "%.*ls". The password change failed. An unexpected error occurred during password validation. %.*ls    |
|    18469    |    10    |    No    |    [CLIENT: %.*hs]    |
|    18470    |    14    |    Yes    |    Login failed for user '%.*ls'. Reason: The account is disabled.%.*ls    |
|    18471    |    14    |    No    |    The login failed for user "%.*ls". The password change failed. The user does not have permission to change the password. %.*ls    |
|    [18482](../../relational-databases/errors-events/mssqlserver-18482-database-engine-error.md)    |    16    |    Yes    |    Could not connect to server '%.*ls' because '%.*ls' is not defined as a remote server. Verify that you have specified the correct server name. %.*ls.    |
|    [18483](../../relational-databases/errors-events/mssqlserver-18483-database-engine-error.md)    |    16    |    Yes    |    Could not connect to server '%.*ls' because '%.*ls' is not defined as a remote login at the server. Verify that you have specified the correct login name. %.*ls.    |
|    18485    |    16    |    Yes    |    Could not connect to server '%.*ls' because it is not configured to accept remote logins. Use the remote access configuration option to allow remote logins.%.*ls    |
|    18486    |    14    |    Yes    |    Login failed for user '%.*ls' because the account is currently locked out. The system administrator can unlock it. %.*ls    |
|    18487    |    14    |    Yes    |    Login failed for user '%.*ls'. Reason: The password of the account has expired.%.*ls    |
|    18488    |    14    |    Yes    |    Login failed for user '%.*ls'. Reason: The password of the account must be changed.%.*ls    |
|    18489    |    10    |    No    |    The dedicated administrator connection is in use by "%.*ls" on "%.*ls".%.*ls    |
|    18491    |    16    |    Yes    |    SQL Server could not start because of an invalid serial number. The serial number information retrieved at startup appears invalid. To proceed, reinstall SQL Server.    |
|    18492    |    16    |    Yes    |    SQL Server cannot start because the license agreement for this '%ls' version of SQL Server is invalid. The server is exiting. To proceed, reinstall SQL Server with a valid license.    |
|    18493    |    16    |    Yes    |    The user instance login flag is not supported on this version of SQL Server. The connection will be closed.%.*ls    |
|    18494    |    16    |    Yes    |    The user instance login flag is not allowed when connecting to a user instance of SQL Server. The connection will be closed.%.*ls    |
|    18495    |    16    |    Yes    |    The user instance login flag cannot be used along with an attach database file name. The connection will be closed.%.*ls    |
|    18496    |    10    |    Yes    |    System Manufacturer: '%ls', System Model: '%ls'.    |
|    18596    |    16    |    No    |    %.*ls cannot start because your system is low on memory.    |
|    18597    |    16    |    No    |    Your %.*ls installation is either corrupt or has been tampered with (%hs). Please uninstall then re-run setup to correct this problem    |
|    18598    |    16    |    No    |    %.*ls could not find the default instance (%.*ls) - error %d. Please specify the name of an existing instance on the invocation of sqlservr.exe.\n\nIf you believe that your installation is corrupt or has been tampered with, uninstall then re-run setup to correct this problem.    |
|    18599    |    16    |    No    |    %.*ls could not find the specified named instance (%.*ls) - error %d. Please specify the name of an existing instance on the invocation of sqlservr.exe.\n\nIf you believe that your installation is corrupt or has been tampered with, uninstall then re-run setup to correct this problem.    |
|    18750    |    16    |    No    |    %ls: The parameter '%ls' is not valid.    |
|    18751    |    16    |    No    |    %ls procedure was called with the wrong number of parameters.    |
|    [18752](../../relational-databases/errors-events/mssqlserver-18752-database-engine-error.md)    |    16    |    No    |    Only one Log Reader Agent or log-related procedure (sp_repldone, sp_replcmds, and sp_replshowcmds) can connect to a database at a time. If you executed a log-related procedure, drop the connection over which the procedure was executed or execute sp_replflush over that connection before starting the Log Reader Agent or executing another log-related procedure.    |
|    18755    |    16    |    No    |    Could not allocate memory for replication. Verify that SQL Server has sufficient memory for all operations.    |
|    18756    |    16    |    No    |    Could not retrieve replication information for table %d. Verify that the table has a primary key, and then rerun the Log Reader Agent.    |
|    18757    |    16    |    No    |    Unable to execute procedure. The database is not published. Execute the procedure in a database that is published for replication.    |
|    18760    |    16    |    No    |    Invalid %ls statement for article %d. Verify that the stored procedures that propagate changes to Subscribers use the appropriate call syntax, and then rerun the Log Reader Agent. Use sp_helparticle and sp_changearticle to view and change the call syntax.    |
|    18761    |    16    |    No    |    Commit record at {%08lx:%08lx:%04lx} has already been distributed.    |
|    18762    |    16    |    No    |    Invalid begin LSN {%08lx:%08lx:%04lx} for commit record {%08lx:%08lx:%04lx}. Check DBTABLE.    |
|    18763    |    16    |    No    |    Commit record {%08lx:%08lx:%04lx} reports oldest active LSN as (0:0:0).    |
|    18764    |    16    |    No    |    Execution of filter stored procedure %d failed. See the SQL Server errorlog for more information.    |
|    18765    |    16    |    No    |    The "%s" log sequence number (LSN) that was specified for the replication log scan is invalid.    |
|    18766    |    16    |    No    |    The replbeginlsn field in the DBTABLE is invalid.    |
|    18767    |    16    |    No    |    The specified begin LSN {%08lx:%08lx:%04lx} for replication log scan occurs before replbeginlsn {%08lx:%08lx:%04lx}.    |
|    18768    |    16    |    No    |    The specified LSN {%08lx:%08lx:%04lx} for repldone log scan occurs before the current start of replication in the log {%08lx:%08lx:%04lx}.    |
|    18769    |    16    |    No    |    The specified LSN {%08lx:%08lx:%04lx} for repldone log scan is not a replicated commit record.    |
|    18770    |    16    |    No    |    The specified LSN {%08lx:%08lx:%04lx} for repldone log scan is not present in the transaction log.    |
|    18771    |    16    |    No    |    Invalid storage type %d specified writing variant of type %d.    |
|    18772    |    16    |    No    |    Invalid server data type (%d) specified in repl type lookup.    |
|    18773    |    16    |    No    |    Could not locate text information records for the column "%.*ls", ID %d during command construction.    |
|    18774    |    16    |    No    |    The stored procedure %s must be executed within a transaction.    |
|    18775    |    16    |    No    |    The Log Reader Agent encountered an unexpected log record of type %u encountered while processing DML operation.    |
|    18776    |    16    |    No    |    An error occurred while waiting on the article cache access event.    |
|    18777    |    16    |    No    |    %s: Error initializing MSMQ components    |
|    18778    |    16    |    No    |    %s: Error opening Microsoft Message Queue %s    |
|    18780    |    16    |    No    |    You have specified a value for the \@dts_package_password parameter. You must also specify a value for the \@dts_package_name parameter.    |
|    18781    |    16    |    No    |    The value specified for the \@backupdevicetype parameter is not valid. The value must be 'logical', 'disk', or 'tape'.    |
|    18782    |    16    |    No    |    Could not locate backup header information for database '%s' in the specified backup device. Specify a backup device that contains a backup of the Publisher database.    |
|    18783    |    16    |    No    |    The subscription setup script path has been truncated, because the snapshot folder directory path is too long. Reconfigure the Distributor to use a shorter path for this Publisher, and then retry the operation.    |
|    18784    |    16    |    No    |    The alternate snapshot folder path generated by replication has been truncated. Reconfigure the publication to use a shorter alternate snapshot folder path, and then retry the operation.    |
|    18786    |    16    |    No    |    The specified publication does not allow subscriptions to be initialized from a backup. To allow initialization from a backup, use sp_changepublication: set 'allow_initialize_from_backup' to 'true'.    |
|    18787    |    16    |    No    |    Snapshot publications cannot use the option to initialize a subscription from a backup. This option is only supported for transactional publications.    |
|    18790    |    16    |    No    |    Cannot enable the option to initialize a subscription from a backup. This is not supported for non-SQL Server Publishers; it is only supported for transactional publications from SQL Server Publishers.    |
|    18795    |    16    |    No    |    The valid new types of a log based indexed view article are 'indexed view logbased', 'indexed view logbased manualfilter', 'indexed view logbased manualview', and 'indexed view logbased manualboth' only.    |
|    18796    |    16    |    No    |    The valid new types of a log based table article are 'logbased', 'logbased manualfilter', 'logbased manualview', and 'logbased manualboth' only.    |
|    18799    |    16    |    No    |    Only users who are members of the following roles can perform this operation: sysadmin fixed server role; dbowner or dbcreator fixed database role in the current database.    |
|    18801    |    16    |    No    |    Unable to allocate memory for replication schema version node.    |
|    18802    |    16    |    No    |    Cannot insert a new schema change into the systranschemas system table. HRESULT = '0x%x'. If the problem persists, contact Customer Support Services.    |
|    18803    |    16    |    No    |    The topic %.*ls is not a supported help topic. To see the list of supported topics, execute the stored procedure sp_replhelp N'helptopics'.    |
|    18804    |    16    |    No    |    Peer-to-peer replication has been enabled, and the Log Reader Agent was unable to find an Extended-Originator-Record (EOR) for a transaction that did not originate at this server. Contact Customer Support Services.    |
|    18805    |    16    |    No    |    The Log-Scan Process failed to construct a replicated command from log sequence number (LSN) {%08lx:%08lx:%04lx}. Back up the publication database and contact Customer Support Services.    |
|    18806    |    16    |    No    |    Cannot initialize the replication resource. Ensure that SQL Server has sufficient memory. If the problem persists, restart SQL Server.    |
|    18807    |    16    |    No    |    Cannot find an object ID for the replication system table '%s'. Verify that the system table exists and is accessible by querying it directly. If it does exist, stop and restart the Log Reader Agent; if it does not exist, drop and reconfigure replication.    |
|    18808    |    16    |    No    |    Article information is not valid. Stop the Log Reader Agent, execute the stored procedure sp_replflush, and then restart the Log Reader Agent.    |
|    18809    |    16    |    No    |    END_UPDATE log record {%08lx:%08lx:%04lx} encountered without matching BEGIN_UPDATE.    |
|    18810    |    16    |    No    |    Cannot restart the scan on table '%s'. HRESULT = '0x%x'. Stop and restart the Log Reader Agent. If the problem persists, contact Customer Support Services.    |
|    18811    |    16    |    No    |    Invalid %s log record.    |
|    18812    |    16    |    No    |    Can not lock the database object in article cache.    |
|    18815    |    16    |    No    |    Expecting %I64d bytes of data, but only %I64d were found in the transaction log. For more information, contact Customer Support Services.    |
|    18817    |    16    |    No    |    Text information block not valid. Contact Customer Support Services.    |
|    18818    |    16    |    No    |    Failed to scan to log sequence number (LSN) {%08lx:%08lx:%04lx}. Contact Customer Support Services.    |
|    18819    |    16    |    No    |    Failed to lock the current log record at log sequence number (LSN) {%08lx:%08lx:%04lx}. Contact Customer Support Services.    |
|    18821    |    16    |    No    |    The rowset does not contain any column with offset %d. Back up the publication database and contact Customer Support Services.    |
|    18823    |    16    |    No    |    Invalid value %d for %s.    |
|    18826    |    16    |    No    |    Failed to delete rows from the systranschemas table. HRESULT = '0x%x'. The rows will be deleted the next time replication executes the stored procedure sp_replcmds.    |
|    18827    |    16    |    No    |    The Log Reader Agent scanned to the end of the log before processing all transactions in the hash table. %d transactions in the hash table, %d transactions processed, end of log LSN {%08lx:%08lx:%04lx}. Back up the publication database and contact Customer Support Services.    |
|    18828    |    16    |    No    |    Invalid filter procedure definition.    |
|    18829    |    16    |    No    |    Failed to scan to the delete log record of an update base on log sequence number (LSN) {%08lx:%08lx:%04lx}. Contact Customer Support Services.    |
|    18830    |    16    |    No    |    A bounded update was logged within the range of another bounded update within the same transaction. First BEGIN_UPDATE {%08lx:%08lx:%04lx}, current BEGIN_UPDATE {%08lx:%08lx:%04lx}. Contact Customer Support Services.    |
|    18832    |    16    |    No    |    The Log Reader Agent scanned to the end of the log while processing a bounded update. BEGIN_UPDATE LSN {%08lx:%08lx:%04lx}, END_UPDATE LSN {%08lx:%08lx:%04lx}, current LSN {%08lx:%08lx:%04lx}. Back up the publication database and contact Customer Support Services.    |
|    18834    |    16    |    No    |    An unexpected Text Information Begin (TIB) log record was encountered while processing the TIB for offset %ld. Last TIB processed: (textInfoFlags 0x%x, coloffset %ld, newSize %I64d, oldSize %I64d). Contact Customer Support Services.    |
|    18835    |    16    |    No    |    Encountered an unexpected Text Information End (TIE) log record. Last Text Information Begin (TIB) processed: (textInfoFlags 0x%x, coloffset %ld, newSize %I64d, oldSize %I64d), text collection state %d. Contact product support.    |
|    18836    |    16    |    No    |    %s, ti: {RowsetId %I64d, {TextTimeStamp %I64d, {RowId {PageId %ld, FileId %u}, SlotId %d}}, coloffset %ld, textInfoFlags 0x%x, textSize %I64d, offset %I64d, oldSize %I64d, newSize %I64d}.    |
|    18837    |    16    |    No    |    Cannot find rowset ID %I64d in the current schema. Stop and restart the Log Reader Agent. If the problem persists, reinitialize all subscriptions to the publication.    |
|    18838    |    16    |    No    |    The Log Reader Agent encountered a NULL command that is not valid. Restart the agent if it has stopped. If the problem persists, reinitialize all subscriptions to the publication.    |
|    18840    |    16    |    No    |    Cannot locate database information in the article cache. Stop and restart SQL Server and the Log Reader Agent. If the problem persists, back up the publication database, and then contact Customer Support Services.    |
|    18842    |    16    |    No    |    Failed to retrieve the oldest active log sequence number (LSN) from a commit record. Stop and restart SQL Server and the Log Reader Agent. If the problem persists, reinitialize all subscriptions to the publication.    |
|    18843    |    16    |    No    |    Failed to allocate or reallocate buffer for replication command, old size %d, new size %d.    |
|    18844    |    16    |    No    |    Invalid compensation range: begin {%08lx:%08lx:%04lx}, end {%08lx:%08lx:%04lx}. Reinitialize all subscriptions to the publication.    |
|    18845    |    16    |    No    |    Cannot retrieve the rowset ID from log records generated from a text pointer based operation. Reinitialize all subscriptions to the publication.    |
|    18846    |    16    |    No    |    Possible inconsistent state in the distribution database: dist_backup_lsn {%08lx:%08lx:%04lx}, dist_last_lsn {%08lx:%08lx:%04lx}. Execute "sp_repldone NULL, NULL, 0, 0, 1", and then execute sp_replflush. Reinitialize all subscriptions to the publication.    |
|    18847    |    16    |    No    |    Cannot retrieve the peer-to-peer database information. Contact Customer Support Services.    |
|    18849    |    16    |    No    |    Failed to evaluate the filter procedure or computed column. Cannot find the column offset information for column ID %d, rowsetId %I64d. Stop and restart the Log Reader Agent. If the problem persists, back up the publication database and then contact Customer Support Services.    |
|    18850    |    16    |    No    |    Unexpected %s log record encountered, last FILESTREAMInfo node processed : {%d, {{%I64d, %I64d}, %I64d, %I64d, %d, %d}, %d, %ld, %I64d, %I64d, %I64d, %I64d, {%08lx:%08lx:%04lx}, %d, {{%I64d, %I64d}, %I64d, %I64d, %d, %d}, {%08lx:%08lx:%04lx}}    |
|    18851    |    16    |    No    |    Failed to %s the replication context for TxF: {%I64d, %.*ls, %ld, %ld, %I64d, %I64d, %I64d, %I64d, {%08lx:%08lx:%04lx}, %I64d, %.*ls, {%08lx:%08lx:%04lx}}. If the problem persists, contact product support.    |
|    18852    |    16    |    No    |    Failed to read the TXF_REPLICATION_RECORD_WRITE structure. Last error returned '%ld'. If the problem persists, contact Customer Support Services.    |
|    18853    |    10    |    No    |    Replication is skipping schema version logging because the systranschemas table is not present in database '%d'. This is an informational message only. No user action is required.    |
|    18854    |    16    |    No    |    One or more subscriptions have been marked inactive. Drop and re-create all subscriptions for this node that are failing with this error.    |
|    18855    |    11    |    No    |    Can not rename the database name because it is published or it is a distribution database used by replication.    |
|    18856    |    16    |    No    |    Agent '%s' is retrying after an error. %d retries attempted. See agent job history in the Jobs folder for more details.    |
|    18857    |    16    |    No    |    The subscription to this publication is not active yet. No user action is required.    |
|    18896    |    16    |    No    |    Failed to compare delete and insert log record for column ID %ld with table ID %ld    |
