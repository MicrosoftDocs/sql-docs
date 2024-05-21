---
title: Get Full Memory Dump to troubleshoot SSMS
description: "Get full memory dump"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 05/10/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---

# Get a full memory dump to troubleshoot SSMS

[!INCLUDE[Applies to](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

In this article, you learn how to capture diagnostic information to troubleshoot a crash or an unresponsive system that you experience in SQL Server Management Studio (SSMS).

## Get a full memory dump after an unresponsive system or crash

Get a full memory dump of SQL Server Management Studio (SSMS) when it stops responding or crashes.

To capture diagnostic information to troubleshoot a crash or an unresponsive SSMS, use the following steps:

1. Download [ProcDump](/sysinternals/downloads/procdump).
1. Unzip the download into a folder.
1. Open a Command Prompt (such as `cmd.exe`), and run the following command.

    ```console
    <PathToProcDumpFolder>\procdump.exe -e -h -ma -w ssms.exe
    ```

    It should prompt you to accept a license agreement, select **Agree**.

1. Start SQL Server Management Studio (SSMS) if not started already.
1. Reproduce your issue.
1. Wait as the text appears in the cmd prompt about writing the dump file, don't proceed until it finishes.
1. Create a new folder and copy the *.dmp file that is written out to that folder.
1. Copy the following files into the same folder.

    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

1. Zip up the folder.

## OutOfMemoryException

You can also get the Full Memory Dump of SSMS when it throws an OutOfMemoryException (can be any managed exception).

To capture diagnostic information to troubleshoot an OutOfMemoryException from SSMS, use the following steps:

1. Download [ProcDump](/sysinternals/downloads/procdump).
1. Unzip the download into a folder.
1. Open Command Prompt and run the following command.

    ```cmd
    <PathToProcDumpFolder>\procdump.exe -e 1 -f System.OutOfMemoryException -ma -w ssms.exe
    ```

    It should prompt you to accept a license agreement, select **Agree**.

1. Start SQL Server Management Studio if not started already.
1. Repro the issue.
1. Wait as the text appears in the cmd prompt about writing the dump file, don't proceed until it finishes.
1. Create a new folder and copy the *.dmp file that is written out to that folder.
1. Copy the following files into the same folder.

    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

1. Zip up the folder.

## Share the information

1. To share the information with the SSMS Team, log the issue at https://aka.ms/sqlfeedback.
1. Then share the memory dump file collected to OneDrive (or equivalent) where the file can be collected.

    > [!Important]
    > Memory dump files may contain sensitive information.

## Next steps

[SQL Server Management Studio](../sql-server-management-studio-ssms.md)
