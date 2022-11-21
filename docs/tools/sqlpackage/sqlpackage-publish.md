---
title: SqlPackage Publish
description: Learn how to automate database development tasks with SqlPackage.exe Publish. View examples and available parameters, properties, and SQLCMD variables.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 9/29/2022
---

# SqlPackage Publish parameters, properties, and SQLCMD variables
The SqlPackage.exe publish operation incrementally updates the schema of a target database to match the structure of a source database. Publishing a deployment package that contains user data for all or a subset of tables update the table data in addition to the schema. Data deployment overwrites the schema and data in existing tables of the target database. Data deployment will not change existing schema or data in the target database for tables not included in the deployment package.  A new database can be created by the publish action when the authenticated user has [create database permissions](../../t-sql/statements/create-database-transact-sql.md#permissions). The required permissions for the publish action on an existing database is *db_owner*.

## Command-line syntax

**SqlPackage.exe** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line.  
  
```bash
SqlPackage /Action:Publish {parameters} {properties} {sqlcmd variables}
```

> [!NOTE]
> When a database with SQL authentication user credentials is extracted, the password is replaced with a different password of suitable complexity. It is assumed that after the dacpac is published that the user password is changed.

### Examples

```bash
# example publish from Azure SQL Database using SQL authentication and a connection string
SqlPackage /Action:Publish /SourceFile:"C:\AdventureWorksLT.dacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# example publish using short form parameter names, skips schema validation
SqlPackage /a:Publish /tsn:"{yourserver}.database.windows.net,1433" /tdn:"AdventureWorksLT" /tu:"sqladmin" \
    /tp:"{your_password}" /sf:"C:\AdventureWorksLT.dacpac" /p:VerifyDeployment=False

# example publish using Azure Active Directory Service Principal
SqlPackage /Action:Publish /SourceFile:"C:\AdventureWorksLT.dacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Service Principal;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# example publish connecting using Azure Active Directory username and password
SqlPackage /Action:Publish /SourceFile:"C:\AdventureWorksLT.dacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;User ID={yourusername};Password={yourpassword}"

# example publish connecting using Azure Active Directory universal authentication
SqlPackage /Action:Publish /SourceFile:"C:\AdventureWorksLT.dacpac" /UniversalAuthentication=True \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# example publish with 2 SQLCMD variables
# as seen in a post deployment script for user passwords
# https://github.com/Azure-Samples/app-sql-devops-demo-project/blob/main/sql/wwi-dw-ssdt/PostDeploymentScripts/AddUsers.sql
SqlPackage /Action:Publish /SourceFile:"C:\AdventureWorksLT.dacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" \
    /v:ETLUserPassword="asecurestringaddedhere" /v:AppUserPassword="asecurestringaddedhere"
```

```powershell
# example publish connecting using an access token associated with a service principal
$Account = Connect-AzAccount -ServicePrincipal -Tenant $Tenant -Credential $Credential
$AccessToken_Object = (Get-AzAccessToken -Account $Account -Resource "https://database.windows.net/")
$AccessToken = $AccessToken_Object.Token

sqlpackage.exe /at:$AccessToken /Action:Publish /SourceFile:"C:\AdventureWorksLT.dacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
# OR
sqlpackage.exe /at:$($AccessToken_Object.Token) /Action:Publish /SourceFile:"C:\AdventureWorksLT.dacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```


## Parameters for the Publish action

|Parameter|Short Form|Value|Description|
|---|---|---|---|
|**/AccessToken:**|**/at**|{string}| Specifies the token-based authentication access token to use when connect to the target database. |
|**/Action:**|**/a**|Publish|Specifies the action to be performed. |
|**/AzureCloudConfig:**|**/acc**|{string}|Specifies the custom endpoints for connecting to Azure Active Directory in the format: AzureActiveDirectoryAuthority={value};DatabaseServicePrincipalName={value}" .|
|**/AzureKeyVaultAuthMethod:**|**/akv**|{Interactive&#124;ClientIdSecret}|Specifies what authentication method is used for accessing Azure KeyVault if a publish operation includes modifications to an encrypted table/column. |
|**/ClientId:**|**/cid**|{string}|Specifies the Client ID to be used in authenticating against Azure KeyVault, when necessary |
|**/DeployReportPath:**|**/drp**|{string}|Specifies an optional file path to output the deployment report xml file. |
|**/DeployScriptPath:**|**/dsp**|{string}|Specifies an optional file path to output the deployment script. For Azure deployments, if there are TSQL commands to create or modify the master database, a script will be written to the same path but with "Filename_Master.sql" as the output file name. |
|**/Diagnostics:**|**/d**|{True&#124;False}|Specifies whether diagnostic logging is output to the console. Defaults to False. |
|**/DiagnosticsFile:**|**/df**|{string}|Specifies a file to store diagnostic logs. |
|**/MaxParallelism:**|**/mp**|{int}| Specifies the degree of parallelism for concurrent operations running against a database. The default value is 8. |
|**/ModelFilePath:**|**/mfp**|{string}|Specifies the file path to override the model.xml in the source file. Use of this setting may result in deployment failure and/or unintended data loss. This setting is intended only for use when troubleshooting issues with publish, import, or script generation.|
|**/OverwriteFiles:**|**/of**|{True&#124;False}|Specifies if SqlPackage.exe should overwrite existing files. Specifying false causes SqlPackage.exe to abort action if an existing file is encountered. Default value is True. |
|**/Profile:**|**/pr**|{string}|Specifies the file path to a DAC Publish Profile. The profile defines a collection of properties and variables to use when generating outputs.|
|**/Properties:**|**/p**|{PropertyName}={Value}|Specifies a name value pair for an [action-specific property](#properties-specific-to-the-publish-action);{PropertyName}={Value}. |
|**/Quiet:**|**/q**|{True&#124;False}|Specifies whether detailed feedback is suppressed. Defaults to False.|
|**/ReferencePaths:**|**/rp**|{PropertyName}={Value}|Specifies the additional directories to search for .dacpac references.|
|**/Secret:**|**/secr**|{string}|Specifies the Client Secret to be used in authenticating against Azure KeyVault, when necessary |
|**/SourceFile:**|**/sf**|{string}|Specifies a source file to be used as the source of action instead of a database from local storage. If this parameter is used, no other source parameter shall be valid. |
|**/SourceConnectionString:**|**/scs**|{string}|Specifies a valid SQL Server/Azure connection string to the source database. If this parameter is specified, it shall be used exclusively of all other source parameters. |
|**/SourceDatabaseName:**|**/sdn**|{string}|Defines the name of the source database. |
|**/SourceEncryptConnection:**|**/sec**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the source database connection. Default value is True. |
|**/SourceHostNameInCertificate:**|**/shnic**|{string}|Specifies value that is used to validate the source SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/SourcePassword:**|**/sp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the source database. |
|**/SourceServerName:**|**/ssn**|{string}|Defines the name of the server hosting the source database. |
|**/SourceTimeout:**|**/st**|{int}|Specifies the timeout for establishing a connection to the source database in seconds. |
|**/SourceTrustServerCertificate:**|**/stsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the source database connection and bypass walking the certificate chain to validate trust. Default value is False. |
|**/SourceUser:**|**/su**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the source database. |
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
|**/ThreadMaxStackSize:**|**/tmss**|{int}|Specifies the maximum size in megabytes for the thread running the SqlPackage.exe action. This option should only be used when encountering stack overflow exceptions that occur when parsing very large TSQL statements.|
|**/UniversalAuthentication:**|**/ua**|{True&#124;False}|Specifies if Universal Authentication should be used. When set to True, the interactive authentication protocol is activated supporting MFA. This option can also be used for Azure AD authentication without MFA, using an interactive protocol requiring the user to enter their username and password or integrated authentication (Windows credentials). When /UniversalAuthentication is set to True, no Azure AD authentication can be specified in SourceConnectionString (/scs). When /UniversalAuthentication is set to False, Azure AD authentication must be specified in SourceConnectionString (/scs). <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|
|**/Variables:**|**/v**|{PropertyName}={Value}|Specifies a name value pair for an action-specific variable;{VariableName}={Value}. The DACPAC file contains the list of valid SQLCMD variables. An error results if a value is not provided for every variable. |

## Properties specific to the Publish action

|Property|Value|Description|
|---|---|---|
|**/p:**|AdditionalDeploymentContributorArguments=(STRING)|Specifies additional deployment contributor arguments for the deployment contributors. This property should be a semi-colon delimited list of values.|
|**/p:**|AdditionalDeploymentContributorPaths=(STRING)|Specifies paths to load additional deployment contributors. This property should be a semi-colon delimited list of values.|
|**/p:**|AdditionalDeploymentContributors=(STRING)|Specifies additional deployment contributors, which should run when the dacpac is deployed. This property should be a semi-colon delimited list of fully qualified build contributor names or IDs.|
|**/p:**|AllowDropBlockingAssemblies=(BOOLEAN 'False')|This property is used by SqlClr deployment to cause any blocking assemblies to be dropped as part of the deployment plan. By default, any blocking/referencing assemblies will block an assembly update if the referencing assembly needs to be dropped.|
|**/p:**|AllowExternalLanguagePaths=(BOOLEAN 'False')|Allows file paths, if available, to be used to generate external language statements.|
|**/p:**|AllowExternalLibraryPaths=(BOOLEAN 'False')|Allows file paths, if available, to be used to generate external library statements.|
|**/p:**|AllowIncompatiblePlatform=(BOOLEAN 'False')|Specifies whether to attempt the action despite incompatible SQL Server platforms.|
|**/p:**|AllowUnsafeRowLevelSecurityDataMovement=(BOOLEAN 'False')|Do not block data motion on a table that has Row Level Security if this property is set to true. Default is false.|
|**/p:**|AzureSharedAccessSignatureToken=(STRING)|Azure shared access signature (SAS) token. See [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#publish).|
|**/p:**|AzureStorageBlobEndpoint=(STRING)|Azure Blob Storage endpoint, see [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#publish).|
|**/p:**|AzureStorageContainer=(STRING)|Azure Blob Storage container, see [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#publish).|
|**/p:**|AzureStorageKey=(STRING)|Azure storage account key, see [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#publish).|
|**/p:**|AzureStorageRootPath=(STRING)|Storage root path within the container. Without this property, the path defaults to `servername/databasename/timestamp/`. See [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#publish).|
|**/p:**|BackupDatabaseBeforeChanges=(BOOLEAN 'False')|Backups the database before deploying any changes. This property is not applicable to Azure SQL Database.|
|**/p:**|BlockOnPossibleDataLoss=(BOOLEAN 'True')| Specifies that the operation will be terminated during the schema validation step if the resulting schema changes could incur a loss of data, including due to data precision reduction or a data type change that requires a cast operation. The default (`True`) value causes the operation to terminate regardless if the target database contains data.  An execution with a `False` value for BlockOnPossibleDataLoss can still fail during deployment plan execution if data is present on the target that cannot be converted to the new column type. |
|**/p:**|BlockWhenDriftDetected=(BOOLEAN 'True')|Specifies whether to block updating a database whose schema no longer matches its registration or is unregistered.|
|**/p:**|CommandTimeout=(INT32 '60')|Specifies the command timeout in seconds when executing queries against SQL Server.|
|**/p:**|CommentOutSetVarDeclarations=(BOOLEAN 'False')|Specifies whether the declaration of SETVAR variables should be commented out in the generated publish script. You might choose to do this if you plan to specify the values on the command line when you publish by using a tool such as SQLCMD.EXE.|
|**/p:**|CompareUsingTargetCollation=(BOOLEAN 'False')|This setting dictates how the database's collation is handled during deployment; by default the target database's collation will be updated if it does not match the collation specified by the source. When this option is set, the target database's (or server's) collation should be used.|
|**/p:**|CreateNewDatabase=(BOOLEAN 'False')|Specifies whether the target database should be updated or whether it should be dropped and re-created when you publish to a database.|
|**/p:**|DatabaseEdition=({ Basic &#124; Standard &#124; Premium &#124; DataWarehouse &#124; GeneralPurpose &#124; BusinessCritical &#124; Hyperscale &#124; Default } 'Default')|Defines the edition of an Azure SQL Database. See [Azure SQL Database service tiers](/azure/azure-sql/database/service-tiers-general-purpose-business-critical). |
|**/p:**|DatabaseLockTimeout=(INT32 '60')|Specifies the database lock timeout in seconds when executing queries against SQLServer. Use -1 to wait indefinitely.|
|**/p:**|DatabaseMaximumSize=(INT32 '0')|Defines the maximum size in GB of an Azure SQL Database.|
|**/p:**|DatabaseServiceObjective=(STRING)|Defines the performance level of an Azure SQL Database such as "P0" or "S1".|
|**/p:**|DeployDatabaseInSingleUserMode=(BOOLEAN 'False')|if true, the database is set to Single User Mode before deploying.|
|**/p:**|DisableAndReenableDdlTriggers=(BOOLEAN 'True')|Specifies whether Data Definition Language (DDL) triggers are disabled at the beginning of the publish process and re-enabled at the end of the publish action.|
|**/p:**|DisableIndexesForDataPhase=(BOOLEAN 'True')|Disable indexes before importing data into SQL Server.|
|**/p:**|DisableParallelismForEnablingIndexes=(BOOLEAN 'False')|Not using parallelism when rebuilding indexes while importing data into SQL Server.|
|**/p:**|DoNotAlterChangeDataCaptureObjects=(BOOLEAN 'True')|If true, Change Data Capture objects are not altered.|
|**/p:**|DoNotAlterReplicatedObjects=(BOOLEAN 'True')|Specifies whether objects that are replicated are identified during verification.|
|**/p:**|DoNotDropDatabaseWorkloadGroups=(BOOLEAN 'False')|When false, Database WorkloadGroups in the target database that are not defined in the source will be dropped during deployment.|
|**/p:**|DoNotDropObjectType=(STRING[])|An object type that should not be dropped when DropObjectsNotInSource is true. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AssemblyFiles, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseOptions, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, Files, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseEncryptionKeys, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, MasterKeys, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers, ExternalStreams, ExternalStreamingJobs, DatabaseWorkloadGroups, WorkloadClassifiers, ExternalLibraries, ExternalLanguages.|
|**/p:**|DoNotDropObjectTypes=(STRING)|A semicolon-delimited list of object types that should not be dropped when DropObjectsNotInSource is true. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AssemblyFiles, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseOptions, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, Files, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseEncryptionKeys, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, MasterKeys, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers, ExternalStreams, ExternalStreamingJobs, DatabaseWorkloadGroups, WorkloadClassifiers, ExternalLibraries, ExternalLanguages.|
|**/p:**|DoNotDropWorkloadClassifiers=(BOOLEAN 'False')|When false, WorkloadClassifiers in the target database that are not defined in the source will be dropped during deployment.|
|**/p:**|DoNotEvaluateSqlCmdVariables=(BOOLEAN 'True')|Specifies whether SQLCMD variables to not replace with values|
|**/p:**|DropConstraintsNotInSource=(BOOLEAN 'True')|Specifies whether constraints that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropDmlTriggersNotInSource=(BOOLEAN 'True')|Specifies whether DML triggers that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropExtendedPropertiesNotInSource=(BOOLEAN 'True')|Specifies whether extended properties that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropIndexesNotInSource=(BOOLEAN 'True')|Specifies whether indexes that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|DropObjectsNotInSource=(BOOLEAN 'False')|Specifies whether objects that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database. This value takes precedence over DropExtendedProperties.|
|**/p:**|DropPermissionsNotInSource=(BOOLEAN 'False')|Specifies whether permissions that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish updates to a database.|
|**/p:**|DropRoleMembersNotInSource=(BOOLEAN 'False')|Specifies whether role members that are not defined in the database snapshot (.dacpac) file will be dropped from the target database when you publish updates to a database.|
|**/p:**|DropStatisticsNotInSource=(BOOLEAN 'True')|Specifies whether statistics that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database.|
|**/p:**|EnclaveAttestationProtocol=(STRING)|Specifies an attestation protocol to be used with enclave based Always Encrypted.|
|**/p:**|EnclaveAttestationUrl=(STRING)|Specifies the enclave attestation URL (an attestation service endpoint) to be used with enclave based Always Encrypted.|
|**/p:**|ExcludeObjectType=(STRING[])|An object type that should be ignored during deployment. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AssemblyFiles, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseOptions, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, Files, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseEncryptionKeys, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, MasterKeys, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers, ExternalStreams, ExternalStreamingJobs, DatabaseWorkloadGroups, WorkloadClassifiers, ExternalLibraries, ExternalLanguages.|
|**/p:**|ExcludeObjectTypes=(STRING)|A semicolon delimited list of object types that should be ignored during deployment. Valid object type names are Aggregates, ApplicationRoles, Assemblies, AssemblyFiles, AsymmetricKeys, BrokerPriorities, Certificates, ColumnEncryptionKeys, ColumnMasterKeys, Contracts, DatabaseOptions, DatabaseRoles, DatabaseTriggers, Defaults, ExtendedProperties, ExternalDataSources, ExternalFileFormats, ExternalTables, Filegroups, Files, FileTables, FullTextCatalogs, FullTextStoplists, MessageTypes, PartitionFunctions, PartitionSchemes, Permissions, Queues, RemoteServiceBindings, RoleMembership, Rules, ScalarValuedFunctions, SearchPropertyLists, SecurityPolicies, Sequences, Services, Signatures, StoredProcedures, SymmetricKeys, Synonyms, Tables, TableValuedFunctions, UserDefinedDataTypes, UserDefinedTableTypes, ClrUserDefinedTypes, Users, Views, XmlSchemaCollections, Audits, Credentials, CryptographicProviders, DatabaseAuditSpecifications, DatabaseEncryptionKeys, DatabaseScopedCredentials, Endpoints, ErrorMessages, EventNotifications, EventSessions, LinkedServerLogins, LinkedServers, Logins, MasterKeys, Routes, ServerAuditSpecifications, ServerRoleMembership, ServerRoles, ServerTriggers, ExternalStreams, ExternalStreamingJobs, DatabaseWorkloadGroups, WorkloadClassifiers, ExternalLibraries, ExternalLanguages.|
|**/p:**|GenerateSmartDefaults=(BOOLEAN 'False')|Automatically provides a default value when updating a table that contains data with a column that does not allow null values.|
|**/p:**|HashObjectNamesInLogs=(BOOLEAN 'False')|Specifies whether to replace all object names in logs with a random hash value.|
|**/p:**|IgnoreAnsiNulls=(BOOLEAN 'True')|Specifies whether differences in the ANSI NULLS setting should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreAuthorizer=(BOOLEAN 'False')|Specifies whether differences in the Authorizer should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreColumnCollation=(BOOLEAN 'False')|Specifies whether differences in the column collations should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreColumnOrder=(BOOLEAN 'False')|Specifies whether differences in table column order should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreComments=(BOOLEAN 'False')|Specifies whether differences in the comments should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreCryptographicProviderFilePath=(BOOLEAN 'True')|Specifies whether differences in the file path for the cryptographic provider should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreDatabaseWorkloadGroups=(BOOLEAN 'False')|Specifies whether to exclude workload groups that exist on the target during deployment.  No Database Workload Groups will be added, modified, or dropped.|
|**/p:**|IgnoreDdlTriggerOrder=(BOOLEAN 'False')|Specifies whether differences in the order of Data Definition Language (DDL) triggers should be ignored or updated when you publish to a database or server.|
|**/p:**|IgnoreDdlTriggerState=(BOOLEAN 'False')|Specifies whether differences in the enabled or disabled state of Data Definition Language (DDL) triggers should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreDefaultSchema=(BOOLEAN 'False')|Specifies whether differences in the default schema should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreDmlTriggerOrder=(BOOLEAN 'False')|Specifies whether differences in the order of Data Manipulation Language (DML) triggers should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreDmlTriggerState=(BOOLEAN 'False')|Specifies whether differences in the enabled or disabled state of DML triggers should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreExtendedProperties=(BOOLEAN 'False')|Specifies whether differences in the extended properties should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreFileAndLogFilePath=(BOOLEAN 'True')|Specifies whether differences in the paths for files and log files should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreFilegroupPlacement=(BOOLEAN 'True')|Specifies whether differences in the placement of objects in FILEGROUPs should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreFileSize=(BOOLEAN 'True')|Specifies whether differences in the file sizes should be ignored or whether a warning should be issued when you publish to a database.|
|**/p:**|IgnoreFillFactor=(BOOLEAN 'True')|Specifies whether differences in the fill factor for index storage should be ignored or whether a warning should be issued when you publish to a database.|
|**/p:**|IgnoreFullTextCatalogFilePath=(BOOLEAN 'True')|Specifies whether differences in the file path for the full-text catalog should be ignored or whether a warning should be issued when you publish to a database.|
|**/p:**|IgnoreIdentitySeed=(BOOLEAN 'False')|Specifies whether differences in the seed for an identity column should be ignored or updated when you publish updates to a database.|
|**/p:**|IgnoreIncrement=(BOOLEAN 'False')|Specifies whether differences in the increment for an identity column should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreIndexOptions=(BOOLEAN 'False')|Specifies whether differences in the index options should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreIndexPadding=(BOOLEAN 'True')|Specifies whether differences in the index padding should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreKeywordCasing=(BOOLEAN 'True')|Specifies whether differences in the casing of keywords should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreLockHintsOnIndexes=(BOOLEAN 'False')|Specifies whether differences in the lock hints on indexes should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreLoginSids=(BOOLEAN 'True')|Specifies whether differences in the security identification number (SID) should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreNotForReplication=(BOOLEAN 'False')|Specifies whether the not for replication settings should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreObjectPlacementOnPartitionScheme=(BOOLEAN 'True')|Specifies whether an object's placement on a partition scheme should be ignored or updated when you publish to a database.|
|**/p:**|IgnorePartitionSchemes=(BOOLEAN 'False')|Specifies whether differences in partition schemes and functions should be ignored or updated when you publish to a database.|
|**/p:**|IgnorePermissions=(BOOLEAN 'False')|Specifies whether differences in the permissions should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreQuotedIdentifiers=(BOOLEAN 'True')|Specifies whether differences in the quoted identifiers setting should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreRoleMembership=(BOOLEAN 'False')|Specifies whether differences in the role membership of logins should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreRouteLifetime=(BOOLEAN 'True')|Specifies whether differences in the amount of time that SQL Server retains the route in the routing table should be ignored or updated when you publish to a database.|
|**/p:**|IgnoreSemicolonBetweenStatements=(BOOLEAN 'True')|Specifies whether differences in the semi-colons between T-SQL statements will be ignored or updated when you publish to a database.|
|**/p:**|IgnoreSensitivityClassifications=(BOOLEAN 'False')|Specifies whether data sensitivity classifications on columns should be ignored when comparing schema models. This only works for classifications added with the ADD SENSITIVITY CLASSIFICATION syntax introduced in SQL 2019.|
|**/p:**|IgnoreTableOptions=(BOOLEAN 'False')|Specifies whether differences in the table options will be ignored or updated when you publish to a database.|
|**/p:**|IgnoreTablePartitionOptions=(BOOLEAN 'False')|Specifies whether differences in the table partition options will be ignored or updated when you publish to a database.  This option applies only to Azure Synapse Analytics dedicated SQL pool databases.|
|**/p:**|IgnoreUserSettingsObjects=(BOOLEAN 'False')|Specifies whether differences in the user settings objects will be ignored or updated when you publish to a database.|
|**/p:**|IgnoreWhitespace=(BOOLEAN 'True')|Specifies whether differences in white space will be ignored or updated when you publish to a database.|
|**/p:**|IgnoreWithNocheckOnCheckConstraints=(BOOLEAN 'False')|Specifies whether differences in the value of the WITH NOCHECK clause for check constraints will be ignored or updated when you publish.|
|**/p:**|IgnoreWithNocheckOnForeignKeys=(BOOLEAN 'False')|Specifies whether differences in the value of the WITH NOCHECK clause for foreign keys will be ignored or updated when you publish to a database.|
|**/p:**|IgnoreWorkloadClassifiers=(BOOLEAN 'False')|Specifies whether to exclude workload classifiers that exist on the target during deployment.|
|**/p:**|IncludeCompositeObjects=(BOOLEAN 'False')|Include all composite elements with the same database as part of a single publish operation.|
|**/p:**|IncludeTransactionalScripts=(BOOLEAN 'False')|Specifies whether transactional statements should be used where possible when you publish to a database.|
|**/p:**|IsAlwaysEncryptedParameterizationEnabled=(BOOLEAN 'False')|Enables variable parameterization on Always Encrypted columns in pre/post deployment scripts.|
|**/p:**|LongRunningCommandTimeout=(INT32 '0')|Specifies the long running command timeout in seconds when executing queries against SQL Server. Use 0 to wait indefinitely.|
|**/p:**|NoAlterStatementsToChangeClrTypes=(BOOLEAN 'False')|Specifies that publish should always drop and re-create an assembly if there is a difference instead of issuing an ALTER ASSEMBLY statement.|
|**/p:**|PopulateFilesOnFileGroups=(BOOLEAN 'True')|Specifies whether a new file is also created when a new FileGroup is created in the target database.|
|**/p:**|PreserveIdentityLastValues=(BOOLEAN 'False')|Specifies whether last values for identity columns should be preserved during deployment.|
|**/p:**|RebuildIndexesOfflineForDataPhase=(BOOLEAN 'False')|Rebuild indexes offline after importing data.|
|**/p:**|RegisterDataTierApplication=(BOOLEAN 'False')|Specifies whether the schema is registered with the database server.|
|**/p:**|RestoreSequenceCurrentValue=(BOOLEAN 'True')|Specifies whether sequence object current value should be deployed with dacpac file, the default value is True.|
|**/p:**|RunDeploymentPlanExecutors=(BOOLEAN 'False')|Specifies whether DeploymentPlanExecutor contributors should be run when other operations are executed.|
|**/p:**|ScriptDatabaseCollation=(BOOLEAN 'False')|Specifies whether differences in the database collation should be ignored or updated when you publish to a database.|
|**/p:**|ScriptDatabaseCompatibility=(BOOLEAN 'False')|Specifies whether differences in the database compatibility should be ignored or updated when you publish to a database.|
|**/p:**|ScriptDatabaseOptions=(BOOLEAN 'True')|Specifies whether target database properties should be set or updated as part of the publish action.|
|**/p:**|ScriptDeployStateChecks=(BOOLEAN 'False')|Specifies whether statements are generated in the publish script to verify that the database name and server name match the names specified in the database project.|
|**/p:**|ScriptFileSize=(BOOLEAN 'False')|Controls whether size is specified when adding a file to a filegroup.|
|**/p:**|ScriptNewConstraintValidation=(BOOLEAN 'True')|At the end of publish all of the constraints will be verified as one set, avoiding data errors caused by a check or foreign key constraint in the middle of publish. If set to False, your constraints are published without checking the corresponding data.|
|**/p:**|ScriptRefreshModule=(BOOLEAN 'True')|Include refresh statements at the end of the publish script.|
|**/p:**|Storage=({File&#124;Memory})|Specifies how elements are stored when building the database model. For performance reasons the default is InMemory. For large databases, File backed storage may be required and is only available for .NET Framework version of SqlPackage.|
|**/p:**|TreatVerificationErrorsAsWarnings=(BOOLEAN 'False')|Specifies whether errors encountered during publish verification should be treated as warnings. The check is performed against the generated deployment plan before the plan is executed against your target database. Plan verification detects problems such as the loss of target-only objects (such as indexes) that must be dropped to make a change. Verification will also detect situations where dependencies (such as a table or view) exist because of a reference to a composite project, but do not exist in the target database. You might choose to do this to get a complete list of all issues, instead of having the publish action stop on the first error.|
|**/p:**|UnmodifiableObjectWarnings=(BOOLEAN 'True')|Specifies whether warnings should be generated when differences are found in objects that cannot be modified, for example, if the file size or file paths were different for a file.|
|**/p:**|VerifyCollationCompatibility=(BOOLEAN 'True')|Specifies whether collation compatibility is verified.|
|**/p:**|VerifyDeployment=(BOOLEAN 'True')|Specifies whether checks should be performed before publishing that will stop the publish action if issues are present that might block successful publishing. For example, your publish action might stop if you have foreign keys on the target database that do not exist in the database project, and that causes errors when you publish.|

## SQLCMD Variables

The following table describes the format of the option that you can use to override the value of a SQL command (**sqlcmd**) variable used during a publish action. The values of variable specified on the command line override other values assigned to the variable (for example, in a publish profile).  
  
|Parameter|Default|Description|  
|-------------|-----------|---------------|  
|**/Variables:{PropertyName}={Value}**||Specifies a name value pair for an action-specific variable; {VariableName}={Value}. The DACPAC file contains the list of valid SQLCMD variables. An error results if a value is not provided for every variable.|  

## Next Steps

- Learn more about [SqlPackage](sqlpackage.md)
- [Troubleshooting with SqlPackage](./troubleshooting-issues-and-performance-with-sqlpackage.md)
