---
title: "File Load and Save Data Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 0101e809-d6ea-4d0c-95ec-65dd77acf665
caps.latest.revision: 5
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# File Load and Save Data Columns
  The File Load and Save event category has the following event class:  
  
|**Event ID**|**Event Name**|**Event Description**|  
|------------------|--------------------|---------------------------|  
|90|File Load Begin|File Load Begin.|  
|91|File Load End|File Load End.|  
|92|File Save Begin|File Save Begin.|  
|93|File Save End|File Save End|  
|94|PageOut Begin|PageOut Begin.|  
|95|PageOut End|PageOut End|  
|96|PageIn Begin|PageIn Begin.|  
|97|PageIn End|PageIn End|  
  
 The following table lists the data columns for this event class.  
  
## File Load Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|SessionID|39|8|Session GUID.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## File Load End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
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
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## File Save Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|SessionID|39|8|Session GUID.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event|  
  
## File Save End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
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
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## PageOut Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|SessionID|39|8|Session GUID.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## PageOut End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
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
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## PageIn Begin  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|JobID|7|1|Job ID for progress.|  
|SessionType|8|8|Session type (what entity caused the operation).|  
|ObjectID|11|8|Object ID (note this is a string).|  
|ObjectType|12|1|Object type.|  
|ObjectName|13|8|Object name.|  
|ObjectPath|14|8|Object path. A comma-separated list of parents, starting with the object's parent.|  
|ConnectionID|25|1|Unique connection ID.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|ClientProcessID|36|1|The process ID of the client application.|  
|SessionID|39|8|Session GUID.|  
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## PageIn End  
  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|---------------------|-------------------|---------------------|----------------------------|  
|EventClass|0|1|Event Class is used to categorize events.|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Amount of time (in milliseconds) taken by the event.|  
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
|TextData|42|9|Text data associated with the event.|  
|ServerName|43|8|Name of the server producing the event.|  
  
## See Also  
 [File Load and Save Event Category](../../analysis-services/trace-events/file-load-and-save-event-category.md)  
  
  