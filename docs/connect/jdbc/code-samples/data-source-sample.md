---
title: "Data Source Sample | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: b4a933ee-f2c6-4e0d-a96d-6dd061abf759
caps.latest.revision: 25
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Data Source Sample
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  This [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] sample application demonstrates how to connect to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] database by using a data source object. It also demonstrates how to retrieve data from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] database by using a stored procedure.  
  
 The code file for this sample is named connectDS.java, and it can be found in the following location:  
  
 \<*installation directory*>\sqljdbc_\<*version*>\\<*language*>\samples\connections  
  
## Requirements  
 To run this sample application, you must set the classpath to include the sqljdbc.jar file or sqljdbc4.jar file. If the classpath is missing an entry for sqljdbc.jar or sqljdbc4.jar, the sample application will throw the common "Class not found" exception. You will also need access to the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database. For more information about how to set the classpath, see [Using the JDBC Driver](../../../connect/jdbc/using-the-jdbc-driver.md).  
  
> [!NOTE]  
>  The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] provides sqljdbc.jar and sqljdbc4.jar class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](../../../connect/jdbc/system-requirements-for-the-jdbc-driver.md).  
  
## Example  
 In the following example, the sample code sets various connection properties by using setter methods of the [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) object, and then calls the [getConnection](../../../connect/jdbc/reference/getconnection-method-sqlserverdatasource.md) method of the SQLServerDataSource object to return a [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
 Next, the sample code uses the [prepareCall](../../../connect/jdbc/reference/preparecall-method-sqlserverconnection.md) method of the SQLServerConnection object to create a [SQLServerCallableStatement](../../../connect/jdbc/reference/sqlservercallablestatement-class.md) object, and then the [executeQuery](../../../connect/jdbc/reference/executequery-method-sqlserverpreparedstatement.md) method is called to execute the stored procedure.  
  
 Finally, the sample uses the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object returned from the executeQuery method to iterate through the results returned by the stored procedure.  
  
```java
import java.sql.*;  
import com.microsoft.sqlserver.jdbc.*;  
  
public class connectDS {  
  
   public static void main(String[] args) {  
  
      // Declare the JDBC objects.  
      Connection con = null;  
      CallableStatement cstmt = null;  
      ResultSet rs = null;  
  
      try {  
         // Establish the connection.   
         SQLServerDataSource ds = new SQLServerDataSource();  
         ds.setUser("UserName");  
         ds.setPassword("*****");  
         ds.setServerName("localhost");  
         ds.setPortNumber(1433);   
         ds.setDatabaseName("AdventureWorks");  
         con = ds.getConnection();  
  
         // Execute a stored procedure that returns some data.  
         cstmt = con.prepareCall("{call dbo.uspGetEmployeeManagers(?)}");  
         cstmt.setInt(1, 50);  
         rs = cstmt.executeQuery();  
  
         // Iterate through the data in the result set and display it.  
         while (rs.next()) {  
            System.out.println("EMPLOYEE: " + rs.getString("LastName") +   
               ", " + rs.getString("FirstName"));  
            System.out.println("MANAGER: " + rs.getString("ManagerLastName") +   
               ", " + rs.getString("ManagerFirstName"));  
            System.out.println();  
         }  
      }  
  
      // Handle any errors that may have occurred.  
      catch (Exception e) {  
         e.printStackTrace();  
      }  
      finally {  
         if (rs != null) try { rs.close(); } catch(Exception e) {}  
         if (cstmt != null) try { cstmt.close(); } catch(Exception e) {}  
         if (con != null) try { con.close(); } catch(Exception e) {}  
         System.exit(1);  
      }  
   }  
}  
```  
  
## See Also  
 [Connecting and Retrieving Data](../../../connect/jdbc/connecting-and-retrieving-data.md)  
  
  