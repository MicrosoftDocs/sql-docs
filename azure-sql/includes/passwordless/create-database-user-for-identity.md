---
author: bobtabor-msft
ms.author: rotabor
ms.date: 06/01/2023
ms.service: azure-sql-database
ms.topic: include
ms.custom: generated
---

Create a SQL database user that maps back to the user-assigned managed identity. Assign the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database.

1) In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER [user-assigned-identity-name] FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER [user-assigned-identity-name];
    ALTER ROLE db_datawriter ADD MEMBER [user-assigned-identity-name];
    ALTER ROLE db_ddladmin ADD MEMBER [user-assigned-identity-name];
    GO
    ```

    :::image type="content" source="../../database/media/passwordless-connections/query-editor-identity-small.png" lightbox="../../database/media/passwordless-connections/query-editor-identity.png" alt-text="A screenshot showing how to use the Azure Query editor to create a SQL user for a managed identity.":::

    Running these commands assigns the [SQL DB Contributor](/azure/role-based-access-control/built-in-roles#sql-db-contributor) role to the user-assigned managed identity. This role allows the identity to read, write, and modify the data and schema of your database.
---

> [!IMPORTANT]
> Use caution when assigning database user roles in enterprise production environments. In those scenarios, the app shouldn't perform all operations using a single, elevated identity. Try to implement the principle of least privilege by configuring multiple identities with specific permissions for specific tasks.
>
> You can read more about configuring database roles and security on the following resources:
>
> * [Tutorial: Secure a database in Azure SQL Database](/azure/azure-sql/database/secure-database-tutorial)
> * [Authorize database access to SQL Database](/azure/azure-sql/database/logins-create-manage)
