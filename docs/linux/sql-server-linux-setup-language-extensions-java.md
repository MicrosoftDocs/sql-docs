---
title: Install Java Language Extension on Linux
titleSuffix: SQL Server Language Extensions
description: Learn how to install SQL Server Java Language Extension on Red Hat, Ubuntu, and SUSE Linux.
author: rothja
ms.author: jroth
ms.topic: how-to
ms.service: sql
ms.subservice: language-extensions
ms.custom:
- intro-installation
- event-tier1-build-2022
ms.date: 05/24/2022
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---

# Install SQL Server Java Language Extension on Linux

[!INCLUDE [SQL Server 2019 - Linux](../includes/applies-to-version/sqlserver2019-linux.md)] and later

Learn how to install the [Java Language Extension](../language-extensions/java-overview.md) component for SQL Server on Linux. The Java Language Extension is part of [SQL Server Language Extensions](../language-extensions/language-extensions-overview.md) and an add-on to the database engine. 

Although you can [install the database engine and Language Extensions concurrently](#install-all), it's a best practice to install and configure the SQL Server database engine first so that you can resolve any issues before adding more components.

## Prerequisites

+ The Linux version must be [supported by SQL Server](sql-server-linux-release-notes-2019.md#supported-platforms), but does not include the Docker Engine. Supported versions include:

   + [Red Hat Enterprise Linux (RHEL)](quickstart-install-connect-red-hat.md)
   + [SUSE Enterprise Linux Server](quickstart-install-connect-suse.md)
   + [Ubuntu](quickstart-install-connect-ubuntu.md)

+ You should have a tool for running T-SQL commands. A query editor is necessary for post-install configuration and validation. We recommend [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md?view=sql-server-2017&preserve-view=true#linux-installation), a free download that runs on Linux.

::: monikerRange="=sql-server-linux-ver15"

+ Package location for the Java extensions is in the SQL Server Linux source repositories. If you already configured source repositories for the database engine install, you can run the **mssql-server-extensibility-java** package install commands using the same repo registration.
::: moniker-end
+ Language Extensions is also supported on Linux containers. We do not provide pre-built containers with Language Extensions, but you can create one from the SQL Server containers using [an example template available on GitHub](https://github.com/Microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-mlservices).

+ Language Extensions and [Machine Learning Services](../machine-learning/index.yml) are installed by default on SQL Server Big Data Clusters. If you use Big Data Clusters, you do not need to follow the steps in this article. For more information, see [Use Machine Learning Services (Python and R) on Big Data Clusters](../big-data-cluster/machine-learning-services.md).

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system. The following table describes all available packages.

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All languages | Extensibility framework used for the Java language extension |
|mssql-server-extensibility-java | Java | **Applies to: SQL Server 2019 on Linux only**. Extensibility framework used for the Java language extension and includes a supported Java runtime |

<a name="RHEL"></a>

## Install Java language extension

::: monikerRange="=sql-server-linux-ver15"

You can install Language Extensions and Java on Linux by installing **mssql-server-extensibility-java**. When you install **mssql-server-extensibility-java**, the package automatically installs JRE 11 if it is not already installed. It will also add the JVM path to an environment variable called JRE_HOME.

To enable the Java Language Extension, build a custom binary by following the instructions from the [Java Language Extension page on GitHub](
https://github.com/microsoft/sql-server-language-extensions/tree/main/language-extensions/java).

> [!Note]
> On an internet-connected server, package dependencies are downloaded and installed as part of the main package installation. If your server is not connected to the internet, see more details in the [offline setup](#offline-install).

::: moniker-end

::: monikerRange="=sql-server-linux-ver16"

You can download and install any Java runtime as desired, including the latest [Microsoft Build of OpenJDK](https://www.microsoft.com/openjdk) or officially licensed Java runtime. Starting with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], no Java runtime is installed by SQL Setup.

To enable the Java Language Extension, build a custom binary by following the instructions from the [Java Language Extension page on GitHub](
https://github.com/microsoft/sql-server-language-extensions/tree/main/language-extensions/java).

::: moniker-end

::: monikerRange="=sql-server-linux-ver15"

### RedHat install command

You can install Language Extensions for Java on RedHat using the command below.

> [!Tip]
> If possible, run `yum clean all` to refresh packages on the system prior to installation.

```bash
# Install as root or sudo
sudo yum install mssql-server-extensibility-java
```

<a name="ubuntu"></a>

### Ubuntu install command

You can install Language Extensions for Java on Ubuntu using the command below.

> [!Tip]
> If possible, run `apt-get update` to refresh packages on the system prior to installation. Additionally, some docker images of Ubuntu might not have the https apt transport option. To install it, use `apt-get install apt-transport-https`.

```bash
# Install as root or sudo
sudo apt-get install mssql-server-extensibility-java
```

<a name="suse"></a>

### SUSE install command

You can install Language Extensions for Java on SUSE using the command below.

```bash
# Install as root or sudo
sudo zypper install mssql-server-extensibility-java
```

::: moniker-end

## Post-install config (required)

1. Grant permissions on Linux

    You don't need to perform this step if you are using external libraries. The recommended way of working is using external libraries. For help creating an external library from your jar file, see [CREATE EXTERNAL LIBRARY](../t-sql/statements/create-external-library-transact-sql.md)

    If you are not using external libraries, you need to provide SQL Server with permissions to execute the Java classes in your jar.

    To grant read and execute access to a jar file, run the following **chmod** command on the jar file. We recommend always putting your class files in a jar when you work with SQL Server. For help creating a jar, see [How to create a jar file](../language-extensions/how-to/create-a-java-jar-file-from-class-files.md).

    ```cmd
    chmod ug+rx <MyJarFile.jar>
    ```

    You also need to give mssql_satellite permissions the jar file to read/execute.

    ```cmd
    chown mssql_satellite:mssql_satellite <MyJarFile.jar>
    ```

    Additional configuration is primarily through the [mssql-conf tool](sql-server-linux-configure-mssql-conf.md).

2. Add the mssql user account used to run the SQL Server service. This is required if you haven't run the setup previously.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

3. Enable outbound network access. Outbound network access is disabled by default. To enable outbound requests, set the "outboundnetworkaccess" Boolean property using the mssql-conf tool. For more information, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md#mlservices-outbound-access).

   ```bash
   # Run as SUDO or root
   # Enable outbound requests over the network
   sudo /opt/mssql/bin/mssql-conf set extensibility outboundnetworkaccess 1
   ```

4. Restart the SQL Server Launchpad service and the database engine instance to read the updated values from the INI file. A restart message reminds you whenever an extensibility-related setting is modified.  

   ```bash
   systemctl restart mssql-launchpadd

   systemctl restart mssql-server.service
   ```

5. Enable external script execution using Azure Data Studio or another tool like SQL Server Management Studio (Windows only) that runs Transact-SQL.

   ```sql
   EXEC sp_configure 'external scripts enabled', 1
   RECONFIGURE WITH OVERRIDE
   ```

6. Restart the `mssql-launchpadd` service again.

7. For each database you want to use language extensions in, you need to register the external language with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). See steps in the next section.

## Register external language

For each database you want to use language extensions in, you need to register the external language with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md).

The following example adds an external language called Java to a database on SQL Server on Linux.

```SQL
CREATE EXTERNAL LANGUAGE Java
FROM (CONTENT = N'/opt/mssql-extensibility/lib/java-lang-extension.tar.gz', 
    FILE_NAME = 'javaextension.so', 
    ENVIRONMENT_VARIABLES = N'{"JRE_HOME":"/opt/mssql/lib/zulu-jre-11"}')
```

For the Java extension, the environment variable `JRE_HOME` is used to determine the path to find and initialize the JVM from.

The `CREATE EXTERNAL LANGUAGE` DDL provides a parameter (ENVIRONMENT_VARIABLES) to set environment variables specifically for the process hosting the extension. This is the recommended and most effective way to set environment variables required by external language extensions.

For more information, see [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md).

## Verify installation

Java feature integration does not include libraries, but you can run `grep -r JRE_HOME /etc` to confirm creation of the `JAVA_HOME` environment variable.

To validate installation, run a T-SQL script that executes a system stored procedure invoking Java. You will need a query tool for this task. Azure Data Studio is a good choice. Other commonly used tools such as SQL Server Management Studio or PowerShell are Windows-only. If you have a Windows computer with these tools, use it to connect to your Linux installation of the database engine.


::: monikerRange="=sql-server-linux-ver15"

<a name="install-all"></a>

## Full install of SQL Server and Java Language Extension

You can install and configure the database engine and Java Language Extension in one procedure by appending Java packages and parameters on a command that installs the database engine.

1. Provide a command line that includes the database engine, plus language extension features.

    You can add Java extensibility to a database engine install.

    ```bash
    sudo yum install -y mssql-server mssql-server-extensibility-java 
    ```

1. Accept license agreements and complete the post-install configuration. Use the **mssql-conf** tool for this task.

    ```bash
    sudo /opt/mssql/bin/mssql-conf setup
    ```

    You will be prompted to accept the license agreement for the database engine, choose an edition, and set the administrator password. 

1. Restart the service, if prompted to do so.

    ```bash
    sudo systemctl restart mssql-server.service
    ```

## Unattended installation

Use the [unattended install](./sql-server-linux-setup.md#unattended) for the Database Engine and add the packages for **mssql-server-extensibility-java**.

<a name="offline-install"></a>

## Offline installation

Follow the [Offline installation](sql-server-linux-setup.md#offline) instructions for steps on installing the packages. Find your download site, and then download specific packages using the package list below.

> [!Tip]
> Several of the package management tools provide commands that can help you determine package dependencies. For yum, use `sudo yum deplist [package]`. For Ubuntu, use `sudo apt-get install --reinstall --download-only [package name]` followed by `dpkg -I [package name].deb`.

#### Download site

You can download the packages from [https://packages.microsoft.com/](https://packages.microsoft.com/). All of the packages for Java are colocated with the database engine package.

#### RedHat/7 paths

|Package|Download location|
|--|----|
| mssql/extensibility-java packages | [https://packages.microsoft.com/rhel/7/mssql-server-2019/](https://packages.microsoft.com/rhel/7/mssql-server-2019/) |

#### Ubuntu/16.04 paths

|Package|Download location|
|--|----|
| mssql/extensibility-java packages | [https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/) |

#### SUSE/12 paths

|Package|Download location|
|--|----|
| mssql/extensibility-java packages | [https://packages.microsoft.com/sles/12/mssql-server-2019/](https://packages.microsoft.com/sles/12/mssql-server-2019/) |

#### Package list
Depending on which extensions you want to use, download the packages necessary for a specific language. Exact filenames include platform information in the suffix, but the file names below should be close enough for you to determine which files to get.

```
# Core packages
mssql-server-15.0.1000
mssql-server-extensibility-15.0.1000

# Java
mssql-server-extensibility-java-15.0.1000
```

::: moniker-end

## Limitations

Implied authentication is currently not available on Linux at this time, which means you cannot connect back to the server from in-progress Java to access data or other resources.

### Resource governance

There is parity between Linux and Windows for [Resource governance](../t-sql/statements/create-external-resource-pool-transact-sql.md) for external resource pools, but the statistics for [sys.dm_resource_governor_external_resource_pools](../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pools.md) currently have different units on Linux.

| Column name   | Description | Value on Linux |
|---------------|--------------|---------------|
|peak_memory_kb | The maximum amount of memory used for the resource pool. | On Linux, this statistic is sourced from the CGroups memory subsystem, where the value is `memory.max_usage_in_bytes` |
|write_io_count | The total write IOs issued since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups blkio subsystem, where the value on the write row is `blkio.throttle.io_serviced` |
|read_io_count | The total read IOs issued since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups blkio subsystem, where value on the read row is `blkio.throttle.io_serviced` |
|total_cpu_kernel_ms | The cumulative CPU user kernel time in milliseconds since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups cpuacct subsystem, where the value on the user row is `cpuacct.stat` |  
|total_cpu_user_ms | The cumulative CPU user time in milliseconds since the Resource Governor statistics were reset.| On Linux, this statistic is sourced from the CGroups cpuacct subsystem, where the value on the system row value is `cpuacct.stat` |
|active_processes_count | The number of external processes running at the moment of the request.| On Linux, this statistic is sourced from the CGroups pids subsystem, where the value is `pids.current` |

## Next steps

Java developers can get started with some simple examples, and learn the basics of how Java works with SQL Server. For your next step, see the following links:

+ [Tutorial: Regular expressions with Java](../language-extensions/tutorials/search-for-string-using-regular-expressions-in-java.md)
