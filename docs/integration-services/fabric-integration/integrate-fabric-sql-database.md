---
title: "Tutorial: Integrate SSIS with SQL Database in Microsoft Fabric"
description: Learn how to integrate SSIS with Fabric SQL Database
ms.reviewer: randolphwest, mathoma
ms.date: 04/06/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - sfi-image-nochange
---
# Integrate SSIS with SQL database in Microsoft Fabric

This tutorial shows how to connect an SSIS package to [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] using Microsoft Entra service principal authentication with the [Microsoft OLE DB Driver for SQL Server](../../connect/oledb/download-oledb-driver-for-sql-server.md).

## Authentication

Use Microsoft Entra service principal authentication for SSIS packages because they typically run non-interactively under agents. This approach provides secure app-only access without user prompts or multifactor authentication (MFA). It lets you apply least-privilege access through Fabric workspace and item permissions.

Service principal authentication aligns with `Authentication=ActiveDirectoryServicePrincipal` support in Microsoft OLE DB Driver for SQL Server version 18.5.0 and later versions, and improves auditability and secret hygiene when you store client secrets in SSIS Catalog environments or Azure Key Vault.

## Prerequisites

- A Fabric workspace with a SQL database.
- Register a service principal (app registration).
- Enable service principal access to Fabric workspace.
- Microsoft OLE DB Driver for SQL Server version 18.5.0 or later versions, including MSOLEDBSQL19.
- Outbound network access to Fabric SQL Database (Default connection policy).

## Configure SSIS OLE DB Connection Manager

Use the Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL) and configure:

- **Authentication**: `ActiveDirectoryServicePrincipal`
- **User name (User ID)**: Application (client) ID of your service principal
- **Password**: Client secret associated with the app registration
- **Initial Catalog**: Fabric SQL Database name (from Settings → Connection strings)
- **Server name (Data Source)**: Fabric SQL host (for example, `<server-unique-identifer>.database.fabric.microsoft.com`)

  :::image type="content" source="media/ole-db-connection-1.png" alt-text="Screenshot of OLE DB Connection Manager part 1.":::

  :::image type="content" source="media/ole-db-connection-2.png" alt-text="Screenshot of OLE DB Connection Manager part 2." lightbox="media/ole-db-connection-2.png":::

## Run in Fabric with Invoke SSIS Package activity

When running in Fabric with the [Invoke SSIS Package activity](/fabric/data-factory/invoke-ssis-package-activity), if your package uses the `DontSaveSensitive` protection level, credentials aren't persisted in the package file. You supply them at runtime through the **Connection Managers** tab of the Invoke SSIS Package activity. Alternatively, you can set the package protection level to `EncryptSensitiveWithPassword`, which encrypts credentials inside the package. You then provide the package password in the Invoke SSIS Package activity at runtime instead of supplying individual connection manager credentials.

### Steps to override connection for DontSaveSensitive

1. In the Invoke SSIS Package activity, select the **Connection Managers** tab.
1. Select **+ New** to add a connection manager override entry.
1. Set the **Name** field to match your OLE DB Connection Manager name in the package.
1. Fill in the connection properties, including **User name** (Application/client ID) and **Password** (client secret) for service principal authentication.
1. Repeat for each connection manager that requires credentials.

:::image type="content" source="media/activity-configuration-manager.png" alt-text="Screenshot of the Connection Managers tab in the Invoke SSIS Package activity." lightbox="media/activity-configuration-manager.png":::

### Steps to provide package password for EncryptSensitiveWithPassword

1. In the SSIS package, set the **ProtectionLevel** property to `EncryptSensitiveWithPassword` and assign a package password. This encrypts all sensitive data (connection strings, credentials) inside the package file.
1. In the Invoke SSIS Package activity, go to the **Settings** tab.
1. In the **Encryption password** field, enter the same password used to encrypt the package.
1. The runtime decrypts the embedded credentials automatically; no individual connection manager overrides are needed.

:::image type="content" source="media/activity-package-encryption.png" alt-text="Screenshot of the package encryption in the Invoke SSIS Package activity." lightbox="media/activity-package-encryption.png":::

## References

- [Invoke SSIS Package activity](/fabric/data-factory/invoke-ssis-package-activity)
- [Authentication in SQL database in Microsoft Fabric](/fabric/database/sql/authentication)
- [Use Microsoft Entra ID](../../connect/oledb/features/using-azure-active-directory.md)
- [Connect to your SQL database in Microsoft Fabric](/fabric/database/sql/connect)
- [Download Microsoft OLE DB Driver for SQL Server](../../connect/oledb/download-oledb-driver-for-sql-server.md)
