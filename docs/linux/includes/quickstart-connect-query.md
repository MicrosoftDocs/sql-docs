---
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/11/2023
ms.service: sql
ms.subservice: linux
ms.custom:
  - linux-related-content
---
## Connect locally

The following steps use **sqlcmd** to locally connect to your new [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

1. Run **sqlcmd** with parameters for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] name (`-S`), the user name (`-U`), and the password (`-P`). In this tutorial, you are connecting locally, so the server name is `localhost`. The user name is `sa` and the password is the one you provided for the SA account during setup.

   ```bash
   sqlcmd -S localhost -U sa -P '<YourPassword>'
   ```

   > [!NOTE]  
   > Newer versions of **sqlcmd** are secure by default. For more information about connection encryption, see [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md) for Windows, and [Connecting with sqlcmd](../../connect/odbc/linux-mac/connecting-with-sqlcmd.md) for Linux and macOS. If the connection doesn't succeed, you can add the `-No` option to **sqlcmd** to specify that encryption is optional, not mandatory.

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
       id INT,
       name NVARCHAR(50),
       quantity INT,
       PRIMARY KEY (id)
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

After installing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux, review the best practices for configuring Linux and [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to improve performance for production scenarios. For more information, see [Performance best practices and configuration guidelines for SQL Server on Linux](../sql-server-linux-performance-best-practices.md).

## Cross-platform data tools

In addition to **sqlcmd**, you can use the following cross-platform tools to manage [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

| Tool | Description |
| --- | --- |
| [Azure Data Studio](../../azure-data-studio/index.yml) | A cross-platform GUI database management utility. |
| [Visual Studio Code](../../tools/visual-studio-code/sql-server-develop-use-vscode.md) | A cross-platform GUI code editor that run Transact-SQL statements with the mssql extension. |
| [PowerShell Core](../sql-server-linux-manage-powershell-core.md) | A cross-platform automation and configuration tool based on cmdlets. |
| [mssql-cli](https://github.com/dbcli/mssql-cli/tree/master/doc) | A cross-platform command-line interface for running Transact-SQL commands. |

## Connect from Windows

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] tools on Windows connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances on Linux in the same way they would connect to any remote [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

If you have a Windows machine that can connect to your Linux machine, try the same steps in this topic from a Windows command-prompt running **sqlcmd**. You must use the target Linux machine name or IP address rather than `localhost`, and make sure that TCP port 1433 is open on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] machine. If you have any problems connecting from Windows, see [connection troubleshooting recommendations](../sql-server-linux-troubleshooting-guide.md#connection).

For other tools that run on Windows but connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux, see:

- [SQL Server Management Studio (SSMS)](../sql-server-linux-manage-ssms.md)
- [Windows PowerShell](../sql-server-linux-manage-powershell.md)
- [SQL Server Data Tools (SSDT)](../sql-server-linux-develop-use-ssdt.md)

## Other deployment scenarios

For other installation scenarios, see the following resources:

- [Upgrade](../sql-server-linux-setup.md#upgrade): Learn how to upgrade an existing installation of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux
- [Uninstall](../sql-server-linux-setup.md#uninstall): Uninstall [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux
- [Unattended install](../sql-server-linux-setup.md#unattended): Learn how to script the installation without prompts
- [Offline install](../sql-server-linux-setup.md#offline): Learn how to manually download the packages for offline installation

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](../sql-server-linux-faq.yml).

## Related content

- [Explore the tutorials for SQL Server on Linux](../sql-server-linux-migrate-restore-database.md)

[!INCLUDE [contribute-to-content](../../includes/paragraph-content/contribute-to-content.md)]
