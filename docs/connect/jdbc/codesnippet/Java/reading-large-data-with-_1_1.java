import java.sql.*;
import java.io.*;
import com.microsoft.sqlserver.jdbc.SQLServerCallableStatement;

public class executeStoredProcedure {

    public static void main(String[] args) {
        // Create a variable for the connection string.
        String connectionUrl = 
           "jdbc:sqlserver://localhost:1433;" +
           "databaseName=AdventureWorks;integratedSecurity=true;";

        // Declare the JDBC objects.
        Connection con = null;
        Statement stmt = null;
        ResultSet rs = null;  

        try {
          // Establish the connection.
          Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
          con = DriverManager.getConnection(connectionUrl);
 
          // Create test data as an example.
          StringBuffer buffer = new StringBuffer(4000);
          for (int i = 0; i < 4000; i++) 
             buffer.append( (char) ('A'));

          PreparedStatement pstmt = con.prepareStatement(
               "UPDATE Production.Document " +
               "SET DocumentSummary = ? WHERE (DocumentID = 1)");
 
          pstmt.setString(1, buffer.toString());
          pstmt.executeUpdate();
          pstmt.close();

          // Query test data by using a stored procedure.
          CallableStatement cstmt = 
             con.prepareCall("{call dbo.GetLargeDataValue(?, ?, ?, ?)}");

          cstmt.setInt(1, 1);
          cstmt.registerOutParameter(2, java.sql.Types.INTEGER);
          cstmt.registerOutParameter(3, java.sql.Types.CHAR);
          cstmt.registerOutParameter(4, java.sql.Types.LONGVARCHAR);

          // Display the response buffering mode.
          SQLServerCallableStatement SQLcstmt = (SQLServerCallableStatement) cstmt;
          System.out.println("Response buffering mode is: " +
               SQLcstmt.getResponseBuffering());

          SQLcstmt.execute();
          System.out.println("DocumentID: " + cstmt.getInt(2));
          System.out.println("Document_Title: " + cstmt.getString(3));

          Reader reader = SQLcstmt.getCharacterStream(4);

          // If your application needs to re-read any portion of the value, 
          // it must call the mark method on the InputStream or Reader to 
          // start buffering data that is to be re-read after a subsequent
          // call to the reset method.	  	 	  
          reader.mark(4000);

          // Read the first half of data.
          char output1[] = new char[2000];
          reader.read(output1);
          String stringOutput1 = new String(output1);

          // Reset the stream.
          reader.reset();

          // Read all the data.
          char output2[] = new char[4000];
          reader.read(output2);
          String stringOutput2 = new String(output2);

          // Close the stream.
          reader.close();
      }
      // Handle any errors that may have occurred.
      catch (Exception e) {
          e.printStackTrace();
      }
      finally {
          if (rs != null) try { rs.close(); } catch(Exception e) {}
          if (stmt != null) try { stmt.close(); } catch(Exception e) {}
          if (con != null) try { con.close(); } catch(Exception e) {}
      }
   }
}