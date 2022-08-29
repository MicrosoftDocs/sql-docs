---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    15001    |    16    |    No    |    Object '%ls' does not exist or is not a valid object for this operation.    |
|    15002    |    16    |    No    |    The procedure '%s' cannot be executed within a transaction.    |
|    15003    |    16    |    No    |    Only members of the %s role can execute this stored procedure.    |
|    15004    |    16    |    No    |    Name cannot be NULL.    |
|    15005    |    10    |    No    |    Statistics for all tables have been updated.    |
|    15006    |    16    |    No    |    '%s' is not a valid name because it contains invalid characters.    |
|    15007    |    16    |    No    |    '%s' is not a valid login or you do not have permission.    |
|    15008    |    16    |    No    |    User '%s' does not exist in the current database.    |
|    15009    |    16    |    No    |    The object '%s' does not exist in database '%s' or is invalid for this operation.    |
|    15010    |    16    |    No    |    The database '%s' does not exist. Supply a valid database name. To see available databases, use sys.databases.    |
|    15011    |    16    |    No    |    Database option '%s' does not exist. Specify a valid database option.    |
|    15012    |    16    |    No    |    The device '%s' does not exist. Use sys.backup_devices to show available devices.    |
|    15013    |    10    |    No    |    Table '%s': No columns without statistics found.    |
|    15014    |    16    |    No    |    The role '%s' does not exist in the current database.    |
|    15015    |    16    |    No    |    The server '%s' does not exist. Use sp_helpserver to show available servers.    |
|    15016    |    16    |    No    |    The default '%s' does not exist.    |
|    15017    |    16    |    No    |    The rule '%s' does not exist.    |
|    15018    |    10    |    No    |    Table '%s': Creating statistics for the following columns:    |
|    15019    |    16    |    No    |    The extended stored procedure '%s' does not exist.    |
|    15020    |    10    |    No    |    Statistics have been created for the %d listed columns of the above tables.    |
|    15021    |    16    |    No    |    Invalid value given for parameter %s. Specify a valid parameter value.    |
|    15022    |    16    |    No    |    The specified user name is already aliased.    |
|    15023    |    16    |    No    |    User, group, or role '%s' already exists in the current database.    |
|    15025    |    16    |    No    |    The server principal '%s' already exists.    |
|    15026    |    16    |    No    |    Logical device '%s' already exists.    |
|    15028    |    16    |    No    |    The server '%s' already exists.    |
|    15032    |    16    |    No    |    The database '%s' already exists. Specify a unique database name.    |
|    15033    |    16    |    No    |    '%s' is not a valid official language name.    |
|    15034    |    16    |    No    |    The application role password must not be NULL.    |
|    15036    |    16    |    No    |    The data type '%s' does not exist or you do not have permission.    |
|    15040    |    16    |    No    |    User-defined error messages must have an ID greater than 50000.    |
|    15041    |    16    |    No    |    User-defined error messages must have a severity level between 1 and 25.    |
|    15042    |    10    |    No    |    The \@with_log parameter is ignored for messages that are not us_english version.    |
|    15043    |    16    |    No    |    You must specify 'REPLACE' to overwrite an existing message.    |
|    15044    |    16    |    No    |    The type "%s" is an unknown backup device type. Use the type "disk" or "tape".    |
|    15045    |    16    |    No    |    The logical name cannot be NULL.    |
|    15046    |    16    |    No    |    The physical name cannot be NULL.    |
|    15048    |    10    |    No    |    Valid values of the database compatibility level are %d, %d, or %d.    |
|    15049    |    11    |    No    |    Cannot unbind from '%s'. Use ALTER TABLE DROP CONSTRAINT.    |
|    15050    |    11    |    No    |    Cannot bind default '%s'. The default must be created using the CREATE DEFAULT statement.    |
|    15051    |    11    |    No    |    Cannot rename the table because it is published for replication.    |
|    15053    |    16    |    No    |    Objects exist which are not owned by the database owner.    |
|    15054    |    10    |    No    |    The current compatibility level is %d.    |
|    15056    |    10    |    No    |    The suspect flag on the database "%s" is already reset.    |
|    15057    |    16    |    No    |    List of %s name contains spaces, which are not allowed.    |
|    15058    |    16    |    No    |    List of %s has too few names.    |
|    15059    |    16    |    No    |    List of %s has too many names.    |
|    15060    |    16    |    No    |    List of %s names contains name(s) which have '%s' non-alphabetic characters.    |
|    15061    |    16    |    No    |    The add device request was denied. A physical device named "%s" already exists. Only one backup device may refer to any physical device name.    |
|    15062    |    16    |    No    |    The guest user cannot be mapped to a login name.    |
|    15063    |    16    |    No    |    The login already has an account under a different user name.    |
|    15065    |    16    |    No    |    All user IDs have been assigned.    |
|    15066    |    16    |    No    |    A default-name mapping of a remote login from remote server '%s' already exists.    |
|    15068    |    16    |    No    |    A remote user '%s' already exists for remote server '%s'.    |
|    15069    |    16    |    No    |    One or more users are using the database. The requested operation cannot be completed.    |
|    15070    |    10    |    No    |    Object '%s' was successfully marked for recompilation.    |
|    15071    |    16    |    No    |    Usage: sp_addmessage \<msgnum\>,\<severity\>,\<msgtext\> [,\<language\> [,FALSE \| TRUE [,REPLACE]]]    |
|    15072    |    16    |    No    |    Usage: sp_addremotelogin remoteserver [,loginname [,remotename]]    |
|    15074    |    10    |    No    |    Warning: You must recover this database prior to access.    |
|    15076    |    16    |    No    |    Default, table, and user data types must be in the current database.    |
|    15077    |    16    |    No    |    Rule, table, and user data type must be in the current database.    |
|    15078    |    16    |    No    |    The table or view must be in the current database.    |
|    15079    |    10    |    No    |    Queries processed: %d.    |
|    15080    |    16    |    No    |    Cannot use parameter %s for a Windows login.    |
|    15081    |    16    |    No    |    Membership of the public role cannot be changed.    |
|    15083    |    16    |    No    |    Physical data type '%s' does not accept a collation    |
|    15084    |    16    |    No    |    The column or user data type must be in the current database.    |
|    15085    |    16    |    No    |    Usage: sp_addtype name, 'data type' [,'NULL' \| 'NOT NULL']    |
|    15096    |    16    |    No    |    Could not find object '%ls' or you do not have required permission or the object is not valid for adding extended property.    |
|    15097    |    16    |    No    |    The size associated with an extended property cannot be more than 7,500 bytes.    |
|    15098    |    16    |    No    |    The name change cannot be performed because the SID of the new name does not match the old SID of the principal.    |
|    15099    |    16    |    No    |    The MUST_CHANGE option cannot be used when CHECK_EXPIRATION is OFF.    |
|    15100    |    16    |    No    |    Usage: sp_bindefault defaultname, objectname [, 'futureonly']    |
|    15101    |    16    |    No    |    Cannot bind a default to a computed column, a sparse column, or to a column of the following data types: timestamp, varchar(max), nvarchar(max), varbinary(max), xml, or CLR type.    |
|    15102    |    16    |    No    |    Cannot bind a default to an identity column.    |
|    15103    |    16    |    No    |    Cannot bind a default to a column created with or altered to have a default value.    |
|    15104    |    16    |    No    |    You do not own a table named '%s' that has a column named '%s'.    |
|    15106    |    16    |    No    |    Usage: sp_bindrule rulename, objectname [, 'futureonly']    |
|    15107    |    16    |    No    |    Cannot bind a rule to a computed column, a sparse column, or to a column of the following data types: text, ntext, image, timestamp, varchar(max), nvarchar(max), varbinary(max), xml, or user-defined data type.    |
|    15108    |    16    |    No    |    sp_addtype cannot be used to define user-defined data types for varchar(max), nvarchar(max) or varbinary(max) data types. Use CREATE TYPE for this purpose.    |
|    15109    |    16    |    No    |    Cannot change the owner of the master, model, tempdb or distribution database.    |
|    15110    |    16    |    No    |    The proposed new database owner is already a user or aliased in the database.    |
|    15112    |    11    |    No    |    The third parameter for table option 'text in row' is invalid. It should be 'on', 'off', '0', or a number from 24 through 7000.    |
|    15113    |    16    |    No    |    Too many failed login attempts. This account has been temporarily locked as a precaution against password guessing. A system administrator can unlock this login with the UNLOCK clause of ALTER LOGIN.    |
|    15114    |    16    |    No    |    Password validation failed. The password for the user is too recent to change.    |
|    15115    |    16    |    No    |    Password validation failed. The password cannot be used at this time.    |
|    15116    |    16    |    No    |    Password validation failed. The password does not meet Windows policy requirements because it is too short.    |
|    15117    |    16    |    No    |    Password validation failed. The password does not meet Windows policy requirements because it is too long.    |
|    15118    |    16    |    No    |    Password validation failed. The password does not meet Windows policy requirements because it is not complex enough.    |
|    15119    |    16    |    No    |    Password validation failed. The password does not meet the requirements of the password filter DLL.    |
|    15120    |    16    |    No    |    An unexpected error occurred during password validation.    |
|    15121    |    16    |    No    |    An error occurred during the execution of %ls. A call to '%ls' failed with error code: '%d'.    |
|    15122    |    16    |    No    |    The CHECK_EXPIRATION option cannot be used when CHECK_POLICY is OFF.    |
|    15123    |    16    |    No    |    The configuration option '%s' does not exist, or it may be an advanced option.    |
|    15124    |    16    |    No    |    The configuration option '%s' is not unique.    |
|    15125    |    16    |    No    |    Trigger '%s' is not a trigger for '%s'.    |
|    15127    |    16    |    No    |    Cannot set the default language to a language ID not defined in syslanguages.    |
|    15128    |    16    |    No    |    The CHECK_POLICY and CHECK_EXPIRATION options cannot be turned OFF when MUST_CHANGE is ON.    |
|    15129    |    16    |    No    |    '%d' is not a valid value for configuration option '%s'.    |
|    15130    |    16    |    No    |    There already exists a '%s' trigger for '%s'.    |
|    15131    |    16    |    No    |    Usage: sp_dbremove \<dbname\> [,dropdev]    |
|    15133    |    16    |    No    |    INSTEAD OF trigger '%s' cannot be associated with an order.    |
|    15134    |    16    |    No    |    No alias exists for the specified user.    |
|    15135    |    16    |    No    |    Object is invalid. Extended properties are not permitted on '%s', or the object does not exist.    |
|    15136    |    16    |    No    |    The database principal is set as the execution context of one or more procedures, functions, or event notifications and cannot be dropped.    |
|    15137    |    16    |    No    |    An error occurred during the execution of sp_xp_cmdshell_proxy_account. Possible reasons: the provided account was invalid or the '%.*ls' credential could not be created. Error code: '%d'.    |
|    15138    |    16    |    No    |    The database principal owns a %S_MSG in the database, and cannot be dropped.    |
|    15141    |    16    |    No    |    The server principal owns one or more %S_MSG(s) and cannot be dropped.    |
|    15143    |    16    |    No    |    '%s' is not a valid option for the \@updateusage parameter. Enter either 'true' or 'false'.    |
|    15144    |    16    |    No    |    The role has members. It must be empty before it can be dropped.    |
|    15145    |    16    |    No    |    An implicit %S_MSG creation has failed. Reason: The %S_MSG may have been dropped or its name may already be in use.    |
|    15146    |    16    |    No    |    An encryption password must be provided to encrypt the private key of this %S_MSG.    |
|    15147    |    16    |    No    |    No decryption password should be provided because the private key of this %S_MSG is encrypted by a master key.    |
|    15148    |    16    |    No    |    The data type or table column '%s' does not exist or you do not have permission.    |
|    15149    |    16    |    No    |    Principal doesn't exist or doesn't have sufficient privileges.    |
|    15150    |    16    |    No    |    Cannot %S_MSG the %S_MSG '%.*ls'.    |
|    15151    |    16    |    No    |    Cannot %S_MSG the %S_MSG '%.*ls', because it does not exist or you do not have permission.    |
|    15152    |    16    |    No    |    Cannot update user instances. Reason: %ls. Error code: 0x%x.    |
|    15153    |    16    |    No    |    The xp_cmdshell proxy account information cannot be retrieved or is invalid. Verify that the '%.*ls' credential exists and contains valid information.    |
|    15154    |    16    |    No    |    The database principal owns an %S_MSG and cannot be dropped.    |
|    15155    |    16    |    No    |    The server principal owns a %S_MSG and cannot be dropped.    |
|    15156    |    16    |    No    |    The password that you specified is too long. The password should have no more than %d characters.    |
|    15157    |    16    |    No    |    Setuser failed because of one of the following reasons: the database principal '%.*ls' does not exist, its corresponding server principal does not have server access, this type of database principal cannot be impersonated, or you do not have permission.    |
|    15158    |    16    |    No    |    Cannot initialize security.    |
|    15159    |    16    |    No    |    Maximum impersonation nesting level exceeded (limit %d)..    |
|    15160    |    16    |    No    |    Cannot issue impersonation token from non-primary impersonation context or for non-Windows user.    |
|    15161    |    16    |    No    |    Cannot set application role '%.*ls' because it does not exist or the password is incorrect.    |
|    15162    |    16    |    No    |    Unexpected error while creating impersonation token.    |
|    15163    |    16    |    No    |    Invalid timeout value. Valid timeout is between 1 and 7200 sec.    |
|    15164    |    16    |    No    |    '%.*ls' is not a valid login or cannot be issued impersonation token.    |
|    15165    |    16    |    No    |    Could not find object '%ls' or you do not have permission.    |
|    15166    |    10    |    No    |    Warning: User types created via sp_addtype are contained in dbo schema. The \@owner parameter if specified is ignored.    |
|    15167    |    16    |    No    |    Cannot generate GUID.    |
|    15168    |    16    |    No    |    Cannot rename the view '%s' and its columns and indexes because it is a system generated view that was created for optimization purposes.    |
|    15169    |    16    |    No    |    The server option "%ls" is not available in this edition of SQL Server.    |
|    15170    |    16    |    No    |    This login is the owner of %ld job(s). You must delete or reassign these jobs before the login can be dropped.    |
|    15171    |    16    |    No    |    Cannot use the parameter "%s" for a certificate or asymmetric key login.    |
|    15172    |    16    |    No    |    FallBack certificate must be created or dropped in master database in single user mode.    |
|    15173    |    16    |    No    |    Login '%s' has granted one or more permission(s). Revoke the permission(s) before dropping the login.    |
|    15174    |    16    |    No    |    Login '%s' owns one or more database(s). Change the owner of the database(s) before dropping the login.    |
|    15175    |    16    |    No    |    Login '%s' is aliased or mapped to a user in one or more database(s). Drop the user or alias before dropping the login.    |
|    15176    |    16    |    No    |    The only valid \@parameter value is 'WITH_LOG'.    |
|    15177    |    16    |    No    |    Usage: sp_dropmessage \<msg number\> [,\<language\> \| 'ALL']    |
|    15178    |    16    |    No    |    Cannot drop or alter a message with an ID less than 50,000.    |
|    15179    |    16    |    No    |    The message number %u or specified language version does not exist.    |
|    15182    |    16    |    No    |    Cannot disable access to the guest user in master or tempdb.    |
|    15183    |    16    |    No    |    The database principal owns objects in the database and cannot be dropped.    |
|    15184    |    16    |    No    |    The database principal owns data types in the database and cannot be dropped.    |
|    15185    |    16    |    No    |    There is no remote user '%s' mapped to local user '%s' from the remote server '%s'.    |
|    15186    |    16    |    No    |    The server principal is set as the execution context of a trigger or event notification and cannot be dropped.    |
|    15187    |    10    |    No    |    The %S_MSG cannot be dropped because it is used by one or more %S_MSG(s).    |
|    15188    |    16    |    No    |    Cannot create an index that does not include all security columns.    |
|    15189    |    16    |    No    |    Cannot have more than one security column for a table.    |
|    15190    |    16    |    No    |    There are still remote logins or linked logins for the server '%s'.    |
|    15192    |    16    |    No    |    Cannot alter or drop the security column of a table.    |
|    15195    |    16    |    No    |    The MUST_CHANGE option is not supported by this version of Microsoft Windows.    |
|    15196    |    16    |    No    |    The current security context is non-revertible. The "Revert" statement failed.    |
|    15197    |    16    |    No    |    There is no text for object '%s'.    |
|    15198    |    16    |    No    |    The name supplied (%s) is not a user, role, or aliased login.    |
|    15199    |    16    |    No    |    The current security context cannot be reverted. Please switch to the original database where '%ls' was called and try it again.    |
|    15200    |    16    |    No    |    There are no remote servers defined.    |
|    15201    |    16    |    No    |    There are no remote logins for the remote server '%s'.    |
|    15202    |    16    |    No    |    There are no remote logins defined.    |
|    15203    |    16    |    No    |    There are no remote logins for '%s'.    |
|    15204    |    16    |    No    |    There are no remote logins for '%s' on remote server '%s'.    |
|    15205    |    16    |    No    |    There are no servers defined.    |
|    15206    |    16    |    No    |    Invalid Remote Server Option: '%s'.    |
|    15207    |    16    |    No    |    The trusted option in remote login mapping is no longer supported.    |
|    15208    |    16    |    No    |    The certificate, asymmetric key, or private key file does not exist or has invalid format.    |
|    15209    |    16    |    No    |    An error occurred during encryption.    |
|    15212    |    16    |    No    |    Invalid certificate subject. The certificate subject must have between 1 and %d characters.    |
|    15213    |    16    |    No    |    Warning: The certificate you created has an invalid validity period; its expiration date precedes its start date.    |
|    15214    |    16    |    No    |    Warning: The certificate you created is expired.    |
|    15215    |    16    |    No    |    Warning: The certificate you created is not yet valid; its start date is in the future.    |
|    15216    |    16    |    No    |    '%s' is not a valid option for the \@delfile parameter.    |
|    15217    |    16    |    No    |    Property cannot be updated or deleted. Property '%.*ls' does not exist for '%.*ls'.    |
|    15218    |    16    |    No    |    Object '%s' is not a table.    |
|    15219    |    16    |    No    |    Cannot change the owner of an indexed view.    |
|    15222    |    16    |    No    |    Remote login option '%s' is not unique.    |
|    15223    |    11    |    No    |    Error: The input parameter '%s' is not allowed to be null.    |
|    15224    |    11    |    No    |    Error: The value for the \@newname parameter contains invalid characters or violates a basic restriction (%s).    |
|    15225    |    11    |    No    |    No item by the name of '%s' could be found in the current database '%s', given that \@itemtype was input as '%s'.    |
|    15226    |    16    |    No    |    Cannot create CLR types from an XML datatype.    |
|    15227    |    16    |    No    |    The database '%s' cannot be renamed.    |
|    15229    |    16    |    No    |    The argument specified for the "%.*ls" parameter of stored procedure sp_db_vardecimal_storage_format is not valid. Valid arguments are 'ON' or 'OFF'.    |
|    15230    |    16    |    No    |    Error starting user instance. Error code: %d.    |
|    15232    |    16    |    No    |    A certificate with name '%s' already exists or this certificate already has been added to the database.    |
|    15233    |    16    |    No    |    Property cannot be added. Property '%.*ls' already exists for '%.*ls'.    |
|    15234    |    16    |    No    |    Objects of this type have no space allocated.    |
|    15236    |    16    |    No    |    Column '%s' has no default.    |
|    15237    |    16    |    No    |    User data type '%s' has no default.    |
|    15238    |    16    |    No    |    Column '%s' has no rule.    |
|    15239    |    16    |    No    |    User data type '%s' has no rule.    |
|    15240    |    16    |    No    |    Cannot write into file '%s'. Verify that you have write permissions, that the file path is valid, and that the file does not already exist.    |
|    15241    |    16    |    No    |    Usage: sp_dboption [dbname [,optname [,'true' \| 'false']]]    |
|    15242    |    16    |    No    |    Database option '%s' is not unique.    |
|    15243    |    16    |    No    |    The option '%s' cannot be changed for the master database.    |
|    15244    |    16    |    No    |    Only members of the sysadmin role or the database owner may set database options.    |
|    15246    |    16    |    No    |    Cannot dump the private key of certificate '%s' because the private key cannot be found.    |
|    15247    |    16    |    No    |    User does not have permission to perform this action.    |
|    15248    |    11    |    No    |    Either the parameter \@objname is ambiguous or the claimed \@objtype (%s) is wrong.    |
|    15249    |    11    |    No    |    Error: Explicit \@objtype '%s' is unrecognized.    |
|    15250    |    16    |    No    |    The database name component of the object qualifier must be the name of the current database.    |
|    15251    |    16    |    No    |    Invalid '%s' specified. It must be %s.    |
|    15252    |    16    |    No    |    The primary or foreign key table name must be given.    |
|    15253    |    11    |    No    |    Syntax error parsing SQL identifier '%s'.    |
|    15254    |    16    |    No    |    Users other than the database owner or guest exist in the database. Drop them before removing the database.    |
|    15255    |    11    |    No    |    '%s' is not a valid value for \@autofix. The only valid value is 'auto'.    |
|    15256    |    16    |    No    |    Usage: sp_certify_removable \<dbname\> [,'auto']    |
|    15257    |    16    |    No    |    The database that you are attempting to certify cannot be in use at the same time.    |
|    15258    |    16    |    No    |    The database must be owned by a member of the sysadmin role before it can be removed.    |
|    15259    |    16    |    No    |    The DEFAULT_SCHEMA clause cannot be used with a Windows group or with principals mapped to certificates or asymmetric keys.    |
|    15260    |    16    |    No    |    The format of the security descriptor string '%s' is invalid.    |
|    15261    |    16    |    No    |    Usage: sp_create_removable \<dbname\>,\<syslogical\>,\<sysphysical\>,\<syssize\>,\<loglogical\>,\<logphysical\>,\<logsize\>,\<datalogical1\>,\<dataphysical1\>,\<datasize1\> [,\<datalogical2\>,\<dataphysical2\>,\<datasize2\>...\<datalogical16\>,\<dataphysical16\>,\<datasize16\>]    |
|    15262    |    10    |    No    |    Invalid file size entered. All files must be at least 1 MB.    |
|    15263    |    16    |    No    |    A SID in the security descriptor string '%s' could not be found in an account lookup operation.    |
|    15264    |    16    |    No    |    Could not create the '%s' portion of the database.    |
|    15265    |    16    |    No    |    An unexpected error has occurred in the processing of the security descriptor string '%s'.    |
|    15266    |    16    |    No    |    Cannot make '%s' database removable.    |
|    15267    |    16    |    No    |    A security descriptor with name '%s' already exists.    |
|    15268    |    10    |    Yes    |    Authentication mode is %s.    |
|    15269    |    16    |    No    |    Logical data device '%s' not created.    |
|    15271    |    16    |    No    |    Invalid \@with_log parameter value. Valid values are 'true' or 'false'.    |
|    15272    |    10    |    No    |    The %s '%.*s' is not trusted to execute.    |
|    15273    |    10    |    No    |    The decryption key is incorrect.    |
|    15274    |    16    |    No    |    Access to the remote server is denied because the current security context is not trusted.    |
|    15276    |    16    |    No    |    Cannot provision master key passwords for system databases.    |
|    15277    |    16    |    No    |    The only valid \@parameter_value values are 'true' or 'false'.    |
|    15278    |    16    |    No    |    Login '%s' is already mapped to user '%s' in database '%s'.    |
|    15279    |    16    |    No    |    You must add the us_english version of this message before you can add the '%s' version.    |
|    15280    |    16    |    No    |    All localized versions of this message must be dropped before the us_english version can be dropped.    |
|    15281    |    10    |    No    |    SQL Server blocked access to %S_MSG '%ls' of component '%.*ls' because this component is turned off as part of the security configuration for this server. A system administrator can enable the use of '%.*ls' by using sp_configure. For more information about enabling '%.*ls', see "Surface Area Configuration" in SQL Server Books Online.    |
|    15282    |    10    |    No    |    A key with name '%.*ls' or user defined unique identifier already exists or you do not have permissions to create it.    |
|    15283    |    16    |    No    |    The name '%s' contains too many characters.    |
|    15284    |    16    |    No    |    The database principal has granted or denied permissions to objects in the database and cannot be dropped.    |
|    15285    |    16    |    No    |    The special word '%s' cannot be used for a logical device name.    |
|    15286    |    16    |    No    |    Terminating this procedure. The \@action '%s' is unrecognized. Try 'REPORT', 'UPDATE_ONE', or 'AUTO_FIX'.    |
|    15287    |    16    |    No    |    Terminating this procedure. '%s' is a forbidden value for the login name parameter in this procedure.    |
|    15288    |    10    |    No    |    Please specify one decryptor to decrypt a key.    |
|    15289    |    16    |    No    |    Terminating this procedure. Cannot have an open transaction when this is run.    |
|    15291    |    16    |    No    |    Terminating this procedure. The %s name '%s' is absent or invalid.    |
|    15292    |    10    |    No    |    The row for user '%s' will be fixed by updating its login link to a login already in existence.    |
|    15293    |    10    |    No    |    Barring a conflict, the row for user '%s' will be fixed by updating its link to a new login.    |
|    15294    |    10    |    No    |    The number of orphaned users fixed by adding new logins and then updating users was %d.    |
|    15295    |    10    |    No    |    The number of orphaned users fixed by updating users was %d.    |
|    15296    |    16    |    No    |    General cryptographic failure.    |
|    15297    |    16    |    No    |    The certificate, asymmetric key, or private key data is invalid.    |
|    15299    |    16    |    No    |    The signature of the public key is invalid.    |
|    15300    |    11    |    No    |    No recognized letter is contained in the parameter value for General Permission Type (%s). Valid letters are in this set: %s .    |
|    15301    |    16    |    No    |    Collation '%s' is supported for Unicode data types only and cannot be set at either the database or server level.    |
|    15302    |    11    |    No    |    Database_Name should not be used to qualify owner.object for the parameter into this procedure.    |
|    15303    |    11    |    No    |    The "user options" config value (%d) was rejected because it would set incompatible options.    |
|    15304    |    16    |    No    |    The severity level of the '%s' version of this message must be the same as the severity level (%ld) of the us_english version.    |
|    15305    |    16    |    No    |    The \@TriggerType parameter value must be 'insert', 'update', or 'delete'.    |
|    15306    |    16    |    No    |    Cannot change the compatibility level of replicated or distributed databases.    |
|    15307    |    16    |    No    |    Could not change the merge publish option because the server is not set up for replication.    |
|    15309    |    16    |    No    |    Cannot alter the trustworthy state of the model or tempdb databases.    |
|    15310    |    16    |    Yes    |    Failed to configure user instance on startup. Error updating server metadata.    |
|    15311    |    16    |    No    |    The file named '%s' does not exist.    |
|    15312    |    16    |    No    |    The file named '%s' is a primary file and cannot be removed.    |
|    15313    |    10    |    No    |    The key is not encrypted using the specified decryptor.    |
|    15314    |    10    |    No    |    Either no algorithm has been specified or the bitlength and the algorithm specified for the key are not available in this installation of Windows.    |
|    15315    |    10    |    No    |    The key '%.*ls' is not open. Please open the key before using it.    |
|    15316    |    10    |    No    |    Global temporary keys are not allowed. You can only use local temporary keys.    |
|    15317    |    10    |    No    |    The master key file does not exist or has invalid format.    |
|    15318    |    10    |    No    |    All fragments for database '%s' on device '%s' are now dedicated for log usage only.    |
|    15319    |    17    |    No    |    Error: DBCC DBREPAIR REMAP failed for database '%s' (device '%s').    |
|    15320    |    16    |    No    |    An error occurred while decrypting %S_MSG '%.*ls' that was encrypted by the old master key. The FORCE option can be used to ignore this error and continue the operation, but data that cannot be decrypted by the old master key will become unavailable.    |
|    15321    |    16    |    No    |    There was some problem removing '%s' from sys.master_files.    |
|    15322    |    10    |    No    |    File '%s' was removed from tempdb, and will take effect upon server restart.    |
|    15323    |    16    |    No    |    The selected index does not exist on table '%s'.    |
|    15324    |    16    |    No    |    The option %s cannot be changed for the '%s' database.    |
|    15325    |    16    |    No    |    The current database does not contain a %s named '%ls'.    |
|    15326    |    10    |    No    |    No extended stored procedures exist.    |
|    15327    |    10    |    No    |    The database is now offline.    |
|    15328    |    10    |    No    |    The database is offline already.    |
|    15329    |    16    |    No    |    The current master key cannot be decrypted. If this is a database master key, you should attempt to open it in the session before performing this operation. The FORCE option can be used to ignore this error and continue the operation but the data encrypted by the old master key will be lost.    |
|    15330    |    11    |    No    |    There are no matching rows on which to report.    |
|    15331    |    11    |    No    |    The user '%s' cannot take the action auto_fix due to duplicate SID.    |
|    15332    |    10    |    No    |    The private key is already set for this file. To change it you should drop and re-create the certificate.    |
|    15333    |    11    |    No    |    Error: The qualified \@oldname references a database (%s) other than the current database.    |
|    15334    |    10    |    No    |    The %S_MSG has a private key that is protected by a user defined password. That password needs to be provided to enable the use of the private key.    |
|    15335    |    11    |    No    |    Error: The new name '%s' is already in use as a %s name and would cause a duplicate that is not permitted.    |
|    15336    |    16    |    No    |    Object '%s' cannot be renamed because the object participates in enforced dependencies.    |
|    15337    |    10    |    No    |    Caution: sys.sql_dependencies shows that other objects (views, procedures and so on) are referencing this object by its old name. These objects will become invalid, and should be dropped and re-created promptly.    |
|    15339    |    10    |    No    |    Creating '%s'.    |
|    15342    |    10    |    No    |    There is no private key provisioned for %S_MSG '%.*ls'.    |
|    15343    |    10    |    No    |    The username and/or password passed in is invalid or the current process does not have sufficient privileges.    |
|    15344    |    16    |    No    |    Ownership change for %S_MSG is not supported.    |
|    15345    |    16    |    No    |    An entity of type %S_MSG cannot be owned by a role, a group, or by principals mapped to certificates or asymmetric keys.    |
|    15346    |    16    |    No    |    Cannot change owner for an object that is owned by a parent object. Change the owner of the parent object instead.    |
|    15347    |    16    |    No    |    Cannot transfer an object that is owned by a parent object.    |
|    15348    |    16    |    No    |    Cannot transfer a schemabound object.    |
|    15349    |    16    |    No    |    Cannot transfer an MS Shipped object.    |
|    15350    |    16    |    No    |    An attempt to attach an auto-named database for file %.*ls failed. A database with the same name exists, or specified file cannot be opened, or it is located on UNC share.    |
|    15351    |    10    |    No    |    The CLR procedure/function/type being signed refers to an assembly that is not signed either by a strong name or an assembly.    |
|    15352    |    16    |    No    |    The %S_MSG cannot be dropped because one or more entities are either signed or encrypted using it.    |
|    15353    |    16    |    No    |    An entity of type %S_MSG cannot be owned by a role, a group, an approle, or by principals mapped to certificates or asymmetric keys.    |
|    15354    |    10    |    No    |    Usage: sp_detach_db \<dbname\>, [TRUE\|FALSE], [TRUE\|FALSE]    |
|    15356    |    16    |    No    |    The current application role has been dropped. The current security context contains no valid database user context.    |
|    15357    |    16    |    No    |    The current security context was set by "%ls". It cannot be reverted by statement "%ls".    |
|    15358    |    10    |    No    |    User-defined filegroups should be made read-only.    |
|    15359    |    16    |    No    |    Cannot add functional unit '%.*ls' to component '%.*ls'. This unit has been already registered with the component.    |
|    15360    |    16    |    No    |    An error occurred while trying to load the xpstar dll to read the agent proxy account from LSA.    |
|    15361    |    16    |    No    |    An error occurred while trying to read the SQLAgent proxy account credentials from the LSA.    |
|    15362    |    16    |    No    |    An error occurred while trying to create the '%.*ls' credential.    |
|    15364    |    16    |    Yes    |    Failed to generate a user instance of SQL Server. Only an integrated connection can generate a user instance. The connection will be closed.%.*ls    |
|    15365    |    16    |    Yes    |    Failed to generate a user instance of SQL Server. Only members of Builtin\Users can generate a user instance. The connection will be closed.%.*ls    |
|    15366    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to low memory. The connection will be closed.%.*ls    |
|    15367    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in generating a unique user instance name. The connection will be closed.%.*ls    |
|    15368    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in reading registry keys. The connection will be closed.%.*ls    |
|    15369    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in impersonating the client. The connection will be closed.%.*ls    |
|    15370    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in copying database files. The connection will be closed.%.*ls    |
|    15371    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in creating user instance event. The connection will be closed.%.*ls    |
|    15372    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in starting the process for the user instance. The connection will be closed.%.*ls    |
|    15373    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in obtaining the user instance's process information. The connection will be closed.%.*ls    |
|    15374    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in persisting the user instance information into system catalog. The connection will be closed.%.*ls    |
|    15375    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in making a connection to the user instance. The connection will be closed.%.*ls    |
|    15376    |    16    |    Yes    |    Failed to generate a user instance of SQL Server. Only the SQL Server Express version lets you generate a user instance. The connection will be closed.%.*ls    |
|    15377    |    16    |    Yes    |    Failed to configure user instance on startup. Error adding user to sysadmin role.    |
|    15378    |    16    |    Yes    |    Failed to configure user instance on startup. Error configuring system database entries in MASTER DB.    |
|    15380    |    16    |    Yes    |    Failed to configure user instance on startup. Error configuring system database paths in MASTER DB.    |
|    15381    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to a failure in updating security descriptor on the process of the user instance.    |
|    15382    |    16    |    Yes    |    Failed to generate a user instance of SQL Server due to failure in retrieving the user's local application data path. Please make sure the user has a local user profile on the computer. The connection will be closed.%.*ls    |
|    15383    |    16    |    Yes    |    Generating user instances in SQL Server is disabled. Use sp_configure 'user instances enabled' to generate user instances.%.*ls    |
|    15384    |    16    |    Yes    |    Failed to configure user instance on startup. Error updating Resource Manager ID.    |
|    15385    |    16    |    No    |    No database principal is defined for sid '%.*ls'.    |
|    15386    |    16    |    No    |    Another batch in the session is changing security context, new batch is not allowed to start.    |
|    15387    |    11    |    No    |    If the qualified object name specifies a database, that database must be the current database.    |
|    15388    |    11    |    No    |    There is no user table matching the input name '%s' in the current database or you do not have permission to access the table.    |
|    15389    |    11    |    No    |    sp_indexoption is not supported for XML or spatial indexes. Use ALTER INDEX instead.    |
|    15390    |    11    |    No    |    Input name '%s' does not have a matching user table or indexed view in the current database.    |
|    15391    |    11    |    No    |    sp_indexoption is not supported for XML Index and the table has an XML index on it. Use ALTER INDEX instead to set the option for ALL the indexes.    |
|    15392    |    16    |    No    |    The specified option '%s' is not supported by this edition of SQL Server and cannot be changed using sp_configure.    |
|    15393    |    16    |    No    |    An error occurred while decrypting the password for linked login '%.*ls' that was encrypted by the old master key. The FORCE option can be used to ignore this error and continue the operation but the data encrypted by the old master key will be lost.    |
|    15394    |    16    |    No    |    Collation '%s' is not supported by the operating system    |
|    15395    |    11    |    No    |    The qualified old name could not be found for item type '%s'.    |
|    15396    |    16    |    No    |    An asymmetric key with name '%s' already exists or this asymmetric key already has been added to the database.    |
|    15397    |    16    |    No    |    The %S_MSG is not protected by a password. A decryption password cannot be used for this operation.    |
|    15398    |    11    |    No    |    Only objects in the master database owned by dbo can have the startup setting changed.    |
|    15399    |    11    |    No    |    Could not change startup option because this option is restricted to objects that have no parameters.    |
|    [15401](../../relational-databases/errors-events/mssqlserver-15401-database-engine-error.md)    |    11    |    No    |    Windows NT user or group '%s' not found. Check the name again.    |
|    15402    |    11    |    No    |    '%s' is not a fixed server role.    |
|    15403    |    16    |    No    |    The server principal "%.*ls" does not exist, does not have server access, or you do not have permission.    |
|    [15404](../../relational-databases/errors-events/mssqlserver-15404-database-engine-error.md)    |    16    |    No    |    Could not obtain information about Windows NT group/user '%ls', error code %#lx.    |
|    15405    |    11    |    No    |    Cannot use the special principal '%s'.    |
|    15406    |    16    |    No    |    Cannot execute as the server principal because the principal "%.*ls" does not exist, this type of principal cannot be impersonated, or you do not have permission.    |
|    15407    |    11    |    No    |    '%s' is not a valid Windows NT name. Give the complete name: <domain\username>.    |
|    15408    |    16    |    No    |    %ls cannot be called in this batch because a simultaneous batch has called it.    |
|    15409    |    11    |    No    |    '%s' is not a role.    |
|    15410    |    11    |    No    |    User or role '%s' does not exist in this database.    |
|    15411    |    11    |    No    |    Database principal or schema '%s' does not exist in this database.    |
|    15412    |    11    |    No    |    '%s' is not a known fixed role.    |
|    15413    |    11    |    No    |    Cannot make a role a member of itself.    |
|    15414    |    16    |    No    |    Cannot set compatibility level because database has a view or computed column that is indexed. These indexes require a SQL Server compatible database.    |
|    15416    |    16    |    No    |    Usage: sp_dbcmptlevel [dbname [, compatibilitylevel]]    |
|    15418    |    16    |    No    |    Only members of the sysadmin role or the database owner may set the database compatibility level.    |
|    15419    |    16    |    No    |    Supplied parameter sid should be binary(16).    |
|    15420    |    16    |    No    |    The group '%s' does not exist in this database.    |
|    15421    |    16    |    No    |    The database principal owns a database role and cannot be dropped.    |
|    15422    |    16    |    No    |    Application roles can only be activated at the ad hoc level.    |
|    15425    |    16    |    No    |    No server principal is defined for sid '%.*ls'.    |
|    15426    |    16    |    No    |    You must specify a provider name with this set of properties.    |
|    15427    |    16    |    No    |    You must specify a provider name for unknown product '%ls'.    |
|    15428    |    16    |    No    |    You cannot specify a provider or any properties for product '%ls'.    |
|    15429    |    16    |    No    |    '%ls' is an invalid product name.    |
|    15431    |    16    |    No    |    You must specify the \@rolename parameter.    |
|    15432    |    16    |    No    |    Stored procedure '%s' can only be executed at the ad hoc level.    |
|    15433    |    16    |    No    |    Supplied parameter sid is in use.    |
|    15434    |    16    |    No    |    Could not drop login '%s' as the user is currently logged in.    |
|    15435    |    10    |    No    |    Database successfully published.    |
|    15436    |    10    |    No    |    Database successfully enabled for subscriptions.    |
|    15437    |    10    |    No    |    Database successfully published using merge replication.    |
|    15438    |    10    |    No    |    Database is already online.    |
|    15439    |    10    |    No    |    Database is now online.    |
|    15440    |    10    |    No    |    Database is no longer published.    |
|    15441    |    10    |    No    |    Database is no longer enabled for subscriptions.    |
|    15442    |    10    |    No    |    Database is no longer enabled for merge publications.    |
|    15443    |    10    |    No    |    Checkpointing database that was changed.    |
|    15448    |    16    |    No    |    Encryption by the machine key cannot be added to the service master key because the service master key cannot be decrypted or does not exist.    |
|    15450    |    10    |    No    |    New language inserted.    |
|    15451    |    16    |    No    |    Dropping an encryption from the service master key failed. No encryption by the machine key exists.    |
|    15452    |    10    |    No    |    No alternate languages are available.    |
|    15453    |    10    |    No    |    us_english is always available, even though it is not in syslanguages.    |
|    15454    |    10    |    No    |    Language deleted.    |
|    15455    |    16    |    No    |    Adding an encryption to the service master key failed. An encryption by the machine key already exists.    |
|    15457    |    10    |    No    |    Configuration option '%ls' changed from %ld to %ld. Run the RECONFIGURE statement to install.    |
|    15458    |    10    |    No    |    Database removed.    |
|    15459    |    10    |    No    |    In the current database, the specified object references the following:    |
|    15460    |    10    |    No    |    In the current database, the specified object is referenced by the following:    |
|    15461    |    10    |    No    |    Object does not reference any object, and no objects reference it.    |
|    15462    |    10    |    No    |    File '%s' closed.    |
|    15463    |    10    |    No    |    Device dropped.    |
|    15464    |    16    |    No    |    Unsupported private key format or key length.    |
|    15465    |    16    |    No    |    The private key password is invalid.    |
|    15466    |    16    |    No    |    An error occurred during decryption.    |
|    15468    |    16    |    No    |    An error occurred during the generation of the %S_MSG.    |
|    15469    |    10    |    No    |    No constraints are defined on object '%ls', or you do not have permissions.    |
|    15470    |    10    |    No    |    No foreign keys reference table '%ls', or you do not have permissions on referencing tables.    |
|    15471    |    10    |    No    |    The text for object '%ls' is encrypted.    |
|    15472    |    10    |    No    |    The object '%ls' does not have any indexes, or you do not have permissions.    |
|    15474    |    16    |    No    |    Invalid private key. The private key does not match the public key of the %S_MSG.    |
|    15475    |    10    |    No    |    The database is renamed and in single user mode.    |
|    15477    |    10    |    No    |    Caution: Changing any part of an object name could break scripts and stored procedures.    |
|    15482    |    16    |    No    |    Cannot change the owner of a table that has an indexed view.    |
|    15490    |    10    |    No    |    The dependent aliases were also dropped.    |
|    15497    |    10    |    No    |    Could not add login using sp_addlogin (user = %s). Terminating this procedure.    |
|    15499    |    10    |    No    |    The dependent aliases were mapped to the new database owner.    |
|    15500    |    10    |    No    |    The dependent aliases were dropped.    |
|    15502    |    10    |    No    |    Setting database owner to SA.    |
|    15503    |    10    |    No    |    Giving ownership of all objects to the database owner.    |
|    15504    |    10    |    No    |    Deleting users except guest and the database owner from the system catalog.    |
|    15505    |    16    |    No    |    Cannot change owner of object '%ls' or one of its child objects because the new owner '%ls' already has an object with the same name.    |
|    15506    |    16    |    No    |    An error occurred while signing.    |
|    15507    |    16    |    No    |    A key required by this operation appears to be corrupted.    |
|    15508    |    16    |    No    |    An error occurred while generating a key required by this operation.    |
|    15509    |    16    |    No    |    The password cannot be dropped because another database may be using it.    |
|    15510    |    16    |    No    |    Cannot enable a login that has an empty password.    |
|    15511    |    10    |    No    |    Default bound to column.    |
|    15512    |    10    |    No    |    Default bound to data type.    |
|    15513    |    10    |    No    |    The new default has been bound to columns(s) of the specified user data type.    |
|    15514    |    10    |    No    |    Rule bound to table column.    |
|    15515    |    10    |    No    |    Rule bound to data type.    |
|    15516    |    10    |    No    |    The new rule has been bound to column(s) of the specified user data type.    |
|    [15517](../../relational-databases/errors-events/mssqlserver-15517-database-engine-error.md)    |    16    |    No    |    Cannot execute as the database principal because the principal "%.*ls" does not exist, this type of principal cannot be impersonated, or you do not have permission.    |
|    15518    |    16    |    No    |    Cannot execute as the Windows token. It is not valid, or you do not have permission.    |
|    15519    |    10    |    No    |    Default unbound from table column.    |
|    15520    |    10    |    No    |    Default unbound from data type.    |
|    15521    |    10    |    No    |    Columns of the specified user data type had their defaults unbound.    |
|    15522    |    10    |    No    |    Rule unbound from table column.    |
|    15523    |    10    |    No    |    Rule unbound from data type.    |
|    15524    |    10    |    No    |    Columns of the specified user data type had their rules unbound.    |
|    15525    |    10    |    No    |    sp_checknames is used to search for non 7-bit ASCII characters.    |
|    15526    |    10    |    No    |    in several important columns of system tables. The following    |
|    15527    |    10    |    No    |    columns are searched:    |
|    15528    |    10    |    No    |    In master:    |
|    15529    |    16    |    No    |    Cannot execute as the ticket. It is not valid, or you do not have permission.    |
|    15530    |    16    |    No    |    The %S_MSG with name "%.*ls" already exists.    |
|    15531    |    16    |    No    |    The security descriptor information is not valid.    |
|    15532    |    16    |    No    |    The security descriptor is invalid because it does not contain information about its owner or about its primary group.    |
|    15533    |    16    |    No    |    Invalid data type is supplied in the '%ls' statement.    |
|    15534    |    16    |    No    |    Cookie generation failed in the '%ls' statement.    |
|    15535    |    16    |    No    |    Cannot set a credential for principal '%.*ls'.    |
|    15536    |    10    |    No    |    In all databases:    |
|    15537    |    16    |    No    |    Login '%.*ls' does not have access to server.    |
|    15538    |    16    |    No    |    Login '%.*ls' does not have access to database.    |
|    15539    |    16    |    No    |    User '%s' cannot be dropped, it can only be disabled. The user is already disabled in the current database.    |
|    15540    |    16    |    No    |    The identity string is too long. The identity string should contain no more than %d characters.    |
|    15541    |    16    |    No    |    Cannot drop the credential '%.*ls' because it is used by a server principal.    |
|    15542    |    10    |    No    |    Cannot create a key without specifying an encryptor.    |
|    15556    |    10    |    No    |    Cannot decrypt or encrypt using the specified %S_MSG, either because it has no private key or because the password provided for the private key is incorrect.    |
|    15557    |    10    |    No    |    There is already a %S_MSG by %S_MSG '%.*ls'.    |
|    15558    |    10    |    No    |    Cannot drop %S_MSG by %S_MSG '%.*s'.    |
|    15559    |    10    |    No    |    Cannot drop %S_MSG '%.*ls' because there is a %S_MSG mapped to it.    |
|    15560    |    10    |    No    |    Cannot add or drop a signature on '%.*ls' because only modules can be signed.    |
|    15561    |    10    |    No    |    Signatures based on certificates or asymmetric keys are the only options supported in this version of the product.    |
|    15562    |    10    |    No    |    The module being executed is not trusted. Either the owner of the database of the module needs to be granted authenticate permission, or the module needs to be digitally signed.    |
|    15563    |    10    |    No    |    The %S_MSG has no private key set for it.    |
|    15574    |    10    |    No    |    This object does not have any statistics.    |
|    15575    |    10    |    No    |    This object does not have any statistics or indexes.    |
|    15576    |    16    |    No    |    You cannot set network name on server '%ls' because it is not a linked SQL Server.    |
|    15577    |    10    |    No    |    Warning: A linked server that refers to the originating server is not a supported scenario. If you wish to use a four-part name to reference a local table, please use the actual server name rather than an alias.    |
|    15578    |    16    |    No    |    There is already a master key in the database. Please drop it before performing this statement.    |
|    15579    |    16    |    No    |    Adding an encryption to the symmetric key failed. An encryption by the same %S_MSG '%.*s' may already exist.    |
|    15580    |    16    |    No    |    Cannot drop %S_MSG because %S_MSG '%.*s' is encrypted by it.    |
|    [15581](../../relational-databases/errors-events/mssqlserver-15581-database-engine-error.md)    |    16    |    No    |    Please create a master key in the database or open the master key in the session before performing this operation.    |
|    15583    |    10    |    No    |    The module being signed is marked to execute as owner. If the owner changes the signature will not be valid.    |
|    15584    |    10    |    No    |    An error occurred while decrypting %S_MSG '%.*ls' that was encrypted by the old master key. The error was ignored because the FORCE option was specified.    |
|    15585    |    10    |    No    |    The current master key cannot be decrypted. The error was ignored because the FORCE option was specified.    |
|    15586    |    16    |    No    |    Error in synchronizing system certificates between master and resource database.    |
|    15587    |    16    |    No    |    Cannot change owner of Assembly '%.*ls' since dependent assembly '%.*ls' is not owned by the new owner.    |
|    15588    |    10    |    No    |    The old and new master keys are identical. No data re-encryption is required.    |
|    15589    |    16    |    No    |    Cannot revert the current security context because the cookie is invalid.    |
|    15590    |    16    |    No    |    Can only use the 'No Revert' or 'Cookie' options with the 'Execute As' statement at the adhoc level.    |
|    15591    |    16    |    No    |    The current security context cannot be reverted using this statement. A cookie may or may not be needed with 'Revert' statement depending on how the context was set with 'Execute As' statement.    |
|    15592    |    16    |    No    |    Cannot unset application role because none was set or the cookie is invalid.    |
|    15593    |    16    |    No    |    An error occurred while decrypting the password for linked login '%.*ls' that was encrypted by the old master key. The error was ignored because the FORCE option was specified.    |
|    15594    |    16    |    No    |    The password is already provisioned for the database '%.*ls'    |
|    15595    |    16    |    No    |    The password cannot be dropped because it is not provisioned for the database '%.*ls'    |
|    15596    |    10    |    No    |    Warning: use of a UNIQUE index, PRIMARY KEY constraint, or UNIQUE constraint on a table with row-level security can allow information disclosure.    |
|    15597    |    10    |    No    |    Warning: use of an IDENTITY column on a table with row-level security can allow information disclosure.    |
|    15598    |    10    |    No    |    Warning: use of an indexed view on a table with row-level security can allow information disclosure.    |
|    [15599](../../relational-databases/errors-events/mssqlserver-15599-database-engine-error.md)    |    10    |    No    |    Warning: use of a FOREIGN KEY constraint on a table with row-level security enabled can allow information disclosure, modification, or deletion not authorized at the row level.    |
|    15600    |    15    |    No    |    An invalid parameter or option was specified for procedure '%s'.    |
|    15601    |    16    |    No    |    Full-Text Search is not enabled for the current database. Use sp_fulltext_database to enable Full-Text Search. The functionality to disable and enable full-text search for a database is deprecated. Please change your application.    |
|    15612    |    16    |    No    |    DBCC DBCONTROL error. Database was not made read-only.    |
|    15615    |    16    |    No    |    DBCC DBCONTROL error. Database was not made single user.    |
|    15622    |    10    |    No    |    No permission to access database '%s'.    |
|    15625    |    10    |    No    |    Option '%ls' not recognized for '%ls' parameter.    |
|    15626    |    10    |    No    |    You attempted to acquire a transactional application lock without an active transaction.    |
|    15627    |    10    |    No    |    sp_dboption command failed.    |
|    15635    |    16    |    No    |    Cannot execute '%ls' because the database is in read-only access mode.    |
|    15645    |    16    |    No    |    Column '%ls' does not exist.    |
|    15646    |    16    |    No    |    Column '%ls' is not a computed column.    |
|    15647    |    10    |    No    |    No views with schema binding reference table '%ls'.    |
|    15650    |    10    |    No    |    Updating %s    |
|    15651    |    10    |    No    |    %d index(es)/statistic(s) have been updated, %d did not require update.    |
|    15652    |    10    |    No    |    %s has been updated...    |
|    15653    |    10    |    No    |    %s, update is not necessary...    |
|    15654    |    10    |    No    |    Table %s: cannot perform the operation on the table because its clustered index is disabled.    |
|    15656    |    16    |    No    |    Cannot create user defined types from XML data type.    |
|    15657    |    16    |    No    |    Vardecimal storage format is not available in system database '%s'.    |
|    15658    |    16    |    No    |    Cannot run sp_resetstatus against a database snapshot.    |
|    15659    |    16    |    No    |    The schema '%ls' specified for parameter schema_name does not exist.    |
|    15660    |    16    |    No    |    Compressing XML index is not supported by the stored procedure sp_estimate_data_compression_savings.    |
|    [15661](../../relational-databases/errors-events/mssqlserver-15661-database-engine-error.md)    |    16    |    No    |    Compressing temporary tables is not supported by the stored procedure sp_estimate_data_compression_savings.    |
|    15662    |    16    |    No    |    Compressing tables with sparse columns or column sets is not supported by the stored procedure sp_estimate_data_compression_savings.    |
