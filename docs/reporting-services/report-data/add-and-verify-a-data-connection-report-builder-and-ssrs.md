---
title: Add and verify a data connection in Report Builder
description: Learn how to use Report Builder to add and verify a data connection to make sure that the credentials you specify are sufficient.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/01/2024
ms.service: reporting-services
ms.subservice: report-data
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to add and verify data sources so that I can use the shared data sources from my report servers.
---

# Add and verify a data connection in Report Builder

In Report Builder, you can add a shared data source from the report server or create an embedded data source for your report. In Report Designer, you can create a shared data source or an embedded data source and deploy it to a report server.

To add a shared data source to your report, browse to a report server and select a shared data source. The shared data source in your report points to the shared data source definition on the report server.

To create an embedded data source, you must have connection information to the external source of data and you must know which permissions you need to access the data. This information usually comes from the owner of the data source. You can test the connection to verify that the credentials that are specified are sufficient.

For more information, see [Create data connection strings in Report Builder](data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md) and [Specify credential and connection information for report data sources](./specify-credential-and-connection-information-for-report-data-sources.md).

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## Create a connection to a shared data source in Report Builder

1. On the menu in the **Report Data** pane, select **New,** and then select **Data Source**. The **Data Source Properties** dialog box opens.

1. In the **Name** text box, enter a name for the data source.

    > [!NOTE]  
    > This name is saved in the local report definition. This name isn't the name of the shared data source on the report server.

1. Select **Use a shared connection or report model**. The list of recently used shared data sources and report models appears. To select one from a report server, choose **Browse** and go to the folder on the report server where shared data sources are available.

    :::image type="content" source="../../reporting-services/report-data/media/use-shared-connection-or-report-model.png" alt-text="Screenshot that shows the Data Source Properties dialog box highlighting the Use a shared connection or report model option.":::

1. Select the shared data source and then choose **Open**.

1. Select **OK**.

The data source appears in the **Report Data** pane.

### Verify a data connection  

1. In the **Report Data** pane, double-click your data source. The **Data Source Properties** dialog box opens.

1. Select **Test Connection**.

1. If the connection is successful, the following message appears: "Connection created successfully." Select **OK**.

1. If the connection isn't successful, the following message appears: "Unable to connect to the data source."  

1. Select **Details**, and use the information to correct the issue.

    For more information, see [Specify credential and connection information for report data sources](./specify-credential-and-connection-information-for-report-data-sources.md).

1. Select **OK**.

## Related content

- [Report datasets (SSRS)](../../reporting-services/report-data/report-datasets-ssrs.md)
- [Report embedded datasets and shared datasets (Report Builder and SSRS)](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)
- [Find, view, and manage reports (Report Builder and SSRS)](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
