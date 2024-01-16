---
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/11/2024
ms.topic: include
---
| Error | Severity | Event logged | Description |
| :--- | :--- | :--- | :--- |
| 28000 | 16 | No | The decrypted session key has an unexpected size. |
| 28001 | 16 | No | A corrupted message has been received. It contains invalid flags. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28002 | 16 | No | Cannot start service broker manager. Operating system error: %ls. |
| 28003 | 16 | No | An internal service broker error occurred. Operating system error: %ls. |
| 28004 | 16 | No | This message could not be delivered because the '%S_MSG' action cannot be performed in the '%.\*ls' state. |
| 28005 | 16 | No | An exception occurred while enqueueing a message in the target queue. Error: %d, State: %d. %.\*ls |
| 28006 | 14 | No | User does not have permission to %S_MSG the conversation '%.\*ls' in state '%.\*ls'. Only members of the sysadmin fixed server role and the db_owner fixed database role have this permission. |
| 28007 | 16 | No | A corrupted message has been received. The highest seen message number must be greater than the acknowledged message number. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28008 | 16 | No | The conversation handle '{%.8x-%.4x-%.4x-%.2x%.2x-%.2x%.2x%.2x%.2x%.2x%.2x}' is invalid. |
| 28009 | 16 | No | The crypto API has detected bad data while trying to perform a decryption operation. |
| 28010 | 16 | No | This message could not be delivered because it contains an invalid acknowledged message number. Highest expected message number: %I64d. Acknowledged message number: %I64d, fragment number: %d. |
| 28011 | 16 | No | This message could not be delivered because its %S_MSG has expired or is invalid. |
| 28012 | 16 | No | The Service Broker in the target database is unavailable: '%S_MSG'. |
| 28013 | 16 | No | The service broker is administratively disabled. |
| 28014 | 16 | No | The database is in read-only mode. |
| 28015 | 16 | No | The database is in single-user mode. |
| 28016 | 16 | No | The message has been dropped because the service broker in the target database is unavailable: '%S_MSG'. |
| 28017 | 16 | No | The message has been dropped because the target service broker is unreachable. |
| 28018 | 16 | No | The database is a replica of a mirrored database. |
| 28019 | 16 | No | System error %d occurred while creating a new message element GUID for this forwarded message. |
| 28020 | 16 | No | Could not create user token for user %d in database %d. |
| 28021 | 16 | No | One or more messages could not be delivered to the local service targeted by this dialog. |
| 28022 | 10 | No | An error occurred while looking up the public key certificate associated with this SQL Server instance: The certificate is not yet valid. |
| 28023 | 10 | No | An error occurred while looking up the public key certificate associated with this SQL Server instance: The certificate has expired. |
| 28024 | 16 | Yes | The security certificate bound to database principal (Id: %i) is not yet valid. Either wait for the certificate to become valid or install a certificate that is currently valid. |
| 28025 | 16 | Yes | The security certificate bound to database principal (Id: %i) has expired. Create or install a new certificate for the database principal. |
| 28026 | 10 | No | Connection handshake failed. Not enough memory available. State %d. |
| 28027 | 10 | No | Connection handshake failed. There is no compatible %S_MSG. State %d. |
| 28028 | 10 | No | Connection handshake failed. Could not send a handshake message because the connection was closed by peer. State %d. |
| 28029 | 10 | No | Connection handshake failed. Unexpected event (%d) for current context (%d). State %d. |
| 28030 | 10 | No | Connection handshake failed. A call to the SQL Server Network Interface failed: (%x) %ls. State %d. |
| 28031 | 10 | No | Connection handshake failed. An OS call failed: (%x) %ls. State %d. |
| 28032 | 10 | No | A previously existing connection with the same peer was detected during connection handshake. This connection lost the arbitration and it will be closed. All traffic will be redirected to the previously existing connection. This is an informational message only. No user action is required. State %d. |
| 28033 | 10 | No | A new connection was established with the same peer. This connection lost the arbitration and it will be closed. All traffic will be redirected to the newly opened connection. This is an informational message only. No user action is required. State %d. |
| 28034 | 10 | No | Connection handshake failed. The login '%.\*ls' does not have CONNECT permission on the endpoint. State %d. |
| 28035 | 10 | No | Connection handshake failed. The certificate used by the peer is invalid due to the following reason: %S_MSG. State %d. |
| 28036 | 10 | No | Connection handshake failed. The certificate used by this endpoint was not found: %S_MSG. Use DBCC CHECKDB in master database to verify the metadata integrity of the endpoints. State %d. |
| 28037 | 10 | No | Connection handshake failed. Error %d occurred while initializing the private key corresponding to the certificate. The SQL Server errorlog and the operating system error log may contain entries related to this error. State %d. |
| 28038 | 10 | No | Connection handshake failed. The handshake verification failed. State %d. |
| 28039 | 10 | No | Connection handshake failed. The receive SSPI packet is not of type of the negotiated package. State %d. |
| 28040 | 10 | No | A corrupted message has been received. The adjacent error message header is invalid. |
| 28041 | 16 | No | A corrupted message has been received. The encrypted payload offset is invalid (%d). |
| 28042 | 16 | No | A corrupted message has been received. The arbitration request header is invalid. |
| 28043 | 16 | No | A corrupted message has been received. The arbitration response header is invalid. |
| 28044 | 16 | No | A corrupted message has been received. It is not encrypted and signed using the currently configured endpoint algorithm. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28045 | 10 | No | Connection handshake failed. The certificate used by the peer does not match the one in master database with same issuer name and serial number. State %d. |
| 28046 | 10 | Yes | %S_MSG Login succeeded for user '%.\*ls'. Authentication mode: %.\*ls. %.\*ls |
| 28047 | 10 | Yes | %S_MSG login attempt failed with error: '%.\*ls'. %.\*ls |
| 28048 | 10 | Yes | %S_MSG login attempt by user '%.\*ls' failed with error: '%.\*ls'. %.\*ls |
| 28049 | 16 | No | A corrupted message has been received. The proxy connect message header is invalid. |
| 28050 | 10 | No | The session keys for this conversation could not be created or accessed. The database master key is required for this operation. |
| 28051 | 10 | No | Could not save a dialog session key. A master key is required in the database to save the session key. |
| 28052 | 16 | No | Cannot decrypt session key while regenerating master key with FORCE option. |
| 28053 | 16 | No | Service Broker could not upgrade conversation session keys in database '%.\*ls' to encrypted format (Error: %d). The Service Broker in this database was disabled. A master key is required to the database in order to enable the broker. |
| 28054 | 16 | No | Service Broker needs to access the master key in the database '%.\*ls'. Error code:%d. The master key has to exist and the service master key encryption is required. |
| 28055 | 16 | No | The certificate '%.\*ls' is not valid for endpoint authentication. The certificate must have a private key encrypted with the database master key and current UTC date has to be between the certificate start date and the certificate expiration date. |
| 28056 | 16 | No | This message could not be delivered because the user with ID %i in database ID %i does not have control permission on the service. Service name: '%.\*ls'. |
| 28057 | 10 | No | Service Broker in database '%.\*ls' has a pending conversation upgrade operation. A database master key in the database is required for this operation to complete. |
| 28058 | 16 | No | Service Broker could not upgrade this conversation during a database upgrade operation. |
| 28059 | 16 | No | Connection handshake failed. The received premaster secret of size %d does not have the expected size of %d. State %d. |
| 28060 | 16 | No | The AES encryption algorithm is only supported on Windows XP, Windows Server 2003 or later versions. |
| 28061 | 16 | No | A corrupted message has been received. The adjacent message integrity check signature could not be validated. |
| 28062 | 16 | No | A corrupted message has been received. The signed dialog message header is invalid. |
| 28063 | 16 | No | A corrupted message has been received. A required variable data field is not present: %S_MSG. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28064 | 16 | No | A corrupted message has been received. A string variable data field is not a valid UNICODE string: %S_MSG. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28065 | 16 | No | A corrupted message has been received. The unsigned dialog message header is invalid. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28066 | 16 | No | A corrupted message has been received. The security dialog message header is invalid. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28067 | 16 | No | A corrupted message has been received. The encrypted offset of the envelope does not match the payload encrypted offset. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28068 | 16 | No | A corrupted message has been received. The envelope payload is bigger than the message. This occurred in the message with Conversation ID '%.\*ls', Initiator: %d, and Message sequence number: %I64d. |
| 28069 | 16 | Yes | Unexpected session key when encrypting a dialog message. |
| 28070 | 10 | No | Connection handshake failed. The received SSPI message Confirm status is unexpected. State %d. |
| 28071 | 10 | No | The received message could not be forwarded onto another connection. State %d. |
| 28072 | 16 | No | A serious error occurred in the Service Broker message transmitter (operation %i): Error: %i, State: %i. Message transmission will resume in %i seconds. |
| 28073 | 16 | No | An out of memory condition has occurred in the Service Broker message transmitter (operation %i). Message transmission will resume in %i seconds. |
| 28074 | 16 | No | Service Broker could not upgrade conversation with conversation_handle '%ls'. Use END CONVERSATION ... WITH CLEANUP to delete this conversation, then try again to enable the broker. Use ALTER DATABASE ... SET ERROR_BROKER to error all conversations in this database. Use ALTER DATABASE ... SET NEW_BROKER to delete all conversations in this database. |
| 28075 | 10 | No | The broker in the sender's database is in single user mode. Messages cannot be delivered while in single user mode. |
| 28076 | 10 | No | Could not query the FIPS compliance mode flag from registry. Error %ls. |
| 28077 | 10 | No | %S_MSG endpoint is running in FIPS compliance mode. This is an informational message only. No user action is required. |
| 28078 | 10 | No | The RC4 encryption algorithm is not supported when running in FIPS compliance mode. |
| 28079 | 10 | No | Connection handshake failed. The received SSPI packet is not of the expected direction. State %d. |
| 28080 | 10 | No | Connection handshake failed. The %S_MSG endpoint is not configured. State %d. |
| 28081 | 10 | No | Connection handshake failed. An unexpected status %d was returned when trying to send a handshake message. State %d. |
| 28082 | 10 | No | Connection handshake failed. An unexpected internal failure occurred when trying to marshal a message. State %d. |
| 28083 | 16 | No | The database principal '%.\*ls' cannot be used in a remote service binding because it cannot own certificates. Remote service bindings cannot be associated with 1) roles, 2) groups or 3) principals mapped to certificates or asymmetric keys. |
| 28084 | 10 | No | An error occurred in Service Broker internal activation while trying to scan the user queue '%ls' for its status. Error: %i, State: %i. %.\*ls This is an informational message only. No user action is required. |
| 28085 | 16 | No | The activated task was ended because the associated queue was dropped. |
| 28086 | 16 | No | The activated task was ended because either the queue or activation was disabled. |
| 28087 | 16 | No | The activated task was aborted because the invoked stored procedure '%ls' did not execute RECEIVE. |
| 28088 | 16 | No | The activated task was aborted due to an error (Error: %i, State %i). Check ERRORLOG or the previous "Broker:Activation" trace event for possible output from the activation stored procedure. |
| 28089 | 16 | No | The database principal '%.\*ls' cannot be used in a remote service binding because it cannot own certificates. This is a special user for backward compatibility with implicitly connected user schemas. |
| 28090 | 16 | No | An error occurred while deleting sent messages from the transmission queue, Error: %i, State: %i. Verify that no other operation is locking the transmission queue, and that the database is available. |
| 28091 | 10 | No | Starting endpoint for %S_MSG with no authentication is not supported. |
| 28092 | 10 | No | Error updating proxy settings |
| 28098 | 10 | No | A previously existing connection with the same peer was found after DNS lookup. This connection will be closed. All traffic will be redirected to the previously existing connection. This is an informational message only. No user action is required. State %d. |
| 28099 | 10 | No | During the database upgrade process on database '%.\*ls', a user object '%S_MSG' named '%.\*ls' was found to already exist. That object is now reserved by the system in this version of SQL Server. Because it already exists in the database, the upgrade process is unable to install the object. Remove or rename the user object from the original (pre-upgraded) database on an older version of SQL Server and then retry the database upgrade process by using CREATE DATABASE FOR ATTACH. Functionality that relies on the reserved object may not function correctly if you continue to use the database in the current state. |
| 28101 | 16 | No | User '%.\*ls\%.\*ls' does not have permission to debug the requested client connection. |
| 28102 | 16 | No | Batch execution is terminated because of debugger request. |
| 28401 | 16 | No | Feature or option '%ls' is not available. Please consult Books Online for more information on this feature or option. |
| 28405 | 17 | Yes | During error handling, a second error was raised, causing an unrecoverable failure. |
| 28502 | 11 | No | The specified object Id is not valid. |
| 28503 | 11 | No | The specified index Id is not valid. |
| 28504 | 11 | No | The specified partition number is not valid. |
| 28506 | 11 | No | Incorrect number of keys was specified. Must specify at least first %d key columns |
| 28554 | 16 | Yes | Data Virtualization Manager not found. |
| 28555 | 10 | Yes | DVM failed to handle matrix reconfiguration. {error_code: %d, state: %d} |
| 28601 | 16 | No | The Transaction Coordination Manager has encountered error %d, state %d, severity %d and is shutting down. This is a serious error condition, which may prevent further transaction processing. Examine previously logged messages to figure out the cause for this unexpected failure. Depending on the failure condition, the Transaction Coordination Manager might be restarted automatically. |
| 28602 | 16 | No | The transaction is aborted because the transaction agent is shutting down. |
| 28603 | 10 | Yes | Transaction agent is shutting down because shutdown request was received from the manager. |
| 28604 | 10 | Yes | Transaction Coordination Manager is starting up. This is an informational message only. No user action is required. |
| 28605 | 10 | Yes | Transaction Coordination Manager is inactive. This is an informational message only. No user action is required. |
| 28606 | 10 | Yes | Transaction Coordination Manager is active with %016I64x as the starting AGE. This is an informational message only. No user action is required. |
| 28607 | 16 | No | TCM %.\*ls has received an out of order message from brick id %d and has to be stopped. Based on the state of the system it may restart and re-synchronize automatically. Typical causes for missed messages are temporary network errors or out of memory conditions. Look for earlier messages in the log which should contain additional information. |
| 28608 | 16 | No | Federated transaction could not be started at this time because the TCM Agent is not in a state, which allows new transactions to begin. The TCM Agent is currently in state: %d. The most common reason for this is that the system is being shutdown. Previous messages in the log should contain additional information. |
| 28609 | 16 | No | Matrix transaction operation is not allowed because the transaction doesn't allow forward progress, the transaction will be rolled back. |
| 28610 | 16 | No | Matrix transaction couldn't commit because the task is aborted. The task is aborted either because of cancel instruction from client application, or because one or more transaction paritipant went offline. |
| 28612 | 20 | No | An error occurred while accessing Transaction Coordination Manager metadata. Could not persist AGE %016I64x. |
| 28613 | 10 | Yes | Transaction Coordination Agent is active. This is an informational message only. No user action is required. |
| 28614 | 16 | No | TCM manager has received a request for re-synchronization from brick id %d and will now restart. Typical causes where restart is necessary are temporary network errors or out of memory conditions. Look for earlier messages in the log on the offending brick for more information. |
| 28615 | 20 | No | The request has performed an operation that is disallowed on a remote branch of a transaction |
| 28701 | 16 | No | Request aborted due to communication errors: an attempt was made to use channel %d.%d.%I64u in the closed state. |
| 28702 | 16 | No | Request aborted due to communication errors: incoming message format violation. |
| 28703 | 16 | No | Request aborted due to communication errors: no enabled channel maps for component %d found. |
| 28704 | 16 | No | Request aborted due to communication errors: unable to allocate new message for the brick %d. Error code %d. |
| 28705 | 16 | No | Request aborted due to communication errors: unable to complete send operation for the pipeline %hs. Error code %d. |
| 28706 | 10 | No | Next message reported have originated on brick %d. |
| 28707 | 20 | No | Brick was unable to communicate with configuration manager and will be disconnected. See previous errors for the details. |
| 28708 | 16 | No | Failure encountered due to Matrix configuration change: a brick was evicted. |
| 28709 | 16 | No | Dispatcher was unable to create new thread. |
| 28710 | 16 | No | Request aborted due to communication errors: pipeline %hs inconsistency detected: send side reported %I64d bytes sent but there were no messages received on other side. |
| 28711 | 16 | No | Request aborted due to communication errors: pipeline %hs inconsistency detected: send side reported %I64d bytes sent at age %d (current age and bytes sent are %I64d and %I64d) but receive side has processed only %I64d at age %d. |
| 28712 | 16 | No | Unable to start expiration manager dispatch thread. Result code %d. |
| 28713 | 16 | No | Communication subsystem failed to process user abort request. Error Number: %d; State: %d. |
| 28714 | 16 | No | Request aborted due to communication errors: an attempt was made to communicate with Brick %d which is no longer online or have been reconfigured. |
| 28715 | 10 | No | Execution of the request was aborted on remote brick. |
| 28716 | 16 | No | Request aborted due to communication errors: too many concurrent operations. |
| 28717 | 16 | No | Request aborted due to communication errors: communication subsystem is shutting down or initialization not complete yet. |
| 28718 | 16 | No | Request aborted due to communication errors: pipeline %hs received message (classId %d, sequenceId %d) from the expired age %d (last acceptable age %d). |
| 28719 | 16 | No | Request aborted due to Fabric errors: unable to enqueue a task for workspace 0x%I64x. |
| 28720 | 16 | No | Mci subsystem failed to start with error %d. |
| 28721 | 16 | No | Mci subsystem failed to start channel %d with error %d |
| 28722 | 20 | No | Force close service was unable to communicate with configuration manager (brick %d) either due errors or timeout (%d milliseconds). Matrix is not functioning properly. Investigation is prudent. |
| 28723 | 16 | No | Brick was shutdown by administrator request |
| 28724 | 16 | No | Channel close verification |
| 28725 | 20 | No | Unable to process cancellation request due to resource availability. Retrying... |
| 28726 | 20 | No | Unable to process activation data for a workspace. |
| 28727 | 20 | No | Unable to process incoming message due to resource availability. |
| 28728 | 16 | No | Request aborted due to communication errors: no channel maps of version %d for component %d found to create channel %I64u. |
| 28729 | 16 | No | Request aborted due to communication errors: a message have arrived for non-activated channel %I64u. |
| 28903 | 16 | Yes | Configuration manager agent enlistment is retrying enlistment since brick \<%d\> eviction is pending. |
| 28904 | 16 | Yes | Configuration manager agent enlistment is sending join request message to manager. |
| 28905 | 16 | Yes | Configuration manager agent enlistment failed to receive reply from manager. See previous errors in error log. |
| 28906 | 16 | Yes | Configuration manager agent enlistment completed with '%s'. |
| 28907 | 16 | Yes | Configuration manager agent enlistment stopped. |
| 28908 | 16 | Yes | Configuration manager agent enlistment failed due to different enlistment protocol versions between manager and agent. Version received from manager: \<%d\>, Version expected: \<%d\>. |
| 28909 | 16 | Yes | Configuration manager agent enlistment failed due to receiving reply from invalid manager brick id \<%d\> . |
| 28911 | 10 | Yes | Configuration manager agent enlistment succeeded. Manager accepted brick \<%d\> join request. |
| 28912 | 16 | Yes | Brick \<%d\> enlistment with configuration manager failed due to '%s' mismatch. Brick will be taken down. |
| 28913 | 16 | Yes | Configuration manager agent enlistment encountered error %d, state %d, severity %d and is shutting down. This is a serious error condition, that prevents the brick from joining the matrix. Brick will be taken down. |
| 28914 | 10 | Yes | Configuration manager agent enlistment: Brick \<%d\>, state \<%d\>. |
| 28915 | 16 | Yes | Configuration manager agent encountered error %d, state %d, severity %d while updating brick \<%lu\> incarnation to %I64u. Examine previous logged messages to determine cause of this error. |
| 28916 | 16 | Yes | Configuration manager agent encountered error %d, state %d, severity %d while updating state information. Examine previous logged messages to determine cause of this error. |
| 28917 | 10 | Yes | Configuration manager agent enlistment received join reply message. |
| 28919 | 16 | Yes | Duplicate brick id \<%d\> detected in configuration file. |
| 28920 | 16 | Yes | Configuration manager agent cannot send reported error to configuration manager due to error %d, state %d, severity %d. |
| 28921 | 16 | Yes | No configuration file specified. |
| 28922 | 16 | Yes | Configuration manager agent thread encountered fatal exception (error: %d severity: %d state: %d). This is a serious error and brick will be taken down. Examine previously logged messages to figure out the cause for this fatal failure. |
| 28923 | 16 | Yes | Configuration manager agent initialization failed. See previous errors in error log. This is a serious error condition and brick will be taken down. |
| 28924 | 16 | Yes | Configuration manager agent activation failed. |
| 28929 | 16 | Yes | Configuration manager agent cannot commit new roster. |
| 28931 | 16 | Yes | Configuration manager agent roster is corrupt. |
| 28932 | 16 | Yes | Configuration manager agent did not receive a shutdown channel map. |
| 28933 | 16 | Yes | Configuration manager agent could not perform brick down notification. |
| 28934 | 16 | Yes | Configuration manager agent could not perform brick up notification. |
| 28935 | 16 | Yes | Configuration manager agent is trying to start non-existing agent %d. |
| 28936 | 16 | Yes | Configuration manager agent start failed (Loc: %d). |
| 28937 | 16 | Yes | Configuration manager agent stop failed. |
| 28938 | 10 | Yes | Configuration manager agent has started. |
| 28939 | 10 | Yes | Configuration manager agent has shutdown. |
| 28940 | 16 | Yes | Configuration manager agent could not delete stale MCI sessions. |
| 28942 | 16 | Yes | Error reading configuration file. (Reason=%d). |
| 28943 | 16 | Yes | Error reading configuration file. Column %s not defined. |
| 28944 | 16 | Yes | Error reading configuration file. Host name or instance name cannot be empty (Reason: %d). |
| 28945 | 16 | Yes | Error reading configuration file. Invalid contents. (Reason=%d). |
| 28946 | 16 | Yes | Error reading matrix.xsd schema configuration file. (Reason=%d). |
| 28947 | 16 | No | Error reading configuration file. (line:%d, column:%d, Reason=%s). |
| 28948 | 16 | Yes | Error reading configuration file. Invalid placement brick id. |
| 28949 | 16 | Yes | Error during configuration check (Function:%s, result: 0x%08lx). |
| 28950 | 16 | Yes | Configuration manager agent cannot store metadata. |
| 28951 | 16 | Yes | Cannot update roster, brick id %d not part of roster. |
| 28952 | 16 | Yes | Configuration manager cannot initialize agent %d. |
| 28953 | 16 | Yes | Matrix agent %d has the invalid state %d. |
| 28955 | 16 | Yes | Cannot create roster memento. |
| 28958 | 16 | Yes | Error copying configuration. (Reason=%s). |
| 28959 | 16 | Yes | Cannot commit metadata change for brick id %d. |
| 28960 | 16 | Yes | Error: Cannot read \<%s\> node of configuration file. |
| 28961 | 16 | Yes | Error: invalid configuration file, no bricks defined. |
| 28962 | 16 | Yes | Error: Invalid value for manager name %s specified. |
| 28963 | 16 | Yes | Error: Invalid value for manager role %s specified. |
| 28964 | 16 | Yes | Error: Cannot create dom object for writing of XML configuration file (Reason %d). |
| 28968 | 10 | Yes | Starting communication infrastructure for communication between configuration manager and agent |
| 28969 | 10 | Yes | Configuration manager agent shutting down... |
| 28970 | 16 | Yes | Error: cannot update matrix metadata (Loc: %d). |
| 28972 | 16 | Yes | Error: cannot determine host name. |
| 28973 | 16 | Yes | Configuration manager agent cannot create notification publisher. |
| 28974 | 16 | Yes | Configuration manager agent cannot create notification service. |
| 28975 | 16 | Yes | Brick not configured correctly, corrupt configuration file. |
| 28976 | 16 | Yes | Configuration manager agent is ignoring stale message from manager brick %d due to '%s' mismatch. Expected %I64u but received %I64u |
| 28977 | 16 | Yes | Configuration manager agent is performing '%s' '%s' operation. |
| 28978 | 16 | Yes | Configuration manager agent cannot '%s' '%s'. Examine previous logged messages to determine cause of this error. This is a fatal error affecting system functionality and brick will be taken down. |
| 28979 | 16 | Yes | Configuration manager agent cannot '%s' '%s' due to invalid state '%s'. This is a fatal error affecting system functionality and brick will be taken down. |
| 28980 | 16 | Yes | Error writing brick simulator state file. (Reason=%s). |
| 29001 | 16 | No | To connect to this server you must use SQL Server Management Studio or SQL Server Management Objects (SMO). |
| 29003 | 16 | No | Invalid parameter combinations. |
| 29004 | 16 | No | Unknown property specified: %s. |
| 29103 | 16 | Yes | TCMA is shutting down since it encountered fatal error %d, state %d, severity %d |
| 29104 | 16 | Yes | Failure in channel announce |
| 29105 | 16 | Yes | Failure in channel enable |
| 29201 | 17 | Yes | Configuration manager enlistment can not initialize communication service to receive enlistment requests |
| 29202 | 16 | Yes | Configuration manager enlistment is ignoring message from brick \<%d\>. Enlistment protocol version mismatch. Agent version \<%X\>, manager version \<%X\>. |
| 29203 | 16 | Yes | Configuration manager enlistment denied join of brick \<%d\>. |
| 29204 | 16 | Yes | Configuration manager enlistment denied join of brick \<%d\>. Server default collation mismatch. Agent collation \<%lu\>, manager collation \<%lu\>. |
| 29205 | 16 | Yes | Configuration manager enlistment denied join of brick \<%d\>. SQL version or build number mismatch. Agent version \<%X\>, manager version \<%X\>. |
| 29206 | 16 | Yes | Configuration manager enlistment is ignoring messages from brick \<%d\> since message queue does not exist. |
| 29207 | 16 | Yes | Configuration manager enlistment is ignoring stale message from brick %d due to '%s' mismatch. Expected %I64u but received %I64u |
| 29208 | 16 | Yes | Cannot commit metadata change '%s' for brick id %d. |
| 29209 | 16 | Yes | Configuration manager enlistment encountered error %d, state %d, severity %d and is shutting down. This is a serious error condition, which prevents further enlistment and bricks joining matrix. Brick will be restarted. |
| 29210 | 16 | Yes | Configuration manager enlistment encountered error %d, state %d, severity %d while sending enlistment reply to brick \<%lu\>. Enlistment reply not sent! Examine previous logged messages to determine cause of this error. |
| 29211 | 16 | Yes | Configuration manager enlistment is ignoring request from invalid brick id \<%d\>. |
| 29212 | 16 | Yes | Configuration manager enlistment encountered error %d, state %d, severity %d while updating brick \<%lu\> incarnation to %I64u. Enlistment request from brick is not processed. Examine previous logged messages to determine cause of this error. |
| 29215 | 10 | Yes | Configuration manager enlistment acknowledges brick join request. Brick \<%d\> joined matrix. |
| 29217 | 10 | Yes | Configuration manager enlistment received a new enlistment message. |
| 29218 | 10 | Yes | Configuration manager enlistment state machine: Brick \<%d\>, state \<%d\> |
| 29219 | 10 | Yes | Reserved brick \<%d\> is now marked active |
| 29220 | 10 | Yes | Configuration manager enlistment started. |
| 29221 | 10 | Yes | Configuration manager enlistment stopped. |
| 29222 | 16 | Yes | Configuration manager enlistment did not receive enlistment messages |
| 29223 | 17 | No | Configuration manager enlistment ran out of memory. Enlistment messages ignored. |
| 29224 | 10 | Yes | Configuration manager enlistment sent reply to brick \<%d\>. |
| 29225 | 10 | Yes | Configuration manager enlistment received join message from brick \<%d\> |
| 29227 | 17 | Yes | Thread creation failed. (RC=%d). |
| 29228 | 10 | Yes | Matrix transitioned from state %d to state %d. |
| 29229 | 10 | Yes | Brick transitioned from state %d to state %d. |
| 29230 | 10 | Yes | Manager transitioned from state %d to state %d. |
| 29231 | 10 | Yes | Matrix transitioned from state %d to state %d. |
| 29232 | 16 | Yes | Cannot create configuration manager enlistment thread. |
| 29233 | 16 | Yes | Configuration manager thread encountered fatal exception (error: %d state: %d severity: %d). This is a serious error and brick will be taken down. Examine previously logged messages to figure out the cause for this fatal failure. |
| 29234 | 16 | Yes | Configuration manager initialization failed. (Reason=%s). |
| 29235 | 16 | Yes | Configuration manager cannot create channel distribution service. |
| 29236 | 16 | Yes | Configuration manager cannot create force close service. |
| 29237 | 16 | Yes | Configuration manager cannot create expiration service. |
| 29239 | 16 | Yes | Configuration manager reconfiguration failed. |
| 29241 | 16 | Yes | Configuration manager initialization failed. |
| 29242 | 16 | Yes | Configuration manager activation failed. |
| 29243 | 16 | Yes | Configuration manager quiesce operation failed. |
| 29244 | 16 | Yes | Configuration manager release operation failed. |
| 29245 | 16 | Yes | Configuration manager cannot quiesce the agents. |
| 29246 | 16 | Yes | Configuration manager reconfiguration failed due to error %d, state %d, severity %d. |
| 29247 | 16 | Yes | Configuration manager error handler initialization failed due to error %d, state %d, severity %d. This is a serious error and brick will be taken down. Examine previously logged messages to figure out the cause for this fatal failure. |
| 29249 | 16 | Yes | Configuration manager broadcast channel shutdown crashed. |
| 29250 | 16 | Yes | Configuration manager cannot perform action %d on other bricks in the matrix due to error %d, state %d, severity %d. |
| 29251 | 16 | Yes | Configuration manager cannot create notification. |
| 29252 | 16 | Yes | Configuration manager cannot create brick down event for unregistered brick \<%d\> |
| 29253 | 16 | Yes | Configuration manager cannot perform channel distribution (Reason: %d, Loc: %d) |
| 29254 | 16 | Yes | Configuration manager cannot activate the agents. |
| 29255 | 16 | Yes | Configuration manager cannot send reconfig event. |
| 29256 | 16 | Yes | Configuration manager cannot retire old channel maps (Loc: %d). |
| 29257 | 16 | Yes | Configuration manager cannot shutdown old channel maps (Loc: %d). |
| 29258 | 16 | Yes | Configuration manager cannot cannot deliver shutdown ack to leaving bricks. |
| 29260 | 16 | Yes | Configuration manager cannot set matrix state. |
| 29261 | 16 | Yes | Configuration manager detected an invalid matrix state. |
| 29262 | 16 | Yes | Configuration manager can only evict bricks from the manager brick. |
| 29263 | 16 | Yes | Configuration manager can only force close bricks from the manager brick. |
| 29264 | 16 | Yes | Configuration manager brick down notification failed. |
| 29265 | 16 | Yes | Configuration manager reconfig announce step failed. |
| 29266 | 16 | Yes | Configuration manager reconfig enable step failed. |
| 29267 | 16 | Yes | Configuration manager performed reconfig without membership change. |
| 29268 | 16 | Yes | Configuration manager reconfig retire step failed. |
| 29269 | 16 | Yes | Configuration manager cannot start non-registered manager %d. |
| 29270 | 10 | Yes | Configuration manager has started accepting enlistment requests |
| 29271 | 10 | Yes | Configuration manager has shutdown. |
| 29272 | 16 | Yes | Configuration manager enlistment denied join of brick \<%d\>. Machine architecture mismatch. Agent machine architecture \<%X\>, manager machine architecture \<%X\>. |
| 29273 | 16 | Yes | Error: cannot bump Configuration manager major version (Loc: %d). |
| 29274 | 16 | Yes | Component %s reported that the component %s in brick %d is in a suspicious state because of the error: %d, severity: %d, state: %d, description: '%s'. Additional description provided was: '%s'. This report will be sent to the configuration manager known to be on brick %d. |
| 29275 | 16 | Yes | Component %s from brick %d reported that the component %s in brick %d is in a suspicious state because of the error: %d, severity: %d, state: %d, description: '%s'. Additional description provided was: '%s'. |
| 29276 | 16 | Yes | Brick down message for brick %d enqueued due to fatal error reported for component %s by component %s in brick %d. Fatal error reported is error code: %d, severity: %d, state: %d, description: '%s'. Additional description provided was: '%s'. |
| 29277 | 16 | Yes | Configuration manager enlistment is queuing a failure request for brick \<%d\> due to receiving new enlistment request from this brick. |
| 29278 | 16 | No | Configuration manager is evicting brick \<%d\> from which it did not receive expected reply during communication. This also includes brick not responding within timeout. |
| 29279 | 16 | Yes | Configuration manager could not read metadata necessary for its operation from the master database. |
| 29280 | 16 | Yes | Configuration manager is performing '%s' '%s' operation on all online bricks. |
| 29281 | 16 | Yes | Configuration manager is performing '%s' '%s' operation on brick \<%lu\>. |
| 29282 | 16 | Yes | Configuration manager cannot '%s' '%s'. Examine previous logged messages to determine cause of this error. This is a fatal error affecting system functionality and brick \<%lu\> will be evicted. |
| 29283 | 16 | Yes | Configuration manager cannot initialize communication object. Examine previous logged messages to determine cause of this error. |
| 29284 | 16 | Yes | Configuration manager could not write the WMI offline configuration file. (Reason=%s). |
| 29285 | 16 | Yes | Configuration manager enlistment denied join of brick \<%lu\>. Supported number of bricks mismatch. Agent supported number of bricks \<%lu\>, manager supported number of bricks \<%lu\>. |
| 29286 | 10 | Yes | Brick \<%lu\> will be allowed to join matrix once its previous brick down reconfiguration is completed. |
| 29300 | 16 | Yes | The configuration file name %ls is invalid. |
| 29301 | 16 | Yes | Failed to open configuration file %ls. |
| 29302 | 16 | Yes | Configuration file %ls is empty. |
| 29303 | 16 | Yes | The configuration file %ls is corrupt. |
| 29304 | 16 | Yes | Cannot create brick configuration. |
| 29305 | 16 | Yes | The configuration property %s is corrupt. |
| 29306 | 16 | Yes | The configuration file name cannot be constructed. (Reason: %d). |
| 29307 | 16 | Yes | Cannot set the roster property %s. |
| 29308 | 16 | Yes | Local brick id \<%d\> does not match brick id \<%d\> in the resource database. |
| 29309 | 16 | Yes | Brick id has not been set. (Resource instance name: %s) |
| 29311 | 16 | Yes | An error occurred while trying to connect to a service control manager on machine %ls. Error returned: %d - %ls. |
| 29312 | 16 | Yes | An error occurred while trying to perform a service operation on service %ls on machine %ls. Error returned: %d - %ls. |
| 29314 | 16 | Yes | The service %ls on machine %ls failed to start in a timely fashion. Service specific error code %d. |
| 29315 | 16 | Yes | OS user does not have the required privileges to start the remote service on brick %d. |
| 29316 | 16 | Yes | The path name for the file matrix.xsd cannot be constructed. (Reason: %d). |
| 29317 | 16 | Yes | Communication involving configuration manager and agent is ignoring stale message from brick \<%lu\> to brick \<%lu\> due to '%s' mismatch. Expected %I64u but received %I64u. |
| 29318 | 16 | Yes | Communication involving configuration manager and agent is unable to send message from brick \<%lu\> to brick \<%lu\> due to error %d, state %d, severity %d. |
| 29319 | 16 | Yes | Communication involving configuration manager and agent did not receive a valid reply from brick \<%lu\> |
| 29320 | 16 | Yes | Communication involving configuration manager and agent cannot be initialized due to error %d, state %d, severity %d. |
| 29321 | 16 | Yes | Communication involving configuration manager and agent received a failure response \<0x%lx\> from brick \<%lu\>. |
| 29400 | 16 | Yes | Synchronous write task failed due to inconsistent prism state. |
| 29401 | 16 | Yes | Prism operation failed due to error code %d. |
| 29402 | 16 | Yes | Synchronous write task was aborted. |
| 29501 | 16 | Yes | Data Virtualization Manager Agent not found. |
| 29601 | 16 | Yes | SM force closing channel to recover after errors |
| 30003 | 16 | No | A fulltext system view or stvf cannot open database id %d. |
| 30004 | 16 | No | A fulltext system view or stvf cannot open user table object id %d. |
| 30005 | 16 | No | The name specified for full-text index fragment %.\*ls is not valid. |
| 30006 | 16 | No | A fulltext system view or stvf cannot open fulltext index for user table object id %d. |
| 30007 | 16 | No | Parameters of dm_fts_index_keywords, dm_fts_index_keywords_by_document, dm_fts_index_keywords_by_property, and dm_fts_index_keywords_position_by_document cannot be null. |
| 30008 | 16 | No | This is an internal error when invoking the TVF to access the full-text index. The level number specified for the TVF is not valid. Valid level numbers start from 0 and must be less than the number of levels of the compressed index. |
| 30009 | 16 | No | The argument data type '%ls' specified for the full-text query is not valid. Allowed data types are char, varchar, nchar, nvarchar. |
| 30020 | 16 | No | The full-text query parameter for %S_MSG is not valid. |
| 30022 | 10 | No | Warning: The configuration of a full-text stoplist was modified using the WITH NO POPULATION clause. This put the full-text index into an inconsistent state. To bring the full-text index into a consistent state, start a full population. The basic Transact-SQL syntax for this is: ALTER FULLTEXT INDEX ON table_name START FULL POPULATION. |
| 30023 | 16 | No | The fulltext stoplist '%.\*ls' does not exist or the current user does not have permission to perform this action. Verify that the correct stoplist name is specified and that the user had the permission required by the Transact-SQL statement. |
| 30024 | 16 | No | The fulltext stoplist '%.\*ls' already exists in the current database. Duplicate stoplist names are not allowed. Rerun the statement and specify a unique stoplist name. |
| 30025 | 16 | No | The search property list '%.\*ls' does not exist or you do not have permission to perform this action. Verify that the correct search property list name is specified and that you have the permission required by the Transact-SQL statement. For a list of the search property lists on the current database, use the sys.registered_search_property_lists catalog view. For information about permissions required by a Transact-SQL statement, see the Transact-SQL reference topic for the statement in SQL Server Books Online. |
| 30026 | 16 | No | The search property list '%.\*ls' already exists in the current database. Duplicate search property list names are not allowed. Rerun the statement and specify a unique name for the search property list. For a list of the search property lists on the current database, use the sys.registered_search_property_lists catalog view. |
| 30027 | 10 | No | The full-text index is in an inconsistent state because the search property list of the full-text index was reconfigured using the WITH NO POPULATION clause. To bring the full-text index into a consistent state, start a full population using the statement ALTER FULLTEXT INDEX ON \<table_name\> START FULL POPULATION;. This is a warning. No user action is necessary. |
| 30028 | 17 | No | Failed to get pipeline interface for '%ls', resulting in error: 0x%X. There is a problem communicating with the host controller or filter daemon host. |
| 30029 | 17 | No | The full-text host controller failed to start. Error: 0x%X. |
| 30030 | 16 | No | The search property '%.\*ls' does not exist, or you do not have permission to perform this action. Verify that the correct search property is specified and that you have the permission required by the Transact-SQL statement. For a list of the search properties on the current database, use the sys.registered_search_properties catalog view. For information about the permissions required by a Transact-SQL statement, see the Transact-SQL reference topic for the statement in SQL Server Books Online. |
| 30031 | 17 | No | A full-text master merge failed on full-text catalog '%ls' in database '%.\*ls' with error 0x%08X. |
| 30032 | 16 | No | The stoplist '%.\*ls' does not contain fulltext stopword '%.\*ls' with locale ID %d. Specify a valid stopword and locale identifier (LCID) in the Transact-SQL statement. |
| 30033 | 16 | No | The stoplist '%.\*ls' already contains full-text stopword '%.\*ls' with locale ID %d. Specify a unique stopword and locale identifier (LCID) in the Transact-SQL statement. |
| 30034 | 16 | No | Full-text stoplist '%.\*ls' cannot be dropped because it is being used by at least one full-text index. To identify which full-text index is using a stoplist: obtain the stoplist ID from the stoplist_id column of the sys.fulltext_indexes catalog view, and then look up that stoplist ID in the stoplist_id column of the sys.fulltext_stoplists catalog view. Either drop the full-text index by using DROP FULLTEXT INDEX or change its stoplist setting by using ALTER FULLTEXT INDEX. Then retry dropping the stoplist. |
| 30035 | 16 | No | The search property '%.\*ls' already exists in the search property list. Specify a search property name that is unique within the specified search property list. For a list of the search properties on the current database, use the sys.registered_search_properties catalog view. |
| 30036 | 16 | No | Search property list '%.\*ls' cannot be dropped because it is being used by at least one full-text index. To identify the full-text indexes that are using the search property list, obtain the search property list id from the property_list_id column of the sys.registered_search_property_lists catalog view, and then obtain the object ID of every table or indexed view whose full-text index is associated with this search property list from the object_id and property_list_id columns of the sys.fulltext_indexes catalog view . For each full-text index, either remove the search property list or drop the full-text index, if it is no longer needed. To remove the search property list, use ALTER FULLTEXT INDEX ON \<table_name\> SET SEARCH PROPERTY LIST OFF;. To drop a full-text index, use DROP FULLTEXT INDEX ON \<table_name\>;. |
| 30037 | 16 | No | An argument passed to a fulltext function is not valid. |
| 30038 | 17 | No | Fulltext index error during compression or decompression. Full-text index may be corrupted on disk. Run dbcc checkdatabase and re-populate the index. |
| 30039 | 17 | No | Data coming back to the SQL Server process from the filter daemon host is corrupted. This may be caused by a bad filter. The batch for the indexing operation will automatically be retried using a smaller batch size. |
| 30040 | 10 | No | During a full-text crawl of table or indexed view '%ls', an unregistered property, '%ls', was found in batch ID %d. This property will be indexed as part of the generic content and will be unavailable for property-scoped full-text queries. Table or indexed view ID is '%d'. Database ID is '%d'. For information about registering properties and updating the full-text index of a table or indexed view, see the full-text search documentation in SQL Server Books Online. This is an informational message. No user action is necessary. |
| 30041 | 10 | No | The master merge started at the end of the full crawl of table or indexed view '%ls' failed with HRESULT = '0x%08x'. Database ID is '%d', table id is %d, catalog ID: %d. |
| 30043 | 16 | No | Stopwords of zero length cannot be added to a full-text stoplist. Specify a unique stopword that contains at least one character. |
| 30044 | 16 | No | The user does not have permission to alter the current default stoplist '%.\*ls'. To change the default stoplist of the database, ALTER permission is required on both new and old default stoplists. |
| 30045 | 17 | No | Fulltext index error during compression or decompression. Full-text index may be corrupted on disk. Run dbcc checkdatabase and re-populate the index. |
| 30046 | 16 | No | SQL Server encountered error 0x%x while communicating with full-text filter daemon host (FDHost) process. Make sure that the FDHost process is running. To re-start the FDHost process, run the sp_fulltext_service 'restart_all_fdhosts' command or restart the SQL Server instance. |
| 30047 | 16 | No | The user does not have permission to %.\*ls stoplist '%.\*ls'. |
| 30048 | 10 | No | Informational: Ignoring duplicate thesaurus rule '%ls' while loading thesaurus file for LCID %d. A duplicate thesaurus phrase was encountered in either the \<sub\> section of an expansion rule or the \<pat\> section of a replacement rule. This causes an ambiguity and hence this phrase will be ignored. |
| 30049 | 17 | No | Fulltext thesaurus internal error (HRESULT = '0x%08x') |
| 30050 | 16 | No | Both the thesaurus file for lcid '%d' and the global thesaurus could not be loaded. |
| 30051 | 16 | No | Phrases longer than 512 unicode characters are not allowed in a thesaurus file. Phrase: '%ls'. |
| 30052 | 16 | No | The full-text query has a very complex NEAR clause in the CONTAINS predicate or CONTAINSTABLE function. To ensure that a NEAR clause runs successfully, use only six or fewer terms. Modify the query to simplify the condition by removing prefixes or repeated terms. |
| [30053](../mssqlserver-30053-database-engine-error.md) | 16 | No | An error has occurred during the full-text query. Common causes include: word-breaking errors or timeout, FDHOST permissions/ACL issues, service account missing privileges, malfunctioning IFilters, communication channel issues with FDHost and sqlservr.exe, etc. |
| 30055 | 10 | No | Full-text catalog import has started for full-text catalog '%ls' in database '%ls'. |
| 30056 | 10 | No | Full-text catalog import has finished for full-text catalog '%ls' in database '%ls'. %d fragments and %d keywords were processed. |
| 30057 | 10 | No | Upgrade option '%ls' is being used for full-text catalog '%ls' in database '%ls'. |
| 30058 | 16 | No | Properties of zero length cannot be added to a search property list. Specify a search property name that contains at least one character and that is unique to the specified search property list. For a list of the search properties on the current database, use the sys.registered_search_properties catalog view. |
| 30059 | 16 | No | A fatal error occurred during a full-text population and caused the population to be cancelled. Population type is: %s; database name is %s (id: %d); catalog name is %s (id: %d); table name %s (id: %d). Fix the errors that are logged in the full-text crawl log. Then, resume the population. The basic Transact-SQL syntax for this is: ALTER FULLTEXT INDEX ON table_name RESUME POPULATION. |
| 30060 | 16 | No | The import population for database %ls (id: %d), catalog %ls (id: %d) is being cancelled because of a fatal error ('%ls'). Fix the errors that are logged in the full-text crawl log. Then resume the import either by detaching the database and re-attaching it, or by taking the database offline and bringing it back online. If the error is not recoverable, rebuild the full-text catalog. |
| 30061 | 17 | No | The SQL Server failed to create full-text filterdata directory. This might be because FulltextDefaultPath is invalid or SQL Server service account does not have permission. Full-text blob indexing will fail until this issue is resolved. Restart SQL Server after the issue is fixed. |
| 30062 | 17 | No | The SQL Server failed to load FDHost service group sid. This might be because installation is corrupted. |
| 30063 | 10 | No | Warning: SQL Server could not set fdhost.exe processor affinity to %d because the value is not valid. |
| 30064 | 17 | No | SQL Server failed to set security information on the full-text FilterData directory in the FTData folder. Full-text indexing of some types of documents may fail until this issue is resolved. You will need to repair the SQL Server installation. |
| 30065 | 10 | No | Filegroup '%ls' is offline, readonly, or no data file. Full-text population on table '%ls' is not resumed. Resume full-text population after fixing the filegroup status. |
| 30067 | 10 | No | Warning: The detach operation cannot delete a full-text index created on table '%ls' in database '%ls' because the index is on a read-only filegroup. To drop the full-text index, re-attach the database, change the read-only filegroup to read/write access and then detach it. This warning will not fail the database detach operation. |
| 30068 | 10 | No | During the database upgrade, the full-text filter component '%ls' that is used by catalog '%ls' was successfully verified. Component version is '%ls'; Full path is '%.\*ls'. |
| 30069 | 11 | No | The full-text filter component '%ls' used to populate catalog '%ls' in a previous SQL Server release is not the current version (component version is '%ls', full path is '%.\*ls'). This may cause search results to differ slightly from previous releases. To avoid this, rebuild the full-text catalog using the current version of the filter component. |
| 30070 | 10 | No | During the database upgrade, the full-text word-breaker component '%ls' that is used by catalog '%ls' was successfully verified. Component version is '%ls'. Full path is '%.\*ls'. Language requested is %d. Language used is %d. |
| 30071 | 11 | No | The full-text word-breaker component '%ls' used to populate catalog '%ls' in a previous SQL Server release is not the current version (component version is '%ls', full path is '%.\*ls', language requested is %d, language used is %d). This may cause search results to differ slightly from previous releases. To avoid this, rebuild the full-text catalog using the current version of the word-breaker component. |
| 30072 | 10 | No | During the database upgrade, the full-text protocol handler component '%ls' that is used by catalog '%ls' was successfully verified. Component version is '%ls'. Full path is '%.\*ls'. Program ID is '%.\*ls'. |
| 30073 | 11 | No | The full-text protocol handler component '%ls' used to populate catalog '%ls' in a previous SQL Server release is not the current version (component version is '%ls', full path is '%.\*ls', program ID is '%.\*ls'). This may cause search results to differ slightly from previous releases. To avoid this, rebuild the full-text catalog using the current version of the protocol handler component. |
| 30074 | 17 | No | The master merge of full-text catalog '%ls' in database '%.\*ls' was cancelled. |
| 30075 | 10 | No | Full-text crawls for database ID: %d, table ID: %d, catalog ID: %d will be stopped since the clustered index on the table has been altered or dropped. Crawl will need to re-start from the beginning. |
| 30076 | 10 | No | Full-text crawl forward progress information for database ID: %d, table ID: %d, catalog ID: %d has been reset due to the modification of the clustered index. Crawl will re-start from the beginning when it is unpaused. |
| 30077 | 16 | No | The full-text query did not use the value specified for the OPTIMIZE FOR query hint. Only single terms are allowed as values for full-text queries that contain an OPTIMIZE FOR query hint. Modify the OPTIMIZE FOR query hint value to be a single, non-empty term. |
| 30078 | 10 | No | The fulltext query did not use the value specified for the OPTIMIZE FOR hint because the query contained more than one type of full-text logical operator. |
| 30079 | 10 | No | The full text query ignored UNKNOWN in the OPTIMIZE FOR hint. |
| 30080 | 16 | No | The full-text population on table '%ls' cannot be started because the full-text catalog is importing data from existing catalogs. After the import operation finishes, rerun the command. |
| 30081 | 10 | No | A cached plan was compiled using trace flags that are incompatible with the current values. Consider recompiling the query with the new trace flag settings. |
| 30082 | 16 | No | Full-text predicates cannot appear in an aggregate expression. Place the aggregate expression in a subquery. |
| 30083 | 16 | No | Full-text predicates cannot appear in the GROUP BY clause. Place a GROUP BY clause expression in a subquery. |
| 30084 | 16 | No | The full-text index cannot be created because filegroup '%.\*ls' does not exist or the filegroup name is incorrectly specified. Specify a valid filegroup name. |
| 30085 | 16 | No | A stoplist cache cannot be generated while processing a full-text query or performing full-text indexing. There is not enough memory to load the stoplist cache. Rerun the query or indexing command when more resources are available. |
| 30086 | 16 | No | The system ran out of memory while building a full-text index. The batch for the full-text indexing operation will automatically be retried using a smaller batch size. |
| 30087 | 17 | No | Data coming back to the SQL Server process from the filter daemon host is corrupted. This may be caused by a bad filter. The batch for the indexing operation will automatically be retried using a smaller batch size. |
| 30088 | 10 | No | The full-text filter daemon host process has stopped normally. The process will be automatically restarted if necessary. |
| [30089](../mssqlserver-30089-database-engine-error.md) | 17 | No | The fulltext filter daemon host (FDHost) process has stopped abnormally. This can occur if an incorrectly configured or malfunctioning linguistic component, such as a wordbreaker, stemmer or filter has caused an irrecoverable error during full-text indexing or query processing. The process will be restarted automatically. |
| 30090 | 10 | No | A new instance of the full-text filter daemon host process has been successfully started with version = %ls. |
| 30091 | 10 | No | A request to start a full-text index population on table or indexed view '%.\*ls' is ignored because a population is currently paused. Either resume or stop the paused population. To resume it, use the following Transact-SQL statement: ALTER FULLTEXT INDEX ON %.\*ls RESUME POPULATION. To stop it, use the following statement: ALTER FULLTEXT INDEX ON %.\*ls STOP POPULATION. |
| 30092 | 16 | No | Full-text stoplist ID '%d' does not exist. |
| 30093 | 17 | No | The SQL Server word-breaking client failed to initialize. This might be because a filter daemon host process is not in a valid state. This can prevent SQL Server from initializing critical system objects. Full-text queries will fail until this issue is resolved. Try stopping SQL Server and any filter daemon host processes and then restarting the instance of SQL Server. |
| 30094 | 17 | No | The full-text indexing pipeline could not be initialized. This might be because the resources on the system are too low to allocate memory or create tasks. Try restarting the instance of SQL Server. |
| 30095 | 10 | No | The version of the language components used by full-text catalog '%ls' in database '%ls' is different from the version of the language components included this version of SQL Server. The full-text catalog will still be imported as part of database upgrade. To avoid any possible inconsistencies of query results, consider rebuilding the full-text catalog. |
| 30096 | 10 | No | A full-text retry pass of %ls population started for table or indexed view '%ls'. Table or indexed view ID is '%d'. Database ID is '%d'. |
| 30097 | 10 | No | The fulltext catalog upgrade failed because of an inconsistency in metadata between sys.master_files and sys.fulltext_catalogs for the catalog ID %d in database ID %d. Try to reattach this database. If this fails, then the catalog will need to be dropped or recreated before attach. |
| 30098 | 10 | No | An internal query to load data for a crawl on database '%.\*ls' and table '%.\*ls' failed with error code %d. Check the sql error code for more information about the condition causing this failure. The crawl needs to be restarted after this condition is removed. |
| 30099 | 17 | No | Fulltext internal error |
| 30103 | 16 | No | Invalid CM instance name. |
| 30104 | 16 | No | Invalid matrix name. |
| 30105 | 16 | No | Invalid TCP port number: %s. |
| 30106 | 16 | No | Invalid network security level. |
| 30107 | 16 | No | Invalid network isolation level. |
| 30108 | 16 | No | Invalid matrix guid. |
| 30109 | 16 | No | Invalid or duplicated brick ID was used: %s |
| 30110 | 16 | No | Invalid parameter. |
| 30111 | 16 | No | Matrix setup stored procedure '%s' failed with HRESULT 0x%x. |
| 30112 | 16 | No | Insert brick into metadata operation failed during execution of stored procedure '%s'. |
| 30113 | 16 | No | Insert manager into metadata operation failed during execution of stored procedure '%s'. |
| 30114 | 16 | No | Insert parameter into metadata operation failed during execution of stored procedure '%s'. |
| 30115 | 16 | No | No more bricks can be reserved. The maximum amount of %lu bricks is reached. |
| 30118 | 16 | No | Invalid server name provided to configure a matrix brick. |
| 30119 | 16 | No | Invalid CM brick GUID. |
| 30120 | 16 | No | Invalid brick GUID. |
| 30121 | 16 | No | '%s' in only allowed in standalone (non matrix) mode. |
| 30122 | 16 | No | '%s' in only allowed in single user (-m) mode. |
| 30123 | 16 | No | Drop existing matrix configuration failed during execution of stored procedure '%s'. |
| 30124 | 16 | No | An error occurred while updating the CM metadata to remove a brick. |
| 30125 | 16 | No | Configuration manager could not write the WMI offline configuration file during the execution of stored procedure '%s'. |
| 30126 | 16 | No | Could not complete the last operation with the brick_id %u due a metadata failure |
| 30127 | 16 | No | Attempt to cancel the reservation of a brick that is not reserved: %u |
| 30128 | 16 | No | The stored procedure '%s' failed with the error code %d |
| 30129 | 16 | No | '%s' in only allowed in matrix mode. |
| 30130 | 16 | No | The brick with server name '%s' already exists in the configuration. |
