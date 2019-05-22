---
title: Install SQL Server Machine Language Extensions (Java) on Linux | Microsoft Docs
description: Learn how to install SQL Server Language Extensions (Java) on Red Hat,  Ubuntu, and SUSE.
author: dphansen
ms.author: davidph
manager: cgronlun
ms.date: 05/22/2019
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install SQL Server 2019 Language Extensions (Java) on Linux

[SQL Server Machine Learning Services](../advanced-analytics/what-is-sql-server-machine-learning.md) runs on Linux operating systems starting in this preview release of SQL Server 2019. Follow the steps in this article to install the Java language extension. 

Language Extensions are an add-on to the database engine. Although you can [install the database engine and Language Extensions concurrently](#install-all), it's a best practice to install and configure the SQL Server database engine first so that you can resolve any issues before adding more components. 

Package location for the Java extensions are in the SQL Server Linux source repositories. If you already configured source repositories for the database engine install, you can run the **mssql-server-extensibility-java** package install commands using the same repo registration.

Language Extensions is also supported on Linux containers. We do not provide pre-built containers with Language Extensions, but you can create one from the SQL Server containers using [an example template available on GitHub](https://github.com/Microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-mlservices).

## Uninstall previous CTP

The package list has changed over the last several CTP releases, resulting in fewer packages. We recommend uninstalling CTP 2.x to remove all previous packages before installing CTP 3.0. Side-by-side installation of multiple versions is not supported.

### 1. Confirm package installation

You might want to check for the existence of a previous installation as a first step. The following files indicate an existing installation: checkinstallextensibility.sh, exthost, launchpad.

```bash
ls /opt/microsoft/mssql/bin
```

### 2. Uninstall previous CTP 2.x packages

Uninstall at the lowest package level. Any upstream package dependent on a lower-level package is automatically uninstalled.

  + For Java integration, remove **mssql-server-extensibility-java**

Commands for removing packages appear in the following table.

| Platform	| Package removal command(s) | 
|-----------|----------------------------|
| RHEL	| `sudo yum remove msssql-server-extensibility-java` |
| SLES	| `sudo zypper remove msssql-server-extensibility-java` |
| Ubuntu	| `sudo apt-get remove msssql-server-extensibility-java`|

### 3. Proceed with CTP 3.0 install

Install at the highest package level using the instructions in this article for your operating system.

For each OS-specific set of installation instructions, *highest package level* is either **Example 1 - Full installation** for the full set of packages, or **Example 2 - Minimal installation** for the least number of packages required for a viable installation.

1. Run install commands using the package managers and syntax for your Linux distribution: 

   + [RedHat](#RHEL)
   + [Ubuntu](#ubuntu)
   + [SUSE](#suse)

## Prerequisites

+ The Linux version must be [supported by SQL Server](sql-server-linux-release-notes-2019.md#supported-platforms), but does not include the Docker Engine. Supported versions include:

   + [Red Hat Enterprise Linux (RHEL)](quickstart-install-connect-red-hat.md)

   + [SUSE Enterprise Linux Server](quickstart-install-connect-suse.md)

   + [Ubuntu](quickstart-install-connect-ubuntu.md)

+ You should have a tool for running T-SQL commands. A query editor is necessary for post-install configuration and validation. We recommend [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download?view=sql-server-2017#get-azure-data-studio-for-linux), a free download that runs on Linux.

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system. The following table describes all available packages.

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All languages | Extensibility framework used to run Java code. |
|mssql-server-extensibility-java | Java | Java extension for loading a Java execution environment. There are no additional libraries or packages for Java. |

<a name="RHEL"></a>

## Install Language Extensions

You can install Language Extensions and Java on Linux by installing **mssql-server-extensibility-java**. When you install **mssql-server-extensibility-java**, the package automatically installs JRE 8 if it is not already installed. It will also add the JVM path to an environment variable called JRE_HOME.

> [!Note]
> On an internet-connected server, package dependencies are downloaded and installed as part of the main package installation. If your server is not connected to the internet, see more details in the [offline setup](#offline-install).

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

## Post-install config (required)

1. Grant permissions on Linux

    You don't need to perform this step if you are using external libraries. The recommended way of working is using external libraries. For help creating an external library from your jar file, see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql)

    If you are not using external libraries, you need to provide SQL Server with permissions to execute the Java classes in your jar.

    To grant read and execute access to a jar file, run the following **chmod** command on the jar file. We recommend always putting your class files in a jar when you work with SQL Server. For help creating a jar, see [How to create a jar file](https://docs.microsoft.com/sql/language-extensions/how-to/create-a-java-jar-file-from-class-files).

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

   ```bash
   EXEC sp_configure 'external scripts enabled', 1
   RECONFIGURE WITH OVERRIDE
   ```

6. Restart the `mssql-launchpadd` service again.

## Verify installation

Java feature integration does not include libraries, but you can run `grep -r JRE_HOME /etc` to confirm creation of the JAVA_HOME environment variable.

To validate installation, run a T-SQL script that executes a system stored procedure invoking Java. You will need a query tool for this task. Azure Data Studio is a good choice. Other commonly used tools such as SQL Server Management Studio or PowerShell are Windows-only. If you have a Windows computer with these tools, use it to connect to your Linux installation of the database engine.

<a name="install-all"></a>

## Full install of SQL Server and Language Extensions

You can install and configure the database engine and Machine Learning Services in one procedure by appending Java packages and parameters on a command that installs the database engine.

1. Provide a command line that includes the database engine, plus language extension features.

  You can add Java extensibility to a database engine install.

  ```bash
  sudo yum install -y mssql-server mssql-server-extensibility-java 
  ```

3. Accept license agreements and complete the post-install configuration. Use the **mssql-conf** tool for this task.

  ```bash
  sudo /opt/mssql/bin/mssql-conf setup
  ```

  You will be prompted to accept the license agreement for the database engine, choose an edition, and set the administrator password. 

4. Restart the service, if prompted to do so.

  ```bash
  sudo systemctl restart mssql-server.service
  ```

## Unattended installation

Using the [unattended install](https://docs.microsoft.com/sql/linux/sql-server-linux-setup#unattended) for the Database Engine, add the packages for mssql-server-extensibility-java.

<a name="offline-install"></a>


## Offline installation

Follow the [Offline installation](sql-server-linux-setup.md#offline) instructions for steps on installing the packages. Find your download site, and then download specific packages using the package list below.

> [!Tip]
> Several of the package management tools provide commands that can help you determine package dependencies. For yum, use `sudo yum deplist [package]`. For Ubuntu, use `sudo apt-get install --reinstall --download-only [package name]` followed by `dpkg -I [package name].deb`.

#### Download site

You can download packages from [https://packages.microsoft.com/](https://packages.microsoft.com/). All of the packages for Java are co-located with database engine package. 

#### RedHat/7 paths

|||
|--|----|
| mssql/extensibility-java packages | [https://packages.microsoft.com/rhel/7/mssql-server-preview/](https://packages.microsoft.com/rhel/7/mssql-server-preview/) |

#### Ubuntu/16.04 paths

|||
|--|----|
| mssql/extensibility-java packages | [https://packages.microsoft.com/ubuntu/16.04/mssql-server-preview/pool/main/m/](https://packages.microsoft.com/ubuntu/16.04/mssql-server-preview/pool/main/m/) |

#### SUSE/12 paths

|||
|--|----|
| mssql/extensibility-java packages | [https://packages.microsoft.com/sles/12/mssql-server-preview/](https://packages.microsoft.com/sles/12/mssql-server-preview/) |

#### Package list

Depending on which extensions you want to use, download the packages necessary for a specific language. Exact filenames include platform information in the suffix, but the file names below should be close enough for you to determine which files to get.

```
# Core packages 
mssql-server-15.0.1000
mssql-server-extensibility-15.0.1000

# Java
mssql-server-extensibility-java-15.0.1000
```

## Limitations in CTP releases

Language Extensions and Java extensibility on Linux is still under active development. The following features are not yet enabled in the preview version.

+ Implied authentication is currently not available on Linux at this time, which means you cannot connect back to the server from in-progress Java to access data or other resources.


### Resource governance

There is parity between Linux and Windows for [Resource governance](../t-sql/statements/create-external-resource-pool-transact-sql.md) for external resource pools, but the statistics for [sys.dm_resource_governor_external_resource_pools](../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pools.md) currently have different units on Linux. Units will align in an upcoming CTP.
 
| Column name   | Description | Value on Linux | 
|---------------|--------------|---------------|
|peak_memory_kb | The maximum amount of memory used for the resource pool. | On Linux, this statistic is sourced from the CGroups memory subsystem, where the value is memory.max_usage_in_bytes |
|write_io_count | The total write IOs issued since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups blkio subsystem, where the value on the write row is blkio.throttle.io_serviced | 
|read_io_count | The total read IOs issued since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups blkio subsystem, where value on the read row is blkio.throttle.io_serviced | 
|total_cpu_kernel_ms | The cumulative CPU user kernel time in milliseconds since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups cpuacct subsystem, where the value on the user row is cpuacct.stat |  
|total_cpu_user_ms | The cumulative CPU user time in milliseconds since the Resource Governor statistics were reset.| On Linux, this statistic is sourced from the CGroups cpuacct subsystem, where the value on the system row value is cpuacct.stat | 
|active_processes_count | The number of external processes running at the moment of the request.| On Linux, this statistic is sourced from the GGroups pids subsystem, where the value is pids.current | 

## Next steps

Java developers can get started with some simple examples, and learn the basics of how Java works with SQL Server. For your next step, see the following links:

+ [Tutorial: Regular expressions with Java](../language-extensions/tutorials/search-for-string-using-regular-expressions-in-java.md)