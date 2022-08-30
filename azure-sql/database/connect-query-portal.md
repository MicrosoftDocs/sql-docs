---
title: Query SQL Database with query editor in the Azure portal
titleSuffix: Azure SQL Database
description: Learn how to connect to an Azure SQL database and use the Azure portal query editor (preview) to run Transact-SQL (T-SQL) queries.
author: mbarickman
ms.author: mbarickman
ms.reviewer: wiassaf, mathoma
ms.date: 06/21/2022
ms.service: sql-database
ms.subservice: development
ms.topic: quickstart
ms.custom:
  - sqldbrb=1
  - contperf-fy21q3-portal
  - mode-ui
  - kr2b-contr-experiment
keywords:
  - connect to sql database
  - query sql database
  - azure portal
  - portal
  - query editor
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Quickstart: Use the Azure portal query editor (preview) to query Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Query editor (preview) is a tool to run SQL queries against Azure SQL Database in the Azure portal. In this quickstart, you connect to an Azure SQL database in the portal and use query editor to run Transact-SQL (T-SQL) queries.

## Prerequisites

- The AdventureWorksLT sample Azure SQL database. If you don't have it, you can [create a database in Azure SQL Database](single-database-create-quickstart.md) that has the AdventureWorks sample data.

- A user account with permissions to connect to the database and query editor. You can either:

  - Have or set up a user that can connect to the database with SQL authentication.
  - Set up an Azure Active Directory (Azure AD) administrator for the database's [SQL server](logical-servers.md).

    An Azure AD server administrator can use a single identity to sign in to the Azure portal and the SQL server and databases. To set up an Azure AD server admin:

    1. In the [Azure portal](https://portal.azure.com), on your Azure SQL database **Overview** page, select **Server name** under **Essentials** to navigate to the server for your database.
    1. On the server page, select **Azure Active Directory** in the **Settings** section of the left menu.
    1. On the **Azure Active Directory** page toolbar, select **Set admin**.

       :::image type="content" source="./media/connect-query-portal/select-active-directory.png" alt-text="Screenshot showing the Set admin selection.":::

    1. On the **Azure Active Directory** form, search for and select the user or group you want to be the admin, and then select **Select**.
    1. On the **Azure Active Directory** main page, select **Save**.

    > [!NOTE]
    > - Email addresses like outlook.com or gmail.com aren't supported as Azure AD admins. The user must either be created natively in the Azure AD or federated into the Azure AD.
    > - Azure AD admin sign-in works with accounts that have two-factor authentication enabled, but the query editor doesn't support two-factor authentication.

## Connect to the query editor

1. On your SQL database **Overview** page in the [Azure portal](https://portal.azure.com), select **Query editor (preview)** from the left menu.

   :::image type="content" source="./media/connect-query-portal/find-query-editor.png" alt-text="Screenshot that shows selecting query editor.":::

1. On the sign-in screen, provide credentials to connect to the database. You can connect using SQL authentication or Azure AD.

   - To connect with SQL authentication, under **SQL server authentication**, enter a **Login** and **Password** for a user that has access to the database, and then select **OK**. You can always use the login and password for the server admin.

     :::image type="content" source="./media/connect-query-portal/login-menu.png" alt-text="Screenshot showing sign-in with SQL authentication.":::

   - To connect using Azure AD, if you're the Azure AD server admin, select **Continue as \<your user or group ID>**. If sign-in is unsuccessful, try refreshing the page.

## Query the database

On the **Query editor (preview)** page, run the following example queries against your AdventureWorksLT sample database.

### Run a SELECT query

1. To query for the top 20 products in the database, paste the following [SELECT](/sql/t-sql/queries/select-transact-sql) query into the query editor:

   ```sql
    SELECT TOP 20 pc.Name as CategoryName, p.name as ProductName
    FROM SalesLT.ProductCategory pc
    JOIN SalesLT.Product p
    ON pc.productcategoryid = p.productcategoryid;
   ```

1. Select **Run**, and then review the output in the **Results** pane.

   :::image type="content" source="./media/connect-query-portal/query-editor-results.png" alt-text="Screenshot showing query editor results for a SELECT query.":::

1. Optionally, you can select **Save query** to save the query as an *.sql* file, or select **Export data as** to export the results as a *.json*, *.csv*, or *.xml* file.

### Run an INSERT query

To add a new product to the `SalesLT.Product` table, run the following [INSERT](/sql/t-sql/statements/insert-transact-sql/) T-SQL statement.

1. In the query editor, replace the previous query with the following query:

    ```sql
    INSERT INTO [SalesLT].[Product]
           ( [Name]
           , [ProductNumber]
           , [Color]
           , [ProductCategoryID]
           , [StandardCost]
           , [ListPrice]
           , [SellStartDate]
           )
    VALUES
           ('myNewProduct'
           ,123456789
           ,'NewColor'
           ,1
           ,100
           ,100
           ,GETDATE() );
   ```

1. Select **Run** to add the new product. After the query runs, the **Messages** pane displays **Query succeeded: Affected rows: 1**.

### Run an UPDATE query

Run the following [UPDATE](/sql/t-sql/queries/update-transact-sql/) T-SQL statement to update the price of your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   UPDATE [SalesLT].[Product]
   SET [ListPrice] = 125
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to update the specified row in the `Product` table. The **Messages** pane displays **Query succeeded: Affected rows: 1**.

### Run a DELETE query

Run the following [DELETE](/sql/t-sql/statements/delete-transact-sql/) T-SQL statement to remove your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   DELETE FROM [SalesLT].[Product]
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to delete the specified row in the `Product` table. The **Messages** pane displays **Query succeeded: Affected rows: 1**.

## Considerations and limitations

The following considerations and limitations apply when connecting to and querying Azure SQL Database with the query editor.

### Query editor limitations

- The query editor doesn't support connecting to the `master` database. To connect to the `master` database, use [SQL Server Management Studio (SSMS)](connect-query-ssms.md), [Visual Studio Code](connect-query-vscode.md), or [Azure Data Studio](/sql/azure-data-studio/quickstart-sql-database).
- The query editor can't connect to a [replica database](read-scale-out.md) with `ApplicationIntent=ReadOnly`. To connect in this way from a rich client, use SSMS and specify `ApplicationIntent=ReadOnly` on the **Additional Connection Parameters** tab in connection options. For more information, see [Connect to a read-only replica](/sql/database-engine/availability-groups/windows/listeners-client-connectivity-application-failover#ConnectToSecondary).
- The query editor has a 5-minute timeout for query execution. To run longer queries, use [SSMS](connect-query-ssms.md), [Visual Studio Code](connect-query-vscode.md), or [Azure Data Studio](/sql/azure-data-studio/quickstart-sql-database).
- The query editor only supports cylindrical projection for geography data types.
- The query editor doesn't support IntelliSense for database tables and views, but supports autocomplete for names that have already been typed. For IntelliSense support, use [SSMS](connect-query-ssms.md), [Visual Studio Code](connect-query-vscode.md), or [Azure Data Studio](/sql/azure-data-studio/quickstart-sql-database).
- Pressing **F5** refreshes the query editor page, and any query currently in the editor isn't saved.

### Connection considerations

- For public connections to the query editor, you need to [add your outbound IP address to the server's allowed firewall rules](firewall-create-server-level-portal-quickstart.md) to access your databases.

  You don't need to add your IP address to the SQL server firewall rules if you have a Private Link connection set up on the server, and you connect to the server from within the private virtual network.
  
- Users need at least the role-based access control (RBAC) permission **Read access to the server and database** to use the query editor. Anyone with this level of access can access the query editor. Users who can't assign themselves as the Azure AD admin or access a SQL administrator account shouldn't access the query editor.

### Connection error troubleshooting

- If you see the error message **The X-CSRF-Signature header could not be validated**, take the following actions to resolve the issue:

  - Verify that your computer's clock is set to the right time and time zone. You can try to match your computer's time zone with Azure by searching for the time zone for your database location, such as East US.
  - If you're on a proxy network, make sure that the request header `X-CSRF-Signature` isn't being modified or dropped.

- You might get one of the following errors in the query editor:

  - **Your local network settings might be preventing the Query Editor from issuing queries. Please click here for instructions on how to configure your network settings.**
  - **A connection to the server could not be established. This might indicate an issue with your local firewall configuration or your network proxy settings.**

  These errors occur because the query editor is unable to communicate through ports 443 and 1443. You need to enable outbound HTTPS traffic on these ports. The following instructions walk you through this process, depending on your OS. Your corporate IT department might need to grant approval to open this connection on your local network.

  **For Windows:**

  1. Open **Windows Defender Firewall**.
  1. On the left menu, select **Advanced settings**.
  1. In **Windows Defender Firewall with Advanced Security**, select **Outbound rules** on the left menu.
  1. Select **New Rule** on the right menu.
  1. In the **New outbound rule wizard**, follow these steps:

     1. Select **port** as the type of rule you want to create, and then select **Next**.
     1. Select **TCP**.
     1. Select **Specific remote ports**, enter *443, 1443*, and then select **Next**.
     1. Select **Allow the connection if it is secure**, select **Next**, and then select **Next** again.
     1. Keep **Domain**, **Private**, and **Public** selected.
     1. Give the rule a name, for example *Access Azure SQL query editor*, and optionally provide a description. Then select **Finish**.

  **For MacOS:**
  1. On the Apple menu, open **System Preferences**.
  1. Select **Security & Privacy**, and then select **Firewall**.
  1. If **Firewall** is off, select **Click the lock to make changes** at the bottom, and select **Turn on Firewall**.
  1. Select **Firewall Options**.
  1. In the **Security & Privacy** window, select **Automatically allow signed software to receive incoming connections**.

  **For Linux:**

  Run these commands to update `iptables`:
  ```bash
  sudo iptables -A OUTPUT -p tcp --dport 443 -j ACCEPT
  sudo iptables -A OUTPUT -p tcp --dport 1443 -j ACCEPT
  ```

## Next steps

- [What is Azure SQL?](../azure-sql-iaas-vs-paas-what-is-overview.md)
- [Azure SQL glossary of terms](../glossary-terms.md)
- [T-SQL differences between SQL Server and Azure SQL Database](transact-sql-tsql-differences-sql-server.md)
- [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](connect-query-ssms.md)
- [Quickstart: Use Visual Studio Code to connect and query](connect-query-vscode.md)
- [Quickstart: Use Azure Data Studio to connect and query Azure SQL Database](/sql/azure-data-studio/quickstart-sql-database)

