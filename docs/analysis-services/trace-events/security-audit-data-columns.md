---
title: "Security Audit Data Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "Security Audit event category [SQL Server]"
ms.assetid: fac1a7f9-5961-4f4b-bb04-847616b505d7
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Security Audit Data Columns
  The Security Audit event category has the following event classes:  
  
||||  
|-|-|-|  
|**Event ID**|**Event Name**|**Event Description**|  
|1|Audit Login|Collects all new connection events since the trace was started, such as when a client requests a connection to a server running an instance of SQL Server.|  
|2|Audit Logout|Collects all new disconnect events since the trace was started, such as when a client issues a disconnect command.|  
|4|Audit Server Starts And Stops|Records service shut down, start, and pause activities.|  
|18|Audit Object Permission Event|Records object permission changes.|  
|19|Audit Admin Operations Event|Records server backup/restore/synchronize/attach/detach/imageload/imagesave.|  
  
 The following tables list the data columns for each of these event classes.  
  
## Audit Login  
  
|||||  
|-|-|-|-|  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Severity|22|1|Severity level of an exception.|  
|Success|23|1|1 = success. 0 = failure (for example, a 1 means success of a permissions check and a 0 means a failure of that check).|  
|Error|24|1|Error number of a given event.|  
|ConnectionID|25|1|Unique connection ID.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientHostName|35|8|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Audit Logout  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|Success|23|1|1 = success. 0 = failure (for example, a 1 means success of a permissions check and a 0 means a failure of that check).|  
|ConnectionID|25|1|Unique connection ID.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientHostName|35|8|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Audit Server Starts And Stops  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 1: Instance Shutdown<br /><br /> 2: Instance Started<br /><br /> 3: Instance Paused<br /><br /> 4: Instance Continued|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Severity|22|1|Severity level of an exception.|  
|Success|23|1|1 = success. 0 = failure (for example, a 1 means success of a permissions check and a 0 means a failure of that check).|  
|Error|24|1|Error number of a given event.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Audit Object Permission Event  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ObjectReference|15|8|Object reference. Encoded as XML for all parents, using tags to describe the object.|  
|Severity|22|1|Severity level of an exception.|  
|Success|23|1|1 = success. 0 = failure (for example, a 1 means success of a permissions check and a 0 means a failure of that check).|  
|Error|24|1|Error number of a given event.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientHostName|35|8|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Audit Admin Operations Event  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 1: **Backup**<br /><br /> 2: **Restore**<br /><br /> 3: **Synchronize**<br /><br /> 4: **Detach**<br /><br /> 5: **Attach**<br /><br /> 6: **ImageLoad**<br /><br /> 7: **ImageSave**|  
|Severity|22|1|Severity level of an exception.|  
|Success|23|1|1 = success. 0 = failure (for example, a 1 means success of a permissions check and a 0 means a failure of that check).|  
|Error|24|1|Error number of a given event.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientHostName|35|8|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## See Also  
 [Security Audit Event Category](../../analysis-services/trace-events/security-audit-event-category.md)  
  
  