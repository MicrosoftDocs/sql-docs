---
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/11/2024
ms.topic: include
---
| Error | Severity | Event logged | Description |
| :--- | :--- | :--- | :--- |
| 26010 | 10 | Yes | The server could not load the certificate it needs to initiate an SSL connection. It returned the following error: %#x. Check certificates to make sure they are valid. |
| 26011 | 16 | Yes | The server was unable to initialize encryption because of a problem with a security library. The security library may be missing. Verify that security.dll exists on the system. |
| 26012 | 16 | Yes | The server has attempted to initialize SSL encryption when it has already been initialized. This indicates a problem with SQL Server. Contact Technical Support. |
| [26013](../mssqlserver-26013-database-engine-error.md) | 10 | Yes | The certificate \[Cert Hash(sha1) "%hs"\] was successfully loaded for encryption. |
| [26014](../mssqlserver-26014-database-engine-error.md) | 16 | Yes | Unable to load user-specified certificate \[Cert Hash(sha1) "%hs"\]. The server will not accept a connection. You should verify that the certificate is correctly installed. See "Configuring Certificate for Use by SSL" in Books Online. |
| 26015 | 16 | Yes | Unable to load user-specified certificate. Because connection encryption is required, the server will not be able to accept any connections. You should verify that the certificate is correctly installed. See "Configuring Certificate for Use by SSL" in Books Online. |
| 26017 | 10 | Yes | Unable to initialize SSL encryption because a valid certificate could not be found, and it is not possible to create a self-signed certificate. |
| 26018 | 10 | Yes | A self-generated certificate was successfully loaded for encryption. |
| 26022 | 10 | Yes | Server is listening on \[ %s \<%s\> %d\]. |
| 26023 | 16 | Yes | Server TCP provider failed to listen on \[ %s \<%s\> %d\]. Tcp port is already in use. |
| 26024 | 16 | Yes | Server failed to listen on %s \<%s\> %d. Error: %#x. To proceed, notify your system administrator. |
| 26025 | 10 | Yes | HTTP authentication succeeded for user '%.\*ls'.%.\*ls |
| 26026 | 14 | Yes | HTTP authentication failed.%.\*ls |
| 26027 | 10 | Yes | Virtual Interface Architecture protocol is not supported for this particular edition of SQL Server. |
| 26028 | 10 | Yes | Server named pipe provider is ready to accept connection on \[ %s \]. |
| 26029 | 16 | Yes | Server named pipe provider failed to listen on \[ %s \]. Error: %#x |
| 26032 | 10 | Yes | Server VIA provider is ready for clients to connect to \[ %hs:%d \]. |
| 26033 | 16 | Yes | Server VIA provider failed to initialize. Error: %#x |
| 26034 | 10 | Yes | The SQL Server Network Interface library was unable to execute polite termination due to outstanding connections. It will proceed with immediate termination. |
| 26035 | 16 | Yes | The SQL Server Network Interface library was unable to close socket handle due to a closesocket failure under memory pressure. Winsock error code: %#x. |
| 26037 | 10 | Yes | The SQL Server Network Interface library could not register the Service Principal Name (SPN) for the SQL Server service. Windows return code: %#x, state: %d. Failure to register a SPN might cause integrated authentication to use NTLM instead of Kerberos. This is an informational message. Further action is only required if Kerberos authentication is required by authentication policies and if the SPN has not been manually registered. |
| 26038 | 10 | Yes | The SQL Server Network Interface library could not deregister the Service Principal Name (SPN) for the SQL Server service. Error: %#x, state: %d. Administrator should deregister this SPN manually to avoid client authentication errors. |
| 26039 | 16 | Yes | The SQL Server Network Interface library was unable to load SPN related library. Error: %#x. |
| 26040 | 17 | Yes | Server TCP provider has stopped listening on port \[ %d \] due to an AcceptEx failure. Socket error: %#x, state: %d. The server will automatically attempt to reestablish listening. |
| 26041 | 10 | Yes | Server TCP provider has successfully reestablished listening on port \[ %d \]. |
| 26042 | 17 | Yes | Server HTTP provider has stopped listening due to a failure. Error: %#x, state: %d. The server will automatically attempt to reestablish listening. |
| 26043 | 10 | Yes | Server HTTP provider has successfully reestablished listening. |
| 26044 | 17 | Yes | Server named pipe provider has stopped listening on \[ %s \] due to a failure. Error: %#x, state: %d. The server will automatically attempt to reestablish listening. |
| 26045 | 10 | Yes | Server named pipe provider has successfully reestablished listening on \[ %s \]. |
| 26048 | 10 | Yes | Server local connection provider is ready to accept connection on \[ %s \]. |
| 26049 | 16 | Yes | Server local connection provider failed to listen on \[ %s \]. Error: %#x |
| 26050 | 17 | Yes | Server local connection provider has stopped listening on \[ %s \] due to a failure. Error: %#x, state: %d. The server will automatically attempt to re-establish listening. |
| 26051 | 10 | Yes | Server local connection provider has successfully re-established listening on \[ %s \]. |
| 26052 | 10 | Yes | SQL Server Network Interfaces initialized listeners on node %ld of a multi-node (NUMA) server configuration with node affinity mask 0x%0\*I64x. This is an informational message only. No user action is required. |
| 26053 | 16 | Yes | SQL Server Network Interfaces failed to initialize listeners on node %ld of a multi-node (NUMA) server configuration with node affinity mask 0x%0\*I64x. There may be insufficient memory. Free up additional memory, then turn the node off and on again. If the failure persists repeat this multiple times or restart the SQL Server. |
| 26054 | 16 | Yes | Could not find any IP address that this SQL Server instance depends upon. Make sure that the cluster service is running, that the dependency relationship between SQL Server and Network Name resources is correct, and that the IP addresses on which this SQL Server instance depends are available. Error code: %#x. |
| 26055 | 16 | Yes | The SQL Server failed to initialize VIA support library \[%hs\]. This normally indicates the VIA support library does not exist or is corrupted. Please repair or disable the VIA network protocol. Error: %#x. |
| 26056 | 10 | Yes | Failed to update the dedicated administrator connection (DAC) port number in the registry. Clients may not be able to discover the correct DAC port through the SQL Server Browser Service. Error: %#x. |
| 26057 | 16 | Yes | Failed to determine the fully qualified domain name of the computer while initializing SSL support. This might indicate a problem with the network configuration of the computer. Error: %#x. |
| 26058 | 16 | Yes | A TCP provider is enabled, but there are no TCP listening ports configured. The server cannot accept TCP connections. |
| 26059 | 10 | Yes | The SQL Server Network Interface library successfully registered the Service Principal Name (SPN) \[ %ls \] for the SQL Server service. |
| 26060 | 10 | Yes | The SQL Server Network Interface library successfully deregistered the Service Principal Name (SPN) \[ %ls \] for the SQL Server service. |
| 26061 | 10 | Yes | Failed to determine the fully qualified domain name of the computer while composing the Service Principal Name (SPN). This might indicate a problem with the network configuration of the computer. Error: %#x. |
| 26062 | 16 | Yes | Invalid parameter detected while initializing TCP listening port. Error: %#x, state: %d. Contact Technical Support. |
| 26063 | 10 | Yes | Warning: Support for the VIA protocol is deprecated and will be removed in a future version of Microsoft SQL Server. If possible, use a different network protocol and disable VIA. |
| 26064 | 10 | Yes | SQL Server could not listen on IP address \[%s\] because the cluster resource '%s' is not online (state = %d). This is an informational message and may indicate that resource '%s' has OR type of dependency on several IP addresses some of which are currently offline or in a failed state. Further action is only required if it is generally possible to bind the IP address of the cluster resource '%s' to a network segment on the current hosting node. |
| 26065 | 16 | Yes | Extended Protection for the SQL Server Database Engine is enabled, but the operating system does not support Extended Protection. Connection attempts using Windows Authentication might fail. Check for an operating system service pack to allow for Extended Protection through Service Binding and Channel Binding, or disable Extended Protection for the Database Engine. |
| 26066 | 16 | Yes | An error occurred while configuring cluster virtual IP addresses for Extended Protection. Connection attempts using Integrated Authentication might fail. Error: %d. |
| 26067 | 10 | Yes | The SQL Server Network Interface library could not register the Service Principal Name (SPN) \[ %ls \] for the SQL Server service. Windows return code: %#x, state: %d. Failure to register a SPN might cause integrated authentication to use NTLM instead of Kerberos. This is an informational message. Further action is only required if Kerberos authentication is required by authentication policies and if the SPN has not been manually registered. |
| 26068 | 10 | Yes | The SQL Server Network Interface library could not deregister the Service Principal Name (SPN) \[ %ls \] for the SQL Server service. Error: %#x, state: %d. Administrator should deregister this SPN manually to avoid client authentication errors. |
| 26069 | 10 | Yes | Started listening on virtual network name '%ls'. No user action is required. |
| 26070 | 10 | Yes | Stopped listening on virtual network name '%ls'. No user action is required. |
| 26071 | 16 | Yes | Failed to load the Cluster Resource Libraries: clusapi.dll and resutils.dll. SQL Server will be unable to accept TCP connections on clustered listeners. |
| 26072 | 10 | Yes | Found multiple dependent virtual network names for the Windows Failover Cluster Resource '%ls'. SQL Server will only listen on the first virtual network name resource: '%ls'. This may indicate that there is an incorrect configuration of the Windows Failover Cluster Resources for SQL Server. |
| 26073 | 16 | Yes | Failed to clean up event associated with TCP connection, this is most likely because the server was under heavy load. Operating system return code: %#x |
| 26075 | 16 | Yes | Failed to start a listener for virtual network name '%ls'. Error: %d. |
| 26076 | 10 | Yes | SQL Server is attempting to register a Service Principal Name (SPN) for the SQL Server service. Kerberos authentication will not be possible until a SPN is registered for the SQL Server service. This is an informational message. No user action is required. |
| 26077 | 20 | No | An asynchronous read has timed out |
| 26078 | 20 | No | Client disconnected during login |
| 26079 | 17 | Yes | Server TCP provider has stopped listening on port \[ %d \] due to a CreateSocket failure. Error: %#x, state: %d. The server will automatically attempt to reestablish listening. |
| 26080 | 10 | Yes | DISTRIBUTED_NETWORK_NAME is defined for FCI, listen to all IP addresses. No user action is required. |
| 26081 | 10 | Yes | DISTRIBUTED_NETWORK_NAME is NOT defined for FCI, listen to VNN IP address. No user action is required. |
| 26082 | 10 | Yes | DISTRIBUTED_NETWORK_NAME probing failed for cluster group \[ %ls \] with error code %#x. |
| 26083 | 10 | Yes | DISTRIBUTED_NETWORK_NAME probing failed with error code %#x. |
| 26084 | 10 | Yes | Started listening on listener network name '%ls' (VNN or DISTRIBUTED_NETWORK_NAME). No user action is required. |
| 26085 | 10 | Yes | Stopped listening on listener network name '%ls' (VNN or DISTRIBUTED_NETWORK_NAME). No user action is required. |
| 27001 | 16 | No | Reserved error message. Should never be issued. |
| 27002 | 16 | No | A null or invalid SqlCommand object was supplied to Fuzzy Lookup Table Maintenance by SQLCLR. Reset the connection. |
| 27003 | 16 | No | Bad token encountered during tokenization. |
| 27004 | 16 | No | Unexpected token type encountered during tokenization. |
| 27005 | 16 | No | Error Tolerant Index is corrupt. |
| 27006 | 16 | No | Deleted more than one rid from ridlist during delete operation. Error Tolerant Index is corrupt. |
| 27007 | 16 | No | Attempt to delete from empty ridlist. Error Tolerant Index is corrupt. |
| 27008 | 16 | No | rid to be deleted not found in rid-list. Error Tolerant Index is corrupt. |
| 27009 | 16 | No | Error Tolerant Index frequencies must be non-negative. Error Tolerant Index is corrupt. |
| 27010 | 16 | No | Attempt to insert row whose ID is already present. Error Tolerant Index is corrupt. |
| 27011 | 16 | No | No ridlist provided for appending. Error Tolerant Index is corrupt. |
| 27012 | 16 | No | Cannot delete token. Error Tolerant Index is corrupt. |
| 27013 | 16 | No | Tokenizer object has no delimiter set. Error Tolerant Index is corrupt. |
| 27014 | 16 | No | Deletion failed because token does not occur in index. Error Tolerant Index is corrupt. |
| 27015 | 16 | No | Unexpected ridlist length. Error Tolerant Index is corrupt. |
| 27016 | 16 | No | Cannot connect to Error Tolerant Index. Bad or missing SqlCommand object. |
| 27017 | 16 | No | Failed to drop index on reference table copy. |
| 27018 | 16 | No | Could not retrieve metadata from Error Tolerant Index. The index is probably corrupt. |
| 27019 | 16 | No | Could not initialize from metadata contained in Error Tolerant Index. The index is probably corrupt. |
| 27022 | 16 | No | An error specific to fuzzy lookup table maintenance has occurred. |
| 27023 | 16 | No | A system error occurred during execution of fuzzy lookup table maintenance. |
| 27024 | 16 | No | Cannot write at negative index position. Could not update Error Tolerant Index. The index is probably corrupt. |
| 27025 | 16 | No | Argument is not a valid hex string. Could not initialize from metadata contained in Error Tolerant Index. The index is probably corrupt. |
| 27026 | 16 | No | Negative count in Error Tolerant Index metadata. The index is probably corrupt. |
| 27027 | 16 | No | Error tolerant index metadata contains unsupported normalization flags. The index is probably corrupt. |
| 27028 | 16 | No | Invalid Error Tolerant Index metadata. The index is probably corrupt. |
| 27029 | 16 | No | Invalid Error Tolerant Index metadata version. |
| 27030 | 16 | No | Missing metadata. The Error Tolerant Index is probably corrupt. |
| 27031 | 16 | No | Unable to parse token counts in Error Tolerant Index metadata. The index is probably corrupt. |
| 27032 | 16 | No | Error Tolerant Index Metadata string too long. Index is probably corrupt. |
| 27033 | 16 | No | Error Tolerant Index Metadata length limit exceeded. |
| 27034 | 16 | No | Unexpected end of Error Tolerant Index metadata. Index is probably corrupt. |
| 27037 | 16 | No | No table name provided for Error Tolerant Index. The index is probably corrupt. |
| 27038 | 16 | No | No input provided for decoding in Error Tolerant Index metadata. Index is probably corrupt. |
| 27039 | 16 | No | No input provided for encoding in Error Tolerant Index metadata. Index is probably corrupt. |
| 27040 | 16 | No | No Error Tolerant Index metadata string provided for initialization. Index is probably corrupt. |
| 27041 | 16 | No | No Error Tolerant Index metadata provided for serialization. The index is probably corrupt. |
| 27042 | 16 | No | Could not lookup object_id. No object name provided. |
| 27043 | 16 | No | Could not lookup object_id. Null command object provided. |
| 27044 | 16 | No | Open connection required. Cannot query Error Tolerant Index. |
| 27045 | 16 | No | Cannot write to null output buffer. Could not update Error Tolerant Index. The index is probably corrupt. |
| 27046 | 16 | No | Output buffer provided is too small. Could not update Error Tolerant Index. The index is probably corrupt. |
| 27047 | 16 | No | The number of min-hash q-grams per token must be positive. |
| 27048 | 16 | No | Could not create index on reference table copy. |
| 27049 | 16 | No | Reference table (or internal copy) missing integer identity column. Error tolerant index is probably corrupt. |
| 27050 | 16 | No | The maximum allow integer identity value has been reached. Consider re-building the error tolerant index to use any gaps in sequence. |
| 27051 | 16 | No | Could not read rid from data provided (missing column name, null reader object, or corrupted data). The index is probably corrupted. |
| 27052 | 16 | No | Table maintenance insertion failed. |
| 27053 | 16 | No | A positive q-gram length is required for tokenization. |
| 27054 | 16 | No | Maintenance trigger already installed on this reference table. |
| 27055 | 16 | No | Missing extended property on maintenance trigger. |
| 27056 | 16 | No | Maintenance trigger name out of sync with Error Tolerant Index metadata. Index is probably corrupt. |
| 27058 | 16 | No | A SQL error occurred during execution of fuzzy lookup table maintenance. |
| 27059 | 16 | No | Could not lookup object_id. The reference table or maintenance trigger could not be found. |
| 27060 | 16 | No | The Error Tolerant Index table name provided is not a valid SQL identifier. |
| 27061 | 16 | No | The Error Tolerant Index table name provided refers to a missing table. Check sys.tables. |
| 27062 | 16 | No | An auxiliary Fuzzy Lookup table maintenance table is missing. |
| 27063 | 16 | No | An auxiliary Fuzzy Lookup table maintenance table name is null. Maintenance cannot proceed. |
| 27064 | 16 | No | The row deleted from the reference table could not be located in the reference table copy. |
| 27065 | 16 | No | Fuzzy Lookup Table Maintenance is not installed or the Error Tolerant Index is corrupt. |
| 27100 | 16 | No | The input parameter, '%ls', cannot be null. Provide a valid value for this parameter. |
| 27101 | 16 | No | The value specified for the input parameter, '%ls', is not valid. Provide a valid value for this parameter. |
| 27102 | 16 | No | The input parameter, '%ls', cannot be empty. Provide a valid value for this parameter. |
| 27103 | 16 | No | Cannot find the execution instance '%I64d' because it does not exist or you do not have sufficient permissions. |
| 27104 | 16 | No | Cannot find the folder '%ls' because it does not exist or you do not have sufficient permissions. |
| 27105 | 16 | No | Cannot find the operation '%I64d' because it does not exist or you do not have sufficient permissions. |
| 27106 | 16 | No | Cannot find the parameter '%ls' because it does not exist. |
| 27107 | 16 | No | The specified %ls already exists. |
| 27108 | 16 | No | The path for '%ls' cannot be found. The operation will now exit. |
| 27109 | 16 | No | Cannot find the project '%ls' because it does not exist or you do not have sufficient permissions. |
| 27110 | 16 | No | Default permissions for the project cannot be granted to the user. Make sure that the user can be assigned these permissions. |
| 27111 | 16 | No | Cannot find the reference '%I64d' because it is not part of the project or you do not have sufficient permissions. |
| 27112 | 16 | No | Unable to update the row in the table, '%ls'. Make sure that this row exists. |
| 27113 | 16 | No | Unable to delete one or more rows in the table, '%ls'. Make sure that these rows exist. |
| 27114 | 16 | No | Cannot find the reference '%I64d' because it does not exist or you do not have sufficient permissions. |
| 27115 | 16 | No | Cannot find the target folder '%ls' because it does not exist or you do not have sufficient permissions. |
| 27116 | 16 | No | Conversion failed while performing encryption. |
| 27117 | 16 | No | Failed to decrypt the project. The symmetric key that was used to encrypt it may have been deleted. Delete the project and deploy it again. |
| 27118 | 16 | No | Failed to deploy the project. Try again later. |
| 27119 | 16 | No | Failed to encrypt the project named '%ls'. The symmetric key may have been deleted. Delete the project and deploy it again. |
| 27120 | 16 | No | Failed to grant permission '%ls'. |
| 27121 | 16 | No | The project is currently running or has completed. An instance of execution can only be started once. |
| 27122 | 16 | No | Unable to perform impact analysis and lineage. The package data or configuration data might not be valid. To validate package data, open the package in Business Intelligence Development Studio. To validate configuration data, open the configuration XML file in an XML editor. |
| 27123 | 16 | No | The operation cannot be started by an account that uses SQL Server Authentication. Start the operation with an account that uses Integrated Authentication. |
| 27124 | 16 | No | Integration Services server cannot stop the operation. The specified operation with ID '%I64d' is not valid or is not running. |
| 27125 | 16 | No | Integration Services server cannot stop the operation. The specified operation is not in a consistent state and cannot be stopped. |
| 27126 | 16 | No | Integration Services server cannot stop the operation. The specified operation is already in Stopping state. |
| 27127 | 16 | No | The Integration Services catalog '%ls' does not exists. |
| 27128 | 16 | No | The name '%ls' is not valid. It consists of characters that are not allowed. |
| 27129 | 16 | No | The folder '%ls' already exists or you have not been granted the appropriate permissions to change it. |
| 27130 | 16 | No | Integration Services server was unable to impersonate the caller. Operating system returned error code: %ls. |
| 27131 | 16 | No | Integration Services server was unable to start the process, '%ls'. Operating system returned error code: %ls. |
| 27132 | 16 | No | Integration Services server was unable to create process component, '%ls'. Operating system returned error code: %ls. |
| 27133 | 16 | No | Integration Services server was unable to wait for process, '%ls', to finish. Operating system returned error code: %ls. |
| 27135 | 16 | No | The database, '%ls', already exists. Rename or remove the existing database, and then run SQL Server Setup again. |
| 27136 | 16 | No | The required components for the 32-bit edition of Integration Services cannot be found. Run SQL Server Setup to add the required components. |
| 27137 | 16 | No | The registry key for the system setting '%ls' could not be found. The operation will now exit. |
| 27138 | 16 | No | The input parameter cannot be null. Provide a valid value for the parameter. |
| 27139 | 16 | No | Integration Services server cannot be configured because there are active operations. Wait until there are no active operations, and then try to configure the server again. |
| 27140 | 16 | No | The operation cannot be started because the user is not a member of the database role, '%ls', or the server role, '%ls'. Log in as a member of one of these roles, and then try to start the operation again. |
| 27141 | 16 | No | Integration Services server cannot be configured because there are execution logs. Please cleanup all execution logs, and then try to configure the server again. |
| 27142 | 16 | No | '%ls' is not a valid environment name. It consists of characters that are not allowed. |
| 27143 | 16 | No | Cannot access the operation with ID, '%I64d'. Verify that the user has appropriate permissions. |
| 27145 | 16 | No | '%ls' is not a valid project name. It consists of characters that are not allowed. |
| 27146 | 16 | No | Cannot access the package or the package does not exist. Verify that the package exists and that the user has permissions to it. |
| 27147 | 16 | No | The data type of the input value is not compatible with the data type of the '%ls'. |
| 27148 | 16 | No | The data type of the parameter does not match the data type of the environment variable. |
| 27149 | 16 | No | Integration Services server cannot perform the requested operation on the specified package now, because the package is in pending state. Wait until the package is not in pending state, and try to perform the operation again. |
| 27150 | 16 | No | The version of the project has changed since the instance of the execution has been created. Create a new execution instance and try again. |
| 27151 | 10 | No | The restore operation for project '%ls' from version '%I64d' has started. |
| 27152 | 10 | No | The restore operation for project '%ls' to version '%I64d' has completed. |
| 27153 | 16 | No | Default permissions for the operation cannot be granted to the user. Make sure that the user can be assigned these permissions. |
| 27154 | 16 | No | The @sensitive parameter is missing. This parameter is used to indicate if the parameter contains a sensitive value. |
| 27155 | 16 | No | Project restore has failed. You cannot restore a project having an object_version_lsn that is the same as the current project. |
| 27156 | 16 | No | The Integration Services server property, '%ls', cannot be found. Check the name of the property and try again. |
| 27157 | 16 | No | The environment '%ls' already exists or you have not been granted the appropriate permissions to create it. |
| 27158 | 16 | No | Error number, %d, occurred in the procedure, '%ls', at line number, %d. The error message was: '%ls' The error level was %d and the state was %d. |
| 27159 | 16 | No | Data type of the input value is not supported. |
| 27160 | 16 | No | The Stored Procedure, '%ls', failed to run because the Integration Services database (SSISDB) is not in single-user mode. In SQL Server Management Studio, launch Database Properties dialog box for SSISDB, switch to the Options tab, and set the Restrict Access property to single-user mode (SINGLE_USER). Then, try to run the stored procedure again. |
| 27161 | 10 | No | Warning: the requested permission has already been granted to the user. The duplicate request will be ignored. |
| 27162 | 16 | No | The property, '%ls', cannot be changed because the Integration Services database is not in single-user mode. In Management Studio, in the Database Properties dialog box, set the Restrict Access property to single-user mode. Then, try to change the value of the property again. |
| 27163 | 16 | No | The value for the Integration Services server property, '%ls', is not valid. In Management Studio, in the Integration Services Properties dialog box, enter a valid value for this property. |
| 27165 | 10 | No | Warning: During startup, the Integration Services server marked operation %I64d (type %d, status %d) as terminated. Please check the operating system error logs for operation details. |
| 27166 | 16 | No | The installed version of SQL Server does not support the installation of the Integration Services server. Update SQL Server, and then try installing the Integration Services server again. |
| 27167 | 16 | No | Failed to change the encryption algorithm to '%ls'. An error occurred while encrypting the environment variables with the '%ls' algorithm. |
| 27168 | 16 | No | Failed to change the encryption algorithm to '%ls'. An error occurred while encrypting the parameter values using the '%ls' algorithm. |
| 27169 | 16 | No | Failed to create a log entry for the requested operation. |
| 27170 | 16 | No | Failed to retrieve the project named '%ls'. |
| 27171 | 16 | No | The value specified is not valid. A value of data type '%ls' is required. |
| 27172 | 16 | No | The certificate and symmetric key used to encrypt project '%ls' does not exist or you do not have sufficient permissions. |
| 27173 | 16 | No | The environment variable '%ls' already exists. |
| 27175 | 16 | No | The execution has already completed. |
| 27176 | 16 | No | The parameter '%ls' does not exist or you do not have sufficient permissions. |
| 27177 | 16 | No | Environment names must be unique. There is already an environment named '%ls'. |
| 27178 | 16 | No | Unable to execute the project named '%ls'. You do not have sufficient permissions. |
| 27179 | 16 | No | The object version does not match the project id or you do not have sufficient permissions. |
| 27180 | 16 | No | %ls is not a valid environment variable name. It consists of characters that are not allowed. |
| 27181 | 16 | No | The project '%ls' already exists or you have not been granted the appropriate permissions to access it. |
| 27182 | 16 | No | The environment '%s' does not exist or you have not been granted the appropriate permissions to access it. |
| 27183 | 16 | No | The environment variable '%ls' does not exist or you have not been granted the appropriate permissions to access it. |
| 27184 | 16 | No | In order to execute this package, you need to specify values for the required parameters. |
| 27185 | 16 | No | The validation record for ID '%I64d' does not exist or you have not been granted the appropriate permissions to access it. |
| 27186 | 16 | No | One or more environment variables could not be found in the referenced environment. |
| 27187 | 16 | No | The project does not exist or you have not been granted the appropriate permissions to access it. |
| 27188 | 16 | No | Only members of the ssis_admin or sysadmin server roles can create, delete, or rename a catalog folder. |
| 27189 | 16 | No | Catalog folder name cannot be NULL or an empty string. |
| 27190 | 16 | No | The folder '%ls' already exists or you have not been granted the appropriate permissions to create it. |
| 27191 | 16 | No | The '%d' permission is not applicable to objects of type '%d'. Grant, deny, or revoke of this permission is not allowed. |
| 27192 | 16 | No | The caller has not been granted the MANAGEPERMISSION permission for the specified object. |
| 27193 | 16 | No | SQL Server %ls is required to install Integration Services. It cannot be installed on this version of SQL Server. |
| 27194 | 16 | No | Cannot find the project because it does not exist or you do not have sufficient permissions. |
| 27195 | 16 | No | The operation failed because the execution timed out. |
| 27196 | 16 | No | Failed to delete the folder '%ls' because it is not empty. Only empty folders can be deleted. |
| 27197 | 16 | No | The specified %ls %ld does not exist. |
| 27198 | 16 | No | Failed to locate records for the specified operation (ID %I64d). |
| 27199 | 16 | No | %ls is not a valid folder name. It consists of characters that are not allowed. |
| 27200 | 16 | No | The project is missing the specified environment reference. |
| 27201 | 16 | No | Values are missing for required parameters in this package. Specify values in order to start the validation. |
| 27202 | 16 | No | This project is missing one or more environment references. In order to use environment variables, specify the corresponding environment reference identifier. |
| 27203 | 16 | No | Failed to deploy project. For more information, query the operation_messages view for the operation identifier '%I64d'. |
| 27204 | 16 | No | Failed to create environment reference. This project already has an reference to the specified environment. |
| 27205 | 16 | No | Some of the property values for this parameter are missing. |
| 27206 | 16 | No | Failed to deploy project to the folder '%ls'. You do not have sufficient permissions to deploy this project. |
| 27207 | 16 | No | Failed to locate one or more variables in the environment '%ls'. |
| 27208 | 16 | No | The environment reference '%I64d' is not associated with the project. |
| 27209 | 16 | No | Failed to create environment to the folder '%ls'. You do not have sufficient permissions to create this environment. |
| 27210 | 16 | No | Conversion failed when converting the %ls to data type %ls. |
| 27212 | 16 | No | Data tap can only be added or removed when the execution status is created. |
| 27213 | 16 | No | The package path and data flow path ID strings already exist for the execution ID %I64d. Provide package path and data flow path ID strings that are not in the catalog.execution_data_taps view. |
| 27214 | 16 | No | The data flow task GUID '%ls' and the data flow path ID string already exist for the execution ID %I64d. Provide a data flow task GUID and a data flow path ID string that are not in the catalog.execution_data_taps view. |
| 27215 | 16 | No | The data tap '%I64d' does not exist, or you do not have sufficient permissions to remove it. Provide a valid data tap ID. |
| 27216 | 16 | No | The number of rows must be a non-negative value. Specify a valid value. |
| 27217 | 16 | No | The logging level '%d' is undefined. Provide one of the following logging levels: 0 (None), 1 (Basic), 2 (Performance), 3 (Verbose), 4 (RuntimeLineage), 100 (Customized). |
| 27218 | 16 | No | The Integration Services Server cannot find the running process for execution ID %I64d. Provide a valid execution ID. |
| 27219 | 16 | No | Caller does not have permissions to execute the stored procedure. |
| 27220 | 16 | No | SSISDB database does not exist. Create the SSISDB database. |
| 27221 | 16 | No | Unable to map an environment variable with the sensitive property set to True, to a parameter with the sensitive property set to False. Ensure that the property settings match. |
| 27222 | 16 | No | The required components for the 64-bit edition of Integration Services cannot be found. Run SQL Server Setup to install the required components. |
| 27223 | 16 | No | Cannot move the project to folder '%ls' due to insufficient permissions. |
| 27224 | 16 | No | The parameter value cannot be changed after the execution has been started. |
| 27225 | 16 | No | The property cannot be overridden after the execution has been started. |
| 27226 | 16 | No | The database principal has granted or denied permissions to catalog objects in the database and cannot be dropped. |
| 27227 | 16 | No | The server '%ls' already exists. |
| 27228 | 16 | No | The server '%ls' does not exists. |
| 27229 | 16 | No | Cannot find the SQL login '%ls'. |
| 27230 | 16 | No | Failed to deploy the packages. Try again later. |
| 27231 | 16 | No | Failed to deploy packages. For more information, query the operation_messages view for the operation identifier '%I64d'. |
| 27232 | 16 | No | Failed to create customized logging level '%ls'. You do not have sufficient permissions to create customized logging level. |
| 27233 | 16 | No | Customized logging level name cannot be NULL or an empty string. |
| 27234 | 16 | No | '%ls' is not a valid customized logging level name. It consists of characters that are not allowed. |
| 27235 | 16 | No | The customized logging level '%ls' already exists. |
| 27236 | 16 | No | Failed to delete customized logging level '%ls'. You do not have sufficient permissions to delete customized logging level. |
| 27237 | 16 | No | The customized logging level '%ls' does not exist. |
| 27238 | 16 | No | Failed to rename customized logging level '%ls' with '%ls'. You do not have sufficient permissions to rename customized logging level. |
| 27239 | 16 | No | Failed to update customized logging level '%ls'. You do not have sufficient permissions to update customized logging level. |
| 27240 | 16 | No | The value can not be less than zero. |
| 27241 | 16 | No | Failed to update logging level '%d'. Provide the value for SERVER_CUSTOMIZED_LOGGING_LEVEL property, as it can not be null or empty for logging level '%d'. |
| 27242 | 16 | No | Cannot find the cluster execution job instance '%ls' because it does not exist or you do not have sufficient permissions. |
| 27243 | 16 | No | Cannot find the cluster worker agent '%ls' because it does not exist or you do not have sufficient permissions. |
| 27246 | 16 | No | Cannot find the cluster execution task instance '%ls' because it does not exist or you do not have sufficient permissions. |
| 27251 | 16 | No | Cannot add a worker agent to execute the cluster job instance '%ls' because the job is specified for all worker agents. |
| 27252 | 16 | No | The Integration Services cluster worker agent '%ls' is disabled. |
| 27255 | 16 | No | The job id of the execution is null. Create a job for the execution first and try again. |
| 27256 | 16 | No | The event message does not exist. |
| 27257 | 16 | No | There is no active worker agent. |
| 27259 | 16 | No | Failed to enable worker agent because current SQL Server edition only support limited number of worker agents. |
| 27260 | 16 | No | Only members of sysadmin server roles can perform this action. |
| 27261 | 16 | No | There is no Scale Out Master installed. |
| 27301 | 16 | Yes | Operation is not supported unless all bricks are online. Bring the offline bricks back online, and retry the operation. |
| 27302 | 16 | Yes | An internal communication error occurred. Retry the operation. |
| 27303 | 16 | Yes | An unexpected error with HRESULT 0x%x occurred while processing backup files. Refer to the SQL Server error log for information about any related errors that were encountered. Address those errors if necessary, and retry the operation. |
| 27304 | 16 | Yes | The number of matching backup files (%d) is less than the required number (%d). Copy all the right backup files to the proper backup location, and retry the restore operation. |
| 27305 | 16 | Yes | The number of backup files (%d) is greater than the available number of bricks (%d). You must restore backup files on to at least as many bricks as present in the original MatrixDB where the backup was created. Configure a new MatrixDB with enough bricks to match the number of bricks in the original MatrixDB, and retry the operation. |
| 27306 | 16 | Yes | No matching backup files were found by any of the bricks. Copy the backup files to the correct backup location, and retry the operation. If the backup files belong to a different family, drop the database and then retry the operation. |
| 27307 | 16 | Yes | Brick ID %d has no backup files available. Copy the backup file for the segment to this brick, and retry the operation. |
| 27308 | 16 | Yes | SegmentID %d from the archive does not match the segmentID %d from the specified filename on brick %d. |
| 27309 | 16 | Yes | Restore recovery failed because a problem was detected with data in a temporary file that is needed for recovery. This error can be caused because of a storage device failure or because the restore checkpoint restart file has been moved or deleted. Retry the last restore operation. If the operation fails again, retry the entire restore sequence. |
| 27310 | 16 | Yes | Restore failed because all the backup files did not come from the same backup operation. Retry the restore operation using the backup files from a single backup operation. |
| 27311 | 16 | Yes | Restore failed because of an internal communications error. Retry the restore operation after addressing any network issues. |
| 27312 | 16 | Yes | Device name '%ls' is not valid in a Matrix Backup/Restore command. The device name contains illegal characters or file name is empty. Retry the command using a valid device name. |
| 27313 | 16 | Yes | Restore command is not valid as both Standalone and Matrix archives were found while disambiguating the device name(s). Retry the command after making sure that there is only a Standalone archive or matching Matrix archives. |
| 27314 | 16 | Yes | Restore failed because the brick count of %ld is different from the BACKUP GROUP COUNT %ld from the backup file. Make sure that all Matrix backup files are from the same backup operation, or re-configure the brick count of the MatrixDB to match the group count of the MatrixDB where the backup files were created. |
| 27315 | 16 | Yes | An unexpected error occurred while executing a Backup/Restore command. Refer to the SQL Server error log for information about any related errors that were encountered. Address those errors if necessary, and retry the operation. |
| 27316 | 16 | Yes | Device name '%.\*ls' is not a valid MOVE target of a Matrix Restore command. Retry the command using a valid relative path name. |
| 27317 | 16 | Yes | The backup files of the RESTORE command do not match with the segments of the existing database. Copy the backup files to the correct backup location, using family GUID if necessary, and retry the restore operation. |
| 27318 | 16 | Yes | The backup files of the FULL RESTORE command do not match with the segments of the existing database. Copy the correct backup files to the backup location, using family GUID if necessary, and retry the restore operation or use WITH REPLACE option to overwrite the existing database. |
| 27319 | 16 | Yes | The backup type of all the archive segments is not the same. Copy the correct backup files to the backup location, and retry the restore operation. |
