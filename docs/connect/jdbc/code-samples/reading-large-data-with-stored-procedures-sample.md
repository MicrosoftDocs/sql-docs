---
title: "Reading Large Data with Stored Procedures Sample | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 58c76635-a117-4661-8781-d6cb231c5809
author: MightyPen
ms.author: genemi
manager: craigg
---

# Reading Large Data with Stored Procedures Sample

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] sample application demonstrates how to retrieve a large OUT parameter from a stored procedure.

The code file for this sample is named ExecuteStoredProcedure.java, and can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\adaptive
```

## Requirements

To run this sample application, you'll need access to the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database. Also set the classpath to include the mssql-jdbc jar file. For more information about how to set the classpath, see [Using the JDBC Driver](../../../connect/jdbc/using-the-jdbc-driver.md).

> [!NOTE]  
> The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](../../../connect/jdbc/system-requirements-for-the-jdbc-driver.md).

Create the following stored procedure in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database:

```sql
CREATE PROCEDURE GetLargeDataValue
  (@Document_ID int,
   @Document_ID_out int OUTPUT,
   @Document_Title varchar(50) OUTPUT,  
   @Document_Summary nvarchar(max) OUTPUT)  

AS
BEGIN
   SELECT @Document_ID_out = DocumentID,
          @Document_Title = Title,  
          @Document_Summary = DocumentSummary
    FROM  Production.Document  
    WHERE DocumentID = @Document_ID  
END  
```

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] database. Next, the sample code creates sample data and updates the Production.Document table by using a parameterized query. Then, the sample code gets the adaptive buffering mode by using the [getResponseBuffering](../../../connect/jdbc/reference/getresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) class and executes the GetLargeDataValue stored procedure. Note that starting with the JDBC driver version 2.0 release, the responseBuffering connection property is set to "adaptive" by default.

Finally, the sample code displays the data returned with the OUT parameters and also demonstrates how to use the `mark` and `reset` methods on the stream to re-read any portion of the data.

[!code[JDBC#UsingAdaptiveBuffering2](../../../connect/jdbc/codesnippet/Java/reading-large-data-with-_1_1.java)]

## See Also

[Working with Large Data](../../../connect/jdbc/code-samples/working-with-large-data.md)
