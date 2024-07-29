---
title: Start the sqlcmd utility
description: Learn how to start the sqlcmd utility, which lets you enter Transact-SQL statements, system procedures, and script files, in SQLCMD mode or in scripts and jobs.
author: dlevy-msft
ms.author: dlevy
ms.reviewer: maghan, randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Start the sqlcmd utility

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The [**sqlcmd** utility](sqlcmd-utility.md) lets you enter Transact-SQL statements, system procedures, and script files at the command prompt, in [Edit SQLCMD Scripts with Query Editor](edit-sqlcmd-scripts-query-editor.md) in [About SQL Server Management Studio](../../ssms/menu-help/about-sql-server-management-studio.md), and in a Windows script file or in [an operating system (Cmd.exe) job step](../../ssms/agent/create-a-cmdexec-job-step.md) of a SQL Server Agent job.

> [!NOTE]  
> Windows Authentication is the default authentication mode for **sqlcmd**. To use SQL Server Authentication, you must specify a user name and password by using the `-U` and `-P` options.

By default, [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] installs as the named instance `sqlexpress`.

## Start the sqlcmd utility and connect to a default instance of SQL Server

1. On the Start menu, select **Run**. In the **Open** box type **cmd**, and then select **OK** to open a Command Prompt window. (If you haven't connected to this instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] before, you might have to configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to accept connections.)

1. At the command prompt, type `sqlcmd`.

1. Press `ENTER`.

     You now have a trusted connection to the default instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that is running on your computer.

     `1>` is the **sqlcmd** prompt that specifies the line number. Each time you press `ENTER`, the number increases by one.

1. To end the **sqlcmd** session, type `EXIT` at the **sqlcmd** prompt.

## Start the sqlcmd utility and connect to a named instance of SQL Server

1. Open a Command Prompt window, and type `sqlcmd -S<myServer\instanceName>`. Replace `<myServer\instanceName>` with the name of the computer and the instance of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] that you want to connect to.

1. Press `ENTER`.

   The **sqlcmd** prompt (`1>`) indicates that you're connected to the specified instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

   The Transact-SQL statements you enter are stored in a buffer. They're executed as a batch when the `GO` command is encountered.

## Related content

- [Edit SQLCMD Scripts with Query Editor](edit-sqlcmd-scripts-query-editor.md)
- [sqlcmd - Run Transact-SQL script files](sqlcmd-run-transact-sql-script-files.md)
- [sqlcmd - use the utility](sqlcmd-use-utility.md)
- [SQL Server Utilities Statements - GO](../../t-sql/language-elements/sql-server-utilities-statements-go.md)
