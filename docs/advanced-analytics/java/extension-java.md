---
title: Java language extension in SQL Server 2019 - SQL Server Machine Learning Services
description: Install, configure, and validate the Java language extension on SQL Server 2019 for both Linux and Windows systems.
ms.prod: sql
ms.technology: machine-learning

ms.date: 02/21/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Java language extension in SQL Server 2019 

Starting in SQL Server 2019 preview on both Windows and Linux, you can run custom Java code in the [extensibility framework](../concepts/extensibility-framework.md) as an add-on to the database engine instance. 

The extensibility framework is an architecture for executing external code: Java (starting in SQL Server 2019), [Python (starting in SQL Server 2017)](../concepts/extension-python.md), and [R (starting in SQL Server 2016)](../concepts/extension-r.md). Code execution is isolated from core engine processes, but fully integrated with SQL Server query execution. This means that you can push data from any SQL Server query to the external runtime, and consume or persist results back in SQL Server.

As with any programming language extension, the system stored procedure [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) is the interface for executing pre-compiled Java code.

<a name="prerequisites"></a>

## Prerequisites

A SQL Server 2019 preview instance is required. Earlier versions do not have Java integration.

Java 8 is supported. The Java Runtime Environment (JRE) is the minimum requirement, but JDKs are useful if you need the Java compiler or development packages. Because the JDK is all inclusive, if you install the JDK, the JRE is not necessary.

You can use your preferred Java 8 distribution. Below are two suggested distributions:

| Distribution | Java version | Operating systems | JDK | JRE |
|-|-|-|-|-|
| [Oracle Java SE](https://www.oracle.com/technetwork/java/javase/downloads/index.html) | 8 | Windows and Linux | Yes | Yes |
| [Zulu OpenJDK](https://www.azul.com/downloads/zulu/) | 8 | Windows and Linux | Yes | No |

On Linux, the **mssql-server-extensibility-java** package automatically installs JRE 8 if it is not already installed. Installation scripts also add the JVM path to an environment variable called JAVA_HOME.

On Windows, we recommend installing the JDK under the default `/Program Files/` folder if possible. Otherwise, extra configuration is required to grant permissions to executables. For more information, see the [grant permissions (Windows)](#perms-nonwindows) section in this document.

> [!Note]
> Given that Java is backwards compatible, earlier versions might work, but the supported and tested version for this early CTP release is Java 8. 

<a name="install-on-linux"></a>

## Install on Linux

You can install the [database engine and the Java extension together](../../linux/sql-server-linux-setup-machine-learning.md#install-all), or add Java support to an existing instance. The following examples add the Java extension to an existing installation.  

```bash
# RedHat install commands
sudo yum install mssql-server-extensibility-java

# Ubuntu install commands
sudo apt-get install mssql-server-extensibility-java

# USE install commands
sudo zypper install mssql-server-extensibility-java
```

When you install **mssql-server-extensibility-java**, the package automatically installs JRE 8 if it is not already installed. It will also add the JVM path to an environment variable called JAVA_HOME.

After completing installation, your next step is [Configure external script execution](#configure-script-execution).

> [!Note]
> On an internet-connected device, package dependencies are downloaded and installed as part of the main package installation. For more details, including offline setup, see [Install SQL Server Machine Learning on Linux](../../linux/sql-server-linux-setup-machine-learning.md).

### Grant permissions on Linux

To provide SQL Server with permissions to execute the Java classes, you need to set permissions.

To grant read and execute access to jar files or class files, run the following **chmod** command on each class or jar file. We recommend putting your class files in a jar when you work with SQL Server. For help creating a jar, see [How to create a jar file](#create-jar).

```cmd
chmod ug+rx <MyJarFile.jar>
```
You also need to give mssql_satellite permissions on the directory or jar file to read/execute.

* If you are calling class files from SQL Server, mssql_satellite will need read/execute permissions on *every* directory in the folder hierarchy, from root down to the direct parent.

* If you are calling a jar file from SQL Server, it is enough to run the command on the jar file itself.

```cmd
chown mssql_satellite:mssql_satellite <directory>
```

```cmd
chown mssql_satellite:mssql_satellite <MyJarFile.jar>
```

<a name="install-on-windows"></a>

## Install on Windows

1. Ensure a supported version of Java is installed. For more information, see the [prerequisites](#prerequisites).

2. [Run Setup](../install/sql-machine-learning-services-windows-install.md) to install SQL Server 2019.

3. When you get to Feature Selection, choose **Machine Learning Services (in-database)**. 

   Although Java integration does not come with machine learning libraries, this is the option in setup that provides the extensibility framework. You can omit R and Python if you wish.

4. Finish the installation wizard, and then continue with the next two tasks.

### Add the JAVA_HOME variable

JAVA_HOME is an environment variable that specifies the location of the Java interpreter. In this step, create a system environment variable for it on Windows.

1. Find and copy the JDK/JRE path (for example, `C:\Program Files\Java\jdk1.8.0_201`).

    Depending on your preferred Java distribution, your location of the JDK or JRE might be different than the example path above.

2. In Control Panel, open **System and Security**, open **System**, and click **Advanced System Properties**.

3. Click **Environment Variables**.

4. Create a new system variable for `JAVA_HOME` with the value of the JDK/JRE path (found in step 1).

5. Restart [Launchpad](../concepts/extensibility-framework.md#launchpad).

    1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

    2. Under SQL Server Services, right-click SQL Server Launchpad and select **Restart**.

<a name="perms-nonwindows"></a>

### Grant access to non-default JDK folder (Windows only)

You can skip this step if you installed the JDK/JRE in the default folder. 

For a non-default folder installation, run the **icacls** commands from an *elevated* line to grant access to the **SQLRUsergroup** and SQL Server service accounts (in **ALL_APPLICATION_PACKAGES**) for accessing the JVM and the Java classpath. The commands will recursively grant access to all files and folders under the given directory path.

#### SQLRUserGroup permissions

For a named instance,  append the instance name to SQLRUsergroup (for example, `SQLRUsergroupINSTANCENAME`).

```cmd
icacls "<PATH TO CLASS or JAR FILES>" /grant "SQLRUsergroup":(OI)(CI)RX /T
```

#### AppContainer permissions

```cmd
icacls "PATH to JDK/JRE" /grant "ALL APPLICATION PACKAGES":(OI)(CI)RX /T
```

<a name="configure-script-execution"></a>

## Configure script execution

At this point, you are almost ready to run Java code on Linux or Windows. As a last step, switch to SQL Server Management Studio or another tool that runs Transact-SQL script to enable external script execution.

  ```sql
  EXEC sp_configure 'external scripts enabled', 1
  RECONFIGURE WITH OVERRIDE
-- No restart is required after this step as of SQl Server 2019
 ```

## Verify installation

To confirm the installation is operational, create and run a [sample application](java-first-sample.md) using the JDK you just installed, placing the files in the classpath you configured earlier.

## Differences in CTP 2.3

If you are already familiar with Machine Learning Services, the authorization and isolation model for extensions has changed in this release. For more information, see [Differences in a SQL Server Machine 2019 Learning Services installation](../install/sql-machine-learning-services-ver15.md).

## Limitations in CTP 2.3

* The number of values in input and output buffers cannot exceed `MAX_INT (2^31-1)` since that is the maximum number of elements that can be allocated in an array in Java.

* Output parameters in [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) are not supported in this version.

* Streaming using the sp_execute_external_script parameter @r_rowsPerRead is not supported in this CTP.

* Partitioning using the sp_execute_external_script parameter @input_data_1_partition_by_columns is not supported in this CTP.

<a name="create-jar"></a>

## How to create a jar file from class files

Navigate to the folder containing your class file and run this command:

```cmd
jar -cf <MyJar.jar> *.class
```

Make sure the path to **jar.exe** is part of the system path variable. Alternatively, specify the full path to jar which can be found under /bin in the JDK folder: `C:\Users\MyUser\Desktop\jdk1.8.0_201\bin\jar -cf <MyJar.jar> *.class`

## Next steps

+ [How to call Java in SQL Server](howto-call-java-from-sql.md)
+ [Java sample in SQL Server](java-first-sample.md)
+ [Java and SQL Server data types](java-sql-datatypes.md)