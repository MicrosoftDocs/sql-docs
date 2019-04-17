---
title: "Monitoring Traces (XMLA) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Monitoring Traces (XMLA)
  You can use the [Subscribe](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/subscribe-element-xmla) command in XML for Analysis (XMLA) to monitor an existing trace defined on an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The **Subscribe** command returns the results of a trace as a rowset.  
  
## Specifying a Trace  
 The [Object](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/object-element-xmla) property of the **Subscribe** command must contain an object reference to either an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance or a trace on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. If the **Object** property is not specified, or a trace identifier is not specified in the **Object** property, the **Subscribe** command monitors the default session trace for the explicit session specified in the SOAP header for the command.  
  
## Returning Results  
 The **Subscribe** command returns a rowset containing the trace events captured by the specified trace. The **Subscribe** command returns trace results until the command is canceled by the [Cancel](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/cancel-element-xmla) command.  
  
 The rowset contains the columns listed in the following table.  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|EventClass|Integer|The event class of the event received by the trace.|  
|EventSubclass|Long integer|The event subclass of the event received by the trace.|  
|CurrentTime|Datetime|The time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|StartTime|Datetime|The time at which the event started, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.|  
|EndTime|Datetime|The time at which the event ended, when available. For filtering, expected formats are 'YYYY-MM-DD' and 'YYYY-MM-DD HH:MM:SS'.<br /><br /> This column is not populated for event classes that describe the start of a process or action.|  
|Duration|Long integer|The amount of total time (in milliseconds) elapsed for the event.|  
|CPUTime|Long integer|The amount of processor time (in milliseconds) elapsed for the event.|  
|JobID|Long integer|The job identifier for the process.|  
|SessionID|String|The identifier of the session for which the event occurred.|  
|SessionType|String|The type of the session for which the event occurred.|  
|ProgressTotal|Long integer|The total number or amount of progress reported by the event.|  
|IntegerData|Long integer|Integer data associated with the event. The contents of this column depend on the event class and subclass of the event.|  
|ObjectID|String|The identifier of the object for which the event occurred.|  
|ObjectType|String|The type of the object specified in ObjectName.|  
|ObjectName|String|The name of the object for which the event occurred.|  
|ObjectPath|String|The hierarchical path of the object for which the event occurred. The path is represented as a comma-delimited string of object identifiers for the parents of the object specified in ObjectName.|  
|ObjectReference|String|The XML representation of the object reference for the object specified in ObjectName.|  
|NestLevel|Integer|The level of the transaction for which the event occurred.|  
|NumSegments|Long integer|The number of data segments affected or accessed by the command for which the event occurred.|  
|Severity|Integer|The severity level of an exception for the event. The column can contain one of the following values:<br /><br /> <br /><br /> 0: Success<br /><br /> <br /><br /> 1: Information<br /><br /> <br /><br /> 2: Warning<br /><br /> <br /><br /> 3: Error|  
|Success|Boolean|Indicates whether a command succeeded or failed.|  
|Error|Long integer|The error number of the event, if applicable.|  
|ConnectionID|String|The identifier of the connection for which the event occurred.|  
|DatabaseName|String|The name of the database for which the event occurred.|  
|NTUserName|String|The Windows user name of the user associated with the event.|  
|NTDomainName|String|The Windows domain of the user associated with the event.|  
|ClientHostName|String|The name of the computer on which the client application is running. This column is populated with the values passed by the client application.|  
|ClientProcessID|Long integer|The process identifier of the client application.|  
|ApplicationName|String|The name of the client application that created the connection to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. This column is populated with the values passed by the client application, rather than the displayed name of the program.|  
|NTCanonicalUserName|String|The Windows canonical user name of the user associated with the event.|  
|SPID|String|The server process ID (SPID) of the session for which the event occurred. The value of this column directly corresponds to the session ID specified in the SOAP header of the XMLA message for which the event occurred.|  
|TextData|String|The text data associated with the event. The contents of this column depend on the event class and subclass of the event.|  
|ServerName|String|The name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance for which the event occurred.|  
|RequestParameters|String|The parameters of the parameterized query or XMLA command for which the event occurred.|  
|RequestProperties|String|The properties of the XMLA method for which the event occurred.|  
  
## See Also  
 [Developing with XMLA in Analysis Services](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)  
  
  
