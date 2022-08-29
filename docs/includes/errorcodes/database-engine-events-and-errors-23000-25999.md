---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    23003    |    17    |    No    |    The WinFS share permissions have become corrupted {Error: %ld}. Please try setting the share permissions again.    |
|    23100    |    16    |    No    |    Invalid input parameter(s).    |
|    23101    |    16    |    No    |    Access is denied.    |
|    23102    |    16    |    No    |    Item does not exist {ItemId: %ls}.    |
|    23103    |    16    |    No    |    Folder already exists {ItemId: %ls}.    |
|    23104    |    16    |    No    |    Folder does not exist {ItemId: %ls}.    |
|    23105    |    16    |    No    |    Operation violates hierarchical namespace uniqueness.    |
|    23106    |    16    |    No    |    Container is not empty {ItemId: %ls}.    |
|    23107    |    16    |    No    |    Item cannot be copied onto itself.    |
|    23108    |    16    |    No    |    Scoping path does not exist or is invalid.    |
|    23109    |    16    |    No    |    Container does not exist.    |
|    23110    |    16    |    No    |    No more items to enumerate.    |
|    23111    |    16    |    No    |    Item does not exist in the given scope {ItemId: %ls, Scope: %ls}.    |
|    23112    |    16    |    No    |    Transaction not in active state.    |
|    23113    |    16    |    No    |    Item either does not exist or it is not a file-backed one.    |
|    23114    |    16    |    No    |    Sharing violation.    |
|    23115    |    16    |    No    |    Transaction bindtoken must be null when called within the context of a transaction.    |
|    23116    |    16    |    No    |    Inconsistent StreamSize and/or AllocationSize data {ItemId: %ls}.    |
|    23117    |    16    |    No    |    File-backed item does not exist {ItemId: %ls}.    |
|    23200    |    16    |    No    |    ItemId of folder '%ls' not found.    |
|    23201    |    16    |    No    |    Share '%ls' does not exist in Catalog.    |
|    23202    |    16    |    No    |    Could not delete Share '%ls' in Catalog.    |
|    23203    |    16    |    No    |    Store item not found in Catalog.    |
|    23204    |    16    |    No    |    Could not delete Store item in Catalog.    |
|    23205    |    16    |    No    |    Store database name not found in Catalog.    |
|    23206    |    16    |    No    |    Could not create share to the ItemPath '%ls'.    |
|    23207    |    16    |    No    |    Could not add Share item for '%ls' in Catalog.    |
|    23208    |    16    |    No    |    ItemPath '%ls' does not exist in Store.    |
|    23209    |    16    |    No    |    Could not update Store state in Catalog.    |
|    23210    |    16    |    No    |    Itempath '%ls' is a file-backed item or within its sub-tree.    |
|    23211    |    16    |    No    |    Could not start Store Manager. Please look in WinFS UT Log for details.    |
|    23212    |    16    |    No    |    Itempath '%ls' is a compound item.    |
|    23401    |    16    |    No    |    Operation failed with HRESULT 0x%x.    |
|    23402    |    16    |    No    |    Multiple store items were found for the given store name in the catalog.    |
|    23403    |    16    |    No    |    Cannot create a store with the specified name, it already exists.    |
|    23404    |    16    |    No    |    Cannot delete the system stores (Catalog or DefaultStore)    |
|    23405    |    16    |    No    |    Store not found.    |
|    23406    |    16    |    No    |    A share with the same name already exists.    |
|    23407    |    16    |    No    |    Could not find a share with the specified name.    |
|    23408    |    16    |    No    |    The specified itempath is invalid.    |
|    23409    |    16    |    No    |    Cannot delete system shares (Catalog or DefaultStore)    |
|    23410    |    16    |    No    |    System error occurred {ErrorCode: %d}.    |
|    23411    |    16    |    No    |    WinFS could not find the specified volume.    |
|    23412    |    16    |    No    |    WinFS Store manager is not ready for requests.    |
|    23413    |    16    |    No    |    WinFS Store Manager currently does not support non-system volumes.    |
|    23414    |    16    |    No    |    There was a timeout trying to get a lock on the volume object for this operation.    |
|    23415    |    16    |    No    |    The specified volume is not mounted or is not supported by WinFS    |
|    23416    |    16    |    No    |    Access Denied. Look in the WinFS UT log for more details.    |
|    23417    |    16    |    No    |    Invalid argument(S)    |
|    23418    |    16    |    No    |    Itempath is a compound item.    |
|    23419    |    16    |    No    |    Itempath is a file-backed item or within it's sub tree.    |
|    23420    |    16    |    No    |    Itempath does not exist in Store.    |
|    23421    |    16    |    No    |    Not enough memory available in the system to process the request.    |
|    23422    |    16    |    No    |    The Disk or File Group is full.    |
|    23423    |    16    |    No    |    Error accessing files on disk. Refer to the WinFS UT log for more information.    |
|    23424    |    16    |    No    |    The database operation was rolled back due to a deadlock. Refer to WinFS UT log for details.    |
|    23425    |    16    |    No    |    Cannot call sp_winfs_deletestore on a connection to the store specified for delete.    |
|    23426    |    16    |    No    |    Could not initialize the WSM memory clerk during startup.    |
|    23427    |    16    |    No    |    Could not initialize the WSM memory object during startup.    |
|    23428    |    16    |    No    |    Could not initialize WSM tracing during startup.    |
|    23500    |    16    |    No    |    Item container does not exist.    |
|    23501    |    16    |    No    |    Owning Item does not exist.    |
|    23502    |    16    |    No    |    NamespaceName is empty or exceeds the maximum length.    |
|    23503    |    16    |    No    |    Invalid Source endpoint type    |
|    23504    |    16    |    No    |    Invalid Target endpoint type    |
|    23505    |    16    |    No    |    A File-backed item must be a compound item type.    |
|    23506    |    16    |    No    |    A File Backed Item may not contain other Items.    |
|    23509    |    16    |    No    |    Source Item does not exist.    |
|    23510    |    16    |    No    |    Item with name already exists in container.    |
|    23511    |    16    |    No    |    New container cannot be a sub-container of item.    |
|    23513    |    16    |    No    |    Item does not exist.    |
|    23515    |    16    |    No    |    Item can not be deleted if it has children    |
|    23519    |    16    |    No    |    Target Item does not exist.    |
|    23525    |    16    |    No    |    Invalid Namespace Name.    |
|    23530    |    16    |    No    |    Operation can not be called inside a un-committable transaction    |
|    23536    |    16    |    No    |    Win32 file handle is open for item    |
|    23573    |    16    |    No    |    Cannot change ContainerId when replacing item.    |
|    23579    |    16    |    No    |    This procedure is reserved and cannot be called.    |
|    23587    |    16    |    No    |    File stream cannot be null.    |
|    23588    |    16    |    No    |    Container ids must be the same.    |
|    23996    |    16    |    No    |    The request could not be performed because of an device I/O error.    |
|    23997    |    16    |    No    |    System error occurred {ErrorCode: %d}.    |
|    23998    |    16    |    No    |    Not enough memory available in the system to process the request.    |
|    23999    |    16    |    No    |    Unspecified error(s) occurred.    |
|    25002    |    16    |    No    |    The specified Publisher is not enabled as a remote Publisher at this Distributor. Ensure the value specified for the parameter \@publisher is correct, and that the Publisher is enabled as a remote Publisher at the Distributor.    |
|    25003    |    16    |    No    |    Upgrade of the distribution database MSmerge_subscriptions table failed. Rerun the upgrade procedure in order to upgrade the distribution database.    |
|    25005    |    16    |    No    |    It is invalid to drop the default constraint on the rowguid column that is used by merge replication.    |
|    25006    |    16    |    No    |    The new column cannot be added to article '%s' because it has more than %d replicated columns.    |
|    25007    |    16    |    No    |    Cannot synchronize the subscription because the schemas of the article at the Publisher and the Subscriber do not match. It is likely that pending schema changes have not yet been propagated to the Subscriber. Run the Merge Agent again to propagate the changes and synchronize the data.    |
|    25008    |    16    |    No    |    The merge replication views could not be regenerated after performing the data definition language (DDL) operation.    |
|    25009    |    16    |    No    |    Invalid value '%s' specified while executing sp_changemergearticle on article '%s' for the 'identityrangemanagementoption' property.    |
|    25010    |    16    |    No    |    The constraint is used by merge replication for identity management and cannot be dropped directly. Execute sp_changemergearticle \@publication, \@article, "identityrangemanagementoption", "none" to disable merge identity management, which will also drop the constraint.    |
|    25012    |    16    |    No    |    Cannot add an identity column since the table is published for merge replication.    |
|    25013    |    16    |    No    |    Cannot perform alter table because the table is published in one or more publications with a publication_compatibility_level of lower than '90RTM'. Use sp_repladdcolumn or sp_repldropcolumn.    |
|    25014    |    16    |    No    |    sp_repladdcolumn does not allow adding columns of datatypes that are new to this release.    |
|    25015    |    10    |    No    |    Schema Changes and Bulk Inserts    |
|    25016    |    10    |    No    |    Prepare Dynamic Snapshot    |
|    25017    |    16    |    No    |    Failed to execute the command "%s" through xp_cmdshell. Detailed error information is returned in a result set.    |
|    25018    |    16    |    No    |    Precomputed partitions cannot be used because articles "%s" and "%s" are part of a join filter and at least one of them has a constraint with a CASCADE action defined.    |
|    25019    |    16    |    No    |    The logical record relationship between articles "%s" and "%s" cannot be added because at least one of the articles has a constraint with a CASCADE action defined.    |
|    25020    |    16    |    No    |    The article cannot be created on table '%s' because it has more than %d columns and column-level tracking is being used. Either reduce the number of columns in the table or change to row-level tracking.    |
|    25021    |    16    |    No    |    Replication stored procedure sp_MSupdategenhistory failed to update the generation '%s'. This generation will be retried in the next merge.    |
|    25022    |    16    |    No    |    The snapshot storage option (\@snapshot_storage_option) must be 'file system', or 'database'.    |
|    25023    |    16    |    No    |    Stored procedures containing table-value parameters cannot be published as '[serializable] proc exec' articles.    |
|    25024    |    16    |    No    |    A snapshot storage option of 'database' is incompatible with the use of character mode bcp for snapshot generation.    |
|    25025    |    16    |    No    |    Cannot add a sparse column or a sparse column set because the table is published for merge replication. Merge replication does not support sparse columns.    |
|    25026    |    16    |    No    |    The proc sp_registercustomresolver cannot proceed because it is not run in the context of the distribution database, or the distribution database is not properly upgraded.    |
|    25601    |    17    |    No    |    The extended event engine is out of memory.    |
|    25602    |    17    |    No    |    The %S_MSG, "%.*ls", encountered a configuration error during initialization. Object cannot be added to the event session.    |
|    25603    |    17    |    No    |    The %S_MSG, "%.*ls", could not be added. The maximum number of singleton targets has been reached.    |
|    25604    |    17    |    No    |    The extended event engine is disabled.    |
|    25605    |    17    |    No    |    The %S_MSG, "%.*ls", could not be added. The maximum number of packages has been reached.    |
|    25606    |    17    |    No    |    The extended event engine could not be initialized. Check the SQL Server error log and the Windows event logs for information about possible related problems.    |
|    25607    |    17    |    No    |    The extended event engine has been disabled by startup options. Features that depend on extended events may fail to start.    |
|    25623    |    16    |    No    |    The %S_MSG name, "%.*ls", is invalid, or the object could not be found    |
|    25624    |    16    |    No    |    The constraints of %S_MSG name, "%.*ls", have been violated. The object does not support binding to actions or predicates. Event not added to event session.    |
|    25625    |    16    |    No    |    The %S_MSG, "%.*ls", already exists in the event session. Object cannot be added to the event session.    |
|    25629    |    16    |    No    |    For %S_MSG, "%.*ls", the customizable attribute, "%ls", does not exist.    |
|    25630    |    16    |    No    |    The predicate expression bound to %S_MSG ,"%.*ls", has mismatching types.    |
|    25631    |    16    |    No    |    The %S_MSG, "%.*ls", already exists. Choose a unique name for the event session.    |
|    25632    |    16    |    No    |    The specified buffer size is less than the minimum size.    |
|    25633    |    16    |    No    |    The buffer size specified exceeds the maximum size.    |
|    25634    |    16    |    No    |    The dispatch latency specified is below the minimum size.    |
|    25635    |    16    |    No    |    An attempt was made to add an asynchronous target to a session with a maximum memory of 0. For asynchronous targets to be added to a session, the session must have a maximum memory greater than 0.    |
|    25636    |    16    |    No    |    Source and comparator types of the predicate do not match.    |
|    25638    |    16    |    No    |    Event data exceeded allowed maximum.    |
|    25639    |    16    |    No    |    The %S_MSG, "%.*ls", exceeds the number of allowable bound actions.    |
|    25640    |    16    |    No    |    Maximum event size is smaller than configured event session memory. Specify a larger value for maximum event size, or specify 0.    |
|    25641    |    16    |    No    |    For %S_MSG, "%.*ls", the parameter "%ls" passed is invalid. %ls    |
|    25642    |    16    |    No    |    Mandatory customizable attributes are missing for %S_MSG, "%.*ls".    |
|    25643    |    16    |    No    |    The %S_MSG, "%.*ls", can not be added to an event session that specifies no event loss.    |
|    25644    |    16    |    No    |    The %S_MSG, "%.*ls", cannot be bound to the event session.    |
|    25646    |    16    |    No    |    The %S_MSG name, "%.*ls", is invalid.    |
|    25647    |    16    |    No    |    The %S_MSG, "%.*ls", could not be found. Ensure that the object exists and the name is spelled correctly.    |
|    25648    |    16    |    No    |    The %S_MSG, "%.*ls", could not be found. Ensure that the package exists and the name is spelled correctly.    |
|    25649    |    16    |    No    |    Two of the actions/predicates for %S_MSG, "%.*ls", can not coexist. Please remove one.    |
|    25650    |    16    |    No    |    For %S_MSG, "%.*ls" the customizable attribute, "%ls", was specified multiple times.    |
|    25651    |    16    |    No    |    For %S_MSG, "%.*ls", the value specified for customizable attribute, "%ls", did not match the expected type, "%ls".    |
|    25653    |    16    |    No    |    The %S_MSG, "%.*ls", does not exist in the event session. Object cannot be dropped from the event session.    |
|    25699    |    17    |    No    |    The extended event engine failed unexpectedly while performing an operation.    |
|    25701    |    15    |    No    |    Invalid event session name "%.*ls". Temporary event sessions are not allowed.    |
|    25702    |    16    |    No    |    The event session option, "%.*ls", is set more than once. Remove the duplicate session option and re-issue the statement.    |
|    25703    |    16    |    No    |    The event session option, "%.*ls", has an invalid value. Correct the value and re-issue the statement.    |
|    25704    |    16    |    No    |    The event session has already been stopped.    |
|    25705    |    16    |    No    |    The event session has already been started.    |
|    25706    |    16    |    No    |    The %S_MSG, "%.*ls", could not be found.    |
|    25707    |    16    |    No    |    Event session option "%.*ls" cannot be changed while the session is running. Stop the event session before changing this session option.    |
|    25708    |    16    |    No    |    The "%.*ls" specified exceeds the maximum allowed value. Specify a smaller configuration value.    |
|    25709    |    16    |    No    |    One or more event sessions failed to start. Refer to previous errors in the current session to identify the cause, and correct any associated problems.    |
|    25710    |    16    |    Yes    |    Event session "%.*ls" failed to start. Refer to previous errors in the current session to identify the cause, and correct any associated problems.    |
|    25711    |    16    |    No    |    Failed to parse an event predicate.    |
|    25712    |    16    |    No    |    An invalid comparison operator was specified for an event predicate.    |
|    25713    |    16    |    No    |    The value specified for %S_MSG, "%.*ls", %S_MSG, "%.*ls", is invalid.    |
|    25715    |    16    |    No    |    The predicate on event "%ls" is invalid. Operator '%ls' is not defined for type "%ls", %S_MSG: "%.*ls".    |
|    25716    |    16    |    No    |    The predicate on event, "%.*ls", exceeds the maximum length of %d characters.    |
|    25717    |    16    |    No    |    The operating system returned error %ls while reading from the file '%s'.    |
|    25718    |    16    |    No    |    The log file name "%s" is invalid. Verify that the file exists and that the SQL Server service account has access to it.    |
|    25719    |    16    |    No    |    Initial file name and initial offset must be specified as a pair. Please correct the parameters and retry your query.    |
|    25720    |    10    |    No    |    'sys.fn_xe_file_target_read_file' is skipping records from "%.*ls" at offset %I64d.    |
|    25721    |    16    |    No    |    The metadata file name "%s" is invalid. Verify that the file exists and that the SQL Server service account has access to it.    |
|    25722    |    16    |    No    |    The offset %d is invalid for log file "%s". Specify an offset that exists in the log file and retry your query.    |
|    25723    |    16    |    No    |    An error occurred while obtaining metadata information from the file "%s". The file may be damaged.    |
|    25724    |    16    |    No    |    Predicate too large for display    |
|    25807    |    16    |    No    |    The %d KB buffer size specified is too low for the ETW target. Increase the size and retry your command.    |
|    25809    |    16    |    No    |    The log file path "%ls" to the ETW target is invalid. Verify that the path exists and that the SQL Server startup account has access to the log file.    |
|    25810    |    16    |    No    |    The session name specified exceeds the maximum length of %d characters.    |
|    25811    |    16    |    No    |    The operating system returned error 5 (ACCESS_DENIED) while creating an ETW tracing session. Ensure that the SQL Server startup account is a member of the 'Performance Log Users' group and then retry your command.    |
|    25812    |    16    |    No    |    The field '%ls' is invalid for the event '%ls'. Verify that the field exists and try your command again.    |
|    25813    |    16    |    No    |    The action "%ls" does not exist. Verify that the action exists and try your command again.    |
|    25814    |    16    |    No    |    The filtering event "%ls" does not exist. Verify that the event exists and try your command again.    |
|    25815    |    16    |    No    |    The source type, %d, is invalid. Valid types are 0 for event fields or 1 for actions.    |
|    25816    |    16    |    No    |    The slot count, %d, exceeds the maximum slot count size of %d. Reduce the slot count value and try your command again.    |
|    25817    |    16    |    No    |    The column or action name specified exceeds the maximum length of %d characters.    |
|    25818    |    16    |    No    |    The event "%ls" was not found.    |
|    25819    |    16    |    No    |    The column "%ls" was not found.    |
|    25820    |    16    |    No    |    The action "%ls" was not found.    |
|    25821    |    16    |    No    |    The number of begin and end columns must match. Specify the same number of columns for begin and end matching.    |
|    25822    |    16    |    No    |    The number of begin and end actions must match. Specify the same number of actions for begin and end matching.    |
|    25824    |    16    |    No    |    The begin and end columns at position %d are not of the same type. Specify matching columns that are the same type and try your command again.    |
|    25825    |    16    |    No    |    The begin and end action at postition %d are not of the same type. Specify matching actions that are the same type and try your command again.    |
|    25827    |    16    |    No    |    The operating system returned error %d while creating the file '%ls'. Verify that the path exists and that the SQL Server startup account has access to the location.    |
|    25833    |    16    |    No    |    Counter target could not add package "%ls" to the list of packages to count. Events from this package will not be counted.    |
|    25834    |    16    |    No    |    Ring Buffer target could not add package "%ls" to the package occurrence list. Events from this package will not be kept based on occurrence number.    |
