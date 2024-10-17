The following steps are required to connect the App Service instance to Azure SQL Database:

1. Create a managed identity for the App Service.
1. Create a SQL database user and associate it with the App Service managed identity.
1. Assign SQL roles to the database user that allow for read, write, and potentially other permissions.

There are multiple tools available to implement these steps:

## [Service Connector (Recommended)](#tab/service-connector)

Service Connector is a tool that streamlines authenticated connections between different services in Azure. Service Connector currently supports connecting an App Service to an Azure SQL database via the Azure CLI using the `az webapp connection create sql` command. This single command completes the three steps mentioned above for you.

### Create the managed identity with Service Connector

Run the following command in the Azure portal's Cloud Shell. The Cloud Shell has the latest version of the Azure CLI. Replace the variables in `<>` with your own values. 

```azurecli
az webapp connection create sql \
    -g <app-service-resource-group> \
    -n <app-service-name> \
    --tg <database-server-resource-group> \
    --server <database-server-name> \
    --database <database-name> \
    --system-identity
```

### Verify the App Service app settings

You can verify the changes made by Service Connector on the App Service settings.

1. In Visual Studio Code, in the Azure explorer, right-click your App Service and select **Open in portal**.
1. Navigate to the **Identity** page for your App Service. Under the **System assigned** tab, the **Status** should be set to **On**. This value means that a system-assigned managed identity was enabled for your app.
1. Navigate to the **Configuration** page for your App Service. Under the **Application Settings** tab, you should see several environment variables, which were already in the **mssql** configuration object. 

    * `AZURE_SQL_SERVER`
    * `AZURE_SQL_DATABASE`
    * `AZURE_SQL_PORT`
    * `AZURE_SQL_AUTHENTICATIONTYPE`

    Don't delete or change the property names or values.



## [Azure portal](#tab/azure-portal)

The Azure portal allows you to work with managed identities and run queries against Azure SQL Database. Complete the following steps to create a passwordless connection from your App Service instance to Azure SQL Database:

### Create the managed identity

1. In the Azure portal, navigate to your App Service and select **Identity** on the left navigation.

1. On the identity page, change the **System-assigned** status to **on** and select **Save**. 
1. When asked to enable the identity, select **Yes**.

    When this setting is enabled, a system-assigned managed identity is created with the same name as your App Service. System-assigned identities are tied to the service instance and are destroyed with the app when it's deleted.

### Create the database user and assign roles

1. In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

1. Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

1. On the query editor view, run the following T-SQL commands. Replace `<your-app-service-name>` with your App Service resource's name.

    ```sql
    CREATE USER "<your-app-service-name>" FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER "<your-app-service-name>";
    ALTER ROLE db_datawriter ADD MEMBER "<your-app-service-name>";
    ALTER ROLE db_ddladmin ADD MEMBER "<your-app-service-name>";
    GO
    ```

    :::image type="content" source="../database/media/passwordless-connections/query-editor-small.png" lightbox="../database/media/passwordless-connections/query-editor.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

    This SQL script creates a SQL database user that maps back to the managed identity of your App Service instance. It also assigns the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database. After this step is completed, your services are connected.

> [!IMPORTANT]
> Although this solution provides a simple approach for getting started, it's not a best practice for production-grade environments. In those scenarios, the app shouldn't perform all operations using a single, elevated identity. You should try to implement the principle of least privilege by configuring multiple identities with specific permissions for specific tasks.
>
> You can read more about configuring database roles and security on the following resources:
>
> - [Tutorial: Secure a database in Azure SQL Database](../database/secure-database-tutorial.md)
> - [Authorize database access to SQL Database](../database/logins-create-manage.md)

### Create the App Service app settings

1. In the Azure portal, navigate to your App Service and select **Configuration** on the left navigation.
1. Select **+ New application setting** for each environment variable below. Add your own appropriate value to create the required environment variables for your App Service instance to connect to your database.

    ```text
    AZURE_SQL_SERVER=<YOURSERVERNAME>.database.windows.net
    AZURE_SQL_DATABASE=<YOURDATABASENAME>
    AZURE_SQL_PORT=1433
    AZURE_SQL_AUTHENTICATIONTYPE=azure-active-directory-default
    ```

1. When you're done adding settings, select **Save**.
---
