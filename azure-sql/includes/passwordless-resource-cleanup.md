---
author: alexwolfmsft
ms.author: alexwolf
ms.date: 05/11/2023
ms.topic: include
ms.service: sql-database
---

## Clean up the resources

When you are finished working with the Azure SQL Database, delete the resource to avoid unintended costs.

## [Azure Portal](#tab/portal)

1) In the Azure portal search bar, search for *Azure SQL* and select the matching result.

1) Locate and select your database on the list of Azure SQL Databases.

1) On the **Overview** page of your Azure SQL Database, select **Delete**.

1) In the flyout menu, type the name of your database to confirm, and then press **Delete**.

## [Azure CLI](#tab/azure-cli)

Delete your database using the `az sql db delete` command. Replace the placeholder parameters with your own values.

```azurecli
az sql db delete --name <database-name> --resource-group <resource-group-name> --server <logical-server-name>
```

---
