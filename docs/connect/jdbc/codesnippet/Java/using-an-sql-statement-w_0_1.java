public static void executeStatement(Connection con) {
    try(Statement stmt = con.createStatement();) {
        String SQL = "SELECT LastName, FirstName FROM Person.Contact ORDER BY LastName";
        ResultSet rs = stmt.executeQuery(SQL);

        while (rs.next()) {
            System.out.println(rs.getString("LastName") + ", " + rs.getString("FirstName"));
        }
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
