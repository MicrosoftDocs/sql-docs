---
title: SDK for Java extension
description: Description of the Microsoft Extensibility SDK for Java for Microsoft SQL Server 2019
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/18/2019
ms.topic: conceptual
author: nelgson
ms.author: negust
ms.reviewer: dphansen
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# Microsoft Extensibility SDK for Java for Microsoft SQL Server

In CTP 2.5, we are changing the way you implement a Java program that uses the Java language extension to communicate with SQL Server. This will provide a better developer experience when interacting with SQL Server from Java.

> [!Note]
> Please note that this is a big change from previous CTPs, and that any samples you had working will need to be updated to use the SDK instead.

We are introducing an extension Java SDK for SQL Server. This is an interface the Java extension uses to exchange data with SQL Server and to execute Java code from SQL Server.

## Implementation requirements

The SDK interface defines a set of requirements that need to be fulfilled for SQL Server to communicate with the Java runtime. This means that you need to follow some implementation rules in your main class. SQL Server can then execute a specific method in the Java class and exchange data using the Java language extension.

Please see this end-to-end [sample](java-first-sample.md) for an example of how the SDK can be used.

## SDK Classes

This SDK consists of three classes.

Two abstract classes that define the interface the Java extension uses to exchange data with SQL Server:

- **AbstractSqlServerExtensionExecutor**
- **AbstractSqlServerExtensionDataset**

The third class is a helper class which contains an implementation of a data set object. This is an optional class to use to make it easier to get started. You can use your own implementation of such a class.

- **PrimitiveDataset**

Below follows details and source code for each class in the Java language extension for SQL Server.

## AbstractSqlServerExtensionExecutor

This is an Abstract class containing the interface used to execute Java code by the Java language extension for SQL Server.

Your main Java class needs to inherit from this class. Inheriting from this class means that there are certain methods in the class you need to implement in your own class.

To inherit from this abstract class, you extend with the abstract class name in the class declaration:

```java
public class <MyClass> extends AbstractSqlServerExtensionExecutor {}
```
At a minimum, your main class needs to implement the execute(...) method.

### Method execute

The execute method is the method that is called from SQL Server via the Java language extension, to invoke Java code. You should view this as a key method where you include the main operations you wish to execute from SQL Server.

To pass method arguments to Java from SQL Server, use the @param parameter in sp_execute_external_script. The method **execute**** takes its arguments that way.

```java
public AbstractSqlServerExtensionDataset execute(AbstractSqlServerExtensionDataset input, LinkedHashMap<String, Object> params)  {}
```

### Method init

The init method is executed after the constructor and before execute method. Any operations that need to be performed prior to execute(...) can be done in this method.

```java
public void init(String sessionId, int taskId, int numtask)
```

### AbstractSqlServerExtensionExecutor source code

Details about other methods and variables can be found below in source code:

```java
package com.microsoft.sqlserver.javalangextension;

import com.microsoft.sqlserver.javalangextension.AbstractSqlServerExtensionDataset;
import java.lang.UnsupportedOperationException;
import java.util.LinkedHashMap;

/**
 * Abstract class containing interface used by the Java extension
 */
public abstract class AbstractSqlServerExtensionExecutor {
	/* Supported versions of the Java extension */
	public final int SQLSERVER_JAVA_LANG_EXTENSION_V1 = 1;

	/* Members used by the extension to determine application specifics */
	protected int executorExtensionVersion;
	protected String executorInputDatasetClassName;
	protected String executorOutputDatasetClassName;

	public AbstractSqlServerExtensionExecutor() { }

    /*The init method is executed after the constructor and before execute method*/
	public void init(String sessionId, int taskId, int numTasks) {
		/* Default implementation of init() is no-op */
	}

	public AbstractSqlServerExtensionDataset execute(AbstractSqlServerExtensionDataset input, LinkedHashMap<String, Object> params) {
		throw new UnsupportedOperationException("AbstractSqlServerExtensionExecutor execute() is not implemented");
	}

	public void cleanup() {
		/* Default implementation of cleanup() is no-op */
	}
}
```

### AbstractSqlServerExtensionDataset

 This is an abstract class containing an interface for handling input and output data used by the Java language extension for SQL Server.

If you wish to implement your own dataset class, you can use **PrimitiveDataset** as an example.
 
```java
package com.microsoft.sqlserver.javalangextension;

import java.lang.UnsupportedOperationException;

/*
 Abstract class containing interface for handling input and output data used by the Java extension.
*/
public class AbstractSqlServerExtensionDataset {
	/**
	 * Column metadata interfaces
	 */
	public void addColumnMetadata(int columnId, String columnName, int columnType, int precision, int scale) {
		throw new UnsupportedOperationException("addColumnMetadata is not implemented");
	}

	public int getColumnCount() {
		throw new UnsupportedOperationException("getColumnCount is not implemented");
	}

	public String getColumnName(int columnId) {
		throw new UnsupportedOperationException("getColumnName is not implemented");
	}

	public int getColumnPrecision(int columnId) {
		throw new UnsupportedOperationException("getColumnPrecision is not implemented");
	}

	public int getColumnScale(int columnId) {
		throw new UnsupportedOperationException("getColumnScale is not implemented");
	}

	public int getColumnType(int columnId) {
		throw new UnsupportedOperationException("getColumnNullMap is not implemented");
	}

	public boolean[] getColumnNullMap(int columnId) {
		throw new UnsupportedOperationException("getColumnNullMap is not implemented");
	}

	/**
	 * Adding column interfaces
	 */
	public void addIntColumn(int columnId, int[] rows, boolean[] nullMap) {
		throw new UnsupportedOperationException("addIntColumn is not implemented");
	}

	public void addBooleanColumn(int columnId, boolean[] rows, boolean[] nullMap) {
		throw new UnsupportedOperationException("addBooleanColumn is not implemented");
	}

	public void addLongColumn(int columnId, long[] rows, boolean[] nullMap) {
		throw new UnsupportedOperationException("addLongColumn is not implemented");
	}

	public void addFloatColumn(int columnId, float[] rows, boolean[] nullMap) {
		throw new UnsupportedOperationException("addFloatColumn is not implemented");
	}

	public void addDoubleColumn(int columnId, double[] rows, boolean[] nullMap) {
		throw new UnsupportedOperationException("addDoubleColumn is not implemented");
	}

	public void addShortColumn(int columnId, short[] rows, boolean[] nullMap) {
		throw new UnsupportedOperationException("addShortColumn is not implemented");
	}

	public void addStringColumn(int columnId, String[] rows) {
		throw new UnsupportedOperationException("addStringColumn is not implemented");
	}

	public void addBinaryColumn(int columnId, byte[][] rows) {
		throw new UnsupportedOperationException("addBinaryColumn is not implemented");
	}

	/**
	 * Retrieving column interfaces
	 */
	public int[] getIntColumn(int columnId) {
		throw new UnsupportedOperationException("getIntColumn is not implemented");
	}

	public long[] getLongColumn(int columnId) {
		throw new UnsupportedOperationException("getLongColumn is not implemented");
	}

	public float[] getFloatColumn(int columnId) {
		throw new UnsupportedOperationException("getFloatColumn is not implemented");
	}

	public short[] getShortColumn(int columnId) {
		throw new UnsupportedOperationException("getShortColumn is not implemented");
	}

	public boolean[] getBooleanColumn(int columnId) {
		throw new UnsupportedOperationException("getBooleanColumn is not implemented");
	}

	public double[] getDoubleColumn(int columnId) {
		throw new UnsupportedOperationException("getDoubleColumn is not implemented");
	}

	public String[] getStringColumn(int columnId) {
		throw new UnsupportedOperationException("getStringColumn is not implemented");
	}

	public byte[][] getBinaryColumn(int columnId) {
		throw new UnsupportedOperationException("getBinaryColumn is not implemented");
	}
}
```

### PrimitiveDataset

This class is an implementation of **AbstractSqlServerExtensionDataset** that stores simple types as primitives arrays. It is provided in the SDK simply as a helper class that is optional to use.

If you don't use this class, you need to implement your own class that inherits from **AbstractSqlServerExtensionDataset**.  

```java
package com.microsoft.sqlserver.javalangextension;

import com.microsoft.sqlserver.javalangextension.AbstractSqlServerExtensionDataset;
import java.lang.IllegalArgumentException;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

/**
 * Implementation of AbstractSqlServerExtensionDataset that stores
 * simple types as primitives arrays
 */
public class PrimitiveDataset extends AbstractSqlServerExtensionDataset {
	Map<Integer, String>  columnNames;
	Map<Integer, Integer> columnTypes;
	Map<Integer, Integer> columnPrecisions;
	Map<Integer, Integer> columnScales;
	Map<Integer, Object>  columns;
	Map<Integer, boolean[]> columnNullMaps;

	public PrimitiveDataset() {
		columnTypes = new HashMap<>();
		columnNames = new HashMap<>();
		columnPrecisions = new HashMap<>();
		columnScales = new HashMap<>();
		columns = new HashMap<>();
		columnNullMaps = new HashMap<>();
	}

	/**
	 * Column metadata interfaces. Metadata is stored in a hash map, using the   column ID as the key
	 */
	public void addColumnMetadata(int columnId, String columnName, int sqlType, int precision, int scale) {
		if (columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " already exists");
		}

		columnTypes.put(columnId, sqlType);
		columnNames.put(columnId, columnName);
		columnPrecisions.put(columnId, precision);
		columnScales.put(columnId, scale);
	}

	public int getColumnCount() {
		return columnTypes.size();
	}

	public String getColumnName(int columnId) {
		if (!columnNames.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return columnNames.get(columnId);
	}

	public int getColumnPrecision(int columnId) {
		if (!columnPrecisions.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return columnPrecisions.get(columnId).intValue();
	}

	public int getColumnScale(int columnId) {
		if (!columnScales.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return columnScales.get(columnId).intValue();
	}

	public int getColumnType(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return columnTypes.get(columnId).intValue();
	}

	public boolean[] getColumnNullMap(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return columnNullMaps.get(columnId);
	}

	/**
	 * Adding column data interfaces. Column data is stored in a hash map, using the column ID as the key and
	 * an array of the corresponding type representing each row. Primitives cannot be null, thus null values
	 * are represented by an additional boolean array containing a flag to indicate if that value is null.
	 * A null indicator array indicates that there are no null values in that column.
	 */
	public void addIntColumn(int columnId, int[] rows, boolean[] nullMap) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
		columnNullMaps.put(columnId, nullMap);
	}

	public void addBooleanColumn(int columnId, boolean[] rows, boolean[] nullMap) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
		columnNullMaps.put(columnId, nullMap);
	};

	public void addLongColumn(int columnId, long[] rows, boolean[] nullMap) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
		columnNullMaps.put(columnId, nullMap);
	}

	public void addFloatColumn(int columnId, float[] rows, boolean[] nullMap) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
		columnNullMaps.put(columnId, nullMap);
	}

	public void addDoubleColumn(int columnId, double[] rows, boolean[] nullMap) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
		columnNullMaps.put(columnId, nullMap);
	}

	public void addShortColumn(int columnId, short[] rows, boolean[] nullMap) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
		columnNullMaps.put(columnId, nullMap);
	}

	public void addStringColumn(int columnId, String[] rows) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
	}

	public void addBinaryColumn(int columnId, byte[][] rows) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		columns.put(columnId, rows);
	}


	/**
	 * Retrieving column data interfaces. For primitive types, calling getColumnNullMap() for the column ID
	 * will return the boolean array indicating null values.
	 */
	public int[] getIntColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (int[])columns.get(columnId);
	}

	public long[] getLongColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (long[])columns.get(columnId);
	}

	public float[] getFloatColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (float[])columns.get(columnId);
	}

	public short[] getShortColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (short[])columns.get(columnId);
	}

	public boolean[] getBooleanColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (boolean[])columns.get(columnId);
	}

	public double[] getDoubleColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (double[])columns.get(columnId);
	}

	public String[] getStringColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (String[])columns.get(columnId);
	}

	public byte[][] getBinaryColumn(int columnId) {
		if (!columnTypes.containsKey(columnId))
		{
			throw new IllegalArgumentException("Metadata for column ID #: " + columnId + " does not exists");
		}

		return (byte[][])columns.get(columnId);
	}
}
```

## See also

+ [End to end Java sample using the SDK](java-first-sample.md)
+ [How to call Java in SQL Server](howto-call-java-from-sql.md)
+ [Java extensions in SQL Server](extension-java.md)
+ [Java and SQL Server data types](java-sql-datatypes.md)
