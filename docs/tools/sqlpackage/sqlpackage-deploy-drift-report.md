---
title: SqlPackage deploy report and drift report
description: Learn how to automate database development tasks with SqlPackage.exe Deploy Report and Drift Report. View examples and available parameters, properties, and SQLCMD variables.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 11/14/2021
---

# SqlPackage deploy report and drift report
The SqlPackage.exe [DeployReport](#deployreport-action-parameters) action creates an XML report of the changes that would be made by a publish action.
The SqlPackage.exe [DriftReport](#driftreport-action-parameters) action creates an XML report of the changes that have been made to the registered database since it was last registered.  

## Command-line syntax

**SqlPackage.exe** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line.  
  
```bash
SqlPackage {parameters}{properties}{SQLCMD Variables}  
```

## DeployReport action parameters

|Parameter|Short Form|Value|Description|
|---|---|---|---|
|**/Action:**|**/a**|DeployReport|Specifies the action to be performed. |
|**/AccessToken:**|**/at**|{string}| Specifies the token based authentication access token to use when connect to the target database. |
|**/Diagnostics:**|**/d**|{True&#124;False}|Specifies whether diagnostic logging is output to the console. Defaults to False. |
|**/DiagnosticsFile:**|**/df**|{string}|Specifies a file to store diagnostic logs. |
|**/MaxParallelism:**|**/mp**|{int}| Specifies the degree of parallelism for concurrent operations running against a database. The default value is 8. |
|**/OutputPath:**|**/op**|{string}|Specifies the file path where the output files are generated. |
|**/OverwriteFiles:**|**/of**|{True&#124;False}|Specifies if SqlPackage.exe should overwrite existing files. Specifying false causes SqlPackage.exe to abort action if an existing file is encountered. Default value is True. |
|**/Profile:**|**/pr**|{string}|Specifies the file path to a DAC Publish Profile. The profile defines a collection of properties and variables to use when generating outputs. |
|**/Properties:**|**/p**|{PropertyName}={Value}|Specifies a name value pair for an [action-specific property](#deployreport-action-properties); {PropertyName}={Value}. |
|**/Quiet:**|**/q**|{True&#124;False}|Specifies whether detailed feedback is suppressed. Defaults to False. |
|**/SourceFile:**|**/sf**|{string}|Specifies a source file to be used as the source of action instead of a database. If this parameter is used, no other source parameter shall be valid. |
|**/SourceConnectionString:**|**/scs**|{string}|Specifies a valid SQL Server/Azure connection string to the source database. If this parameter is specified, it shall be used exclusively of all other source parameters. |
|**/SourceDatabaseName:**|**/sdn**|{string}|Defines the name of the source database. |
|**/SourceEncryptConnection:**|**/sec**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the source database connection. Default value is True. |
|**/SourceHostNameInCertificate:**|**/shnic**|{string}|Specifies value that is used to validate the source SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/SourcePassword:**|**/sp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the source database. |
|**/SourceServerName:**|**/ssn**|{string}|Defines the name of the server hosting the source database. |
|**/SourceTimeout:**|**/st**|{int}|Specifies the timeout for establishing a connection to the source database in seconds. |
|**/SourceTrustServerCertificate:**|**/stsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the source database connection and bypass walking the certificate chain to validate trust. Default value is False. |
|**/SourceUser:**|**/su**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the source database. |
|**/TargetFile:**|**/tf**|{string}|Specifies a target file (that is, a .dacpac file) to be used as the target of action instead of a database. If this parameter is used, no other target parameter shall be valid. This parameter shall be invalid for actions that only support database targets.|
|**/TargetConnectionString:**|**/tcs**|{string}|Specifies a valid SQL Server/Azure connection string to the target database. If this parameter is specified, it shall be used exclusively of all other target parameters. |
|**/TargetDatabaseName:**|**/tdn**|{string}|Specifies an override for the name of the database that is the target of SqlPackage.exe Action. |
|**/TargetEncryptConnection:**|**/tec**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the target database connection. Default value is True. |
|**/TargetHostNameInCertificate:**|**/thnic**|{string}|Specifies value that is used to validate the target SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/TargetPassword:**|**/tp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the target database. |
|**/TargetServerName:**|**/tsn**|{string}|Defines the name of the server hosting the target database. |
|**/TargetTimeout:**|**/tt**|{int}|Specifies the timeout for establishing a connection to the target database in seconds. For Azure AD, it is recommended that this value be greater than or equal to 30 seconds.|
|**/TargetTrustServerCertificate:**|**/ttsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the target database connection and bypass walking the certificate chain to validate trust. Default value is False. |
|**/TargetUser:**|**/tu**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the target database. |
|**/TenantId:**|**/tid**|{string}|Represents the Azure AD tenant ID or domain name. This option is required to support guest or imported Azure AD users as well as Microsoft accounts such as outlook.com, hotmail.com, or live.com. If this parameter is omitted, the default tenant ID for Azure AD will be used, assuming that the authenticated user is a native user for this AD. However, in this case any guest or imported users and/or Microsoft accounts hosted in this Azure AD are not supported and the operation will fail. <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|
|**/UniversalAuthentication:**|**/ua**|{True&#124;False}|Specifies if Universal Authentication should be used. When set to True, the interactive authentication protocol is activated supporting MFA. This option can also be used for Azure AD authentication without MFA, using an interactive protocol requiring the user to enter their username and password or integrated authentication (Windows credentials). When /UniversalAuthentication is set to True, no Azure AD authentication can be specified in SourceConnectionString (/scs). When /UniversalAuthentication is set to False, Azure AD authentication must be specified in SourceConnectionString (/scs). <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|
|**/Variables:**|**/v**|{PropertyName}={Value}|Specifies a name value pair for an action-specific variable; {VariableName}={Value}. The DACPAC file contains the list of valid SQLCMD variables. An error results if a value is not provided for every variable. |

## DeployReport action properties

|Property|Value|Description|
|---|---|---|
|**/p:**|AdditionalDeploymentContributorArguments=(STRING)|Specifies additional deployment contributor arguments for the deployment contributors. This should be a semi-colon delimited list of values.|
|**/p:**|AdditionalDeploymentContributors=(STRING)|Specifies additional deployment contributors, which should run when the dacpac is deployed. This should be a semi-colon delimited list of fully qualified build contributor names or IDs.|
|**/p:**|AdditionalDeploymentContributorPaths=(STRING)| Specifies paths to load additional deployment contributors. This should be a semi-colon delimited list of values. | 
|**/p:**|AllowDropBlocking Assemblies=(BOOLEAN)|This property is used by SqlClr deployment to cause any blocking assemblies to be dropped as part of the deployment plan. By default, any blocking/referencing assemblies will block an assembly update if the referencing assembly needs to be dropped.|
|**/p:**|AllowIncompatiblePlatform=(BOOLEAN)|Specifies whether to attempt the action despite incompatible SQL Server platforms.|
|**/p:**|AllowUnsafeRowLevelSecurityDataMovement=(BOOLEAN)|Do not block data motion on a table that has Row Level Security if this property is set to true. Default is false.|
|**/p:**|BackupDatabaseBeforeChanges=(BOOLEAN)|Backups the database before deploying any changes.|
|**/p:**|BlockOnPossibleDataLoss=(BOOLEAN 'True')| Specifies that the operation will be terminated during the schema validation step if the resulting schema changes could incur a loss of data, including due to data precision reduction or a data type change that requires a cast operation. The default (`True`) value causes the operation to terminate regardless if the target database contains data.  An execution with a `False` value for BlockOnPossibleDataLoss can still fail during deployment plan execution if data is present on the target that cannot be converted to the new column type. |
|**/p:**|BlockWhenDriftDetected=(BOOLEAN 'True')|Specifies whether to block updating a database whose schema no longer matches its registration or is unregistered. |
|**/p:**|CommandTimeout=(INT32 '60')|Specifies the command timeout in seconds when executing queries against SQL Server. |
|**/p:**|CommentOutSetVarDeclarations=(BOOLEAN)|Specifies whether the declaration of SETVAR variables should be commented out in the generated publish script. You might choose to do this if you plan to specify the values on the command line when you publish by using a tool such as SQLCMD.EXE. |
|**/p:**|CompareUsingTargetCollation=(BOOLEAN)|This setting dictates how the database's collation is handled during deployment; by default the target database's collation will be updated if it does not match the collation specified by the source. When this option is set, the target database's (or server's) collation should be used. |
|**/p:**|CreateNewDatabase=(BOOLEAN)|Specifies whether the target database should be updated or whether it should be dropped and re-created when you publish to a database. |
|**/p:**|DatabaseEdition=({ Basic &#124; Standard &#124; Premium &#124; DataWarehouse &#124; GeneralPurpose &#124; BusinessCritical &#124; Hyperscale &#124; Default } 'Default')|Defines the edition of an Azure SQL Database. See [Azure SQL Database service tiers](/azure/azure-sql/database/service-tiers-general-purpose-business-critical).|
|**/p:**|DatabaseLockTimeout=(INT32 '60')| Specifies the database lock timeout in seconds when executing queries against SQLServer. Use -1 to wait indefinitely.|
|**/p:**|DatabaseMaximumSize=(INT32)|Defines the maximum size in GB of an Azure SQL Database.|
|**/p:**|DatabaseServiceObjective=(STRING)|Defines the performance level of an Azure SQL Database such as "P0" or "S1". |
|**/p:**|DeployDatabaseInSingleUserMode=(BOOLEAN)|if true, the database is set to Single User Mode before deploying. |
|**/p:**|DisableAndReenableDdlTriggers=(BOOLEAN 'True')| Specifies whether Data Definition Language (DDL) triggers are disabled at the beginning of the publish process and re-enabled at the end of the publish action.|
|**/p:**|DoNotAlterChangeDataCaptureObjects=(BOOLEAN 'True')|If true, Change Data Capture objects are not altered.|
|**/p:**|DoNotAlterReplicatedObjects=(BOOLEAN 'True')|Specifies whether objects that are replicated are identified during verification.|
|**/p:**|DoNotDropObjectType=(STRING)|An object type that should not be dropped when DropObjectsNotInSource is true. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers. |
|**/p:**|DoNotDropObjectTypes=(STRING)|A semicolon-delimited list of object types that should not be dropped when DropObjectsNotInSource is true. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers.|
|**/p:**|DropConstraintsNotInSource=(BOOLEAN 'True')|Specifies whether constraints that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropDmlTriggersNotInSource=(BOOLEAN 'True')|Specifies whether DML triggers that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropExtendedPropertiesNotInSource=(BOOLEAN 'True')|Specifies whether extended properties that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropIndexesNotInSource=(BOOLEAN 'True')|Specifies whether indexes that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropObjectsNotInSource=(BOOLEAN)|Specifies whether objects that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database. This value takes precedence over DropExtendedProperties.|
|**/p:**|DropPermissionsNotInSource=(BOOLEAN)|Specifies whether permissions that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish updates to a database.|
|**/p:**|DropRoleMembersNotInSource=(BOOLEAN)|Specifies whether role members that are not defined in the database snapshot (.dacpac) file will be dropped from the target database when you publish updates to a database.|
|**/p:**|DropStatisticsNotInSource=(BOOLEAN 'True')|Specifies whether statistics that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|ExcludeObjectType=(STRING)|An object type that should be ignored during deployment. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers.|
|**/p:**|ExcludeObjectTypes=(STRING)|A semicolon-delimited list of object types that should be ignored during deployment. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers.|
|**/p:**|GenerateSmartDefaults=(BOOLEAN)|Automatically provides a default value when updating a table that contains data with a column that does not allow null values.|
|**/p:**|IgnoreAnsiNulls=(BOOLEAN 'True')|Specifies whether differences in the ANSI NULLS setting should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreAuthorizer=(BOOLEAN)|Specifies whether differences in the Authorizer should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreColumnCollation=(BOOLEAN)|Specifies whether differences in the column collations should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreColumnOrder=(BOOLEAN)|Specifies whether differences in table column order should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreComments=(BOOLEAN)|Specifies whether differences in the comments should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreCryptographicProviderFilePath=(BOOLEAN 'True')|Specifies whether differences in the file path for the cryptographic provider should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreDdlTriggerOrder=(BOOLEAN)|Specifies whether differences in the order of Data Definition Language (DDL) triggers should be ignored or updated when you publish to a database or server.|
|**/p:**|IgnoreDdlTriggerState=(BOOLEAN)|Specifies whether differences in the enabled or disabled state of Data Definition Language (DDL) triggers should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreDefaultSchema=(BOOLEAN)|Specifies whether differences in the default schema should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreDmlTriggerOrder=(BOOLEAN)|Specifies whether differences in the order of Data Manipulation Language (DML) triggers should be ignored or updated when you publish to a database.| 
|**/p:**|IgnoreDmlTriggerState=(BOOLEAN)|Specifies whether differences in the enabled or disabled state of DML triggers should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreExtendedProperties=(BOOLEAN)|Specifies whether differences in the extended properties should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreFileAndLogFilePath=(BOOLEAN 'True')|Specifies whether differences in the paths for files and log files should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreFilegroupPlacement=(BOOLEAN 'True')|Specifies whether differences in the placement of objects in FILEGROUPs should be ignored or updated when you publish to a database.| 
|**/p:**|IgnoreFileSize=(BOOLEAN 'True')|Specifies whether differences in the file sizes should be ignored or whether a warning should be issued when you publish to a database. |
|**/p:**|IgnoreFillFactor=(BOOLEAN 'True')|Specifies whether differences in the fill factor for index storage should be ignored or whether a warning should be issued when you publish to a database|
|**/p:**|IgnoreFullTextCatalogFilePath=(BOOLEAN 'True')|Specifies whether differences in the file path for the full-text catalog should be ignored or whether a warning should be issued when you publish to a database.| 
|**/p:**|IgnoreIdentitySeed=(BOOLEAN)|Specifies whether differences in the seed for an identity column should be ignored or updated when you publish updates to a database. |
|**/p:**|IgnoreIncrement=(BOOLEAN)|Specifies whether differences in the increment for an identity column should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreIndexOptions=(BOOLEAN)|Specifies whether differences in the index options should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreIndexPadding=(BOOLEAN 'True')|Specifies whether differences in the index padding should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreKeywordCasing=(BOOLEAN 'True')|Specifies whether differences in the casing of keywords should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreLockHintsOnIndexes=(BOOLEAN)|Specifies whether differences in the lock hints on indexes should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreLoginSids=(BOOLEAN 'True')| Specifies whether differences in the security identification number (SID) should be ignored or updated when you publish to a database.| 
|**/p:**|IgnoreNotForReplication=(BOOLEAN)|Specifies whether the not for replication settings should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreObjectPlacementOnPartitionScheme=(BOOLEAN 'True')|Specifies whether an object's placement on a partition scheme should be ignored or updated when you publish to a database.|
 |**/p:**|IgnorePartitionSchemes=(BOOLEAN)|Specifies whether differences in partition schemes and functions should be ignored or updated when you publish to a database.|
|**/p:**|IgnorePermissions=(BOOLEAN)|Specifies whether differences in the permissions should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreQuotedIdentifiers=(BOOLEAN 'True')|Specifies whether differences in the quoted identifiers setting should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreRoleMembership=(BOOLEAN)|Specifies whether differences in the role membership of logins should be ignored or updated when you publish to a database. |
|**/p:**|IgnoreRouteLifetime=(BOOLEAN 'True')|Specifies whether differences in the amount of time that SQL Server retains the route in the routing table should be ignored or updated when you publish to a database|
|**/p:**|IgnoreSemicolonBetweenStatements=(BOOLEAN 'True')|Specifies whether differences in the semi-colons between T-SQL statements will be ignored or updated when you publish to a database.| 
|**/p:**|IgnoreTableOptions=(BOOLEAN)|Specifies whether differences in the table options will be ignored or updated when you publish to a database.| 
|**/p:**|IgnoreTablePartitionOptions=(BOOLEAN)|Specifies whether differences in the table partition options will be ignored or updated when you publish to a database.  This option applies only to Azure Synapse Analytics data warehouse databases.|
|**/p:**|IgnoreUserSettingsObjects=(BOOLEAN)|Specifies whether differences in the user settings objects will be ignored or updated when you publish to a database.|
|**/p:**|IgnoreWhitespace=(BOOLEAN 'True')|Specifies whether differences in white space will be ignored or updated when you publish to a database. |
|**/p:**|IgnoreWithNocheckOnCheckConstraints=(BOOLEAN)|Specifies whether differences in the value of the WITH NOCHECK clause for check constraints will be ignored or updated when you publish to a database.| 
|**/p:**|IgnoreWithNocheckOnForeignKeys=(BOOLEAN)|Specifies whether differences in the value of the WITH NOCHECK clause for foreign keys will be ignored or updated when you publish to a database.| 
|**/p:**|IncludeCompositeObjects=(BOOLEAN)|Include all composite elements with the same database as part of a single publish operation.|
|**/p:**|IncludeTransactionalScripts=(BOOLEAN)|Specifies whether transactional statements should be used where possible when you publish to a database.|
|**/p:**|LongRunningCommandTimeout=(INT32 '0')| Specifies the long running command timeout in seconds when executing queries against SQL Server. Use 0 to wait indefinitely.|
|**/p:**|NoAlterStatementsToChangeClrTypes=(BOOLEAN)|Specifies that publish should always drop and re-create an assembly if there is a difference instead of issuing an ALTER ASSEMBLY statement. |
|**/p:**|PopulateFilesOnFileGroups=(BOOLEAN 'True')|Specifies whether a new file is also created when a new FileGroup is created in the target database. |
|**/p:**|RegisterDataTierApplication=(BOOLEAN)|Specifies whether the schema is registered with the database server. 
|**/p:**|RunDeploymentPlanExecutors=(BOOLEAN)|Specifies whether DeploymentPlanExecutor contributors should be run when other operations are executed.|
|**/p:**|ScriptDatabaseCollation=(BOOLEAN)|Specifies whether differences in the database collation should be ignored or updated when you publish to a database. |
|**/p:**|ScriptDatabaseCompatibility=(BOOLEAN)|Specifies whether differences in the database compatibility should be ignored or updated when you publish to a database. |
|**/p:**|ScriptDatabaseOptions=(BOOLEAN 'True')|Specifies whether target database properties should be set or updated as part of the publish action. |
|**/p:**|ScriptDeployStateChecks=(BOOLEAN)|Specifies whether statements are generated in the publish script to verify that the database name and server name match the names specified in the database project.|
|**/p:**|ScriptFileSize=(BOOLEAN)|Controls whether size is specified when adding a file to a filegroup. |
|**/p:**|ScriptNewConstraintValidation=(BOOLEAN 'True')|At the end of publish all of the constraints will be verified as one set, avoiding data errors caused by a check or foreign key constraint in the middle of publish. If set to False, your constraints are published without checking the corresponding data.|
|**/p:**|ScriptRefreshModule=(BOOLEAN 'True')|Include refresh statements at the end of the publish script.|
|**/p:**|Storage=({File&#124;Memory})|Specifies how elements are stored when building the database model. For performance reasons the default is InMemory. For large databases, File backed storage may be required and is only available for .NET Framework version of SqlPackage.|
|**/p:**|TreatVerificationErrorsAsWarnings=(BOOLEAN)|Specifies whether errors encountered during publish verification should be treated as warnings. The check is performed against the generated deployment plan before the plan is executed against your target database. Plan verification detects problems such as the loss of target-only objects (such as indexes) that must be dropped to make a change. Verification will also detect situations where dependencies (such as a table or view) exist because of a reference to a composite project, but do not exist in the target database. You might choose to do this to get a complete list of all issues, instead of having the publish action stop on the first error. |
|**/p:**|UnmodifiableObjectWarnings=(BOOLEAN 'True')|Specifies whether warnings should be generated when differences are found in objects that cannot be modified, for example, if the file size or file paths were different for a file.| 
|**/p:**|VerifyCollationCompatibility=(BOOLEAN 'True')|Specifies whether collation compatibility is verified.| 
|**/p:**|VerifyDeployment=(BOOLEAN 'True')|Specifies whether checks should be performed before publishing that will stop the publish action if issues are present that might block successful publishing. For example, your publish action might stop if you have foreign keys on the target database that do not exist in the database project, and that causes errors when you publish. |
  
## DriftReport action parameters

|Parameter|Short Form|Value|Description|
|---|---|---|---|
|**/Action:**|**/a**|DriftReport|Specifies the action to be performed. |
|**/AccessToken:**|**/at**|{string}| Specifies the token based authentication access token to use when connect to the target database. |
|**/Diagnostics:**|**/d**|{True&#124;False}|Specifies whether diagnostic logging is output to the console. Defaults to False. |
|**/DiagnosticsFile:**|**/df**|{string}|Specifies a file to store diagnostic logs. |
|**/MaxParallelism:**|**/mp**|{int}| Specifies the degree of parallelism for concurrent operations running against a database. The default value is 8. |
|**/OutputPath:**|**/op**|{string}|Specifies the file path where the output files are generated. |
|**/OverwriteFiles:**|**/of**|{True&#124;False}|Specifies if SqlPackage.exe should overwrite existing files. Specifying false causes SqlPackage.exe to abort action if an existing file is encountered. Default value is True. |
|**/Quiet:**|**/q**|{True&#124;False}|Specifies whether detailed feedback is suppressed. Defaults to False.|
|**/TargetConnectionString:**|**/tcs**|{string}|Specifies a valid SQL Server/Azure connection string to the target database. If this parameter is specified, it shall be used exclusively of all other target parameters. |
|**/TargetDatabaseName:**|**/tdn**|{string}|Specifies an override for the name of the database that is the target of SqlPackage.exe Action. |
|**/TargetEncryptConnection:**|**/tec**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the target database connection. Default value is True. |
|**/TargetHostNameInCertificate:**|**/thnic**|{string}|Specifies value that is used to validate the target SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/TargetPassword:**|**/tp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the target database. |
|**/TargetServerName:**|**/tsn**|{string}|Defines the name of the server hosting the target database. |
|**/TargetTimeout:**|**/tt**|{int}|Specifies the timeout for establishing a connection to the target database in seconds. For Azure AD, it is recommended that this value be greater than or equal to 30 seconds.|
|**/TargetTrustServerCertificate:**|**/ttsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the target database connection and bypass walking the certificate chain to validate trust. Default value is False. |
|**/TargetUser:**|**/tu**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the target database. |
|**/TenantId:**|**/tid**|{string}|Represents the Azure AD tenant ID or domain name. This option is required to support guest or imported Azure AD users as well as Microsoft accounts such as outlook.com, hotmail.com, or live.com. If this parameter is omitted, the default tenant ID for Azure AD will be used, assuming that the authenticated user is a native user for this AD. However, in this case any guest or imported users and/or Microsoft accounts hosted in this Azure AD are not supported and the operation will fail. <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|
|**/UniversalAuthentication:**|**/ua**|{True&#124;False}|Specifies if Universal Authentication should be used. When set to True, the interactive authentication protocol is activated supporting MFA. This option can also be used for Azure AD authentication without MFA, using an interactive protocol requiring the user to enter their username and password or integrated authentication (Windows credentials). When /UniversalAuthentication is set to True, no Azure AD authentication can be specified in SourceConnectionString (/scs). When /UniversalAuthentication is set to False, Azure AD authentication must be specified in SourceConnectionString (/scs). <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|

## Next Steps

- Learn more about [SqlPackage](sqlpackage.md)
