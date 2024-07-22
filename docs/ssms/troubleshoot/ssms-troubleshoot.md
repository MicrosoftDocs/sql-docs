---
title: Advanced troubleshooting for SSMS
description: "Get diagnostic data after a SQL Server Management Studio (SSMS) crash"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 05/10/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---

# Advanced troubleshooting for SQL Server Management Studio (SSMS)

[!INCLUDE[Applies to](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

There are scenarios where trying to capture a [memory dump](get-full-memory-dump.md) for SSMS doesn't generate the expected output, and it requires advanced troubleshooting.

The following steps require [Visual Studio](https://visualstudio.microsoft.com/vs/community/)(Community Edition or higher) to be installed.

To capture a diagnostic information with Visual Studio to troubleshoot a crash or an unresponsive SSMS, use the following steps:

1. Open Visual Studio.
1. Select **Continue without code** to open an empty window.
1. Start SSMS, if it's not already open.
1. Select **Debug > Attach to Process...**.
1. In the **Attach to Process** dialog, within the **Filter processes** box, enter SSMS.
1. In the list of processes select SSMS.exe and then **Attach**.
1. An Output window appears, with **Debug** selected for **Show output from:**.
1. Recreate the problematic behavior in SSMS.
1. Once SSMS closes, select **Debug > Save Dump As...** in Visual Studio and save the .dmp file to a folder.
1. Zip up the folder.
1. Stop debugging before closing Visual Studio.

## Share the information

1. To share the information with the SSMS Team, log the issue at https://aka.ms/sqlfeedback.
1. Then share the memory dump file collected to OneDrive (or equivalent) where the file can be collected.

    > [!Important]
    > Memory dump files may contain sensitive information.

## Enable verbose logging

The information logged from SSMS doesn't always provide enough detail for troubleshooting, and verbose logging can be enabled to capture more details.

1. Determine the location of the SSMS executable (ssms.exe). The default location for SSMS 20 is C:\Program Files (x86)\Microsoft SQL Server Management Studio 20\Common7\IDE, but may be different on your machine.
1. Open a command prompt and run the following, using the ssms.exe location in the previous step for the second line.

    ```cmd
    SET VsLogActivity=1
    "C:\Program Files (x86)\Microsoft SQL Server Management Studio 20\Common7\IDE\ssms.exe"
    ```

1. SSMS starts.
1. Open Windows Explorer and navigate to %USERPROFILE%\AppData\Roaming\Microsoft\AppEnv\15.0.
1. Close SSMS to stop the verbose logging.
1. Inspect the ActivityLog.xml file which now contains more details that can assist with troubleshooting.

## Clear SSMS cache files

Data stored in cache files may unexpectedly interfere with SSMS behavior. To rule out this problem, you can clear the files manually.  

1. Close all instances of SSMS.
1. Remove all files in the following folders (it's recommended to make a copy of the RegSrvr*.xml file if you want to retain any entries under **Local Server Groups** in **Registered Servers**).

    * "%USERPROFILE%\AppData\Local\Microsoft\SQL Server Management Studio"
    * "%USERPROFILE%\AppData\Roaming\Microsoft\SQL Server Management Studio"

1. Start SSMS and observe if removing the cache files resolved the issue.

## Related content

[SQL Server Management Studio](../sql-server-management-studio-ssms.md)
