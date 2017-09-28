---
title: Restore a SQL Server database in Docker | Microsoft Docs
description: This tutorial shows how restore a SQL Server database backup in a new Linux Docker container.
author: rothja
ms.author: jroth
manager: jhubbard
ms.date: 10/02/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
---
# Restore a SQL Server database in a Linux Docker container

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

This tutorial demonstrates how to move and restore a SQL Server backup file into a SQL Server 2017 container image running on Docker.

> [!div class="checklist"]
> * Pull and run the latest SQL Server 2017 Linux container image.
> * Copy the World Wide Importers database file into the container.
> * Restore the database in the container.
> * Run Transact-SQL statements to view and modify the database.
> * Backup the modified database.

## Prerequisites

* Docker Engine 1.8+ on any supported Linux distribution or Docker for Mac/Windows. For more information, see [Install Docker](https://docs.docker.com/engine/installation/).
* Minimum of 4 GB of disk space
* Minimum of 4 GB of RAM
* [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

> [!IMPORTANT]
> The default on Docker for Mac and Docker for Windows is 2 GB for the Moby VM, so you must change it to 4 GB. If you are running on Mac or Windows, increase your memory settings using the [instructions in the Docker quickstart](quickstart-install-connect-docker.md).

## Pull and run the container image

1. Open a bash terminal on Linux/Mac or an elevated PowerShell session on Windows.

1. Pull the SQL Server 2017 Linux container image from Docker Hub.

    ```bash
    sudo docker pull microsoft/mssql-server-linux
    ```

    ```PowerShell
    docker pull microsoft/mssql-server-linux
    ```

    > [!TIP]
    > Throughout this tutorial, docker command examples are given for both the bash shell (Linux/Mac) and PowerShell (Windows).

1. To run the container image with Docker, you can use the following command:

    ```bash
    sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Your.StrongPwd1' --name 'sql1' -e 'MSSQL_PID=Developer' -p 1401:1433 -v sql1data:/var/opt/mssql -d microsoft/mssql-server-linux
    ```

    ```PowerShell
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Your.StrongPwd1" --name "sql1" -e "MSSQL_PID=Developer" -p 1401:1433 -v sql1data:/var/opt/mssql -d microsoft/mssql-server-linux
    ```

    This command creates a SQL Server 2017 container with the **Developer** Edition. SQL Server port **1433** is exposed on the host as port **1401**. The optional `-v sql1data:/var/opt/mssql` parameter creates a data volume container named **sql1ddata**. This is used to persist the data created by SQL Server.

1. To view your Docker containers, use the `docker ps` command.

    ```bash
    sudo docker ps -a
    ```

    ```PowerShell
    docker ps -a
    ```
 
1. If the **STATUS** column shows a status of **Up**, then SQL Server is running in the container and listening on the port specified in the **PORTS** column. If the **STATUS** column for your SQL Server container shows **Exited**, see the [Troubleshooting section of the configuration guide](sql-server-linux-configure-docker.md#troubleshooting).

   ```
   $ sudo docker ps -a
   
   CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
   941e1bdf8e1d        microsoft/mssql-server-linux   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour    0.0.0.0:1401->1433/tcp   sql1
   ```

## Change the SA password

[!INCLUDE [Change docker password](../includes/sql-server-linux-change-docker-password.md)]

## Copy the backup file into the container

For this tutorial, 

## Connect external

This means that to connect from outside from the container, you would also need to specify port 1401. Connecting externally is not part of this tutorial, but you can see an example in the [Docker quickstart](quickstart-install-connect-docker.md#connectexternal)

## Next steps

In this tutorial, you learned how to back up a database on Windows and move it to a Linux server running SQL Server 2017 RC2. You learned how to:
> [!div class="checklist"]
> * TBD

Next, explore other migration scenarios for SQL Server on Linux.

> [!div class="nextstepaction"]
>[Configuration guide for SQL Server 2017 on Docker](sql-server-linux-configure-docker.md)

