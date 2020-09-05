---
title: Secure SQL Server Docker containers
description: Understand the different ways to secure SQL Server Docker containers and how you can run containers as different non-root user on the host
author: vin-yu
ms.author: vinsonyu
ms.reviewer: vanto
ms.date: 09/04/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
moniker: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---

# Secure SQL Server Docker containers

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

SQL Server 2017 containers start up as the root user by default. This can cause some security concerns. This article talks about security options that you have when running SQL Server Docker containers, and how to build a SQL Server container as a non-root user.

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

    ```bash
    docker exec -it sql1 bash
    ```

    Run `whoami`, which will return the user running within the container.
    
    ```bash
    whoami
    ```

## <a id="nonrootuser"></a> Run container as a different non-root user on the host

To run the SQL Server container as a different non-root user, add the -u flag to the docker run command. The non-root container has the restriction that it must run as part of the root group unless a volume is mounted to `/var/opt/mssql` that the non-root user can access. The root group doesn’t grant any extra root permissions to the non-root user.

#### Run as a user with a UID 4000

You can start SQL Server with a custom UID. For example, the command below starts SQL Server with UID 4000:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u 4000:0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

> [!Warning]
> Ensure that the SQL Server container has a named user such as 'mssql' or 'root' or SQLCMD will not be able to run within the container. You can check if the SQL Server container is running as a named user by running `whoami` within the container.

#### Run the non-root container as the root user

You can run the non-root container as the root user if necessary. This would also grant all file permissions automatically to the container because it has higher privilege.

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" -u 0:0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

#### Run as a user on your host machine

You can start SQL Server with an existing user on the host machine with the following command:
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u $(id -u myusername):0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

#### Run as a different user and group

You can start SQL Server with a custom user and group. In this example, the mounted volume has permissions configured for the user or group on the host machine.

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u (id -u myusername):(id -g myusername) -v /path/to/mssql:/var/opt/mssql -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

## <a id="storagepermissions"></a> Configure persistent storage permissions for non-root containers

To allow the non-root user to access database files that are on mounted volumes, ensure that the user or group you run the container under can read/write the persistent file storage.  

You can get the current ownership of the database files with this command.

```bash
ls -ll <database file dir>
```

Run one of the following commands if SQL Server does not have access to persisted database files.

#### Grant the root group r/w access to the DB files

Grant the root group permissions to the following directories so that the non-root SQL Server container has access to database files.

```bash
chgrp -R 0 <database file dir>
chmod -R g=u <database file dir>
```

#### Set the non-root user as the owner of the files

This can be the default non-root user, or any other non-root user you’d like to specify. In this example, we set UID 10001 as the non-root user.

```bash
chown -R 10001:0 <database file dir>
```

## Next steps

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

- Get started with SQL Server 2017 container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md?view=sql-server-2017)

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

- Get started with SQL Server 2019 container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md?view=sql-server-ver15)

::: moniker-end

- [Deploy and connect to SQL Server Docker containers](sql-server-linux-docker-container-deployment.md)

- [Reference additional configuration and customization to Docker containers](sql-server-linux-docker-container-configure.md)

- [Troubleshooting SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md)