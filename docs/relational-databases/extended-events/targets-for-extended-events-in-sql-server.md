---
title: "Targets for Extended Events in SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xevents
ms.topic: conceptual
ms.assetid: 47c64144-4432-4778-93b5-00496749665b
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# Targets for Extended Events in SQL Server

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]


This article explains when and how to use the package0 targets for extended events in SQL Server. For each target, the present article explains:

- Its abilities in gathering and reporting the data sent by events.
- Its parameters, except where the parameter is self-explanatory.


#### XQuery example


The [ring_buffer section](#h2_target_ring_buffer) includes an example of using [XQuery in Transact-SQL](../../xquery/xquery-language-reference-sql-server.md) to copy a string of XML into a relational rowset.


### Prerequisites


- Be generally familiar with the basics of extended events, as described in [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md).


- Have installed a recent version of the frequently updated utility SQL Server Management Studio (SSMS.exe). For details see:
    - [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md)


- In SSMS.exe, know how to use the **Object Explorer** to right-click the target node under your event session, for [easy viewing of the output data](../../relational-databases/extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server.md).
	- The event data is captured as an XML string. Yet in this article the data is displayed in relational rows. SSMS was used to view the data, and then was copied and pasted into this article.
	- The alternative T-SQL technique for generating rowsets from XML is explained in the [ring_buffer section](#h2_target_ring_buffer). It involves XQuery.



## Parameters, actions, and fields


In Transact-SQL, the [CREATE EVENT SESSION](~/t-sql/statements/create-event-session-transact-sql.md) statement is central to extended events. To write the statement you often need a list and description of the following:

- The fields associated with your chosen event.
- The parameters associated with your chosen target.

SELECT statements which return such lists from system views are available to copy from the following article, in its section C:

- [SELECTs and JOINs From System Views for Extended Events in SQL Server](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md)
	- [C.4](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_4_data_fields) SELECT fields for an event.
	- [C.6](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_6_parameters_targets) SELECT parameters for a target.
	- [C.3](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_3_select_all_available_objects) SELECT actions.


You can see parameters, fields, and actions used in the context of an actual CREATE EVENT SESSION statement, at [this link](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_B_2_TSQL_perspective).



<a name="h2_target_etw_classic_sync_target"></a>

## etw_classic_sync_target target


SQL Server extended events can inter-operate with Event Tracing for Windows (ETW) to monitor system activity. For more information, see:

- [Event Tracing for Windows Target](../../relational-databases/extended-events/event-tracing-for-windows-target.md)
- [Monitor System Activity Using Extended Events](../../relational-databases/extended-events/monitor-system-activity-using-extended-events.md)


This ETW target processes *synchronously* the data it receives, whereas most targets process *asynchronously*.

> [!NOTE]
> Azure SQL Database does not support the `etw_classic_sync_target target`.

<!-- After OPS Versioning is live, the above !NOTE could be converted into a "3colon ZONE".  GeneMi = MightyPen. -->

<a name="h2_target_event_counter"></a>

## event_counter target


The event_counter target merely counts how many times each specified event occurs.


Unlike most other targets:

- The event_counter has no parameters.


- Unlike most targets, the event_counter target processes *synchronously* the data it receives.
	- Synchronous is acceptable for the simple event_counter because the event_counter involves so little processing.
	- The database engine will disconnect from any target which is too slow and which thereby threatens to slow the performance of the database engine. This is one reason why most targets process *asynchronously*.


#### Example output captured by event_counter


```
package_name   event_name         count
------------   ----------         -----
sqlserver      checkpoint_begin   4
```


Next is the CREATE EVENT SESSION that led to the previous results. For this test, on the EVENT...WHERE clause, the **package0.counter** field was used to cease the counting after the count climbed to 4.


```sql
CREATE EVENT SESSION [event_counter_1]
	ON SERVER 
	ADD EVENT sqlserver.checkpoint_begin   -- Test by issuing CHECKPOINT; statements.
	(
		WHERE ([package0].[counter] <= (4))   -- A predicate filter.
	)
	ADD TARGET package0.event_counter
	WITH
	(
		MAX_MEMORY = 4096 KB,
		MAX_DISPATCH_LATENCY = 3 SECONDS
	);
```



<a name="h2_target_event_file"></a>

## event_file target


The **event_file** target writes event session output from buffer to a disk file:


- You specify the *filename=* parameter on the ADD TARGET clause.
	- **.xel** must be the extension of the file.


- The file name you choose is used by the system as a prefix to which a date-time based long integer is appended, followed by the .xel extension.

::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || = sqlallproducts-allversions"

> [!NOTE]
> Azure SQL Database only supports storing `xel` files on Azure blob storage. 
>
> For an **event_file** code example particular to SQL Database (and to SQL Database Managed Instance), see [Event File target code for extended events in SQL Database](https://docs.microsoft.com/azure/sql-database/sql-database-xevent-code-event-file).

::: moniker-end


#### CREATE EVENT SESSION with **event_file** target


Next is the CREATE EVENT SESSION that we used to test with. One of the ADD TARGET clauses specifies an event_file.


```sql
CREATE EVENT SESSION [locks_acq_rel_eventfile_22]
	ON SERVER 
	ADD EVENT sqlserver.lock_acquired
	(
		SET
			collect_database_name=(1),
			collect_resource_description=(1)

		ACTION (sqlserver.sql_text,sqlserver.transaction_id)

		WHERE
		(
			[database_name]=N'InMemTest2'
			AND
			[object_id]=(370100359)
		)
	),
	ADD EVENT sqlserver.lock_released
	(
		SET
			collect_database_name=(1),
			collect_resource_description=(1)

		ACTION(sqlserver.sql_text,sqlserver.transaction_id)

		WHERE
		(
			[database_name]=N'InMemTest2'
			AND
			[object_id]=(370100359)
		)
	)
	ADD TARGET package0.event_counter,
	ADD TARGET package0.event_file
	(
		SET 	filename=N'C:\Junk\locks_acq_rel_eventfile_22-.xel'
	)
	WITH
	(
		MAX_MEMORY=4096 KB,
		MAX_DISPATCH_LATENCY=10 SECONDS
	);
```


#### sys.fn_xe_file_target_read_file function


The event_file target stores the data it receives in a binary format that is not human readable. Transact-SQL can report the contents of the .xel file by SELECTing FROM the [**sys.fn_xe_file_target_read_file**](../../relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql.md) function.


For SQL Server **2016** and later, the following T-SQL SELECT reported the data. The *.xel suffix in the 


```
SELECT f.*
		--,CAST(f.event_data AS XML)  AS [Event-Data-Cast-To-XML]  -- Optional
	FROM
		sys.fn_xe_file_target_read_file(
			'C:\junk\locks_acq_rel_eventfile_22-*.xel',
			null, null, null)  AS f;
```


For SQL Server **2014**, a SELECT similar to the following would report the data. After SQL Server 2014, the .xem files are no longer used.


```
SELECT f.*
		--,CAST(f.event_data AS XML)  AS [Event-Data-Cast-To-XML]  -- Optional
	FROM
		sys.fn_xe_file_target_read_file(
			'C:\junk\locks_acq_rel_eventfile_22-*.xel',
			'C:\junk\metafile.xem',
			null, null)  AS f;
```


Of course, you can also manually use the SSMS UI to see the .xel data:


#### Data stored in the event_file target


Next is the report from SELECTing from **sys.fn_xe_file_target_read_file**, in SQL Server 2016.


```
module_guid                            package_guid                           object_name     event_data                                                                                                                                                                                                                                                                                          file_name                                                      file_offset
-----------                            ------------                           -----------     ----------                                                                                                                                                                                                                                                                                          ---------                                                      -----------
D5149520-6282-11DE-8A39-0800200C9A66   03FDA7D0-91BA-45F8-9875-8B6DD0B8E9F2   lock_acquired   <event name="lock_acquired" package="sqlserver" timestamp="2016-08-07T20:13:35.827Z"><action name="transaction_id" package="sqlserver"><value>39194</value></action><action name="sql_text" package="sqlserver"><value><![CDATA[  select top 1 * from dbo.T_Target;  ]]></value></action></event>   C:\junk\locks_acq_rel_eventfile_22-_0_131150744126230000.xel   11776
D5149520-6282-11DE-8A39-0800200C9A66   03FDA7D0-91BA-45F8-9875-8B6DD0B8E9F2   lock_released   <event name="lock_released" package="sqlserver" timestamp="2016-08-07T20:13:35.832Z"><action name="transaction_id" package="sqlserver"><value>39194</value></action><action name="sql_text" package="sqlserver"><value><![CDATA[  select top 1 * from dbo.T_Target;  ]]></value></action></event>   C:\junk\locks_acq_rel_eventfile_22-_0_131150744126230000.xel   11776
```



<a name="h2_target_histogram"></a>

## histogram target


The **histogram** target is fancier than the event_counter target. The histogram can do the following:

- Count occurrences for several items separately.
- Count occurrences of different types of items:
	- Event fields.
	- Actions.


The **source_type** parameter is the key to controlling the histogram target:

- **source_type=0** - Means collect data for *event fields*).
- **source_type=1** - Means collect data for *actions*.
	- 1 is the default.


The 'slots' parameter default is 256. If you assign another value, the value is rounded up to the next power of 2.

- For example, slots=59 would be rounded up to =64.


### *Action* example for histogram


On its TARGET...SET clause, the following Transact-SQL CREATE EVENT SESSION statement specifies the target parameter assignment of **source_type=1**. The 1 means the histogram target tracks an action.

In the present example, the EVENT...ACTION clause offer happens to offer only one action for the target to choose, namely **sqlos.system_thread_id**. On the TARGET...SET clause, we see the assignment **source=N'sqlos.system_thread_id'** assignment.

- To track more than one source action, you can add a second histogram target to your CREATE EVENT SESSION statement.


```sql
CREATE EVENT SESSION [histogram_lockacquired]
	ON SERVER 
	ADD EVENT sqlserver.lock_acquired
		(
		ACTION
			(
			sqlos.system_thread_id
			)
		)
	ADD TARGET package0.histogram
		(
		SET
			filtering_event_name=N'sqlserver.lock_acquired',
			slots=(16),
			source=N'sqlos.system_thread_id',
			source_type=1
		)
	WITH
		(
		<.... (For brevity, numerous parameter assignments generated by SSMS.exe are not shown here.) ....>
		);
```


 The following data was captured. The values under the **value** column were system_thread_id values. For instance, a total of 236 locks were taken under thread 6540.


```
value   count
-----   -----
 6540     236
 9308      91
 9668      74
10144      49
 5244      44
 2396      28
```


#### SELECT to discover available actions


The [C.3](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_3_select_all_available_objects) SELECT statement can find the actions that the system has available for you to specify on your CREATE EVENT SESSION statement. In the WHERE clause, you would first edit the **o.name LIKE** filter to match the actions that interest you.


Next is a sample rowset returned by the C.3 SELECT. The **system_thread_id** action is seen in the second row.


```
Package-Name   Action-Name                 Action-Description
------------   -----------                 ------------------
package0       collect_current_thread_id   Collect the current Windows thread ID
sqlos          system_thread_id            Collect current system thread ID
sqlserver      create_dump_all_threads     Create mini dump including all threads
sqlserver      create_dump_single_thread   Create mini dump for the current thread
```


### Event *field* example for histogram


The following example sets **source_type=0**. The value assigned to **source=** is an event field (not an action).



```sql
CREATE EVENT SESSION [histogram_checkpoint_dbid]
	ON SERVER 
	ADD EVENT  sqlserver.checkpoint_begin
	ADD TARGET package0.histogram
	(
	SET
		filtering_event_name = N'sqlserver.checkpoint_begin',
		source               = N'database_id',
		source_type          = (0)
	)
	WITH
	( <....> );
```


The following data was captured by the histogram target. The data shows that the database that is ID=5 experienced 7 checkpoint_begin events.


```
value   count
-----   -----
5       7
7       4
6       3
```


#### SELECT to discover available fields on your chosen event


The [C.4](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_4_data_fields) SELECT statement shows event fields that you can choose from. You would first edit the **o.name LIKE** filter to be your chosen event name.


The following rowset was returned by the C.4 SELECT. The rowset shows that database_id is the only one field on the checkpoint_begin event that can supply values for the histogram target.


```
Package-Name   Event-Name         Field-Name   Field-Description
------------   ----------         ----------   -----------------
sqlserver      checkpoint_begin   database_id  NULL
sqlserver      checkpoint_end     database_id  NULL
```


<a name="h2_target_pair_matching"></a>

## pair_matching target


The pair_matching target enables you to detect start events that occurs without a corresponding end event. For instance, it might be a problem when a lock_acquired event occurs but no matching lock_released event follows in a timely manner.


The system does not automatically match start and end events. Instead, you explain the matching to the system in your CREATE EVENT SESSION statement. When a start and end event are matched, the pair is discarded so everyone can focus on the unmatched start events.


#### Finding matchable fields for the start and end event pair


By using the [C.4 SELECT](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_4_data_fields), we see in the following rowset there are about 16 fields for the lock_acquired event. The rowset displayed here has been manually split to show which fields our example matched on. Some fields would be silly to try matching, such as on the **duration** of both events.


```
Package-Name   Event-Name   Field-Name               Field-Description
------------   ----------   ----------               -----------------
sqlserver   lock_acquired   database_name            NULL
sqlserver   lock_acquired   mode                     NULL
sqlserver   lock_acquired   resource_0               The ID of the locked object, when lock_resource_type is OBJECT.
sqlserver   lock_acquired   resource_1               NULL
sqlserver   lock_acquired   resource_2               The ID of the lock partition, when lock_resource_type is OBJECT, and resource_1 is 0.
sqlserver   lock_acquired   transaction_id           NULL

sqlserver   lock_acquired   associated_object_id     The ID of the object that requested the lock that was acquired.
sqlserver   lock_acquired   database_id              NULL
sqlserver   lock_acquired   duration                 The time (in microseconds) between when the lock was requested and when it was canceled.
sqlserver   lock_acquired   lockspace_nest_id        NULL
sqlserver   lock_acquired   lockspace_sub_id         NULL
sqlserver   lock_acquired   lockspace_workspace_id   NULL
sqlserver   lock_acquired   object_id                The ID of the locked object, when lock_resource_type is OBJECT. For other lock resource types it will be 0
sqlserver   lock_acquired   owner_type               NULL
sqlserver   lock_acquired   resource_description     The description of the lock resource. The description depends on the type of lock. This is the same value as the resource_description column in the sys.dm_tran_locks view.
sqlserver   lock_acquired   resource_type            NULL
```


### Example of pair_matching


The following CREATE EVENT SESSION statement specifies two events, and two targets. The pair_matching target specifies two sets of fields to match the events into pairs. The sequence of comma-delimited fields assigned to **begin_matching_columns=** and **end_matching_columns=** must be the same. No tabs or newlines are allowed between the fields mentioned in the comma-delimited value, although spaces are okay.

To narrow the results, we first SELECTed from sys.objects to find the object_id of our test table. We added a filter for that one ID to the EVENT...WHERE clause.


```sql
CREATE EVENT SESSION [pair_matching_lock_a_r_33]
	ON SERVER 
	ADD EVENT sqlserver.lock_acquired
	(
		SET
			collect_database_name = (1),
			collect_resource_description = (1)

		ACTION (sqlserver.transaction_id)

		WHERE
		(
			[database_name] = 'InMemTest2'
			AND
			[object_id] = 370100359
		)
	),
	ADD EVENT sqlserver.lock_released
	(
		SET
			collect_database_name = (1),
			collect_resource_description = (1)

		ACTION (sqlserver.transaction_id)

		WHERE
		(
			[database_name] = 'InMemTest2'
			AND
			[object_id] = 370100359
		)
	)
	ADD TARGET package0.event_counter,
	ADD TARGET package0.pair_matching
	(
		SET
			begin_event = N'sqlserver.lock_acquired',
			begin_matching_columns =
				N'resource_0, resource_1, resource_2, transaction_id, database_id',

			end_event = N'sqlserver.lock_released',
			end_matching_columns =
				N'resource_0, resource_1, resource_2, transaction_id, database_id',

			respond_to_memory_pressure = (1)
	)
	WITH
	(
		MAX_MEMORY = 8192 KB,
		MAX_DISPATCH_LATENCY = 15 SECONDS
	);
```


To test the event session, we purposely prevented to acquired locks from being released. We did this with the following T-SQL steps:

1. BEGIN TRANSACTION.
2. UPDATE MyTable....
3. Purposely not issue a COMMIT TRANSACTION, until after we examined the targets.
4. Later after testing, we issued a COMMIT TRANSACTION.


The simple **event_counter** target provided the following output rows. Because 52-50=2, the output tells us we should see 2 unpaired lock_acquired events when we examine the output from the pair-matching target.


```
package_name   event_name      count
------------   ----------      -----
sqlserver      lock_acquired   52
sqlserver      lock_released   50
```


The **pair_matching** target provided the following output. As suggested by the event_counter output, we do indeed see 2 lock_acquired rows. The fact that we see these rows at all means these two lock_acquired events are unpaired.


```
package_name   event_name      timestamp                     database_name   duration   mode   object_id   owner_type   resource_0   resource_1   resource_2   resource_description   resource_type   transaction_id
------------   ----------      ---------                     -------------   --------   ----   ---------   ----------   ----------   ----------   ----------   --------------------   -------------   --------------
sqlserver      lock_acquired   2016-08-05 12:45:47.9980000   InMemTest2      0          S      370100359   Transaction  370100359    3            0            [INDEX_OPERATION]      OBJECT          34126
sqlserver      lock_acquired   2016-08-05 12:45:47.9980000   InMemTest2      0          IX     370100359   Transaction  370100359    0            0                                   OBJECT          34126
```


The rows for the unpaired lock_acquired events could include the T-SQL text, or **sqlserver.sql_text**, that took the locks. But we did not want to bloat the display.


<a name="h2_target_ring_buffer"></a>

## ring_buffer target


The ring_buffer target is handy for quick and simple event testing. When you stop the event session, the stored output is discarded.

In this ring_buffer section we also show how you can use the Transact-SQL implementation of XQuery to copy the XML contents of the ring_buffer into a more readable relational rowset.


#### CREATE EVENT SESSION with ring_buffer


There is nothing special about this CREATE EVENT SESSION statement, which uses the ring_buffer target.


```sql
CREATE EVENT SESSION [ring_buffer_lock_acquired_4]
	ON SERVER 
	ADD EVENT sqlserver.lock_acquired
	(
		SET collect_resource_description=(1)

		ACTION(sqlserver.database_name)

		WHERE
		(
			[object_id]=(370100359)  -- ID of MyTable
			AND
			sqlserver.database_name='InMemTest2'
		)
	)
	ADD TARGET package0.ring_buffer
	(
		SET max_events_limit=(98)
	)
	WITH
	(
		MAX_MEMORY=4096 KB,
		MAX_DISPATCH_LATENCY=3 SECONDS
	);
```


### XML output received for lock_acquired by ring_buffer


When retrieved by a SELECT statement, the content is in the form of a string of XML. The XML string that was stored by the ring_buffer target in our testing, is shown next. However, for brevity of the following XML display, all but two &#x3c;event&#x3e; elements have been erased. Further, within each &#x3c;event&#x3e;, a handful of extraneous &#x3c;data&#x3e; elements have been deleted.


```xml
<RingBufferTarget truncated="0" processingTime="0" totalEventsProcessed="6" eventCount="6" droppedCount="0" memoryUsed="1032">
  <event name="lock_acquired" package="sqlserver" timestamp="2016-08-05T23:59:53.987Z">
    <data name="mode">
      <type name="lock_mode" package="sqlserver"></type>
      <value>1</value>
      <text><![CDATA[SCH_S]]></text>
    </data>
    <data name="transaction_id">
      <type name="int64" package="package0"></type>
      <value>111030</value>
    </data>
    <data name="database_id">
      <type name="uint32" package="package0"></type>
      <value>5</value>
    </data>
    <data name="resource_0">
      <type name="uint32" package="package0"></type>
      <value>370100359</value>
    </data>
    <data name="resource_1">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="resource_2">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="database_name">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[]]></value>
    </data>
    <action name="database_name" package="sqlserver">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[InMemTest2]]></value>
    </action>
  </event>
  <event name="lock_acquired" package="sqlserver" timestamp="2016-08-05T23:59:56.012Z">
    <data name="mode">
      <type name="lock_mode" package="sqlserver"></type>
      <value>1</value>
      <text><![CDATA[SCH_S]]></text>
    </data>
    <data name="transaction_id">
      <type name="int64" package="package0"></type>
      <value>111039</value>
    </data>
    <data name="database_id">
      <type name="uint32" package="package0"></type>
      <value>5</value>
    </data>
    <data name="resource_0">
      <type name="uint32" package="package0"></type>
      <value>370100359</value>
    </data>
    <data name="resource_1">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="resource_2">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="database_name">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[]]></value>
    </data>
    <action name="database_name" package="sqlserver">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[InMemTest2]]></value>
    </action>
  </event>
</RingBufferTarget>
```


To see the preceding XML, you can issue the following SELECT while the event session is active. The active XML data is retrieved from the system view **sys.dm_xe_session_targets**.


```sql
SELECT
		CAST(LocksAcquired.TargetXml AS XML)  AS RBufXml,
	INTO
		#XmlAsTable
	FROM
		(
		SELECT
				CAST(t.target_data AS XML)  AS TargetXml
			FROM
				     sys.dm_xe_session_targets  AS t
				JOIN sys.dm_xe_sessions         AS s

					ON s.address = t.event_session_address
			WHERE
				t.target_name = 'ring_buffer'
				AND
				s.name        = 'ring_buffer_lock_acquired_4'
		)
			AS LocksAcquired;


SELECT * FROM #XmlAsTable;
```


### XQuery to see the XML as a rowset


To see the preceding XML as a relational rowset, continue from the preceding SELECT statement by issuing the following T-SQL. The commented lines explain each use of XQuery.


```sql
SELECT
		 -- (A)
		 ObjectLocks.value('(@timestamp)[1]',
			'datetime'     )  AS [OccurredDtTm]

		-- (B)
		,ObjectLocks.value('(data[@name="mode"]/text)[1]',
			'nvarchar(32)' )  AS [Mode]

		-- (C)
		,ObjectLocks.value('(data[@name="transaction_id"]/value)[1]',
			'bigint' )  AS [TxnId]

		-- (D)
		,ObjectLocks.value('(action[@name="database_name" and @package="sqlserver"]/value)[1]',
			'nvarchar(128)')  AS [DatabaseName]
	FROM
		#TableXmlCell
	CROSS APPLY
		-- (E)
		TargetDateAsXml.nodes('/RingBufferTarget/event[@name="lock_acquired"]')  AS T(ObjectLocks);
```


#### XQuery notes from preceding SELECT


(A)
- timestamp= attribute's value, on &#x3c;event&#x3e; element.
- The '(...)[1]' construct ensures only 1 value returned per iteration, as is a required limitation of the .value() XQuery method of XML data type variable and columns.


(B)
- &#x3c;text&#x3e; element's inner value, within a &#x3c;data&#x3e; element which has its name= attribute equal to "mode".


(C)
- &#x3c;value&#x3e; elements inner value, within a &#x3c;data&#x3e; element which has its name= attribute equal to "transaction_id".


(D)
- &#x3c;event&#x3e; contains &#x3c;action&#x3e;.
- &#x3c;action&#x3e; having name= attribute equal to "database_name", and package= attribute equal to "sqlserver" (not "package0"), get the inner value of &#x3c;value&#x3e; element.


(E)
- C.A. causes processing to repeat for every individual &#x3c;event&#x3e; element which has its name= attribute equal to "lock_acquired".
- This applies to the XML returned by the preceding FROM clause.


#### Output from XQuery SELECT


Next is the rowset generated by the preceding T-SQL which includes XQuery.


```
OccurredDtTm              Mode    DatabaseName
------------              ----    ------------
2016-08-05 23:59:53.987   SCH_S   InMemTest2
2016-08-05 23:59:56.013   SCH_S   InMemTest2
```



## XEvent .NET namespaces and C&#x23;


Package0 has two more targets, but they cannot be used in Transact-SQL:

- compressed_history
- event_stream


One way we know those two targets cannot be used in T-SQL is that their non-null values in the column *sys.dm_xe_objects.capabilities* do not include the bit 0x1.


The event_stream target can be used in .NET programs written in languages like C#. C# and other .NET developers can access an event stream through a .NET Framework class, such as in namespace Microsoft.SqlServer.XEvents.Linq.

If encountered, error **25726** means the event stream filled up with data faster than the client could consume the data. This caused the database engine to disconnect from the event stream to avoid slowing the performance of the server.


### XEvent namespaces


- [Microsoft.SqlServer.Management.XEvent Namespace](https://msdn.microsoft.com/library/microsoft.sqlserver.management.xevent.aspx)

- [Microsoft.SqlServer.XEvent.Linq Namespace](https://msdn.microsoft.com/library/microsoft.sqlserver.xevent.linq.aspx)



