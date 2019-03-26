---
title: "QN:Subscription Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "event classes [SQL Server], QN:Subscription"
ms.assetid: 4916167e-8541-43b4-900e-ec8e6adcbc34
author: stevestein
ms.author: sstein
manager: craigg
---
# QN:Subscription Event Class
  The QN:Subscription event reports information on notification subscriptions.  
  
## QN:Subscription Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|ApplicationName|`nvarchar`|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|`int`|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|`int`|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the Server Name data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|The name of the database in which the user statement is running.|35|Yes|  
|EventClass|`int`|Type of event = 199.|27|No|  
|EventSequence|`int`|Sequence number for this event.|51|No|  
|EventSubClass|`nvarchar`|The type of event subclass, providing further information about each event class. This column may contain the following values:<br /><br /> Subscription registered: Indicates when the query notification subscription is successfully registered in the database.<br /><br /> Subscription rewound: Indicates when the [!INCLUDE[ssDE](../../includes/ssde-md.md)] receives a subscription request that exactly matches an existing subscription. In this case, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] sets the time-out value of the existing subscription to the time-out specified in the new subscription request.<br /><br /> Subscription fired: Indicates when a notification subscription produces a notification message.<br /><br /> Firing failed with broker error: Indicates when a notification message fails due to a [!INCLUDE[ssSB](../../includes/sssb-md.md)] error.<br /><br /> Firing failed without broker error: Indicates when a notification message fails but is not due to a [!INCLUDE[ssSB](../../includes/sssb-md.md)] error.<br /><br /> Broker error intercepted: Indicates that [!INCLUDE[ssSB](../../includes/sssb-md.md)] delivered an error in the conversation that the query notification uses.<br /><br /> Subscription deletion attempt: Indicates that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] attempted to delete an expired subscription to free up resources.<br /><br /> Subscription deletion failed: Indicates that the attempt to delete an expired subscription has failed. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will automatically reschedule the subscription for deletion to free up resources.<br /><br /> Subscription destroyed: Indicates that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] successfully deleted an expired subscription|21|Yes|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|`nvarchar`|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process.<br /><br /> 0 = user<br /><br /> 1 = system|60|No|  
|LoginName|`nvarchar`|The name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the Windows login credentials in the form of DOMAIN\Username).|11|No|  
|LoginSID|`image`|The security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|The Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|The name of the user that owns the connection that generated this event.|6|Yes|  
|RequestID|`int`|Identifier of the request that contains the statement.|49|Yes|  
|ServerName|`nvarchar`|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|`nvarchar`|Login name of the user that originated the session. For example, if an application connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and executes a statement as Login2, SessionLoginName shows "Login1" and LoginName shows "Login2". This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the event started, if available.|14|Yes|  
|TextData|`ntext`|Returns an XML document containing information specific to this event. This document conforms to the XML schema available at the [SQL Server Query Notification Profiler Event Schema](https://go.microsoft.com/fwlink/?LinkId=63331) page.|1|Yes|  
  
  
