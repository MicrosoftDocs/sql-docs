---
title: Restore a SQL Server Database in a Linux Container
description: This tutorial shows how to restore a SQL Server database backup in a new Linux container.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - intro-migration
  - linux-related-content
  - sfi-ropc-blocked
  - ignite-2025
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017"
---
# Restore a SQL Server database in a Linux container

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux -->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

This tutorial demonstrates how to move and restore a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backup file into a [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Linux container image running on Docker.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

This tutorial demonstrates how to move and restore a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backup file into a [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] Linux container image running on Docker.

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

This tutorial demonstrates how to move and restore a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backup file into a [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Linux container image running on Docker.

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

This tutorial demonstrates how to move and restore a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backup file into a [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Linux container image running on Docker.

::: moniker-end

> [!div class="checklist"]
> - Pull and run the latest [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Linux container image.
> - Copy the Wide World Importers database backup file into the container.
> - Restore the database in the container.
> - Run Transact-SQL (T-SQL) statements to view and modify the database.
> - Back up the modified database.

## Prerequisites

- A container runtime installed, such as [Docker](https://www.docker.com/) or [Podman](https://podman.io/)
- Install the latest **[sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md?tabs=go%2Clinux&pivots=cs1-bash)**
- [System requirements for SQL Server on Linux](../install-upgrade/setup.md#system)

## Deployment options

This section provides deployment options for your environment.

**`sqlcmd`** doesn't currently support the `MSSQL_PID` parameter when creating containers. If you use the **`sqlcmd`** instructions in this tutorial, you create a container with the Developer edition of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Use the command line interface (CLI) instructions to create a container using the license of your choice. For more information, see [Deploy and connect to SQL Server Linux containers](../containers/deploy.md).

## [CLI](#tab/cli)

### Pull and run the container image

<!--SQL Server 2017 on Linux -->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

1. Open a bash terminal on Linux.

1. Pull the [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Linux container image from the Microsoft Container Registry.

   ```bash
   sudo docker pull mcr.microsoft.com/mssql/server:2017-latest
   ```

1. To run the container image with Docker, you can use the following command:

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql1' -p 1401:1433 \
      -v sql1data:/var/opt/mssql \
      -d mcr.microsoft.com/mssql/server:2017-latest
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../includes/password-complexity.md)]

   This command creates a [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] container with the Developer edition (default). [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] port `1433` is exposed on the host as port `1401`. The optional `-v sql1data:/var/opt/mssql` parameter creates a data volume container named `sql1data`. This is used to persist the data created by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

   > [!IMPORTANT]  
   > This example uses a data volume container within Docker. For more information, see [Configure SQL Server container images on Docker](../containers/configure.md#persist).

1. To view your containers, use the `docker ps` command.

   ```bash
   sudo docker ps -a
   ```

1. If the `STATUS` column shows a status of `Up`, then [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is running in the container and listening on the port specified in the `PORTS` column. If the `STATUS` column for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container shows `Exited`, see [Troubleshoot SQL Server Docker containers](../containers/troubleshoot.md).

   ```bash
   $ sudo docker ps -a

   CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
   941e1bdf8e1d        mcr.microsoft.com/mssql/server:2017-latest   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour    0.0.0.0:1401->1433/tcp   sql1
   ```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

1. Open a bash terminal on Linux.

1. Pull the [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] Linux container image from the Microsoft Container Registry.

   ```bash
   sudo docker pull mcr.microsoft.com/mssql/server:2019-latest
   ```

1. To run the container image with Docker, you can use the following command:

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql1' -p 1401:1433 \
      -v sql1data:/var/opt/mssql \
      -d mcr.microsoft.com/mssql/server:2019-latest
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../includes/password-complexity.md)]

   This command creates a [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] container with the Developer edition (default). [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] port `1433` is exposed on the host as port `1401`. The optional `-v sql1data:/var/opt/mssql` parameter creates a data volume container named `sql1data`. This is used to persist the data created by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

   > [!IMPORTANT]  
   > This example uses a data volume container within Docker. For more information, see [Configure SQL Server container images on Docker](../containers/configure.md#persist).

1. To view your containers, use the `docker ps` command.

   ```bash
   sudo docker ps -a
   ```

1. If the `STATUS` column shows a status of `Up`, then [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is running in the container and listening on the port specified in the `PORTS` column. If the `STATUS` column for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container shows `Exited`, see [Troubleshoot SQL Server Docker containers](../containers/troubleshoot.md).

   ```bash
   $ sudo docker ps -a

   CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
   941e1bdf8e1d        mcr.microsoft.com/mssql/server:2019-latest   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour    0.0.0.0:1401->1433/tcp   sql1
   ```

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

1. Open a bash terminal on Linux.

1. Pull the [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Linux container image from the Microsoft Container Registry.

   ```bash
   sudo docker pull mcr.microsoft.com/mssql/server:2022-latest
   ```

1. To run the container image with Docker, you can use the following command:

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql1' -p 1401:1433 \
      -v sql1data:/var/opt/mssql \
      -d mcr.microsoft.com/mssql/server:2022-latest
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../includes/password-complexity.md)]

   This command creates a [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] container with the Developer edition (default). [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] port `1433` is exposed on the host as port `1401`. The optional `-v sql1data:/var/opt/mssql` parameter creates a data volume container named `sql1data`. This is used to persist the data created by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

   > [!IMPORTANT]  
   > This example uses a data volume container within Docker. For more information, see [Configure SQL Server container images on Docker](../containers/configure.md#persist).

1. To view your containers, use the `docker ps` command.

   ```bash
   sudo docker ps -a
   ```

1. If the `STATUS` column shows a status of `Up`, then [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is running in the container and listening on the port specified in the `PORTS` column. If the `STATUS` column for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container shows `Exited`, see [Troubleshoot SQL Server Docker containers](../containers/troubleshoot.md).

   ```bash
   $ sudo docker ps -a

   CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
   941e1bdf8e1d        mcr.microsoft.com/mssql/server:2022-latest   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour    0.0.0.0:1401->1433/tcp   sql1
   ```

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

1. Open a bash terminal on Linux.

1. Pull the [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Linux container image from the Microsoft Container Registry.

   ```bash
   sudo docker pull mcr.microsoft.com/mssql/server:2025-latest
   ```

1. To run the container image with Docker, you can use the following command:

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql1' -p 1401:1433 \
      -v sql1data:/var/opt/mssql \
      -d mcr.microsoft.com/mssql/server:2025-latest
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../includes/password-complexity.md)]

   This command creates a [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] container with the Developer edition (default). [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] port `1433` is exposed on the host as port `1401`. The optional `-v sql1data:/var/opt/mssql` parameter creates a data volume container named `sql1data`. This is used to persist the data created by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

   > [!IMPORTANT]  
   > This example uses a data volume container within Docker. For more information, see [Configure SQL Server container images on Docker](../containers/configure.md#persist).

1. To view your containers, use the `docker ps` command.

   ```bash
   sudo docker ps -a
   ```

1. If the `STATUS` column shows a status of `Up`, then [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is running in the container and listening on the port specified in the `PORTS` column. If the `STATUS` column for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container shows `Exited`, see [Troubleshoot SQL Server Docker containers](../containers/troubleshoot.md).

   ```bash
   $ sudo docker ps -a

   CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
   941e1bdf8e1d        mcr.microsoft.com/mssql/server:2025-latest   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour    0.0.0.0:1401->1433/tcp   sql1
   ```

::: moniker-end

### Change the system administrator (SA) password

[!INCLUDE [change-docker-password](../includes/change-docker-password.md)]

### Copy a backup file into the container

This tutorial uses the [Wide World Importers sample databases for Microsoft SQL](../../samples/wide-world-importers-what-is.md). Use the following steps to download and copy the Wide World Importers database backup file into your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container.

1. First, use `docker exec` to create a backup folder. The following command creates a `/var/opt/mssql/backup` directory inside the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container.

   ```bash
   sudo docker exec -it sql1 mkdir /var/opt/mssql/backup
   ```

1. Next, download the [WideWorldImporters-Full.bak](https://github.com/Microsoft/sql-server-samples/releases/tag/wide-world-importers-v1.0) file to your host machine. The following commands navigate to the home/user directory and download the backup file as `wwi.bak`.

   ```bash
   cd ~
   curl -L -o wwi.bak 'https://github.com/Microsoft/sql-server-samples/releases/download/wide-world-importers-v1.0/WideWorldImporters-Full.bak'
   ```

1. Use `docker cp` to copy the backup file into the container in the `/var/opt/mssql/backup` directory.

   ```bash
   sudo docker cp wwi.bak sql1:/var/opt/mssql/backup
   ```

### Restore the database

The backup file is now located inside the container. Before restoring the backup, it's important to know the logical file names and file types inside the backup. The following T-SQL commands inspect the backup and perform the restore using **`sqlcmd`** in the container.

> [!TIP]  
> [!INCLUDE [connect-instance-client](../../includes/connect-instance-client.md)] This tutorial uses **`sqlcmd`** inside the container. To connect, use the host port that was mapped to port 1433 in the container. In this example, the host and port are `localhost,1401` on the host machine, and `Host_IP_Address,1401` remotely.

1. Run **`sqlcmd`** inside the container to list out logical file names and paths inside the backup. This is done with the `RESTORE FILELISTONLY` T-SQL statement.

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools18/bin/sqlcmd -S localhost \
      -U sa -P '<new-password>' \
      -Q "RESTORE FILELISTONLY FROM DISK = '/var/opt/mssql/backup/wwi.bak'" \
      | tr -s ' ' | cut -d ' ' -f 1-2
   ```

   The results should look similar to the following output:

   ```output
   LogicalName   PhysicalName
   ------------------------------------------
   WWI_Primary   D:\Data\WideWorldImporters.mdf
   WWI_UserData   D:\Data\WideWorldImporters_UserData.ndf
   WWI_Log   E:\Log\WideWorldImporters.ldf
   WWI_InMemory_Data_1   D:\Data\WideWorldImporters_InMemory_Data_1
   ```

1. Call the `RESTORE DATABASE` command to restore the database inside the container. Specify new paths for each of the files in the previous step.

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q "RESTORE DATABASE WideWorldImporters FROM DISK = '/var/opt/mssql/backup/wwi.bak' WITH MOVE 'WWI_Primary' TO '/var/opt/mssql/data/WideWorldImporters.mdf', MOVE 'WWI_UserData' TO '/var/opt/mssql/data/WideWorldImporters_userdata.ndf', MOVE 'WWI_Log' TO '/var/opt/mssql/data/WideWorldImporters.ldf', MOVE 'WWI_InMemory_Data_1' TO '/var/opt/mssql/data/WideWorldImporters_InMemory_Data_1'"
   ```

   The results should look similar to the following output:

   ```output
   Processed 1464 pages for database 'WideWorldImporters', file 'WWI_Primary' on file 1.
   Processed 53096 pages for database 'WideWorldImporters', file 'WWI_UserData' on file 1.
   Processed 33 pages for database 'WideWorldImporters', file 'WWI_Log' on file 1.
   Processed 3862 pages for database 'WideWorldImporters', file 'WWI_InMemory_Data_1' on file 1.
   Converting database 'WideWorldImporters' from version 852 to the current version 869.
   Database 'WideWorldImporters' running the upgrade step from version 852 to version 853.
   Database 'WideWorldImporters' running the upgrade step from version 853 to version 854.
   Database 'WideWorldImporters' running the upgrade step from version 854 to version 855.
   Database 'WideWorldImporters' running the upgrade step from version 855 to version 856.
   Database 'WideWorldImporters' running the upgrade step from version 856 to version 857.
   Database 'WideWorldImporters' running the upgrade step from version 857 to version 858.
   Database 'WideWorldImporters' running the upgrade step from version 858 to version 859.
   Database 'WideWorldImporters' running the upgrade step from version 859 to version 860.
   Database 'WideWorldImporters' running the upgrade step from version 860 to version 861.
   Database 'WideWorldImporters' running the upgrade step from version 861 to version 862.
   Database 'WideWorldImporters' running the upgrade step from version 862 to version 863.
   Database 'WideWorldImporters' running the upgrade step from version 863 to version 864.
   Database 'WideWorldImporters' running the upgrade step from version 864 to version 865.
   Database 'WideWorldImporters' running the upgrade step from version 865 to version 866.
   Database 'WideWorldImporters' running the upgrade step from version 866 to version 867.
   Database 'WideWorldImporters' running the upgrade step from version 867 to version 868.
   Database 'WideWorldImporters' running the upgrade step from version 868 to version 869.
   RESTORE DATABASE successfully processed 58455 pages in 18.069 seconds (25.273 MB/sec).
   ```

### Verify the restored database

Run the following query to display a list of database names in your container:

```bash
sudo docker exec -it sql1 /opt/mssql-tools18/bin/sqlcmd \
   -S localhost -U sa -P '<new-password>' \
   -Q 'SELECT name FROM sys.databases'
```

You should see `WideWorldImporters` in the list of databases.

### Make a change

Follow these steps to make a change in the database.

1. Run a query to view the top 10 items in the `Warehouse.StockItems` table.

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q 'SELECT TOP 10 StockItemID, StockItemName FROM WideWorldImporters.Warehouse.StockItems ORDER BY StockItemID'
   ```

   You should see a list of item identifiers and names:

   ```output
   StockItemID StockItemName
   ----------- -----------------
             1 USB missile launcher (Green)
             2 USB rocket launcher (Gray)
             3 Office cube periscope (Black)
             4 USB food flash drive - sushi roll
             5 USB food flash drive - hamburger
             6 USB food flash drive - hot dog
             7 USB food flash drive - pizza slice
             8 USB food flash drive - dim sum 10 drive variety pack
             9 USB food flash drive - banana
            10 USB food flash drive - chocolate bar
   ```

1. Update the name of the first item with the following `UPDATE` statement:

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q "UPDATE WideWorldImporters.Warehouse.StockItems SET StockItemName='USB missile launcher (Dark Green)' WHERE StockItemID=1; SELECT StockItemID, StockItemName FROM WideWorldImporters.Warehouse.StockItems WHERE StockItemID=1"
   ```

   You should see an output similar to the following text:

   ```output
   StockItemID StockItemName
   ----------- ------------------------------------
             1 USB missile launcher (Dark Green)
   ```

### Create a new backup

After you restore your database into a container, you might also want to regularly create database backups inside the running container. The steps follow a similar pattern to the previous steps but in reverse.

1. Use the `BACKUP DATABASE` T-SQL command to create a database backup in the container. This tutorial creates a new backup file, `wwi_2.bak`, in the previously created `/var/opt/mssql/backup` directory.

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q "BACKUP DATABASE [WideWorldImporters] TO DISK = N'/var/opt/mssql/backup/wwi_2.bak' WITH NOFORMAT, NOINIT, NAME = 'WideWorldImporters-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
   ```

   The results should look similar to the following output:

   ```output
   10 percent processed.
   20 percent processed.
   30 percent processed.
   40 percent processed.
   50 percent processed.
   60 percent processed.
   70 percent processed.
   Processed 1200 pages for database 'WideWorldImporters', file 'WWI_Primary' on file 1.
   Processed 53096 pages for database 'WideWorldImporters', file 'WWI_UserData' on file 1.
   80 percent processed.
   Processed 3865 pages for database 'WideWorldImporters', file 'WWI_InMemory_Data_1' on file 1.
   Processed 938 pages for database 'WideWorldImporters', file 'WWI_Log' on file 1.
   100 percent processed.
   BACKUP DATABASE successfully processed 59099 pages in 25.056 seconds (18.427 MB/sec).
   ```

1. Next, copy the backup file out of the container and onto your host machine.

   ```bash
   cd ~
   sudo docker cp sql1:/var/opt/mssql/backup/wwi_2.bak wwi_2.bak
   ls -l wwi*
   ```

### Use the persisted data

In addition to taking database backups for protecting your data, you can also use data volume containers. The beginning of this tutorial created the `sql1` container with the `-v sql1data:/var/opt/mssql` parameter. The `sql1data` data volume container persists the `/var/opt/mssql` data even after the container is removed. The following steps completely remove the `sql1` container and then create a new container, `sql2`, with the persisted data.

<!--SQL Server 2017 on Linux -->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

1. Stop the `sql1` container.

   ```bash
   sudo docker stop sql1
   ```

1. Remove the container. This doesn't delete the previously created `sql1data` data volume container and the persisted data in it.

   ```bash
   sudo docker rm sql1
   ```

1. Create a new container, `sql2`, and reuse the `sql1data` data volume container.

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql2' -e 'MSSQL_PID=Developer' -p 1401:1433 \
      -v sql1data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
   ```

1. The Wide World Importers database is now in the new container. Run a query to verify the previous change you made.

   ```bash
   sudo docker exec -it sql2 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q 'SELECT StockItemID, StockItemName FROM WideWorldImporters.Warehouse.StockItems WHERE StockItemID=1'
   ```

   The `sa` password isn't the password you specified for the `sql2` container, `MSSQL_SA_PASSWORD=<password>`. All of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data was restored from `sql1`, including the changed password from earlier in the tutorial. In effect, some options like this are ignored due to restoring the data in `/var/opt/mssql`. For this reason, the password is `<new-password>` as shown here.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

1. Stop the `sql1` container.

   ```bash
   sudo docker stop sql1
   ```

1. Remove the container. This doesn't delete the previously created `sql1data` data volume container and the persisted data in it.

   ```bash
   sudo docker rm sql1
   ```

1. Create a new container, `sql2`, and reuse the `sql1data` data volume container.

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql2' -e 'MSSQL_PID=Developer' -p 1401:1433 \
      -v sql1data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest
   ```

1. The Wide World Importers database is now in the new container. Run a query to verify the previous change you made.

   ```bash
   sudo docker exec -it sql2 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q 'SELECT StockItemID, StockItemName FROM WideWorldImporters.Warehouse.StockItems WHERE StockItemID=1'
   ```

   The `sa` password isn't the password you specified for the `sql2` container, `MSSQL_SA_PASSWORD=<password>`. All of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data was restored from `sql1`, including the changed password from earlier in the tutorial. In effect, some options like this are ignored due to restoring the data in `/var/opt/mssql`. For this reason, the password is `<new-password>` as shown here.

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

1. Stop the `sql1` container.

   ```bash
   sudo docker stop sql1
   ```

1. Remove the container. This doesn't delete the previously created `sql1data` data volume container and the persisted data in it.

   ```bash
   sudo docker rm sql1
   ```

1. Create a new container, `sql2`, and reuse the `sql1data` data volume container.

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql2' -e 'MSSQL_PID=Developer' -p 1401:1433 \
      -v sql1data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest
   ```

1. The Wide World Importers database is now in the new container. Run a query to verify the previous change you made.

   ```bash
   sudo docker exec -it sql2 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q 'SELECT StockItemID, StockItemName FROM WideWorldImporters.Warehouse.StockItems WHERE StockItemID=1'
   ```

   The `sa` password isn't the password you specified for the `sql2` container, `MSSQL_SA_PASSWORD=<password>`. All of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data was restored from `sql1`, including the changed password from earlier in the tutorial. In effect, some options like this are ignored due to restoring the data in `/var/opt/mssql`. For this reason, the password is `<new-password>` as shown here.

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

1. Stop the `sql1` container.

   ```bash
   sudo docker stop sql1
   ```

1. Remove the container. This doesn't delete the previously created `sql1data` data volume container and the persisted data in it.

   ```bash
   sudo docker rm sql1
   ```

1. Create a new container, `sql2`, and reuse the `sql1data` data volume container.

   ```bash
   sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' \
      --name 'sql2' -e 'MSSQL_PID=Developer' -p 1401:1433 \
      -v sql1data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2025-latest
   ```

1. The Wide World Importers database is now in the new container. Run a query to verify the previous change you made.

   ```bash
   sudo docker exec -it sql2 /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P '<new-password>' \
      -Q 'SELECT StockItemID, StockItemName FROM WideWorldImporters.Warehouse.StockItems WHERE StockItemID=1'
   ```

   The `sa` password isn't the password you specified for the `sql2` container, `MSSQL_SA_PASSWORD=<password>`. All of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data was restored from `sql1`, including the changed password from earlier in the tutorial. In effect, some options like this are ignored due to restoring the data in `/var/opt/mssql`. For this reason, the password is `<new-password>` as shown here.

::: moniker-end

> [!CAUTION]  
> [!INCLUDE [password-complexity](../includes/password-complexity.md)]

## [sqlcmd](#tab/sqlcmd)

### Create a container and restore a database

You can use a single command in **`sqlcmd`** (Go) to create a new container, and restore a database to that container to create a new local copy of a database, for development or testing. For more information, see [Create and query a SQL Server container](../../tools/sqlcmd/sqlcmd-use-utility.md#create-and-query-a-sql-server-container).

[!INCLUDE [sqlcmd-create-container](../../includes/paragraph-content/sqlcmd-create-container.md)]

### Make a change

Follow these steps to make a change in the database.

1. Run a query to view the top 10 items in the `Warehouse.StockItems` table.

   ```bash
   sqlcmd -Q "SELECT TOP 10 StockItemID, StockItemName FROM Warehouse.StockItems ORDER BY StockItemID"
   ```

   You should see a list of item identifiers and names:

   ```output
   StockItemID StockItemName
   ----------- -----------------
             1 USB missile launcher (Green)
             2 USB rocket launcher (Gray)
             3 Office cube periscope (Black)
             4 USB food flash drive - sushi roll
             5 USB food flash drive - hamburger
             6 USB food flash drive - hot dog
             7 USB food flash drive - pizza slice
             8 USB food flash drive - dim sum 10 drive variety pack
             9 USB food flash drive - banana
            10 USB food flash drive - chocolate bar
   ```

1. Update the name of the first item with the following `UPDATE` statement:

   ```bash
   sqlcmd -Q "UPDATE Warehouse.StockItems SET StockItemName='USB missile launcher (Dark Green)' WHERE StockItemID=1; SELECT StockItemID, StockItemName FROM Warehouse.StockItems WHERE StockItemID=1"
   ```

   You should see an output similar to the following text:

   ```output
   StockItemID StockItemName
   ----------- ------------------------------------
             1 USB missile launcher (Dark Green)
   ```

### Create a new backup

After you restore your database into a container, you might also want to regularly create database backups inside the running container. The steps follow a similar pattern to the previous steps but in reverse.

1. Use the `BACKUP DATABASE` T-SQL command to create a database backup in the container. This tutorial creates a new backup file, `wwi_2.bak`, in the `/var/opt/mssql/backup` directory.

   ```bash
   sqlcmd -Q "BACKUP DATABASE [WideWorldImporters-Full] TO DISK = N'/var/opt/mssql/backup/wwi_2.bak' WITH NOFORMAT, NOINIT, NAME = 'WideWorldImporters-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
   ```

   The results should look similar to the following output:

   ```output
   10 percent processed.
   20 percent processed.
   Processed 1536 pages for database 'WideWorldImporters-Full', file 'WWI_Primary' on file 1.
   Processed 53112 pages for database 'WideWorldImporters-Full', file 'WWI_UserData' on file 1.
   Processed 3865 pages for database 'WideWorldImporters-Full', file 'WWI_InMemory_Data_1' on file 1.
   Processed 287 pages for database 'WideWorldImporters-Full', file 'WWI_Log' on file 1.
   100 percent processed.
   BACKUP DATABASE successfully processed 58800 pages in 3.536 seconds (129.913 MB/sec).
   ```

1. Next, copy the backup file out of the container and onto your host machine.

   ```bash
   cd ~
   sudo docker cp sql1:/var/opt/mssql/backup/wwi_2.bak wwi_2.bak
   ls -l wwi*
   ```

### Clean up

Now that the backup is copied off the container, it can be cleaned up. The following steps completely remove the `sql1` container.

1. Remove the container. **`sqlcmd`** has built-in safeguards to prevent deleting a container that is in use. The way it determines if a container is still in use is whether it has any user databases. For production scenarios, you should delete user databases individually after verifying they're no longer in use. For development/testing, you can use the `--force` parameter to delete the container without deleting the user database.

   ```bash
   sqlcmd delete --force
   ```

---

## Next step

<!--SQL Server 2017 on Linux -->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

In this tutorial, you learned how to restore a database backup in a Linux container running [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. You learned how to:

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

In this tutorial, you learned how to restore a database backup in a Linux container running [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. You learned how to:

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

In this tutorial, you learned how to restore a database backup in a Linux container running [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. You learned how to:

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

In this tutorial, you learned how to restore a database backup in a Linux container running [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. You learned how to:

::: moniker-end

> [!div class="checklist"]
> - Create [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Linux containers.
> - Copy [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] database backups into a container.
> - Run T-SQL statements with **`sqlcmd`**.
> - Create and extract backup files from a container.
> - Use data volume containers to persist [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] production data.

Next, review other container configuration and troubleshooting scenarios:

> [!div class="nextstepaction"]
> [Deploy and connect to SQL Server Linux containers](../containers/deploy.md)

[!INCLUDE [contribute-to-content](../../includes/paragraph-content/contribute-to-content.md)]
