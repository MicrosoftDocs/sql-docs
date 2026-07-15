---
title: Data API Builder
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use the Data API builder integration in the MSSQL extension for Visual Studio Code to create REST, GraphQL, and MCP endpoints for your SQL databases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: yoleichen, roblescarlos
ms.date: 06/01/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Data API builder

The MSSQL extension for Visual Studio Code includes an integrated UI for [Data API builder](https://aka.ms/dab/), so you can create REST, GraphQL, and MCP endpoints for your SQL database tables without writing configuration files or leaving Visual Studio Code. You can select which tables to expose, configure CRUD permissions, choose API types, preview the generated configuration, and deploy a local backend powered by Data API builder, all from a visual interface.

:::image type="content" source="media/mssql-data-api-builder/data-api-builder.png" alt-text="Screenshot of the Data API builder UI with entity list and CRUD checkboxes in Visual Studio Code." lightbox="media/mssql-data-api-builder/data-api-builder-configuration-view.png":::

> [!IMPORTANT]  
> This feature has known limitations, including SQL authentication-only support for container deployment and restricted data type compatibility. Review [Known limitations](#known-limitations) and [Known issues](#known-issues) before deploying.

## Features

Data API builder integration offers these capabilities:

- Select database entities (tables) and individual columns to expose as API endpoints, organized by schema with collapsible grouping.
- Configure Create, Read, Update, and Delete (CRUD) permissions independently for each entity.
- Use column-level configuration to choose exactly which columns are included in the generated API.
- Choose API types to generate: REST, GraphQL, MCP, or any combination.
- Configure advanced entity settings including custom REST paths, custom GraphQL type names, and authorization roles.
- Preview the generated Data API builder JSON configuration in a read-only Definition panel.
- Deploy Data API builder locally as a Docker container with automated prerequisite checks.
- Test running APIs directly in Visual Studio Code using the built-in Simple Browser.
- Use GitHub Copilot chat to configure entities through natural language prompts.

## Prerequisites

Before you use Data API builder, ensure the following requirements are met:

- The MSSQL extension for Visual Studio Code is installed. For installation steps, see the [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md) overview.
- An active database connection is established through the MSSQL extension. For connection steps, see [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md).
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) is installed and running on your machine (required for local deployment).
- (Optional) GitHub Copilot and GitHub Copilot Chat extensions are installed for AI-assisted entity configuration.

## Open Data API builder

You can open the Data API builder configuration view from two entry points:

- **From the Object Explorer**: Right-click on a database node and select **Build Data API...**.

  :::image type="content" source="media/mssql-data-api-builder/data-api-builder-configuration-view.png" alt-text="Screenshot of the Data API builder configuration view with entity list and CRUD checkboxes in Visual Studio Code." lightbox="media/mssql-data-api-builder/data-api-builder-configuration-view.png":::

- **From the Schema Designer**: Select the **Design API** button (button in the top-right corner of the toolbar), or select the **Backend** icon in the left-side panel.

  :::image type="content" source="media/mssql-data-api-builder/data-api-builder-entry-point.png" alt-text="Screenshot of the Design API button and Backend icon in the Schema Designer toolbar." lightbox="media/mssql-data-api-builder/data-api-builder-entry-point.png":::

The Data API builder configuration view opens, displaying your database entities, API type options, and configuration controls.

## Select entities

The entity selection view lists all tables from your connected database, grouped by schema.

- Each schema row is collapsible and shows a count badge indicating how many entities are enabled (for example, "3/5").
- Select a schema-level checkbox to toggle all entities in that schema. The checkbox supports tri-state selection: all, none, or mixed.
- Each entity row displays: the enable checkbox, entity name, source table, CRUD checkboxes, and a settings button.
- Disabling an entity grays out its row and disables the CRUD checkboxes and settings button.

Use the **filter box** at the top to search entities by name, schema, or source table. The filter is case-insensitive, and the enabled count updates based on filtered results.

:::image type="content" source="media/mssql-data-api-builder/data-api-builder-text-filter.png" alt-text="Screenshot of the entity filter box with a search term filtering the entity list." lightbox="media/mssql-data-api-builder/data-api-builder-text-filter.png":::

## Configure permissions and API types

### CRUD permissions

Toggle individual **Create**, **Read**, **Update**, and **Delete** checkboxes for each entity. The header-level CRUD checkboxes toggle that action for all enabled entities and support tri-state selection.

### API type selection

At the top of the configuration view, select the API types to generate:

- **REST API**: Generates REST endpoints with Swagger UI for testing.
- **GraphQL**: Generates GraphQL endpoints with Nitro GraphQL playground.
- **MCP** ([Preview](https://aka.ms/sql/mcp/preview)): Generates Model Context Protocol endpoints.
- **All**: Selects or deselects all API types.

Select at least one API type.

:::image type="content" source="media/mssql-data-api-builder/data-api-builder-api-type-selection.png" alt-text="Screenshot of the REST API, GraphQL, MCP, and All checkboxes at the top of the Data API builder configuration view." lightbox="media/mssql-data-api-builder/data-api-builder-api-type-selection.png":::

### Advanced entity configuration

Select the gear icon on an entity row to open the **Advanced Entity Configuration** dialog, where you can configure:

- **Entity Name**: The name used in API routes and responses (defaults to the table name).
- **Authorization Role**: Toggle between **Anonymous** (no authentication required) and **Authenticated** (requires user authentication).
- **Custom REST Path**: Optional override for the default `api/entityName` path.
- **Custom GraphQL Type**: Optional override for the default GraphQL type name.

Select **Apply Changes** to save your configuration, or **Cancel** to discard.

:::image type="content" source="media/mssql-data-api-builder/data-api-builder-entity-settings.png" alt-text="Screenshot of the Advanced Entity Configuration dialog showing Entity Name, Authorization Role, Custom REST Path, and Custom GraphQL Type fields." lightbox="media/mssql-data-api-builder/data-api-builder-entity-settings.png":::

## Preview configuration

Select the **View Config** button in the toolbar to open the **Definition** panel at the bottom of the configuration view. This panel shows the generated Data API builder JSON configuration file in a read-only format.

The Definition panel:

- Reflects the current entity selection, API types, and advanced settings.
- Stays in sync with the UI and GitHub Copilot chat: changes made in either location immediately update the preview.
- Only includes enabled entities in the configuration output.
- Shows REST, GraphQL, and MCP runtime sections based on selected API types.

Select **Open in Editor** to view the configuration in a full Visual Studio Code editor tab. Select **Copy** to copy the configuration to the clipboard.

:::image type="content" source="media/mssql-data-api-builder/data-api-builder-config-preview.png" alt-text="Screenshot of the Definition panel showing the generated Data API builder JSON configuration with Open in Editor and Copy buttons." lightbox="media/mssql-data-api-builder/data-api-builder-config-preview.png":::

## Deploy locally with Docker

Data API builder deploys as a local Docker container. The deployment wizard guides you through the process:

1. Select the **Deploy** button in the toolbar.

1. The **Deploy DAB Container** dialog opens, describing the local container deployment. Select **Next**.

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-deploy-dialog.png" alt-text="Screenshot of the Deploy DAB Container dialog with the local container deployment description." lightbox="media/mssql-data-api-builder/data-api-builder-deploy-dialog.png":::

1. The **Getting Docker Ready** screen runs prerequisite checks sequentially:

   - **Checking Docker installation**: Verifies Docker is installed on your system.
   - **Starting Docker Desktop**: Ensures Docker Desktop is running.
   - **Checking Docker engine**: Verifies the Docker engine is ready.

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-docker-check-start.png" alt-text="Screenshot of the Docker prerequisite checks running sequentially." lightbox="media/mssql-data-api-builder/data-api-builder-docker-check-start.png":::

   Select **Next** to proceed once all checks are complete.

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-docker-check-finish.png" alt-text="Screenshot of the Docker prerequisite checks completed successfully." lightbox="media/mssql-data-api-builder/data-api-builder-docker-check-finish.png":::

1. The **Container Settings** screen appears:

   - **Container Name**: Optional name for the Docker container (an autogenerated default is provided).
   - **Port**: The port to expose the API on (default: `5000`).
   - The container reuses the connection string from the active database connection.

   Select **Create Container**.

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-container-settings.png" alt-text="Screenshot of the Container Settings screen with Container Name and Port fields." lightbox="media/mssql-data-api-builder/data-api-builder-container-settings.png":::

1. The deployment executes three steps sequentially: pull image, start container, and check readiness.

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-deployment.png" alt-text="Screenshot of the deployment progress showing container creation steps." lightbox="media/mssql-data-api-builder/data-api-builder-deployment.png":::

1. On successful deployment, the wizard displays the endpoint URLs for each enabled API type:

   | API type | Endpoint | Action |
   | --- | --- | --- |
   | REST | `http://localhost:{port}/api` | **View Swagger** opens the Swagger UI |
   | GraphQL | `http://localhost:{port}/graphql` | **Nitro** opens the GraphQL playground |
   | MCP | `http://localhost:{port}/mcp` | **Add to VS Code** writes the MCP server configuration to `.vscode/mcp.json` |

   Select any link to open the testing interface in the Visual Studio Code built-in Simple Browser.

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-deployment-complete.png" alt-text="Screenshot of the deployment complete screen showing the Data API builder container is running with endpoint URLs." lightbox="media/mssql-data-api-builder/data-api-builder-deployment-complete.png":::

   The following example shows the Swagger UI for testing REST endpoints directly in Visual Studio Code:

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-swagger.png" alt-text="Screenshot of the Swagger UI for REST endpoints in the Visual Studio Code Simple Browser." lightbox="media/mssql-data-api-builder/data-api-builder-swagger.png":::

   The following example shows the Nitro GraphQL playground for testing GraphQL queries and mutations:

   :::image type="content" source="media/mssql-data-api-builder/data-api-builder-graphql-playground.png" alt-text="Screenshot of the Nitro GraphQL playground in the Visual Studio Code Simple Browser." lightbox="media/mssql-data-api-builder/data-api-builder-graphql-playground.png":::

## Test the running API

After deployment, you can test your APIs directly from the deployment completion dialog using the Visual Studio Code built-in Simple Browser.

### REST API

Select **View Swagger** to open the [Swagger UI](https://swagger.io/tools/swagger-ui/), an interactive visual interface for exploring and testing REST endpoints. You can browse available entities, view request and response schemas, and execute API calls directly.

Data API builder generates the following REST endpoints for each enabled entity:

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/api/{entity}` | List all records for an entity |
| `GET` | `/api/{entity}/{primaryKey}/{value}` | Get a single record by primary key |
| `POST` | `/api/{entity}` | Create a new record |
| `PUT` | `/api/{entity}/{primaryKey}/{value}` | Replace an existing record |
| `PATCH` | `/api/{entity}/{primaryKey}/{value}` | Update specific fields on a record |
| `DELETE` | `/api/{entity}/{primaryKey}/{value}` | Delete a record |

For more information on REST endpoints, see [Data API builder REST API](/azure/data-api-builder/concept/api/rest).

### GraphQL

Select **Nitro** to open the [Nitro](https://chillicream.com/docs/nitro) GraphQL playground, where you can write and test GraphQL queries and mutations interactively.

For more information on GraphQL endpoints, see [Data API builder GraphQL API](/azure/data-api-builder/concept/api/graphql).

### MCP

Select **Add to VS Code** to write the MCP server configuration to `.vscode/mcp.json`. This configuration makes the Data API builder endpoint available as an MCP server within Visual Studio Code. AI tools such as GitHub Copilot can then interact with your database through the Data API builder API.

For more information about MCP in Visual Studio Code, see [Use MCP servers in Visual Studio Code](https://code.visualstudio.com/docs/copilot/customization/mcp-servers).

### Terminal testing

You can also test endpoints from the terminal:

**REST API**:

Get all records from a specific entity:

```bash
curl http://localhost:{port}/api/{entityName}
```

Create a new record (if Create permission is enabled):

```bash
curl -X POST http://localhost:{port}/api/{entityName} \
  -H "Content-Type: application/json" \
  -d '{"Column1": "Value1", "Column2": "Value2"}'
```

**GraphQL**:

```bash
curl -X POST http://localhost:{port}/graphql \
  -H "Content-Type: application/json" \
  -d '{"query": "{ {entityName} { items { Column1 Column2 } } }"}'
```

> [!TIP]  
> Replace `{port}` with the port you configured during deployment (default: `5000`).

## GitHub Copilot integration

For developers who prefer natural language, GitHub Copilot is built into the Data API builder experience. Select the **Chat** button in the toolbar to open a GitHub Copilot chat session scoped to the Data API builder configuration context. GitHub Copilot and the UI stay in sync: changes made through chat are immediately reflected in the UI and vice versa.

Here are some example prompts:

- `"Enable all SalesLT entities for read operations"`
- `"Expose only the Customer and Product tables with full CRUD permissions"`
- `"Set all entities in the dbo schema to read-only"`
- `"Disable the BuildVersion and ErrorLog entities"`
- `"Can you also enable MCP for the Data API builder API?"`

The following example shows GitHub Copilot enabling entities and configuring CRUD permissions through a chat prompt:

:::image type="content" source="media/mssql-data-api-builder/data-api-builder-chat-prompt-1.png" alt-text="Screenshot of GitHub Copilot enabling entities and setting CRUD permissions through a natural language prompt in the Data API builder chat." lightbox="media/mssql-data-api-builder/data-api-builder-chat-prompt-1.png":::

The following example shows GitHub Copilot enabling MCP endpoints for the Data API builder configuration:

:::image type="content" source="media/mssql-data-api-builder/data-api-builder-chat-prompt-2.png" alt-text="Screenshot of GitHub Copilot enabling MCP endpoints through a natural language prompt in the Data API builder chat." lightbox="media/mssql-data-api-builder/data-api-builder-chat-prompt-2.png":::

> [!NOTE]  
> GitHub Copilot integration requires the GitHub Copilot and GitHub Copilot Chat extensions to be installed and signed in. For setup instructions, see [Set up GitHub Copilot](../github-copilot/overview.md#set-up-github-copilot-in-visual-studio-code).

## Known limitations

- **Tables only**: The configuration UI supports tables only. Views and stored procedures aren't available in the designer at this time.
- **Docker Desktop required**: Local deployment requires Docker Desktop to be installed and running.
- **SQL authentication only**: Local Docker containers don't support Microsoft Entra ID authentication methods, such as `ActiveDirectoryInteractive`, because the container environment can't open a browser for the interactive sign-in flow. The extension displays a notification if your current connection uses an unsupported authentication type.
- **SQL database in Microsoft Fabric isn't supported**: SQL database in Microsoft Fabric requires Microsoft Entra authentication exclusively and doesn't support SQL authentication. Because local container deployment requires SQL authentication, deploying against SQL database in Fabric isn't a viable scenario.
- **Primary key required**: Every table entity exposed through Data API builder must have a primary key constraint defined at the database level. Tables without a primary key cause the Data API builder engine to fail at startup.
- **AI-generated output should be reviewed**: GitHub Copilot might produce incorrect or suboptimal configurations. Always review generated configurations before deploying.

## Known issues

- **Unsupported SQL Server data types**: Data API builder can't serialize certain SQL Server data types. Tables containing columns with unsupported types can cause the engine to fail at startup. Unsupported types include `geography`, `geometry`, `hierarchyid`, `rowversion`, `sql_variant`, and `xml`. The extension marks affected entities with a warning icon and prevents them from being selected for deployment. For the latest information on data type support, see [GitHub issue #3181](https://github.com/Azure/data-api-builder/issues/3181).
- **Interactive Microsoft Entra ID authentication not supported for container deployment**: The Data API builder container can't perform interactive Microsoft Entra authentication. Connections using interactive Microsoft Entra ID methods are blocked with a notification. For more information, see [GitHub issue #3246](https://github.com/Azure/data-api-builder/issues/3246).
- **MCP is in preview**: The Data API builder MCP experience is currently in preview. For more information, see [Data API builder MCP Preview](https://aka.ms/sql/mcp/preview).

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [What is Data API builder?](https://aka.ms/dab/)
- [Data API builder documentation](/azure/data-api-builder)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Schema Designer](mssql-schema-designer.md)
- [GitHub Copilot integration in Schema Designer](mssql-schema-designer-copilot.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [Local SQL Server container](mssql-local-container.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
