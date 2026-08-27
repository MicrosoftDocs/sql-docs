---
title: Fabric Integration
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use the MSSQL extension for Visual Studio Code to connect to Fabric workspaces and provision SQL databases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos, tsiddique
ms.date: 03/13/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Fabric integration

The MSSQL extension for Visual Studio Code supports connecting to SQL database in Microsoft Fabric. The Connection dialog includes a Fabric connectivity option that you use to sign in using Microsoft Entra ID. You can browse Fabric workspaces in a tree view, search across workspaces, and connect to SQL databases or endpoints without manually configuring connection strings. The extension supports persistent sign-in, tenant switching, and an integrated **Open in MSSQL** flow from the Fabric extension.

You can create new SQL databases in Fabric directly from Visual Studio Code. Authenticate, select or create a workspace, specify a database name, and connect after provisioning completes. An indicator keeps you up to date on the progress.

## Fabric connectivity (Browse)

Use the Fabric browse experience in the MSSQL extension to connect to your SQL database in Fabric or SQL analytics endpoints without manually copying connection strings from the Fabric portal.

### Capabilities

- **Dedicated Fabric experience**: A Fabric-specific option within the Connection dialog for connecting to Fabric databases.

- **Single sign-on**: Uses your Microsoft account for sign-in. You authenticate once to access all your Fabric workspaces and databases.

- **Workspace browsing**: Displays all Fabric workspaces in a hierarchical tree view with resources loaded on demand.

- **Search and discovery**: Real-time search to locate workspaces or databases without scrolling through long lists.

- **Cross-extension support**: Open Fabric databases in the MSSQL extension from the Fabric extension or portal.

### Prerequisites

- Install the latest version of the [MSSQL extension for Visual Studio Code](https://aka.ms/vscode-mssql).
- Active Fabric subscription and workspace permissions.
- A valid Microsoft account with access to your Fabric workspaces and resources.
- Microsoft Entra ID for authentication. After signing in, you can choose both the account and the specific tenant to browse the correct set of Fabric workspaces and databases.

> [!NOTE]  
> For this Fabric Connectivity (Browse) experience, you don't need connection strings or personal access tokens.

### Get started

1. Open the **Connection** dialog and select **Browse Fabric**.

   :::image type="content" source="media/mssql-fabric-integration/fabric-connectivity.png" alt-text="Screenshot of Fabric connectivity in the MSSQL extension for Visual Studio Code." lightbox="media/mssql-fabric-integration/fabric-connectivity.png":::

1. Sign in with Microsoft Entra ID.

   **First-time authentication**

   - The authentication type defaults to *Microsoft Entra ID - Universal with MFA support*.
   - If you're not signed in, open the **Account** dropdown list and select **Add an account**.
   - Choose your account and complete the sign-in process in the browser window that opens.
   - Return to Visual Studio Code once the process finishes successfully.

   **Returning user authentication**

   - If you sign in frequently, your account information appears prepopulated.
   - Verify the correct account is selected and proceed.

1. (Optional) Select a tenant.

   - Use the **Tenant ID** dropdown list to choose your organization's tenant.
   - This selection is useful if you have access to multiple tenant environments and need to browse resources in a different tenant.

1. Browse Fabric workspaces in the **workspace tree**.
1. Use the **search bar** at the top of the tree to quickly locate a workspace or database.
1. Select the desired database or analytics endpoint from the list.
1. Confirm authentication for the selected resource if prompted.

   :::image type="content" source="media/mssql-fabric-integration/fabric-connect.png" alt-text="Screenshot of connecting to Fabric from the Fabric browse experience." lightbox="media/mssql-fabric-integration/fabric-connect.png":::

1. Select **Connect** to add the selected database to your **Connections** list.
1. The new connection appears in **Connections**, and a query window opens ready for use.

   :::image type="content" source="media/mssql-fabric-integration/fabric-connect-object-explorer.png" alt-text="Screenshot of new Fabric connection in the Object Explorer.":::

> [!TIP]  
> Use **Search** at the top of the tree to quickly locate a workspace or database. Results prioritize closest matches and recently used items.

### Errors and edge cases

If something goes wrong, the UI shows a clear message and recommended actions:

| Type | Message | Recommended action |
| --- | --- | --- |
| **No access to workspace** | `You do not have permission to view this workspace.` | Request access or switch account. |
| **Service unavailable or throttled** | `Fabric service is temporarily unavailable.` | Retry or check service health. |
| **Expired session** | `Your session has expired.` | Reauthenticate with Microsoft Entra ID. |
| **Network restrictions** | `Connection blocked by network policy.` | Verify corporate, VPN, or firewall settings. |

> [!IMPORTANT]  
> To keep the tree responsive, browsing large tenants might load children **on demand**. You might notice brief loading indicators when expanding nodes with many items.

### Known limitations

- Only **SQL databases and SQL analytics endpoints** are supported. You can't open other Fabric item types from the MSSQL extension.
- You can browse across tenants by switching tenants, but you must create each database connection separately. There's no unified cross-tenant or multi-database connection.
- Offline sign-in and device-code authentication flows aren't supported.

## Provision a SQL database in Fabric

The SQL database provisioning experience is integrated into the Deployments page of the MSSQL extension. You can create and connect to new Fabric SQL databases without leaving Visual Studio Code.

### Capabilities

- **Guided workflow**: Walks you through authentication, workspace selection or creation, database naming, and provisioning from the **Deployments** page.

- **Immediate connection**: Automatically adds the newly provisioned database to your connections list.

- **Consistent experience**: Follows the same provisioning flow as other supported backends, such as local SQL Server containers.

- **Capacity awareness**: Indicates when capacity constraints disable workspaces, with tooltips explaining the reason.

### Prerequisites

- A valid Microsoft Fabric subscription.
- Sufficient capacity in the target workspace to provision new SQL databases.
- Microsoft Entra ID account with the required workspace permissions (Admin or Member).
- Visual Studio Code with the MSSQL extension installed.

### Get started

1. Open the **Deployments** page and choose **SQL database in Fabric**.
1. **Authenticate** with Microsoft Entra ID.
1. **Select or create** a Fabric workspace.
1. Enter a **database name**, and then select **Create**.
1. When provisioning finishes, the database is **auto-connected** and appears under **Connections**, with a success confirmation.

> [!TIP]  
> The end-to-end flow typically finishes in less than a minute.

The following animation shows the SQL database in Fabric provisioning workflow in action:

:::image type="content" source="media/mssql-fabric-integration/fabric-provisioning.gif" alt-text="Screenshot of animation showing the end-to-end SQL database in Fabric provisioning flow." lightbox="media/mssql-fabric-integration/fabric-provisioning.gif":::

### Capacity awareness details

- The provisioning flow validates the Fabric account and workspace capacity to ensure the database can be created successfully.
- The workspace dropdown list shows workspaces without available capacity as **disabled**, with a tooltip explaining the reason.

:::image type="content" source="media/mssql-fabric-integration/fabric-provisioning-capacity.png" alt-text="Screenshot of workspace dropdown list showing disabled workspaces due to capacity constraints.":::

### Post-provisioning behavior

- The new connection adopts the **Connection Group** you selected (optional) for quick visual identification.
- You can pair this experience with the **Fabric browse** feature to discover and connect to the newly created database from another Visual Studio Code environment.

### Troubleshooting

- **Name already exists**: Choose a unique database name within the selected workspace.
- **Insufficient permissions**: Ensure you have **Workspace Admin / Member** rights to create databases.
- **Network or tenant errors**: Reauthenticate or switch the signed-in account from the account menu.
- **Provisioning timeout or failure**: If the wizard doesn't complete, retry the operation or check Fabric service health.

### Known limitations

- You can only provision **SQL databases** in Fabric. Other Fabric item types aren't supported.
- You can provision across tenants when you explicitly select a different tenant during authentication. However, Visual Studio Code doesn't yet support scenarios involving multiple simultaneous tenants or automated cross-tenant workflows.
- You can't queue provisioning when capacity is fully consumed.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Local SQL Server container](mssql-local-container.md)
- [Schema Designer](mssql-schema-designer.md)
- [Schema Compare](mssql-schema-compare.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
