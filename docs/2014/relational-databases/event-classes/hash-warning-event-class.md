---
title: "Hash Warning Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Hash Warning event class"
ms.assetid: cb93c620-4be9-4362-8bf0-af3f2048bdaf
author: stevestein
ms.author: sstein
manager: craigg
---
# Hash Warning Event Class
  The Hash Warning event class can be used to monitor when a hash recursion or cessation of hashing (hash bailout) has occurred during a hashing operation.  
  
 Hash recursion occurs when the build input does not fit into available memory, resulting in the split of input into multiple partitions that are processed separately. If any of these partitions still do not fit into available memory, it is split into subpartitions, which are also processed separately. This splitting process continues until each partition fits into available memory or until the maximum recursion level is reached (displayed in the IntegerData data column).  
  
 Hash bailout occurs when a hashing operation reaches its maximum recursion level and shifts to an alternate plan to process the remaining partitioned data. Hash bailout usually occurs because of skewed data.  
  
 Hash recursion and hash bailout cause reduced performance in your server. To eliminate or reduce the frequency of hash recursion and bailouts, do one of the following:  
  
-   Make sure that statistics exist on the columns that are being joined or grouped.  
  
-   If statistics exist on the columns, update them.  
  
-   Use a different type of join. For example, use a MERGE or LOOP join instead, if appropriate.  
  
-   Increase available memory on the computer. Hash recursion or bailout occurs when there is not enough memory to process queries in place and they need to spill to disk.  
  
 Creating or updating the statistics on the column involved in the join is the most effective way to reduce the number of hash recursion or bailouts that occur.  
  
> [!NOTE]  
>  The terms *grace hash join* and *recursive hash join* are also used to describe hash bailout.  
  
> [!IMPORTANT]  
>  To determine where the Hash Warning event is occurring when the query optimizer generates an execution plan, you should also collect a Showplan event class in the trace. You can choose any of the Showplan event classes except the Showplan Text and Showplan Text (Unencoded) event classes, which do not return a Node ID. Node IDs in Showplans identify each operation the query optimizer performs when it generates a query execution plan. These operations are called *operators*, and each operator in a Showplan has a Node ID. The ObjectID column for Hash Warning events corresponds to the Node ID in Showplans so you can determine which operator, or operation, is causing the error.  
  
## Hash Warning Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|`nvarchar`|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than with the displayed name of the program.|10|Yes|  
|ClientProcessID|`int`|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides a client process ID.|9|Yes|  
|DatabaseID|`int`|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|Name of the database in which the user statement is running.|35|Yes|  
|EventClass|`int`|Type of event = 55.|27|No|  
|EventSequence|`int`|Sequence of a given event within the request.|51|No|  
|EventSubClass|`int`|Type of event subclass.<br /><br /> 0=Recursion<br /><br /> 1=Bailout|21|Yes|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|`nvarchar`|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData|`int`|Recursion level (hash recursion only).|25|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LoginName|`nvarchar`|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the Windows login credentials in the form of *\<DOMAIN>\\<username\>*).|11|Yes|  
|LoginSid|`image`|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|Windows user name.|6|Yes|  
|ObjectID|`int`|Node ID of the root of the hash team involved in the repartition. Corresponds with the Node ID in Showplans.|22|Yes|  
|RequestID|`int`|ID of the request that contains the statement.|49|Yes|  
|ServerName|`nvarchar`|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is being traced.|26||  
|SessionLoginName|`nvarchar`|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the event started, if available.|14|Yes|  
|TransactionID|`bigint`|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|`bigint`|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
  
