import java.sql.*;
import java.io.*;
import com.microsoft.sqlserver.jdbc.SQLServerStatement;

public class updateLargeData {
	
   public static void main(String[] args) {

      // Create a variable for the connection string.
      String connectionUrl = 
    	    "jdbc:sqlserver://localhost:1433;" +
            "databaseName=AdventureWorks;integratedSecurity=true;";

      // Declare the JDBC objects.
      Connection con = null;
      Statement stmt = null;
      ResultSet rs = null;  
      Reader reader = null;
           
      try {
          // Establish the connection.
          Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
          con = DriverManager.getConnection(connectionUrl);
         
          stmt = con.createStatement(ResultSet.TYPE_FORWARD_ONLY, ResultSet.CONCUR_UPDATABLE);

          // Since the summaries could be large, make sure that
          // the driver reads them incrementally from a database, 
          // even though a server cursor is used for the updatable result sets.
          
          // The recommended way to access the Microsoft JDBC Driver for SQL Server 
          // specific methods is to use the JDBC 4.0 Wrapper functionality. 
          // The following code statement demonstrates how to use the 
          // Statement.isWrapperFor and Statement.unwrap methods
          // to access the driver specific response buffering methods.
          
          if (stmt.isWrapperFor(com.microsoft.sqlserver.jdbc.SQLServerStatement.class)) {
              SQLServerStatement SQLstmt = 
                 stmt.unwrap(com.microsoft.sqlserver.jdbc.SQLServerStatement.class);
        	  
              SQLstmt.setResponseBuffering("adaptive");
              System.out.println("Response buffering mode has been set to " +
                 SQLstmt.getResponseBuffering());
          }

          // Select all of the document summaries.
          rs = stmt.executeQuery("SELECT Title, DocumentSummary FROM Production.Document");

          // Update each document summary.
          while (rs.next()) {

               // Retrieve the original document summary.
               reader = rs.getCharacterStream("DocumentSummary");

               if (reader == null)
               {
                   // Update the document summary.
                   System.out.println("Updating " + rs.getString("Title"));
                   rs.updateString("DocumentSummary", "Work in progress");
                   rs.updateRow();
               }
               else
               {
            	   // Do something with the chunk of the data that was                   
                   // read.
                   System.out.println("reading " + rs.getString("Title"));
                   reader.close();
                   reader = null;
               }
          }
      }
      // Handle any errors that may have occurred.
      catch (Exception e) {
         e.printStackTrace();
      }
      finally {
          if (reader != null) try { reader.close(); } catch(Exception e) {}
          if (rs != null) try { rs.close(); } catch(Exception e) {}
          if (stmt != null) try { stmt.close(); } catch(Exception e) {}
    	  if (con != null) try { con.close(); } catch(Exception e) {}
      }
   }
}