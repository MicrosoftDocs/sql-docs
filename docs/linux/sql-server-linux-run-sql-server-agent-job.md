---
# required metadata

title: Create and run jobs for SQL Server on Linux | Microsoft Docs
description: This tutorial shows how to run SQL Server Agent job on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 03/15/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 1d93d95e-9c89-4274-9b3f-fa2608ec2792

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Create and run SQL Server Agent jobs on Linux

Intro with pointer to [SQL Server Agent installation topic](sql-server-linux-setup-sql-agent.md). Caveats on supportability at this time. 

## Create a job with Transact-SQL

The following steps provide an example of how to create a SQL Server Agent job on Linux with Transact-SQL commands. You can use any T-SQL client to run these commands. For example, on Linux you can use [sqlcmd](sql-server-linux-connect-and-query-sqlcmd.md) or [Visual Studio Code](sql-server-linux-develop-use-vscode.md). From a remote Windows Server, you can also run queries in SQL Server Management Studio (SSMS) or use the UI interface for job management, which is described in the next section.

1. Create the job.

    ```tsql
    tbd
    ```

2. Add one or more job steps.

    ```tsql
    tbd
    ```

3. Create a job schedule.

    ```tsql
    tbd
    ```

4. Attach the job schedule to the job.

    ```tsql
    tbd
    ```

5. Assign the job to a target server. 

    ```tsql
    tbd
    ```

## Create a job with SSMS

You can also create and manage jobs remotely using SQL Server Management Studio (SSMS) on Windows.

TBD: Screenshot/potential steps/general description.

## Next Steps

Pointer to technical docs on [SQL Server Agent](https://docs.microsoft.com/sql/ssms/agent/sql-server-agent).
