---
# required metadata

title: Run SQL Server 2017 on Docker | Microsoft Docs
description: Download and run the Docker image for SQL Server 2017.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 05/09/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 82737f18-f5d6-4dce-a255-688889fdde69

# optional metadata

# keywords: "" 
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
ms.custom: H1Hack27Feb2017

---
# Run the SQL Server 2017 container image on Docker on Linux, Mac, or Windows

This topic explains how to pull and run the [mssql-server-linux container image](https://hub.docker.com/r/microsoft/mssql-server-linux/) using Docker. This image consists of SQL Server running on Linux based on Ubuntu 16.04. It can be used with the Docker Engine 1.8+ on Linux or on Docker for Mac/Windows.

> [!NOTE]
> This topic specifically focuses on using the mssql-server-linux image. The Windows image is not covered, but you can learn more about it on the [mssql-server-windows Docker Hub page](https://hub.docker.com/r/microsoft/mssql-server-windows/).

## <a id="requirements"></a> Requirements for Docker

- Docker Engine 1.8+ on any supported Linux distribution or Docker for Mac/Windows.
- Minimum of 4 GB of disk space
- Minimum of 4 GB of RAM
- [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

> [!IMPORTANT]
> The default on Docker for Mac and Docker for Windows is 2 GB for the Moby VM, so you must change it to 4 GB.
>
> **Docker for Mac procedure**
> 
> 1. Click the Docker logo on the top status bar.
> 2. Select **Preferences**.
> 3. Move the memory indicator to 4 GB or more.
> 4. Click the **restart** button at the button of the screen.
>
> **Docker for Windows procedure**
>
> 1. Right-click on the Docker icon from the task bar.
> 2. Click **Settings** under that menu.
> 3. Click the **Advanced** Tab.
> 4. Move the memory indicator to 4 GB or more.
> 5. Click the **Apply** button.

## Pull and run the container image

1. Pull the container image from Docker Hub.

    ```bash
    docker pull microsoft/mssql-server-linux
    ```

    > [!TIP]
    > For Linux, depending on your system and user configuration, you might need to preface each `docker` command with `sudo`.

2. To run the container image with Docker, you can use the following command:

    ```bash
    docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -d microsoft/mssql-server-linux
    ```

    | Parameter | Description |
    |-----|-----|
    | **-e 'ACCEPT_EULA=Y'** |  Set the **ACCEPT_EULA** variable to any value to confirm your acceptance of the [End-User Licensing Agreement](http://go.microsoft.com/fwlink/?LinkId=746388). Required setting for the SQL Server image. |
    | **-e 'SA_PASSWORD=\<YourStrong!Passw0rd\>'** | Specify your own strong password that is at least 8 characters and meets [SQL Server's password requirements](../relational-databases/security/password-policy.md). Required setting for the SQL Server image. |
    | **-p 1433:1433** | Map a TCP port on the host environment (first value) with a TCP port in the container (second value). In this example, SQL Server is listening on TCP 1433 in the container and this is exposed to the same port, 1433, on the host. |
    | **microsoft/mssql-server-linux** | The SQL Server Linux container image. Unless otherwise specified, this defaults to the **latest** image. |

3. To view your Docker containers, use the `docker ps` command.

    ```bash
    docker ps -a
    ```

    You should see output similar to the following screenshot:

    ![Docker ps command output](./media/sql-server-linux-setup-docker/docker-ps-command.png)

4. If the **STATUS** column shows a status of **Up**, then SQL Server is running in the container and listening on the port specified in the **PORTS** column. If the **STATUS** column for your SQL Server container shows **Exited**, see the [Troubleshooting](#troubleshooting) section.

> [!WARNING]
> After creating your SQL Server container, the `SA_PASSWORD` environment variable you specified is discoverable by running `echo $SA_PASSWORD` in the container. For security purposes, consider changing your SA password. The following example runs **sqlcmd** in the container to change the password to a new value:
>
> ```bash
> docker exec -it <Container ID> /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '<Old Password>' -Q 'ALTER LOGIN SA WITH PASSWORD="<New Password>";'
> ```

## Connect and query

You can connect and query SQL Server in a container from either outside the container or from within the container. The following sections explain both scenarios. 

### Tools outside the container

You can connect to the SQL Server instance on your Docker machine from any external Linux, Windows, or macOS tool that supports SQL connections. Some common tools include:

- [sqlcmd](sql-server-linux-connect-and-query-sqlcmd.md)
- [Visual Studio Code](sql-server-linux-develop-use-vscode.md)
- [SQL Server Management Studio (SSMS) on Windows](sql-server-linux-develop-use-ssms.md)

The following example uses **sqlcmd** to connect to SQL Server running in a Docker container. The IP address in the connection string is the IP address of the host machine that is running the container.

```bash
sqlcmd -S 10.3.2.4 -U SA -P '<YourPassword>'
```

If you mapped a host port that was not the default **1433**, add that port to the connection string. For example, if you specified `-p 1400:1433` in your `docker run` command, then connect by explicitly specify port 1400.

```bash
sqlcmd -S 10.3.2.4,1400 -U SA -P '<YourPassword>'
```

### Tools inside the container

Starting with SQL Server 2017 CTP 2.0, the [SQL Server command-line tools](sql-server-linux-setup-tools.md) are included in the container image. If you attach to the image with an interactive command-prompt, you can run the tools locally.

1. Use the `docker exec -it` command to start an interactive bash shell inside your running container. In the following example `e69e056c702d` is the container ID.

    ```bash
    docker exec -it e69e056c702d "bash"
    ```

    > [!TIP]
    > You don't always have to specify the entire container id. You only have to specify enough characters to uniquely identify it. So in this example, it might be enough to use `e6` or `e69` rather than the full id.

2. Once inside the container, connect locally with sqlcmd. Note that sqlcmd is not in the path by default, so you have to specify the full path.

    ```bash
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '<YourPassword>'
    ```

3. When finished with sqlcmd, type `exit`.

4. When finished with the interactive command-prompt, type `exit`. Your container continues to run after you exit the interactive bash shell.

## Run multiple SQL Server containers

Docker provides a way to run multiple SQL Server containers on the same host machine. This is the approach for scenarios that require multiple instances of SQL Server on the same host. Each container must expose itself on a different port.

The following example creates two SQL Server containers and maps them to ports **1401** and **1402** on the host machine.

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1401:1433 -d microsoft/mssql-server-linux
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1402:1433 -d microsoft/mssql-server-linux
```

Now there are two instances of SQL Server running in separate containers. Clients can connect to each SQL Server instance by using the IP address of the Docker host and the port number for the container.

```bash
sqlcmd -S 10.3.2.4,1401 -U SA -P '<YourPassword>'
sqlcmd -S 10.3.2.4,1402 -U SA -P '<YourPassword>'
```

## <a id="persist"></a> Persist your data

Your SQL Server configuration changes and database files are persisted in the container even if you restart the container with `docker stop` and `docker start`. However, if you remove the container with `docker rm`, everything in the container is deleted, including SQL Server and your databases. The following section explains how to use **data volumes** to persist your database files even if the associated containers are deleted.

> [!IMPORTANT]
> For SQL Server, it is critical that you understand data persistence in Docker. In addition to the discussion in this section, see Docker's documentation on [how to manage data in Docker containers](https://docs.docker.com/engine/tutorials/dockervolumes/).

### Mount a host directory as data volume

The first option is to mount a directory on your host as a data volume in your container. To do that, use the `docker run` command with the `-v <host directory>:/var/opt/mssql` flag. This allows the data to be restored between container executions.

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d microsoft/mssql-server-linux
```

This technique also enables you to share and view the files on the host outside of Docker.

> [!IMPORTANT]
> Host volume mapping for Docker on Mac with the SQL Server on Linux image is not supported at this time. Use data volume containers instead. This restriction is specific to the `/var/opt/msql` directory. Reading from a mounted directory works fine. For example, you can mount a host directory using –v on Mac and restore a backup from a .bak file that resides on the host.

### Use data volume containers

The second option is to use a data volume container. You can create a data volume container by specifying a volume name instead of a host directory with the `-v` parameter. The following example creates a shared data volume named **sqlvolume**.

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d microsoft/mssql-server-linux
```

> [!NOTE]
> This technique for implicitly creating a data volume in the run command does not work with older versions of Docker. In that case, use the explicit steps outlined in the Docker documentation, [Creating and mounting a data volume container](https://docs.docker.com/engine/tutorials/dockervolumes/#creating-and-mounting-a-data-volume-container).

Even if you stop and remove this container, the data volume persists. You can view it with the `docker volume ls` command.

```bash
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

## Execute commands in a container

If you have a running container, you can execute commands within the container from a host terminal.

To get the container ID run:

```bash
docker ps
```

To start a bash terminal in the container run:

```bash
docker exec -ti <Container ID> /bin/bash
```

Now you can run commands as though you are running them at the terminal inside the container. When finished, type `exit`. This exits in the interactive command session, but your container continues to run.

## Copy files from a container

To copy a file out of the container, use the following command:

```bash
docker cp <Container ID>:<Container path> <host path>
```

For example:

```bash
docker cp d6b75213ef80:/var/opt/mssql/log/errorlog /tmp/errorlog
```

To copy a file into the container, use the following command:

```bash
docker cp <Host path> <Container ID>:<Container path>
```

For example:

```bash
docker cp /tmp/mydb.mdf d6b75213ef80:/var/opt/mssql/data
```

## Upgrade SQL Server in containers

To upgrade the container image with Docker, pull the latest version from the registry. Use the `docker pull` command:

```bash
docker pull microsoft/mssql-server-linux:latest
```

This updates the SQL Server image for any new containers you create, but it does not update SQL Server in any running containers. To do this, you must create a new container with the latest SQL Server container image and migrate your data to that new container.

1. First, make sure you are using one of the [data persistence techniques](#persist) for your existing SQL Server container.

2. Stop the SQL Server container with the `docker stop` command.

3. Create a new SQL Server container with `docker run` and specify either a mapped host directory or a data volume container. The new container now uses a new version of SQL Server with your existing SQL Server data.

4. Verify your databases and data in the new container.

5. Optionally, remove the old container with `docker rm`.

## <a id="troubleshooting"></a> Troubleshooting

The following sections provide troubleshooting suggestions for running SQL Server in containers.

### Docker command errors

If you get errors for any `docker` commands, make sure that the docker service is running, and try to run with elevated permissions.

For example, on Linux, you might get the following error when running `docker` commands:

```
Cannot connect to the Docker daemon. Is the docker daemon running on this host?
```

If you get this error on Linux, try running the same commands prefaced with `sudo`. If that fails, verify the docker service is running, and start it if necessary.

```bash
sudo systemctl status docker
sudo systemctl start docker
```

### SQL Server container startup errors

If the SQL Server container fails to run, try the following tests:

- If you get an error such as **'failed to create endpoint CONTAINER_NAME on network bridge. Error starting proxy: listen tcp 0.0.0.0:1433 bind: address already in use.'**, then you are attempting to map the container port 1433 to a port that is already in use. This can happen if you're running SQL Server locally on the host machine. It can also happen if you start two SQL Server containers and try to map them both to the same host port. If this happens, use the `-p` parameter to map the container port 1433 to a different host port. For example: 

    ```bash
    docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1400:1433 -d microsoft/mssql-server-linux`.
    ```

- Check to see if there are any error messages from container.

    ```bash
    docker logs e69e056c702d
    ```

- Make sure that you meet the minimum memory and disk requirements specified in the [Requirements](#requirements) section of this topic.

- If you are using any container management software, make sure it supports container processes running as root. The sqlservr process in the container runs as root.

- Review the [SQL Server setup and error logs](#errorlogs).

### SQL Server connection failures

If you can't connect to the SQL Server instance running in your container, try the following tests:

- Make sure that your SQL Server container is running by looking at the **STATUS** column of the `docker ps -a` output. If not, use `docker start <Container ID>` to start it.

- If you mapped to a non-default host port (not 1433), make sure you are specifying the port in your connection string. You can see your port mapping in the **PORTS** column of the `docker ps -a` output. For example, the following command connects sqlcmd to a container listening on port 1401:

    ```bash
    sqlcmd -S 10.3.2.4,1401 -U SA -P '<YourPassword>'
    ```

- If you used `docker run` with an existing mapped data volume or data volume container, SQL Server ignores the value of `SA_PASSWORD`. Instead, the pre-configured SA user password is used from the SQL Server data in the data volume or data volume container. Verify that you are using the SA password associated with the data you're attaching to.

- Review the [SQL Server setup and error logs](#errorlogs).

### SQL Server Availability Groups

If you are using Docker with SQL Server Availability Groups, there are two additional requirements.

- Map the port that is used for replica communication (default 5022). For example, specify `-p 5022:5022` as part of your `docker run` command.

- Explicitly set the container host name with the `-h YOURHOSTNAME` parameter of the `docker run` command. This host name is used when you configure your Availability Group. If you don't specify it with `-h`, it defaults to the container ID.

### <a id="errorlogs"></a> SQL Server setup and error logs

You can look at the SQL Server setup and error logs in **/var/opt/mssql/log**. If the container is not running, first start the container. Then use an interactive command-prompt to inspect the logs.

```bash
docker start e69e056c702d
docker exec -it e69e056c702d "bash"
```

From the bash session inside your container, run the following commands:

```bash
cd /var/opt/mssql/log
cat setup*.log
cat errorlog
```

> [!TIP]
> If you mounted a host directory to **/var/opt/mssql** when you created your container, you can instead look in the **log** subdirectory on the mapped path on the host.

## Next steps

After installing SQL Server on Linux, next see [how to connect to the server and run basic Transact-SQL queries](sql-server-linux-connect-and-query-sqlcmd.md). Also, check out the [mssql-docker GitHub repository](https://github.com/Microsoft/mssql-docker) for resources, feedback, and known issues.
