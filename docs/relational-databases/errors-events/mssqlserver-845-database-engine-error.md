---
title: "MSSQLSERVER_845"
description: "MSSQLSERVER_845"
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 05/30/2025
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "845 (Database Engine error)"
---
# MSSQLSERVER_845

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 845 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | BUFLATCH_TIMEOUT |
| Message Text | Time-out occurred while waiting for buffer latch type %d for page %S_PGID, database ID %d. |

## Explanation

A process was waiting to acquire a latch, but the process waited until the time limit expired and failed to acquire one. This can occur if an I/O operation takes too long to complete, usually as a result of other tasks blocking system processes. In some instances, this error might be the result of hardware failure.

## Cause

This error message is dependent on the overall environment of your system. Any of the following circumstances might lead to an overstressed system:

- Hardware that doesn't meet your input/output (I/O) and memory needs
- Improperly configured and tested settings
- Inefficient design

You might observe error 845 when your system is under a heavy load and can't meet the workload demands. Some of the most common causes of a stressed environment are:

- Hardware problems
- Compressed volumes
- Non-default [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] configuration settings
- Inefficient queries or index design
- Frequent database autogrow or autoshrink operations

## User action

Try the following to prevent this error from occurring:

- Determine if you have any hardware bottlenecks. See [Identify Bottlenecks](../performance/identify-bottlenecks.md) for a good place to start. If necessary, upgrade your hardware so it can service the needs of your environment's configuration, queries, and load.

- Verify that all your hardware functions properly. Check for any logged errors and run any diagnostics provided by your hardware vendor. Check for associated I/O failures in error log or event log. I/O failures typically point to a disk malfunction.

- Make sure that your disk volumes aren't compressed. Storing data and log files on compressed drives isn't supported, see [Database files and filegroups](../databases/database-files-and-filegroups.md). For additional information on compressed drive support, review the following article: [Description of support for SQL Server databases on compressed volumes](/troubleshoot/sql/database-engine/database-file-operations/support-databases-compressed-volumes)

- See if the error messages disappear when you turn off all the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] configuration options:
  - The [priority boost option](../../database-engine/configure-windows/configure-the-priority-boost-server-configuration-option.md)
  - The [lightweight pooling (fiber mode) option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md)
  - The [set working set size option](../../database-engine/configure-windows/set-working-set-size-server-configuration-option.md)

- Tune queries to reduce resources used on the system. Performance tuning helps reduce the stress on a system and improve response time for individual queries.

- Set the autoshrink property to OFF to reduce the overhead of changes to your database size.

- Make sure you set the autogrow property to increments that are large enough to be infrequent. Schedule a job to check the available space in your databases, and then increase the database size during off-peak hours.

- Check the error log for non-yielding tasks and other critical errors. Resolve those errors first as they could point to the root cause of the underlying issue.

- If critical errors such as asserts frequently occur, resolve these problems.

- If the 845 error messages are infrequent, then you can ignore the errors.
