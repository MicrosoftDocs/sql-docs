---
title: How to call Java from SQL - SQL Server Machine Learning Services
description: Learn how to call Java classes from SQL Server stored procedures using the Java programming language extension in SQL Server 2019.
ms.prod: sql
ms.technology: machine-learning

ms.date: 03/19/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# How to call Java from SQL Server 2019 preview

When using the [Java language extension](extension-java.md), the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) system stored procedure is the interface used to call the Java runtime. Permissions on the database apply to Java code execution.

This article explains implementation details for Java classes and methods that execute on SQL Server. Once you are familiar with these details, review the [Java sample](java-first-sample.md) as your next step.

There are two methods for calling Java classes in SQL Server:

1. Place .class or .jar files in your [Java classpath](#classpath). This is available for both Windows and Linux.

2. Upload compiled classes in a .jar file and other dependencies into the database using the [external library](#external-library) DDL. This option is available for Windows and Linux in CTP 2.4.

> [!NOTE]
> As a general recommendation, use .jar files and not individual .class files. This is common practice in Java and will make the overall experience easier. See also: [How to create a jar file from class files](extension-java.md#create-jar).

<a name="classpath"></a>

## Classpath

### Basic principles

* Compiled custom Java classes must exist in .class files or .jar files in your Java classpath. The [CLASSPATH parameter](#set-classpath) provides the path to the compiled Java files. 

* The Java method you are calling must be provided in the "script" parameter on the stored procedure.

* If the class belongs to a package, the "packageName" needs to be provided.

* "params" is used to pass parameters to a Java class. Calling a method that requires arguments is not supported, which makes parameters the only way to pass argument values to your method. 

> [!Note]
> This note restates supported and unsupported operations specific to Java in CTP 2.x.
> * On the stored procedure, input parameters are supported. Output parameters are not.
> * Streaming using the sp_execute_external_script parameter @r_rowsPerRead is not supported.
> * Partitioning using @input_data_1_partition_by_columns is not supported.
> * Parallel processing using @parallel=1 is supported.

### Call class

Applicable to both Windows and Linux, the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) system stored procedure is the interface used to call the Java runtime. The following example shows an sp_execute_external_script using the Java extension, and parameters for specifying path, script, and your custom code.

```sql
DECLARE @myClassPath nvarchar(30)
DECLARE @param1 int

SET @myClassPath = N'/<my path>/program.jar'
SET @param1 = 3

EXEC sp_execute_external_script
  @language = N'Java'
, @script = N'<packageName>.<ClassName>.<methodName>'
, @input_data_1 = N'<Input Query>'
, @params = N'@CLASSPATH nvarchar(30), @param1 INT'
, @CLASSPATH = @myClassPath
, @param1 = @param1
```

<a name="set-classpath"></a>

### Set CLASSPATH

Once you have compiled your Java class or classes and placed the .class file(s) or .jar files in your Java classpath, you have two options for providing the classpath to the SQL Server Java extension:

**Option 1: Pass as a parameter**

One approach for specifying a path to compiled code is by setting CLASSPATH as an input parameter to the sp_execute_external_script procedure. The [Java sample](java-first-sample.md#call-method) demonstrates this technique. If you choose this approach, and have multiple paths, be sure to use the path separator that is valid for the underlying operating system:

* On Linux, separate the paths in the CLASSPATH with colon ":".
* On Windows, separate the paths in CLASSPATH with a semi-colon ";"

**Option 2: Register a system variable**

Just as you created a system variable for the JDK executables, you can create a system variable for code paths. To do this, created a system environment variable called "CLASSPATH"

<a name="external-library"></a>

## External library

In SQL Server 2019 CTP 2.4, you can use external libraries for the Java language on Windows and Linux. The same functionality will be available on Linux in an upcoming CTP. You can compile your classes into a .jar file and upload the .jar file and other dependencies into the database using the [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) DDL.

Example of how to upload a .jar file with external library:

```sql 
CREATE EXTERNAL LIBRARY myJar
FROM (CONTENT = '<local path to .jar file>') 
WITH (LANGUAGE = 'Java'); 
GO
```

By creating an external library, you don't need to provide a [classpath](#classpath) in the sp_execute_external_script call. SQL Server will automatically have access to the Java classes and you do not need to set any special permissions to the classpath.

Example of calling a method in a class from a package uploaded as an external library:

```sql
EXEC sp_execute_external_script
  @language = N'Java'
, @script = N'MyPackage.MyCLass.myMethod'
, @input_data_1 = N'SELECT * FROM MYTABLE'
with result sets ((column1 int))
```

For more information, see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql).

## Class requirements

In order for SQL Server to communicate with the Java runtime, you need to implement specific static variables in your class. SQL Server can then execute a method in the Java class and exchange data using the Java language extension.

> [!Note]
> Expect the implementation details to change in upcoming CTPs as we work to improve the experience for developers.

## Method requirements
To pass arguments, use the @param parameter in sp_execute_external_script. The method itself cannot have any arguments. The return type must be void.  

```java
public static void test()  {}
```

## Data inputs 

This section explains how to push data to Java from a SQL Server query using **InputDataSet** in sp_execute_external_script.

For every input column your SQL query pushes to Java, you need to declare an array.

### inputDataCol

In the current version of the Java extension, the **inputDataColN** variable is required, where *N* is the column number. 

```java
public static <type>[] inputDataColN = new <type>[1]
```

These arrays have to be initialized (the size of the array needs to be greater than 0, and does not have to reflect the actual length of the column).

Example: `public static int[] inputDataCol1 = new int[1];`

These array variables will be populated with input data from a SQL server query before execution of the Java program you are calling.

### inputNullMap

Null map is used by the extension to know which values are null. This variable will be populated with information about null values by SQL Server before execution of the user function.

The user only needs to initialize this variable (and the size of the array needs to be greater than 0).

```java
public static boolean[][] inputNullMap = new boolean[1][1];
```

## Data outputs

This section describes **OutputDataSet**, the output data sets returned from Java, which you can send to and persist in SQL Server.

> [!Note]
> Output parameters in [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) are not supported in this version.

### outputDataColN

Similar to **inputDataSet**, for every output column your Java program sends back to SQL Server, you must declare an array variable. All **outputDataCol** arrays should have the same length. You need to make sure this is initialized by the time the class execution finishes.

```java
public static <type>[] outputDataColN = new <type>[]
```

### numberofOutputCols

Set this variable to the number of output data columns you expect to have when the user function finishes execution.

```java
public static short numberofOutputCols = <expected number of output columns>;
```

### outputNullMap

Null map is used by the extension to indicate which values are null. We require this since primitive types don't support null. Currently, we also require the null map for String types, even though Strings can be null. Null values are indicated by "true".

This NullMap must be populated with the expected number of columns and rows you are returning to SQL Server.

```java
public static boolean[][] outputNullMap
```

## Next steps

+ [Java sample in SQL Server](java-first-sample.md)
+ [Java and SQL Server data types](java-sql-datatypes.md)
+ [Java language extension in SQL Server](extension-java.md)
