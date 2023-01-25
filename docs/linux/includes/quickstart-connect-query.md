---
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/20/2022
ms.service: sql
ms.subservice: linux
---
## Connect locally

The following steps use **sqlcmd** to locally connect to your new SQL Server instance.

1. Run **sqlcmd** with parameters for your SQL Server name (`-S`), the user name (`-U`), and the password (`-P`). In this tutorial, you are connecting locally, so the server name is `localhost`. The user name is `sa` and the password is the one you provided for the SA account during setup.

   ```bash
   sqlcmd -S localhost -U sa -P '<YourPassword>'
   ```

   You can omit the password on the command line to be prompted to enter it.

   If you later decide to connect remotely, specify the machine name or IP address for the `-S` parameter, and make sure port 1433 is open on your firewall.

1. If successful, you should get to a **sqlcmd** command prompt: `1>`.

1. If you get a connection failure, first attempt to diagnose the problem from the error message. Then review the [connection troubleshooting recommendations](../sql-server-linux-troubleshooting-guide.md#connection).

## Create and query data

The following sections walk you through using **sqlcmd** to create a new database, add data, and run a simple query.

For more information about writing Transact-SQL statements and queries, see [Tutorial: Writing Transact-SQL Statements](../../t-sql/tutorial-writing-transact-sql-statements.md).

### Create a new database

The following steps create a new database named `TestDB`.

1. From the **sqlcmd** command prompt, paste the following Transact-SQL command to create a test database:

   ```sql
   CREATE DATABASE TestDB;
   ```

1. On the next line, write a query to return the name of all of the databases on your server:

   ```sql
   SELECT Name from sys.databases;
   ```

1. The previous two commands were not executed immediately. You must type `GO` on a new line to execute the previous commands:

   ```sql
   GO
   ```

### Insert data

Next create a new table, `dbo.Inventory`, and insert two new rows.

1. From the **sqlcmd** command prompt, switch context to the new `TestDB` database:

   ```sql
   USE TestDB;
   ```

1. Create new table named `dbo.Inventory`:

   ```sql
   CREATE TABLE dbo.Inventory (
      id INT, name NVARCHAR(50),
      quantity INT
   );
   ```

1. Insert data into the new table:

   ```sql
   INSERT INTO dbo.Inventory VALUES (1, 'banana', 150);
   INSERT INTO dbo.Inventory VALUES (2, 'orange', 154);
   ```

1. Type `GO` to execute the previous commands:

   ```sql
   GO
   ```

### Select data

Now, run a query to return data from the `dbo.Inventory` table.

1. From the **sqlcmd** command prompt, enter a query that returns rows from the `dbo.Inventory` table where the quantity is greater than 152:

   ```sql
   SELECT * FROM dbo.Inventory
   WHERE quantity > 152;
   ```

1. Execute the command:

   ```sql
   GO
   ```

### Exit the sqlcmd command prompt

To end your **sqlcmd** session, type `QUIT`:

```sql
QUIT
```

## Performance best practices

After installing SQL Server on Linux, review the best practices for configuring Linux and SQL Server to improve performance for production scenarios. For more information, see [Performance best practices and configuration guidelines for SQL Server on Linux](../sql-server-linux-performance-best-practices.md).

## Cross-platform data tools

In addition to **sqlcmd**, you can use the following cross-platform tools to manage SQL Server:

| Tool | Description |
| ---- | ----------- |
| [Azure Data Studio](../../azure-data-studio/index.yml) | A cross-platform GUI database management utility. |
| [Visual Studio Code](../../tools/visual-studio-code/sql-server-develop-use-vscode.md) | A cross-platform GUI code editor that run Transact-SQL statements with the mssql extension. |
| [PowerShell Core](../sql-server-linux-manage-powershell-core.md) | A cross-platform automation and configuration tool based on cmdlets. |
| [mssql-cli](https://github.com/dbcli/mssql-cli/tree/master/doc) | A cross-platform command-line interface for running Transact-SQL commands. |

## Connecting from Windows

SQL Server tools on Windows connect to SQL Server instances on Linux in the same way they would connect to any remote SQL Server instance.

If you have a Windows machine that can connect to your Linux machine, try the same steps in this topic from a Windows command-prompt running **sqlcmd**. You must use the target Linux machine name or IP address rather than `localhost`, and make sure that TCP port 1433 is open on the SQL Server machine. If you have any problems connecting from Windows, see [connection troubleshooting recommendations](../sql-server-linux-troubleshooting-guide.md#connection).

For other tools that run on Windows but connect to SQL Server on Linux, see:

- [SQL Server Management Studio (SSMS)](../sql-server-linux-manage-ssms.md)
- [Windows PowerShell](../sql-server-linux-manage-powershell.md)
- [SQL Server Data Tools (SSDT)](../sql-server-linux-develop-use-ssdt.md)

## Other deployment scenarios

For other installation scenarios, see the following resources:

- [Upgrade](../sql-server-linux-setup.md#upgrade): Learn how to upgrade an existing installation of SQL Server on Linux
- [Uninstall](../sql-server-linux-setup.md#uninstall): Uninstall SQL Server on Linux
- [Unattended install](../sql-server-linux-setup.md#unattended): Learn how to script the installation without prompts
- [Offline install](../sql-server-linux-setup.md#offline): Learn how to manually download the packages for offline installation

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](../sql-server-linux-faq.yml).

## Next steps

- [Explore the tutorials for SQL Server on Linux](../sql-server-linux-migrate-restore-database.md).
