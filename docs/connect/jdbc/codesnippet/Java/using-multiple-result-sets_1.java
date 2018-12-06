public static void executeStatement(Connection con) {
    try (Statement stmt = con.createStatement();) {
        String SQL = "SELECT TOP 10 * FROM Person.Contact; SELECT TOP 20 * FROM Person.Contact";

        boolean results = stmt.execute(SQL);
        int rsCount = 0;

        // Loop through the available result sets.
        do {
            if (results) {
                ResultSet rs = stmt.getResultSet();
                rsCount++;

                // Show data from the result set.
                System.out.println("RESULT SET #" + rsCount);
                while (rs.next()) {
                    System.out.println(rs.getString("LastName") + ", " + rs.getString("FirstName"));
                }
            }
            System.out.println();
            results = stmt.getMoreResults();
        } while (results);
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
