---
description: "Exchange Spill Event Class"
title: "Exchange Spill Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Exchange Spill event class"
ms.assetid: fb876cec-f88d-4975-b3fd-0fb85dc0a7ff
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Exchange Spill Event Class
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The **Exchange Spill** event class indicates that communication buffers in a parallel query plan have been temporarily written to the **tempdb** database. This occurs rarely and only when a query plan has multiple range scans.  
  
 Normally, the [!INCLUDE[tsql](../../includes/tsql-md.md)] query that generates such range scans has many BETWEEN operators, each of which selects a range of rows from a table or an index. Alternatively, you can obtain multiple ranges using expressions such as (T.a > 10 AND T.a < 20) OR (T.a > 100 AND T.a < 120). Additionally, the query plans must require that these ranges be scanned in order either because there is an ORDER BY clause on T.a, or because an iterator within the plan requires that it consume the tuples in sorted order.  
  
 When a query plan for such a query has multiple **Parallelism** operators, the memory communication buffers used by the **Parallelism** operators become full, and a situation can arise whereby the query's execution progress stops. In this situation, one of the **Parallelism** operators writes its output buffer to **tempdb** (an operation called an *exchange spill*) so that it can consume rows from some of its input buffers. Eventually, the spilled rows are returned to the consumer when the consumer is ready to consume them.  
  
 Very rarely, multiple exchange spills can occur within the same execution plan, causing the query to execute slowly. If you notice more than five spills within the same query plan's execution, contact your support professional.  
  
 Exchange spills are sometimes transient and may disappear as data distribution changes.  
  
 There are several ways to avoid exchange spill events:  
  
-   Omit the ORDER BY clause if you do not need the result set to be ordered.  
  
-   If ORDER BY is required, eliminate the column that participates in the multiple range scans (T.a in the example above) from the ORDER BY clause.  
  
-   Using an index hint, force the optimizer to use a different access path on the table in question.  
  
-   Rewrite the query to produce a different query execution plan.  
  
-   Force serial execution of the query by adding the MAXDOP = 1 option to the end of the query or index operation. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) and [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
> [!IMPORTANT]  
>  To determine where the **Exchange Spill** event is occurring when the query optimizer generates an execution plan, you should also collect a Showplan event class in the trace. You can choose any of the Showplan event classes except the **Showplan Text** and **Showplan Text (Unencoded)** event classes, which do not return a Node ID. Node IDs in Showplans identify each operation the query optimizer performs when it generates a query execution plan. These operations are called operators and each operator in a Showplan has a Node ID. The **ObjectID** column for **Exchange Spill** events corresponds to the Node ID in Showplans so you can determine which operator, or operation, is causing the error.  
  
## Exchange Spill Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**ApplicationName**|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**ClientProcessID**|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|**DatabaseID**|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**DatabaseName**|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|**EventClass**|**int**|Type of event = 127.|27|No|  
|**EventSequence**|**int**|Sequence of a given event within the request.|51|No|  
|**EventSubClass**|**int**|Type of event subclass.<br /><br /> 1=Spill begin<br /><br /> 2=Spill end|21|Yes|  
|**GroupID**|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|**HostName**|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**LoginName**|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the Windows login credentials in the form of *\<DOMAIN>\\<username\>*).|11|Yes|  
|**LoginSid**|**image**|Security identification number (SID) of the logged-in user. You can find this information in the **syslogins** table of the **master** database. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|Windows user name.|6|Yes|  
|**ObjectID**|**int**|System-assigned ID of the object. Corresponds with the Node ID in Showplans.|22|Yes|  
|**RequestID**|**int**|ID of the request containing the statement.|49|Yes|  
|**ServerName**|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SessionLoginName**|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**TransactionID**|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|**XactSequence**|**bigint**|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [Set Index Options](../../relational-databases/indexes/set-index-options.md)   
 [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)  
  
  
