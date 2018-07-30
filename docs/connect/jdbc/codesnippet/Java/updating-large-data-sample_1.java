import java.io.Reader;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerStatement;


public class UpdateLargeData {

    public static void main(String[] args) {

    	// Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";

        // Establish the connection.
        try (Connection con = DriverManager.getConnection(connectionUrl); Statement stmt = con.createStatement();
                Statement stmt1 = con.createStatement(ResultSet.TYPE_FORWARD_ONLY, ResultSet.CONCUR_UPDATABLE);) {

            createTable(stmt);

            // Since the summaries could be large, we should make sure that
            // the driver reads them incrementally from a database,
            // even though a server cursor is used for the updatable result sets.

            // The recommended way to access the Microsoft JDBC Driver for SQL Server
            // specific methods is to use the JDBC 4.0 Wrapper functionality.
            // The following code statement demonstrates how to use the
            // Statement.isWrapperFor and Statement.unwrap methods
            // to access the driver specific response buffering methods.

            if (stmt.isWrapperFor(com.microsoft.sqlserver.jdbc.SQLServerStatement.class)) {
                SQLServerStatement SQLstmt = stmt.unwrap(com.microsoft.sqlserver.jdbc.SQLServerStatement.class);

                SQLstmt.setResponseBuffering("adaptive");
                System.out.println("Response buffering mode has been set to " + SQLstmt.getResponseBuffering());
            }

            // Select all of the document summaries.
            try (ResultSet rs = stmt1.executeQuery("SELECT Title, DocumentSummary FROM Document_JDBC_Sample")) {

                // Update each document summary.
                while (rs.next()) {

                    // Retrieve the original document summary.
                    try (Reader reader = rs.getCharacterStream("DocumentSummary")) {

                        if (reader == null) {
                            // Update the document summary.
                            System.out.println("Updating " + rs.getString("Title"));
                            rs.updateString("DocumentSummary", "Work in progress");
                            rs.updateRow();
                        }
                    }
                }
            }
        }
        // Handle any errors that may have occurred.
        catch (Exception e) {
            e.printStackTrace();
        }
    }

    private static void createTable(Statement stmt) throws SQLException {
        stmt.execute("if exists (select * from sys.objects where name = 'Document_JDBC_Sample')"
                + "drop table Document_JDBC_Sample");

        String sql = "CREATE TABLE Document_JDBC_Sample (" + "[DocumentID] [int] NOT NULL identity,"
                + "[Title] [char](50) NOT NULL," + "[DocumentSummary] [varchar](max) NULL)";

        stmt.execute(sql);

        sql = "INSERT Document_JDBC_Sample VALUES ('title1','summary1') ";
        stmt.execute(sql);

        sql = "INSERT Document_JDBC_Sample (title) VALUES ('title2') ";
        stmt.execute(sql);

        sql = "INSERT Document_JDBC_Sample (title) VALUES ('title3') ";
        stmt.execute(sql);

        sql = "INSERT Document_JDBC_Sample VALUES ('title4','summary3') ";
        stmt.execute(sql);
    }
}
