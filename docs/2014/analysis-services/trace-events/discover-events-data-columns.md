---
title: "Discover Events Data Columns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Discover Events event category"
ms.assetid: 10ec598e-5b51-4767-b4f7-42e261d96a40
author: minewiskan
ms.author: owend
manager: craigg
---
# Discover Events Data Columns
  The Discover Events category has the following event classes:  
  
-   Discover Begin class  
  
-   Discover End class  
  
 The following tables list the data columns for each of these event classes.  
  
## Discover Begin Class—Data Columns  
  
|||||  
|-|-|-|-|  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|EventClass|0|1|Event Class is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class. The following are valid value: name pairs:<br /><br /> 0: **DBSCHEMA_CATALOGS**<br />1: **DBSCHEMA_TABLES**<br />2: **DBSCHEMA_COLUMNS**<br />3: **DBSCHEMA_PROVIDER_TYPES**<br />4: **MDSCHEMA_CUBES**<br />5: **MDSCHEMA_DIMENSIONS**<br />6: **MDSCHEMA_HIERARCHIES**<br />7: **MDSCHEMA_LEVELS**<br />8: **MDSCHEMA_MEASURES**<br />9: **MDSCHEMA_PROPERTIES**<br />10: **MDSCHEMA_MEMBERS**<br />11: **MDSCHEMA_FUNCTIONS**<br />12: **MDSCHEMA_ACTIONS**<br />13: **MDSCHEMA_SETS**<br />14: **DISCOVER_INSTANCES**<br />15: **MDSCHEMA_KPIS**<br />16: **MDSCHEMA_MEASUREGROUPS**<br />17: **MDSCHEMA_COMMANDS**<br />18: **DMSCHEMA_MINING_SERVICES**<br />19: **DMSCHEMA_MINING_SERVICE_PARAMETERS**<br />20: **DMSCHEMA_MINING_FUNCTIONS**<br />21: **DMSCHEMA_MINING_MODEL_CONTENT**<br />22: **DMSCHEMA_MINING_MODEL_XML**<br />23: **DMSCHEMA_MINING_MODELS**<br />24: **DMSCHEMA_MINING_COLUMNS**<br />25: **DISCOVER_DATASOURCES**<br />26: **DISCOVER_PROPERTIES**<br />27: **DISCOVER_SCHEMA_ROWSETS**<br />28: **DISCOVER_ENUMERATORS**<br />29: **DISCOVER_KEYWORDS**<br />30: **DISCOVER_LITERALS**<br />31: **DISCOVER_XML_METADATA**<br />32: **DISCOVER_TRACES**<br />33: **DISCOVER_TRACE_DEFINITION_PROVIDERINFO**<br />34: **DISCOVER_TRACE_COLUMNS**<br />35: **DISCOVER_TRACE_EVENT_CATEGORIES**<br />36: **DMSCHEMA_MINING_STRUCTURES**<br />37: **DMSCHEMA_MINING_STRUCTURE_COLUMNS**<br />38: **DISCOVER_MASTER_KEY**<br />39: **MDSCHEMA_INPUT_DATASOURCES**<br />40: **DISCOVER_LOCATIONS**<br />41: **DISCOVER_PARTITION_DIMENSION_STAT**<br />42: **DISCOVER_PARTITION_STAT**<br />43: **DISCOVER_DIMENSION_STAT**<br />44: **MDSCHEMA_MEASUREGROUP_DIMENSIONS**<br />45: **DISCOVER_XEVENT_TRACE_DEFINITION**|  
|CurrentTime|2|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|ConnectionID|25|1|Contains the unique connection ID associated with the discover event.|  
|DatabaseName|28|8|Name of the database in which the statement of the user is running.|  
|NTUserName|32|8|Contains the Windows user name associated with the discover event. The user name is in canonical form. For example, engineering.microsoft.com/software/user.|  
|NTDomainName|33|8|Contains the Windows domain associated with the discover event.|  
|ClientProcessID|36|1|Contains the process ID of the client application.|  
|ApplicationName|37|8|Name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|SessionID|39|8|Contains the session ID associated with the discover event.|  
|NTCanonicalUserName|40|8|Contains the Windows user name associated with the discover event. The user name is in canonical form. For example, engineering.microsoft.com/software/user.|  
|SPID|41|1|Contains the server process ID (SPID) that uniquely identifies the user session associated with the discover event. The SPID directly corresponds to the session GUID used by XMLA.|  
|TextData|42|9|Contains the text data associated with the event.|  
|ServerName|43|8|Contains the name of the instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on which the discover event occurred.|  
|RequestProperties|45|9|Contains the XML for Analysis (XMLA) request properties associated with the discover event.|  
  
## Discover End Class—Data Columns  
  
|||||  
|-|-|-|-|  
|**Column Name**|**Column Id**|**Column Type**|**Column Description**|  
|EventClass|0|1|Contains the event class; this is used to categorize events.|  
|EventSubclass|1|1|Event Subclass provides additional information about each event class. The following are valid value: name pairs:<br /><br /> 0: **DBSCHEMA_CATALOGS**<br />1: **DBSCHEMA_TABLES**<br />2: **DBSCHEMA_COLUMNS**<br />3: **DBSCHEMA_PROVIDER_TYPES**<br />4: **MDSCHEMA_CUBES**<br />5: **MDSCHEMA_DIMENSIONS**<br />6: **MDSCHEMA_HIERARCHIES**<br />7: **MDSCHEMA_LEVELS**<br />8: **MDSCHEMA_MEASURES**<br />9: **MDSCHEMA_PROPERTIES**<br />10: **MDSCHEMA_MEMBERS**<br />11: **MDSCHEMA_FUNCTIONS**<br />12: **MDSCHEMA_ACTIONS**<br />13: **MDSCHEMA_SETS**<br />14: **DISCOVER_INSTANCES**<br />15: **MDSCHEMA_KPIS**<br />16: **MDSCHEMA_MEASUREGROUPS**<br />17: **MDSCHEMA_COMMANDS**<br />18: **DMSCHEMA_MINING_SERVICES**<br />19: **DMSCHEMA_MINING_SERVICE_PARAMETERS**<br />20: **DMSCHEMA_MINING_FUNCTIONS**<br />21: **DMSCHEMA_MINING_MODEL_CONTENT**<br />22: **DMSCHEMA_MINING_MODEL_XML**<br />23: **DMSCHEMA_MINING_MODELS**<br />24: **DMSCHEMA_MINING_COLUMNS**<br />25: **DISCOVER_DATASOURCES**<br />26: **DISCOVER_PROPERTIES**<br />27: **DISCOVER_SCHEMA_ROWSETS**<br />28: **DISCOVER_ENUMERATORS**<br />29: **DISCOVER_KEYWORDS**<br />30: **DISCOVER_LITERALS**<br />31: **DISCOVER_XML_METADATA**<br />32: **DISCOVER_TRACES**<br />33: **DISCOVER_TRACE_DEFINITION_PROVIDERINFO**<br />34: **DISCOVER_TRACE_COLUMNS**<br />35: **DISCOVER_TRACE_EVENT_CATEGORIES**<br />36: **DMSCHEMA_MINING_STRUCTURES**<br />37: **DMSCHEMA_MINING_STRUCTURE_COLUMNS**<br />38: **DISCOVER_MASTER_KEY**<br />39: **MDSCHEMA_INPUT_DATASOURCES**<br />40: **DISCOVER_LOCATIONS**<br />41: **DISCOVER_PARTITION_DIMENSION_STAT**<br />42: **DISCOVER_PARTITION_STAT**<br />43: **DISCOVER_DIMENSION_STAT**<br />44: **MDSCHEMA_MEASUREGROUP_DIMENSIONS**<br />45: **DISCOVER_XEVENT_TRACE_DEFINITION**|  
|CurrentTime|2|5|Contains the current time of the discover event, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|3|5|Contains the time (if available) at which the discover end event started.. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|4|5|Contains the time at which the event ended. This column is not populated for starting event classes, such as SQL:BatchStarting or SP:Starting. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|Duration|5|2|Contains the approximate amount of time (milliseconds) taken by the discover event.|  
|CPUTime|6|2|Contains the amount of CPU time (in milliseconds) used by the event.|  
|Severity|22|1|Contains the severity level of an exception.|  
|Success|23|1|Contains the success or failure of the discover event. Values are:<br /><br /> 0 = Failure<br /><br /> 1 = Success|  
|Error|24|1|Contains the error number of any error associated the discover event.|  
|ConnectionID|25|1|Contains the unique connection ID associated with the discover event.|  
|DatabaseName|28|8|Contains the name of the database in which the discover event occurred.|  
|NTUserName|32|8|Contains the Windows user name associated with the object permission event.|  
|NTDomainName|33|8|Contains the Windows domain account associated with the discover event.|  
|ClientProcessID|36|1|Contains the client process ID of the application that initiated the event.|  
|ApplicationName|37|8|Contains the name of the client application that created the connection to the server. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|SessionID|39|8|Contains the session ID associated with the discover event.|  
|NTCanonicalUserName|40|8|Contains the Windows user name associated with the object permission event. The user name is in canonical form. For example, engineering.microsoft.com/software/user.|  
|SPID|41|1|Contains the server process ID (SPID) that uniquely identifies the user session associated with the discover end event. The SPID directly corresponds to the session GUID used by XMLA.|  
|TextData|42|9|Contains the text data associated with the event.|  
|ServerName|43|8|Contains the name of the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on which the discover event occurred.|  
|RequestProperties|45|9|Contains the properties in the XMLA request.|  
  
## See Also  
 [Discover Events Event Category](discover-events-event-category.md)  
  
  
