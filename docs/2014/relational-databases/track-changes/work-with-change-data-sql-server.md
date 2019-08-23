---
title: "Work with Change Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "change data [SQL Server]"
  - "change data capture [SQL Server], query function scenarios"
  - "change data capture [SQL Server], LSN boundaries"
  - "change data capture [SQL Server], query functions"
ms.assetid: 5346b852-1af8-4080-b278-12efb9b735eb
author: rothja
ms.author: jroth
manager: craigg
---
# Work with Change Data (SQL Server)
  Change data is made available to change data capture consumers through table-valued functions (TVFs). All queries of these functions require two parameters to define the range of Log Sequence Numbers (LSNs) that are eligible for consideration when developing the returned result set. Both the upper and lower LSN values that bound the interval are considered to be included within the interval.  
  
 Several functions are provided to help determine appropriate LSN values for use in querying a TVF. The function [sys.fn_cdc_get_min_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-get-min-lsn-transact-sql) returns the smallest LSN that is associated with a capture instance validity interval. The validity interval is the time interval for which change data is currently available for its capture instances. The function [sys.fn_cdc_get_max_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-get-max-lsn-transact-sql) returns the largest LSN in the validity interval. The functions [sys.fn_cdc_map_time_to_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-map-time-to-lsn-transact-sql) and [sys.fn_cdc_map_lsn_to_time](/sql/relational-databases/system-functions/sys-fn-cdc-map-lsn-to-time-transact-sql) are available to help place LSN values on a conventional timeline. Because change data capture uses closed query intervals, it is sometimes necessary to generate the next LSN value in a sequence to ensure that changes are not duplicated in consecutive query windows. The functions [sys.fn_cdc_increment_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-increment-lsn-transact-sql) and [sys.fn_cdc_decrement_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-decrement-lsn-transact-sql) are useful when an incremental adjustment to an LSN value is required.  
  
##  <a name="LSN"></a> Validating LSN Boundaries  
 We recommend validating the LSN boundaries that are to be used in a TVF query before their use. Null endpoints or endpoints that lie outside the validity interval for a capture instance will force an error to be returned by a change data capture TVF.  
  
 For example, the following error is returned for a query for all changes when a parameter that is used to define the query interval is not valid, or is out of range, or the row filter option is invalid.  
  
 `Msg 313, Level 16, State 3, Line 1`  
  
 `An insufficient number of arguments were supplied for the procedure or function cdc.fn_cdc_get_all_changes_ ...`  
  
 The corresponding error returned for a `net changes` query is the following:  
  
 `Msg 313, Level 16, State 3, Line 1`  
  
 `An insufficient number of arguments were supplied for the procedure or function cdc.fn_cdc_get_net_changes_ ...`  
  
> [!NOTE]  
>  It is recognized that the message for Msg 313 is misleading and does not convey the actual cause of the failure. This awkward usage stems from the inability to raise an explicit error from within a TVF. Nevertheless, the value of returning a recognizable, if inaccurate, error was deemed preferable to simply returning an empty result. An empty result set would not be distinguishable from a valid query returning no changes.  
  
 Authorization failures will return failures when querying for all changes, as shown:  
  
 `Msg 229, Level 14, State 5, Line 1`  
  
 `The SELECT permission was denied on the object 'fn_cdc_get_all_changes_...', database 'MyDB', schema 'cdc'.`  
  
 The same is true when querying for net changes:  
  
 `Msg 229, Level 14, State 5, Line 1`  
  
 `The SELECT permission was denied on the object fn_cdc_get_net_changes_...', database 'MyDB', schema 'cdc'.`  
  
 See the template Enumerate Net Changes Using TRY CATCH for a demonstration of how to intercept these known TVF errors and return more meaningful information about the failure.  
  
> [!NOTE]  
>  To locate change data capture templates in SQL Server Management Studio, on the **View** menu, click **Template Explorer**, expand **SQL Server Templates** and then expand the **Change Data Capture** folder.  
  
##  <a name="Functions"></a> Query Functions  
 Depending on the characteristics of the source table being tracked and the way in which its capture instance is configured, either one or two TVFs for querying change data are generated.  
  
-   The function [cdc.fn_cdc_get_all_changes_<capture_instance>](/sql/relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql) returns all changes that occurred for the specified interval. This function is always generated. Entries are always returned sorted, first by the transaction commit LSN of the change, and then by a value that sequences the change within its transaction. Depending on the row filter option chosen, either the final row is returned on update (row filter option "all") or both the new and old values are returned on update (row filter option "all update old"').  
  
-   The function [cdc.fn_cdc_get_net_changes_<capture_instance>](/sql/relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql) is generated when the parameter @supports_net_changes is set to 1 when the source table is enabled.  
  
    > [!NOTE]  
    >  This option is only supported if the source table has a defined primary key or if the parameter @index_name has been used to identify a unique index.  
  
     The **netchanges** function returns one change per modified source table row. If more than one change is logged for the row during the specified interval, the column values will reflect the final contents of the row. To correctly identify the operation that is necessary to update the target environment, the TVF must consider both the initial operation on the row during the interval and the final operation on the row. When the row filter option 'all' is specified, the operations that are returned by a `net changes` query will either be insert, delete, or update (new values). This option always returns the update mask as null because there is a cost associated with computing an aggregate mask. If you require an aggregate mask that reflects all changes to a row, use the 'all with mask' option. If downstream processing does not require inserts and updates to be distinguished, use the 'all with merge' option. In this case, the operation value will only take on two values: 1 for delete and 5 for an operation that could be either an insert or an update. This option eliminates the additional processing needed to determine whether the derived operation should be an insert or an update, and can improve the performance of the query when this differentiation is not necessary.  
  
 The update mask that is returned from a query function is a compact representation that identifies all columns that changed in a row of change data. Typically, this information is only required for a small subset of the captured columns. Functions are available to assist in extracting information from the mask in a form that is more directly usable by applications. The function [sys.fn_cdc_get_column_ordinal](/sql/relational-databases/system-functions/sys-fn-cdc-get-column-ordinal-transact-sql) returns the ordinal position of a named column for a given capture instance, whereas the function [sys.fn_cdc_is_bit_set](/sql/relational-databases/system-functions/sys-fn-cdc-is-bit-set-transact-sql) returns the parity of the bit in the provided mask based on the ordinal that was passed in the function call. Together, these two functions allow information from the update mask to be efficiently extracted and returned with the request for change data. See the template Enumerate Net Changes Using All With Mask for a demonstration of how these functions are used.  
  
##  <a name="Scenarios"></a> Query Function Scenarios  
 The following sections describe common scenarios for querying change data capture data by using the query functions cdc.fn_cdc_get_all_changes_<capture_instance> and cdc.fn_cdc_get_net_changes_<capture_instance>.  
  
### Querying for All Changes Within the Capture Instance Validity Interval  
 The most straightforward request for change data is one that returns all of the current change data in a capture instance's validity interval. To make this request, first determine the lower and upper LSN boundaries of the validity interval. Then, use these values to identify the parameters @from_lsn and @to_lsn passed to the query function cdc.fn_cdc_get_all_changes_<capture_instance> or cdc.fn_cdc_get_net_changes_<capture_instance>. Use the function [sys.fn_cdc_get_min_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-get-min-lsn-transact-sql) to obtain the lower bound, and [sys.fn_cdc_get_max_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-get-max-lsn-transact-sql) to obtain the upper bound. See the template Enumerate All Changes for the Valid Range for sample code to query for all current valid changes by using the query function cdc.fn_cdc_get_all_changes_<capture_instance>. See the template Enumerate Net Changes for the Valid Range for a similar example of using the function cdc.fn_cdc_get_net_changes_<capture_instance>.  
  
### Querying for All New Changes Since the Last Set of Changes  
 For typical applications, querying for change data will be an ongoing process, making periodic requests for all of the changes that occurred since the last request. For such queries, you can use the function [sys.fn_cdc_increment_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-increment-lsn-transact-sql) to derive the lower bound of the current query from the upper bound of the previous query. This method ensures that no rows are repeated because the query interval is always treated as a closed interval where both end-points are included in the interval. Then, use the function [sys.fn_cdc_get_max_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-get-max-lsn-transact-sql) to obtain the high end-point for the new request interval. See the template Enumerate All Changes Since Previous Request for sample code to systematically move the query window to obtain all changes since the last request.  
  
### Querying for all New Changes Up Until Now  
 A typical constraint that is placed on the changes returned by a query function is to include only the changes that occurred between the previous request until the current date and time. For this query, apply the function sys.fn_cdc_increment_lsn to the @from_lsn value that was used in the previous request to determine the lower bound. Because the upper bound on the time interval is expressed as a specific point in time, it must be converted to an LSN value before it can be used by a query function. Before the datetime value can be converted to a corresponding LSN value, you must ensure that the capture process has processed all changes that were committed through the specified upper bound. This is required to ensure that all the qualifying changes have been propagated to the change table. One way to do this is to structure a wait loop that periodically checks to see if the current maximum commit lsn recorded for any database change table exceeds the desired end time of the request interval.  
  
 After the delay loop verifies that the capture process has already processed all the relevant log entries, use the function [sys.fn_cdc_map_time_to_lsn](/sql/relational-databases/system-functions/sys-fn-cdc-map-time-to-lsn-transact-sql) to determine the new high end-point expressed as an LSN value. To ensure that all entries that were committed through the specified time are retrieved, call the function sys.fn_cdc_map_time_to_lsn, and use the option 'largest less than or equal'.  
  
> [!NOTE]  
>  In periods of inactivity, a dummy entry is added to the table cdc.lsn_time_mapping to mark the fact that the capture process has processed the changes up to a given commit time. This prevents it from appearing that the capture process has fallen behind when there are simply no recent changes to process.  
  
 The template Enumerate All Changes Up Until Now demonstrates how to use the previous strategy to query for change data.  
  
### Adding a Commit Time to an All Changes Result Set  
 The commit time of each transaction with an associated entry in a database change table is available in the table [cdc.lsn_time_mapping](/sql/relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql). By joining the __$start_lsn value returned in a request for all changes with the start_lsn value of a cdc.lsn_time_mapping table entry, you can return the tran_end_time along with the change data to stamp the change with the commit time of the transaction at the source. The template Append Commit Time to All Changes Result Set demonstrates how to perform this join.  
  
### Joining Change Data with Other Data from the Same Transaction  
 Occasionally, it is useful to join change data with other information gathered about the transaction when it committed at the source. The tran_begin_lsn column in the table cdc.lsn_time_mapping provides the information needed to perform such a join. When the update of the source occurs, the value for database_transaction_begin_lsn from the system dynamic view [sys.dm_tran_database_transactions](/sql/relational-databases/system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql) must be saved along with any other information to be joined with the change data. Use the function fn_convertnumericlsntobinary to compare the database_transaction_begin_lsn and tran_begin_lsn values. The code to create this function is available in the template Create Function fn_convertnumericlsntobinary. The template Return All Changes with a Given tran_begin_lsn demonstrates how to effect the join.  
  
### Querying Using Datetime Wrapper Functions  
 A typical application scenario for querying for change data is to periodically request change data by using a sliding window bounded by datetime values. For this class of consumers, change data capture provides the stored procedure [sys.sp_cdc_generate_wrapper_function](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-generate-wrapper-function-transact-sql) that generates scripts to create custom wrapper functions for the change data capture query functions. These custom wrappers allow the query interval to be expressed as a datetime pair.  
  
 Calling options for the stored procedure allow for wrappers to be generated for all capture instances that the caller has access to, or only a specified capture instance. Supported options also include the ability to specify whether the high end-point of the capture interval should be open or closed, which of the available captured columns should be included in the result set and which of the included columns should have associated update flags. The procedure returns a result set with two columns: the generated function name, which is derivable from the capture instance name, and the create statement for the wrapper stored procedure. The function to wrap the all changes query is always generated. If the @supports_net_changes parameter was set when the capture instance was created, the function to wrap the net changes function is also generated.  
  
 It is the responsibility of the application designer to call the script generation stored procedure to generate the create statements for the wrapper stored procedures, and to execute the resulting create scripts to create the functions. This does not occur automatically when a capture instance is created.  
  
 Datetime wrappers are owned by the user, and not are created in the default schema of the caller. The generated function is suitable without modification for most users. However, further customization can always be applied to the generated script prior to creating the function.  
  
 The name of the function to wrap the all changes query is fn_all_changes_ followed by the capture instance name. The prefix that is used for the net changes wrapper is fn_net_changes_. Both functions take three arguments, just as their associated change data capture TVFs do. However, the query interval for the wrappers is bounded by two datetime values instead of than by two LSN values. The @row_filter_option parameter for both sets of functions are the same.  
  
 The generated wrapper functions support the following convention for systematically walking the change data capture timeline: It is expected that the @end_time parameter of the previous interval be used as the @start_time parameter of the subsequent interval. The wrapper function takes care of mapping the datetime values to LSN values and ensuring that no data is lost or repeated if this convention is followed.  
  
 The wrappers can be generated to support either a closed upper bound or an open upper bound on the specified query window. That is, the caller can specify whether entries having a commit time equal to the upper bound of the extraction interval are to be included within the interval. By default, the upper bound is included.  
  
 While the generated query TVFs fail if supplied a null value for either the @from_lsn value or the @to_lsn value, the datetime wrapper functions use null to allow the datetime wrappers to return all current changes. That is, if null is passed as the low end-point of the query window to the datetime wrapper, the low end point of the capture instance validity interval is used in the underlying SELECT statement that is applied to the query TVF. Similarly, if null is passed as the high end-point of the query window, the high end-point of the capture instance validity interval is used when selecting from the query TVF.  
  
 The result set returned by a wrapper function includes all the requested columns followed by an operation column, recoded as one or two characters to identify the operation that is associated with the row. If update flags have been requested, they appear as bit columns after the operation code, in the order specified in the @update_flag_list parameter. For information about the calling options for customizing the generated datetime wrappers, see [sys.sp_cdc_generate_wrapper_function &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-generate-wrapper-function-transact-sql).  
  
 The template Instantiate a Wrapper TVF With Update Flag shows how to customize a generated wrapper function to append an update flag for a specified column to the result set returned by a net changes query. The template Instantiate CDC Wrapper TVFs for a Schema shows how to instantiate the Datetime Wrappers for the Query TVFs for all of the capture instances created for the source tables in a given database schema.  
  
 For an example that uses a datetime wrapper to query for change data, see the template Get Net Changes Using Wrapper With Update Flags. This template demonstrates how to query for net changes with a wrapper function when the wrapper is configured to return update flags. Note that the row filter option 'all with mask' is required for the underlying query function to return a non-null update mask on update. Null values are passed for both the lower and upper datetime interval boundaries to signal the function to use the low end point and the high end point of the validity interval for the capture instance when performing the underlying LSN based query. The query returns one row for each modification to a source row that occurred within the valid range for the capture instance.  
  
### Using the Datetime Wrapper Functions to Transition Between Capture Instances  
 Change data capture supports up to two capture instances for a single tracked source table. The principal use of this capability is to accommodate a transition between multiple capture instances when data definition language (DDL) changes to the source table expand the set of available columns for tracking. When transitioning to a new capture instance, one way to protect higher application levels from changes in the names of the underlying query functions is to use a wrapper function to wrap the underlying call. Then, ensure that the name of the wrapper function remains the same. When the switch is to occur, the old wrapper function can be dropped, and a new one with the same name created that references the new query functions. By first modifying the generated script to create a wrapper function of the same name, you can make the switch to a new capture instance without affecting higher application layers.  
  
## See Also  
 [Track Data Changes &#40;SQL Server&#41;](../track-changes/track-data-changes-sql-server.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../track-changes/about-change-data-capture-sql-server.md)   
 [Enable and Disable Change Data Capture &#40;SQL Server&#41;](../track-changes/enable-and-disable-change-data-capture-sql-server.md)   
 [Administer and Monitor Change Data Capture &#40;SQL Server&#41;](../track-changes/administer-and-monitor-change-data-capture-sql-server.md)  
  
  
