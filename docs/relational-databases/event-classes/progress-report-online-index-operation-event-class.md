---
title: "Progress Report: Online Index Operation Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Progress Report: Online Index Operation event class [SQL Server]"
ms.assetid: 491616c1-f666-4b16-a5ea-1192bf156692
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Progress Report: Online Index Operation Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The Progress Report: Online Index Operation event class indicates the progress of an online index build operation while the build process is running.  
  
## Progress Report: Online Index Operation Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BigintData1|**bigint**|Number of rows inserted.|52|Yes|  
|BigintData2|**bigint**|0 = serial plan; otherwise, the thread ID during parallel execution.|53|Yes|  
|ClientProcessID|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|Duration|**bigint**|Amount of time (in microseconds) taken by the event.|13|Yes|  
|EndTime|**datetime**|Time at which the online index operation completed.|15|Yes|  
|EventClass|**int**|Type of event = 190.|27|No|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|EventSubClass|**int**|Type of event subclass.<br /><br /> 1=Start<br /><br /> 2=Stage 1 execution begin<br /><br /> 3=Stage 1 execution end<br /><br /> 4=Stage 2 execution begin<br /><br /> 5=Stage 2 execution end<br /><br /> 6=Inserted row count<br /><br /> 7=Done<br /><br /> Stage 1 refers to the base object (clustered index or heap), or if the index operation involves one non-clustered index only. Stage 2 is used when an index build operation involves both the original rebuild plus additional non-clustered indexes.  For example, if an object has a clustered index and several non-clustered indexes, 'rebuild all' would rebuild all indexes. The base object (the clustered index) is rebuilt in stage 1, and then all the non-clustered indexes are rebuilt in stage 2.|21|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IndexID|**int**|ID for the index on the object affected by the event.|24|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LoginName|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|**image**|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|Windows user name.|6|Yes|  
|ObjectID|**int**|System-assigned ID of the object.|22|Yes|  
|ObjectName|**nvarchar**|Name of the object being referenced.|34|Yes|  
|PartitionId|**bigint**|The ID of the partition being built.|65|Yes|  
|PartitionNumber|**int**|The ordinary number of the partition being built.|25|Yes|  
|ServerName|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|StartTime|**datetime**|Time at which the event started.|14|Yes|  
|TransactionID|**bigint**|System-assigned ID of the transaction.|4|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
