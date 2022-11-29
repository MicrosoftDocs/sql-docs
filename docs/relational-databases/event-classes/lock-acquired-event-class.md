---
description: "Lock:Acquired Event Class"
title: "Lock:Acquired Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Acquired event class"
ms.assetid: a6b1df2a-06ed-4fc3-8a84-f0becd5810d5
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Lock:Acquired Event Class
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The Lock:Acquiredevent class indicates that acquisition of a lock on a resource, such as a data page, has been achieved.  
  
 The Lock:Acquired and Lock:Released event classes can be used to monitor when objects are being locked, the type of locks taken, and for how long the locks were retained. Locks retained for long periods of time may cause contention issues and should be investigated. For example, an application can be acquiring locks on rows in a table, and then waiting for user input. Because the user input can take a long time to occur, the locks can block other users. In this instance, the application should be redesigned to make lock requests only when needed and not require user input when locks have been acquired.  
  
## Lock:Acquired Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BigintData1|**bigint**|Partition ID if the lock resource is partitioned.|52|Yes|  
|BinaryData|**image**|Lock resource identifier.|2|Yes|  
|ClientProcessID|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|**int**|ID of the database in which the lock was acquired. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|Duration|**bigint**|Amount of time (in microseconds) between the time the lock request was issued and the time the lock was acquired.|13|Yes|  
|EndTime|**datetime**|Time at which the event ended.|15|Yes|  
|EventClass|**int**|Type of event = 24.|27|No|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData2|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|55|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LoginName|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|**image**|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|Mode|**int**|Resulting mode after the lock was acquired.<br /><br /> 0=NULL - Compatible with all other lock modes (LCK_M_NL)<br /><br /> 1=Schema Stability lock (LCK_M_SCH_S)<br /><br /> 2=Schema Modification Lock (LCK_M_SCH_M)<br /><br /> 3=Shared Lock (LCK_M_S)<br /><br /> 4=Update Lock (LCK_M_U)<br /><br /> 5=Exclusive Lock (LCK_M_X)<br /><br /> 6=Intent Shared Lock (LCK_M_IS)<br /><br /> 7=Intent Update Lock (LCK_M_IU)<br /><br /> 8=Intent Exclusive Lock (LCK_M_IX)<br /><br /> 9=Shared with intent to Update (LCK_M_SIU)<br /><br /> 10=Shared with Intent Exclusive (LCK_M_SIX)<br /><br /> 11=Update with Intent Exclusive (LCK_M_UIX)<br /><br /> 12=Bulk Update Lock (LCK_M_BU)<br /><br /> 13=Key range Shared/Shared (LCK_M_RS_S)<br /><br /> 14=Key range Shared/Update (LCK_M_RS_U)<br /><br /> 15=Key Range Insert NULL (LCK_M_RI_NL)<br /><br /> 16=Key Range Insert Shared (LCK_M_RI_S)<br /><br /> 17=Key Range Insert Update (LCK_M_RI_U)<br /><br /> 18=Key Range Insert Exclusive (LCK_M_RI_X)<br /><br /> 19=Key Range Exclusive Shared (LCK_M_RX_S)<br /><br /> 20=Key Range Exclusive Update (LCK_M_RX_U)<br /><br /> 21=Key Range Exclusive Exclusive (LCK_M_RX_X)|32|Yes|  
|NTDomainName|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|Windows user name.|6|Yes|  
|ObjectID|**int**|ID of the object on which the lock was acquired, if available and applicable.|22|Yes|  
|ObjectID2|**bigint**|ID of the related object or entity, if available and applicable.|56|Yes|  
|OwnerID|**int**|1=TRANSACTION<br /><br /> 2=CURSOR<br /><br /> 3=SESSION<br /><br /> 4=SHARED_TRANSACTION_WORKSPACE<br /><br /> 5=EXCLUSIVE_TRANSACTION_WORKSPACE|58|Yes|  
|RequestID|**int**|ID of the request containing the statement.|49|Yes|  
|ServerName|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|Text value dependent on the lock type that was acquired. This is the same value as the resource_description column in sys.dm_tran_locks.|1|Yes|  
|TransactionID|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|Type|**int**|1=NULL_RESOURCE<br /><br /> 2=DATABASE<br /><br /> 3=FILE<br /><br /> 5=OBJECT<br /><br /> 6=PAGE<br /><br /> 7=KEY<br /><br /> 8=EXTENT<br /><br /> 9=RID<br /><br /> 10=APPLICATION<br /><br /> 11=METADATA<br /><br /> 12=AUTONAMEDB<br /><br /> 13=HOBT<br /><br /> 14=ALLOCATION_UNIT|57|Yes|  
  
## See Also  
 [Lock:Released Event Class](../../relational-databases/event-classes/lock-released-event-class.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)  
  
  
