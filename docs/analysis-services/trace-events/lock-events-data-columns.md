---
title: "Lock Events Data Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: c223157f-41a0-405c-bc1a-41c999506936
caps.latest.revision: 5
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Lock Events Data Columns
  The Lock event category has the following event class:  
  
|**Event ID**|**Event Name**|**Event Description**|  
|------------------|--------------------|---------------------------|  
|50|Deadlock|Information on current locks in deadlock state.|  
|51|Lock Timeout|Information on locks that had recently timed out|  
|52|Lock Acquired|Information on recently acquired locks|  
|53|Lock Released|Information on recently released locks|  
|54|Lock Waiting|Information on locks waiting on other locks|  
  
 The following table lists the data columns for this event class.  
  
## Deadlock  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Lock Timeout  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Lock Acquired  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|ConnectionID|25|1|Unique connection ID.|  
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
  
## Lock Released  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|ConnectionID|25|1|Unique connection ID.|  
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
  
## Lock Waiting  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|ConnectionID|25|1|Unique connection ID.|  
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
 [Lock Events Category](../../analysis-services/trace-events/lock-events-category.md)  
  
  