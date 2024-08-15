---
author: bobtabor-msft
ms.author: rotabor
ms.date: 06/01/2023
ms.service: azure-sql-database
ms.topic: include
ms.custom: generated
---

1) In the [Azure portal](https://portal.azure.com), browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER [user@domain] FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER [user@domain];
    ALTER ROLE db_datawriter ADD MEMBER [user@domain];
    ALTER ROLE db_ddladmin ADD MEMBER [user@domain];
    GO
    ```

    :::image type="content" source="../../database/media/passwordless-connections/query-editor-user-small.png" lightbox="../../database/media/passwordless-connections/query-editor-user.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

    Running these commands assigns the [SQL DB Contributor](/azure/role-based-access-control/built-in-roles#sql-db-contributor) role to the account specified. This role allows the identity to read, write, and modify the data and schema of your database. For more information about the roles assigned, see [Fixed-database roles](/sql/relational-databases/security/authentication-access/database-level-roles#fixed-database-roles).
