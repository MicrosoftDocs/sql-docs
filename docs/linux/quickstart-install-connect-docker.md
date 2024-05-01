---
title: "Docker: Install containers for SQL Server on Linux"
description: This quickstart shows how to use Docker to run the SQL Server Linux container images. You connect to a database and run a query.
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 02/22/2024
ms.service: sql
ms.subservice: linux
ms.topic: quickstart
ms.custom:
  - intro-quickstart
  - kr2b-contr-experiment
  - linux-related-content
zone_pivot_groups: cs1-command-shell
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017"
---
# Quickstart: Run SQL Server Linux container images with Docker

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

In this quickstart, you use Docker to pull and run the [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] Linux container image, [mssql-server-linux](https://mcr.microsoft.com/product/mssql/server/about). Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md).

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true#pullandrun2019) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver16&preserve-view=true#pullandrun2022) versions of this article.

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

In this quickstart, you use Docker to pull and run the [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] Linux container image, [mssql-server-linux](https://mcr.microsoft.com/product/mssql/server/about). Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md).

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-2017&preserve-view=true#pullandrun2017) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver16&preserve-view=true#pullandrun2022) versions of this article.

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

In this quickstart, you use Docker to pull and run the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Linux container image, [mssql-server-linux](https://mcr.microsoft.com/product/mssql/server/about). Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for [!INCLUDE [sssql22](../includes/sssql22-md.md)] on Linux](sql-server-linux-release-notes-2022.md).

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-2017&preserve-view=true#pullandrun2017) or [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true#pullandrun2019) versions of this article.

::: moniker-end

This image consists of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running on Linux based on Ubuntu. It can be used with the Docker Engine 1.8+ on Linux.

The examples in this article use the `docker` command. However, most of these commands also work with Podman. Podman provides a command-line interface similar to the Docker Engine. You can [find out more about Podman](http://docs.podman.io/en/latest).

> [!IMPORTANT]  
> **sqlcmd** doesn't currently support the `MSSQL_PID` parameter when creating containers. If you use the **sqlcmd** instructions in this quickstart, you create a container with the Developer edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Use the command line interface (CLI) instructions to create a container using the license of your choice. For more information, see [Deploy and connect to SQL Server Linux containers](sql-server-linux-docker-container-deployment.md).

## <a id="requirements"></a> Prerequisites

- Docker Engine 1.8+ on any supported Linux distribution. For more information, see [Install Docker](https://docs.docker.com/engine/installation/).

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

- For more information on hardware requirements and processor support, see [SQL Server 2016 and 2017: Hardware and software requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

- For more information on hardware requirements and processor support, see [SQL Server 2019: Hardware and software requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md)

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

- For more information on hardware requirements and processor support, see [SQL Server 2022: Hardware and software requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)

::: moniker-end

- Docker `overlay2` storage driver. This driver is the default for most users. If you aren't using this storage provider and need to change, see the instructions and warnings in the [Docker documentation for configuring overlay2](https://docs.docker.com/storage/storagedriver/overlayfs-driver/#configure-docker-with-the-overlay-or-overlay2-storage-driver).

- Install the latest **[sqlcmd](../tools/sqlcmd/sqlcmd-utility.md?&tabs=go)** on your Docker host.

- At least 2 GB of disk space.

- At least 2 GB of RAM.

- [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"
## <a id="pullandrun2017"></a> Pull and run the SQL Server Linux container image

Before starting the following steps, make sure that you select your preferred shell (**bash**, **PowerShell**, or **cmd**) at the top of this article.

::: zone pivot="cs1-bash"
> [!NOTE]  
> For the bash commands in this article, `sudo` is used. If you don't want to use `sudo` to run Docker, you can configure a `docker` group and add users to that group. For more information, see [Post-installation steps for Linux](https://docs.docker.com/install/linux/linux-postinstall/).
::: zone-end

## [CLI](#tab/cli)

### Pull the container from the registry

Pull the [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] Linux container image from the Microsoft Container Registry.

::: zone pivot="cs1-bash"

```bash
sudo docker pull mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
docker pull mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker pull mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true#pullandrun2019) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver16&preserve-view=true#pullandrun2022) versions of this article.

The previous command pulls the latest [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] Linux container image. If you want to pull a specific image, you add a colon and the tag name, such as `mcr.microsoft.com/mssql/server:2017-GA-ubuntu`. To see all available images, see the [Microsoft Artifact Registry](https://mcr.microsoft.com/product/mssql/server/about).

### Run the container

To run the Linux container image with Docker, you can use the following command from a bash shell or elevated PowerShell command prompt.

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

::: zone pivot="cs1-bash"

```bash
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

> [!NOTE]  
> If you're using PowerShell Core, replace the double quotes with single quotes.

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" `
   -p 1433:1433 --name sql1 --hostname sql1 `
   -d `
   mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" `
   -p 1433:1433 --name sql1 --hostname sql1 `
   -d `
   mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

Your password should follow the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] default password policy, otherwise the container can't set up [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], and stops working. By default, the password must be at least eight characters long and contain characters from three of the following four sets: uppercase letters, lowercase letters, base-10 digits, and symbols. You can examine the error log by using the [`docker logs`](https://docs.docker.com/engine/reference/commandline/logs/) command.

By default, this quickstart creates a container with the Developer edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The process for running production editions in containers is slightly different. For more information, see [Run production container images](./sql-server-linux-docker-container-deployment.md#production).

The following table provides a description of the parameters in the previous `docker run` example:

| Parameter | Description |
| --- | --- |
| `-e "ACCEPT_EULA=Y"` | Set the `ACCEPT_EULA` variable to any value to confirm your acceptance of the End-User Licensing Agreement. Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `-e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>"` | Specify your own strong password that is at least eight characters and meets the [Password Policy](../relational-databases/security/password-policy.md). Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `-e "MSSQL_COLLATION=<SQL_Server_collation>"` | Specify a custom SQL Server collation, instead of the default `SQL_Latin1_General_CP1_CI_AS`. |
| `-p 1433:1433` | Map a TCP port on the host environment (first value) with a TCP port in the container (second value). In this example, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is listening on TCP 1433 in the container and this container port is then exposed to TCP port 1433 on the host. |
| `--name sql1` | Specify a custom name for the container rather than a randomly generated one. If you run more than one container, you can't reuse this same name. |
| `--hostname sql1` | Used to explicitly set the container hostname. If you don't specify the hostname, it defaults to the container ID, which is a randomly generated system GUID. |
| `-d` | Run the container in the background (daemon). |
| `mcr.microsoft.com/mssql/server:2017-latest` | The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image. |

## [sqlcmd](#tab/sqlcmd)

### Pull and run the container

Pull and run the [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] Linux container image from the Microsoft Container Registry.

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd create mssql --tag 2017-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd create mssql --tag 2017-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd create mssql --tag 2017-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true#pullandrun2019) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver16&preserve-view=true#pullandrun2022) versions of this article.

The previous command uses the latest [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] Linux container image. If you want to pull a specific image, change the tag name, such as `2017-GA-ubuntu`. To see all available images, run the following command:

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd create mssql get-tags
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd create mssql get-tags
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd create mssql get-tags
```

::: zone-end

The following table provides a description of the parameters in the previous `sqlcmd create mssql` example:

| Parameter | Description |
| --- | --- |
| `--ACCEPT-EULA` | Include the `ACCEPT-EULA` flag to confirm your acceptance of the End-User Licensing Agreement. Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `--port 1433` | Map a TCP port on the host environment and a TCP port in the container. In this example, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is listening on TCP 1433 in the container and this container port is then exposed to TCP port 1433 on the host. |
| `--name sql1` | Specify a custom name for the container rather than a randomly generated one. If you run more than one container, you can't reuse this same name. |
| `--hostname sql1` | Used to explicitly set the container hostname. If you don't specify the hostname, it defaults to the container ID, which is a randomly generated system GUID. |
| `--tag 2017-latest` | The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image. |

---

### View list of containers

1. To view your Docker containers, use the `docker ps` command.

   ::: zone pivot="cs1-bash"

   ```bash
   sudo docker ps -a
   ```

   ::: zone-end

   ::: zone pivot="cs1-powershell"

   ```PowerShell
   docker ps -a
   ```

   ::: zone-end

   ::: zone pivot="cs1-cmd"

   ```cmd
   docker ps -a
   ```

   ::: zone-end

   You should see output similar to the following example:

   ```output
   CONTAINER ID   IMAGE                                        COMMAND                    CREATED         STATUS         PORTS                                       NAMES
   d4a1999ef83e   mcr.microsoft.com/mssql/server:2017-latest   "/opt/mssql/bin/perm..."   2 minutes ago   Up 2 minutes   0.0.0.0:1433->1433/tcp, :::1433->1433/tcp   sql1
   ```

1. If the `STATUS` column shows a status of `Up`, then [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running in the container and listening on the port specified in the `PORTS` column. If the `STATUS` column for your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container shows `Exited`, see [Troubleshoot SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md). The server is ready for connections once the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error logs display the message: `SQL Server is now ready for client connections. This is an informational message; no user action is required`. You can review the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error log inside the container using the command:

   ```bash
   sudo docker exec -t sql1 cat /var/opt/mssql/log/errorlog | grep connection
   ```

   The `--hostname` parameter, as discussed previously, changes the internal name of the container to a custom value. This value is the name you see returned in the following Transact-SQL query:

   ```sql
   SELECT @@SERVERNAME,
       SERVERPROPERTY('ComputerNamePhysicalNetBIOS'),
       SERVERPROPERTY('MachineName'),
       SERVERPROPERTY('ServerName');
   ```

   Setting `--hostname` and `--name` to the same value is a good way to easily identify the target container.

1. As a final step, [change your SA password](#sapassword) in a production environment, because the `MSSQL_SA_PASSWORD` is visible in `ps -eax` output and stored in the environment variable of the same name.

---

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"
## <a id="pullandrun2019"></a> Pull and run the SQL Server Linux container image

Before starting the following steps, make sure that you select your preferred shell (**bash**, **PowerShell**, or **cmd**) at the top of this article.

::: zone pivot="cs1-bash"
> [!NOTE]  
> For the bash commands in this article, `sudo` is used. If you don't want to use `sudo` to run Docker, you can configure a `docker` group and add users to that group. For more information, see [Post-installation steps for Linux](https://docs.docker.com/install/linux/linux-postinstall/).
::: zone-end

## [CLI](#tab/cli)

### Pull the container from the registry

Pull the [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] Linux container image from the Microsoft Container Registry.

::: zone pivot="cs1-bash"

```bash
sudo docker pull mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
docker pull mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker pull mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-2017&preserve-view=true#pullandrun2017) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver16&preserve-view=true#pullandrun2022) versions of this article.

The previous command pulls the latest [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] Linux container image. If you want to pull a specific image, you add a colon and the tag name, such as `mcr.microsoft.com/mssql/server:2019-GA-ubuntu`. To see all available images, see the [Microsoft Artifact Registry](https://mcr.microsoft.com/product/mssql/server/about).

### Run the container

To run the Linux container image with Docker, you can use the following command from a bash shell or elevated PowerShell command prompt.

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

::: zone pivot="cs1-bash"

```bash
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

> [!NOTE]  
> If you're using PowerShell Core, replace the double quotes with single quotes.

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" `
   -p 1433:1433 --name sql1 --hostname sql1 `
   -d `
   mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" `
   -p 1433:1433 --name sql1 --hostname sql1 `
   -d `
   mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

Your password should follow the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] default password policy, otherwise the container can't set up [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], and stops working. By default, the password must be at least eight characters long and contain characters from three of the following four sets: uppercase letters, lowercase letters, base-10 digits, and symbols. You can examine the error log by using the [`docker logs`](https://docs.docker.com/engine/reference/commandline/logs/) command.

By default, this quickstart creates a container with the Developer edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The process for running production editions in containers is slightly different. For more information, see [Run production container images](./sql-server-linux-docker-container-deployment.md#production).

The following table provides a description of the parameters in the previous `docker run` example:

| Parameter | Description |
| --- | --- |
| `-e "ACCEPT_EULA=Y"` | Set the `ACCEPT_EULA` variable to any value to confirm your acceptance of the End-User Licensing Agreement. Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `-e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>"` | Specify your own strong password that is at least eight characters and meets the [Password Policy](../relational-databases/security/password-policy.md). Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `-e "MSSQL_COLLATION=<SQL_Server_collation>"` | Specify a custom SQL Server collation, instead of the default `SQL_Latin1_General_CP1_CI_AS`. |
| `-p 1433:1433` | Map a TCP port on the host environment (first value) with a TCP port in the container (second value). In this example, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is listening on TCP 1433 in the container and this container port is then exposed to TCP port 1433 on the host. |
| `--name sql1` | Specify a custom name for the container rather than a randomly generated one. If you run more than one container, you can't reuse this same name. |
| `--hostname sql1` | Used to explicitly set the container hostname. If you don't specify the hostname, it defaults to the container ID, which is a randomly generated system GUID. |
| `-d` | Run the container in the background (daemon). |
| `mcr.microsoft.com/mssql/server:2019-latest` | The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image. |

## [sqlcmd](#tab/sqlcmd)

### Pull and run the container

Pull and run the [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] Linux container image from the Microsoft Container Registry.

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd create mssql --tag 2019-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd create mssql --tag 2019-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd create mssql --tag 2019-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-2017&preserve-view=true#pullandrun2017) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver16&preserve-view=true#pullandrun2022) versions of this article.

The previous command pulls the latest [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] Linux container image. If you want to pull a specific image, change the tag name, such as `2019-GA-ubuntu-16.04`. To see all available images, run the following command:

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd create mssql get-tags
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd create mssql get-tags
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd create mssql get-tags
```

::: zone-end

By default, this quickstart creates a container with the Developer edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The process for running production editions in containers is slightly different. For more information, see [Run production container images](./sql-server-linux-docker-container-deployment.md#production).

The following table provides a description of the parameters in the previous `docker run` example:

| Parameter | Description |
| --- | --- |
| `--ACCEPT_EULA` | Include the `ACCEPT_EULA` flag to confirm your acceptance of the End-User Licensing Agreement. Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `--port 1433` | Map a TCP port on the host environment and a TCP port in the container. In this example, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is listening on TCP 1433 in the container and this container port is then exposed to TCP port 1433 on the host. |
| `--name sql1` | Specify a custom name for the container rather than a randomly generated one. If you run more than one container, you can't reuse this same name. |
| `--hostname sql1` | Used to explicitly set the container hostname. If you don't specify the hostname, it defaults to the container ID, which is a randomly generated system GUID. |
| `--tag 2019-latest` | The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image. |

---

### View list of containers

1. To view your Docker containers, use the `docker ps` command.

   ::: zone pivot="cs1-bash"

   ```bash
   sudo docker ps -a
   ```

   ::: zone-end

   ::: zone pivot="cs1-powershell"

   ```PowerShell
   docker ps -a
   ```

   ::: zone-end

   ::: zone pivot="cs1-cmd"

   ```cmd
   docker ps -a
   ```

   ::: zone-end

   You should see output similar to the following example:

   ```output
   CONTAINER ID   IMAGE                                        COMMAND                    CREATED         STATUS         PORTS                                       NAMES
   d4a1999ef83e   mcr.microsoft.com/mssql/server:2019-latest   "/opt/mssql/bin/perm..."   2 minutes ago   Up 2 minutes   0.0.0.0:1433->1433/tcp, :::1433->1433/tcp   sql1
   ```

1. If the `STATUS` column shows a status of `Up`, then [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running in the container and listening on the port specified in the `PORTS` column. If the `STATUS` column for your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container shows `Exited`, see [Troubleshoot SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md). The server is ready for connections once the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error logs display the message: `SQL Server is now ready for client connections. This is an informational message; no user action is required`. You can review the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error log inside the container using the command:

   ```bash
   sudo docker exec -t sql1 cat /var/opt/mssql/log/errorlog | grep connection
   ```

   The `--hostname` parameter, as discussed previously, changes the internal name of the container to a custom value. This value is the name you see returned in the following Transact-SQL query:

   ```sql
   SELECT @@SERVERNAME,
       SERVERPROPERTY('ComputerNamePhysicalNetBIOS'),
       SERVERPROPERTY('MachineName'),
       SERVERPROPERTY('ServerName');
   ```

   Setting `--hostname` and `--name` to the same value is a good way to easily identify the target container.

1. As a final step, [change your SA password](#sapassword) in a production environment, because the `MSSQL_SA_PASSWORD` is visible in `ps -eax` output and stored in the environment variable of the same name.

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"
## <a id="pullandrun2022"></a> Pull and run the SQL Server Linux container image

Before starting the following steps, make sure that you select your preferred shell (**bash**, **PowerShell**, or **cmd**) at the top of this article.

::: zone pivot="cs1-bash"
> [!NOTE]  
> For the bash commands in this article, `sudo` is used. If you don't want to use `sudo` to run Docker, you can configure a `docker` group and add users to that group. For more information, see [Post-installation steps for Linux](https://docs.docker.com/install/linux/linux-postinstall/).
::: zone-end

## [CLI](#tab/cli)

### Pull the container from the registry

Pull the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Linux container image from the Microsoft Container Registry.

::: zone pivot="cs1-bash"

```bash
sudo docker pull mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-2017&preserve-view=true#pullandrun2017) or [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true#pullandrun2019) versions of this article.

The previous command pulls the latest [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Linux container image. If you want to pull a specific image, you add a colon and the tag name, such as `mcr.microsoft.com/mssql/server:2022-GA-ubuntu`. To see all available images, see the [Microsoft Artifact Registry](https://mcr.microsoft.com/product/mssql/server/about).

### Run the container

To run the Linux container image with Docker, you can use the following command from a bash shell or elevated PowerShell command prompt.

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

::: zone pivot="cs1-bash"

```bash
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

> [!NOTE]  
> If you're using PowerShell Core, replace the double quotes with single quotes.

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" `
   -p 1433:1433 --name sql1 --hostname sql1 `
   -d `
   mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" `
   -p 1433:1433 --name sql1 --hostname sql1 `
   -d `
   mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

Your password should follow the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] default password policy, otherwise the container can't set up [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], and stops working. By default, the password must be at least eight characters long and contain characters from three of the following four sets: uppercase letters, lowercase letters, base-10 digits, and symbols. You can examine the error log by using the [`docker logs`](https://docs.docker.com/engine/reference/commandline/logs/) command.

By default, this quickstart creates a container with the Developer edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The process for running production editions in containers is slightly different. For more information, see [Run production container images](./sql-server-linux-docker-container-deployment.md#production).

The following table provides a description of the parameters in the previous `docker run` example:

| Parameter | Description |
| --- | --- |
| `-e "ACCEPT_EULA=Y"` | Set the `ACCEPT_EULA` variable to any value to confirm your acceptance of the End-User Licensing Agreement. Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `-e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>"` | Specify your own strong password that is at least eight characters and meets the [Password Policy](../relational-databases/security/password-policy.md). Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `-e "MSSQL_COLLATION=<SQL_Server_collation>"` | Specify a custom SQL Server collation, instead of the default `SQL_Latin1_General_CP1_CI_AS`. |
| `-p 1433:1433` | Map a TCP port on the host environment (first value) with a TCP port in the container (second value). In this example, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is listening on TCP 1433 in the container and this container port is then exposed to TCP port 1433 on the host. |
| `--name sql1` | Specify a custom name for the container rather than a randomly generated one. If you run more than one container, you can't reuse this same name. |
| `--hostname sql1` | Used to explicitly set the container hostname. If you don't specify the hostname, it defaults to the container ID, which is a randomly generated system GUID. |
| `-d` | Run the container in the background (daemon). |
| `mcr.microsoft.com/mssql/server:2022-latest` | The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image. |

## <a id="sapassword"></a> Change the system administrator password

The **SA** account is a system administrator on the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance that gets created during setup. After you create your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container, the `MSSQL_SA_PASSWORD` environment variable you specified is discoverable by running `echo $MSSQL_SA_PASSWORD` in the container. For security purposes, you should change your SA password in a production environment.

1. Choose a strong password to use for the SA user.

1. Use `docker exec` to run **sqlcmd** to change the password using Transact-SQL. In the following example, the old and new passwords are read from user input.

   ::: zone pivot="cs1-bash"

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA \
    -P "$(read -sp "Enter current SA password: "; echo "${REPLY}")" \
    -Q "ALTER LOGIN SA WITH PASSWORD=\"$(read -sp "Enter new SA password: "; echo "${REPLY}")\""
   ```

   ::: zone-end

   ::: zone pivot="cs1-powershell"

   ```PowerShell
   docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd `
      -S localhost -U SA -P "<YourStrong@Passw0rd>" `
      -Q "ALTER LOGIN SA WITH PASSWORD='<YourNewStrong@Passw0rd>'"
   ```

   ::: zone-end

   ::: zone pivot="cs1-cmd"

   ```cmd
   docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd `
      -S localhost -U SA -P "<YourStrong@Passw0rd>" `
      -Q "ALTER LOGIN SA WITH PASSWORD='<YourNewStrong@Passw0rd>'"
   ```

   ::: zone-end

   > [!NOTE]  
   > Newer versions of **sqlcmd** are secure by default. For more information about connection encryption, see [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md) for Windows, and [Connecting with sqlcmd](../connect/odbc/linux-mac/connecting-with-sqlcmd.md) for Linux and macOS. If the connection doesn't succeed, you can add the `-No` option to **sqlcmd** to specify that encryption is optional, not mandatory.

## Disable the `sa` account as a best practice

> [!IMPORTANT]
> You'll need these credentials for later steps. Be sure to write down the user ID and password that you enter here.

[!INCLUDE [connect-with-sa](includes/connect-with-sa.md)]

## [sqlcmd](#tab/sqlcmd)

### Pull and run the container

Pull and run the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Linux container image from the Microsoft Container Registry.

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd create mssql --tag 2022-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd create mssql --tag 2022-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd create mssql --tag 2022-latest --hostname sql1 --name sql1 --port 1433 --accept-eula
```

::: zone-end

> [!TIP]  
> This quickstart creates [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] containers. If you prefer to create Linux containers for different versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-2017&preserve-view=true#pullandrun2017) or [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true#pullandrun2019) versions of this article.

The previous command pulls the latest [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Linux container image. If you want to pull a specific image, change the tag name, such as `2022-CU11-ubuntu-22.04`. To see all available images, run the following command:

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd create mssql get-tags
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd create mssql get-tags
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd create mssql get-tags
```

::: zone-end

By default, this quickstart creates a container with the Developer edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The process for running production editions in containers is slightly different. For more information, see [Run production container images](./sql-server-linux-docker-container-deployment.md#production).

The following table provides a description of the parameters in the previous `docker run` example:

| Parameter | Description |
| --- | --- |
| `--ACCEPT-EULA` | Include the `--ACCEPT-EULA` flag to confirm your acceptance of the End-User Licensing Agreement. Required setting for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] image. |
| `--port 1433` | Map a TCP port on the host environment and a TCP port in the container. In this example, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is listening on TCP 1433 in the container and this container port is then exposed to TCP port 1433 on the host. |
| `--name sql1` | Specify a custom name for the container rather than a randomly generated one. If you run more than one container, you can't reuse this same name. |
| `--hostname sql1` | Used to explicitly set the container hostname. If you don't specify the hostname, it defaults to the container ID, which is a randomly generated system GUID. |
| `--tag 2022-latest` | The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image. |

**sqlcmd** disables the SA password and creates a new login based on the current user when it creates a container. Use the following command to view your login information. You'll need it in later steps.

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd config view --raw
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd config view --raw
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd config view --raw
```

::: zone-end

---

### View list of containers

1. To view your Docker containers, use the `docker ps` command.

   ::: zone pivot="cs1-bash"

   ```bash
   sudo docker ps -a
   ```

   ::: zone-end

   ::: zone pivot="cs1-powershell"

   ```PowerShell
   docker ps -a
   ```

   ::: zone-end

   ::: zone pivot="cs1-cmd"

   ```cmd
   docker ps -a
   ```

   ::: zone-end

   You should see output similar to the following example:

   ```output
   CONTAINER ID   IMAGE                                        COMMAND                    CREATED         STATUS         PORTS                                       NAMES
   d4a1999ef83e   mcr.microsoft.com/mssql/server:2022-latest   "/opt/mssql/bin/perm..."   2 minutes ago   Up 2 minutes   0.0.0.0:1433->1433/tcp, :::1433->1433/tcp   sql1
   ```

1. If the `STATUS` column shows a status of `Up`, then [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running in the container and listening on the port specified in the `PORTS` column. If the `STATUS` column for your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container shows `Exited`, see [Troubleshoot SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md). The server is ready for connections once the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error logs display the message: `SQL Server is now ready for client connections. This is an informational message; no user action is required`. You can review the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error log inside the container using the command:

   ```bash
   sudo docker exec -t sql1 cat /var/opt/mssql/log/errorlog | grep connection
   ```

   The `--hostname` parameter, as discussed previously, changes the internal name of the container to a custom value. This value is the name you see returned in the following Transact-SQL query:

   ```sql
   SELECT @@SERVERNAME,
       SERVERPROPERTY('ComputerNamePhysicalNetBIOS'),
       SERVERPROPERTY('MachineName'),
       SERVERPROPERTY('ServerName');
   ```

   Setting `--hostname` and `--name` to the same value is a good way to easily identify the target container.

::: moniker-end

## Connect to SQL Server

The following steps use the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tool, [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md), inside the container to connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

1. Use the `docker exec -it` command to start an interactive bash shell inside your running container. In the following example, `sql1` is name specified by the `--name` parameter when you created the container.

   ::: zone pivot="cs1-bash"

   ```bash
   sudo docker exec -it sql1 "bash"
   ```

   ::: zone-end

   ::: zone pivot="cs1-powershell"

   ```PowerShell
   docker exec -it sql1 "bash"
   ```

   ::: zone-end

   ::: zone pivot="cs1-cmd"

   ```cmd
   docker exec -it sql1 "bash"
   ```

   ::: zone-end

1. Once inside the container, connect locally with **sqlcmd**, using its full path.

   ```bash
   sudo /opt/mssql-tools/bin/sqlcmd -S localhost -U <userid> -P "<YourNewStrong@Passw0rd>"
   ```

   > [!NOTE]  
   > Newer versions of **sqlcmd** are secure by default. For more information about connection encryption, see [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md) for Windows, and [Connecting with sqlcmd](../connect/odbc/linux-mac/connecting-with-sqlcmd.md) for Linux and macOS. If the connection doesn't succeed, you can add the `-No` option to **sqlcmd** to specify that encryption is optional, not mandatory.

   You can omit the password on the command-line to be prompted to enter it. For example:

   ```bash
   sudo /opt/mssql-tools/bin/sqlcmd -S localhost -U <userid>
   ```

1. If successful, you should get to a **sqlcmd** command prompt: `1>`.

## Create and query data

The following sections walk you through using **sqlcmd** and Transact-SQL to create a new database, add data, and run a query.

### Create a new database

The following steps create a new database named `TestDB`.

1. From the **sqlcmd** command prompt, paste the following Transact-SQL command to create a test database:

   ```sql
   CREATE DATABASE TestDB;
   ```

1. On the next line, write a query to return the name of all of the databases on your server:

   ```sql
   SELECT Name from sys.databases;
   ```

1. The previous two commands weren't run immediately. Type `GO` on a new line to run the previous commands:

   ```sql
   GO
   ```

### Insert data

Next create a new table, `Inventory`, and insert two new rows.

1. From the *sqlcmd* command prompt, switch context to the new `TestDB` database:

   ```sql
   USE TestDB;
   ```

1. Create new table named `Inventory`:

   ```sql
   CREATE TABLE Inventory (id INT, name NVARCHAR(50), quantity INT);
   ```

1. Insert data into the new table:

   ```sql
   INSERT INTO Inventory VALUES (1, 'banana', 150); INSERT INTO Inventory VALUES (2, 'orange', 154);
   ```

1. Type `GO` to run the previous commands:

   ```sql
   GO
   ```

### Select data

Now, run a query to return data from the `Inventory` table.

1. From the **sqlcmd** command prompt, enter a query that returns rows from the `Inventory` table where the quantity is greater than 152:

   ```sql
   SELECT * FROM Inventory WHERE quantity > 152;
   ```

1. Run the command:

   ```sql
   GO
   ```

### Exit the sqlcmd command prompt

1. To end your **sqlcmd** session, type `QUIT`:

   ```sql
   QUIT
   ```

1. To exit the interactive command-prompt in your container, type `exit`. Your container continues to run after you exit the interactive bash shell.

## <a id="connectexternal"></a> Connect from outside the container

## [CLI](#tab/cli)

You can also connect to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance on your Docker machine from any external Linux, Windows, or macOS tool that supports SQL connections. The external tool uses the IP address for the host machine.

The following steps use **sqlcmd** outside of your container to connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running in the container. These steps assume that you already have the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tools installed outside of your container. The same principles apply when using other tools, but the process of connecting is unique to each tool.

1. Find the IP address for your container's host machine, using `ifconfig` or `ip addr`.

1. For this example, install the **sqlcmd** tool on your client machine. For more information, see [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md) or [Install the SQL Server command-line tools sqlcmd and bcp on Linux](sql-server-linux-setup-tools.md).

1. Run **sqlcmd** specifying the IP address and the port mapped to port 1433 in your container. In this example, the port is the same as port 1433 on the host machine. If you specified a different mapped port on the host machine, you would use it here. You also need to open the appropriate inbound port on your firewall to allow the connection.

   > [!NOTE]  
   > Newer versions of **sqlcmd** are secure by default. If the connection doesn't succeed, and you're using version 18 or higher, you can add the `-No` option to **sqlcmd** to specify that encryption is optional, not mandatory.

   ::: zone pivot="cs1-bash"

   ```bash
   sudo sqlcmd -S <ip_address>,1433 -U <userid> -P "<YourNewStrong@Passw0rd>"
   ```

   ::: zone-end

   ::: zone pivot="cs1-powershell"

   ```PowerShell
   sqlcmd -S <ip_address>,1433 -U <userid> -P "<YourNewStrong@Passw0rd>"
   ```

   ::: zone-end

   ::: zone pivot="cs1-cmd"

   ```cmd
   sqlcmd -S <ip_address>,1433 -U <userid> -P "<YourNewStrong@Passw0rd>"
   ```

   ::: zone-end

1. Run Transact-SQL commands. When finished, type `QUIT`.

## [sqlcmd](#tab/sqlcmd)

You can also connect to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance on your Docker machine from any external Linux, Windows, or macOS tool that supports SQL connections. The external tool uses the IP address for the host machine.

The following steps use **sqlcmd** outside of your container to connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running in the container. The same principles apply when using other tools, but the process of connecting is unique to each tool.

1. Run **sqlcmd** in the same session you used to create your container. It keeps track of the connection information via contexts so you can easily connect at any time. `sqlcmd config view` can be used to view your available contexts.

   ::: zone pivot="cs1-bash"

   ```bash
   sudo sqlcmd
   ```

   ::: zone-end

   ::: zone pivot="cs1-powershell"

   ```PowerShell
   sqlcmd query
   ```

   ::: zone-end

   ::: zone pivot="cs1-cmd"

   ```cmd
   sqlcmd query
   ```

   ::: zone-end

1. Run Transact-SQL commands. When finished, type `QUIT`.

---

Other common tools to connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] include:

- [SQL Server extension for Visual Studio Code](../tools/visual-studio-code/sql-server-develop-use-vscode.md)
- [Use SQL Server Management Studio on Windows to manage SQL Server on Linux](sql-server-linux-manage-ssms.md)
- [What is Azure Data Studio?](../azure-data-studio/what-is-azure-data-studio.md)
- [mssql-cli (Preview)](https://github.com/dbcli/mssql-cli/blob/master/doc/usage_guide.md)
- [Manage SQL Server on Linux with PowerShell Core](sql-server-linux-manage-powershell-core.md)

## Remove your container

## [CLI](#tab/cli)

If you want to remove the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container used in this tutorial, run the following commands:

::: zone pivot="cs1-bash"

```bash
sudo docker stop sql1
sudo docker rm sql1
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
docker stop sql1
docker rm sql1
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker stop sql1
docker rm sql1
```

::: zone-end

## [sqlcmd](#tab/sqlcmd)

If you want to remove the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container used in this tutorial, run the following command:

::: zone pivot="cs1-bash"

```bash
sudo sqlcmd delete --force
```

::: zone-end

::: zone pivot="cs1-powershell"

```PowerShell
sqlcmd delete --force
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd delete --force
```

::: zone-end

---

> [!WARNING]  
> Stopping and removing a container permanently deletes any [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data in the container. If you need to preserve your data, [create and copy a backup file out of the container](tutorial-restore-backup-in-sql-server-container.md) or use a [container data persistence technique](sql-server-linux-docker-container-configure.md#persist).

## Docker demo

After you finish using the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image for Docker, you might want to know how Docker is used to improve development and testing. The following video shows how Docker can be used in a continuous integration and deployment scenario.

> [!VIDEO https://channel9.msdn.com/Events/Connect/2017/T152/player]

## Related tasks

- [Run multiple SQL Server containers](sql-server-linux-docker-container-deployment.md#multiple)
- [Persist your data](sql-server-linux-docker-container-configure.md#persist)

## Related content

- [Restore a SQL Server database in a Linux container](tutorial-restore-backup-in-sql-server-container.md)
- [Troubleshoot SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md)
- [mssql-docker GitHub repository](https://github.com/microsoft/mssql-docker)

[!INCLUDE [contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
