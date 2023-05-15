1) In the [Azure portal](https://portal.azure.com), browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER <user@domain> FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER <user@domain>;
    ALTER ROLE db_datawriter ADD MEMBER <user@domain>;
    ALTER ROLE db_ddladmin ADD MEMBER <user@domain>;
    GO
    ```

    :::image type="content" source="../../database/media/passwordless-connections/query-editor-user-small.png" lightbox="media/passwordless-connections/query-editor-user.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

    For more information about the roles assigned, see [Fixed-database roles](/sql/relational-databases/security/authentication-access/database-level-roles#fixed-database-roles).
