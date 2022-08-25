---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    11000    |    16    |    No    |    Unknown status code for this column.    |
|    11001    |    16    |    No    |    Non-NULL value successfully returned.    |
|    [11001](../../relational-databases/errors-events/mssqlserver-11001-database-engine-error.md)    |        |        |    An error has occurred while establishing a connection to the server. When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: TCP Provider, error: 0 - No such host is known.) (.Net SqlClient Data Provider    |
|    11002    |    16    |    No    |    Deferred accessor validation occurred. Invalid binding for this column.    |
|    11003    |    16    |    No    |    Could not convert the data value due to reasons other than sign mismatch or overflow.    |
|    11004    |    16    |    No    |    Successfully returned a NULL value.    |
|    11005    |    16    |    No    |    Successfully returned a truncated value.    |
|    11006    |    16    |    No    |    Could not convert the data type because of a sign mismatch.    |
|    11007    |    16    |    No    |    Conversion failed because the data value overflowed the data type used by the provider.    |
|    11008    |    16    |    No    |    The provider cannot allocate memory or open another storage object on this column.    |
|    11009    |    16    |    No    |    The provider cannot determine the value for this column.    |
|    11010    |    16    |    No    |    The user did not have permission to write to the column.    |
|    11011    |    16    |    No    |    The data value violated the integrity constraints for the column.    |
|    11012    |    16    |    No    |    The data value violated the schema for the column.    |
|    11013    |    16    |    No    |    The column had a bad status.    |
|    11014    |    16    |    No    |    The column used the default value.    |
|    11015    |    16    |    No    |    The column was skipped when setting data.    |
|    11031    |    16    |    No    |    The row was successfully deleted.    |
|    11032    |    16    |    No    |    The table was in immediate-update mode, and deleting a single row caused more than one row to be deleted in the data source.    |
|    11033    |    16    |    No    |    The row was released even though it had a pending change.    |
|    11034    |    16    |    No    |    Deletion of the row was canceled during notification.    |
|    11036    |    16    |    No    |    The rowset was using optimistic concurrency and the value of a column has been changed after the containing row was last fetched or resynchronized.    |
|    11037    |    16    |    No    |    The row has a pending delete or the deletion had been transmitted to the data source.    |
|    11038    |    16    |    No    |    The row is a pending insert row.    |
|    11039    |    16    |    No    |    DBPROP_CHANGEINSERTEDROWS was VARIANT_FALSE and the insertion for the row has been transmitted to the data source.    |
|    11040    |    16    |    No    |    Deleting the row violated the integrity constraints for the column or table.    |
|    11041    |    16    |    No    |    The row handle was invalid or was a row handle to which the current thread does not have access rights.    |
|    11042    |    16    |    No    |    Deleting the row would exceed the limit for pending changes specified by the rowset property DBPROP_MAXPENDINGROWS.    |
|    11043    |    16    |    No    |    The row has a storage object open.    |
|    11044    |    16    |    No    |    The provider ran out of memory and could not fetch the row.    |
|    11045    |    16    |    No    |    User did not have sufficient permission to delete the row.    |
|    11046    |    16    |    No    |    The table was in immediate-update mode and the row was not deleted due to reaching a limit on the server, such as query execution timing out.    |
|    11047    |    16    |    No    |    Updating did not meet the schema requirements.    |
|    11048    |    16    |    No    |    There was a recoverable, provider-specific error, such as an RPC failure.    |
|    11100    |    16    |    No    |    The provider indicates that conflicts occurred with other properties or requirements.    |
|    11101    |    16    |    No    |    Could not obtain an interface required for text, ntext, or image access.    |
|    11102    |    16    |    No    |    The provider could not support a required row lookup interface.    |
|    11103    |    16    |    No    |    The provider could not support an interface required for the UPDATE/DELETE/INSERT statements.    |
|    11104    |    16    |    No    |    The provider could not support insertion on this table.    |
|    11105    |    16    |    No    |    The provider could not support updates on this table.    |
|    11106    |    16    |    No    |    The provider could not support deletion on this table.    |
|    11107    |    16    |    No    |    The provider could not support a row lookup position.    |
|    11108    |    16    |    No    |    The provider could not support a required property.    |
|    11109    |    16    |    No    |    The provider does not support an index scan on this data source.    |
|    11201    |    16    |    No    |    This message could not be delivered because the FROM service name is missing. The message origin is: '%ls'.    |
|    11202    |    16    |    No    |    This message has been dropped because the FROM service name exceeds the maximum size of %d bytes. Service name: "%.*ls". Message origin: "%ls".    |
|    11203    |    16    |    No    |    This message has been dropped because the FROM broker instance is missing. The message origin is '%ls'.    |
|    11204    |    16    |    No    |    This message has been dropped because the FROM broker instance exceeds the maximum size of %d bytes. Broker instance: "%.*ls". Message origin: "%ls".    |
|    11205    |    16    |    No    |    This message has been dropped because the TO service name is missing. The message origin is "%ls".    |
|    11206    |    16    |    No    |    This message has been dropped because the TO service name exceeds the maximum size of %d bytes. Service name: "%.*ls". Message origin: "%ls".    |
|    11207    |    16    |    No    |    This message has been dropped because the service contract name is missing. The message origin is "%ls".    |
|    11208    |    16    |    No    |    This message has been dropped because the service contract name exceeds the maximum size of %d bytes. Contract name "%.*ls". Message origin: "%ls".    |
|    11209    |    16    |    No    |    This message could not be delivered because the conversation ID could not be associated with an active conversation. The message origin is: '%ls'.    |
|    11210    |    16    |    No    |    This message has been dropped because the TO service could not be found. Service name: "%.*ls". Message origin: "%ls".    |
|    11211    |    16    |    No    |    This message has been dropped because the user does not have permission to access the target database. Database ID: %d. Message origin: &amp;quot;%ls&amp;quot;.    |
|    11212    |    16    |    No    |    This message could not be delivered because the conversation endpoint has already been closed.    |
|    11213    |    16    |    No    |    This message could not be delivered because this is not the first message in the conversation.    |
|    11214    |    16    |    No    |    This message could not be delivered because the '%.*ls' contract could not be found or the service does not accept conversations for the contract.    |
|    11215    |    16    |    No    |    This message could not be delivered because the user with ID %i in database ID %i does not have permission to send to the service. Service name: '%.*ls'.    |
|    11216    |    16    |    No    |    This message could not be delivered because there is already another task processing this message.    |
|    11217    |    16    |    No    |    This message could not be delivered because it is out of sequence with respect to the conversation. Conversation receive sequence number: %I64d, Message sequence number: %I64d.    |
|    11218    |    16    |    No    |    This message could not be delivered because it is a duplicate.    |
|    11219    |    16    |    No    |    This message could not be delivered because the destination queue has been disabled. Queue ID: %d.    |
|    11220    |    16    |    No    |    This message could not be delivered because the TO broker instance is missing.    |
|    11221    |    16    |    No    |    This message could not be delivered because there is an inconsistency in the message header.    |
|    11222    |    16    |    No    |    This message could not be delivered because the TO service name in the message does not match the name in the conversation endpoint. Message TO Service Name: '%.*ls'. Conversation Endpoint TO Service Name: '%.*ls'.    |
|    11223    |    16    |    No    |    This message could not be delivered because the service contract name in the message does not match the name in the conversation endpoint. Message service contract name: '%.*ls'. Conversation endpoint service contract name: '%.*ls'.    |
|    11224    |    16    |    No    |    This message could not be delivered because another instance of this service program has already started conversing with this endpoint.    |
|    11225    |    16    |    No    |    This message could not be delivered because the message type name could not be found. Message type name: '%.*ls'.    |
|    11226    |    16    |    No    |    This message could not be delivered because the message type is not part of the service contract. Message type name: '%.*ls'. Service contract name: '%.*ls'.    |
|    11227    |    16    |    No    |    This message could not be delivered because the initiator service has sent a message with a message type that can only be sent by the target. Message type name: '%.*ls'. Service contract name: '%.*ls'.    |
|    11228    |    16    |    No    |    This message could not be delivered because the target service has sent a message with a message type that can only be sent by the initiator. Message type name: '%.*ls'. Service contract name: '%.*ls'.    |
|    11229    |    16    |    No    |    This message could not be delivered because the security context could not be retrieved.    |
|    11230    |    16    |    No    |    This message could not be delivered because the message could not be decrypted and validated.    |
|    11231    |    16    |    No    |    This message could not be delivered because the conversation endpoint is not secured, however the message is secured.    |
|    11232    |    16    |    No    |    This message could not be delivered because the conversation endpoint is secured, however the message is not secured.    |
|    11233    |    16    |    No    |    This message has been dropped because the session key of the conversation endpoint does not match that of the message.    |
|    11234    |    16    |    No    |    This message could not be delivered because an internal error was encountered while processing it. Error code %d, state %d: %.*ls.    |
|    11235    |    16    |    No    |    Received a malformed message. The binary message class (%d:%d) is not defined. This may indicate network problems or that another application is connected to the Service Broker endpoint.    |
|    11236    |    16    |    No    |    A corrupted message has been received. The binary header size of %d is expected, however the header size received was %d.    |
|    11237    |    16    |    No    |    A %S_MSG message could not be processed due to insufficient memory. The message was dropped.    |
|    11238    |    16    |    No    |    A corrupted message has been received. The private variable data segment is malformed.    |
|    11239    |    16    |    No    |    A corrupted message has been received. The private variable data segment extends beyond the length of the message.    |
|    11240    |    16    |    No    |    A corrupted message has been received. The binary message preamble is malformed.    |
|    11241    |    16    |    No    |    A corrupted message has been received. The conversation security version number is not %d.%d.    |
|    11242    |    16    |    No    |    A corrupted message has been received. The maximum number of public variable data elements (%d) has been exceeded. Public variable data elements found: %d.    |
|    11243    |    16    |    No    |    A corrupted message has been received. The public variable data element (%d) has been duplicated in this message.    |
|    11244    |    16    |    No    |    A corrupted message has been received. The handshake validation header is malformed.    |
|    11245    |    16    |    No    |    A corrupted message has been received. The maximum number of private variable data elements (%d) has been exceeded. Private variable data elements found: %d.    |
|    11246    |    16    |    No    |    A corrupted message has been received. The private variable data element (%d) has been duplicated in this message.    |
|    11247    |    16    |    No    |    A corrupted message has been received. The login negotiate header is invalid.    |
|    11248    |    16    |    No    |    A corrupted message has been received. The SSPI login header is invalid.    |
|    11249    |    16    |    No    |    A corrupted message has been received. The pre-master-secret is invalid.    |
|    11250    |    16    |    No    |    A corrupted message has been received. The security certificate key fields must both be present or both be absent. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11251    |    16    |    No    |    A corrupted message has been received. The service pair security header source certificate and the signature must both be present or both be absent. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11252    |    16    |    No    |    A corrupted message has been received. The destination certificate serial number is missing. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11253    |    16    |    No    |    A corrupted message has been received. The service pair security header destination certificate, the key exchange key, the key exchange key ID, and the session key must all be present or all be absent. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11254    |    16    |    No    |    A corrupted message has been received. The session key ID is missing. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11255    |    16    |    No    |    A corrupted message has been received. The encryption flag is set, however the message body, MIC or salt is missing. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11256    |    16    |    No    |    A corrupted message has been received. The MIC is present, however the message body or encryption flag is missing. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11257    |    16    |    No    |    A corrupted message has been received. The MIC and session key ID are in an invalid state. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11258    |    16    |    No    |    A corrupted message has been received. The MIC size is %d, however it must be no greater than %d bytes in length. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11259    |    16    |    No    |    A corrupted message has been received. The certificate serial number size is %d, however it must be no greater than %d bytes in length. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11260    |    16    |    No    |    A corrupted message has been received. The certificate issuer name size is %d, however it must be no greater than %d bytes in length. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11261    |    16    |    No    |    A corrupted message has been received. The destination certificate serial number size is %d, however it must be no greater than %d bytes in length. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11262    |    16    |    No    |    A corrupted message has been received. The destination certificate issuer name size is %d, however it must be no greater than %d bytes in length. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11263    |    16    |    No    |    A corrupted message has been received. The service pair security header size is %d, however it must be between %d and %d bytes. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11264    |    16    |    No    |    A corrupted message has been received. The key exchange key size is %d, however it must be between %d and %d bytes. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11265    |    16    |    No    |    A corrupted message has been received. The key exchange key ID is invalid. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11266    |    16    |    No    |    A corrupted message has been received. The encrypted session key size is %d, however it must be %d bytes. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11267    |    16    |    No    |    A corrupted message has been received. The session key ID size is %d, however it must be %d bytes. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11268    |    16    |    No    |    A corrupted message has been received. The salt size is %d, however it must be %d bytes. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11269    |    16    |    No    |    A corrupted message has been received. A UNICODE string is not two byte aligned within the message. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11270    |    16    |    No    |    A corrupted message has been received. A UNICODE string is greater than the maximum allowed size of %d bytes. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11271    |    16    |    No    |    A corrupted message has been received. The conversation ID must not be NULL. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11272    |    16    |    No    |    A corrupted message has been received. The message ID must not be NULL.    |
|    11273    |    16    |    No    |    A corrupted message has been received. The message body is not properly padded for encryption. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11274    |    16    |    No    |    A corrupted message has been received. A sequence number is larger than allowed. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11275    |    16    |    No    |    A corrupted message has been received. The End of Conversation and Error flags are both set. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11276    |    16    |    No    |    A corrupted message has been received. The End of Conversation flag has been set on an unsequenced message. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11277    |    16    |    No    |    A corrupted message has been received. The End of Conversation and Error flags may not be set in the first sequenced message. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11278    |    16    |    No    |    A corrupted message has been received. The message type is missing for this message. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11279    |    16    |    No    |    A corrupted message has been received. The message type must not be set in this message. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11280    |    16    |    No    |    A packet of size %lu bytes could not be processed because it exceeds the receive buffer count.    |
|    11281    |    16    |    No    |    A corrupted message has been received. The private portion of the message header is malformed.    |
|    11282    |    16    |    No    |    This message has been dropped due to licensing restrictions. See the documentation for further details.    |
|    11285    |    16    |    No    |    This forwarded message has been dropped because the hops remaining count has reached 0.    |
|    11286    |    16    |    No    |    Dropped this forwarded message because this SQL Server instance is out of memory.    |
|    11288    |    16    |    No    |    This forwarded message has been dropped because a duplicate message is already being forwarded.    |
|    11289    |    16    |    No    |    This forwarded message has been dropped because its memory usage would exceed the configured memory limit of %d bytes for forwarded messages.    |
|    11290    |    16    |    No    |    This forwarded message was dropped because the message could not be delivered within the message time to live. This may indicate that the forwarding route is incorrectly configured or that the destination is unavailable.    |
|    11291    |    16    |    No    |    This forwarded message has been dropped because the time consumed has exceeded the message's time to live of %u seconds (the message arrived with %u seconds consumed and used %u seconds in this broker).    |
|    11292    |    16    |    No    |    The forwarded message has been dropped because a transport send error occurred when sending the message. Check previous events for the error.    |
|    11293    |    16    |    No    |    This forwarded message has been dropped because a transport is shutdown.    |
|    11294    |    16    |    No    |    This forwarded message has been dropped because the destination route is not valid.    |
|    11295    |    10    |    No    |    Endpoint configuration change detected. Service Broker manager and transport will now restart.    |
|    11296    |    10    |    No    |    Certificate change detected. Service Broker manager and transport will now restart.    |
|    11297    |    16    |    No    |    A corrupted message has been received. The private variable data segment offset is incorrect.    |
|    11298    |    16    |    No    |    A corrupted message has been received. The public variable data segment offset is incorrect.    |
|    11299    |    10    |    No    |    A corrupted message has been received. An unsequenced message had a non-zero sequence number. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    11300    |    10    |    Yes    |    Error while committing a readonly or a TEMPDB XDES, Shutting down the server.    |
|    11301    |    10    |    Yes    |    Error while performing transaction notification for object %p event %d.    |
|    11302    |    10    |    Yes    |    Error during rollback. shutting down database (location: %d).    |
|    11303    |    10    |    Yes    |    Error releasing reserved log space: %ls space %I64d, code %d, state %d.    |
|    11304    |    10    |    Yes    |    Failed to record outcome of a local two-phase commit transaction. Taking database offline.    |
|    11400    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Index '%.*ls' on indexed view '%.*ls' uses partition function '%.*ls', but table '%.*ls' uses non-equivalent partition function '%.*ls'. Index on indexed view '%.*ls' and table '%.*ls' must use an equivalent partition function.    |
|    11401    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Table '%.*ls' is %S_MSG, but index '%.*ls' on indexed view '%.*ls' is %S_MSG.    |
|    11402    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' is referenced by %d indexed view(s), but source table '%.*ls' is only referenced by %d indexed view(s). Every indexed view on the target table must have at least one matching indexed view on the source table.    |
|    11403    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Indexed view '%.*ls' is not aligned with table '%.*ls'. The partitioning column '%.*ls' from the indexed view calculates its value from one or more columns or an expression, rather than directly selecting from the table partitioning column '%.*ls'. Change the indexed view definition, so that the partitioning column is directly selected from table partitioning column '%.*ls'.    |
|    11404    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' is referenced by %d indexed view(s), but source table '%.*ls' is only referenced by %d matching indexed view(s). Every indexed view on the target table must have at least one matching indexed view on the source table.    |
|    11405    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Table '%.*ls' is not aligned with the index '%.*ls' on indexed view '%.*ls'. The table is partitioned on column '%.*ls', but the index on the indexed view is partitioned on column '%.*ls', which is selected from a different column '%.*ls' in table '%.*ls'. Change the indexed view definition so that the partitioning column is the same as the table's partitioning column.    |
|    11406    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Source and target partitions have different values for the DATA_COMPRESSION option.    |
|    11407    |    16    |    No    |    Vardecimal storage format can not be enabled for '%.*ls'. Only Enterprise edition of SQL Server supports vardecimal.    |
|    11408    |    16    |    No    |    Cannot modify the column '%.*ls' in the table '%.*ls' to add or remove the COLUMN_SET attribute. To change a COLUMN_SET attribute of a column, either modify the table to remove the column and then add the column again, or drop and re-create the table.    |
|    [11409](../../relational-databases/errors-events/mssqlserver-11409-database-engine-error.md)    |    16    |    No    |    Cannot remove the column set '%.*ls' in the table '%.*ls' because the table contains more than 1025 columns. Reduce the number of columns in the table to less than 1025.    |
|    11410    |    16    |    No    |    Cannot modify the column '%.*ls' in the table '%.*ls' to a sparse column because the column has a default or rule bound to it. Unbind the rule or default from the column before designating the column as sparse.    |
|    11411    |    16    |    No    |    Cannot add the sparse column '%.*ls' to the table '%.*ls' because the data type of the column has a default or rule bound to it. Unbind the rule or default from the data type before adding the sparse column to the table.    |
|    11412    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same sparse storage attribute in tables '%.*ls' and '%.*ls'.    |
|    11413    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same column set property in tables '%.*ls' and '%.*ls'.    |
|    11414    |    10    |    No    |    Warning: Option %ls is not applicable to table %.*ls because it does not have a clustered index. This option will be applied only to the table's nonclustered indexes, if it has any.    |
|    11415    |    16    |    No    |    Object '%.*ls' cannot be disabled or enabled. This action applies only to foreign key and check constraints.    |
|    11418    |    16    |    No    |    Cannot %S_MSG table '%.*ls' because the table either contains sparse columns or a column set column which are incompatible with compression.    |
|    12002    |    16    |    No    |    The requested %S_MSG index on column '%.*ls' of table '%.*ls' could not be created because the column type is not %S_MSG . Specify a column name that refers to a column with a %S_MSG data type.    |
|    12003    |    16    |    No    |    Could not find spatial tessellation scheme '%.*ls'. Specify a valid tessellation scheme name in your USING clause.    |
|    12004    |    16    |    No    |    Could not find the default spatial tessellation scheme for the column '%.*ls' on table '%.*ls'. Make sure that the column reference is correct, or specify the extension scheme in a USING clause.    |
|    12005    |    16    |    No    |    Incorrect parameters were passed to the CREATE %S_MSG statement near '%.*ls'. Validate the statement against the index-creation syntax.    |
|    12006    |    16    |    No    |    Duplicate parameters were passed to the create index statement. Validate the statement against the index-creation syntax.    |
|    12007    |    16    |    No    |    The CREATE %S_MSG statement is missing the required parameter '%.*ls'. Validate the statement against the index-creation syntax.    |
|    12008    |    16    |    No    |    Table '%.*ls' does not have a clustered primary key as required by the %S_MSG index. Make sure that the primary key column exists on the table before creating a %S_MSG index.    |
|    12009    |    16    |    No    |    Could not find the %S_MSG index '%.*ls' on table '%.*ls'. Either no %S_MSG index with this name exists, or a non-%S_MSG index might be using the same name. Fix the index name, avoiding duplicates. If a relational index has the same name, drop the regular relational index.    |
|    12010    |    16    |    No    |    Only one spatial index hint may appear per table, either as the first or the last hinted index.    |
|    12011    |    16    |    No    |    The value of parameter '%.*ls' of CREATE %S_MSG must be less than %d.    |
|    12012    |    16    |    No    |    The value of parameter '%.*ls' of CREATE %S_MSG must be greater than %d.    |
|    12013    |    16    |    No    |    The value of parameter '%.*ls' of CREATE %S_MSG must be greater than the value of parameter '%.*ls'.    |
|    12014    |    16    |    No    |    The '%.*ls' parameter of CREATE %S_MSG is incompletely defined. If the parameter has more than one part, all the parts must be defined.    |
|    12015    |    16    |    No    |    The index option %.*ls in the CREATE %S_MSG statement has to appear before the general index options.    |
|    12016    |    16    |    No    |    Creating a %S_MSG index requires that the primary key in the base table satisfy the following restrictions. The maximum number of primary-key columns is %d. The maximum combined per-row size of the primary-key columns is %d bytes. The primary key on the base table '%.*ls' has %d columns, and contains %d bytes. Alter the base table to satisfy the primary-key restrictions imposed by the %S_MSG index.    |
|    12017    |    10    |    No    |    The spatial index is disabled or offline    |
|    12018    |    10    |    No    |    The spatial object is not defined in the scope of the predicate    |
|    12019    |    10    |    No    |    Spatial indexes do not support the comparand supplied in the predicate    |
|    12020    |    10    |    No    |    Spatial indexes do not support the comparator supplied in the predicate    |
|    12021    |    10    |    No    |    Spatial indexes do not support the method name supplied in the predicate    |
|    12022    |    10    |    No    |    The comparand references a column that is defined below the predicate    |
|    12023    |    10    |    No    |    The comparand in the comparison predicate is not deterministic    |
|    12024    |    10    |    No    |    The spatial parameter references a column that is defined below the predicate    |
|    12025    |    10    |    No    |    Could not find required binary spatial method in a condition    |
|    12026    |    10    |    No    |    Could not find required comparison predicate    |
|    12100    |    16    |    No    |    ALTER DATABASE failed because FILESTREAM filegroups cannot be added to a database that has either the READ_COMMITTED_SNAPSHOT or the ALLOW_SNAPSHOT_ISOLATION option set to ON. To add FILESTREAM filegroups, you must set READ_COMMITTED_SNAPSHOT and ALLOW_SNAPSHOT_ISOLATION to OFF.    |
|    [12300](../../relational-databases/errors-events/mssqlserver-12300-database-engine-error.md)    |        |        |    Computed columns are not supported with '*construct*'.    |
|    [12301](../../relational-databases/errors-events/mssqlserver-12301-database-engine-error.md)    |        |        |    Nullable columns in the index key are not supported with '*construct*'.    |
|    [12302](../../relational-databases/errors-events/mssqlserver-12302-database-engine-error.md)    |        |        |    Updating columns that are part of the PRIMARY KEY constraint is not supported with '*construct*'.    |
|    [12303](../../relational-databases/errors-events/mssqlserver-12303-database-engine-error.md)    |        |        |    The 'number' clause is not supported with '*construct*'.    |
|    [12304](../../relational-databases/errors-events/mssqlserver-12304-database-engine-error.md)    |        |        |    Using a memory optimized table type that uses the IDENTITY property with any of its columns is not supported when using the type outside the context of a natively compiled stored procedure.    |
|    [12305](../../relational-databases/errors-events/mssqlserver-12305-database-engine-error.md)    |        |        |    Inline table variables are not supported with '*construct*'.    |
|    [12306](../../relational-databases/errors-events/mssqlserver-12306-database-engine-error.md)    |        |        |    Cursors are not supported with '*construct*'.    |
|    [12307](../../relational-databases/errors-events/mssqlserver-12307-database-engine-error.md)    |        |        |    Default values for parameters in '*construct*' must be constants.    |
|    [12308](../../relational-databases/errors-events/mssqlserver-12308-database-engine-error.md)    |        |        |    Table-valued functions are not supported with '*construct*'.    |
|    [12329](../../relational-databases/errors-events/mssqlserver-12329-database-engine-error.md)    |        |        |    The data types char(n) and varchar(n) using a collation that has a code page other than 1252 are not supported with  *construct*.    |
|    12980    |    16    |    No    |    Supply either %s or %s to identify the log entries.    |
|    12981    |    16    |    No    |    You must specify %s when creating a subplan.    |
|    12982    |    16    |    No    |    Supply either %s or %s to identify the plan or sub-plan to be run.    |
