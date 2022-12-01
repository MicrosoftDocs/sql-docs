---
title: Connect to SQL Server with strict encryption
description: This article describes how to connect to SQL Server using the strict encryption type
ms.service: sql
ms.subservice: security
ms.topic: how-to
ms.custom:
- event-tier1-build-2022
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
monikerRange: ">= sql-server-ver16||>= sql-server-linux-ver16"
---

# Connect to SQL Server with strict encryption

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

Strict connection encryption enforces good security practices and makes SQL Server traffic manageable by standard network appliances.

In this article, we'll show you how to connect to [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] using the strict connection type.

## Prerequisite

- [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]
- ODBC or OLE DB Driver for SQL Server
  - [ODBC Driver for SQL Server](../../../connect/odbc/download-odbc-driver-for-sql-server.md) version 18.1.2.1 or higher
  - [OLE DB Driver for SQL Server](../../../connect/oledb/download-oledb-driver-for-sql-server.md) version 19.2.0 or higher
- Create and install a TLS certificate in SQL Server. For more information, see [Enable encrypted connections to the Database Engine](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)

## Connect to SQL Server using a .NET application

For information on building and connecting to SQL Server using the `strict` encryption type, see [Connection String Syntax](/dotnet/framework/data/adonet/connection-string-syntax) on how to properly build the connection string. For more information on the new connection string properties, see [Additional changes to connection string encryption properties](./tds-8-and-tls-1-3.md#additional-changes-to-connection-string-encryption-properties).

## Connect using an ODBC DSN

You can test a connection with the `Strict` connection encryption type using an ODBC DSN to SQL Server.

1. Search for the **ODBC Data Sources** app in Windows.

   :::image type="content" source="media/odbc-data-sources-app.png" alt-text="Screenshot of the O D B C data sources app.":::

1. Make sure you have the latest ODBC driver by looking in the **Drivers** tab of ODBC Data Source Administrator.

   :::image type="content" source="media/odbc-drivers.png" alt-text="Screenshot of available drivers.":::

1. In the **System DSN** tab, select **Add** to create a DSN. Then select the **ODBC Driver 18 for SQL Server**. Select **Finish**. We're going to use this to test our connection.

1. In the **Create a New Data Source to SQL Server** window, provide a name for this data source, and add your [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] server name to **Server**. Select **Next**.

   :::image type="content" source="media/create-data-source.png" alt-text="Screenshot of creating a data source using the O D B C driver.":::

1. Use all default values for all the settings until you get to the screen that has **Connection Encryption**. Select **Strict**. If the server name that you entered differs from that in the certificate or if the IP address is used instead, set **HostName in certificate** to the one used in your certificate. Select **Finish**.

   :::image type="content" source="media/connection-encryption-strict.png" alt-text="Screenshot showing the strict encryption type.":::

1. When the dialog box **ODBC Microsoft SQL Server Setup** pops up, select the **Test Data Source...** button to test the connection. This should enforce the `strict` connection to SQL Server for this test.

## Connect using Universal Data Link

You can also test the connection to SQL Server with `strict` encryption using the OLE DB Driver with Universal Data Link (UDL).

1. To create a UDL file to test your connection, right-click on your desktop, and select **New** > **Text Document**. You'll need to change the extension from `txt` to `udl`. You can give the file any name you want.

   > [!NOTE]
   > You'll need to be able to see the extension name in order to change the extension from `txt` to `udl`. If you cannot see the extension, you can enable viewing the extension by opening **File Explorer** > **View** > **Show** > **File name extensions**.

1. Open the UDL file that you created, and go over to the **Provider** tab to select the **Microsoft OLE DB Driver 19 for SQL Server**. Select **Next >>**.

   :::image type="content" source="media/udl-providers.png" alt-text="Screenshot of the U D L provider screen.":::

1. On the **Connection** tab, enter your SQL Server server name, and select the authentication method you use for logging into SQL Server.

   :::image type="content" source="media/udl-connection.png" alt-text="Screenshot of the U D L connection screen.":::

1. In the **Advanced** tab, select **Strict** for **Connection encryption**. If the server name that you entered differs from that in the certificate or if the IP address is used instead, set **Host name in certificate** to the one used in your certificate. Go back to the **Connection** tab when you're done.

   :::image type="content" source="media/udl-advanced.png" alt-text="Screenshot of the U D L advanced screen.":::

1. Select **Test Connection** to test the connection with the `strict` connection encryption.

   :::image type="content" source="media/udl-test-connection.png" alt-text="Screenshot of the U D L connection screen and testing connection.":::

## Remarks

If you see `SSL certificate validation failed`, validate that:

- Server certificate is valid on the machine you're using for testing
- At least one of the following is true:
  - Provider SQL Server matches CA name or one of the DNS names in the certificate.
  - `HostNameInCertificate` connection string property matches CA name or one of the DNS names in the certificate.

## See also

- [TDS 8.0 and TLS 1.3 support](tds-8-and-tls-1-3.md)
