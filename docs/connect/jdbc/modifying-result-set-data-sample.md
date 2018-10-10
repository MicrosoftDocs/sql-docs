---
title: "Modifying Result Set Data Sample | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: b5ae54dc-2a79-4664-bb21-cacdb7d745e1
author: MightyPen
ms.author: genemi
manager: craigg
---

# Modifying Result Set Data Sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to retrieve an updatable set of data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Then, using methods of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object, it inserts, modifies, and then finally deletes a row of data from the set of data.

The code file for this sample is named UpdateResultSet.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\resultsets
```

## Requirements

To run this sample application, you must set the classpath to include the mssql-jdbc jar file. You'll also need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database. For more information about how to set the classpath, see [Using the JDBC Driver](../../connect/jdbc/using-the-jdbc-driver.md).

> [!NOTE]  
> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](../../connect/jdbc/system-requirements-for-the-jdbc-driver.md).

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database. Then, using an SQL statement with the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object, it runs the SQL statement and places the data that it returns into an updatable SQLServerResultSet object.

Next, the sample code uses the [moveToInsertRow](../../connect/jdbc/reference/movetoinsertrow-method-sqlserverresultset.md) method to move the result set cursor to the insert row, uses a series of [updateString](../../connect/jdbc/reference/updatestring-method-sqlserverresultset.md) methods to insert data into the new row, and then calls the [insertRow](../../connect/jdbc/reference/insertrow-method-sqlserverresultset.md) method to persist the new row of data back to the database.

After inserting the new row of data, the sample code uses an SQL statement to retrieve the previously inserted row, and then uses the combination of updateString and [updateRow](../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md) methods to update the row of data and again persist it back to the database.

Finally, the sample code retrieves the previously updated row of data and then deletes it from the database using the [deleteRow](../../connect/jdbc/reference/deleterow-method-sqlserverresultset.md) method.

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class UpdateResultSet {

    public static void main(String[] args) {

        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";

        try (Connection con = DriverManager.getConnection(connectionUrl);
                Statement stmt = con.createStatement(ResultSet.TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE);) {

            // Create and execute an SQL statement, retrieving an updateable result set.
            String SQL = "SELECT * FROM HumanResources.Department;";
            ResultSet rs = stmt.executeQuery(SQL);

            // Insert a row of data.
            rs.moveToInsertRow();
            rs.updateString("Name", "Accounting");
            rs.updateString("GroupName", "Executive General and Administration");
            rs.updateString("ModifiedDate", "08/01/2006");
            rs.insertRow();

            // Retrieve the inserted row of data and display it.
            SQL = "SELECT * FROM HumanResources.Department WHERE Name = 'Accounting';";
            rs = stmt.executeQuery(SQL);
            displayRow("ADDED ROW", rs);

            // Update the row of data.
            rs.first();
            rs.updateString("GroupName", "Finance");
            rs.updateRow();

            // Retrieve the updated row of data and display it.
            rs = stmt.executeQuery(SQL);
            displayRow("UPDATED ROW", rs);

            // Delete the row of data.
            rs.first();
            rs.deleteRow();
            System.out.println("ROW DELETED");
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
            System.out.println(rs.getString("Name") + " : " + rs.getString("GroupName"));
            System.out.println();
        }
    }
}
```

## See Also

[Working with Result Sets](../../connect/jdbc/working-with-result-sets.md)
