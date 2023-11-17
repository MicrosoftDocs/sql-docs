---
title: Connect to Server (Additional Connection Parameters page) Database Engine
description: Connect to Server (Additional Connection Parameters page) Database Engine.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 11/16/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.connecttosqlserver.additionalconnparams.f1"
---

# Connect to Server (Additional Connection Parameters page) - Database Engine

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use this tab to enter advanced parameters when connecting to an instance of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] or registering [!INCLUDE [ssDE](../../includes/ssde-md.md)] in **Registered Servers**. **Connect** and **Options** only appear in this dialog box when connecting to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. **Test** and **Save** only appear in this dialog box when registering [!INCLUDE [ssDE](../../includes/ssde-md.md)]. Access this tab by selecting **Options >>** on the login tab.

[!INCLUDE [entra-id-hard-coded](../../includes/entra-id-hard-coded.md)]

## Additional parameters

The Additional Connection Parameters dialog box allows you to enter connection properties that don't have an equivalent option exposed in any other tab, usually to support an advanced scenario. The parameters entered in the Additional Connection Parameters dialog box are added to any options selected or altered in the **Login** and **Connection Properties** dialogs.

> [!IMPORTANT]  
> Any parameters entered in this dialog box override selections made on the **Login** and **Connection Properties** dialogs. Parameters are transmitted as clear text. Don't include sensitive information in the dialog.

## Add a parameter

Parameters are entered with the property name and followed by the value for the property. For scenarios that include Availability Groups, to connect to a secondary replica that supports read-intent so you can execute read-only queries, enter `ApplicationIntent=ReadOnly`. If you connect to an Availability Group with replicas in multiple subnets and specify the Listener in the **Server name** dialog on the **Login** page, enter `MultiSubnetFailover=True` for faster detection of, and connection to, the (currently) active server.

When entering multiple parameters, separate them with a semicolon. For example, `ApplicationIntent=ReadOnly; InitialCatalog=WideWorldImporters`.

For a complete list of parameters, see [Properties](/../../dotnet/api/microsoft.data.sqlclient.sqlconnectionstringbuilder?view=sqlclient-dotnet-standard-3.1&preserve-view=true#properties).

> [!CAUTION]  
> Additional parameters aren't typically required and exist to support advanced scenarios. If you determine that a connection isn't working as expected, check the **Additional Connection Parameters** dialog to see if any unexpected options exist. It isn't uncommon that users enter options for testing purposes and then forget to remove them.

## Related content

- [Connect to Server (Login page) - Database Engine](connect-to-server-login-page-database-engine.md)
- [Connect to Server (Connection Properties page) - Database Engine](connect-to-server-connection-properties-page-database-engine.md)
