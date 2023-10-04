The following steps are required to create a passwordless connection between the App Service instance and Azure SQL Database:

1) Create a managed identity for the App Service. The `Microsoft.Data.SqlClient` library included in your app will automatically discover the managed identity, just like it discovered your local Visual Studio user.
2) Create a SQL database user and associate it with the App Service managed identity.
3) Assign SQL roles to the database user that allow for read, write, and potentially other permissions.

There are multiple tools available to implement these steps:

## [Service Connector (Recommended)](#tab/service-connector)

Service Connector is a tool that streamlines authenticated connections between different services in Azure. Service Connector currently supports connecting an App Service to a SQL database via the Azure CLI using the `az webapp connection create sql` command. This single command completes the three steps mentioned above for you.

```azurecli
az webapp connection create sql \
    -g <app-service-resource-group> \
    -n <app-service-name> \
    --tg <database-server-resource-group> \
    --server <database-server-name> \
    --database <database-name> \
    --system-identity
```

You can verify the changes made by Service Connector on the App Service settings.

1) Navigate to the **Identity** page for your App Service. Under the **System assigned** tab, the **Status** should be set to **On**. This value means that a system-assigned managed identity was enabled for your app.

2) Navigate to the **Configuration** page for your App Service. Under the **Connection strings** tab, you should see a connection string called **AZURE_SQL_CONNECTIONSTRING**. Select the **Click to show value** text to view the generated passwordless connection string. The name of this connection string matches the one you configured in your app, so it will be discovered automatically when running in Azure.

## [Azure portal](#tab/azure-portal)

The Azure portal allows you to work with managed identities and run queries against Azure SQL Database. Complete the following steps to create a passwordless connection from your App Service instance to Azure SQL Database:

### Create the managed identity

1) In the Azure portal, navigate to your App Service and select **Identity** on the left navigation.

2) On the **Identity** page's **System assigned** tab, make sure the **Status** toggle is set to **On**. When this setting is enabled, a system-assigned managed identity is created with the same name as your App Service. System-assigned identities are tied to the service instance and are destroyed with the app when it's deleted.

### Create the database user and assign roles

1) In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER <your-app-service-name> FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_datawriter ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_ddladmin ADD MEMBER <your-app-service-name>;
    GO
    ```

    :::image type="content" source="../database/media/passwordless-connections/query-editor-small.png" lightbox="../database/media/passwordless-connections/query-editor.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

    This SQL script creates a SQL database user that maps back to the managed identity of your App Service instance. It also assigns the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database. After this step is completed, your services are connected.

---

> [!IMPORTANT]
> Although this solution provides a simple approach for getting started, it's not a best practice for production-grade environments. In those scenarios, the app shouldn't perform all operations using a single, elevated identity. You should try to implement the principle of least privilege by configuring multiple identities with specific permissions for specific tasks.
>
> You can read more about configuring database roles and security on the following resources:
>
> - [Tutorial: Secure a database in Azure SQL Database](../database/secure-database-tutorial.md)
> - [Authorize database access to SQL Database](../database/logins-create-manage.md)
