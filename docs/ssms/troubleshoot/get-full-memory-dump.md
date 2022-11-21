---
description: "Get Full Memory Dump"
title: Get Full Memory Dump to troubleshoot SSMS
Description: Capture diagnostic information from SQL Server Management Studio (SSMS) so you can troubleshoot a crash or an unresponsive system.
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: c28ffa44-7b8b-4efa-b755-c7a3b1c11ce4
author: markingmyname
ms.author: maghan
ms.reviewer: drskwier
ms.custom: seo-lt-2019
ms.date: 05/17/2019
---

# Get Full Memory Dump

[!INCLUDE[Applies to](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

In this article, you learn how to capture diagnostic information to troubleshoot a crash or a unresponsive system that you experienced from SQL Server Management Studio (SSMS).

To capture diagnostic information to troubleshoot, follow the steps below.

1. Download [ProcDump](/sysinternals/downloads/procdump).

2. Unzip the download into a folder.

3. Open a Command Prompt (such as `cmd.exe`), and run the following command.

    ```console
    <PathToProcDumpFolder>\procdump.exe -e -h -ma -w ssms.exe
    ```

    It should prompt you to accept a license agreement, select **Agree**.

4. Start SQL Server Management Studio (SSMS) if not started already.

5. Reproduce your issue.

6. The text should appear in the cmd prompt about writing the dump file, wait for that to finish.

7. Create a new folder and copy the *.dmp file that is written out to that folder.

8. Copy the following files into the same folder.

    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

9. Zip up the folder.

## OutOfMemoryException

You can also get the Full Memory Dump of SSMS when it throws an OutOfMemoryException (can be any managed exception).

To capture diagnostic information to troubleshoot an OutOfMemoryException from SSMS, Follow the steps below.

1. Download [ProcDump](/sysinternals/downloads/procdump).

2. Unzip the download into a folder.

3. Open Command Prompt and run the following command.

    ```cmd
    <PathToProcDumpFolder>\procdump.exe -e 1 -f System.OutOfMemoryException -ma -w ssms.exe
    ```

    It should prompt you to accept a license agreement, select **Agree**.

4. Start SQL Server Management Studio if not started already.

5. Repro the issue.

6. The text should appear in the cmd prompt about writing the dump file, wait for that to finish.

7. Create a new folder and copy the *.dmp file that is written out to that folder.

8. Copy the following files into the same folder.

    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

9. Zip up the folder.

## Share the information

1. To share the information with the SSMS Team, log the issue at https://aka.ms/sqlfeedback.

2. Then share the memory dump file collected to OneDrive (or equivalent) where the file can be collected.

    > [!Important]
    > Memory dump files may contain sensitive information.

## Next steps

[SQL Server Management Studio](../sql-server-management-studio-ssms.md)