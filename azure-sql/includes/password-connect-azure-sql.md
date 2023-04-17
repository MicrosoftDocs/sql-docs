There are multiple tools available to connect your App Service to the Azure SQL Database:

## [Service Connector (Recommended)](#tab/service-connector)

Service Connector is a tool that streamlines authenticated connections between different services in Azure. Service Connector currently supports connecting an App Service to a SQL database via the Azure CLI using the `az webapp connection create sql` command.

```azurecli
az webapp connection create sql
-g <your-resource-group>
-n <your-app-service-name>
--tg <your-database-server-resource-group>
--server <your-database-server-name>
--database <your-database-name>
--config-connstr
```

You can verify the changes made by Service Connector on the App Service settings.

1) Navigate to the **Configuration** page for your App Service.

1) Under the **Connection strings** tab, you should see a connection string called **AZURE_SQL_CONNECTIONSTRING**. Select the **Click to show value** text to view the generated connection string. The name of this connection string matches with the one you configured in your app locally, so it will be discovered automatically when running in Azure.

## [Azure portal](#tab/azure-portal)

1) In the Azure portal, navigate to your App Service and select **Configuration** on the left navigation.

1) On the **Application settings** tab, select **New connection string**.

    :::image type="content" source="../database/media/passwordless-connections/add-connection-string.png" lightbox="../database/media/passwordless-connections/add-connection-string.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

1) In the **Add/Edit connection string** flyout menu, enter the following values:

    * **Name**: Enter a value of *AZURE_SQL_CONNECTIONSTRING*.
    * **Value**: Paste the same connection string value you configured locally.
    * **Type**: Select **SQLServer**.

1) Select **OK** at the bottom of the panel.

1) Select **Save** at the top of the page.

---
