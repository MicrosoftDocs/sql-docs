---
title: Secure SQL Server Linux containers
description: Understand the different ways to secure SQL Server Linux containers and how you can run containers as different non-root user on the host
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 09/30/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - contperf-fy21q1
  - engagement-fy23
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2017"
---

# Secure SQL Server Linux containers

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

[!INCLUDE [sssql17-md](../includes/sssql17-md.md)] containers start up as the root user by default. This can cause some security concerns. This article talks about security options that you have when running [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux containers, and how to build a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container as a non-root user.

The examples in this article assume that you're using Docker, but you can apply the same principles to other container orchestration tools including Kubernetes.

## <a id="buildnonrootcontainer"></a> Build and run non-root SQL Server 2017 containers

Follow the steps below to build a [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] container that starts up as the `mssql` (non-root) user.

> [!NOTE]  
> [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and later version containers automatically start up as non-root, so the following steps only apply to [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] containers, which start as root by default.

1. Download the [sample Dockerfile for non-root SQL Server containers](https://raw.githubusercontent.com/microsoft/mssql-docker/master/linux/preview/examples/mssql-server-linux-non-root/Dockerfile) and save it as `dockerfile`.

1. Run the following command in the context of the dockerfile directory to build the non-root [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container:

    ```bash
    cd <path to dockerfile>
    docker build -t 2017-latest-non-root .
    ```

1. Start the container.

   > [!IMPORTANT]  
   > The `SA_PASSWORD` environment variable is deprecated. Please use `MSSQL_SA_PASSWORD` instead.

    ```bash
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyStrongPassword@" --cap-add SYS_PTRACE --name sql1 -p 1433:1433 -d 2017-latest-non-root
    ```

    > [!NOTE]  
    > The `--cap-add SYS_PTRACE` flag is required for non-root [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] containers to generate dumps for troubleshooting purposes.

1. Check that the container is running as non-root user:

    ```bash
    docker exec -it sql1 bash
    ```

    Run `whoami`, which will return the user running within the container.

    ```bash
    whoami
    ```

## <a id="nonrootuser"></a> Run container as a different non-root user on the host

To run the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container as a different non-root user, add the `-u` flag to the `docker run` command. The non-root container has the restriction that it must run as part of the `root` group unless a volume is mounted to `/var/opt/mssql` that the non-root user can access. The `root` group doesn't grant any extra root permissions to the non-root user.

#### Run as a user with a UID 4000

You can start [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] with a custom UID. For example, the command below starts [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] with UID 4000:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u 4000:0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

> [!WARNING]  
> Ensure that the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container has a named user such as `mssql` or `root`, otherwise **sqlcmd** will not be able to run within the container. You can check if the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container is running as a named user by running `whoami` within the container.

#### Run the non-root container as the root user

You can run the non-root container as the root user if necessary. This would also grant all file permissions automatically to the container because it has higher privilege.

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyStrongPassword" -u 0:0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

#### Run as a user on your host machine

You can start [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] with an existing user on the host machine with the following command:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u $(id -u myusername):0 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

#### Run as a different user and group

You can start [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] with a custom user and group. In this example, the mounted volume has permissions configured for the user or group on the host machine.

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyStrongPassword" --cap-add SYS_PTRACE -u $(id -u myusername):$(id -g myusername) -v /path/to/mssql:/var/opt/mssql -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

## <a id="storagepermissions"></a> Configure persistent storage permissions for non-root containers

To allow the non-root user to access database files that are on mounted volumes, ensure that the user or group you run the container under can read from and write to the persistent file storage.

You can get the current ownership of the database files with this command.

```bash
ls -ll <database file dir>
```

Run one of the following commands if [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] doesn't have access to persisted database files.

#### Grant the root group read/write access to the database files

Grant the root group permissions to the following directories so that the non-root [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container has access to database files.

```bash
chgrp -R 0 <database file dir>
chmod -R g=u <database file dir>
```

#### Set the non-root user as the owner of the files

This can be the default non-root user, or any other non-root user you'd like to specify. In this example, we set UID 10001 as the non-root user.

```bash
chown -R 10001:0 <database file dir>
```

## Encrypt connections to SQL Server Linux containers

> [!IMPORTANT]  
> When configuring Active Directory authentication or encryption options such as Transparent Data Encryption (TDE) and SSL for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux or containers, there are several files, such as the keytab, certificates, and machine key, that are created by default under the folder `/var/opt/mssql/secrets`, and access to which is restricted by default to `mssql` and `root` users. When configuring persistent storage for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] containers, please use the same access strategy, ensuring that the path on the host or shared volume that is mapped to the `/var/opt/mssql/secrets` folder inside the container is protected and accessible only to the `mssql` and `root` users on the host as well. If the access to this path/folder is compromised, a malicious user can gain access to these critical files, compromising the encryption hierarchy and/or Active Directory configurations.

To encrypt connections to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux containers, you'll need a certificate with the following [requirements](sql-server-linux-encrypted-connections.md).

Below is an example of how the connection can be encrypted to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux containers. Here we use a self-signed certificate, which shouldn't be used for production scenarios. For such environments, you should use CA certificates instead.

1. Create a self-signed certificate, which is suited for test and non-production environments only.

   ```bash
   openssl req -x509 -nodes -newkey rsa:2048 -subj '/CN=sql1.contoso.com' -keyout /container/sql1/mssql.key -out /container/sql1/mssql.pem -days 365
   ```

   In the previous code sample, `sql1` is the hostname of the SQL container, so when connecting to this container the name used in the connection string is going to be `sql1.contoso.com,port`. You must also ensure that the folder path `/container/sql1/` already exists before running the above command.

1. Ensure you set the right permissions on the `mssql.key` and `mssql.pem` files, so you avoid errors when you mount the files to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container:

   ```bash
   chmod 440 /container/sql1/mssql.pem
   chmod 440 /container/sql1/mssql.key
   ```

1. Now create a `mssql.conf` file with the below content to enable the Server Initiated encryption. For Client initiated encryption, change the last line to `forceencryption = 0`.

   ```bash
   [network]
   tlscert = /etc/ssl/certs/mssql.pem
   tlskey = /etc/ssl/private/mssql.key
   tlsprotocols = 1.2
   forceencryption = 1
    ```

   > [!NOTE]  
   > For some Linux distributions the path for storing the certificate and key could also be : /etc/pki/tls/certs/ and /etc/pki/tls/private/ respectively. Please verify the path before updating the `mssql.conf` for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] containers. The location you set in the `mssql.conf` will be the location where [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] in the container is going to search for the certificate and its key. In this case, that location is `/etc/ssl/certs/` and `/etc/ssl/private/`.

   The `mssql.conf` file is also created under the same folder location `/container/sql1/`. After running the above steps, you should have three files: `mssql.conf`, `mssql.key`, and `mssql.pem` in the `sql1` folder.

1. Deploy the SQL container with the command shown below:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ssw0rd" -p 5434:1433 --name sql1 -h sql1 -v /container/sql1/mssql.conf:/var/opt/mssql/mssql.conf -v   /container/sql1/mssql.pem:/etc/ssl/certs/mssql.pem -v /container/sql1/mssql.key:/etc/ssl/private/mssql.key -d mcr.microsoft.com/mssql/server:2019-latest
   ```

   In the command above, we have mounted the `mssql.conf`, `mssql.pem`, and `mssql.key` files to the container and mapped the 1433 ([!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] default port) port in the container to port 5434 on the host.

   > [!NOTE]  
   > If you are using RHEL 8 and above, you can also use `podman run` command instead of `docker run`.

Follow the "Register the certificate on your client machine" and "Example connection strings" sections documented in [Client Initiated Encryption](sql-server-linux-encrypted-connections.md#client-initiated-encryption) to start encrypting connections to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux containers.

## Next steps

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

- Get started with [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md?view=sql-server-2017&preserve-view=true)

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

- Get started with [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md)

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

- Get started with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] container images on Docker by going through the [quickstart](quickstart-install-connect-docker.md)

::: moniker-end

- [Deploy and connect to SQL Server Docker containers](sql-server-linux-docker-container-deployment.md)

- [Reference additional configuration and customization to Docker containers](sql-server-linux-docker-container-configure.md)

- [Troubleshooting SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md)
