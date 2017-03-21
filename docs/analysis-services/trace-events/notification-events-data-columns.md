---
title: "Notification Events Data Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "Notification Events event category"
ms.assetid: 0ecf06da-1586-415a-9da8-60d4c634f030
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Notification Events Data Columns
  Notification events are events that are not directly caused by users of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For example, notifications occur because of users updating underlying tables for proactive caching.  
  
 The Notifications Events event category has the following event class:  
  
|**Event ID**|**Event Name**|**Event Description**|  
|------------------|--------------------|---------------------------|  
|39|Notification|Notification event.|  
|40|User Defined|User defined Event.|  
  
 The following table lists the data columns for the event class.  
  
## Notification  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class.<br /><br /> The following **Sub Class Id**:<br />                      **Sub Class Name** pairs are defined:<br /><br /> 0: Proactive Caching Begin<br /><br /> 1: Proactive Caching End<br /><br /> 2: Flight Recorder Started<br /><br /> 3: Flight Recorder Stopped<br /><br /> 4: Configuration Properties Updated<br /><br /> 5: SQL Trace<br /><br /> 6: Object Created<br /><br /> 7: Object Deleted<br /><br /> 8: Object Altered<br /><br /> 9: Proactive Caching Polling Begin<br /><br /> 10: Proactive Caching Polling End<br /><br /> 11: Flight Recorder Snapshot Begin<br /><br /> 12: Flight Recorder Snapshot End<br /><br /> 13: Proactive Caching: notifiable object updated<br /><br /> 14: Lazy Processing: start processing<br /><br /> 15: Lazy Processing: processing complete<br /><br /> 16: SessionOpened Event Begin<br /><br /> 17: SessionOpened Event End<br /><br /> 18: SessionClosing Event Begin<br /><br /> 19: SessionClosing Event End<br /><br /> 20: CubeOpened Event Begin<br /><br /> 21: CubeOpened Event End<br /><br /> 22: CubeClosing Event Begin<br /><br /> 23: CubeClosing Event End<br /><br /> 24: Transaction abort requested|  
|CurrentTime|2|5|Contains the current time of the notification event, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Contains the time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Contains the time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Contains the amount of time (in milliseconds) taken by the event.|  
|IntegerData|10|1|Contains the integer data associated with the notification event. When the EventSubclass column is 8, values are:<br /><br /> 1 = Created<br /><br /> 2 = Deleted<br /><br /> 3 = Changed object's properties<br /><br /> 4 = Changed properties of the object's children<br /><br /> 6 = Children added<br /><br /> 7 = Children deleted<br /><br /> 8 = Object fully processed<br /><br /> 9 = Object partially processed<br /><br /> 10 = Object unprocessed<br /><br /> 11 = Object fully optimized<br /><br /> 12 = Object partially optimized<br /><br /> 13 = Object not optimized|  
|ObjectID|11|8|Contains the Object ID for which this notification is issued; this is a string value.|  
|ObjectType|12|1|Contains the object type associated with the notification event.|  
|ObjectName|13|8|Contains the object name associated with the notification event.|  
|ObjectPath|14|8|Contains the object path associated with the notification event. The path is returned as a comma-separated list of parents, starting with the object's parent.|  
|ObjectReference|15|8|Contains the object reference for the progress report end event. The object reference is encoded as XML by all parents by using tags to describe the object.|  
|ConnectionID|25|1|Contains the unique connection ID associated with the notification event.|  
|DatabaseName|28|8|Contains the name of the database in which the notification event occurred.|  
|NTUserName|32|8|Contains the Windows user name associated with the notification event.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|SessionID|39|8|Contains the session ID associated with the notification event.|  
|NTCanonicalUserName|40|8|Contains the Windows user name associated with the notification event. The user name is in canonical form. For example, engineering.microsoft.com/software/user.|  
|SPID|41|1|Contains the server process ID (SPID) that uniquely identifies the user session associated with the notification event. The SPID directly corresponds to the session GUID used by XMLA.|  
|TextData|42|9|Contains the text data associated with the notification event.|  
|ServerName|43|8|Contains the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance on which the notification event occurred.|  
|RequestProperties|45|9|Contains the properties of the XMLA request.|  
  
## User Defined  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|A specific user event subclass that provides additional information about each event class.|  
|CurrentTime|2|5|Contains the current time of the notification event, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|IntegerData|10|1|A specific user defined event information.|  
|ConnectionID|25|1|Contains the unique connection ID associated with the notification event.|  
|DatabaseName|28|8|Contains the name of the database in which the notification event occurred.|  
|NTUserName|32|8|Contains the Windows user name associated with the notification event.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|SessionID|39|8|Contains the session ID associated with the notification event.|  
|NTCanonicalUserName|40|8|Contains the Windows user name associated with the notification event. The user name is in canonical form. For example, engineering.microsoft.com/software/user.|  
|SPID|41|1|Contains the server process ID (SPID) that uniquely identifies the user session associated with the notification event. The SPID directly corresponds to the session GUID used by XMLA.|  
|TextData|42|9|Contains the text data associated with the notification event.|  
|ServerName|43|8|Contains the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance on which the notification event occurred.|  
  
## See Also  
 [Notification Events Event Category](../../analysis-services/trace-events/notification-events-event-category.md)  
  
  