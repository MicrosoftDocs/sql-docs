---
title: Configure and customize SQL Server Docker containers
description: Understand the different ways to customize SQL Server Docker Containers and how you can configure it based on your requirements.
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 05/30/2022
ms.service: sql
ms.subservice: linux
ms.topic: troubleshooting
ms.custom: contperf-fy21q1
zone_pivot_groups: cs1-command-shell
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2017"
---
# Configure and customize SQL Server Docker containers

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how you can configure and customize SQL Server Docker containers, such as persisting your data, moving files from and to containers, and changing default settings.

## <a id="customcontainer"></a> Create a customized container

It is possible to create your own [Dockerfile](https://docs.docker.com/engine/reference/builder/#usage) to create a customized SQL Server container. For more information, see [a demo that combines SQL Server and a Node application](https://github.com/twright-msft/mssql-node-docker-demo-app). If you do create your own Dockerfile, be aware of the foreground process, because this process controls the life of the container. If it exits, the container will shut down. For example, if you want to run a script and start SQL Server, make sure that the SQL Server process is the right-most command. All other commands are run in the background. The following command illustrates this inside a Dockerfile:

```bash
/usr/src/app/do-my-sql-commands.sh & /opt/mssql/bin/sqlservr
```

If you reversed the commands in the previous example, the container would shut down when the do-my-sql-commands.sh script completes.

## <a id="persist"></a> Persist your data

Your SQL Server configuration changes and database files are persisted in the container even if you restart the container with `docker stop` and `docker start`. However, if you remove the container with `docker rm`, everything in the container is deleted, including SQL Server and your databases. The following section explains how to use **data volumes** to persist your database files even if the associated containers are deleted.

> [!IMPORTANT]
> For SQL Server, it is critical that you understand data persistence in Docker. In addition to the discussion in this section, see Docker's documentation on [how to manage data in Docker containers](https://docs.docker.com/engine/tutorials/dockervolumes/).

### Mount a host directory as data volume

The first option is to mount a directory on your host as a data volume in your container. To do that, use the `docker run` command with the `-v <host directory>:/var/opt/mssql` flag. This allows the data to be restored between container executions.

> [!NOTE]
> SQL Server 2019 containers automatically start up as non-root, while SQL Server 2017 containers start as root by default. For more information on running SQL Server containers as non-root, see [Configure security](sql-server-linux-docker-container-security.md).

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Please use `MSSQL_SA_PASSWORD` instead.

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

::: zone pivot="cs1-bash"
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

::: zone pivot="cs1-bash"
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: moniker-end

This technique also enables you to share and view the files on the host outside of Docker.

### Use data volume containers

The second option is to use a data volume container. You can create a data volume container by specifying a volume name instead of a host directory with the `-v` parameter. The following example creates a shared data volume named **sqlvolume**.

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

::: zone pivot="cs1-bash"
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

::: zone pivot="cs1-bash"
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: moniker-end

> [!NOTE]
> This technique for implicitly creating a data volume in the run command does not work with older versions of Docker. In that case, use the explicit steps outlined in the Docker documentation, [Creating and mounting a data volume container](https://docs.docker.com/engine/tutorials/dockervolumes/#creating-and-mounting-a-data-volume-container).

Even if you stop and remove this container, the data volume persists. You can view it with the `docker volume ls` command.

```command
docker volume ls
```

If you then create another container with the same volume name, the new container uses the same SQL Server data contained in the volume.

To remove a data volume container, use the `docker volume rm` command.

> [!WARNING]
> If you delete the data volume container, any SQL Server data in the container is *permanently* deleted.

### Backup and restore

In addition to these container techniques, you can also use standard SQL Server backup and restore techniques. You can use backup files to protect your data or to move the data to another SQL Server instance. For more information, see [Backup and restore SQL Server databases on Linux](sql-server-linux-backup-and-restore-database.md).

> [!WARNING]
> If you do create backups, make sure to create or copy the backup files outside of the container. Otherwise, if the container is removed, the backup files are also deleted.

## Enable VDI backup and restore in containers

Virtual Device Interface (VDI) backup and restore operations are now supported in SQL Server container deployments beginning with CU15 for SQL Server 2019 and CU28 for SQL Server 2017. Follow the steps below to enable VDI-based backup or restores for SQL Server containers:

1.	When deploying SQL Server containers, use the `--shm-size` option. To begin, set the sizing to 1 GB, as shown in the sample command below:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Mystr0ngP@ssw0rd!" --shm-size 1g  -p 1433:1433 --name sql19 --hostname sql19 -d mcr.microsoft.com/mssql/server:2019-latest
   ```

   The option `--shm-size` allows you to configure the size of the shared memory directory (`/dev/shm`) inside the container, which is set to 64 MB by default. This default size of the shared memory is insufficient to support VDI backups. We recommend that you configure this to a minimum of 1 GB when you deploy SQL Server containers and want to support VDI backups.

2.	You must also enable the new parameter **memory.enablecontainersharedmemory** in **mssql.conf** inside the container. You can mount mssql.conf at the deployment of the container using the `-v` option as described in the [Persist your data](#persist) section, or after you have deployed the container by manually updating mssql.conf inside the container. Here's a sample mssql.conf file with the **memory.enablecontainersharedmemory** setting set to **true**.

   ```output
   [memory]
   enablecontainersharedmemory = true
   ```

## Copy files from a container

To copy a file out of the container, use the following command:

```command
docker cp <Container ID>:<Container path> <host path>
```

You can get the Container ID by running the command `docker ps -a`.

**Example:**

::: zone pivot="cs1-bash"
```bash
docker cp d6b75213ef80:/var/opt/mssql/log/errorlog /tmp/errorlog
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker cp d6b75213ef80:/var/opt/mssql/log/errorlog C:\Temp\errorlog
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker cp d6b75213ef80:/var/opt/mssql/log/errorlog C:\Temp\errorlog
```
::: zone-end

## Copy files into a container

To copy a file into the container, use the following command:

```command
docker cp <Host path> <Container ID>:<Container path>
```

**Example:**

::: zone pivot="cs1-bash"
```bash
docker cp /tmp/mydb.mdf d6b75213ef80:/var/opt/mssql/data
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker cp C:\Temp\mydb.mdf d6b75213ef80:/var/opt/mssql/data
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker cp C:\Temp\mydb.mdf d6b75213ef80:/var/opt/mssql/data
```
::: zone-end

## <a id="tz"></a> Configure the time zone

To run SQL Server in a Linux container with a specific time zone, configure the `TZ` environment variable (see [Configure the time zone on Linux](sql-server-linux-configure-time-zone.md) for more information). To find the right time zone value, run the `tzselect` command from a Linux bash prompt:

```command
tzselect
```

After selecting the time zone, `tzselect` displays output similar to the following:

```output
The following information has been given:

        United States
        Pacific

Therefore TZ='America/Los_Angeles' will be used.
```

You can use this information to set the same environment variable in your Linux container. The following example shows how to run SQL Server in a container in the `Americas/Los_Angeles` time zone:

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

::: zone pivot="cs1-bash"
```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'A_PASSWORD=<YourStrong!Passw0rd>' \
-p 1433:1433 --name sql1 \
-e 'TZ=America/Los_Angeles'\
-d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
sudo docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
-p 1433:1433 --name sql1 `
-e "TZ=America/Los_Angeles" `
-d mcr.microsoft.com/mssql/server:2017-latest 
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
sudo docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
-p 1433:1433 --name sql1 ^
-e "TZ=America/Los_Angeles" ^
-d mcr.microsoft.com/mssql/server:2017-latest 
```
::: zone-end

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

::: zone pivot="cs1-bash"
```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' \
   -p 1433:1433 --name sql1 \
   -e 'TZ=America/Los_Angeles'\
   -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
sudo docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
   -p 1433:1433 --name sql1 `
   -e "TZ=America/Los_Angeles" `
   -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
sudo docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
   -p 1433:1433 --name sql1 `
   -e "TZ=America/Los_Angeles" `
   -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: moniker-end

## Change the tempdb path

It's a good practice to keep your `tempdb` database separate from your user databases.

1. Connect to the SQL Server instance, and then run the following Transact-SQL (T-SQL) script. If there are more files associated with `tempdb`, you'll need to move them as well.

   ```sql
   ALTER DATABASE tempdb MODIFY FILE (
      NAME = tempdev, FILENAME = '/var/opt/mssql/tempdb/tempdb.mdf'
   );
   GO

   ALTER DATABASE tempdb MODIFY FILE (
       NAME = templog, FILENAME = '/var/opt/mssql/tempdb/templog.ldf'
   );
   GO
   ```

1. Verify that the `tempdb` file location has been modified, using the following T-SQL script:

   ```sql
   SELECT *
   FROM sys.sysaltfiles
   WHERE dbid = 2;
   ```

1. You must restart the SQL Server container for these changes to take effect.

   ::: zone pivot="cs1-bash"
   ```bash
   docker stop sql1
   docker start sql1
   ```
   ::: zone-end

   ::: zone pivot="cs1-powershell"
   ```PowerShell
   docker stop sql1
   docker start sql1
   ```
   ::: zone-end

   ::: zone pivot="cs1-cmd"
   ```cmd
   docker stop sql1
   docker start sql1
   ```
   ::: zone-end

1. Open an interactive `bash` session to connect to the container.

   ::: zone pivot="cs1-bash"
   ```bash
     docker exec -it sql1 bash
   ```
   ::: zone-end

   ::: zone pivot="cs1-powershell"
   ```PowerShell
     docker exec -it sql1 bash
   ```
   ::: zone-end

   ::: zone pivot="cs1-cmd"
   ```cmd
     docker exec -it sql1 bash
   ```
   ::: zone-end

    Once connected to the interactive shell, run the following command to check the location of `tempdb`:

    ```bash
    ls /var/opt/mssql/tempdb/
    ```

    If the move was successful, you'll see similar output:

    ```output
    tempdb.mdf templog.ldf
    ```

## <a id="changefilelocation"></a> Change the default file location

Add the `MSSQL_DATA_DIR` variable to change your data directory in your `docker run` command, then mount a volume to that location that your container's user has access to.

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

::: zone pivot="cs1-bash"
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=MyStrongPassword' -e 'MSSQL_DATA_DIR=/my/file/path' -v /my/host/path:/my/file/path -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=MyStrongPassword" -e "MSSQL_DATA_DIR=/my/file/path" -v /my/host/path:/my/file/path -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyStrongPassword" -e "MSSQL_DATA_DIR=/my/file/path" -v /my/host/path:/my/file/path -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```
::: zone-end

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

::: zone pivot="cs1-bash"
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=MyStrongPassword' -e 'MSSQL_DATA_DIR=/my/file/path' -v /my/host/path:/my/file/path -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-powershell"
```PowerShell
docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=MyStrongPassword" -e "MSSQL_DATA_DIR=/my/file/path" -v /my/host/path:/my/file/path -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: zone pivot="cs1-cmd"
```cmd
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyStrongPassword" -e "MSSQL_DATA_DIR=/my/file/path" -v /my/host/path:/my/file/path -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
::: zone-end

::: moniker-end

## Examples of custom Docker containers

For examples of custom Docker containers, see <https://github.com/microsoft/mssql-docker/tree/master/linux/preview/examples>. The examples include:

- [Dockerfile example with Full-Text Search](https://github.com/microsoft/mssql-docker/blob/master/linux/preview/examples/mssql-agent-fts-ha-tools/Dockerfile)
- [Dockerfile example for RHEL 7 and SQL Server 2019](https://github.com/microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-rhel7-sql2019)
- [Dockerfile example for RHEL 8 and SQL Server 2017](https://github.com/microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-rhel8-sql2017)
- [Dockerfile example for Ubuntu 20.04 and SQL Server 2019 with Full-Text Search, PolyBase, and Tools](https://github.com/microsoft/mssql-docker/blob/master/linux/preview/examples/mssql-polybase-fts-tools/Dockerfile)

For information on how to build and run Docker containers using Dockerfiles, see <https://github.com/microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-mlservices>.

## Next steps

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

- Get started with SQL Server 2017 container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md?view=sql-server-2017&preserve-view=true)

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

- Get started with SQL Server 2019 container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md)

::: moniker-end

- [Deploy and connect to SQL Server Docker containers](sql-server-linux-docker-container-deployment.md)

- [Troubleshooting SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md)

- [Secure SQL Server Docker containers](sql-server-linux-docker-container-security.md)
