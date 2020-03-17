---
title: Resource Monitor nonyielding condition in SQL Server (KB 2216485)
description: Describes the issue in which the Resource Monitor enters a non-yielding condition on a server that is running SQL Server.
ms.date: 01/27/2020
---
# Resource Monitor enters a nonyielding condition on a server running SQL Server

_Original version of product:_ &nbsp; 2008  
_Original KB date:_ &nbsp; 11/05/2018  
_Original KB number:_ &nbsp; 2216485

This article explains the behavior of the Resource Monitor when nonyielding memory conditions occur in SQL Server.

## Symptoms

On a server that's running Microsoft SQL Server 2008, or a later version, the Resource Monitor task logs the following message every 5 seconds:

```console
Date_And_Time Server Using 'dbghelp.dll' version '4.0.5'
Date_And_TimeServer **Dump thread - spid = 0, PSS = 0x0000000000000
000, EC = 0x0000000000000000
Date_And_TimeLogon Login succeeded for user 'OPENTEXT\sqlcrmusr'. Connection: trusted. [CLIENT:
IP_Address]
Date_And_Timespid78 Error: 4014, Severity: 20, State: 2.
Date_And_Timespid78 A fatal error occurred while reading the input stream from the network. The session will be terminated.
Date_And_TimeServer ***Stack Dump being sent to Drive:\MSSQL2005\LOG\SQLDump####.txt
Date_And_TimeServer * *******************************************************************************
Date_And_TimeServer *
Date_And_TimeServer * BEGIN STACK DUMP:
Date_And_TimeServer *
Date_And_Timespid 0
Date_And_TimeServer *
Date_And_TimeServer * Non-yielding Resource Monitor
Date_And_TimeServer *
Date_And_TimeServer * *******************************************************************************
Date_And_TimeServer * -------------------------------------------------------------------------------
Date_And_TimeServer * Short Stack Dump
Date_And_TimeServer Stack Signature for the dump is 0x000000000000005C
Date_And_Time,Server,Unknown,Resource Monitor (0x9b0) Worker 0x0000000003A2C1C0 appears to be non-yielding on
Node_#. Memory freed: 0 KB. Approx CPU Used: kernel 0 msnull user 0 msnull Interval:
Interval_value.
```

## Cause

The Resource Monitor task wakes up periodically to listen for memory events classified as low, high, or steady. The monitor notifies event subscribers when these events occur.

These memory events may be _external_ to SQL Server. External events include notifications from the operating system, and are system-wide. Other events are _internal_ to SQL Server. Internal events include notifications from the buffer pool, and are process-wide. When such notifications are received, various memory consumers trim their memory usage.

> [!NOTE]
> Consumers can be memory clerks that are cache stores, user stores, or object stores.

If certain memory consumers use a large amount of memory, the trimming that the consumers perform may take a long time to prepare.

The Scheduler Monitor task runs every 5 seconds. The Schedule Monitor checks whether the Resource Monitor has moved from one consumer to another during the past 60 seconds. When the Scheduler Monitor detects that the Resource Monitor has not moved past a consumer for 60 seconds, the Schedule Monitor interprets this scenario as the Resource Monitor entering a non-yielding state. This interpretation causes the Schedule Monitor to log the error message that is mentioned in the Symptoms section.

> [!NOTE]
> Starting with SQL Server 2019, the 60-second interval is increased to 120 seconds. This increase reduces the frequency of these diagnostic notifications. And it reduces the generation of memory dump files.

These messages are also raised if the rate at which the Resource Monitor frees memory is less than 2 MB every 5 seconds.

These messages are only an indication that the Resource Monitor is busy cleaning up large consumers. These messages do not necessarily indicate a problem with the Resource Monitor itself.

## Resolution

Starting with the following service packs, the non-yielding Resource Monitor message extends to easily isolate the memory clerk that leads to the non-yielding condition:

- Service Pack 2 of Microsoft SQL Server 2008
- Service Pack 1 of Microsoft SQL Server 2008 R2

The new message will resemble the following example:

> Resource Monitor (0x9b0) Worker 0x0000000003A2C1C0 appears to be non-yielding on Node Node_#. Memory freed: 0 KB. Last wait: \> lastwaittype. Last clerk: type clerk_type, name clerk_name. Approx CPU Used: kernel 0 ms, user 0 ms, Interval: Interval_value.

The following are descriptions of the various fields that are used in this message:

- **Memory freed:** This field is how much memory is freed by Resource Monitor for the specified interval. It is measured in kilobytes. If the rate at which the memory is freed does not exceed 2 MB every 5 seconds, the Scheduler Monitor detects this condition as a non-yielding condition.

- **Last wait:** This field is the last wait type for the Resource Monitor thread. You can use this field in combination with the `Approx CPU Used` field. This field combination can identify whether the Resource Monitor thread is running or waiting for a significant part of the interval.

- **Last clerk:** This field is the type and name of the memory clerk that was trimming its memory when the non-yielding condition occurred.

- **Approximate CPU Used:** This field is the kernel and user time that is used by Resource Monitor, as measured in milliseconds. You can use this field together with other fields to verify that Resource Monitor is making progress during the specified interval.

- **Interval:** This field is the time that elapsed since the last clerk was notified as measured in milliseconds.

You can use this message to identify the source of the low memory notification. You can also use the RING_BUFFER_RESOURCE_MONITOR entries from the time of the message.

For more information about how to interpret the RING_BUFFER_RESOURCE MONITOR, see the following MSDN blog:
- [How It Works: What are the RING_BUFFER_RESOURCE_MONITOR telling me?](https://techcommunity.microsoft.com/t5/sql-server-support/how-it-works-what-are-the-ring-buffer-resource-monitor-telling/ba-p/315837)

The Resource Monitor task can troubleshoot memory-related performance issues in SQL Server. SQL Server listens for, and responds to, memory notifications. For more information about these items, see the following MSDN blog articles:
- [The SQL Server Working Set Message](https://techcommunity.microsoft.com/t5/sql-server-support/the-sql-server-working-set-message/ba-p/315418)
- [Troubleshooting Performance Problems in SQL Server 2008](https://msdn.microsoft.com/library/dd672789%28v=sql.100%29.aspx)
