---
title: "Quickstart: Use Azure Data Studio to connect and query MySQL"
description: Use Azure Data Studio to connect to a MySQL server (hosted on-premises, on VMs, on managed MySQL in other clouds or on Azure Database for MySQL - Flexible Server), create a database, and use SQL statements to query, insert, update and delete data in the database.
author: shreyaaithal
ms.author: shaithal
ms.reviewer: erinstellato
ms.date: 10/12/2022
ms.service: azure-data-studio
ms.topic: quickstart
ms.custom: intro-quickstart, ignite-2022
---

# Quickstart: Use Azure Data Studio to connect and query MySQL (Preview)

This quickstart shows how to use Azure Data Studio to connect to a MySQL server (hosted on-premises, on VMs, on managed MySQL in other clouds or on Azure Database for MySQL - Flexible Server), create a database, and use SQL statements to insert and query data in the database.

## Prerequisites

To complete this quickstart, you need Azure Data Studio, the MySQL extension for Azure Data Studio, and access to a MySQL server.

- [Install Azure Data Studio](./download-azure-data-studio.md).
- [Install the MySQL extension for Azure Data Studio](./extensions/mysql-extension.md).
- A MySQL server. You can either create a managed MySQL server on Azure using [Azure Database for MySQL - Flexible Server](/azure/mysql/flexible-server/quickstart-create-server-portal) or [install MySQL locally](https://dev.mysql.com/downloads/mysql/).

## Connect to MySQL

1. Start **Azure Data Studio**.

2. The first time you start Azure Data Studio the **Connection** dialog opens. If the **Connection** dialog doesn't open, select the **New Connection** icon on the **SERVERS** view in the **Connections** tab:

    :::image type="content" source="media/quickstart-mysql/new-connection-icon.png" alt-text="Screenshot of new connection icon in the Servers sidebar.":::

3. In the dialog window that pops up, go to **Connection type** and select **MySQL** from the drop-down.

4. Enter your MySQL server name, user name, and password for authentication:

    :::image type="content" source="media/quickstart-mysql/new-connection-screen.png" alt-text="Screenshot of new connection screen to connect to MySQL server.":::

   | Setting       | Example value | Description |
   | ------------ | ------------------ | ------------------------------------------------- |
   | **Server name** | localhost / exampleserver.mysql.database.azure.con | The fully qualified server name. |
   | **User name** | mysqluser | The user name you want to sign in with. |
   | **Password (SQL Login)** | *password* | The password for the user account you're logging in with. |
   | **Remember Password** | *Check* | Check this box if you don't want to enter the password each time you connect. |
   | **Database name** | \<Default\> | Enter a database name if you want the connection to specify a database. |
   | **Server group** | \<Default\> | This option lets you assign this connection to a specific server group you create. |
   | **Name (optional)** | *leave blank* | This option lets you specify a friendly name for your server. |

5. If your MySQL server requires SSL encryptions, navigate to **Advanced Properties** window by selecting **Advanced...** button, enter the SSL configuration details and select **OK**. By default, SSL mode is configured as *Require*. For more information on SSL encryption and modes, see [Configuring MySQL to Use Encrypted Connections](https://dev.mysql.com/doc/refman/8.0/en/using-encrypted-connections.html).

6. Review the connection details and select **Connect**.

Once a successful connection is established, your server opens in the **SERVERS** sidebar.

## Create a database

The following steps will create a database named **tutorialdb**:

1. Right-click on your MySQL server in the **SERVERS** sidebar and select **New Query**.

2. Paste this SQL statement in the query editor that opens up.

   ```sql
   CREATE DATABASE tutorialdb;
   ```

3. From the toolbar, select **Run** to execute the query. Notifications appear in the **MESSAGES** pane to show query progress.

>[!TIP]
> You can use **F5** on your keyboard to execute the statement instead of using **Run**.

After the query completes, right-click **Databases** under your MySQL server in the **SERVERS** sidebar, and select **Refresh** to see **tutorialdb** listed under the **Databases** node.

## Create a table

 The following steps will create a table in the **tutorialdb**:

1. Change the connection context to **tutorialdb** using the drop-down in the query editor.

    :::image type="content" source="media/quickstart-mysql/change-context.png" alt-text="Screenshot showing connection context drop-down in query editor.":::

2. Paste the following SQL statement into the query editor and select **Run**.

   > [!NOTE]
   > You can either append this or overwrite the existing query in the editor. Selecting **Run** executes only the query that is highlighted. If nothing is highlighted, selecting **Run** executes all queries in the editor.

   ```sql
   -- Drop the table if it already exists
   DROP TABLE IF EXISTS customers;
   -- Create a new table called 'customers'
   CREATE TABLE customers(
       customer_id SERIAL PRIMARY KEY,
       name VARCHAR (50) NOT NULL,
       location VARCHAR (50) NOT NULL,
       email VARCHAR (50) NOT NULL
   );
   ```

## Insert data

Paste the following snippet into the query window and select **Run**:

   ```sql
   -- Insert rows into table 'customers'
   INSERT INTO customers
       (customer_id, name, location, email)
    VALUES
      ( 1, 'Orlando', 'Australia', ''),
      ( 2, 'Keith', 'India', 'keith0@adventure-works.com'),
      ( 3, 'Donna', 'Germany', 'donna0@adventure-works.com'),
      ( 4, 'Janet', 'United States','janet1@adventure-works.com');
   ```

## Query data

1. Paste the following snippet into the query editor and select **Run**:

   ```sql
   -- Select rows from table 'customers'
   SELECT * FROM customers; 
   ```

2. The results of the query are displayed:

   :::image type="content" source="media/quickstart-mysql/view-results.png" alt-text="Screenshot showing results of the SELECT query.":::

Alternatively, in the **SERVERS** sidebar, navigate down to the **customers** table, right-click on the table and select **Select Top 1000** to query the data.

## Next Steps

- Learn about the [scenarios available for MySQL in Azure Data Studio](./extensions/mysql-extension.md).
