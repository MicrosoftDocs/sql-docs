---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    26010    |    10    |    Yes    |    The server could not load the certificate it needs to initiate an SSL connection. It returned the following error: %#x. Check certificates to make sure they are valid.    |
|    26011    |    16    |    Yes    |    The server was unable to initialize encryption because of a problem with a security library. The security library may be missing. Verify that security.dll exists on the system.    |
|    26012    |    16    |    Yes    |    The server has attempted to initialize SSL encryption when it has already been initialized. This indicates a problem with SQL Server. Contact Technical Support.    |
|    [26013](../../relational-databases/errors-events/mssqlserver-26013-database-engine-error.md)    |    10    |    Yes    |    The certificate [Cert Hash(sha1) "%hs"] was successfully loaded for encryption.    |
|    [26014](../../relational-databases/errors-events/mssqlserver-26014-database-engine-error.md)    |    16    |    Yes    |    Unable to load user-specified certificate [Cert Hash(sha1) "%hs"]. The server will not accept a connection. You should verify that the certificate is correctly installed. See "Configuring Certificate for Use by SSL" in Books Online.    |
|    26015    |    16    |    Yes    |    Unable to load user-specified certificate. Because connection encryption is required, the server will not be able to accept any connections. You should verify that the certificate is correctly installed. See "Configuring Certificate for Use by SSL" in Books Online.    |
|    26017    |    10    |    Yes    |    Unable to initialize SSL encryption because a valid certificate could not be found, and it is not possible to create a self-signed certificate.    |
|    26018    |    10    |    Yes    |    A self-generated certificate was successfully loaded for encryption.    |
|    26022    |    10    |    Yes    |    Server is listening on [ %hs <%hs> %d].    |
|    26023    |    16    |    Yes    |    Server TCP provider failed to listen on [ %hs <%hs> %d]. Tcp port is already in use.    |
|    26024    |    16    |    Yes    |    Server failed to listen on %hs <%hs> %d. Error: %#x. To proceed, notify your system administrator.    |
|    26025    |    10    |    Yes    |    HTTP authentication succeeded for user '%.*ls'.%.*ls    |
|    26026    |    14    |    Yes    |    HTTP authentication failed.%.*ls    |
|    26027    |    10    |    Yes    |    Virtual Interface Architecture protocol is not supported for this particular edition of SQL Server.    |
|    26028    |    10    |    Yes    |    Server named pipe provider is ready to accept connection on [ %hs ].    |
|    26029    |    16    |    Yes    |    Server named pipe provider failed to listen on [ %hs ]. Error: %#x    |
|    26030    |    10    |    Yes    |    Server shared memory provider is ready for clients to connect to [ %hs ].    |
|    26031    |    16    |    Yes    |    Server shared memory provider failed to initialize. Error: %#x    |
|    26032    |    10    |    Yes    |    Server VIA provider is ready for clients to connect to [ %hs:%d ].    |
|    26033    |    16    |    Yes    |    Server VIA provider failed to initialize. Error: %#x    |
|    26034    |    10    |    Yes    |    The SQL Server Network Interface library was unable to execute polite termination due to outstanding connections. It will proceed with immediate termination.    |
|    26035    |    16    |    Yes    |    The SQL Server Network Interface library was unable to close socket handle due to a closesocket failure under memory pressure. Winsock error code: %#x.    |
|    26036    |    10    |    Yes    |    Server named pipe connections are restricted to local access only. This is an informational message only. No user action is required.    |
|    26037    |    10    |    Yes    |    The SQL Server Network Interface library could not register the Service Principal Name (SPN) for the SQL Server service. Error: %#x, state: %d. Failure to register an SPN may cause integrated authentication to fall back to NTLM instead of Kerberos. This is an informational message. Further action is only required if Kerberos authentication is required by authentication policies.    |
|    26038    |    10    |    Yes    |    The SQL Server Network Interface library could not deregister the Service Principal Name (SPN) for the SQL Server service. Error: %#x, state: %d. Administrator should deregister this SPN manually to avoid client authentication errors.    |
|    26039    |    16    |    Yes    |    The SQL Server Network Interface library was unable to load SPN related library. Error: %#x.    |
|    26040    |    17    |    Yes    |    Server TCP provider has stopped listening on port [ %d ] due to a failure. Error: %#x, state: %d. The server will automatically attempt to reestablish listening.    |
|    26041    |    10    |    Yes    |    Server TCP provider has successfully reestablished listening on port [ %d ].    |
|    26042    |    17    |    Yes    |    Server HTTP provider has stopped listening due to a failure. Error: %#x, state: %d. The server will automatically attempt to reestablish listening.    |
|    26043    |    10    |    Yes    |    Server HTTP provider has successfully reestablished listening.    |
|    26044    |    17    |    Yes    |    Server named pipe provider has stopped listening on [ %hs ] due to a failure. Error: %#x, state: %d. The server will automatically attempt to reestablish listening.    |
|    26045    |    10    |    Yes    |    Server named pipe provider has successfully reestablished listening on [ %hs ].    |
|    26046    |    17    |    Yes    |    Server shared memory provider has stopped listening due to a failure. Error: %#x, state: %d. The server will automatically attempt to reestablish listening.    |
|    26047    |    10    |    Yes    |    Server shared memory provider has successfully reestablished listening.    |
|    26048    |    10    |    Yes    |    Server local connection provider is ready to accept connection on [ %hs ].    |
|    26049    |    16    |    Yes    |    Server local connection provider failed to listen on [ %hs ]. Error: %#x    |
|    26050    |    17    |    Yes    |    Server local connection provider has stopped listening on [ %hs ] due to a failure. Error: %#x, state: %d. The server will automatically attempt to re-establish listening.    |
|    26051    |    10    |    Yes    |    Server local connection provider has successfully re-established listening on [ %hs ].    |
|    26052    |    10    |    Yes    |    SQL Server Network Interfaces initialized listeners on node %ld of a multi-node (NUMA) server configuration with node affinity mask 0x%0*I64x. This is an informational message only. No user action is required.    |
|    26053    |    16    |    Yes    |    SQL Server Network Interfaces failed to initialize listeners on node %ld of a multi-node (NUMA) server configuration with node affinity mask 0x%0*I64x. There may be insufficient memory. Free up additional memory, then turn the node off and on again. If the failure persists repeat this multiple times or restart the SQL Server.    |
|    26054    |    16    |    Yes    |    Could not find any IP address that this SQL Server instance depends upon. Make sure that the cluster service is running, that the dependency relationship between SQL Server and Network Name resources is correct, and that the IP addresses on which this SQL Server instance depends are available. Error code: %#x.    |
|    26055    |    16    |    Yes    |    The SQL Server failed to initialize VIA support library [%hs]. This normally indicates the VIA support library does not exist or is corrupted. Please repair or disable the VIA network protocol. Error: %#x.    |
|    26056    |    10    |    Yes    |    Failed to update the dedicated administrator connection (DAC) port number in the registry. Clients may not be able to discover the correct DAC port through the SQL Server Browser Service. Error: %#x.    |
|    26057    |    16    |    Yes    |    Failed to determine the fully qualified domain name of the computer while initializing SSL support. This might indicate a problem with the network configuration of the computer. Error: %#x.    |
|    26058    |    16    |    Yes    |    A TCP provider is enabled, but there are no TCP listening ports configured. The server cannot accept TCP connections.    |
|    26059    |    10    |    Yes    |    The SQL Server Network Interface library successfully registered the Service Principal Name (SPN) [ %ls ] for the SQL Server service.    |
|    26060    |    10    |    Yes    |    The SQL Server Network Interface library successfully deregistered the Service Principal Name (SPN) [ %ls ] for the SQL Server service.    |
|    26061    |    10    |    Yes    |    Failed to determine the fully qualified domain name of the computer while composing the Service Principal Name (SPN). This might indicate a problem with the network configuration of the computer. Error: %#x.    |
|    26062    |    16    |    Yes    |    Invalid parameter detected while initializing TCP listening port. Error: %#x, state: %d. Contact Technical Support.    |
|    27001    |    16    |    No    |    Reserved error message. Should never be issued.    |
|    27002    |    16    |    No    |    A null or invalid SqlCommand object was supplied to Fuzzy Lookup Table Maintenance by SQLCLR. Reset the connection.    |
|    27003    |    16    |    No    |    Bad token encountered during tokenization.    |
|    27004    |    16    |    No    |    Unexpected token type encountered during tokenization.    |
|    27005    |    16    |    No    |    Error Tolerant Index is corrupt.    |
|    27006    |    16    |    No    |    Deleted more than one rid from ridlist during delete operation. Error Tolerant Index is corrupt.    |
|    27007    |    16    |    No    |    Attempt to delete from empty ridlist. Error Tolerant Index is corrupt.    |
|    27008    |    16    |    No    |    rid to be deleted not found in rid-list. Error Tolerant Index is corrupt.    |
|    27009    |    16    |    No    |    Error Tolerant Index frequencies must be non-negative. Error Tolerant Index is corrupt.    |
|    27010    |    16    |    No    |    Attempt to insert row whose ID is already present. Error Tolerant Index is corrupt.    |
|    27011    |    16    |    No    |    No ridlist provided for appending. Error Tolerant Index is corrupt.    |
|    27012    |    16    |    No    |    Cannot delete token. Error Tolerant Index is corrupt.    |
|    27013    |    16    |    No    |    Tokenizer object has no delimiter set. Error Tolerant Index is corrupt.    |
|    27014    |    16    |    No    |    Deletion failed because token does not occur in index. Error Tolerant Index is corrupt.    |
|    27015    |    16    |    No    |    Unexpected ridlist length. Error Tolerant Index is corrupt.    |
|    27016    |    16    |    No    |    Cannot connect to Error Tolerant Index. Bad or missing SqlCommand object.    |
|    27017    |    16    |    No    |    Failed to drop index on reference table copy.    |
|    27018    |    16    |    No    |    Could not retrieve metadata from Error Tolerant Index. The index is probably corrupt.    |
|    27019    |    16    |    No    |    Could not initialize from metadata contained in Error Tolerant Index. The index is probably corrupt.    |
|    27022    |    16    |    No    |    An error specific to fuzzy lookup table maintenance has occurred.    |
|    27023    |    16    |    No    |    A system error occurred during execution of fuzzy lookup table maintenance.    |
|    27024    |    16    |    No    |    Cannot write at negative index position. Could not update Error Tolerant Index. The index is probably corrupt.    |
|    27025    |    16    |    No    |    Argument is not a valid hex string. Could not initialize from metadata contained in Error Tolerant Index. The index is probably corrupt.    |
|    27026    |    16    |    No    |    Negative count in Error Tolerant Index metadata. The index is probably corrupt.    |
|    27027    |    16    |    No    |    Error tolerant index metadata contains unsupported normalization flags. The index is probably corrupt.    |
|    27028    |    16    |    No    |    Invalid Error Tolerant Index metadata. The index is probably corrupt.    |
|    27029    |    16    |    No    |    Invalid Error Tolerant Index metadata version.    |
|    27030    |    16    |    No    |    Missing metadata. The Error Tolerant Index is probably corrupt.    |
|    27031    |    16    |    No    |    Unable to parse token counts in Error Tolerant Index metadata. The index is probably corrupt.    |
|    27032    |    16    |    No    |    Error Tolerant Index Metadata string too long. Index is probably corrupt.    |
|    27033    |    16    |    No    |    Error Tolerant Index Metadata length limit exceeded.    |
|    27034    |    16    |    No    |    Unexpected end of Error Tolerant Index metadata. Index is probably corrupt.    |
|    27037    |    16    |    No    |    No table name provided for Error Tolerant Index. The index is probably corrupt.    |
|    27038    |    16    |    No    |    No input provided for decoding in Error Tolerant Index metadata. Index is probably corrupt.    |
|    27039    |    16    |    No    |    No input provided for encoding in Error Tolerant Index metadata. Index is probably corrupt.    |
|    27040    |    16    |    No    |    No Error Tolerant Index metadata string provided for initialization. Index is probably corrupt.    |
|    27041    |    16    |    No    |    No Error Tolerant Index metadata provided for serialization. The index is probably corrupt.    |
|    27042    |    16    |    No    |    Could not lookup object_id. No object name provided.    |
|    27043    |    16    |    No    |    Could not lookup object_id. Null command object provided.    |
|    27044    |    16    |    No    |    Open connection required. Cannot query Error Tolerant Index.    |
|    27045    |    16    |    No    |    Cannot write to null output buffer. Could not update Error Tolerant Index. The index is probably corrupt.    |
|    27046    |    16    |    No    |    Output buffer provided is too small. Could not update Error Tolerant Index. The index is probably corrupt.    |
|    27047    |    16    |    No    |    The number of min-hash q-grams per token must be positive.    |
|    27048    |    16    |    No    |    Could not create index on reference table copy.    |
|    27049    |    16    |    No    |    Reference table (or internal copy) missing integer identity column. Error tolerant index is probably corrupt.    |
|    27050    |    16    |    No    |    The maximum allow integer identity value has been reached. Consider re-building the error tolerant index to use any gaps in sequence.    |
|    27051    |    16    |    No    |    Could not read rid from data provided (missing column name, null reader object, or corrupted data). The index is probably corrupted.    |
|    27052    |    16    |    No    |    Table maintenance insertion failed.    |
|    27053    |    16    |    No    |    A positive q-gram length is required for tokenization.    |
|    27054    |    16    |    No    |    Maintenance trigger already installed on this reference table.    |
|    27055    |    16    |    No    |    Missing extended property on maintenance trigger.    |
|    27056    |    16    |    No    |    Maintenance trigger name out of sync with Error Tolerant Index metadata. Index is probably corrupt.    |
|    27058    |    16    |    No    |    A SQL error occurred during execution of fuzzy lookup table maintenance.    |
|    27059    |    16    |    No    |    Could not lookup object_id. The reference table or maintenance trigger could not be found.    |
|    27060    |    16    |    No    |    The Error Tolerant Index table name provided is not a valid SQL identifier.    |
|    27061    |    16    |    No    |    The Error Tolerant Index table name provided refers to a missing table. Check sys.tables.    |
|    27062    |    16    |    No    |    An auxiliary Fuzzy Lookup table maintenance table is missing.    |
|    27063    |    16    |    No    |    An auxiliary Fuzzy Lookup table maintenance table name is null. Maintenance cannot proceed.    |
|    27064    |    16    |    No    |    The row deleted from the reference table could not be located in the reference table copy.    |
|    27065    |    16    |    No    |    Fuzzy Lookup Table Maintenance is not installed or the Error Tolerant Index is corrupt.    |
