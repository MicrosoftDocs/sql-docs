---
title: Network Security Perimeter
titleSuffix: Azure SQL Database
description: Overview of network security perimeter for Azure SQL Database
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/10/2026
ai-usage: ai-assisted
ms.service: azure-sql-database
ms.subservice: security
ms.topic: how-to
---

# Network security perimeter for Azure SQL Database (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

The network security perimeter (preview) secures both inbound and outbound network traffic between Azure SQL Database and other platform as a service (PaaS) resources, such as Azure Storage and Azure Key Vault. It blocks attempts to communicate with Azure resources outside the perimeter.

> [!NOTE]  
> As a preview feature, the technology presented in this article is subject to [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## Get started

1. In the Azure portal, search for **Network Security Perimeter** in the resource list and then select **Create**.

   :::image type="content" source="media/network-security-perimeter/associate-sql-network-security-perimeter.png" alt-text="Screenshot of creating a network security perimeter in the Azure portal.":::

1. Provide a **Name** and **Region** and choose the subscription.
1. Under the **Resources** section, select the **Add** button and navigate to the SQL Database you want to associate with the perimeter.
1. Add an inbound access rule. The source type can be either an IP address, a subscription, or other network security perimeters.
1. Add an outbound access rule to allow resources inside the perimeter to connect to resources outside the perimeter.

If you already have an Azure SQL Database and want to add a network security perimeter, use the following steps:

1. In the Azure portal, search for the existing Network Security Perimeter.
1. Select **Associated Resources** from the **Settings** menu.
1. Select the **Add** button and select **Associate resources with an existing profile**.

   :::image type="content" source="media/network-security-perimeter/associated-resources-sql-network-security-perimeter.png" alt-text="Screenshot of associated resources for network security perimeter in the Azure portal.":::

1. Select your **Profile** from the dropdown list and select **Add**.

   :::image type="content" source="media/network-security-perimeter/select-associated-resources-sql-network-security-perimeter.png" alt-text="Screenshot of adding an associated resource for network security perimeter in the Azure portal.":::

1. Search for your SQL Database resource, select the required resource, and then select **Associate**.

## Use SQL Database with a network security perimeter

By default, a network security perimeter uses [transition mode](/azure/private-link/network-security-perimeter-concepts#access-modes-in-network-security-perimeter), formerly called learning mode, which you can use to log all traffic to and from SQL Database. You can log the network traffic to a Log Analytics workspace or an Azure Storage account by using [Diagnostic logging for Azure Network Security Perimeter](/azure/private-link/network-security-perimeter-diagnostic-logs). When you're ready, switch the perimeter to **Enforced** mode. In **Enforced** mode, denied access returns the following error:

```output
Error 42118
Login failed because the network security perimeter denied inbound access.
```

## Feature support

Not every Azure SQL Database feature interacts with a network security perimeter in the same way. Some features move data between logical servers, some connect outbound to other Azure services, and some don't work inside a perimeter at all. The following table covers the features documented so far. This list grows as more features complete perimeter validation. Select a feature to learn more about how it works with a network security perimeter, including setup steps and any limitations.

| Feature | Perimeter support | What you need to know |
| --- | --- | --- |
| [Active geo-replication](active-geo-replication-overview.md) | Supported | Every logical server in the geo-replication set needs both an inbound and an outbound rule to every other server, because a failover reverses the direction of data movement. Replication on an established link keeps running even if you later change the rules to disallow it. |
| [Change event streaming](/sql/relational-databases/track-changes/change-event-streaming/overview) | Supported | The database connects outbound to Azure Event Hubs or Fabric Eventstream. In enforced mode, add an outbound rule that names the destination FQDN. Without that rule, you can't create a stream group, and an existing stream stops publishing and holds the transaction log open. |
| [Database copy](database-copy.md) | Supported | In enforced mode, the source server needs an outbound rule and the target server needs an inbound rule, unless both servers are in the same perimeter or the copy stays on one server. You can't use Transact-SQL to copy to a different logical server once a perimeter is attached. |
| [Elastic jobs](elastic-jobs-overview.md) | Supported | The job agent's logical server needs an outbound rule to each target server. The perimeter is checked at run time, so a job can be created successfully and then fail during execution. |
| [Elastic query](elastic-query-overview.md) | Supported within a single perimeter | The database that runs the query and the remote database must be in the same perimeter. Elastic query needs no perimeter configuration of its own. |
| [Extended Events](xevent-db-diff-from-svr.md) | Supported | The `event_file` target and the functions that read event data connect outbound to Azure Storage. In enforced mode, use a managed identity, or add an outbound FQDN rule for the storage account when you use a SAS token. |
| [Import and export](database-export.md) | Supported | The database connects outbound to Azure Blob Storage. In enforced mode, add an outbound rule for the storage account unless it's in the same perimeter. The perimeter is checked when data transfers, not when the operation starts. The same requirements apply when you [import a BACPAC](database-import.md). |
| [Long-term retention restore](long-term-retention-overview.md) | Supported | In enforced mode, the source server needs an outbound rule and the target server needs an inbound rule, unless both are in the same perimeter. After the source server is deleted, only the target side can be validated. |
| [SQL Data Sync](sql-data-sync-data-sql-server-sql-database.md) | Not supported by design | Data Sync runs as a proxy service rather than an Azure resource, so it has no FQDN or IP address to write perimeter rules against. A perimeter blocks the network paths Data Sync requires in both transition and enforced mode. |

## Limitations

- A logical server in Azure SQL Database can't be associated with a network security perimeter if it contains one or more dedicated SQL pools (formerly SQL DW).
- Network security perimeter support for Azure SQL Database is available in Azure public cloud regions. It isn't available in Azure Government. For the current list of onboarded services, see [Onboarded private link resources](/azure/private-link/network-security-perimeter-concepts#onboarded-private-link-resources).

## Related content

- [What is a network security perimeter?](/azure/private-link/network-security-perimeter-concepts)
- [Quickstart: Create a network security perimeter - Azure portal](/azure/private-link/create-network-security-perimeter-portal)
- [Quickstart: Create a network security perimeter - Azure PowerShell](/azure/private-link/create-network-security-perimeter-powershell)
- [Quickstart: Create a network security perimeter - Azure CLI](/azure/private-link/create-network-security-perimeter-cli)
- [Azure SQL Connectivity Architecture](connectivity-architecture.md)
