---
description: "Logging and Defining Log Entries in a Data Flow Component"
title: "Logging and Defining Log Entries in a Data Flow Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "logs [Integration Services], custom"
  - "custom log entries [Integration Services]"
  - "custom data flow components [Integration Services], logging"
  - "data flow components [Integration Services], logging"
ms.assetid: 2190dba9-59b5-480b-b8e9-21d5a54c5917
author: chugugrace
ms.author: chugu
---
# Logging and Defining Log Entries in a Data Flow Component

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  Custom data flow components can post messages to an existing log entry by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.PostLogMessage%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface. They can also present information to the user by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.FireInformation%2A> method or similar methods of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface. However, this approach incurs the overhead of raising and handling additional events, and forces the user to sift through verbose informational messages for the messages that may be of interest to them. You can use a custom log entry as described below to provide distinctly labeled custom log information to users of your component.  
  
## Registering and Using a Custom Log Entry  
  
### Registering a Custom Log Entry  
 To register a custom log entry for use by your component, override the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.RegisterLogEntries%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> base class. The following example registers a custom log entry and provides a name and description.  
  
```csharp  
using Microsoft.SqlServer.Dts.Runtime;  
...  
private const string MyLogEntryName = "My Custom Component Log Entry";  
private const string MyLogEntryDescription = "Log entry from My Custom Component ";  
...  
    public override void RegisterLogEntries()  
    {  
      this.LogEntryInfos.Add(MyLogEntryName,  
        MyLogEntryDescription,  
        Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSLogEntryFrequency.DTSLEF_CONSISTENT);  
    }  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
...  
Private Const MyLogEntryName As String = "My Custom Component Log Entry"   
Private Const MyLogEntryDescription As String = "Log entry from My Custom Component "  
...  
Public  Overrides Sub RegisterLogEntries()   
  Me.LogEntryInfos.Add(MyLogEntryName, _  
    MyLogEntryDescription, _  
    Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSLogEntryFrequency.DTSLEF_CONSISTENT)   
End Sub  
```  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSLogEntryFrequency> enumeration provides a hint to the runtime about how frequently the event will be logged:  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSLogEntryFrequency.DTSLEF_OCCASIONAL>: Event is logged only sometimes, not during every execution.  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSLogEntryFrequency.DTSLEF_CONSISTENT>: Event is logged a constant number of times during every execution.  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSLogEntryFrequency.DTSLEF_PROPORTIONAL>: Event is logged a number of times proportional to the amount of work completed.  
  
 The example above uses <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSLogEntryFrequency.DTSLEF_CONSISTENT> because the component expects to log an entry once per execution.  
  
 After registering the custom log entry and adding an instance of your custom component to the data flow designer surface, the **Logging** dialog box in the designer displays a new log entry with the name "My Custom Component Log Entry" in the list of available log entries.  
  
### Logging to a Custom Log Entry  
 After registering the custom log entry, the component can now log custom messages. The example below writes a custom log entry during the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PreExecute%2A> method that contains the text of a SQL statement used by the component.  
  
```csharp  
public override void PreExecute()  
{  
  DateTime now = DateTime.Now;  
  byte[] additionalData = null;  
  this.ComponentMetaData.PostLogMessage(MyLogEntryName,  
    this.ComponentMetaData.Name,  
    "Command Sent was: " + myCommand.CommandText,  
    now, now, 0, ref additionalData);  
}  
```  
  
```vb  
Public  Overrides Sub PreExecute()   
  Dim now As DateTime = DateTime.Now   
  Dim additionalData As Byte() = Nothing   
  Me.ComponentMetaData.PostLogMessage(MyLogEntryName, _  
    Me.ComponentMetaData.Name, _  
    "Command Sent was: " + myCommand.CommandText, _  
    now, now, 0, additionalData)   
End Sub  
```  
  
 Now when the user executes the package, after selecting the "My Custom Component Log Entry" in the **Logging** dialog box, the log will contain an entry clearly labeled as "User::My Custom Component Log Entry." This new log entry contains the text of the SQL statement, the timestamp, and any additional data logged by the developer.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](../../../integration-services/performance/integration-services-ssis-logging.md)  
  
  
