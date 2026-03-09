---
title: "How to Manage a Local CDC Service"
description: "How to Manage a Local CDC Service"
author: chugugrace
ms.author: chugu
ms.reviewer: mikeray
ms.date: 02/24/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
---
# How to Manage a Local CDC Service

[!INCLUDE [oracle-cdc-retirement](../includes/attunity-oracle-cdc-retirement.md)]

This procedure describes how to use the CDC Service Configuration Console to manage specific CDC services.

<a id="to-manage-a-specific-cdc-service"></a>

### Manage a specific CDC Service

1. From the **Start** menu, select the **CDC Service Configuration for Oracle**.

1. From the left pane in the CDC Service Configuration Console, expand **Local CDC Services**.

1. Select the CDC service you want to work with.

   You can also right-click the CDC service you want to work with and select the desired action.

   **OR**

   Select **Local CDC Services** from the left pane in the CDC Service Configuration Console then select the service you want to work with from the middle section of the CDC Service Configuration Console.

   You can also right-click the CDC service you want to work with and select the desired action.

1. You can carry out the following tasks when working with a CDC service.

   - **Delete the service**

     From the **Actions** pane on the right side of the CDC Service Configuration Console, select **Delete** to delete the service.

     You can also right-click the CDC service you want to delete and select **Delete**.

     > [!NOTE]
     > If the service is running when deleting the service, the service is stopped before being deleted.

     To delete an Oracle CDC Windows Service definition, the program needs update access to the MSXDBCDC database in the associated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you select **OK** to delete the service, the program attempts to delete the Oracle CDC Service registration in the MSXDBCDC database. If it fails due to lack of permissions, a dialog box is displayed to prompt the user to enter a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login with an update access to the MSXDBCDC database.

     For information about the data you must enter in the Connect to SQL Server dialog box, see [Manage an Oracle CDC Service](the-oracle-cdc-service.md#manage-an-oracle-cdc-service) and [Connection to SQL Server for Delete](the-oracle-cdc-service.md#connection-to-sql-server-for-delete).

   - **Edit the CDC Service Properties**

     From the **Actions** pane on the right side of the CDC Service Configuration Console, select **Properties**.

     You can also right-click the CDC service where you want to edit the properties and select **Properties**.

## Related content

- [Manage an Oracle CDC Service](the-oracle-cdc-service.md#manage-an-oracle-cdc-service)
