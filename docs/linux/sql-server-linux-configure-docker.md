---
title: Configuration options for SQL Server on Docker
description: Explore different ways of using and interacting with SQL Server 2017 and 2019 container images in Docker. This includes persisting data, copying files, and troubleshooting.
author: vin-yu
ms.author: vinsonyu
ms.reviewer: vanto
ms.date: 01/08/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.assetid: 82737f18-f5d6-4dce-a255-688889fdde69
moniker: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---
# Configure SQL Server container images on Docker

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how to configure and use the [mssql-server-linux container image](https://hub.docker.com/_/microsoft-mssql-server) with Docker. 

For other deployment scenarios, see:

- [Windows](../database-engine/install-windows/install-sql-server.md)
- [Linux](../linux/sql-server-linux-setup.md)
- [Kubernetes - Big Data Clusters](../big-data-cluster/deploy-get-started.md)

This image consists of SQL Server running on Linux based on Ubuntu 16.04. It can be used with the Docker Engine 1.8+ on Linux or on Docker for Mac/Windows.

> [!NOTE]
> This article specifically focuses on using the mssql-server-linux image. The Windows image is not covered, but you can learn more about it on the [mssql-server-windows Docker Hub page](https://hub.docker.com/r/microsoft/mssql-server-windows-developer/).

> [!IMPORTANT]
> Before choosing to run a SQL Server container for production use cases, please review our [support policy for SQL Server Containers](https://support.microsoft.com/help/4047326/support-policy-for-microsoft-sql-server) to ensure that you are running on a supported configuration.

This 6-minute video provides an introduction into running SQL Server on containers:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2019-in-Containers/player?WT.mc_id=dataexposed-c9-niner]


## Pull and run the container image

To pull and run the Docker container images for SQL Server 2017 and SQL Server 2019, follow the prerequisites and steps in the following quickstart:

- [Run the SQL Server 2017 container image with Docker](quickstart-install-connect-docker.md?view=sql-server-2017)
- [Run the SQL Server 2019 container image with Docker](quickstart-install-connect-docker.md?view=sql-server-ver15)

This configuration article provides additional usage scenarios in the following sections.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

## <a id="rhel"></a> Run RHEL-based container images

The documentation for SQL Server Linux container images points to Ubuntu-based containers. Beginning with SQL Server 2019, you can use containers based on Red Hat Enterprise Linux (RHEL). Change the container repository from **mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04** to **mcr.microsoft.com/mssql/rhel/server:2019-CU1-rhel-8** in all of your docker commands.

For example, the following command pulls the Cumulative Update 1 for SQL Server 2019 container that uses RHEL 8:

```bash
sudo docker pull mcr.microsoft.com/mssql/rhel/server:2019-CU1-rhel-8
```

```PowerShell
docker pull mcr.microsoft.com/mssql/rhel/server:2019-CU1-rhel-8
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
      -d mcr.microsoft.com/mssql/server:2017-latest
```

```PowerShell
docker run --name sqlenterprise `
      -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
      -e "MSSQL_PID=Enterprise" -p 1433:1433 `
      -d "mcr.microsoft.com/mssql/server:2017-latest"
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

Starting with SQL Server 2017, the [SQL Server command-line tools](sql-server-linux-setup-tools.md) are included in the container image. If you attach to the image with an interactive command-prompt, you can run the tools locally.

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

Docker provides a way to run multiple SQL Server containers on the same host machine. Use this approach for scenarios that require multiple instances of SQL Server on the same host. Each container must expose itself on a different port.

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

The following example creates two SQL Server 2019 containers and maps them to ports **1401** and **1402** on the host machine.

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1402:1433 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1402:1433 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
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

It is possible to create your own [Dockerfile](https://docs.docker.com/engine/reference/builder/#usage) to create a customized SQL Server container. For more information, see [a demo that combines SQL Server and a Node application](https://github.com/twright-msft/mssql-node-docker-demo-app). If you do create your own Dockerfile, be aware of the foreground process, because this process controls the life of the container. If it exits, the container will shutdown. For example, if you want to run a script and start SQL Server, make sure that the SQL Server process is the right-most command. All other commands are run in the background. The following command illustrates this inside a Dockerfile:

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
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2017-latest
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>/data:/var/opt/mssql/data -v <host directory>/log:/var/opt/mssql/log -v <host directory>/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

::: moniker-end

This technique also enables you to share and view the files on the host outside of Docker.

> [!IMPORTANT]
> Host volume mapping for **Docker on Windows** does not currently support mapping the complete `/var/opt/mssql` directory. However, you can map a subdirectory, such as `/var/opt/mssql/data` to your host machine.

> [!IMPORTANT]
> Host volume mapping for **Docker on Mac** with the SQL Server on Linux image is not supported at this time. Use data volume containers instead. This restriction is specific to the `/var/opt/mssql` directory. Reading from a mounted directory works fine. For example, you can mount a host directory using -v on Mac and restore a backup from a .bak file that resides on the host.

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
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
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
docker exec -it <Container ID> /bin/bash
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

To run SQL Server in a Linux container with a specific timezone, configure the `TZ` environment variable. To find the right timezone value, run the `tzselect` command from a Linux bash prompt:

```bash
tzselect
```

After selecting the timezone, `tzselect` displays output similar to the following:

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
docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
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
   -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

```PowerShell
docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" `
   -p 1433:1433 --name sql1 `
   -e "TZ=America/Los_Angeles" `
   -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```
::: moniker-end

## <a id="tags"></a> Run a specific SQL Server container image

There are scenarios where you might not want to use the latest SQL Server container image. To run a specific SQL Server container image, use the following steps:

1. Identify the Docker **tag** for the release you want to use. To view the available tags, see [the mssql-server-linux Docker hub page](https://hub.docker.com/_/microsoft-mssql-server).

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

You can also identify the SQL Server version and build number for a target docker container image. The following command displays the SQL Server version and build information for the **mcr.microsoft.com/mssql/server:2017-latest** image. It does this by running a new container with an environment variable **PAL_PROGRAM_INFO=1**. The resulting container instantly exits, and the `docker rm` command removes it.

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

## <a id="buildnonrootcontainer"></a> Build and run non-root SQL Server 2017 containers

Follow the steps below to build a SQL Server 2017 container that starts up as the `mssql`(non-root) user.

> [!NOTE]
> SQL Server 2019 containers automatically start up as non-root, so the following steps only apply to SQL Server 2017 containers, which start as root by default.

1. Download the [sample dockerfile for non-root SQL Server Container](https://raw.githubusercontent.com/microsoft/mssql-docker/master/linux/preview/examples/mssql-server-linux-non-root/Dockerfile) and save it as `dockerfile`.

2. Run the following command in the context of the dockerfile directory to build the non-root SQL Server container:

```bash
cd <path to dockerfile>
docker build -t 2017-latest-non-root .
```

3. Start the container.

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword@" --cap-add SYS_PTRACE --name sql1 -p 1433:1433 -d 2017-latest-non-root
```

> [!NOTE]
> The `--cap-add SYS_PTRACE` flag is required for non-root SQL Server containers to generate dumps for troubleshooting purposes.

4. Check that the container is running as non-root user:

docker exec into the container.
```bash
docker exec -it sql1 bash
```

Run `whoami` which will return the user running within the container.

```bash
whoami
```

## <a id="nonrootuser"></a> Run container as a different non-root user on the host

To run the SQL Server container as a different non-root user, add the -u flag to the docker run command. The non-root container has the restriction that it must run as part of the root group unless a volume is mounted to '/var/opt/mssql' that the non-root user can access. The root group doesn’t grant any extra root permissions to the non-root user.

**Run as a user with a UID 4000**

You can start SQL Server with a custom UID. For example, the command below starts SQL Server with UID 4000:
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u 4000:0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

> [!Warning]
> Ensure that the SQL Server container has a named user such as 'mssql' or 'root' or SQLCMD will not be able to run within the container. You can check if the SQL Server container is running as a named user by running `whoami` within the container.

**Run the non-root container as the root user**

You can run the non-root container as the root user if required. This would also grant all file permissions automatically to the container because it is higher privilege.

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" -u 0:0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

**Run as a user on your host machine**

You can start SQL Server with an existing user on the host machine with the following command:
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u $(id -u myusername):0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

**Run as a different user and group**

You can start SQL Server with a custom user and group. In this example, the mounted volume has permissions configured for the user or group on the host machine.

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u (id -u myusername):(id -g myusername) -v /path/to/mssql:/var/opt/mssql -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

## <a id="storagepermissions"></a> Configure persistent storage permissions for non-root containers

To allow the non-root user to access DB files that are on mounted volumes, ensure that the user/group you run the container under can touch the persistent file storage.  

You can get the current ownership of the database files with this command.

```bash
ls -ll <database file dir>
```

Run one of the following commands if SQL Server does not have access to persisted database files.

**Grant the root group r/w access to the DB files**

Grant the root group permissions to the following directories so that the non-root SQL Server container has access to database files.

```bash
chgrp -R 0 <database file dir>
chmod -R g=u <database file dir>
```

**Set the non-root user as the owner of the files.**

This can be the default non-root user, or any other non-root user you’d like to specify. In this example, we set UID 10001 as the non-root user.

```bash
chown -R 10001:0 <database file dir>
```

## <a id="changefilelocation"></a> Change the default file location

Add the `MSSQL_DATA_DIR` variable to change your data directory in your `docker run` command, then mount a volume to that location that your container’s user has access to.

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" -e "MSSQL_DATA_DIR=/my/file/path" -v /my/host/path:/my/file/path -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

## <a id="enablesqlagent"></a> Enable SQL Server Agent

Enable SQL Server Agent and it starts automatically with SQL Server

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" -e "MSSQL_AGENT_ENABLED=true" --name sql1 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

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
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1400:1433 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04`.
```

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1400:1433 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04`.
```

::: moniker-end

- If you get an error such as **'Got permission denied while trying to connect to the Docker daemon socket at unix:///var/run/docker.sock: Get http://%2Fvar%2Frun%2Fdocker.sock/v1.30tdout=1&tail=all: dial unix /var/run/docker.sock: connect: permission denied'** when trying to start a container, then add your user to the docker group in Ubuntu. Then logout and login again as this change will affect new sessions. 

   ```bash
    usermod -aG docker $USER
   ```
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
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
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
