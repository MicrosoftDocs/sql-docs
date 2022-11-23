---
description: "Lock:Deadlock Event Class"
title: "Lock:Deadlock Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Deadlock event class"
ms.assetid: 3e0394bc-6ea8-4533-845c-76782bec73c2
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Lock:Deadlock Event Class
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The Lock:Deadlock event class is produced when an attempt to acquire a lock is canceled because the attempt was part of a deadlock and was chosen as the deadlock victim.  
  
 Use the Lock:Deadlock event class to monitor when deadlocks occur and which objects are involved. You can use this information to determine if deadlocks are significantly affecting the performance of your application. You can then examine the application code to determine if you can make changes to minimize deadlocks.  
  
## Lock:Deadlock Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BinaryData|**image**|Lock resource identifier.|2|Yes|  
|ClientProcessID|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|**int**|ID of the database in which the lock was being acquired. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|Name of the database in which the lock was being acquired.|35|Yes|  
|Duration|**bigint**|Amount of time (in microseconds) between the time the lock request was issued and the time the deadlock occurred.|13|Yes|  
|EndTime|**datetime**|Time at which the deadlock ended.|15|Yes|  
|EventClass|**int**|Type of event = 25.|27|No|  
|EventSequence|**int**|The sequence of a given event within the request.|51|No|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData|**int**|Deadlock number. Numbers are assigned beginning with 0 when the server is started and are incremented for each deadlock.|25|Yes|  
|IntegerData2|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|55|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LoginName|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|**image**|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|Mode|**int**|The resulting mode after the deadlock.<br /><br /> 0=NULL - Compatible with all other lock modes (LCK_M_NL)<br /><br /> 1=Schema Stability lock (LCK_M_SCH_S)<br /><br /> 2=Schema Modification Lock (LCK_M_SCH_M)<br /><br /> 3=Shared Lock (LCK_M_S)<br /><br /> 4=Update Lock (LCK_M_U)<br /><br /> 5=Exclusive Lock (LCK_M_X)<br /><br /> 6=Intent Shared Lock (LCK_M_IS)<br /><br /> 7=Intent Update Lock (LCK_M_IU)<br /><br /> 8=Intent Exclusive Lock (LCK_M_IX)<br /><br /> 9=Shared with intent to Update (LCK_M_SIU)<br /><br /> 10=Shared with Intent Exclusive (LCK_M_SIX)<br /><br /> 11=Update with Intent Exclusive (LCK_M_UIX)<br /><br /> 12=Bulk Update Lock (LCK_M_BU)<br /><br /> 13=Key range Shared/Shared (LCK_M_RS_S)<br /><br /> 14=Key range Shared/Update (LCK_M_RS_U)<br /><br /> 15=Key Range Insert NULL (LCK_M_RI_NL)<br /><br /> 16=Key Range Insert Shared (LCK_M_RI_S)<br /><br /> 17=Key Range Insert Update (LCK_M_RI_U)<br /><br /> 18=Key Range Insert Exclusive (LCK_M_RI_X)<br /><br /> 19=Key Range Exclusive Shared (LCK_M_RX_S)<br /><br /> 20=Key Range Exclusive Update (LCK_M_RX_U)<br /><br /> 21=Key Range Exclusive Exclusive (LCK_M_RX_X)|32|Yes|  
|NTDomainName|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|Windows user name.|6|Yes|  
|ObjectID|**int**|ID of the object in contention, if available and applicable.|22|Yes|  
|ObjectID2|**bigint**|ID of the related object or entity, if available and applicable.|56|Yes|  
|OwnerID|**int**|1=TRANSACTION<br /><br /> 2=CURSOR<br /><br /> 3=SESSION<br /><br /> 4=SHARED_TRANSACTION_WORKSPACE<br /><br /> 5=EXCLUSIVE_TRANSACTION_WORKSPACE|58|Yes|  
|RequestID|**int**|The ID of the request containing the statement.|49|Yes|  
|ServerName|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|StartTime|**datetime**|Time at which the event started, when available.|14|Yes|  
|TextData|**ntext**|Text value dependent on the lock type that was being acquired.|1|Yes|  
|TransactionID|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|Type|**int**|1=NULL_RESOURCE<br /><br /> 2=DATABASE<br /><br /> 3=FILE<br /><br /> 5=OBJECT<br /><br /> 6=PAGE<br /><br /> 7=KEY<br /><br /> 8=EXTENT<br /><br /> 9=RID<br /><br /> 10=APPLICATION<br /><br /> 11=METADATA<br /><br /> 12=AUTONAMEDB<br /><br /> 13=HOBT<br /><br /> 14=ALLOCATION_UNIT|57|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)  
  
  
