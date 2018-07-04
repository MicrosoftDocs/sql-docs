---
title: "Retrieving Result Set Data Sample | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.suite: "sql"
ms.technology: connectivity
ms.tgt_pltfrm: ""
ms.topic: conceptual
ms.assetid: 1b190c36-3d38-49a2-8599-612329675851
caps.latest.revision: 20
author: MightyPen
ms.author: genemi
manager: craigg
---
# Retrieving Result Set Data Sample
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to retrieve a set of data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] database, and then display that data.  
  
 The code file for this sample is named RetrieveRS.java, and it can be found in the following location:  
  
 \<*installation directory*>\sqljdbc_\<*version*>\\<*language*>\samples\resultsets  
  
## Requirements  
 To run this sample application, you must set the classpath to include the mssql-jdbc jar file. You will also need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database. For more information about how to set the classpath, see [Using the JDBC Driver](../../connect/jdbc/using-the-jdbc-driver.md).  
  
> [!NOTE]  
>  The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](../../connect/jdbc/system-requirements-for-the-jdbc-driver.md).  
  
## Example  
 In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database. Then, using an SQL statement with the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object, it runs the SQL statement and places the data that it returns into a [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
 Next, the sample code calls the custom displayRow method to iterate through the rows of data that are contained in the result set, and uses the [getString](../../connect/jdbc/reference/getstring-method-sqlserverresultset.md) method to display some of the data that it contains.  
  
```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class RetrieveRS {

    public static void main(String[] args) {

        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";

        try (Connection con = DriverManager.getConnection(connectionUrl); Statement stmt = con.createStatement();) {
            String SQL = "SELECT * FROM Production.Product;";
            ResultSet rs = stmt.executeQuery(SQL);
            displayRow("PRODUCTS", rs);
        }
        // Handle any errors that may have occurred.
        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static void displayRow(String title,
            ResultSet rs) throws SQLException {
        System.out.println(title);
        while (rs.next()) {
            System.out.println(rs.getString("ProductNumber") + " : " + rs.getString("Name"));
        }
    }
}
```  
  
## See Also  
 [Working with Result Sets](../../connect/jdbc/working-with-result-sets.md)  
  
  
