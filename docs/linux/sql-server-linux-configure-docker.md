---
title: Configuration options for SQL Server on Docker | Microsoft Docs
description: Explore different ways of using and interacting with SQL Server 2017 and 2019 preview container images in Docker. This includes persisting data, copying files, and troubleshooting.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 01/17/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.assetid: 82737f18-f5d6-4dce-a255-688889fdde69
ms.custom: "sql-linux"
moniker: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---
# Configure SQL Server container images on Docker

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article explains how to configure and use the [mssql-server-linux container image](https://hub.docker.com/r/microsoft/mssql-server-linux/) with Docker. This image consists of SQL Server running on Linux based on Ubuntu 16.04. It can be used with the Docker Engine 1.8+ on Linux or on Docker for Mac/Windows.

> [!NOTE]
> This article specifically focuses on using the mssql-server-linux image. The Windows image is not covered, but you can learn more about it on the [mssql-server-windows Docker Hub page](https://hub.docker.com/r/microsoft/mssql-server-windows-developer/).

## Pull and run the container image

To pull and run the Docker container images for SQL Server 2017 and SQL Server 2019 preview, follow the prerequisites and steps in the following quickstart:

- [Run the SQL Server 2017 container image with Docker](quickstart-install-connect-docker.md?view=sql-server-2017)
- [Run the SQL Server 2019 preview container image with Docker](quickstart-install-connect-docker.md?view=sql-server-ver15)

This configuration article provides additional usage scenarios in the following sections.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

## <a id="rhel"></a> Run RHEL-based container images

All of the documentation on SQL Server Linux container images point to Ubuntu-based containers. Beginning with SQL Server 2019 preview, you can use containers based on Red Hat Enterprise Linux (RHEL). Change the container repository from **mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu** to **mcr.microsoft.com/mssql/rhel/server:2019-CTP2.4** in all of your docker commands.

For example, the following command pulls the latest SQL Server 2019 preview container that uses RHEL:

```bash
sudo docker pull mcr.microsoft.com/mssql/rhel/server:2019-CTP2.4
```

```PowerShell
docker pull mcr.microsoft.com/mssql/rhel/server:2019-CTP2.4
```

::: moniker-end

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

## <a id="production"></a> Run production container images

The quickstart in the previous section runs the free Developer edition of SQL Server from Docker Hub. Most of the information still applies if you want to run production container images, such as Enterprise, Standard, or Web editions. However, there are a few differences that are outlined here.

- You can only use SQL Server in a production environment if you have a valid license. You can obtain a free SQL Server Express production license [here](https://go.microsoft.com/fwlink/?linkid=857693). SQL Server Standard and Enterprise Edition licenses are available through [Microsoft Volume Licensing](https://www.microsoft.com/licensing/default.aspx).


- The Developer container image can be configured to run the production editions as well. Use the following steps to run production editions:

Review the requirements and run procedures in the [quickstart](quickstart-install-connect-docker.md). You must specify your production edition with the **MSSQL_PID** environment variable. The following example shows how to run the latest SQL Server 2017 container image for the Enterprise Edition:

```bash
docker run --name sqlenterprise \
      -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' \
      -e 'MSSQL_PID=Enterprise' -p 1433:1433 \
      -d store/microsoft/mssql-server-linux:2017-latest
```

```PowerShell
docker run --name sqlenterprise `
      -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
      -e "MSSQL_PID=Enterprise" -p 1433:1433 `
      -d "store/microsoft/mssql-server-linux:2017-latest"
 ```

      > [!IMPORTANT]
      > By passing the value **Y** to the environment variable **ACCEPT_EULA** and an edition value to **MSSQL_PID**, you are expressing that you have a valid and existing license for the edition and version of SQL Server that you intend to use. You also agree that your use of SQL Server software running in a Docker container image will be governed by the terms of your SQL Server license.

      > [!NOTE]
      > For a full list of possible values for **MSSQL_PID**, see [Configure SQL Server settings with environment variables on Linux](sql-server-linux-configure-environment-variables.md).

::: moniker-end

## Connect and query

You can connect and query SQL Server in a container from either outside the container or from within the container. The following sections explain both scenarios. 

### Tools outside the container

You can connect to the SQL Server instance on your Docker machine from any external Linux, Windows, or macOS tool that supports SQL connections. Some common tools include:

- [sqlcmd](sql-server-linux-setup-tools.md)
- [Visual Studio Code](sql-server-linux-develop-use-vscode.md)
- [SQL Server Management Studio (SSMS) on Windows](sql-server-linux-manage-ssms.md)

The following example uses **sqlcmd** to connect to SQL Server running in a Docker container. The IP address in the connection string is the IP address of the host machine that is running the container.

```bash
sqlcmd -S 10.3.2.4 -U SA -P '<YourPassword>'
```

```PowerShell
sqlcmd -S 10.3.2.4 -U SA -P "<YourPassword>"
```

If you mapped a host port that was not the default **1433**, add that port to the connection string. For example, if you specified `-p 1400:1433` in your `docker run` command, then connect by explicitly specify port 1400.

```bash
sqlcmd -S 10.3.2.4,1400 -U SA -P '<YourPassword>'
```

```PowerShell
sqlcmd -S 10.3.2.4,1400 -U SA -P "<YourPassword>"
```

### Tools inside the container

Starting with SQL Server 2017 preview, the [SQL Server command-line tools](sql-server-linux-setup-tools.md) are included in the container image. If you attach to the image with an interactive command-prompt, you can run the tools locally.

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

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

The following example creates two SQL Server 2017 containers and maps them to ports **1401** and **1402** on the host machine.

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-latest
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1402:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1402:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

The following example creates two SQL Server 2019 preview containers and maps them to ports **1401** and **1402** on the host machine.

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1402:1433 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1402:1433 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

::: moniker-end

Now there are two instances of SQL Server running in separate containers. Clients can connect to each SQL Server instance by using the IP address of the Docker host and the port number for the container.

```bash
sqlcmd -S 10.3.2.4,1401 -U SA -P '<YourPassword>'
sqlcmd -S 10.3.2.4,1402 -U SA -P '<YourPassword>'
```

```PowerShell
sqlcmd -S 10.3.2.4,1401 -U SA -P "<YourPassword>"
sqlcmd -S 10.3.2.4,1402 -U SA -P "<YourPassword>"
```

## <a id="customcontainer"></a> Create a customized container

It is possible to create your own [Dockerfile](https://docs.docker.com/engine/reference/builder/#usage) to create a customized SQL Server container. For more information, see [a demo that combines SQL Server and a Node application](https://github.com/twright-msft/mssql-node-docker-demo-app). If you do create your own Dockerfile, be aware of the foreground process, because this process controls the life of the container. If it exits, the container will shutdown. For example, if you want to run a script and start SQL Server, make sure that the SQL Server process is the right-most command. All other commands are run in the background. This is illustrated in the following command inside a Dockerfile:

```bash
/usr/src/app/do-my-sql-commands.sh & /opt/mssql/bin/sqlservr
```

If you reversed the commands in the previous example, the container would shutdown when the do-my-sql-commands.sh script completes.

## <a id="persist"></a> Persist your data

Your SQL Server configuration changes and database files are persisted in the container even if you restart the container with `docker stop` and `docker start`. However, if you remove the container with `docker rm`, everything in the container is deleted, including SQL Server and your databases. The following section explains how to use **data volumes** to persist your database files even if the associated containers are deleted.

> [!IMPORTANT]
> For SQL Server, it is critical that you understand data persistence in Docker. In addition to the discussion in this section, see Docker's documentation on [how to manage data in Docker containers](https://docs.docker.com/engine/tutorials/dockervolumes/).

### Mount a host directory as data volume

The first option is to mount a directory on your host as a data volume in your container. To do that, use the `docker run` command with the `-v <host directory>:/var/opt/mssql` flag. This allows the data to be restored between container executions.

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

::: moniker-end

This technique also enables you to share and view the files on the host outside of Docker.

> [!IMPORTANT]
> Host volume mapping for Docker on Mac with the SQL Server on Linux image is not supported at this time. Use data volume containers instead. This restriction is specific to the `/var/opt/mssql` directory. Reading from a mounted directory works fine. For example, you can mount a host directory using -v on Mac and restore a backup from a .bak file that resides on the host.

### Use data volume containers

The second option is to use a data volume container. You can create a data volume container by specifying a volume name instead of a host directory with the `-v` parameter. The following example creates a shared data volume named **sqlvolume**.

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```
::: moniker-end

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

**Example:**

```bash
docker cp d6b75213ef80:/var/opt/mssql/log/errorlog /tmp/errorlog
```

```PowerShell
docker cp d6b75213ef80:/var/opt/mssql/log/errorlog C:\Temp\errorlog
```

## Copy files into a container

To copy a file into the container, use the following command:

```bash
docker cp <Host path> <Container ID>:<Container path>
```

**Example:**

```bash
docker cp /tmp/mydb.mdf d6b75213ef80:/var/opt/mssql/data
```

```PowerShell
docker cp C:\Temp\mydb.mdf d6b75213ef80:/var/opt/mssql/data
```
## <a id="tz"></a> Configure the timezone

To run SQL Server in a Linux container with a specific timezone, configure the **TZ** environment variable. To find the right timezone value, run the **tzselect** command from a Linux bash prompt:

```bash
tzselect
```

After selecting the timezone, **tzselect** displays output similar to the following:

```bash
The following information has been given:

        United States
        Pacific

Therefore TZ='America/Los_Angeles' will be used.
```

You can use this information to set the same environment variable in your Linux container. The following example shows how to run SQL Server in a container in the `Americas/Los_Angeles` timezone:

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' \
   -p 1433:1433 --name sql1 \
   -e 'TZ=America/Los_Angeles'\
   -d mcr.microsoft.com/mssql/server:2017-latest 
```

```PowerShell
sudo docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
   -p 1433:1433 --name sql1 `
   -e "TZ=America/Los_Angeles" `
   -d mcr.microsoft.com/mssql/server:2017-latest 
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' \
   -p 1433:1433 --name sql1 \
   -e 'TZ=America/Los_Angeles'\
   -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

```PowerShell
sudo docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
   -p 1433:1433 --name sql1 `
   -e "TZ=America/Los_Angeles" `
   -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```
::: moniker-end

## <a id="tags"></a> Run a specific SQL Server container image

There are scenarios where you might not want to use the latest SQL Server container image. To run a specific SQL Server container image, use the following steps:

1. Identify the Docker **tag** for the release you want to use. To view the available tags, see [the mssql-server-linux Docker hub page](https://hub.docker.com/r/microsoft/mssql-server-linux/tags/).

2. Pull the SQL Server container image with the tag. For example, to pull the RC1 image, replace `<image_tag>` in the following command with `rc1`.

   ```bash
   docker pull mcr.microsoft.com/mssql/server:<image_tag>
   ```

3. To run a new container with that image, specify the tag name in the `docker run` command. In the following command, replace `<image_tag>` with the version you want to run.

   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1401:1433 -d mcr.microsoft.com/mssql/server:<image_tag>
   ```

   ```PowerShell
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1401:1433 -d mcr.microsoft.com/mssql/server:<image_tag>
   ```

These steps can also be used to downgrade an existing container. For example, you might want to rollback or downgrade a running container for troubleshooting or testing. To downgrade a running container, you must be using a persistence technique for the data folder. Follow the same steps outlined in the [upgrade section](#upgrade), but specify the tag name of the older version when you run the new container.

## <a id="version"></a> Check the container version

If you want to know the version of SQL Server in a running docker container, run the following command to display it. Replace `<Container ID or name>` with the target container ID or name. Replace `<YourStrong!Passw0rd>` with the SQL Server password for the SA login.

```bash
sudo docker exec -it <Container ID or name> /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '<YourStrong!Passw0rd>' \
   -Q 'SELECT @@VERSION'
```

```PowerShell
docker exec -it <Container ID or name> /opt/mssql-tools/bin/sqlcmd `
   -S localhost -U SA -P "<YourStrong!Passw0rd>" `
   -Q 'SELECT @@VERSION'
```

You can also identify the SQL Server version and build number for a target docker container image. The following command displays the SQL Server version and build information for the **microsoft/mssql-server-linux:2017-latest** image. It does this by running a new container with an environment variable **PAL_PROGRAM_INFO=1**. The resulting container instantly exits, and the `docker rm` command removes it.

```bash
sudo docker run -e PAL_PROGRAM_INFO=1 --name sqlver \
   -ti mcr.microsoft.com/mssql/server:2017-latest && \
   sudo docker rm sqlver
```

```PowerShell
docker run -e PAL_PROGRAM_INFO=1 --name sqlver `
   -ti mcr.microsoft.com/mssql/server:2017-latest; `
   docker rm sqlver
```

The previous commands display version information similar to the following output:

```Text
sqlservr
  Version 14.0.3029.16
  Build ID ee3d3882f1c48a7a7e590a620153012eaedc2f37143d485df945a079b9d4eeea
  Build Type release
  Git Version 65d42c4
  Built at Sat Jun 16 01:20:11 GMT 2018

PAL
  Build ID 60cfcb134bbae96d311f6a4f56aeb5a685b3809de80bcb61ec587a8f58b555eb
  Build Type release
  Git Version 21a4c11
  Built at Sat Jun 16 01:18:53 GMT 2018

Packages
  system.sfp                    6.2.9200.1,21a4c1178,
  system.common.sfp             10.0.15063.540
  system.certificates.sfp       6.2.9200.1,21a4c1178,
  system.netfx.sfp              4.6.1590.0
  secforwarderxplat.sfp         14.0.3029.16
  sqlservr.sfp                  14.0.3029.16
  sqlagent.sfp                  14.0.3029.16
```

## <a id="upgrade"></a> Upgrade SQL Server in containers

To upgrade the container image with Docker, first identify the tag for the release for your upgrade. Pull this version from the registry with the `docker pull` command:

```bash
docker pull mcr.microsoft.com/mssql/server:<image_tag>
```

This updates the SQL Server image for any new containers you create, but it does not update SQL Server in any running containers. To do this, you must create a new container with the latest SQL Server container image and migrate your data to that new container.

1. Make sure you are using one of the [data persistence techniques](#persist) for your existing SQL Server container. This enables you to start a new container with the same data.

1. Stop the SQL Server container with the `docker stop` command.

1. Create a new SQL Server container with `docker run` and specify either a mapped host directory or a data volume container. Make sure to use the specific tag for your SQL Server upgrade. The new container now uses a new version of SQL Server with your existing SQL Server data.

   > [!IMPORTANT]
   > Upgrade is only supported between RC1, RC2, and GA at this time.

1. Verify your databases and data in the new container.

1. Optionally, remove the old container with `docker rm`.

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

On Windows, verify that you are launching PowerShell or your command-prompt as an Administrator.

### SQL Server container startup errors

If the SQL Server container fails to run, try the following tests:

- If you get an error such as **'failed to create endpoint CONTAINER_NAME on network bridge. Error starting proxy: listen tcp 0.0.0.0:1433 bind: address already in use.'**, then you are attempting to map the container port 1433 to a port that is already in use. This can happen if you're running SQL Server locally on the host machine. It can also happen if you start two SQL Server containers and try to map them both to the same host port. If this happens, use the `-p` parameter to map the container port 1433 to a different host port. For example: 

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1400:1433 -d mcr.microsoft.com/mssql/server:2017-latest`.
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2017-latest`.
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1400:1433 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu`.
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu`.
```

::: moniker-end

- Check to see if there are any error messages from container.

    ```bash
    docker logs e69e056c702d
    ```

- Make sure that you meet the minimum memory and disk requirements specified in the [prerequisites](quickstart-install-connect-docker.md#requirements) section of the quickstart article.

- If you are using any container management software, make sure it supports container processes running as root. The sqlservr process in the container runs as root.

- Review the [SQL Server setup and error logs](#errorlogs).

### Enable dump captures

If the SQL Server process is failing inside the container, you should create a new container with **SYS_PTRACE** enabled. This adds the Linux capability to trace a process, which is necessary for creating a dump file on an exception. The dump file can be used by support to help troubleshoot the problem. The following docker run command enables this capability.

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

::: moniker-end

### SQL Server connection failures

If you can't connect to the SQL Server instance running in your container, try the following tests:

- Make sure that your SQL Server container is running by looking at the **STATUS** column of the `docker ps -a` output. If not, use `docker start <Container ID>` to start it.

- If you mapped to a non-default host port (not 1433), make sure you are specifying the port in your connection string. You can see your port mapping in the **PORTS** column of the `docker ps -a` output. For example, the following command connects sqlcmd to a container listening on port 1401:

    ```bash
    sqlcmd -S 10.3.2.4,1401 -U SA -P '<YourPassword>'
    ```

    ```PowerShell
    sqlcmd -S 10.3.2.4,1401 -U SA -P "<YourPassword>"
    ```

- If you used `docker run` with an existing mapped data volume or data volume container, SQL Server ignores the value of `MSSQL_SA_PASSWORD`. Instead, the pre-configured SA user password is used from the SQL Server data in the data volume or data volume container. Verify that you are using the SA password associated with the data you're attaching to.

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

Get started with SQL Server 2017 container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md).

Also, see the [mssql-docker GitHub repository](https://github.com/Microsoft/mssql-docker) for resources, feedback, and known issues.

[Explore high availability for SQL Server containers](sql-server-linux-container-ha-overview.md)
