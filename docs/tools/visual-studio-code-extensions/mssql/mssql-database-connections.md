---
title: Connect to a Database
titleSuffix: MSSQL Extension for Visual Studio Code
description: Connect to SQL Server, Azure SQL, or SQL database in Fabric with the MSSQL extension for Visual Studio Code. Manage connections, authentication, and groups.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos, benjind
ms.date: 06/01/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Connect to a database with the MSSQL extension for Visual Studio Code

The MSSQL extension for Visual Studio Code centers around your connections to SQL Server, Azure SQL, and SQL database in Microsoft Fabric. This article shows you how to create connections with the Connection Dialog and which authentication types the extension supports. It also explains how to organize saved connections in the Object Explorer and how to choose the connection that the extension uses when you open a new SQL file.

After you connect, see [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md) to create a database, add data, and run Transact-SQL statements.

## Connection Dialog

The Connection Dialog is the primary way to create or edit a connection. Open it by selecting **Add Connection** in the Object Explorer, or by running the **MS SQL: Add Connection** command from the Command Palette.

The dialog has three main areas:

- A **form area** in the middle, where you fill in connection details.
- A **Saved Connections** and **Recent Connections** panel on the right side, listing connections you can quickly reopen or edit.
- A footer with the **Advanced**, **Test connection**, **Save without connecting**, and **Connect** actions.

:::image type="content" source="media/mssql-database-connections/connection-dialog.png" alt-text="Screenshot of the Connection Dialog for the MSSQL extension, showing several input modes and parameters, and a sidebar with recently used and saved connections. " lightbox="media/mssql-database-connections/connection-dialog.png":::

### Input types

At the top of the form, the **Input type** selector controls how you enter connection details. Use the input type that best matches what you already know about the target server.

- **Parameters**: Fill in individual fields such as server name, database name, authentication type, user name, and password. This input type is the default and is the easiest option for most scenarios.

- **Connection String**: Paste a full ADO.NET-style connection string. This input type is useful when a connection string was already provided to you (for example, from the Azure portal or by an administrator), or when you need to configure options not exposed in the **Parameters** view.

- **Browse Azure**: Sign in to Azure and pick a server and database from your subscriptions. You can filter by subscription and resource group to find the database you want. This option works for Azure SQL Database and Azure SQL Managed Instance (both private and public endpoints are listed). You can favorite subscriptions so they appear at the top of the list and load automatically.

- **Browse Fabric**: Sign in to Microsoft Fabric and pick a SQL database from one of your workspaces. The dialog lists workspaces you have access to and the SQL databases inside them. Like when browsing Azure, you can favorite workspaces so they appear at the top of the list and load automatically.

Both **Browse Azure** and **Browse Fabric** use the Microsoft accounts that you sign in to Visual Studio Code with. You can sign in with as many Microsoft accounts as you like and switch between accounts and their tenants using the dropdowns at the top of the browse panel. GitHub accounts can't be used to browse Azure and Fabric databases.

:::image type="content" source="media/mssql-database-connections/connection-input-type.png" alt-text="Screenshot of the Input type selector at the top of the Connection Dialog, showing Parameters, Connection String, Browse Azure, and Browse Fabric." lightbox="media/mssql-database-connections/connection-input-type.png":::

### Choose a database

The **Database** field lets you connect to the server's default database or to a specific database.

- Leave the field set to **\<Default\>** to connect to whatever default database the server assigns your login. This is the simplest option, and you can still switch databases later from the editor.

- Select a specific database from the dropdown list. After you fill in enough details to authenticate, the extension tries to fetch the list of databases on the server in the background. If the list loads successfully, you can pick from it.

- If the database list can't be loaded - for example, when your login doesn't have permission to enumerate databases on the server - you can still type the database name directly into the field.

:::image type="content" source="media/mssql-database-connections/connection-database-dropdown.png" alt-text="Screenshot of the Database dropdown list in the Connection Dialog with a populated list of databases." lightbox="media/mssql-database-connections/connection-database-dropdown.png":::

### Advanced settings

Select **Advanced** in the footer to open a side panel with the full set of more connection options, such as **Always Encrypted**, **Command Timeout**, and **Application Intent** (read-only or read-write). These are the same options you'd find in a connection string, grouped into categories such as **Security**, **Connection Resiliency**, and **Pooling**. Use the search box at the top of the panel to quickly find a specific setting.

:::image type="content" source="media/mssql-database-connections/connection-advanced-settings.png" alt-text="Screenshot of the Advanced Connection Settings drawer with the search box and setting categories." lightbox="media/mssql-database-connections/connection-advanced-settings.png":::

### Footer actions

The buttons in the footer control what happens when you're done filling in the form.

- **Connect**: Connects to the database and adds the connection to your saved connections list.

- **Test connection**: Tries to connect using the current form values without saving anything. Use this to verify that the server, credentials, and other settings are correct before committing to a connection.

- **Save without connecting**: Saves the connection profile to your list of saved connections, but doesn't open a session. This is useful when you're setting up connections in advance, or when you want to rename an existing connection without connecting.

### Work with existing connections

The **Saved Connections** and **Recent Connections** lists on the right side of the dialog make it easy to start from a connection you already have.

- Hover over a saved connection to reveal an action menu. From there, you can **Edit** the connection's details, **Create a new connection** based on an existing one (a useful shortcut when several connections share the same server but different databases or credentials), or remove the connection from the list.

- Recent connections work the same way but are limited to connections you used recently, even if they aren't saved.

:::image type="content" source="media/mssql-database-connections/connection-recent-saved-connections.png" alt-text="Screenshot of the Saved and Recent Connections panel in the Connection Dialog with the hover actions menu." lightbox="media/mssql-database-connections/connection-recent-saved-connections.png":::

## Supported authentication types

The MSSQL extension supports several authentication types. Choose the one that matches how your server is configured.

### SQL Login

Enter a user name and password that are defined on the SQL Server itself. SQL Login works for SQL Server, Azure SQL Database, and Azure SQL Managed Instance.

You can optionally save the password so you don't have to reenter it every time you connect.

### Windows Authentication

Use your current Windows account to sign in to the server, with no user name or password required. Windows Authentication only works when you're connecting to a SQL Server instance that's configured to accept it, typically on a domain-joined network or on the same machine as the server.

This option is sometimes called *Integrated Authentication*. It isn't available for Azure SQL Database, Azure SQL Managed Instance, or SQL database in Fabric.

### Microsoft Entra ID - Universal with MFA

Sign in with a Microsoft Entra ID account. This option supports multifactor authentication (MFA), conditional access policies, and personal Microsoft accounts that are guests in a Microsoft Entra tenant.

When you select this option, the dialog prompts you to choose or add a Microsoft Entra ID account, and to pick a tenant if the account has access to more than one.

The extension uses the Microsoft accounts that you've signed into Visual Studio Code with (the same accounts shown in the **Accounts** menu in the lower-left corner of the window). If you aren't signed in to Visual Studio Code yet or haven't given the MSSQL extension permission to use accounts yet, you're prompted to sign in when you connect.

> [!NOTE]  
> If you previously signed in to the MSSQL extension using the extension's own account system (used in MSSQL 1.42.2 and earlier), you'll be prompted to sign in to Visual Studio Code (if you aren't already) the next time you connect using one of your saved connections.
>
> If using Visual Studio Code account system isn't working for you [let us know](https://aka.ms/vscode-mssql-bug). You can fall back to the previous sign-in mechanism by setting `mssql.preview.useVscodeAccountsForEntraMFA` to `false`.

:::image type="content" source="media/mssql-database-connections/connection-entra-mfa-sign-in.png" alt-text="Screenshot of the Microsoft Entra ID account picker in the Connection Dialog, including the Add an account option." lightbox="media/mssql-database-connections/connection-entra-mfa-sign-in.png":::

### Microsoft Entra ID - Default

This option uses *Microsoft Entra ID default authentication*. The Microsoft Data SQL (MDS) driver automatically selects an available Microsoft Entra ID identity from credential providers installed on your system. This authentication type is useful when you have specific authentication requirements that aren't directly supported by the MSSQL extension.

Identities can come from several different sources, such as a signed-in Azure CLI session (`az login`) or environment variables, and you can direct a specific identity to be used by setting the `User name` box. For more information on how default authentication selects an identity, see [DefaultAzureCredential in the Azure Identity client library](../../../connect/ado-net/sql/azure-active-directory-authentication.md#using-default-authentication)

### Microsoft Entra ID - Service Principal

Authenticate as a Microsoft Entra ID *service principal* (an application identity rather than a user). Use this option for automation scenarios, shared workstations, or any case where it's preferable to grant database access to an application identity instead of a person.

When you select this option, the **User name** and **Password** fields are repurposed:

- Enter the service principal's **Application (Client) ID** as the user name.
- Enter the service principal's **Client Secret** as the password.

For more information on how to use a service principal with SQL, see [Service Principal in the Azure Identity client library](../../../connect/ado-net/sql/azure-active-directory-authentication.md#using-service-principal-authentication)

## Connections in the Object Explorer

Every connection you create from the Connection Dialog (whether you connect immediately or save without connecting) appears in the **Object Explorer** in the MSSQL view. The Object Explorer is where you go to browse server contents, run actions like backups, and reconnect to databases you've used before.

### Connection groups

Connections can be organized into **connection groups**. Groups act like folders: you can name them, assign a color, and place connections inside them to keep environments visually separated (for example, *Production*, *Staging*, and *Local*).

- **Create a group**: Use the **New Connection Group** command, or assign a new group while creating or editing a connection.

- **Drag and drop to organize**: Drag a connection onto a group to move it into that group. Drag a group onto another group to nest it. You can create nested groups several levels deep.

- **Expand and collapse**: Use the chevrons next to each group to expand and collapse it, so you only see the connections you're currently working with. To always start with groups collapsed when Visual Studio Code launches, enable the `mssql.collapseConnectionGroupsOnStartup` setting.

:::image type="content" source="media/mssql-database-connections/connection-object-explorer.png" alt-text="Screenshot of the Object Explorer showing connection groups, including nested groups and contained connections." lightbox="media/mssql-database-connections/connection-object-explorer.png":::

### Connection context menu

Right-click a server connection in the Object Explorer to see actions that apply to the connection itself. The most commonly used connection-related options include:

- **Connect** / **Disconnect**: Start or end a session against the server.
- **Edit Connection**: Open the Connection Dialog with the connection profile loaded to edit its parameters.
- **Copy Connection String**: Copy a connection string for the saved connection to your clipboard. This is handy when you need to share the connection with another tool or paste it into application code. Passwords and secrets aren't included.
- **Remove Connection**: Delete the connection from your saved list.

:::image type="content" source="media/mssql-database-connections/connection-copy-connection-string.png" alt-text="Screenshot of the Object Explorer right-click menu for a server connection, with Copy Connection String highlighted." lightbox="media/mssql-database-connections/connection-copy-connection-string.png":::

## Firewall rules for Azure SQL

When you connect to an Azure SQL Database or Azure SQL Managed Instance from a client IP address that isn't allowed by the server's firewall, you can use the MSSQL extension to add a firewall rule via the **Add Firewall Rule** dialog.

In the **Add Firewall Rule** dialog, sign in with a Microsoft account that has permission to manage the server, give the rule a name, and choose whether to allow just your current IP address or a range. After you save the rule, your connection will be retried automatically.

:::image type="content" source="media/mssql-database-connections/connection-add-firewall-rule.png" alt-text="Screenshot of the Add Firewall Rule dialog with fields for rule name and IP range." lightbox="media/mssql-database-connections/connection-add-firewall-rule.png":::

## Workspace Connections

Saved connections and connection groups are stored in your Visual Studio Code `settings.json`. The extension reads connections from two scopes:

- **User (global) settings**: New connections are saved here. They're available in all of your Visual Studio Code sessions, regardless of which folder is open.

- **Workspace settings**: Connections saved at the workspace level are only available when that workspace is open. This scope is useful for project-specific connections that you want to share with collaborators by checking the workspace `.code-workspace` file into source control.

To move a connection from user settings to workspace settings, copy the connection's JSON entry from your user `settings.json` into the workspace `settings.json`, then delete it from the user configuration.

> [!NOTE]  
> The extension doesn't read connections from individual *workspace folder* settings (the per-folder `.vscode/settings.json` inside a multi-root workspace). If you want a connection to apply to a specific project, save it at the workspace level instead.

When you save a connection with a password or secret, its password isn't stored in `settings.json`. Passwords are kept separately in Visual Studio Code's secure credential store.

## Connection selection when opening a new SQL file or editor

When you open a `.sql` file or create a new SQL editor, the extension can either leave the editor disconnected or automatically connect it.

This behavior is controlled by the `mssql.newEditorConnectionBehavior` setting, which supports three modes:

| Mode | Description |
| --- | --- |
| `none` | New SQL editors open without a connection. You're prompted to pick a connection the first time you run a query, or you can use the **SQL: Connect** command to attach a connection manually. |
| `transferActive` (default) | New SQL editors are automatically connected to the same database as your currently active SQL editor. This is convenient when you're working on multiple files against the same database and don't want to reconnect each one. If no SQL editor is currently active, the new editor opens without a connection. |
| `defaultConnection` | New SQL editors are automatically connected to a specific connection that you've designated as your default. The default connection is identified by the `mssql.defaultConnectionId` setting.<br /><br />To use this mode, you also need to set `mssql.defaultConnectionId` to the ID of one of your saved connections. You can find the ID by looking at your saved connections in `settings.json`. If `mssql.defaultConnectionId` isn't set or no longer matches a saved connection, the extension prompts you to choose a default connection the next time you open a new SQL editor. |

You can change these settings from the Visual Studio Code settings UI by searching for `mssql.newEditorConnectionBehavior` or `mssql.defaultConnectionId`.

## Related content

- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Tutorial: Write Transact-SQL statements](../../../t-sql/tutorial-writing-transact-sql-statements.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
