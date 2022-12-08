---
title: "MSSQLSERVER_912"
description: The database script level could not be upgraded to the latest required by the server.
ms.custom: ""
ms.date: "12/01/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "912 (Database Engine error)"
author: PijoCoder
ms.author: jopilov
---
# MSSQLSERVER_912
 [!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sqlserver2019-windows-only.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|912|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DB_RUNSCRIPTUPGRADE_STEP_FAILED|  
|Message Text|	Script level upgrade for database '%.*ls' failed because upgrade step '%.*ls' encountered error %d, state %d, severity %d. This is a serious error condition which might interfere with regular operation and the database will be taken offline. If the error happened during upgrade of the 'master' database, it will prevent the entire SQL Server instance from starting. Examine the previous errorlog entries for errors, take the appropriate corrective actions and re-start the database so that the script upgrade steps run to completion.|  

## Explanation

Error 912 indicates that the database script failed to be executed and to upgrade the database(s) to the latest level required by the server. It is a general error message that contains a reference to the upgrade script that failed and what error the failed script encountered.

When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is upgraded or a Cumulative Update is applied, only the binaries are upgraded initially. The database and its objects remain unmodified. Once the binaries are replaced with new versions and the service restarts for the first time, a database upgrade is started. The upgrade scripts to be executed are located under C:\Program Files\Microsoft SQL Server\MSSQLXX.YYYY\MSSQL\Install\.

If the upgrade process encounters script-level upgrade errors (Error 912), other errors may be raised. For example, these errors may accompany error 912 and help further explain the failure:

```output
Error: 1101, Severity: 17, State: 1.
Could not allocate a new page for database 'tempdb' because of insufficient disk space in filegroup 'PRIMARY'. Create the necessary space by dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup.

Error: 912, Severity: 21, State: 2.
Script level upgrade for database 'master' failed because upgrade step 'xxx.sql' encountered error <Error Number>, state <Error State>, severity <Error Severity>. This is a serious error condition which might interfere with regular operation and the database will be taken offline. If the error happened during upgrade of the 'master' database, it will prevent the entire SQL Server instance from starting. Examine the previous errorlog entries for errors, take the appropriate corrective actions and re-start the database so that the script upgrade steps run to completion.

Error: 3417, Severity: 21, State: 3.
Cannot recover the master database. SQL Server is unable to run. Restore master from a full backup, repair it, or rebuild it. For more information about how to rebuild the master database, see SQL Server Books Online.
```

Commonly when the installation process fails, the user may see the following error in the GUI, assuming the installation is done manually using the Wizard. Keep in mind that this error may be raised with a wide variety of installation issues. But in all cases it directs you to check the SQL Server error log for more information.

```output
Wait on the Database Engine recovery handle failed. Check the SQL Server error log for potential causes.
```

:::image type="content" source="../media/upgrade-errors-check-errorlog.png" alt-text="upgrade failure in the GUI":::

## User action  
  
To find the cause of the issue, follow these steps:
1. Locate and open the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Errorlog](../../tools/configuration-manager/viewing-the-sql-server-error-log.md). 
1. Examine the log for errors that occurred immediately before error 912 and focus on troubleshooting the error referenced in the messaging of Error 912. 
1. For some common scenarios that Microsoft customers have reported see, ["Wait on Database Engine recovery handle failed" and "912" and "3417" errors](/troubleshoot/sql/install/sqlserver-patching-issues#wait-on-database-engine-recovery-handle-failed-and-912-and-3417-errors)
1. In some cases, as part of the process, you may need to start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service with [trace flag 902](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf902) (see steps below). Starting the service with T902 allows the service to skip execution of the upgrade scripts during startup. That way, you get a chance to investigate and fix the underlying issue.
1. Be sure to remove the trace flag once you have resolved the issue so the setup process can restart the upgrade script execution phase.


### Steps to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with trace flag 902 

#### Using Configuration Manager

1. Launch SQL Server Configuration Manager.
1. Select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance in **SQL Server Services**.
1. Right-click the instance, and then select **Properties**.
1. Select the **Startup Parameters** tab.
1. Use the **Specify a startup parameter** field to add the trace flag. Type "-T902" (without quotes) and click **Add**.
1. Select **OK** and close instance properties.
1. Start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.

For more information on how to configure startup-options, see [SQL Configuration Manager Services - Configure Server Startup Options](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md)

>[!NOTE]
> Be sure to remove -T902 from the configuration once you have resolved the issue.

#### Command prompt using sqlservr.exe

1. Open a command prompt with administrative privileges and change directory to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Binn directory, for example, C:\Program Files\Microsoft SQL Server\MSSQLXX.YYYY\MSSQL\Binn\.

1. Execute the `sqlservr.exe -s <instance> -T902`

   **Default Instance:**

    ```console
    cd \Program Files\Microsoft SQL Server\MSSQL<version>\MSSQL\Binn
    sqlservr.exe -s MSSQLSERVER  -T902
    ```

    **Named Instance, where "sql2016" is an example of an instance name:**

    ```console
    cd \Program Files\Microsoft SQL Server\MSSQL<version>.<instance name>\MSSQL\Binn
    sqlservr.exe -s sql2016  -T902
    ```

1. To stop the instance when you finished, press `CTRL+C` , then `Y`

#### Command prompt using `net start`

**Default Instance:**

```console
NET START MSSQLSERVER /T902 
```

**Named Instance:**

```console
NET START MSSQL$INSTANCENAME  /T902
```

