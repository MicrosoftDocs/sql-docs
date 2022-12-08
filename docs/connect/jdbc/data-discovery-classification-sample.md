---
title: SQL Data Discovery and Classification Sample
description: This Microsoft JDBC Driver for SQL Server sample application demonstrates how to use a result set to retrieve Data Discovery and Classification information.
author: David-Engel
ms.author: v-davidengel
ms.date: 07/31/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# SQL Data Discovery and Classification JDBC Sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to use result set getter methods to retrieve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 'SQL Data Discovery and Classification information' from the tables holding such information.

The code file for this sample is named DataDiscoveryAndClassification.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\dataclassification
```

## Requirements

To run this sample application, you must set the classpath to include the mssql-jdbc jar file. You'll also need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. For more information about how to set the classpath, see [Using the JDBC Driver](using-the-jdbc-driver.md).

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerResultSet;
import com.microsoft.sqlserver.jdbc.dataclassification.SensitivityProperty;


public class DataDiscoveryAndClassification {

    private static boolean featureSupported = false;

    public static void main(String[] args) {

        // Provides table name to be used for running test.
        String tableName = "JDBC_SQL_DATA_DISCOVERY_CLASSIFICATION";

        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;encrypt=true;databaseName=<database>;username=<user>;password=<password>;";

        // Establish the connection.
        try (Connection con = DriverManager.getConnection(connectionUrl); Statement stmt = con.createStatement()) {
            verifySupportability(stmt);
            if (featureSupported) {
                createTable(stmt, tableName);
                runTests(stmt, tableName);
                drop_table(stmt, tableName);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Verifies if SQL Discovery and Classification feature is applicable on target server.
     * 
     * @param stmt
     *        Statement object to work with
     */
    private static void verifySupportability(Statement stmt) {
        try {
            stmt.execute("SELECT * FROM SYS.SENSITIVITY_CLASSIFICATIONS");
            featureSupported = true;
        } catch (SQLException e) {
            // Error Code 208 : Object Not Found
            if (e.getErrorCode() == 208) {
                featureSupported = false;
                System.err.println("This feature is not supported on the target SQL Server.");
            }
        }
    }

    /**
     * Creates table for the test and sets tags for Sensitivity Classification
     * 
     * @param stmt
     *        Statement to work with
     * @param tableName
     *        Table to be created
     * @throws SQLException
     *         If an exception occurs
     */
    private static void createTable(Statement stmt, String tableName) throws SQLException {
        // Creates table for storing Supplier data
        stmt.execute("CREATE TABLE " + tableName + " (" + "[Id] [int] IDENTITY(1,1) NOT NULL,"
                + "[CompanyName] [nvarchar](40) NOT NULL," + "[ContactName] [nvarchar](50) NULL,"
                + "[ContactTitle] [nvarchar](40) NULL," + "[City] [nvarchar](40) NULL,"
                + "[Country] [nvarchar](40) NULL," + "[Phone] [nvarchar](30) MASKED WITH (FUNCTION = 'default()') NULL,"
                + "[Fax] [nvarchar](30) MASKED WITH (FUNCTION = 'default()') NULL," + "CONSTRAINT [PK_" + tableName
                + "] PRIMARY KEY CLUSTERED" + "([Id] ASC "
                + ")WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]" + ") ON [PRIMARY]");

        // Set Sensitivity Classification tags to table columns
        stmt.execute("ADD SENSITIVITY CLASSIFICATION TO " + tableName
                + ".CompanyName WITH (LABEL='PII', LABEL_ID='L1', INFORMATION_TYPE='Company name', INFORMATION_TYPE_ID='COMPANY', RANK='LOW')");
        stmt.execute("ADD SENSITIVITY CLASSIFICATION TO " + tableName
                + ".ContactName WITH (LABEL='PII', LABEL_ID='L1', INFORMATION_TYPE='Person name', INFORMATION_TYPE_ID='NAME', RANK='MEDIUM')");
        stmt.execute("ADD SENSITIVITY CLASSIFICATION TO " + tableName
                + ".Phone WITH (LABEL='PII', LABEL_ID='L1', INFORMATION_TYPE='Contact Information', INFORMATION_TYPE_ID='CONTACT', RANK='HIGH')");
        stmt.execute("ADD SENSITIVITY CLASSIFICATION TO " + tableName
                + ".Fax WITH (LABEL='PII', LABEL_ID='L1', INFORMATION_TYPE='Contact Information', INFORMATION_TYPE_ID='CONTACT', RANK='CRITICAL')");
    }

    /**
     * Runs query to fetch ResultSet from target table
     * 
     * @param stmt
     *        Statement to work with
     * @param tableName
     *        Name of table to fetch results from
     * @throws SQLException
     *         If an exception occurs
     */
    private static void runTests(Statement stmt, String tableName) throws SQLException {
        String query = "SELECT * FROM " + tableName;
        try (SQLServerResultSet rs = (SQLServerResultSet) stmt.executeQuery(query)) {
            printSensitivityClassification(rs);
        }
    }

    /**
     * Prints Sensitivity Classification data as received in ResultSet
     * 
     * @param rs
     *        Active ResultSet to read data from
     * @throws SQLException
     *         If an exception occurs
     */
    private static void printSensitivityClassification(SQLServerResultSet rs) throws SQLException {
        if (null != rs.getSensitivityClassification()) {
            for (int columnPos = 0; columnPos < rs.getSensitivityClassification().getColumnSensitivities().size();
                    columnPos++) {
                for (SensitivityProperty sp : rs.getSensitivityClassification().getColumnSensitivities().get(columnPos)
                        .getSensitivityProperties()) {
                    if (sp.getLabel() != null) {
                        System.out.println("Labels received for Column : " + columnPos);
                        System.out.println("Label ID: " + sp.getLabel().getId());
                        System.out.println("Label Name: " + sp.getLabel().getName());
                        System.out.println();
                    }

                    if (sp.getInformationType() != null) {
                        System.out.println("Information Types received for Column : " + columnPos);
                        System.out.println("Information Type ID: " + sp.getInformationType().getId());
                        System.out.println("Information Type Name: " + sp.getInformationType().getName());
                        System.out.println();
                    }
                    
                    System.out.println("Rank: " + sp.getSensitivityRank());
                }
                
                System.out.println("Rank: " + rs.getSensitivityClassification.getSensitivityRank());
            }
        }
    }

    /**
     * Drops the table created for test
     * 
     * @param stmt
     *        Statement to work with
     * @param tableName
     *        Table Name to be used
     * @throws SQLException
     *         If an exception occurs
     */
    private static void drop_table(Statement stmt, String tableName) throws SQLException {
        stmt.execute("DROP TABLE " + tableName);
    }
}
```

## See also

[Sample JDBC driver applications](sample-jdbc-driver-applications.md)
