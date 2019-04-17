---
title: "SQL Server, General Statistics Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:General Statistics"
  - "General Statistics object"
ms.assetid: c738e549-d7e7-4211-9ec3-064ac140af7c
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, General Statistics Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **SQLServer:General Statistics** object in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor general server-wide activity, such as the number of current connections and the number of users connecting and disconnecting per second from computers running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This can be useful when you are working on large online transaction processing (OLTP) type systems where there are many clients connecting and disconnecting from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **General Statistics** counters.  
  
|SQL Server General Statistics counters|Description|  
|--------------------------------------------|-----------------|  
|**Active Temp Tables**|Number of temporary tables/table variables in use.|  
|**Connection resets/sec**|Total number of logins started from the connection pool.|  
|**Event Notifications Delayed Drop**|Number of event notifications waiting to be dropped by a system thread.|  
|**HTTP Authenticated Requests**|Number of authenticated HTTP requests started per second.|  
|**Logical Connections**|Number of logical connections to the system.<br /><br /> The main purpose of logical connections is to service multiple active result sets (MARS) requests. For MARS requests, every time that an application makes a connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there may be more than one logical connection that corresponds to a physical connection.<br /><br /> When MARS is not used, the ratio between physical and logical connections is 1:1. Therefore, every time that an application makes a connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], logical connections will increase by 1.|  
|**Logins/sec**|Total number of logins started per second. This does not include pooled connections.|  
|**Logouts/sec**|Total number of logout operations started per second.|  
|**Mars Deadlocks**|Number of MARS deadlocks detected.|  
|**Non-atomic yield rate**|Number of non-atomic yields per second.|  
|**Processes blocked**|Number of currently blocked processes.|  
|**SOAP Empty Requests**|Number of empty SOAP requests started per second.|  
|**SOAP Method Invocations**|Number of SOAP method invocations started per second.|  
|**SOAP Session Initiate Requests**|Number of SOAP Session initiate requests started per second.|  
|**SOAP Session Terminate Requests**|Number of SOAP Session terminate requests started per second.|  
|**SOAP SQL Requests**|Number of SOAP SQL requests started per second.|  
|**SOAP WSDL Requests**|Number of SOAP Web Service Description Language requests started per second.|  
|**SQL Trace IO Provider Lock Waits**|Number of waits for the File IO Provider lock per second.| 
|**Temp Tables Creation Rate**|Number of temporary tables/table variables created per second.|  
|**Temp Tables For Destruction**|Number of temporary tables/table variables waiting to be destroyed by the cleanup system thread.|  
|**Tempdb recovery unit id**|Number of duplicate tempdb recovery unit id generated.|
|**Tempdb rowset id**|Number of duplicate tempdb rowset id generated.| 
|**Trace Event Notifications Queue**|Number of trace event notification instances waiting in the internal queue to be sent through Service Broker.|  
|**Transactions**|Number of transaction enlistments (local, DTC, bound all combined).|  
|**User Connections**|Counts the number of users currently connected to SQL Server.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
