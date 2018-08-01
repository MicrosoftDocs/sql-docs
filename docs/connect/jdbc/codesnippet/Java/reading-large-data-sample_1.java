import java.io.IOException;
import java.io.Reader;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerStatement;

public class ReadLargeData {

    public static void main(String[] args) {
        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";
        
        // Create test data as an example.
        StringBuffer buffer = new StringBuffer(4000);
        for (int i = 0; i < 4000; i++)
            buffer.append((char) ('A'));
        
        try (Connection con = DriverManager.getConnection(connectionUrl);
                Statement stmt = con.createStatement();
                PreparedStatement pstmt = con.prepareStatement("UPDATE Production.Document SET DocumentSummary = ? WHERE (DocumentID = 1)");) {

            pstmt.setString(1, buffer.toString());
            pstmt.executeUpdate();

            // In adaptive mode, the application does not have to use a server cursor
            // to avoid OutOfMemoryError when the SELECT statement produces very large
            // results.

            // Create and execute an SQL statement that returns some data.
            String SQL = "SELECT Title, DocumentSummary FROM Production.Document";

            // Display the response buffering mode.
            SQLServerStatement SQLstmt = (SQLServerStatement) stmt;
            System.out.println("Response buffering mode is: " + SQLstmt.getResponseBuffering());
            SQLstmt.close();

            // Get the updated data from the database and display it.
            ResultSet rs = stmt.executeQuery(SQL);

            while (rs.next()) {
                Reader reader = rs.getCharacterStream(2);
                if (reader != null) {
                    char output[] = new char[40];
                    while (reader.read(output) != -1) {
                        // Do something with the chunk of the data that was
                        // read.
                    }

                    System.out.println(rs.getString(1) + " has been accessed for the summary column.");
                    // Close the stream.
                    reader.close();
                }
            }
        }
        // Handle any errors that may have occurred.
        catch (SQLException | IOException e) {
            e.printStackTrace();
        }
    }
}
