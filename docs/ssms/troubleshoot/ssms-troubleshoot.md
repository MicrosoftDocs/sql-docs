---
title: Troubleshooting an unresponsive system or crash with SSMS
description: "Get diagnostic data after a SQL Server Management Studio (SSMS) crash"
author: markingmyname
ms.author: maghan
ms.reviewer: drskwier
ms.date: 09/18/2019
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---

# Get diagnostic data after a SQL Server Management Studio (SSMS) crash

[!INCLUDE[Applies to](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

## Get full memory dump after an unresponsive system or crash

Get a full memory dump of SQL Server Management Studio (SSMS) when it stops responding or crashes.

To capture diagnostic information to troubleshoot a crash or an unresponsive SSMS, use the following steps:

1. Download [ProcDump](/sysinternals/downloads/procdump).

2. Unzip the download into a folder.

3. Open the command prompt and run the following command.

    ```console
    <PathToProcDumpFolder>\procdump.exe -e -h -ma -w ssms.exe
    ```

    If it prompts you to accept a license agreement, select *Agree*.

4. Start SSMS, if it isn't running already.

5. Reproduce the issue.

6. Wait as the text appears in the cmd prompt about writing the dump file, don't proceed until it has finished.

7. Create a new folder and copy the *.dmp file that is written out to that folder.

8. Copy the following files into the same folder.

    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

9. Zip up the folder

## Get full memory dump for an OutOfMemoryException

Get a full memory dump of SSMS when it throws an OutOfMemoryException.

You can get a full memory dump with any managed exception.

To capture diagnostic information to troubleshoot an OutOfMemoryException from SSMS, use the following steps:

1. Download [ProcDump](/sysinternals/downloads/procdump).

2. Unzip the download into a folder.

3. Open Command Prompt and run the following command.

    ```console
    <PathToProcDumpFolder>\procdump.exe -e 1 -f System.OutOfMemoryException -ma -w ssms.exe
    ```

    If it prompts you to accept a license agreement, select *Agree*.

4. Start SQL Server Management Studio if not started already.

5. Reproduce the issue.

6. Wait as the text appears in the cmd prompt about writing the dump file, don't proceed until it has finished.

7. Create a new folder and copy the *.dmp file that is written out to that folder.

8. Copy the following files into the same folder.

    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscordacwks.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\SOS.dll"
    * "C:\Windows\Microsoft.NET\Framework\v4.0.30319\clr.dll"

9. Zip up the folder.
