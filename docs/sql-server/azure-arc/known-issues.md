---
title: "Known Issues: SQL Server Enabled by Azure Arc"
description: "Describes current behavior that isn't expected for SQL Server enabled by Azure Arc."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 07/24/2025
ms.topic: troubleshooting-known-issue
---

# Known issues: SQL Server enabled by Azure Arc

This article provides information about known issues associated with SQL Server enabled by Azure Arc.

## Databases

If a database isn't online and updatable, required permissions aren't assigned to that database. Features requiring permissions to that database are affected.

### Resolution

- Verify that the databases are online and updateable. Review [Verify state of user databases](prerequisites.md#verify-state-of-user-databases).

- Check the error logs. The error log might show Microsoft SQL Server error 945:

  ```output
  Database cannot be opened due to inaccessible files or insufficient memory or disk space
  ```

  To resolve, follow the steps at [MSSQLSERVER_945](../../relational-databases/errors-events/mssqlserver-945-database-engine-error.md).

## Cost report tags

Currently `Microsoft.AzureArcData` tags are not included in cost reports. For updates, review [Microsoft.AzureArcData tag support](/azure/azure-resource-manager/management/tag-support#microsoftazurearcdata)

## Throttling errors

You might occasionally see throttling notifications for Azure resource updates in Activity log, as shown below. This behavior is expected due to service-imposed rate limits. Updates for SQL Server instances, databases, and availability groups inventory run continuously on an hourly basis and should eventually complete successfully.

:::image type="content" source="media/known-issues/throttling-error-notification.png" alt-text="Screenshot of throttling error notification in Activity log." lightbox="media/known-issues/throttling-error-notification.png":::

Contact the support team for assistance if you:

- Encounter these errors repeatedly
- Notice data inconsistencies in Azure for your SQL Server instances, databases, or availability groups
- Experience failures when using other features of SQL Server enabled by Azure Arc

## Related content

- [Troubleshoot Azure extension for SQL Server deployment](troubleshoot-deployment.md)
- [Troubleshoot Azure extension for SQL Server](troubleshoot-extension.md)
- [Microsoft.AzureArcData tag support](/azure/azure-resource-manager/management/tag-support#microsoftazurearcdata)
