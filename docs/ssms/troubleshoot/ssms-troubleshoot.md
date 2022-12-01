---
description: "Get diagnostic data after a SQL Server Management Studio (SSMS) crash"
title: Troubleshooting an unresponsive system or crash with SSMS
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: c28ffa44-7b8b-4efa-b755-c7a3b1c11ce4
author: markingmyname
ms.author: maghan
ms.reviewer: drskwier
ms.custom: seo-lt-2019
ms.date: 09/18/2019
---

# Get diagnostic data after a SQL Server Management Studio (SSMS) crash

[!INCLUDE[Applies to](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

## Get full memory dump after an unresponsive system or crash

Get a full memory dump of SQL Server Management Studio (SSMS) when it stops responding or crashes.

To capture diagnostic information to troubleshoot a crash or an unresponsive SSMS, follow the steps below.

1. Download [ProcDump](/sysinternals/downloads/procdump).

2. Unzip the download into a folder.

3. Open the command prompt and run the following command.

    ```console
    <PathToProcDumpFolder>\procdump.exe -e -h -ma -w ssms.exe
    ```

    If it prompts you to accept a license agreement, select *Agree*.

4. Start SSMS, if it hasn't started already.

5. Reproduce the issue.

6. The text should appear in the cmd prompt about writing the dump file, wait for that to finish.

7. Create a new folder and copy the *.dmp file that is written out to that folder.

8. Copy the following files into the same folder.

    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

9. Zip up the folder

## Get full memory dump for an OutOfMemoryException

Get a full memory dump of SSMS when it throws an OutOfMemoryException.

You can get a full memory dump with any managed exception.

To capture diagnostic information to troubleshoot an OutOfMemoryException from SSMS, follow the steps below.

1. Download [ProcDump](/sysinternals/downloads/procdump).

2. Unzip the download into a folder.

3. Open Command Prompt and run the following command.

    ```console
    <PathToProcDumpFolder>\procdump.exe -e 1 -f System.OutOfMemoryException -ma -w ssms.exe
    ```

    If it prompts you to accept a license agreement, select *Agree*.

4. Start SQL Server Management Studio if not started already.

5. Reproduce the issue.

6. The text should appear in the cmd prompt about writing the dump file, wait for that to finish.

7. Create a new folder and copy the *.dmp file that is written out to that folder.

8. Copy the following files into the same folder.

    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

9. Zip up the folder.