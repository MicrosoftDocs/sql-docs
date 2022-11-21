---
title: "PolyBase errors and possible solutions"
description: PolyBase reference for errors and suggested solutions.
ms.date: 03/22/2021
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
dev_langs: 
-  "TSQL"
-  "XML"
f1_keywords: 
   - "PolyBase, monitoring"
   - "PolyBase, performance monitoring"
helpviewer_keywords: 
   - "PolyBase, troubleshooting"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-2016"
---
# PolyBase errors and possible solutions

[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article contains common error scenarios and solutions for PolyBase.

For more on monitoring and troubleshooting PolyBase, see [Monitor and troubleshoot PolyBase](polybase-troubleshooting.md).

For common PolyBase log file locations in Windows and Linux, see [Monitor and troubleshoot PolyBase](polybase-troubleshooting.md#log-file-locations).


## Error messages and possible solutions

### Error: "100001;Failed to generate query plan"

The "Failed to generate query plan" error can occur when the SQL Server database engine has been patched to at least Cumulative Update 8 (15.0.4073) but the PolyBase feature hasn't been updated to the same build. This can occur when adding the PolyBase feature to an existing SQL Server instance. For more information, see [PolyBase error - 100001;Failed to generate query plan](https://techcommunity.microsoft.com/t5/sql-server-support/polybase-error-100001-failed-to-generate-query-plan/ba-p/2174693).

Always follow up installing the PolyBase feature by bringing the new feature up to the same version level. Install service packs (SPs), cumulative updates (CUs), and/or general distribution releases (GDRs) as needed. To determine the version of PolyBase, see [Determine the version, edition, and update level of SQL Server and its components](/troubleshoot/sql/general/determine-version-edition-update-level#polybase).

### Service account change

Example error message:

> 107035;Dms authorization failed due to [DOMAIN\user] is not member of group [PdwDataMovementAccess] <BR>
> 107017;Invalid DMS control header:

This error is likely due to changing the PolyBase service account. To change the service accounts for the PolyBase Engine and PolyBase Data Movement service, uninstall and reinstall the PolyBase feature.


### Data movement service permissions errors

For more information on troubleshooting and resolving permissions issues with the data movement service, see [PolyBase Service Account Permissions and Common Errors Observed When They Are Missing](https://techcommunity.microsoft.com/t5/sql-server-support/polybase-service-account-permissions-and-common-errors-observed/ba-p/2112711).


### Windows authentication failure

For more information on troubleshooting and resolving permissions issues related to a failure in Windows authentication, see [PolyBase Service Account Permissions and Common Errors Observed When They Are Missing](https://techcommunity.microsoft.com/t5/sql-server-support/polybase-service-account-permissions-and-common-errors-observed/ba-p/2112711).


### Cannot execute the query "Remote Query" 

Example error message:

> Msg 7320, Level 16, State 110, Line 14<BR>
> Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". Query aborted-- the maximum reject threshold (0 rows) was reached while reading from an external source: 1 rows rejected out of total 1 rows processed.
(/nation/sensors.ldjson.txt)Column ordinal: 0, Expected data type: INT, Offending value: {"id":"S2740036465E2B","time":"2016-02-26T16:59:02.9300000Z","temp":23.3,"hum":0.77,"wind":17,"press":1032,"loc":[-76.90914996169623,38.8929314364726]} (Column Conversion Error), Error: Error converting data type NVARCHAR to INT.

Keep in mind there may be derivations of this error. The name of the first rejected file shows in SQL Server Management Studio (SSMS) with offending data types or values.

**Possible Reason:**  
The reason this error happens is because each file has different schema. The PolyBase external table DDL when pointed to a directory recursively reads all the files in that directory. When a column or data type mismatch happens, this error could be seen in SSMS.

**Possible Solution:**  
If the data for each table consists of one file, then use the filename in the LOCATION section prefixed by the directory of the external files. 
If there are multiple files per table, put each set of files into different directories in Azure Blob Storage. Point LOCATION to the directory instead of a particular file. This solution is recommended.

**Example:**  

```syntaxsql
Create External Table foo
(col1 int)WITH (LOCATION='/bar/foobar.txt',DATA_SOURCE…); OR
Create External Table foo
(col1 int) WITH (LOCATION = '/bar/', DATA_SOURCE…);
```

> [!NOTE] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)]

### Kerberos support

SQL Server is configured to access a supported Hadoop Cluster. Kerberos security is not enforced in Hadoop Cluster.

Selecting from the external table returns the following error:

> Msg 105019, Level 16, State 1, Line 55<BR>
> EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect: Error [Unable to instantiate LoginClass] occurred while accessing external file.'<BR>
> Msg 7320, Level 16, State 110, Line 55<BR>
> Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect: Error [Unable to instantiate LoginClass] occurred while accessing external file.'

Interrogation of DWEngine Server log shows the following error:

> [Thread:16432] [EngineInstrumentation:EngineQueryErrorEvent] (Error, High):<BR>
> EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect: Error [com.microsoft.polybase.client.KerberosSecureLogin] occurred while accessing external file.'
Microsoft.SqlServer.DataWarehouse.Common.ErrorHandling.MppSqlException: EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect: Error [com.microsoft.polybase.client.KerberosSecureLogin] occurred while accessing external file.' ---> Microsoft.SqlServer.DataWarehouse.DataMovement.Common.ExternalAccess.HdfsAccessException: Java exception raised on call to HdfsBridge_Connect: Error [com.microsoft.polybase.client.KerberosSecureLogin] occurred while accessing external file.

**Possible Reason:**  
Kerberos is not enabled in Hadoop Cluster, but Kerberos security is enabled in core-site.xml, yarn-site.xml, or the hdfs-site.xml located by default under Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\PolyBase\Hadoop\conf. In Linux, the files are by default located in /var/opt/mssql/binn/polybase/hadoop/conf/.

**Possible Solution:**  
Comment out the Kerberos security information from the above mentioned files.

For more information on troubleshooting PolyBase and Kerberos, see [Troubleshoot PolyBase Kerberos connectivity](polybase-troubleshoot-connectivity.md).

### Internal query processor error

Querying an external table returns the following error:

> Msg 8680, Level 17, State 5, Line 118<BR>
> Internal Query Processor Error: The query processor encountered an unexpected error during the processing of a remote query phase.

The DWEngine server log contains the following messages:

> [Thread:5216] [ControlNodeMessenger:ErrorEvent] (Error, High): ***** DMS System has disconnected nodes :<BR>
> [Thread:5216] [ControlNodeMessenger:ErrorEvent] (Error, High): ***** DMS System has disconnected nodes :<BR>
> [Thread:5216] [ControlNodeMessenger:ErrorEvent] (Error, High): ***** DMS System has disconnected nodes :<BR>

**Possible Reason:**  
The reason for this error could be that SQL Server was not restarted after configuring PolyBase.

**Possible Solution:**  
Restart SQL Server. Check DWEngine Server Log to confirm there are no DMS disconnections after restart.


### User needed for HDFS access

**Scenario:**  
SQL Server is connected to an unsecured Hadoop cluster (Kerberos is not enabled). PolyBase is configured to push computation to Hadoop cluster.

**Sample Query:**  

```sql
select count(*) from foo WITH (FORCE EXTERNALPUSHDOWN);
```

An error message similar to the following is returned: 

> Msg 105019, Level 16, State 1, Line 1<BR>
> EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to JobSubmitter_PollJobStatus: Error [java.net.ConnectException: Call From big1506sql2016/172.16.1.4 to 0.0.0.0:10020 failed on connection exception: java.net ConnectException: Connection refused: no further information; For more information, see:  http://wiki.apache.org/hadoop/ConnectionRefused] occurred while accessing external file.'<BR>
> OLE DB provider "SQLNCLI11" for linked server "(null)" returned message "Unspecified error".<BR>
> Msg 7421, Level 16, State 2, Line 1<BR>
> Cannot fetch the rowset from OLE DB provider "SQLNCLI11" for linked server "(null)". .<BR>

Hadoop Yarn Log Error:
> Job setup failed : org.apache.hadoop.security.AccessControlException: Permission denied: user=pdw_user, access=WRITE, inode="/user":hdfs:hdfs:drwxr-xr-x at org.apache.hadoop.hdfs.server.namenode.FSPermissionChecker.checkFsPermission(FSPermissionChecker.java:265) at org.apache.hadoop.hdfs.server.namenode.FSPermissionChecker.check(FSPermissionChecker.java:251) at org.apache.hadoop.hdfs.server.namenode.FSPermissionChecker.check(FSPermissionChecker.java:232) org.apache.hadoop.hdfs.server.namenode.FSPermissionChecker.checkPermission(FSPermissionChecker.java:176) at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.checkPermission(FSNamesystem.java:5525)

**Possible Reason:**  
With Kerberos disabled, PolyBase will use pdw_user as the user for accessing HDFS and submitting MapReduce jobs.

**Possible Solution:**  
Create pdw_user on Hadoop and give it sufficient permissions to the directories used during mapreduce processing. Also make sure that pdw_user is the owner of the /user/pdw_user HDFS directory.

Below is an example of how to create home directory and assign permissions for pdw_user:

```console
sudo -u hdfs hadoop fs -mkdir /user/pdw_user
sudo -u hdfs hadoop fs -chown pdw_user /user/pdw_user
```

After this make sure that pdw_user has read, write, and execute permissions on the /user/pdw_user directory. Make sure the /tmp directory has 777 permissions.

For more information on troubleshooting PolyBase and Kerberos, see [Troubleshoot PolyBase Kerberos connectivity](polybase-troubleshoot-connectivity.md).

### Java memory error due to UTF-8

**Scenario:**  
SQL Server PolyBase is set up with Hadoop Cluster or Azure Blob Storage. Any Select query fails with the following error:

> Msg 106000, Level 16, State 1, Line 1<BR>
> Java heap space<BR>

**Possible Reason:**  
Illegal input may cause the java out of memory error. The file may not be in UTF-8 format. DMS tries to read the whole file as one row since it cannot decode the row delimiter and runs into Java heap space error.

**Possible Solution:**  
Convert the file to UTF-8 format since PolyBase currently requires UTF-8 format for text-delimited files.

### Hadoop connectivity configuration

Configuring SQL Server PolyBase to connect to Azure Blob Storage returns the following error message in SQL Server:

> Msg 105019, Level 16, State 1, Line 74<BR>
> EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect: Error [No FileSystem for scheme: wasbs] occurred while accessing external file.'<BR>

**Possible Reason:**  
Hadoop connectivity is not set to the configuration value for accessing Azure Blob storage.

**Possible Solution:**  
Set the Hadoop connectivity to a value (preferably 7) which supports Azure Blob Storage and restart SQL Server. For a list of connectivity values and supported types, see [PolyBase Connectivity Configuration](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md#arguments).


### Create Table As Select error

**Scenario:**  
Trying to export data to Azure Blob Storage or Hadoop file system using PolyBase with CREATE EXTERNAL TABLE AS SELECT (CETAS) syntax from SQL Server fails with the following error message:

> Msg 156, Level 15, State 1, Line 177<BR>
> Incorrect syntax near the keyword 'WITH'.<BR>
> Msg 319, Level 15, State 1, Line 177<BR>
> Incorrect syntax near the keyword 'with'. If this statement is a common table expression, an xmlnamespaces clause or a change tracking context clause, the previous statement must be terminated with a semicolon.<BR>

**Possible Reason:**  
When exporting data to Hadoop or Azure Blob Storage via PolyBase, only the data is exported, not the column names (metadata) as defined in the CREATE EXTERNAL TABLE command.

**Possible Solution:**  
Create the external table first and then use INSERT INTO SELECT to export to the external location. For a code sample, see [PolyBase query scenarios](polybase-queries.md#export-data).


### Create External Table from Azure Blob Storage fails

**Scenario:**  
Dedicated SQL pool (formerly SQL DW) is set up to import data from Azure Blob Storage. Create external table fails with the following message.

> Msg 105019, Level 16, State 1, Line 34<BR>
> External TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_IsDirExist. Java exception message:com.microsoft.azure.storage.StorageException: Server failed to authenticate the request. Make sure the value of Authorization header is formed correctly including the signature.: Error [com.microsoft.azure.storage.StorageException: Server failed to authenticate the request. Make sure the value of Authorization header is formed correctly including the signature.] occurred while accessing external file.'<BR>

**Possible Reason:**  
Wrong Azure storage key was used to create the database scoped credential.

**Possible Solution:**  
Drop all related objects (i.e: data source, file format) and then drop and recreate the database scoped credential with the right storage key.


### Kerberos configuration capitalization

**Scenario:**  
SQL Server is set up with Kerberos enabled Cloudera Cluster. SQL Server has been restarted after all the configuration changes. PolyBase Engine and PolyBase Data Movement services are running after restart. The following error messages are returned:

Data Source configured without job tracker location:  
> org.apache.hadoop.fs.FileSystem: Provider org.apache.hadoop.fs.viewfs.ViewFileSystem could not be instantiated

Data Source configured with job tracker location:  
> Error [Can't get Kerberos realm] occurred while accessing external file

**Possible Reason:**  
The value for the "hadoop.security.authentication" property says kerberos in the Coresite.xml.

**Possible Solution:**  
Coresite.xml's "hadoop.security.authentication" property should be KERBEROS (all upper case) as the value. 

For more information on troubleshooting PolyBase and Kerberos, see [Troubleshoot PolyBase Kerberos connectivity](polybase-troubleshoot-connectivity.md).

### Mapred-site.xml missing needed values

**Scenario:**  
SQL Server or APS is set up with supported HDP Cluster. Queries that do not require pushdown work, but fails with the following message when 'FORCE PUSHDOWN' hint is used with the following error messages:

> Msg 7320, Level 16, State 110, Line 35<BR>
> Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to JobSubmitter_PollJobStatus: Error [org.apache.hadoop.ipc.RemoteException(java.lang.NullPointerException): java.lang.NullPointerException<BR>
> at org.apache.hadoop.mapreduce.v2.hs.HistoryClientService$HSClientProtocolHandler.getTaskAttemptCompletionEvents(HistoryClientService.java:277)<BR>
> at org.apache.hadoop.mapreduce.v2.api.impl.pb.service.MRClientProtocolPBServiceImpl.getTaskAttemptCompletionEvents(MRClientProtocolPBServiceImpl.java:173)<BR>
> at org.apache.hadoop.yarn.proto.MRClientProtocol$MRClientProtocolService$2.callBlockingMethod(MRClientProtocol.java:283)<BR>
> at org.apache.hadoop.ipc.ProtobufRpcEngine$Server$ProtoBufRpcInvoker.call(ProtobufRpcEngine.java:619)<BR>
> at org.apache.hadoop.ipc.RPC$Server.call(RPC.java:962)<BR>
> at org.apache.hadoop.ipc.Server$Handler$1.run(Server.java:2127)<BR>
> at org.apache.hadoop.ipc.Server$Handler$1.run(Server.java:2123)<BR>
> at java.security.AccessController.doPrivileged(Native Method)<BR>
> at javax.security.auth.Subject.doAs(Subject.java:415)<BR>
> at org.apache.hadoop.security.UserGroupInformation.doAs(UserGroupInformation.java:1671)<BR>
> at org.apache.hadoop.ipc.Server$Handler.run(Server.java:2121)<BR>
> ] occurred while accessing external file.'<BR>

**Possible Reason:**  
Mapred-site.xml is missing some needed values that checks for intermediate and end results.

**Possible Solution:**  
Add the following properties and associate the correct values as it shows on Ambari in the mapred-site.xml file on SQL Server. 

```xml
<property>
<name>yarn.app.mapreduce.am.staging-dir</name>
<value>/user</value>
</property>
<property>
<name>mapreduce.jobhistory.done-dir</name>
<value>/mr-history/done</value>
</property>
<property>
<name>mapreduce.jobhistory.intermediate-done-dir</name>
<value>/mr-history/tmp</value>
</property>
```

### Configuring access by hostname 

**Scenario:**  
SQL Server is set up to access a supported Hadoop Cluster. Creating an external table returns one of the following errors:

> Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110802;An internal DMS error occurred that caused this operation to fail. Details: Exception: Microsoft.SqlServer.DataWarehouse.DataMovement.Workers.DmsSqlNativeException, Message: SqlNativeBufferReader.Run, error in OdbcExecuteQuery: SqlState: 42000, NativeError: 8680, 'Error calling: SQLExecDirect(this->GetHstmt(), (SQLWCHAR *)statementText, SQL_NTS), SQL return code: -1 | SQL Error Info: SrvrMsgState: 26, SrvrSeverity: 17,  Error <1>: ErrorMsg: [Microsoft][ODBC Driver 13 for SQL Server][SQL Server]Internal Query Processor Error: The query processor encountered an unexpected error during the processing of a remote query phase. | Error calling: pReadConn->ExecuteQuery(statementText, bufferFormat) | state: FFFF, number: 24, active connections: 8', Connection String: Driver={pdwodbc};APP=RCSmall-DmsNativeReader:WAD1D16HD2001\mpdwsvc (3600)-ODBC-PoolId1433;Trusted_Connection=yes;AutoTranslate=no;Server=\\.\pipe\sql\query<BR>

> [Thread:30544] [AbstractReaderWorker:ErrorEvent] (Error, High): QueryId QID2433 PlanId 6c3a4551-e54c-4c06-a5ed-a8733edac691 StepId 7:<BR>
> Could not obtain block: BP-1726738607-192.168.225.121-1443123675290:blk_1159687047_86196509 file=/user/hive/warehouse/u_data/000000_0<BR>
> Microsoft.SqlServer.DataWarehouse.Common.ErrorHandling.MppSqlException: Could not obtain block: BP-1726738607-192.168.225.121-1443123675290:blk_1159687047_86196509 file=/user/hive/warehouse/u_data/000000_0<BR>
> at Microsoft.SqlServer.DataWarehouse.DataMovement.Common.ExternalAccess.HdfsBridgeReadAccess.Read(MemoryBuffer buffer, Boolean& isDone)<BR>
> at Microsoft.SqlServer.DataWarehouse.DataMovement.Workers.DataReader.ExternalMoveBufferReader.Read()<BR>
> at Microsoft.SqlServer.DataWarehouse.DataMovement.Workers.ExternalMoveReaderWorker.ReadAndSendData()<BR>
> at Microsoft.SqlServer.DataWarehouse.DataMovement.Workers.ExternalMoveReaderWorker.Execute(Object status)<BR>

**Possible Reason:**  
This error message can show up when the Hadoop Cluster is set up in a configuration where data nodes are only accessible outside the cluster using the Hostname and not the IP address.

**Possible Solution:**  
Add the following to hdfs-site.xml file on the client (SQL Server) side. This configuration will force the name node to return a URI for the data nodes with the Hostname instead of the internal IP address.

```xml
<property>
<name>dfs.client.use.datanode.hostname</name>
<value>true</value>
</property>
```

### Folder organization forces excess memory overhead

**Scenario:**  
SQL Server is running a PolyBase query on a directory with a large number of files (>30,000 files under the directory path recursively), and one of the following error messages is returned:

> Msg 105019, Level 16, State 1, Line 1<BR>
> EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_GetFileNameByIndex. Java exception message:
GC overhead limit exceeded: Error [GC overhead limit exceeded] occurred while accessing external file.'<BR>

> Msg 105019, Level 16, State 1, Line 1<BR>
> EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_GetDirectoryFiles. Java exception message:
Java heap space: Error [Java heap space] occurred while accessing external file.'

**Possible Reason:**  
When processing a path, PolyBase enumerates all files under that path, and there is fixed memory overhead associated with the data structure that is used to represent the files. With a large number of files, this overhead becomes noticeable and can eventually consume all memory available to the JVM.

**Possible Solution:**  
Rearrange the data in multiple directories so that each directory contains a subset of files and then break down the query in multiple ones that operate on a part of the original path at a time and materialize the tables as SQL Server tables (before joining them).

Example: Let's assume your external table data is in the following location:
Orders/file1.txt,...,file30K.txt.

Change the layout so the data is laid out in a conventional file partition structure in Orders/*yyyy*/*mm*/*dd*/file1.txt.
Point your external table to a lower directory path like month(mm) or day(dd) and import the files into SQL Server tables in pieces and then add them as part of one table.
Even if you had the right directory structure to begin with, follow step #2 to be able to work with that many files without running out of JVM memory.


### Unexpected characters in configuration files

**Scenario:**  
Setting up SQL Server or APS with a Hadoop cluster, which involves modifying yarn-site.xml, hdfs-site.xml, and other configuration files. The following SQL Server error message is observed:

> Msg 105019, Level 16, State 1, Line 1<BR>
> Microsoft.SqlServer.DataWarehouse.Common.ErrorHandling.MppSqlException: EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect. Java exception message:com.sun.org.apache.xerces.internal.impl.io.MalformedByteSequenceException: Invalid byte 1 of 1-byte UTF-8 sequence.: Error [com.sun.org.apache.xerces.internal.impl.io.MalformedByteSequenceException: Invalid byte 1 of 1-byte UTF-8 sequence.] occurred while accessing external file.' ---><BR>

**Possible Reason:**  
This can happen if you have copied and pasted text into configuration files from a website or chat window. It is possible that unwanted/unprintable characters are in the configuration files.

**Possible Solution:**  
Open the files in a different text editor (other than notepad) and look for these characters and eliminate them. Restart the necessary services. 


## See also

 - [Monitor and troubleshoot PolyBase](polybase-troubleshooting.md)  
 - [Troubleshoot PolyBase Kerberos connectivity](polybase-troubleshoot-connectivity.md)  
 - [PolyBase Frequently Asked Questions](polybase-faq.yml)

