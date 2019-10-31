---
title: Run SQL Server Machine Learning Services in a container | Microsoft Docs
description: This tutorial shows you how to use SQL Server Machine Learning Services in a Linux container running on Docker.
author: uc-msft
ms.author: umajay
ms.date: 06/26/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.collection: linux-container
monikerRange: ">= sql-server-linux-ver15  || >= sql-server-ver15 || = sqlallproducts-allversions"
---
# Run SQL Server Machine Learning Services in a container

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This tutorial demonstrates how to build a Docker container by using SQL Server Machine Learning Services, and how to run Machine Learning scripts from Transact-SQL. Coverage includes the following tasks:

> [!div class="checklist"]
> * Clone the mssql-docker repository.
> * Build a SQL Server Linux container image with Machine Learning Services.
> * Run the SQL Server Linux container image with Machine Learning Services.
> * Run R or Python scripts by using Transact-SQL statements.
> * Stop and remove the SQL Server Linux container.

## Prerequisites

* Git command-line interface.
* Docker Engine 1.8+ on any supported Linux distribution, or Docker for Mac/Windows. For more information, see [Install Docker](https://docs.docker.com/engine/installation/).
* Minimum of 2 gigabytes (GB) of disk space.
* Minimum of 2 GB of RAM.
* [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## Clone the mssql-docker repository

1. Open a Bash terminal on Linux or Mac, or open a WSL terminal on Windows.

1. Create a local directory to hold a local copy of the mssql-docker repository.
1. Run the git clone command to clone the mssql-docker repository:

    ```bash
    git clone https://github.com/microsoft/mssql-docker mssql-docker
    ```

## Build a SQL Server Linux container image with Machine Learning Services

1. Change the directory to the mssql-mlservices directory:

    ```bash
    cd mssql-docker/linux/preview/examples/mssql-mlservices
    ```

1. Run the build.sh script:

   ```bash
   ./build.sh
   ```

   > [!NOTE]
   > To build the Docker image, you must install packages that are several GBs in size. The script may take up to 20 minutes to finish running, depending on network bandwidth.

## Run the SQL Server Linux container image with Machine Learning Services

1. Set your environment variables before running the container. Set the PATH_TO_MSSQL environment variable to a host directory:

   ```bash
    export MSSQL_PID='Developer'
    export ACCEPT_EULA='Y'
    export ACCEPT_EULA_ML='Y'
    export PATH_TO_MSSQL='/home/mssql/'
   ```

1. Run the run.sh script:

   ```bash
   ./run.sh
   ```

   This command creates a SQL Server container with Machine Learning Services, using the Developer edition (default). SQL Server port **1433** is exposed on the host as port **1401**.

   > [!NOTE]
   > The process for running production SQL Server editions in containers is slightly different. For more information, see [Configure SQL Server container images on Docker](sql-server-linux-configure-docker.md). If you use the same container names and ports, the rest of this walk-through still works with production containers.

1. To view your Docker containers, run the `docker ps` command:

   ```bash
   sudo docker ps -a
   ```

1. If the **STATUS** column shows a status of **Up**, SQL Server is running in the container and listening on the port specified in the **PORTS** column. If the **STATUS** column for your SQL Server container shows **Exited**, see the [Troubleshooting section of the configuration guide](sql-server-linux-configure-docker.md#troubleshooting).

   ```bash
   $ sudo docker ps -a
   ```

    Output: 
    
    ```
    CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
    941e1bdf8e1d        mcr.microsoft.com/mssql/server/mssql-server-linux   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour     0.0.0.0:1401->1433/tcp   sql1
    ```

## Change the SA password

[!INCLUDE [Change docker password](../includes/sql-server-linux-change-docker-password.md)]

## Execute R / Python scripts from Transact-SQL

1. Connect to SQL Server in the container, and enable the external script configuration option by running the following T-SQL statement:

    ```sql
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    go
    ```

1. Verify that Machine Learning Services is working by running the following simple R/Python sp_execute_external_script:

    ```sql
    execute sp_execute_external_script 
    @language = N'R',
    @script = N'
    print("Hello World!")
    print(R.version)
    print(Revo.version)
    OutputDataSet <- InputDataSet', 
    @input_data_1 = N'select 1'
    with result sets((i int));
    go
    ```

    ```sql
    execute sp_execute_external_script 
    @language = N'Python',
    @script = N'
    import sys
    print(sys.version)
    print("Hello World!")
    OutputDataSet = InputDataSet',
    @input_data_1 = N'select 1'
    with result sets((i int));
    go 
    ```

## Next steps

In this tutorial, you learned to do the following:

> [!div class="checklist"]
> * Clone the mssql-docker repository
> * Build SQL Server Linux container image with Machine Learning Services
> * Run SQL Server Linux container image with Machine Learning Services
> * Run R or Python scripts using Transact-SQL statements
> * Stop and remove the SQL Server Linux container

Next, review other Docker configuration and troubleshooting scenarios:

> [!div class="nextstepaction"]
>[Configuration guide for SQL Server on Docker](sql-server-linux-configure-docker.md)
