---
title: Troubleshooting SQL Server Docker Containers
description: Explore the different troubleshooting techniques that you can use to resolve common errors that are seen when using Linux Docker containers with SQL Server images
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 01/27/2026
ms.service: sql
ms.subservice: linux
ms.topic: troubleshooting
ms.custom:
  - linux-related-content
  - sfi-ropc-blocked
  - ignite-2025
zone_pivot_groups: cs1-command-shell
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017"
---
# Troubleshoot SQL Server Docker containers

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This article talks about common errors seen when deploying and using SQL Server Docker containers, and provide troubleshooting techniques to help resolve the issue.

## Docker command errors

If you get errors for any `docker` commands, make sure that the docker service is running, and try to run with elevated permissions.

For example, on Linux, you might get the following error when running `docker` commands:

```output
Cannot connect to the Docker daemon. Is the docker daemon running on this host?
```

If you get this error on Linux, try running the same commands prefaced with `sudo`. If that fails, verify the docker service is running, and start it if necessary.

```bash
sudo systemctl status docker
sudo systemctl start docker
```

On Windows, verify that you're launching PowerShell or your command-prompt as an Administrator.

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

## SQL Server container startup errors

If the SQL Server container fails to run, try the following tests:

- If you get an error such as `failed to create endpoint CONTAINER_NAME on network bridge. Error starting proxy: listen tcp 0.0.0.0:1433 bind: address already in use.`, you're attempting to map the container port 1433 to a port that is already in use. This can happen if you're running SQL Server locally on the host machine. It can also happen if you start two SQL Server containers and try to map them both to the same host port. If this happens, use the `-p` parameter to map the container port 1433 to a different host port. For example:

  <!--SQL Server 2017 on Linux -->
  ::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

  ::: zone pivot="cs1-bash"

  ```bash
  docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -p 1400:1433 -d mcr.microsoft.com/mssql/server:2017-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-powershell"

  ```powershell
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2017-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-cmd"

  ```cmd
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2017-latest`.
  ```

  ::: zone-end

  ::: moniker-end

  <!--SQL Server 2019 on Linux-->
  ::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

  ::: zone pivot="cs1-bash"

  ```bash
  docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -p 1400:1433 -d mcr.microsoft.com/mssql/server:2019-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-powershell"

  ```powershell
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2019-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-cmd"

  ```cmd
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2019-latest`.
  ```

  ::: zone-end

  ::: moniker-end

  <!--SQL Server 2022 on Linux-->
  ::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

  ::: zone pivot="cs1-bash"

  ```bash
  docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -p 1400:1433 -d mcr.microsoft.com/mssql/server:2022-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-powershell"

  ```powershell
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2022-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-cmd"

  ```cmd
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2022-latest`.
  ```

  ::: zone-end

  ::: moniker-end

  <!--SQL Server 2025 on Linux-->
  ::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

  ::: zone pivot="cs1-bash"

  ```bash
  docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -p 1400:1433 -d mcr.microsoft.com/mssql/server:2025-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-powershell"

  ```powershell
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2025-latest`.
  ```

  ::: zone-end

  ::: zone pivot="cs1-cmd"

  ```cmd
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2025-latest`.
  ```

  ::: zone-end

  ::: moniker-end

  > [!CAUTION]  
  > [!INCLUDE [password-complexity](../includes/password-complexity.md)]

- If you get an error such as `Got permission denied while trying to connect to the Docker daemon socket at unix:///var/run/docker.sock: Get http://%2Fvar%2Frun%2Fdocker.sock/v1.30tdout=1&tail=all: dial unix /var/run/docker.sock: connect: permission denied` when trying to start a container, then add your user to the docker group in Ubuntu. Then logout and login again as this change will affect new sessions.

  ```bash
   usermod -aG docker $USER
  ```

- Check to see if there are any error messages from container.

  ```bash
  docker logs e69e056c702d
  ```

- Make sure that you meet the minimum memory and disk requirements specified in the [prerequisites](../install-upgrade/quickstart-install-docker.md#requirements) section of the quickstart article.

- If you're using any container management software, make sure it supports container processes running as root. The sqlservr process in the container runs as root.

- If your SQL Server Docker container exits immediately after starting, check your docker logs. If you're using PowerShell on Windows with the `docker run` command, use double quotes instead of single quotes. With PowerShell Core, use single quotes.

- Review the [SQL Server setup and error logs](#errorlogs).

## Enable dump captures

If the SQL Server process is failing inside the container, you should create a new container with **SYS_PTRACE** enabled. This adds the Linux capability to trace a process, which is necessary for creating a dump file on an exception. The dump file can be used by support to help troubleshoot the problem. The following docker run command enables this capability.

<!--SQL Server 2017 on Linux -->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

::: zone pivot="cs1-bash"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -e 'MSSQL_PID=Developer' --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

::: zone-end

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

::: zone pivot="cs1-bash"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -e 'MSSQL_PID=Developer' --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

::: zone-end

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

::: zone pivot="cs1-bash"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -e 'MSSQL_PID=Developer' --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

::: zone-end

::: moniker-end

<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

::: zone pivot="cs1-bash"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -e 'MSSQL_PID=Developer' --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2025-latest
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2025-latest
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2025-latest
```

::: zone-end

::: moniker-end

> [!CAUTION]  
> [!INCLUDE [password-complexity](../includes/password-complexity.md)]

## SQL Server connection failures

If you can't connect to the SQL Server instance running in your container, try the following tests:

- Make sure that your SQL Server container is running by looking at the **STATUS** column of the `docker ps -a` output. If not, use `docker start <Container ID>` to start it.

- If you mapped to a non-default host port (not 1433), make sure you're specifying the port in your connection string. You can see your port mapping in the **PORTS** column of the `docker ps -a` output. For example, the following command connects sqlcmd to a container listening on port 1401:

  ::: zone pivot="cs1-bash"

  ```bash
  sqlcmd -S 10.3.2.4,1401 -U sa -P '<password>'
  ```

  ::: zone-end

  ::: zone pivot="cs1-powershell"

  ```powershell
  sqlcmd -S 10.3.2.4,1401 -U sa -P "<password>"
  ```

  ::: zone-end

  ::: zone pivot="cs1-cmd"

  ```cmd
  sqlcmd -S 10.3.2.4,1401 -U sa -P "<password>"
  ```

  ::: zone-end

- If you used `docker run` with an existing mapped data volume or data volume container, SQL Server ignores the value of `MSSQL_SA_PASSWORD`. Instead, the pre-configured `sa` account password is used from the SQL Server data in the data volume or data volume container. Verify that you're using the `sa` password associated with the data you're attaching to.

- Review the [SQL Server setup and error logs](#errorlogs).

## SQL Server Availability Groups

If you're using Docker with SQL Server Availability Groups, there are two additional requirements.

- Map the port that is used for replica communication (default 5022). For example, specify `-p 5022:5022` as part of your `docker run` command.

- Explicitly set the container host name with the `-h YOURHOSTNAME` parameter of the `docker run` command. This host name is used when you configure your Availability Group. If you don't specify it with `-h`, it defaults to the **container ID**.

<a id="errorlogs"></a>

## SQL Server setup and error logs

You can look at the SQL Server setup and error logs in **/var/opt/mssql/log**. If the container isn't running, first start the container. Then use an interactive command-prompt to inspect the logs. You can get the container ID by running the command `docker ps`.

```bash
docker start <ContainerID>
docker exec -it <ContainerID> "bash"
```

From the bash session inside your container, run the following commands:

```bash
cd /var/opt/mssql/log
cat setup*.log
cat errorlog
```

> [!TIP]  
> If you mounted a host directory to **/var/opt/mssql** when you created your container, you can instead look in the **log** subdirectory on the mapped path on the host.

## Execute commands in a container

If you have a running container, you can execute commands within the container from a host terminal.

To get the container ID run:

```bash
docker ps -a
```

To start a bash terminal in the container run:

```bash
docker exec -it <Container ID> /bin/bash
```

Now you can run commands as though you're running them at the terminal inside the container. When finished, type `exit`. This exits in the interactive command session, but your container continues to run.

## Related content

<!--SQL Server 2017 on Linux -->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

- [Quickstart: Run SQL Server Linux container images with Docker](../install-upgrade/quickstart-install-docker.md?view=sql-server-2017&preserve-view=true)

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

- [Quickstart: Run SQL Server Linux container images with Docker](../install-upgrade/quickstart-install-docker.md?view=sql-server-ver15&preserve-view=true)

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

- [Quickstart: Run SQL Server Linux container images with Docker](../install-upgrade/quickstart-install-docker.md?view=sql-server-ver16&preserve-view=true)

::: moniker-end

<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

- [Quickstart: Run SQL Server Linux container images with Docker](../install-upgrade/quickstart-install-docker.md?view=sql-server-ver17&preserve-view=true)

::: moniker-end

- [Deploy and connect to SQL Server Linux containers](deploy.md)

- [Configure and customize SQL Server Linux containers](configure.md)

- [Secure SQL Server Linux containers](security.md)

[!INCLUDE [contribute-to-content](../../includes/paragraph-content/contribute-to-content.md)]
