---
title: SqlErrorLogEvent class
description: Provides properties for viewing events in a specified SQL Server log file.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 02/23/2023
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SqlErrorLogEvent class"
  - "SqlErrorLogFile class"
---
# SqlErrorLogEvent class

[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

Provides properties for viewing events in a specified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log file.

## Syntax

```csharp
class SQLErrorLogEvent
{
    stringFileName;
    stringInstanceName;
    datetimeLogDate;
    stringMessage;
    stringProcessInfo;
};
```

## Properties

The SQLErrorLogEvent class defines the following properties.

| Property | Description |
| --- | --- |
| FileName | Data type: **string**<br /><br />Access type: Read-only<br /><br />The name of the error log file. |
| InstanceName | Data type: **string**<br /><br />Access type: Read-only<br /><br />Qualifiers: Key<br /><br />The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where the log file resides. |
| LogDate | Data type: **datetime**<br /><br />Access type: Read-only<br /><br />Qualifiers: Key<br /><br />The date and time that the event was recorded in the log file. |
| Message | Data type: **string**<br /><br />Access type: Read-only<br /><br />The event message. |
| ProcessInfo | Data type: **string**<br /><br />Access type: Read-only<br /><br />Information about the source server process ID (SPID) for the event. |

## Remarks

| Type | Name |
| --- | --- |
| MOF | - `sqlmgmprovider.mof` ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions)<br />- `sqlmgmproviderxpsp2up.mof` ([!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions) |
| DLL | `sqlmgmprovider.dll` |
| Namespace | `\root\Microsoft\SqlServer\ComputerManagement10` |

## Example

The following example shows how to retrieve values for all logged events in a specified log file. To run the example, replace \<*Instance_Name*> with the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as 'Instance1', and replace 'File_Name' with the name of the error log file, such as 'ERRORLOG.1'.

```vbnet
on error resume next
strComputer = "."
Set objWMIService = GetObject("winmgmts:" _
    & "{impersonationLevel=impersonate}!\\" _
    & strComputer & "\root\MICROSOFT\SqlServer\ComputerManagement10")
set logEvents = objWmiService.ExecQuery("SELECT * FROM SqlErrorLogEvent WHERE InstanceName = '<Instance_Name>' AND FileName = 'File_Name'")
  
For Each logEvent in logEvents
WScript.Echo "Instance Name: " & logEvent.InstanceName & vbNewLine _
    & "Log Date: " & logEvent.LogDate & vbNewLine _
    & "Log File Name: " & logEvent.FileName & vbNewLine _
    & "Process Info: " & logEvent.ProcessInfo & vbNewLine _
    & "Message: " & logEvent.Message & vbNewLine _
  
Next
```

## Comments

When *InstanceName* or *FileName* aren't provided in the WQL statement, the query returns information for the default instance and the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log file. For example, the following WQL statement returns all log events from the current log file (ERRORLOG) on the default instance (MSSQLSERVER).

```wql
"SELECT * FROM SqlErrorLogEvent"
```

## Security

To connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log file through WMI, you must have the following permissions on both the local and remote computers:

- Read access to the **Root\Microsoft\SqlServer\ComputerManagement10** WMI namespace. By default, everyone has read access through the Enable Account permission.

- Read permission to the folder that contains the error logs. By default the error logs are located in the following path (where \<*Drive>* represents the drive where you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and \<*InstanceName*> is the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]):

  **\<Drive>:\Program Files\Microsoft SQL Server\MSSQL13** **.\<InstanceName>\MSSQL\Log**

If you're connecting through a firewall, ensure that an exception is set in the firewall for WMI on remote target computers. For more information, see [Connecting to WMI Remotely Starting with Windows Vista](/windows/win32/wmisdk/connecting-to-wmi-remotely-starting-with-vista).

## See also

- [SqlErrorLogFile Class](../../relational-databases/wmi-provider-configuration-classes/sqlerrorlogfile-class.md)
- [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md)
