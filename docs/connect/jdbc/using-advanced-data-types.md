---
title: "Using advanced data types"
description: "Learn how to use JDBC advanced data types to convert from SQL Server data types to Java data types using the Microsoft JDBC Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using advanced data types

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] uses the JDBC advanced data types to convert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types to a format that can be understood by the Java programming language.  
  
## Remarks

The following table lists the default mappings between the advanced [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], JDBC, and Java programming language data types.  
  
|SQL Server Types|JDBC Types (java.sql.Types)|Java Language Types|  
|----------------------|-----------------------------------|-------------------------|  
|varbinary(max)<br /><br /> image|LONGVARBINARY|byte[] \(default), Blob, InputStream, String|  
|text<br /><br /> varchar(max)|LONGVARCHAR|String (default), Clob, InputStream|  
|ntext<br /><br /> nvarchar(max)|LONGVARCHAR<br /><br /> LONGNVARCHAR (Java SE 6.0)|String (default), Clob, NClob|  
|xml|LONGVARCHAR<br /><br /> SQLXML|String (default), InputStream, Clob, byte[], Blob, SQLXML|  
|Udt<sup>1</sup>|VARBINARY|String (default), byte[], InputStream|  
|sqlvariant|SQLVARIANT|Object|  
|geometry<br /><br /> geography|VARBINARY|byte[]|  


<sup>1</sup> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports sending and retrieving CLR UDTs as binary data but doesn't support manipulation of the CLR metadata.  
  
The following sections provide examples of how you can use the JDBC driver and the advanced data types.  
  
## BLOB and CLOB and NCLOB data types

The JDBC driver implements all the methods of the java.sql.Blob, java.sql.Clob, and java.sql.NClob interfaces.  
  
> [!NOTE]  
> CLOB values can be used with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] (or later) large-value data types. Specifically, CLOB types can be used with the **varchar(max)** and **nvarchar(max)** data types, BLOB types can be used with **varbinary(max)** and **image** data types, and NCLOB types can be used with **ntext** and **nvarchar(max)**.  

## Large value data types

In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], working with large-value data types required special handling. Large-value data types are those that exceed the maximum row size of 8 KB. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] introduces a max specifier for **varchar**, **nvarchar**, and **varbinary** data types to allow storage of values as large as 2^31 bytes. Table columns and [!INCLUDE[tsql](../../includes/tsql-md.md)] variables can specify **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** data types.  

The primary scenarios for working with large-value types involve retrieving them from a database, or adding them to a database. The following sections describe different approaches to accomplish these tasks.  

### Retrieving large-value types from a database

When you retrieve a non-binary large-value data type-such as the **varchar(max)** data type-from a database, one approach is to read that data as a character stream. In the following example, the [executeQuery](../../connect/jdbc/reference/executequery-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class is used to retrieve data from the database and return it as a result set. Then the [getCharacterStream](../../connect/jdbc/reference/getcharacterstream-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class is used to read the large-value data from the result set.  

```java
ResultSet rs = stmt.executeQuery("SELECT TOP 1 * FROM Test1");  
rs.next();  
Reader reader = rs.getCharacterStream(2);  
```

> [!NOTE]
> This same approach can also be used for the **text**, **ntext**, and **nvarchar(max)** data types.  

When you retrieve a binary large-value data type-such as the **varbinary(max)** data type-from a database, there are several approaches that you can take. The most efficient approach is to read the data as a binary stream, as in the following:  

```java
ResultSet rs = stmt.executeQuery("SELECT photo FROM mypics");  
rs.next();  
InputStream is = rs.getBinaryStream(2);  
```

You can also use the [getBytes](reference/getbytes-method-sqlserverresultset.md) method to read the data as a byte array, as in the following:  

```java
ResultSet rs = stmt.executeQuery("SELECT photo FROM mypics");  
rs.next();  
byte [] b = rs.getBytes(2);  
```

> [!NOTE]  
> You can also read the data as a BLOB. However, this is less efficient than the two methods shown previously.  

### Adding large-value types to a database

Uploading large data with the JDBC driver works well for the memory-sized cases, and in the larger-than-memory cases, streaming is the primary option. However, the most efficient way to upload large data is through the stream interfaces.  

Using a String or bytes is also an option, as in the following:  

```java
PreparedStatement pstmt = con.prepareStatement("INSERT INTO test1 (c1_id, c2_vcmax) VALUES (?, ?)");  
pstmt.setInt(1, 1);  
pstmt.setString(2, htmlStr);  
pstmt.executeUpdate();  
```

> [!NOTE]  
> This approach can also be used for values that are stored in **text**, **ntext**, and **nvarchar(max)** columns.  

If you have an image library on the server and must upload entire binary image files to a **varbinary(max)** column, the most efficient method with the JDBC driver is to use streams directly, as in the following:  

```java
try (PreparedStatement pstmt = con.prepareStatement("INSERT INTO test1 (Col1, Col2) VALUES(?,?)")) { 
  File inputFile = new File("CLOBFile20mb.jpg");  
  try (FileInputStream inStream = new FileInputStream(inputFile)) {
    int id = 1;  
    pstmt.setInt(1,id);  
    pstmt.setBinaryStream(2, inStream);  
    pstmt.executeUpdate();
  }
}
```

> [!NOTE]  
> Using either the CLOB or BLOB method is not an efficient way to upload large data.  

### Modifying large-value types in a database

In most cases, the recommended method for updating or modifying large values on the database is to pass parameters through the [SQLServerPreparedStatement](reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md) classes by using [!INCLUDE[tsql](../../includes/tsql-md.md)] commands like `UPDATE`, `WRITE`, and `SUBSTRING`.  

If you have to replace the instance of a word in a large text file, such as an archived HTML file, you can use a Clob object, as in the following:  

```java
String SQL = "SELECT * FROM test1;";  
try (Statement stmt = con.createStatement(ResultSet.TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE)
     ResultSet rs = stmt.executeQuery(SQL)) {
  rs.next();

  Clob clob = rs.getClob(2);  
  long pos = clob.position("dog", 1);  
  clob.setString(pos, "cat");  
  rs.updateClob(2, clob);  
  rs.updateRow();  
}
```

Additionally, you could do all the work on the server and just pass parameters to a prepared UPDATE statement.  

For more information about large-value types, see "Using Large-Value Types" in SQL Server Books Online.  

## XML data type

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides an **xml** data type that lets you store XML documents and fragments in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. The **xml** data type is a built-in data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and is in some ways similar to other built-in types, such as **int** and **varchar**. As with other built-in types, you can use the **xml** data type as a column type when you create a table; as a variable type, a parameter type, or a function-return type; or in [!INCLUDE[tsql](../../includes/tsql-md.md)] CAST and CONVERT functions.  
  
In the JDBC driver, the **xml** data type can be mapped as a String, byte array, stream, CLOB, BLOB, or SQLXML object. String is the default. Starting with the JDBC Driver version 2.0, the JDBC driver provides support for the JDBC 4.0 API, which introduces the SQLXML interface. The SQLXML interface defines methods to interact and manipulate XML data. The **SQLXML** data type maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**xml** data type. For more information about how to read and write XML data from and to the relational database with the **SQLXML** Java data type, see [Supporting XML data](../../connect/jdbc/supporting-xml-data.md).  
  
The implementation of the **xml** data type in the JDBC driver provides support for the following:  
  
- Access to the XML as a standard Java UTF-16 string for most common programming scenarios  
  
- Input of UTF-8 and other 8-bit encoded XML  
  
- Access to the XML as a byte array with a leading BOM when encoded in UTF-16 for interchange with other XML processors and disk files  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires a leading BOM for UTF-16-encoded XML. The application must provide this when XML parameter values are supplied as byte arrays. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always outputs XML values as UTF-16 strings with no BOM or embedded encoding declaration. When XML values are retrieved as byte[], BinaryStream or Blob, a UTF-16 BOM is pre-pended to the value.  
  
For more information about the **xml** data type, see "xml Data Type" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## User-defined data type  

The introduction of user-defined types (UDTs) in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] extends the SQL type system by letting you store objects and custom data structures in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. UDTs can contain multiple data types and can have behaviors, differentiating them from the traditional alias data types that consist of a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type. UDTs are defined by using any of the languages supported by the Microsoft .NET common language runtime (CLR) that produce verifiable code. This includes Microsoft Visual C# and Visual Basic .NET. The data is exposed as fields and properties of a .NET Framework-based class or structure, and behaviors are defined by methods of the class or structure.  
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a UDT can be used as the column definition of a table, as a variable in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, or as an argument of a [!INCLUDE[tsql](../../includes/tsql-md.md)] function or stored procedure.  
  
For more information about user-defined data types, see "Using and Modifying Instances of User-defined Types" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Sql_variant data type

For information about sql_variant data type, see [Using Sql_variant data type](using-sql-variant-datatype.md).  

## Spatial data types

For information about spatial data types, see [Using Spatial Datatypes](use-spatial-datatypes.md).  

## See also

[Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md)  
