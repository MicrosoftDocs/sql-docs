---
title: Work with write event log script task.
description: "This article helps you to configure the Script Task."
author: suresh-kandoth
ms.author: sureshka
ms.reviewer: randolphwest
ms.date: "02/10/2023"
ms.service: sql
ms.subservice: integration-services
ms.topic: "reference"
---

# Write information to the Application log

You can include a Script Task in the Microsoft SQL Server Integration Services (SSIS) package to run a task. For example, the task may write a collection of variable information to the Windows Application log. You can create an SSIS package that contains a Data Flow task. This Data Flow task includes a Row Count transformation. You can use a Script Task to write the data that was populated by the Row Count transformation to the Windows Application log.

This article describes how to use a Script Task to write information to the Windows Application log.

_Original product version_: &nbsp; SQL Server  
_Original KB number_: &nbsp; 906560

## Description

This example assumes that you created the following elements in the SSIS package:

- A Data Flow task.
- A Script Task.
- A connector from the Data Flow task to the Script Task.
- In the Data Flow task, you created a Row Count transformation in the data flow.

When you run the package, the Row Count transformation returns the row count data that you want to write to the Windows Application log.

## Configure the Script Task

To configure the Script Task code sample, follow these steps in the SSIS designer:

1. While the **Control Flow** tab is active, right-click the design surface, and then select **Variables**. The **Variables** window appears in the left pane.

1. In the **Variables** window, select **Add variable**, and then provide the variable name as *mycount*. By default, the data type of the new *mycount* variable is `Int32`.

      > [!NOTE]
      > References to variable names are case-sensitive.

1. Double-click the **Data Flow Task**. The active window switches to the **Data Flow** tab.

1. Use the properties of the Row Count transformation to set the value of the **VariableName** property to *mycount*.

1. Select the **Control Flow** tab, and then double-click the **Script Task**. The **Script Task Editor** dialog box appears.

1. In the left pane, select the **Script** item, and then change the value of the **ReadOnlyVariables** property to the following value:

    `PackageName,StartTime,ExecutionInstanceGUID,mycount`

    > [!NOTE]
    > The **PackageName**, **StartTime**, and **ExecutionInstanceGUID** items are system variables. These system variables are used to write the package information to the Windows Application log.

1. In the **Script Task Editor** dialog box, select **Edit Script**.

1. When a new Microsoft Visual Studio for Applications (VSTA) window appears, follow these steps:

    1. Make sure that the following namespaces are included in your code before any other declarations.

        ```vb
        Imports System 
        Imports System.Data
        Imports System.Math
        Imports System.Diagnostics
        Imports Microsoft.SqlServer.Dts.Runtime
        ```

    1. Replace the following code sample with the code in the `Main()` method.

        ```vb
        Dim varMyCount As Variable = Dts.Variables("mycount") '
        Dim varPackageName As Variable = Dts.Variables("PackageName")
        Dim varStartTime As Variable = Dts.Variables("StartTime")
        Dim varInstanceID As Variable = Dts.Variables("ExecutionInstanceGUID")
        Dim PackageDuration As Long
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        ' Event log needs
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        Dim sSource As String
        Dim sLog As String
        Dim sEventMessage As String
        Dim sMachine As String
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        PackageDuration = DateDiff("s", varStartTime.Value, Now())
        sSource = "RowCountReporting from SSIS"
        ' We need the message posted to the Application event log.
        sLog = "Application"
        sEventMessage = "Rows Processed: " & Chr(10) _
        & " case Rows:" + varMyCount.Value().ToString + Chr(10) _
        & "=============================================" & Chr(10) _
        & "The Package: " + varPackageName.Value().ToString _
        & Chr(10) _
        & "Started: " & varStartTime.Value().ToString _
        & Chr(10) _
        & "Current Time:" & System.DateTime.Now _
        & Chr(10) _
        & "=============================================" _
        & Chr(10) _
        & "Package Run Duration in seconds: " & PackageDuration _
        & Chr(10) _
        & "Execution GUID: " & varInstanceID.Value().ToString
        sMachine = "."
        If Not EventLog.SourceExists(sSource, sMachine) Then
        EventLog.CreateEventSource(sSource, slog)
        End If
        Dim ELog As New EventLog(sLog, sMachine, sSource)
        Dim category As Short = 2
        ' ELog.WriteEntry("Write from third source", 4, 777, 2)
        ELog.WriteEntry(sEventMessage, EventLogEntryType.Information, 777, category)
        '###############################
        Dts.TaskResult = ScriptResults.Success
        ```

    After the package runs successfully, the following entry appears in the Windows Application log.

     ```output
        Log Name: Application
        Source: RowCountReporting from SSIS
        Date: 12/20/2022 11:21:38 AM
        Event ID: 777
        Task Category: (2)
        Level: Information
        Keywords: Classic
        User: N/A
        Computer: <hostname>
        Description:
        Rows Processed:
        case Rows:0
        =============================================
        The Package: Package
        Started: 12/20/2022 11:21:37 AM
        Current Time:12/20/2022 11:21:38 AM
        =============================================
        Package Run Duration in seconds: 1
        Execution GUID: {9DF22831-E608-47F7-BD62-F9BD3C2F9C77}
        Event Xml:
        <Event xmlns="http://schemas.microsoft.com/win/2004/08/events/event">
        <System>
        <Provider Name="RowCountReporting from SSIS" />
        <EventID Qualifiers="0">777</EventID>
        <Version>0</Version>
        <Level>4</Level>
        <Task>2</Task>
        <Opcode>0</Opcode>
        <Keywords>0x80000000000000</Keywords>
        <TimeCreated SystemTime="2022-12-20T17:21:38.5070621Z" />
        <EventRecordID>122603</EventRecordID>
        <Correlation />
        <Execution ProcessID="41588" ThreadID="0" />
        <Channel>Application</Channel>
        <Computer><hostname>/Computer>
        <Security />
        </System>
        <EventData>
        <Data>Rows Processed:
        case Rows:0
        =============================================
        The Package: Package
        Started: 12/20/2022 11:21:37 AM
        Current Time:12/20/2022 11:21:38 AM
        =============================================
        Package Run Duration in seconds: 1
        Execution GUID: {9DF22831-E608-47F7-BD62-F9BD3C2F9C77}</Data>
        </EventData>
        </Event>
    ```

## References

[EventLog.WriteEntry Method (System.Diagnostics)](/dotnet/api/system.diagnostics.eventlog.writeentry?view=windowsdesktop-7.0&preserve-view=true)

[Instrument Code to Create EventSource Events](/dotnet/core/diagnostics/eventsource-instrumentation)
