---
title: Local Azure SQL Database and SQL Server Containers
titleSuffix: MSSQL Extension for Visual Studio Code
description: Create, start, stop, and manage free local Azure SQL Database containers in the MSSQL extension for Visual Studio Code. Run your first query and develop with AI.
author: vamahtani
ms.author: vamahtani
ms.reviewer: vamahtani
ms.date: 11/07/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Build locally with Azure SQL Database container and SQL Server container

<a id="local-azuresql-container"></a>

Create and manage a local database without writing container commands by using the [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md). Choose an **Azure SQL Database container (Preview)** to build against the Azure SQL Database engine on your own computer, or a **SQL Server container** for SQL Server development. Start, stop, reconnect to, and delete your container from the extension.

**For a new application targeting Azure SQL Database, start with the Azure SQL Database container.** It's free for local development and testing, requires no Azure subscription or credit card, and works with the drivers and object-relational mappers (ORMs) you use for Azure SQL Database.

> [!NOTE]
> Azure SQL Database container is in public preview. It supports both x64 and ARM devices.

## What is an Azure SQL Database container?

A container packages the database engine so a container runtime can run it on your computer. The MSSQL extension guides you through setup, downloads the image, starts the engine, and saves a connection. Your application connects to a local address such as `localhost,1433`, rather than an Azure SQL logical server.

The Azure SQL Database container runs the Azure SQL Database engine, not the SQL Server container image. It supports Azure SQL Database T-SQL and the Tabular Data Stream (TDS) protocol used by SQL drivers. For example, `SERVERPROPERTY('EngineEdition')` returns `5`, identifying the Azure SQL Database engine. Native vectors, vector search, and JSON help you explore AI-ready data scenarios locally.

The container doesn't provide the managed cloud service. Azure-managed backups, high availability, elastic pools, and serverless auto-pause are cloud capabilities, not local container features. Before deployment, test against Azure SQL Database in Azure: preview restrictions, defaults, and feature support can differ. See [known limitations](https://microsoft.github.io/azure-sql-database-container/known-limitations.html).

## Choose your development database

Use the option that matches your application's target, rather than treating the two container images as interchangeable.

| Option | Choose it when | Cost and account requirements |
| --- | --- | --- |
| Azure SQL Database container (Preview) |You want to learn SQL or build and test an application targeting Azure SQL Database locally. Supports X64 and ARM. Recommended for this local development workflow. |Free for local development and testing. No Azure subscription or credit card required. |
| **SQL Server container** |Your application targets SQL Server or needs SQL Server-specific behavior. Supports only X64. |Use an edition licensed for your scenario. No Azure subscription required for local development. |
| Azure SQL Database free tier |You want a managed database in Azure, cloud integration, or a cloud validation environment. |Requires an Azure subscription. Monthly allowances, eligibility requirements, and pause or paid-overage behavior apply. |

> [!IMPORTANT]
> Azure SQL Database containers are for development, testing, CI, and demos, not production. For production, deploy your application and database to Azure SQL Database in Azure. The free local container and the Azure SQL Database free tier are separate options.

## Prerequisites

For the Azure SQL Database container, you need:

- [Visual Studio Code](https://code.visualstudio.com/).
- A current [MSSQL extension](mssql-extension-visual-studio-code.md#install-the-mssql-extension-for-visual-studio-code) release that includes **Azure SQL Database container (Preview)**.
- A supported container runtime installed and running Linux containers. The Azure SQL Database wizard detects available engines and checks their prerequisites. Options include Docker, Podman, containerd with `nerdctl`, and WSL containers on Windows.
- Enough resources for the database engine. Allocate at least 4 GB of memory and two CPU cores to the container runtime, and keep at least 10 GB of free disk space for the image and a small database. Larger datasets require more disk space.
- Internet access for the initial image download. After you cache the image, local queries don't require a cloud database. External AI endpoints and other online services still require connectivity.

The Azure SQL Database image provides native `linux/amd64` and `linux/arm64` variants for x64 and ARM64 hosts. You can develop on macOS, Linux, or Windows with WSL2. SQL Server containers have separate image and runtime requirements.

## Create an Azure SQL Database container

1. In the MSSQL extension's **Connections** view, select **New Deployment** and **Azure SQL Database container (Preview)**.

   :::image type="content" source="media/mssql-local-container/azure-sql-container-new-deployment.png" alt-text="Screenshot of the Connections view with the New Deployment toolbar action highlighted." lightbox="media/mssql-local-container/azure-sql-container-new-deployment.png":::
   
   :::image type="content" source="media/mssql-local-container/azure-sql-container-choose-deployment.png" alt-text="Screenshot of the New SQL database page with the local Azure SQL Database container preview option highlighted." lightbox="media/mssql-local-container/azure-sql-container-choose-deployment.png":::
   
2. Review the developer container overview, and select **Next**. The wizard checks the engine you installed. If the wizard doesn't find an engine, use the provided installation links to install an engine, and then retry the checks. When the prerequisite checks pass, select **Next**.

   :::image type="content" source="media/mssql-local-container/azure-sql-container-overview.png" alt-text="Screenshot of the Azure SQL Database container overview describing local development, native ARM and x64 images, and container engine options." lightbox="media/mssql-local-container/azure-sql-container-overview.png":::
   
   :::image type="content" source="media/mssql-local-container/azure-sql-container-prerequisites.png" alt-text="Screenshot of the container engine prerequisite page showing Docker detected and installation, startup, and configuration checks complete." lightbox="media/mssql-local-container/azure-sql-container-prerequisites.png":::
   
3. Configure your connection with a strong **Password** of at least eight characters (uppercase letters, lowercase letters, numbers, and symbols). Review and accept **Terms & Conditions**, and select **Next** to provision the container. 

      :::image type="content" source="media/mssql-local-container/azure-sql-container-settings.png" alt-text="Screenshot of container configuration with SQL Login, a masked password, profile name, advanced options, port 1436, and the license terms checkbox." lightbox="media/mssql-local-container/azure-sql-container-settings.png":::

4. Wait for the image download, container creation, engine readiness, and connection to complete.

      :::image type="content" source="media/mssql-local-container/azure-sql-container-provisioning.png" alt-text="Screenshot of provisioning with image download, container creation, readiness, and connection steps complete." lightbox="media/mssql-local-container/azure-sql-container-provisioning.png":::

5. Find the saved connection in **Object Explorer** in the **Connections** view. 

      :::image type="content" source="media/mssql-local-container/azure-sql-container-object-explorer.png" alt-text="Screenshot of the running Azure SQL DB container connection in Object Explorer with Databases and Security nodes." lightbox="media/mssql-local-container/azure-sql-container-object-explorer.png":::

The extension creates a saved connection profile. A container, a connection profile, and an application database are different resources: starting the engine doesn't mean your application's database already exists. Continue with the following steps to create one explicitly.

The wizard also offers Microsoft Entra authentication for more advanced scenarios. It requires additional identity and certificate configuration.

## Create a database and run your first query

Use `appdb` as the example database name. Substitute your project's database name consistently if you choose another name.

1. In **Object Explorer**, right-click your container connection and select **Create Database**.
2. In the **Create Database** dialog, enter `appdb` in **Database Name**.

      :::image type="content" source="media/mssql-local-container/azure-sql-container-create-database.png" alt-text="Screenshot of the Create Database dialog for the local Azure SQL Database container, with Database Name and advanced settings." lightbox="media/mssql-local-container/azure-sql-container-create-database.png":::

3. Use **Add Connection** to connect to the same local server and port with **Database** set to `appdb`. Open a new query editor with this connection and run:

      ```sql
   SELECT DB_NAME() AS current_database,
          SERVERPROPERTY('EngineEdition') AS engine_edition,
          SERVERPROPERTY('Edition') AS edition;
   ```

      Expect `current_database` to be `appdb`, `engine_edition` to be `5`, and `edition` to be `SQL Azure`. If the edition differs, check that you selected the Azure SQL Database container, not SQL Server. Select the database in the connection settings, not with `USE appdb`. Azure SQL Database user-database sessions don't support switching databases with `USE`.

1. Run each of these statements separately in `appdb`. Stop if a statement fails. If `dbo.Tasks` already exists, choose a different table name consistently or a different development database; don't overwrite it.

   ```sql
   CREATE TABLE dbo.Tasks
   (
       TaskId INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
       Title NVARCHAR (200) NOT NULL,
       IsComplete BIT NOT NULL DEFAULT (0)
   );
   ```

   ```sql
   INSERT INTO dbo.Tasks (Title)
   VALUES (N'Create a local Azure SQL Database'),
          (N'Connect an application');
   ```

   ```sql
   SELECT TaskId, Title, IsComplete
   FROM dbo.Tasks
   ORDER BY TaskId;
   ```

   After one successful insert, expect two rows with `TaskId` values `1` and `2`, and `IsComplete` set to `0`.

If you prefer SQL for database creation, connect a query editor to `master` and run `CREATE DATABASE appdb;` instead of using the **Create Database** dialog. 

Use `master` for database provisioning and your user database for application queries. Connecting to a database that doesn't exist doesn't create it, and the container doesn't automatically execute initialization scripts from `/docker-entrypoint-initdb.d`.

## Import your schema and data

If you already have a database package, use the extension's DACPAC and BACPAC operations instead of manually rebuilding the schema.

| Package | What it contains | Operation |
| --- | --- | --- |
| `.dacpac` | Database schema. It doesn't include your table data. | **Publish DACPAC** to deploy the schema to a new or existing development database. |
| `.bacpac` | Database schema and table data. | **Import BACPAC** into a new or empty development database. |

1. Right-click your container connection in **Object Explorer**, and select **DACPAC/BACPAC Operations...**. 

      :::image type="content" source="media/mssql-local-container/azure-sql-container-database-package-operations.png" alt-text="Screenshot of the container connection menu with DACPAC/BACPAC Operations highlighted and Stop SQL Container and Delete SQL Container actions visible." lightbox="media/mssql-local-container/azure-sql-container-database-package-operations.png":::

1. Choose the operation that matches your package, select the package file, and confirm the target container connection and database.
1. Review the target and proposed changes before proceeding. Wait for the operation to complete, then refresh **Databases** and connect directly to the target user database to inspect its schema and data.

Don't import a BACPAC into `appdb` after creating the sample `dbo.Tasks` table: that database isn't empty. Choose a new or empty development database instead. Publish only schemas compatible with Azure SQL Database, and review changes before applying them to a database with data you need.

> [!NOTE]
> SQL Server `.bak` files aren't BACPAC files; `BACKUP DATABASE` and `RESTORE DATABASE` aren't supported by the Azure SQL Database container.

## Connect your application

Use the local server address, the wizard's selected host port, your database name, and your credentials with an Azure SQL-compatible driver. For example, this ADO.NET-style connection string targets `appdb`:

```text
Server=localhost,<host-port>;Database=appdb;User ID=sa;Password=<your-password>;Encrypt=True;TrustServerCertificate=True;
```

Replace both placeholders before connecting. `TrustServerCertificate=True` is for the local container's self-signed certificate only. Use certificate validation and an appropriate authentication method, such as Microsoft Entra authentication, for Azure SQL Database in Azure.

Store connection strings in environment variables or your development secret store, not source control or AI prompts. Other drivers use different connection-string syntax; see the quickstarts for [Python](../../../connect/python/mssql-python/python-sql-driver-mssql-python-quickstart.md) and [.NET](../../../connect/ado-net/get-started-sqlclient-driver.md).

## Develop with AI

You can use [GitHub Copilot in the MSSQL extension](../github-copilot/overview.md) to explain SQL, draft queries, and plan schema changes against your local database. Review generated SQL and the selected database before executing it. Container use is free; GitHub Copilot and external model services have their own access and pricing requirements.

For example, after creating `dbo.Tasks`, give Copilot this prompt:

```text
I'm developing against an Azure SQL Database container, not SQL Server.
Use my existing appdb connection. First confirm the active database and
EngineEdition = 5. Explain the dbo.Tasks schema, then draft a parameterized
query to insert a task and a query to list incomplete tasks.
Don't execute changes, delete data, or create cloud resources.
Don't request or display passwords. Don't use USE to switch databases.
```

For code-first workflows, the [Azure SQL Database container agent skills](https://microsoft.github.io/azure-sql-database-container/agent-skills.html) provide setup, connection, migration, and local-to-cloud guidance for supported AI coding agents.

### Try vector similarity without an embedding service

Run this example on your `appdb` connection to explore [native vectors](../../../t-sql/data-types/vector-data-type.md) and exact similarity search. It uses illustrative three-dimensional vectors, doesn't create a table, and doesn't call a paid model endpoint.

```sql
DECLARE @query VECTOR(3) = '[1.0, 0.0, 0.0]';

SELECT TOP (2)
       v.Label,
       VECTOR_DISTANCE('cosine', CAST(v.Embedding AS VECTOR(3)), @query) AS distance
FROM (VALUES
    (N'Closest', '[1.0, 0.0, 0.0]'),
    (N'Perpendicular', '[0.0, 1.0, 0.0]')
) AS v(Label, Embedding)
ORDER BY distance;
```

Expect `Closest` first with distance `0`, followed by `Perpendicular` with distance `1`. In a real application, generate embeddings with the same model for stored data and queries, and use the model's embedding dimensions. `VECTOR_DISTANCE` performs exact search; it doesn't use a vector index. Review preview limitations before choosing an indexing or embedding strategy.

## Features

The local container experiences provide the following capabilities:

- Create an Azure SQL Database or SQL Server container through a guided wizard.
- Customize the container name, hostname, and port.
- Check container runtime prerequisites before deployment.
- Automatically assign an available port if 1433 is already in use.
- Save connection settings for reconnection.
- Manage the container lifecycle from the **Connections** view.

<a id="create-a-local-container"></a>

## Create a SQL Server container

The following screenshots illustrate the SQL Server wizard, not the Azure SQL Database wizard.

1. Open the deployment wizard and choose **SQL Server container** under **Run locally**. You can also use **Create local SQL Container** from the **Connections** menu.

   :::image type="content" source="media/mssql-local-container/local-container-create.png" alt-text="Screenshot showing the SQL Server container creation option in the MSSQL extension." lightbox="media/mssql-local-container/local-container-create.png":::

1. Review the overview screen and select **Get Started**.

   :::image type="content" source="media/mssql-local-container/local-container-get-started.png" alt-text="Screenshot of the overview screen for local SQL Server container deployment with helpful links and Get Started button." lightbox="media/mssql-local-container/local-container-get-started.png":::

1. The MSSQL extension automatically checks for Docker before deployment:

    - If Docker isn't installed, a message with an install link appears. You must install it before continuing.
    - If Docker is installed but not running, you're prompted to start it. If starting Docker fails, retry or cancel.

   When all prerequisites are met, select **Next**.

   :::image type="content" source="media/mssql-local-container/local-container-prerequisites.png" alt-text="Screenshot of the SQL Server wizard's Docker prerequisite check in the MSSQL extension." lightbox="media/mssql-local-container/local-container-prerequisites.png":::

1. In the **Deployment settings** panel:

    1. Select the SQL Server version ([!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] is selected by default).
    1. Enter a password for the `sa` account.
    1. Enter a name for your connection profile (optional).
    1. Optionally set the container name, port, and hostname under the advanced options.
    1. Accept the license terms.
    1. Select **Create Container**.

   :::image type="content" source="media/mssql-local-container/local-container-settings.png" alt-text="Screenshot of container setup configuration form." lightbox="media/mssql-local-container/local-container-settings.png":::

### How SQL container deployment works

For SQL Server, after you select **Create Container**, the extension:

- Pulls the selected SQL Server image (if not already cached).
- Creates a SQL Server container with the chosen settings.
- Monitors the logs to verify that all databases are fully recovered and ready.
- Creates a connection profile and connects to your container.

When deployment finishes, the MSSQL extension connects to the container. Create or select a user database before running application queries.

## Manage your container

For either container type, right-click its saved connection profile in the **Connections** view to manage its lifecycle.

The following screenshot shows a stopped Azure SQL Database container. Its menu offers **Start SQL Container** and **Delete SQL Container**. When the container is running, the menu offers **Stop SQL Container** instead.

:::image type="content" source="media/mssql-local-container/azure-sql-container-lifecycle.png" alt-text="Screenshot of a stopped Azure SQL Database container with Start SQL Container and Delete SQL Container in its connection menu." lightbox="media/mssql-local-container/azure-sql-container-lifecycle.png":::

### Start a stopped container

Select **Start SQL Container**. The extension checks the associated runtime and starts the container before connecting. If the runtime isn't running, follow the prompt to start it.

### Stop a running container

Select **Stop SQL Container** to release the running container's resources without deleting it. Database files remain available when you restart the same container. This action stops the container, not Azure SQL Database serverless auto-pause.

### Delete the container

Select **Delete SQL Container**, and review the confirmation before proceeding. Deletion removes the container and the selected connection profile. Data stored only inside the container is lost; separately managed volumes have their own lifecycle. Preserve any data you need before deletion.

> [!IMPORTANT]
> Don't delete a container or its storage to troubleshoot a connection unless you can discard its data. If you only want to pause development, stop it instead.

## Supported scenarios

Use the Azure SQL Database container to prototype applications, test migrations, learn T-SQL, and explore vectors and JSON before cloud deployment. You can also use the container image in CI, application sidecars, or offline labs; those runtime-specific workflows aren't configured by the VS Code wizard.

Use the SQL Server container to test SQL Server schemas and features. Don't assume a SQL Server-specific feature works in Azure SQL Database just because it works in a SQL Server container.

## Troubleshoot local development

| Symptom | What to check |
| --- | --- |
| The Azure SQL Database container option is missing. | Update the MSSQL extension to a release that includes the public-preview wizard, and select **New Deployment** again. |
| Runtime prerequisite checks fail. | Confirm that the selected runtime is installed, running, and configured for Linux containers. Correct the reported problem and retry. |
| Image download fails. | Review the registry error and check connectivity and runtime configuration. Don't substitute the SQL Server image for Azure SQL Database. |
| The container starts but connections fail. | Wait for engine readiness, then inspect error details and container logs. Check password complexity and memory allocation. A running container process doesn't prove the database is ready. |
| An application can't reach the database. | Use the actual host port selected by the wizard, not an assumed `1433`. `localhost` refers to the application's own host; an application in another container needs an appropriate container-network address. |
| The database doesn't exist. | Connect to `master`, create the database, and reconnect directly to it. |
| `USE appdb` fails. | Select `appdb` in the connection settings. Don't switch databases in an Azure SQL Database user-database session with `USE`. |
| A SQL Server management operation fails. | Check whether the operation is supported by Azure SQL Database. For example, `BACKUP DATABASE` and `RESTORE DATABASE` aren't supported. |

See [known limitations](https://microsoft.github.io/azure-sql-database-container/known-limitations.html) for current preview gaps and workarounds.

## Clean up sample data

On your `appdb` connection, confirm `SELECT DB_NAME()` returns `appdb`. If you created `dbo.Tasks` in this walkthrough and no longer need it, remove only that sample table:

```sql
DROP TABLE dbo.Tasks;
```

Don't run this statement against a table from another project. Stop the container to keep working later, or delete it only when you no longer need its data.

## Move from local to cloud

- By using the MSSQL extension, you can deploy your local database to the cloud. Learn more from [Local to Cloud](mssql-local-to-cloud-deployment.md).

## Related content

- [Azure SQL Database container agent skills](https://microsoft.github.io/azure-sql-dev-hub/#skills)

- [Create an Azure SQL Database with the free tier](/azure/azure-sql/database/free-tier?tabs=azure-portal)

- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)

- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
