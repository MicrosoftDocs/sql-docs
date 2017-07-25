## Connect locally

The following steps use **sqlcmd** to locally connect to your new SQL Server instance.

1. Run **sqlcmd** with parameters for your SQL Server name (-S), the user name (-U), and the password (-P). In this tutorial, you are connecting locally, so the server name is `localhost`. The user name is `SA` and the password is the one you provided for the SA account during setup.

   ```bash
   sqlcmd -S localhost -U SA -P '<YourPassword>'
   ```

   > [!TIP]
   > You can omit the password on the command line to be prompted to enter it.

   > [!TIP]
   > If you later decide to connect remotely, specify the machine name or IP address for the **-S** parameter, and make sure port 1433 is open on your firewall.

1. If successful, you should get to a **sqlcmd** command prompt: `1>`.

1. If you get a connection failure, first attempt to diagnose the problem from the error message. Then review the [connection troubleshooting recommendations](../linux/sql-server-linux-troubleshooting-guide.md#connection).

## Create and query data
The following sections walk you through using **sqlcmd** to create a new database, add data, and run a simple query.

### Create a new database

The following steps create a new database named `TestDB`.

1. From the **sqlcmd** command prompt, paste the following Transact-SQL command to create a test database:

   ```sql
   CREATE DATABASE TestDB
   ```

1. On the next line, write a query to return the name of all of the databases on your server:

   ```sql
   SELECT Name from sys.Databases
   ```

1. The previous two commands were not executed immediately. You must type `GO` on a new line to execute the previous commands:

   ```sql
   GO
   ```

### Insert data

Next create a new table, `Inventory`, and insert two new rows.

1. From the **sqlcmd** command prompt, switch context to the new `TestDB` database:

   ```sql
   USE TestDB
   ```

1. Create new table named `Inventory`:

   ```sql
   CREATE TABLE Inventory (id INT, name NVARCHAR(50), quantity INT)
   ```

1. Insert data into the new table:

   ```sql
   INSERT INTO Inventory VALUES (1, 'banana', 150); INSERT INTO Inventory VALUES (2, 'orange', 154);
   ```

1. Type `GO` to execute the previous commands:

   ```sql
   GO
   ```

### Select data

Now, run a query to return data from the `Inventory` table.

1. From the **sqlcmd** command prompt, enter a query that returns rows from the `Inventory` table where the quantity is greater than 152:

   ```sql
   SELECT * FROM Inventory WHERE quantity > 152;
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

## Connect from Windows

SQL Server tools on Windows connect to SQL Server instances on Linux in the same way they would connect to any remote SQL Server instance.

If you have a Windows machine that can connect to your Linux machine, try the same steps in this topic from a Windows command-prompt running **sqlcmd**. Just verify that you use the target Linux machine name or IP address rather than localhost, and make sure that TCP port 1433 is open. If you have any problems connecting from Windows, see [connection troubleshooting recommendations](../linux/sql-server-linux-troubleshooting-guide.md#connection).

For other tools that run on Windows but connect to SQL Server on Linux, see:

- [SQL Server Management Studio (SSMS)](../linux/sql-server-linux-develop-use-ssms.md)
- [Windows PowerShell](../linux/sql-server-linux-manage-powershell.md)
- [SQL Server Data Tools (SSDT)](../linux/sql-server-linux-develop-use-ssdt.md)

## Next steps

For other installation scenarios, see the following resources:

|||
|---|---|
| [Upgrade](../linux/sql-server-linux-setup.md#upgrade) | Learn how to upgrade and existing installation of SQL Server on Linux |
| [Uninstall](../linux/sql-server-linux-setup.md#uninstall) | Uninstall SQL Server on Linux |
| [Unattended install](../linux/sql-server-linux-setup.md#unattended) | Learn how to script the installation without prompts |
| [Offline install](../linux/sql-server-linux-setup.md#offline) | Learn how to manually download the packages for offline installation |

To explore other ways to connect and manage SQL Server, see [Visual Studio Code](../linux/sql-server-linux-develop-use-vscode.md) and  [SQL Server Management Studio](../linux/sql-server-linux-develop-use-ssms.md).

To learn more about writing Transact-SQL statements and queries, see [Tutorial: Writing Transact-SQL Statements](../t-sql/tutorial-writing-transact-sql-statements.md).