---
title: "Query Processing Events Data Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 81a522bd-440d-406c-a524-3af44a3af101
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Query Processing Events Data Columns
  The Query Processing Events event category has the following event classes:  
  
|**Event ID**|**Event Name**|**Event Description**|  
|------------------|--------------------|---------------------------|  
|70|Query Cube Begin|Query cube begin.|  
|71|Query Cube End|Query cube end.|  
|72|Calculate Non Empty Begin|Calculate non empty begin.|  
|73|Calculate Non Empty Current|Calculate non empty current.|  
|74|Calculate Non Empty End|Calculate non empty end.|  
|75|Serialize Results Begin|Serialize results begin.|  
|76|Serialize Results Current|Serialize results current.|  
|77|Serialize Results End|Serialize results end.|  
|78|Execute MDX Script Begin|Execute MDX script begin.|  
|79|Execute MDX Script Current|Execute MDX script current. Deprecated.|  
|80|Execute MDX Script End|Execute MDX script end.|  
|81|Query Dimension|Query dimension.|  
|11|Query Subcube|Query subcube, for Usage Based Optimization.|  
|12|Query Subcube Verbose|Query subcube with detailed information. This event may have a negative impact on performance when turned on.|  
|60|Get Data From Aggregation|Answer query by getting data from aggregation. This event may have a negative impact on performance when turned on.|  
|61|Get Data From Cache|Answer query by getting data from one of the caches. This event may have a negative impact on performance when turned on.|  
|82|VertiPaq SE Query Begin|VertiPaq SE query|  
|83|VertiPaq SE Query End|VertiPaq SE query|  
|84|Resource Usage|Reports reads, writes, cpu usage after end of commands and queries.|  
|85|VertiPaq SE Query Cache Match|VertiPaq SE query cache use|  
|98|Direct Query Begin|Direct Query Begin.|  
|99|Direct Query End|Direct Query End.|  
  
 The following tables list the data columns for each of these event classes.  
  
## Query Cube Begin  
  
|||||  
|-|-|-|-|  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Query Cube End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Calculate Non Empty Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Calculate Non Empty Current  
  
|||||  
|-|-|-|-|  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 1: Get Data<br /><br /> 2: Process Calculated Members<br /><br /> 3: Post Order|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Calculate Non Empty End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Serialize Results Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Serialize Results Current  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class.<br /><br /> 1: Serialize Axes<br /><br /> 2: Serialize Cells<br /><br /> 3: Serialize SQL Rowset<br /><br /> 4: Serialize Flattened Rowset|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Serialize Results End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Execute MDX Script Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 1: MDX Script<br /><br /> 2: MDX Script Command|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Execute MDX Script Current  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Execute MDX Script End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 1: MDX Script<br /><br /> 2: MDX Script Command|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Query Dimension  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class.<br /><br /> 1: Cache data<br /><br /> 2: Non-cache data|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
|ObjectType|12|1|Object type.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Query Subcube  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 1: Cache data<br /><br /> 2: Non-cache data<br /><br /> 3: Internal data<br /><br /> 4: SQL data<br /><br /> 11: Measure Group Structural Change<br /><br /> 12: Measure Group Deletion|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Query Subcube Verbose  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 21: Cache data<br /><br /> 22: Non-cache data<br /><br /> 23: Internal data<br /><br /> 24: SQL data|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Get Data From Aggregation  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Get Data From Cache  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class.<br /><br /> 1: Get data from measure group cache<br /><br /> 2: Get data from flat cache<br /><br /> 3: Get data from calculation cache<br /><br /> 4: Get data from persisted cache|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## VertiPaq SE Query Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 0: VertiPaq Scan<br /><br /> 1: Tabular Query<br /><br /> 2: User Hierarchy Processing Query<br /><br /> 10: VertiPaq Scan internal<br /><br /> 11: Tabular Query internal<br /><br /> 12: User Hierarchy Processing Query internal|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ObjectReference|15|8|Object reference. Encoded as XML for all parents, using tags to describe the object.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## VertiPaq SE Query End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class.<br /><br /> 0: VertiPaq Scan<br /><br /> 1: Tabular Query<br /><br /> 10: VertiPaq Scan internal<br /><br /> 11: Tabular Query internal|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|ProgressTotal|9|1|Progress total.|  
|IntegerData|10|1|Integer data.|  
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
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Resource Usage  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## VertiPaq SE Query Cache Match  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class:<br /><br /> 0: VertiPaq Cache exact match|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ObjectReference|15|8|Object reference. Encoded as XML for all parents, using tags to describe the object.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Windows user name.|  
|NTDomainName|33|8|Windows domain to which the user belongs.|  
|SessionID|39|8|Session GUID.|  
|NTCanonicalUserName|40|8|User name in canonical form. For example, engineering.microsoft.com/software/someone.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Direct Query Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|IntegerData|10|1|Integer data.|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|Severity|22|1|Severity level of an exception.|  
|Success|23|1|1 = success. 0 = failure (for example, a 1 means success of a permissions check and a 0 means a failure of that check).|  
|Error|24|1|Error number of a given event.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|SessionID|39|8|Session GUID.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## Direct Query End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
|CPUTime|6|2|Amount of CPU time (in milliseconds) used by the event.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|IntegerData|10|1|Integer data.|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|Severity|22|1|Severity level of an exception.|  
|Success|23|1|1 = success. 0 = failure (for example, a 1 means success of a permissions check and a 0 means a failure of that check).|  
|Error|24|1|Error number of a given event.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|SessionID|39|8|Session GUID.|  
|SPID|41|1|Server process ID. This uniquely identifies a user session. This directly corresponds to the session GUID used by XML/A.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## See Also  
 [Query Processing Events Category](../../analysis-services/trace-events/query-processing-events-category.md)  
  
  