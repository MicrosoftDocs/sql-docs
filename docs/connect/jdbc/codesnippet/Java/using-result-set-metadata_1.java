public static void getResultSetMetaData(Connection con) {
    try(Statement stmt = con.createStatement();) {
        String SQL = "SELECT TOP 10 * FROM Person.Contact";

        ResultSet rs = stmt.executeQuery(SQL);
        ResultSetMetaData rsmd = rs.getMetaData();

        // Display the column name and type.
        int cols = rsmd.getColumnCount();
        for (int i = 1; i <= cols; i++) {
            System.out.println("NAME: " + rsmd.getColumnName(i) + " " + "TYPE: " + rsmd.getColumnTypeName(i));
        }
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
