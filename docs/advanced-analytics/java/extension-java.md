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

The extensibility framework is an architecture for executing external code: Java (starting in SQL Server 2019), [Python (starting in SQL Server 2017)](../concepts/extension-python.md), and [R (starting in SQL Server 2016)](../concepts/extension-r.md). Code execution is isolated from core engine processes, but fully integrated with SQL Server query execution. This means that you can push data from any SQL Server query to the external runtime.

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

## 2 - Post-install configuration (required)

Using SQL Server Management Studio or another tool that runs Transact-SQL script, configure external script execution on the database engine instance.

  ```sql
  EXEC sp_configure 'external scripts enabled', 1
  RECONFIGURE WITH OVERRIDE
-- No restart is required after this step as of SQl Server 2019
 ```

## 3 - Bring your own Java

One difference from previous language integrations such as R and Python is that you can choose which JVM to use with SQL Server.

### Java version support in this CTP release

| Java version | Operating system |
|--------------|------------------|
| [Java 1.10](http://jdk.java.net/10/)   | Windows |
| Java 1.8   | Linux | 

Given that Java is backwards compatible, earlier versions might work, but the supported and tested versions are listed in the table. 

## Install Java SDK (JDK) on Windows

Download Windows version of the [Java SE Development Kit](http://www.oracle.com/technetwork/java/javase/downloads/jdk10-downloads-4416644.html).

Install Java SDK under the default /Program File/ folder if you want to avoid having to grant read permission to **ALL APPLICATION PACKAGES** and the **SQLRUserGroup** security groups on an alternate location. The same guidance applies for access to your Java classpath folders, where you keep your .class or .jar files. To learn more about authorization and isolation in this release, see [Differences in a SQL Server Machine 2019 Learning Services installation](../install/sql-machine-learning-services-ver15.md).

### Grant access to non-default JDK folder (Windows only)

You can skip this step if you installed the JDK in the default folder.

For a non-default folder installation, run the following PowerShell scripts to grant access to the **SQLRUsergroup** and SQL Server service accounts (in ALL_APPLICATION_PACKAGES) for accessing the JVM and the Java classpath.

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

### Add the Java SDK path to JAVA_HOME
You also need to add the JDK installation path (for example, "C:\Program Files\Java\jdk-10.0.2") to a system environment variable that you name "JAVA_HOME".

![Environment variable for Java Home](../media/java/env-variable-java-home.png "Setup for Java")

## Install Java SDK (JDK) on Linux

On Linux, the mssql-server-extensibility-java package automatically installs OpenJDK 1.8 if it is not already installed. It will also add the JVM path to an environment variable called JAVA_HOME.

## Call Java from SQL

The [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) system stored procedure is the interface used to call the Java runtime. Any permissions on the database apply to Java code execution.

* The Java method to call needs to me provided in the "script" parameter. 

* If the class belongs to a package, the "packageName" needs to be provided.

* [CLASSPATH parameter](#set-classpath) provides the path to the compiled Java files. 

* "params" is used to pass parameters to the Java class. Calling methods that require arguments is not supported, which makes parameters the recommended way to pass argument values. *Note: Only input parameters are supported in CTP2.0.*

```sql
DECLARE @myClassPath nvarchar(30)
DECLARE @param1 int

SET @myClassPath = N'/<my path>/program.jar'
SET @param1 = 3

EXEC sp_execute_external_script
  @language = N'Java'
, @script = N'<packageName>.<ClassName>.<methodName>'
, @input_data_1 = N'<Input Query>
, @params = N'@CLASSPATH nvarchar(30), @param1 INT'
, @CLASSPATH = @myClassPath
, @param1 = @param1
```

<a name="set-classpath"></a>

### Setting CLASSPATH

Once you have compiled your Java class/classes and placed the .class file(s) or .jar files in your Java classpath, you have two options on how to provide your classpath to the SQL Server Java extension:

* Option 1: Pass CLASSPATH in the call from sp_execute_external_script (see example below). If you choose this approach, and have multiple paths, path separator characters vary by operating system:

  * On Linux, separate the paths in the CLASSPATH with colon ":".
  * On Windows, separate the paths in CLASSPATH with a semi-colon ";"

* Option 2: Set CLASSPATH to a system environment variable called "CLASSPATH"

## Java implementation requirements

Before we show how you can call your Java program from SQL Server, we need to cover some implementation requirements in the Java class you will be calling.

In order for SQL Server to communicate with the Java runtime, you need to implement specific static variables in your class. SQL Server will then be able to execute a method in the Java class and exchange data using the Java language extension.

> [Note!]
> These implementation details are subject to change in upcoming CTPs. We are working to improve the experience for developers.

## Requirements for Java method signature 
Make sure that the method you want to call from SQL Server does NOT have any arguments. The return type must be void.  

```java
public static void test()  {}
```

To pass arguments, use the **@param**s parameter in sp_execute_external_script

## Push data to Java from SQL Server query - InputDataSet

In sp_execute_external_script, you can pass an InputDataSet to Java. For every input column your SQL query is pushing to Java, you need to declare an array.

### inputDataCol
In the current version of the extension, the requirement is that the variable name is inputDataColN, where N is the column number. (This is subject to change for future CTPs.)

```java
public static <type>[] inputDataColN = new <type>[1]
```

These arrays have to be initialized (the size of the array needs to be greater than 0, and does not have to reflect the actual length of the column).

Example: public static int[] inputDataCol1 = new int[1];

These array variables will be populated with input data from a SQL server query before execution of the Java program you are calling.

### inputNullMap

Null map is used by the extension to know which values are null. This variable will be populated with information about null values by SQL Server before execution of the user function.
The user only needs to initialize this variable (and the size of the array needs to be greater than 0).

```java
public static boolean[][] inputNullMap = new boolean[1][1];
```

## Return data from Java to SQL Server - OutputDataSet

### outputDataColN

Similar to inputDataSet, for every output column your Java program is sending back to SQL Server, you need to declare an array variable. All outputDataCol arrays should have the same length.

```java
public static <type>[] outputDataColN = new <type>[1]
```

### numberofOutputCols

Must be set to the number of expected output data columns by the end of execution of the user function.

```java
public static short numberofOutputCols = <expected number of output columns>;
```

### outputNullMap

Null map is used by the extension to know which values are null. We require this since primitive types don't support null. Currently, we also require the null map for String types, even though Strings can be null. Null values are indicated by “true”.

This NullMap must be populated with the expected number of columns and rows you are returning to SQL Server.

```java
public static boolean[][] outputNullMap
```

## Limitations in CTP 2.0

* The number of values in input and output buffers cannot exceed `MAX_INT (2^31-1)` since that is the maximum number of elements that can be allocated in an array in Java.

* Output parameters in sp_execute_external_script are not supported in this version.

* No LOB datatype support for Input and output data sets in this version.


## See also

[Install SQL Server Machine Learning Services on Windows](../install/sql-machine-learning-services-windows-install.md)

[Install SQL Server Machine Learning Services on Linux](../../linux/sql-server-linux-setup-machine-learning.md)