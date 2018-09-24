---
title: Java language extension in SQL Server 2019 | Microsoft Docs
description: Run Java code on SQL Server 2019 using the Java language extension.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/24/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Java language extension in SQL Server 2019 

Starting in SQL Server 2019, you can run custom Java code in the [extensibility framework](../concepts/extensibility-framework.md) as an add-on to the database engine instance. 

The extensibility framework is an architecture for executing external code: Java (starting in SQL Server 2019), [Python (starting in SQL Server 2017)](../concepts/extension-python.md), and [R (starting in SQL Server 2016)](../concepts/extension-r.md). Code execution is isolated from core engine processes, but fully integrated with SQL Server query execution. This means that you can push data from any SQL Server query to the external runtime, and consume or persist results back in SQL Server.

As with any programming language extension, the system stored procedure [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) is the interface for executing pre-compiled Java code.

## 1 - Feature installation

Run SQL Server 2019 Setup on Windows or Linux to install the Java language extension. A SQL Server 2019 database engine instance is required. You cannot add Java integration to earlier versions.

On Windows, start the [Installation Wizard](../install/sql-machine-learning-services-windows-install.md). In Feature Selection, select **Machine Learning Services (in-database)**. Although Java integration does not come with machine learning libraries, this is the option in setup that provides the extensibility framework. You can omit R and Python if you wish.

On Linux, install the [database engine](https://docs.microsoft.com/sql/linux/sql-server-linux-setup), as well as the [extensibility package and Java extension package](../../linux/sql-server-linux-setup-machine-learning.md).

The following examples illustrate the syntax for each Linux operating system.

```bash
# RedHat install commands
sudo yum install mssql-server-extensibility
sudo yum install mssql-server-extensibility-java

# Ubuntu install commands
sudo apt-get install mssql-server-extensibility
sudo apt-get install mssql-server-extensibility-java

# USE install commands
sudo zypper install mssql-server-extensibility
sudo zypper install mssql-server-extensibility-java
```

## 2 - Configuration

Using SQL Server Management Studio or another tool that runs Transact-SQL script, configure external script execution on the database engine instance.

  ```sql
  EXEC sp_configure 'external scripts enabled', 1
  RECONFIGURE WITH OVERRIDE
-- No restart is required after this step as of SQl Server 2019
 ```

## 3 - Bring your own Java

One difference from previous language integrations such as R and Python is that you control which JVM is used with SQL Server.

| Java version | Operating system |
|--------------|------------------|
| [Java 1.10](http://jdk.java.net/10/)   | Windows |
| Java 1.8   | Linux | 

Given that Java is backwards compatible, earlier versions might work, but the supported and tested versions for this early CTP release are listed in the table.

> [!Note]
>To run Java with SQL Server, you technically only need the [Java Runtime Environment](http://www.oracle.com/technetwork/java/javase/downloads/jre10-downloads-4417026.html) installed (JRE). The JDK is a development kit including the Java compiler and other development related packages. If you already have a development environment and only need a Java runtime on the server machine, you can ignore the JDK installation instructions and only install JRE.

## JDK on Windows

Download Windows version of the [Java SE Development Kit (JDK)](http://www.oracle.com/technetwork/java/javase/downloads/jdk10-downloads-4416644.html).

Install the JDK under the default /Program Files/ folder if you want to avoid having to grant read permission to **ALL APPLICATION PACKAGES** and the **SQLRUserGroup** security groups on an alternate location. The same guidance applies for access to your Java classpath folders, where you keep your .class or .jar files. 

> [!Note]
> The authorization and isolation model for extensions has changed in this release. For more information, see [Differences in a SQL Server Machine 2019 Learning Services installation](../install/sql-machine-learning-services-ver15.md).

<a name="perms-nonwindows"></a>

### Grant access to non-default JDK folder (Windows only)

You can skip this step if you installed the JDK/JRE in the default folder. For a non-default folder installation, run the following PowerShell scripts to grant access to the **SQLRUsergroup** and SQL Server service accounts (in ALL_APPLICATION_PACKAGES) for accessing the JVM and the Java classpath.

#### SQLRUserGroup permissions

```powershell
$Acl = Get-Acl "<YOUR PATH TO JDK / CLASSPATH>"
$Ar = New-Object  system.security.accesscontrol.filesystemaccessrule("SQLRUsergroup","FullControl","Allow")
$Acl.SetAccessRule($Ar)
Set-Acl ""<YOUR PATH TO JDK / CLASSPATH>" $Acl 
```

#### AppContainer permissions

```powershell
$Acl = Get-Acl "<YOUR PATH TO JDK / CLASSPATH>" 
$Ar = New-Object  system.security.accesscontrol.filesystemaccessrule("ALL APPLICATION PACKAGES","FullControl","Allow") 
$Acl.SetAccessRule($Ar) 
Set-Acl "<YOUR PATH TO JDK / CLASSPATH>" $Acl 
```

### Add the JDK path to JAVA_HOME
You also need to add the JDK/JRE installation path (for example, "C:\Program Files\Java\jdk-10.0.2") to a system environment variable that you name "JAVA_HOME". 

To create a system variable, use Control Panel > System and Security > System to access **Advanced System Properties**. Click **Environment Variables** and then create a new system variable for JAVA_HOME.

![Environment variable for Java Home](../media/java/env-variable-java-home.png "Setup for Java")

## JDK on Linux

On Linux, the mssql-server-extensibility-java package automatically installs JRE 1.8 if it is not already installed. It will also add the JVM path to an environment variable called JAVA_HOME.

## Limitations in CTP 2.0

* The number of values in input and output buffers cannot exceed `MAX_INT (2^31-1)` since that is the maximum number of elements that can be allocated in an array in Java.

* Output parameters in sp_execute_external_script are not supported in this version.

* No LOB datatype support for input and output data sets in this version. See [Java and SQL Server data types](java-sql-datatypes.md) for details about which data types are supported in this CTP.

* Streaming using the sp_execute_external_script parameter @r_rowsPerRead is not supported in this CTP.

* Partitioning using the sp_execute_external_script parameter @input_data_1_partition_by_columns is not supported in this CTP.

## Next steps

+ [How to call Java in SQL Server](howto-call-java-from-sql.md)
+ [Java sample in SQL Server](java-first-sample.md)
+ [Java and SQL Server data types](java-sql-datatypes.md)