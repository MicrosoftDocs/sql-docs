import java.io.IOException;
import java.io.Reader;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import com.microsoft.sqlserver.jdbc.SQLServerCallableStatement;

public class ExecuteStoredProcedure {

    public static void main(String[] args) {
        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";

        // Create test data as an example.
        StringBuffer buffer = new StringBuffer(4000);
        for (int i = 0; i < 4000; i++)
            buffer.append((char) ('A'));
        
        try (Connection con = DriverManager.getConnection(connectionUrl);
                PreparedStatement pstmt = con.prepareStatement("UPDATE Production.Document SET DocumentSummary = ? WHERE (DocumentID = 1)");
                CallableStatement cstmt = con.prepareCall("{call dbo.GetLargeDataValue(?, ?, ?, ?)}");) {

            pstmt.setString(1, buffer.toString());
            pstmt.executeUpdate();

            // Query test data by using a stored procedure.

            cstmt.setInt(1, 1);
            cstmt.registerOutParameter(2, java.sql.Types.INTEGER);
            cstmt.registerOutParameter(3, java.sql.Types.CHAR);
            cstmt.registerOutParameter(4, java.sql.Types.LONGVARCHAR);

            // Display the response buffering mode.
            try (SQLServerCallableStatement SQLcstmt = (SQLServerCallableStatement) cstmt;) {
                System.out.println("Response buffering mode is: " + SQLcstmt.getResponseBuffering());

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
        }
        // Handle any errors that may have occurred.
        catch (SQLException | IOException e) {
            e.printStackTrace();
        }
    }
}
