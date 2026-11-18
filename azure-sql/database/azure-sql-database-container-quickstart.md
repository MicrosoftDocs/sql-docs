---
title: Build with Azure SQL Database container
titleSuffix: Azure SQL Database
description: Get started with the Azure SQL Database container for free local development and testing, run your first query, and explore Azure SQL Dev Hub.
author: vamahtani
ms.author: vamahtani
ms.date: 11/10/2026
ms.service: azure-sql-database
ms.subservice: development
ms.topic: quickstart
ai-usage: ai-assisted
monikerRange: "=azuresql || =azuresql-db"
---

# Build with Azure SQL Database container

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

In this quickstart, you get started with the Azure SQL Database container, create a local database, and run your first query. Then, use [Azure SQL Dev Hub](https://aka.ms/azuresql-hub) to build an application with samples, prompts, and AI coding agent skills.

> [!NOTE]
> Azure SQL Database container is in public preview.

## What is the Azure SQL Database container?

A container packages the database engine so you can run it on your computer with a container runtime. The Azure SQL Database container runs the **Azure SQL Database engine**, not the SQL Server container image. Your application connects to a local address, such as `localhost,1433`, instead of a database in Azure.

Use the container to learn T-SQL, develop applications, test schema changes, or run continuous integration (CI) tests. You can also explore native vectors, vector search, and JSON for AI applications. It works with the drivers and object-relational mappers (ORMs) you use for Azure SQL Database.

The container is free for local development and testing. You don't need an Azure subscription or a credit card to run it locally.

> [!IMPORTANT]
> The container is for development, testing, CI, and demos, not production. It doesn't provide Azure-managed services such as automated backups, high availability, or elastic pools. For production, deploy your database and application to Azure services, not the local container.

For more information, see [What is the Azure SQL Database container?](https://aka.ms/azuresqldb-container).

## Prerequisites

- A container runtime installed and running Linux containers. Choose Docker, Podman, WSL containers, or Apple's `container` tool as described in the next section.
- A supported x64 or ARM64 computer running macOS, Linux, or Windows with Windows Subsystem for Linux 2 (WSL2). The image provides native `linux/amd64` and `linux/arm64` variants.
- At least two CPU cores, 4 GB of memory available to the container runtime, and 10 GB of free disk space for the image and a small database.
- Internet access for the initial image download. Local queries can run offline afterward; external services still require connectivity.
- An available host port, such as `1433`, and a unique container name. The examples use `sqldb`. Choose a different name or host port if either is already in use.

Review the [container prerequisites](https://microsoft.github.io/azure-sql-dev-hub/container/get-started.html#prerequisites) for current platform and runtime requirements.

## Choose a container runtime

Use the runtime that fits your operating system. These examples use native images for your host architecture; they don't configure emulation.

| Runtime | Setup |
| --- | --- |
| Docker | Install [Docker Desktop](https://docs.docker.com/desktop/) or [Docker Engine](https://docs.docker.com/engine/), and make sure it's running with Linux container support. |
| Podman | Install [Podman](https://podman.io/docs/installation). On macOS and Windows, initialize a Podman machine if needed, then start it with `podman machine start`. |
| WSL containers | Install or update [WSL](/windows/wsl/wsl-container) so that `wslc.exe` is available. Run `wslc` from PowerShell on the Windows host. From Bash inside a WSL distribution, use `wslc.exe`; [WSL interoperability](/windows/wsl/filesystems#run-windows-tools-from-linux) must be enabled and `wslc.exe` must be on your `PATH`. The published port is on the Windows host. |
| Apple container | Install Apple's [container tool](https://github.com/apple/container) on an Apple silicon Mac running macOS 26. Start its service with `container system start`. |

Other OCI-compatible runtimes, such as containerd with `nerdctl` or Rancher Desktop, can also run the image. Use your runtime's documentation for its commands and configuration.

## Get the public preview image

The public preview image reference is `mcr.microsoft.com/mssql/azure-sql-db-server:latest`. Get image information from the [Microsoft Artifact Registry listing](https://mcr.microsoft.com/en-us/artifact/mar/mssql/azure-sql-db-server/about). Public images don't require a private preview registry account or registry sign-in.

The following commands download the image automatically if it isn't cached locally. To download it separately with Docker, run:

```console
docker pull mcr.microsoft.com/mssql/azure-sql-db-server:latest
```

For Podman, WSL containers, or Apple container, use `podman pull`, `wslc pull`, or `container image pull`, respectively, with the same image reference.

## Start the container

Replace `<your-strong-password>` with a unique, strong password for the `sa` authentication. Use at least eight characters with characters from at least three of these groups: uppercase letters, lowercase letters, digits, and symbols. Keep passwords out of source control and shared terminal output. The password is also visible in the container configuration.

Choose your runtime, then run its command. Setting `ACCEPT_EULA=Y` accepts the image's license terms. Each example allocates 2 GiB of memory and two CPUs to the container and publishes database port `1433` on the host's loopback address.

### [Docker](#tab/docker)

```console
docker run --name sqldb --memory 2g --cpus 2 -e ACCEPT_EULA=Y -e "MSSQL_SA_PASSWORD=<your-strong-password>" -p 127.0.0.1:1433:1433 -d mcr.microsoft.com/mssql/azure-sql-db-server:latest
```

### [Podman](#tab/podman)

```console
podman run --name sqldb --memory 2g --cpus 2 -e ACCEPT_EULA=Y -e "MSSQL_SA_PASSWORD=<your-strong-password>" -p 127.0.0.1:1433:1433 -d mcr.microsoft.com/mssql/azure-sql-db-server:latest
```

### [WSL containers](#tab/wsl)

Run this command from PowerShell on the Windows host:

```powershell
wslc run --name sqldb --memory 2g --cpus 2 -e ACCEPT_EULA=Y -e "MSSQL_SA_PASSWORD=<your-strong-password>" -p 127.0.0.1:1433:1433 -d mcr.microsoft.com/mssql/azure-sql-db-server:latest
```

From Bash inside a WSL distribution, replace `wslc` with `wslc.exe`.

### [Apple container](#tab/apple)

```console
container run --name sqldb --memory 2g --cpus 2 -e ACCEPT_EULA=Y -e "MSSQL_SA_PASSWORD=<your-strong-password>" -p 127.0.0.1:1433:1433 -d mcr.microsoft.com/mssql/azure-sql-db-server:latest
```

Apple containers also have a virtual-machine network IP address reachable directly from the host and other containers on the same network. Publishing to loopback doesn't provide network isolation. See the [Apple container networking guide](https://github.com/apple/container/blob/main/docs/networking.md).

---

A running container doesn't necessarily mean the database engine is ready. Use the `sqlcmd` utility bundled with the container to check for a successful SQL connection:

```console
docker exec sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "<your-strong-password>" -C -b -l 5 -t 5 -Q "SELECT @@VERSION;"
```

Replace `docker` with `podman`, `wslc` (or `wslc.exe` inside WSL), or `container` for your runtime. Retry while the engine starts. Successful output includes `Microsoft SQL Azure`. The `-b` option returns an error exit code for SQL errors; `-l 5` and `-t 5` limit login and query waits to five seconds each. `-C` trusts the container's self-signed certificate for this local connection; use proper certificate validation outside local development.

This check connects inside the container, not through the published host port. To connect from your editor or application, use server `127.0.0.1,1433`, SQL authentication, login `sa`, and the password you configured. Adjust the port if you changed the host mapping.

If SQL doesn't accept connections, inspect the logs with `docker logs --tail 80 sqldb`, `podman logs --tail 80 sqldb`, `wslc logs --tail 80 sqldb`, or `container logs -n 80 sqldb`. Check the password policy and available resources before retrying.

If you prefer a graphical setup experience, follow [Build locally with Azure SQL Database container and SQL Server container](../../docs/tools/visual-studio-code-extensions/mssql/mssql-local-container.md) in the MSSQL extension for Visual Studio Code, or [Connect and Query an Azure SQL Database Container | Microsoft Learn](/ssms/quickstarts/ssms-connect-query-azure-sql-database-container?view=sql-server-ver17) for SSMS.

## Create a database and run a query

Use a SQL client, such as the `sqlcmd` utility bundled with the container, or your editor's query window. For example, open an interactive `master` connection with the following command, replacing `docker` with your runtime's CLI and using your configured password:

```console
docker exec -it sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "<your-strong-password>" -C -b -d master
```

In an interactive `sqlcmd` session, enter `GO` on a new line after each SQL statement or query to execute it. Enter `EXIT` to close the session.

1. Connect to the local container's `master` database with the credentials you configured, and run:

   ```sql
   CREATE DATABASE appdb;
   ```

   The name `appdb` is an example; you can choose a different database name. If your setup tool already created a user database, use that database and skip this step.

1. Open a new connection directly to `appdb`. Specify the database in your client's connection settings or with `-d appdb` in `sqlcmd`. Don't use `USE` to switch databases in an Azure SQL Database user-database session.

   For the interactive command preceding this step, exit the `master` session, then run the command again with `-d appdb` instead of `-d master`.

1. Run this query on the `appdb` connection:

   ```sql
   SELECT
       DB_NAME() AS current_database,
       SERVERPROPERTY('EngineEdition') AS engine_edition,
       SERVERPROPERTY('Edition') AS edition,
       1 AS connected;
   ```

   Expect `current_database` to be `appdb`, `engine_edition` to be `5`, `edition` to be `SQL Azure`, and `connected` to be `1`. These values confirm that you connected to your user database on the Azure SQL Database engine. If the edition differs, check that you started the Azure SQL Database image rather than the SQL Server image.

You now have a local database for application development. Use the same database name in your application's connection string.

## Build with Azure SQL Dev Hub

Visit [Azure SQL Dev Hub](https://aka.ms/azuresql-hub) to choose an application scenario and get started with prompts, samples, and agent skills. You can build a task-tracking app, create an API, or explore vector search for a retrieval-augmented generation (RAG) application.

Some Dev Hub prompts, such as the JavaScript app prompt, target cloud databases and require Microsoft Entra authentication. Before running a prompt locally, adapt both its instructions and connection configuration to your container's server, mapped port, database name, and authentication method. If you use the setup guide's default SQL-authentication path, replace cloud-only authentication requirements with your configured SQL credentials, keep credentials out of source control, and trust the self-signed certificate only for the local container connection. Review generated SQL, permissions, and schema changes before you run them.

When you're ready to move to Azure, create a database in Azure SQL Database, deploy your schema and data, and update your application's connection configuration. An Azure subscription is required for cloud resources. The [Azure SQL Database free tier](free-tier.md) is a separate cloud option with its own allowances and limits.

Before deployment, test against Azure SQL Database in Azure. Preview restrictions, defaults, and feature support can differ from the cloud service. Review the [known limitations](https://microsoft.github.io/azure-sql-dev-hub/container/faq.html#feature-availability).

## Clean up resources

When you finish, stop the container with your runtime or development tool.

| Runtime | Stop the container | Remove the stopped container |
| --- | --- | --- |
| Docker | `docker stop sqldb` | `docker rm sqldb` |
| Podman | `podman stop sqldb` | `podman rm sqldb` |
| WSL containers | `wslc stop sqldb` | `wslc rm sqldb` |
| Apple container | `container stop sqldb` | `container delete sqldb` |

The run commands in this quickstart don't configure persistent storage. Removing the container can delete its database files, so preserve any data you need first. If you configure a data volume separately, remove it only if you intend to delete its data.

## Related content

- [Azure SQL Dev Hub](https://microsoft.github.io/azure-sql-dev-hub/).
- [Build locally with Azure SQL Database container and SQL Server container](/sql/tools/visual-studio-code-extensions/mssql/mssql-local-container).
- [Develop applications with Azure SQL Database](develop-overview.md).
- [Create a database in Azure SQL Database using the Azure portal](single-database-create-quickstart.md).
