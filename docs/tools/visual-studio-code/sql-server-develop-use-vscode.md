---
title: Use the Visual Studio Code mssql extension
description: Use the mssql extension for Visual Studio Code to edit and run Transact-SQL scripts on Windows, macOS, and Linux.
ms.topic: conceptual
ms.custom:
- event-tier1-build-2022
ms.service: sql
ms.subservice: tools-other
ms.assetid: 9766ee75-32d3-4045-82a6-4c7968bdbaa6
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 5/24/2022
---

# SQL Server extension for Visual Studio Code

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article shows how to use the **mssql** extension for Visual Studio Code (Visual Studio Code) to work with databases in SQL Server on Windows, macOS, and Linux, as well as Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics. The [mssql extension for Visual Studio Code](https://aka.ms/mssql-marketplace) lets you connect to a SQL Server, query with Transact-SQL (T-SQL), and view the results.

## Create or open a SQL file

The mssql extension enables mssql commands and T-SQL IntelliSense in the code editor when the language mode is set to **SQL**.

1. Select **File** > **New File** or press **Ctrl**+**N**. Visual Studio Code opens a new Plain Text file by default. 

2. Select **Plain Text** on the lower status bar, or press **Ctrl**+**K** > **M**, and select **SQL** from the languages dropdown. 

   ![Screenshot of Visual Studio Code G U I, SQL language mode.](./media/sql-server-develop-use-vscode/vscode-language-mode.png)

   > [!NOTE]
   > If this is the first time you have used the extension, the extension installs the SQL tools service in the background.

If you open an existing file that has a *.sql* file extension, the language mode is automatically set to SQL.  

## Connect to SQL Server

Follow these steps to create a connection profile and connect to a SQL Server.

1. Press **Ctrl**+**Shift**+**P** or **F1** to open the **Command Palette**. 

2. Type *sql* to display the mssql commands, or type *sqlcon*, and then select **MS SQL: Connect** from the dropdown.

   ![Screenshot of Visual Studio Code G U I, m s s q l commands.](./media/sql-server-develop-use-vscode/vscode-commands.png)

   >[!NOTE]
   >A SQL file, such as the empty SQL file you created, must have focus in the code editor before you can execute the mssql commands.

3. Select the **MS SQL: Manage Connection Profiles** command.

4. Then select **Create** to create a new connection profile for your SQL Server.

5. Follow the prompts to specify the properties for the new connection profile. After specifying each value, press **Enter** to continue.

   | Connection property | Description |
   |---|---|
   | **Server name or ADO connection string** | Specify the SQL Server instance name. Use *localhost* to connect to a SQL Server instance on your local machine. To connect to a remote SQL Server, enter the name of the target SQL Server, or its IP address. To connect to a SQL Server container, specify the IP address of the container's host machine. If you need to specify a port, use a comma to separate it from the name. For example, for a server listening on port 1401, enter `<servername or IP>,1401`.<br/><br/>By default, the connection string uses port 1433. A default instance of SQL Server uses 1433 unless modified. If your instance is listening on 1433, you do not need to specify the port.<br/><br/>As an alternative, you can enter the ADO connection string for your database here. |
   | **Database name** (optional) | The database that you want to use. To connect to the default database, don't specify a database name here. |
   | **Authentication Type** | Choose either **Integrated** or **SQL Login**. |
   | **User name** | If you selected **SQL Login**, enter the name of a user with access to a database on the server. |
   | **Password** | Enter the password for the specified user. |
   | **Save Password** | Press **Enter** to select **Yes** and save the password. Select **No** to be prompted for the password each time the connection profile is used. |
   | **Profile Name** (optional) | Type a name for the connection profile, such as *localhost profile*. |

   After you enter all values and select **Enter**, Visual Studio Code creates the connection profile and connects to the SQL Server.

   > [!TIP]
   > If the connection fails, try to diagnose the problem from the error message in the **Output** panel in Visual Studio Code. To open the **Output** panel, select **View** > **Output**. Also review the [connection troubleshooting recommendations](../../linux/sql-server-linux-troubleshooting-guide.md).

6. Verify your connection in the lower status bar.

   ![Screenshot of Visual Studio Code G U I, Connection status.](./media/sql-server-develop-use-vscode/vscode-connection-status.png)

As an alternative to the previous steps, you can also create and edit connection profiles in the User Settings file (*settings.json*). To open the settings file, select **File** > **Preferences** > **Settings**. For more information, see [Manage connection profiles](https://github.com/Microsoft/vscode-mssql/wiki/manage-connection-profiles).

## Create a database

1. In the new SQL file that you started earlier, type *sql* to display a list of editable code snippets.

   ![Screenshot of editor in Visual Studio Code, SQL snippets.](./media/sql-server-develop-use-vscode/vscode-sql-snippets.png)

2. Select **sqlCreateDatabase**.

3. In the snippet, type `TutorialDB` to replace 'DatabaseName':

   ```sql
   -- Create a new database called 'TutorialDB'
   -- Connect to the 'master' database to run this snippet
   USE master
   GO
   IF NOT EXISTS (
      SELECT name
      FROM sys.databases
      WHERE name = N'TutorialDB'
   )
   CREATE DATABASE [TutorialDB]
   GO
   ```

4. Press **Ctrl**+**Shift**+**E** to execute the Transact-SQL commands. View the results in the query window.

    ![Screenshot of Visual Studio code G U I , create database messages.](./media/sql-server-develop-use-vscode/vscode-create-database-messages.png)

    > [!TIP]
    > You can customize the shortcut keys for the mssql commands. See [Customize shortcuts](https://github.com/Microsoft/vscode-mssql/wiki/customize-shortcuts).

## Create a table

1. Delete the contents of the code editor window.

2. Press **Ctrl**+**Shift**+**P** or **F1** to open the **Command Palette**.

3. Type *sql* to display the mssql commands, or type *sqluse*, and then select the **MS SQL: Use Database** command.

4. Select the new **TutorialDB** database.

   ![Screenshot of Visual Studio code G U I , choosing a database.](./media/sql-server-develop-use-vscode/vscode-use-database.png)

5. In the code editor, type *sql* to display the snippets, select **sqlCreateTable**, and then press **Enter**.

6. In the snippet, type `Employees` for the table name.

7. Press **Tab** to get to the next field, and then type `dbo` for the schema name.

8. Replace the column definitions with the following columns:

   ```sql
   EmployeesId INT NOT NULL PRIMARY KEY,
   Name [NVARCHAR](50)  NOT NULL,
   Location [NVARCHAR](50)  NOT NULL
   ```

9. Press **Ctrl**+**Shift**+**E** to create the table.

## Insert and query

1. Add the following statements to insert four rows into the **Employees** table.

   ```sql
   -- Insert rows into table 'Employees'
   INSERT INTO Employees
      ([EmployeesId],[Name],[Location])
   VALUES
      ( 1, N'Jared', N'Australia'),
      ( 2, N'Nikita', N'India'),
      ( 3, N'Tom', N'Germany'),
      ( 4, N'Jake', N'United States')
   GO
   -- Query the total count of employees
   SELECT COUNT(*) as EmployeeCount FROM dbo.Employees;
   -- Query all employee information
   SELECT e.EmployeesId, e.Name, e.Location
   FROM dbo.Employees as e
   GO
   ```

   While you type, T-SQL IntelliSense helps you to complete the statements:

   ![Screenshot of Visual Studio Code U I , T-SQL IntelliSense.](./media/sql-server-develop-use-vscode/vscode-intellisense.png)

   > [!TIP]
   > The mssql extension also has commands to help create INSERT and SELECT statements. These were not used in the previous example.

2. Press **Ctrl**+**Shift**+**E** to execute the commands. The two result sets display in the **Results** window.

   ![Screenshot of Visual Studio Code U I, the Results pane.](./media/sql-server-develop-use-vscode/vscode-result-grid.png)

## View and save the result

1. Select **View** > **Editor Layout** > **Flip Layout** to switch to a vertical or horizontal split layout.

2. Select the **Results** and **Messages** panel headers to collapse and expand the panels.

   ![Screenshot of Visual Studio Code U I, Toggle headers.](./media/sql-server-develop-use-vscode/vscode-toggle-messages-pannel.png)

   > [!TIP]
   > You can customize the default behavior of the mssql extension. See [Customize extension options](https://github.com/Microsoft/vscode-mssql/wiki/customize-options).

3. Select the maximize grid icon on the second result grid to zoom in to those results.

   ![Screenshot of Visual Studio Code U I, Maximize grid.](./media/sql-server-develop-use-vscode/vscode-maximize-grid.png)

   > [!NOTE]
   > The maximize icon displays when your T-SQL script produces two or more result grids.

4. Open the grid context menu by right-clicking on the grid.

   ![Screenshot of Visual Studio Code U I, Context menu.](./media/sql-server-develop-use-vscode/vscode-grid-context-menu.png)

5. Select **Select All**.

6. Open the grid context menu again and select **Save as JSON** to save the result to a *.json* file.

7. Specify a file name for the JSON file.

8. Verify that the JSON file saves and opens in Visual Studio Code.

   ![Screenshot of editor in Visual Studio Code U I, Save as J SON.](./media/sql-server-develop-use-vscode/vscode-save-as-json.png)

If you need to save and run SQL scripts later, for administration or a larger development project, save the scripts with a *.sql* extension.

## Next steps

- If you're new to T-SQL, see [Tutorial: Write Transact-SQL statements](../../t-sql/tutorial-writing-transact-sql-statements.md) and the [Transact-SQL Reference (Database Engine)](../../t-sql/language-reference.md).
- Develop for SQL databases in Visual Studio Code with the [SQL Database Projects extension](../../azure-data-studio/extensions/sql-database-project-extension.md)
- For more information on using or contributing to the mssql extension, see the [mssql extension project wiki](https://github.com/Microsoft/vscode-mssql/wiki).
- For more information on using Visual Studio Code, see the [Visual Studio Code documentation](https://code.visualstudio.com/docs).
