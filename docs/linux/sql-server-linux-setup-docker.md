---
# required metadata

title: Run SQL Server 2017 on Docker | Microsoft Docs
description: Download and run the Docker image for SQL Server 2017.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 05/03/2017
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
# Run the SQL Server 2017 Docker image on Linux, Mac, or Windows

This topic explains how to pull and run the [mssql-server Docker image](https://hub.docker.com/r/microsoft/mssql-server-linux/). This image consists of SQL Server running on Linux and can be used with the Docker Engine 1.8+ on Linux or on Docker for Mac/Windows. We are currently tracking all issues with the Docker image in our [mssql-docker GitHub repository](https://github.com/Microsoft/mssql-docker).

> [!NOTE]
> This image is running SQL Server on an Ubuntu Linux base image. To run the SQL Server on Windows Containers Docker image, check out the [mssql-server-windows Docker Hub page](https://hub.docker.com/r/microsoft/mssql-server-windows/).

## <a id="requirements"></a> Requirements for Docker

- Docker Engine 1.8+ on any supported Linux distribution or Docker for Mac/Windows.
- Minimum of 4 GB of disk space
- Minimum of 4 GB of RAM

> [!IMPORTANT]
> The default on Docker for Mac and Docker for Windows is 2 GB for the Moby VM, so you will need to change it to 4 GB. The following sections explain how.

### Docker for Mac

1. Click on the Docker logo on the top status bar.
2. Select **Preferences**.
3. Move the memory indicator to 4GB or more.
4. Click the **restart** button at the button of the screen.

### Docker for Windows:

1. Right-click on the Docker icon from the task bar.
2. Click **Settings** under that menu.
3. Click on the **Advanced** Tab.
4. Move the memory indicator to 4GB or more.
5. Click the **Apply** button.

## Pull and run the Docker image

1. Pull the Docker image from Docker Hub.

    ```bash
    sudo docker pull microsoft/mssql-server-linux
    ```

    > [!TIP]
    > If you using Docker for Windows, remove the word **sudo** from the command-line. Do this for all occurences in this topic.

2. To run the Docker image, you can use the following command. Specify your own strong password that is at least 8 characters and meets [SQL Server's password requirements](../relational-databases/security/password-policy.md).

    ```bash
    sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -d microsoft/mssql-server-linux
    ```

    > [!NOTE]
    > The **ACCEPT_EULA** and **SA_PASSWORD** environment variables are required to run the image. Setting the **ACCEPT_EULA** variable to any value confirms your acceptance of the [End-User Licensing Agreement](http://go.microsoft.com/fwlink/?LinkId=746388).

3. To view your Docker containers, use the `docker ps` command.

    ```bash
    sudo docker ps -a
    ```

    ![Docker ps command output](./media/sql-server-linux-setup-docker/docker-ps-command.png)

4. If the **STATUS** column shows a status of **Up**, then SQL Server is running in the container and listening on the port specified in the **PORTS** column.

    > [!TIP]
    > If the **STATUS** column for your SQL Server container shows **Exited**, see the [Troubleshooting](#troubleshooting) section.

## Connect and query

You can connect to the SQL Server instance on your Docker machine from any external Windows or Linux tool that supports SQL connections, such as:

- [sqlcmd](sql-server-linux-connect-and-query-sqlcmd.md)
- [Visual Studio Code](sql-server-linux-develop-use-vscode.md)
- [SQL Server Management Studio (SSMS) on Windows](sql-server-linux-develop-use-ssms.md)

### Connect with sqlcmd

The following example uses **sqlcmd** to connect to SQL Server running in a Docker container. The IP address in the connection string is the IP address of the host machine that is running the container.

```bash
sqlcmd -S 10.3.2.4 -U SA -P '<YourPassword>'
```

If you mapped a host port that was not the default **1433**, add that port to the connection string. For example, if you specified `-p 1400:1433` in your `docker run` command, then connect by explicitly specify port 1400.

```bash
sqlcmd -S 10.3.2.4,1400 -U SA -P '<YourPassword>'
```

### Run tools inside the container

Starting with SQL Server 2017 CTP 2.0, the [SQL Server command-line tools](sql-server-linux-setup-tools.md) are included in the Docker image. If you attach to the image with an interactive command-prompt, you can run the tools locally.

1. Use the `docker exec -it` command to start an interactive bash shell inside your running container. In the following example `e6` is the first two characters of the container ID that uniquely identifies the target container.

    ```bash
    sudo docker exec -it e6 "bash"
    ```

2. Once insider the container, connect locally with sqlcmd. Note that sqlcmd is not in the path by default, so you have to specify the full path.

    ```bash
    /opt/mssql-tools/bin/sqlcmd -H localhost -U SA -P '<YourPassword>'
    ```

3. When finished with sqlcmd, type `exit`.

4. When finished with the interactive command-prompt, type `exit`. Your container continues to run after you exit the interactive bash shell.

## Run multiple SQL Server containers

Docker provides a way to support multiple SQL Server instances on the same host machine. The following example creates two SQL Server Docker containers and maps them to ports **1401** and **1402** on the host machine.

```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1401:1433 -d microsoft/mssql-server-linux
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1402:1433 -d microsoft/mssql-server-linux
```

Now there are two instances of SQL Server running in separate containers. Clients can connect to each SQL Server instance by using the IP address of the Docker host and the port number for the container.

```bash
sqlcmd -S 10.3.2.4,1401 -U SA -P '<YourPassword>'
sqlcmd -S 10.3.2.4,1402 -U SA -P '<YourPassword>'
```

## Persist your data

Your SQL Server configuration changes and database files are persisted in the container even if you restart the container with `docker stop` and `docker start`. However, if you remove the container with `docker rm`, everything in the container is deleted, including SQL Server and all of your databases. The following section explains how to use **data volumes** to persist your database files even if the associated containers are deleted.

> [!IMPORTANT]
> For SQL Server, it is critical that you understand data persistence in Docker. In addition to the discussion in this section, see Docker's documentation on [how to manage data in Docker containers](https://docs.docker.com/engine/tutorials/dockervolumes/).

### Mount a host directory as data volume

The first option is to mount a directory on your host as a data volume in your container. To do that, use the `docker run` command with the `-v \<host directory\>:/var/opt/mssql` flag. This allows the data to be restored between container executions.

```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d microsoft/mssql-server-linux
```

This technique also enables you to share and view the files on the host outside of Docker.

> [!IMPORTANT]
> Host volume mapping for Docker on Mac with the SQL Server on Linux image is not supported at this time. Use data volume containers instead.

### Use data volume containers

The second option is to use a data volume container. You can create a data volume container by specifying a volume name instead of a host directory with the `-v` parameter. The following example creates a shared data volume named **sqlvolume**.

```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d microsoft/mssql-server-linux
```

> [!NOTE]
> This technique for implicitly creating a new data volume in the run command does not work with older versions of Docker. In that case, use the explicit steps outlined in the Docker documentation, [Creating and mounting a data volume container](https://docs.docker.com/engine/tutorials/dockervolumes/#creating-and-mounting-a-data-volume-container).

Even if you stop and remove this container, the data volume persists. You can view it with the `docker volume ls` command.

```bash
sudo docker volume ls
```

If you then create a new container with the same volume name, the new container will use the same SQL Server data contained in the volume.

## Upgrade the Docker image

To upgrade the Docker image, pull the latest version from the registry. Use the `docker pull` command:

```bash
    sudo docker pull microsoft/mssql-server-linux:latest
```

You can now create new containers that will have the latest version of SQL Server in Linux on Docker.

## <a id="troubleshooting"></a> Troubleshooting

Use the following troubleshooting suggestions if your SQL Server Docker container fails to run or if you can't connect to the SQL Server instance running in your container.

- First, check to see if there are any error messages from container.

    ```bash
    sudo docker logs e6
    ```

- Look at the SQL Server setup and error logs in **/var/opt/mssql/log**. If the container is not running, first start the container. Then use an interactive command-prompt to inspect the logs.

    ```bash
    sudo docker start e6
    sudo docker exec -it e6 "bash"
    ```

    From the bash session insider your container, run the following commands:

    ```bash
    cd /var/opt/mssql/log
    ls
    cat setup*.log
    cat errorlog
    ```

    > [!TIP]
    > If you mounted a host directory to **/var/opt/mssql** when you created your container, you can instead look in the **log** subdirectory on the mapped path on the host.

- If you mapped to a non-default host port (not 1433), make sure you are specifying the port in your connection string.

- Make sure that you meet the minimum memory and disk requirements specified in the [Requirements](#requirements) section of this topic.

## Next steps

After installing SQL Server on Linux, next see [how to connect to the server and run basic Transact-SQL queries](sql-server-linux-connect-and-query-sqlcmd.md). Also, check out the [mssql-docker GitHub repository](https://github.com/Microsoft/mssql-docker) for resources and feedback.
