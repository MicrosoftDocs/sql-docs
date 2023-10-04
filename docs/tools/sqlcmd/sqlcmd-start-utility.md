---
title: sqlcmd utility - Start the sqlcmd utility
description: Learn how to start the sqlcmd utility, which lets you enter Transact-SQL statements, system procedures, and script files, in SQLCMD mode or in scripts and jobs.
author: dlevy-msft
ms.author: dlevy
ms.reviewer: maghan, randolphwest
ms.date: 08/15/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sqlcmd - Start the utility

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The [**sqlcmd** utility](sqlcmd-utility.md) lets you enter Transact-SQL statements, system procedures, and script files at the command prompt, in [SQLCMD mode](edit-sqlcmd-scripts-query-editor.md) in [SQL Server Management Studio](../../ssms/menu-help/about-sql-server-management-studio.md), and in a Windows script file or in [an operating system (Cmd.exe) job step](../../ssms/agent/create-a-cmdexec-job-step.md) of a SQL Server Agent job.

> [!NOTE]  
> Windows Authentication is the default authentication mode for **sqlcmd**. To use SQL Server Authentication, you must specify a user name and password by using the **-U** and **-P** options.

> [!NOTE]  
> By default, [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] installs as the named instance **sqlexpress**.

## Start the sqlcmd utility and connect to a default instance of SQL Server

1. On the Start menu, select **Run**. In the **Open** box type **cmd**, and then select **OK** to open a Command Prompt window. (If you haven't connected to this instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] before, you may have to configure SQL Server to accept connections.)

1. At the command prompt, type **sqlcmd**.

1. Press ENTER.

     You now have a trusted connection to the default instance of SQL Server that is running on your computer.

     **1>** is the **sqlcmd** prompt that specifies the line number. Each time you press ENTER, the number increases by one.

1. To end the **sqlcmd** session, type **EXIT** at the **sqlcmd** prompt.

## Start the sqlcmd utility and connect to a named instance of SQL Server

1. Open a Command Prompt window, and type **sqlcmd -S**_myServer\instanceName_. Replace *myServer\instanceName* with the name of the computer and the instance of SQL Server that you want to connect to.

1. Press ENTER.

     The **sqlcmd** prompt (`1>`) indicates that you are connected to the specified instance of SQL Server.

    > [!NOTE]  
    > Entered Transact-SQL statements are stored in a buffer. They are executed as a batch when the `GO` command is encountered.

## Next steps

Learn more about sqlcmd and related concepts in the following articles:

- [Use SQLCMD scripting mode in SQL Server Management Studio](edit-sqlcmd-scripts-query-editor.md)
- [Run Transact-SQL Script files using sqlcmd](sqlcmd-run-transact-sql-script-files.md)
- [Use the sqlcmd utility](sqlcmd-use-utility.md)
- [SQL Server utilities statements - GO](../../t-sql/language-elements/sql-server-utilities-statements-go.md)
