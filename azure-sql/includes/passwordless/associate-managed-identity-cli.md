---
author: bobtabor-msft
ms.author: rotabor
ms.date: 06/01/2023
ms.service: sql-database
ms.topic: include
ms.custom: generated, devx-track-azurecli
---

Use the following Azure CLI commands to associate an identity with your app:

Retrieve the fully qualified resource ID of the managed identity you created using the [az identity show](/cli/azure/identity#az-identity-show) command. Copy the output value to use in the next step.

```azurecli
az identity show --name MigrationIdentity -g <your-identity-resource-group-name> --query id
```

# [Azure App Service](#tab/app-service-identity)

You can assign a managed identity to an Azure App Service instance with the [az webapp identity assign](/cli/azure/webapp/identity#az-webapp-identity-assign) command. The `--identities` parameter requires the fully qualified resource ID of the managed identity you retrieved in the previous step. A fully qualified resource ID starts with '/subscriptions/{subscriptionId}' or '/providers/{resourceProviderNamespace}/'.

```azurecli
az webapp identity assign \
    --resource-group <resource-group-name> \
    --name <webapp-name> \
    --identities <managed-identity-id>
```

If you are working with Git Bash, be careful of path conversions when using fully qualified resource IDs. To disable path conversion, add `MSYS_NO_PATHCONV=1` to the beginning of your command. For more information, see [Auto translation of resource IDs](https://github.com/Azure/azure-cli/blob/dev/doc/use_cli_with_git_bash.md#auto-translation-of-resource-ids).

# [Azure Spring Apps](#tab/spring-apps-identity)

You can assign a managed identity to an Azure Spring Apps instance with the [az spring app identity assign](/cli/azure/spring/app/identity) command.

```azurecli
az spring app identity assign \
    --resource-group <resource-group-name> \
    --name <app-name> \
    --service <service-name> \
    --user-assigned <managed-identity-id>
```

# [Azure Container Apps](#tab/container-apps-identity)

You can assign a managed identity to a virtual machine with the [az containerapp identity assign](/cli/azure/containerapp/identity) command.

```azurecli
az containerapp identity assign \
    --resource-group <resource-group-name> \
    --name <app-name> \
    --user-assigned <managed-identity-id>
```

# [Azure virtual machines](#tab/virtual-machines-identity)

You can assign a managed identity to a virtual machine with the [az vm identity assign](/cli/azure/vm/identity) command.

```azurecli
az vm identity assign \
    --resource-group <resource-group-name> \
    --name <virtual-machine-name> \
    --identities <managed-identity-id>
```

# [Azure Kubernetes Service](#tab/aks-identity)

You can assign a managed identity to an Azure Kubernetes Service (AKS) instance with the [az aks update](/cli/azure/aks) command.

```azurecli
az aks update \
    --resource-group <resource-group-name> \
    --name <cluster-name> \
    --enable-managed-identity \
    --assign-identity <managed-identity-id> \
    --assign-kubelet-identity <managed-identity-id>
```

---
