---
title: "Session Events Data Columns | Microsoft Docs"
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
  - "Session Events event category"
ms.assetid: 35853451-6768-4a02-8b8f-81a8ae37a333
caps.latest.revision: 21
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Session Events Data Columns
  The Session Events event category has the following event class:  
  
|**Event ID**|**Event Name**|**Event Description**|  
|------------------|--------------------|---------------------------|  
|41|Existing Connection|Existing user connection.|  
|42|Existing Session|Existing session.|  
|43|Session Initialize|Session Initialize.|  
  
 The following table lists the data columns for this event class.  
  
## Existing Connection  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|ConnectionID|25|1|Unique connection ID.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientHostName|35|8|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Existing Session  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientHostName|35|8|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
|RequestProperties|45|9|XMLA request properties.|  
  
## Session Initialize  
  
|||||  
|-|-|-|-|  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientHostName|35|8|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
|RequestProperties|45|9|XMLA request properties.|  
  
## See Also  
 [Security Audit Event Category](../../analysis-services/trace-events/security-audit-event-category.md)  
  
  