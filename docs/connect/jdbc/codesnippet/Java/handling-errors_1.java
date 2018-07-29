public static void executeSQLException(Connection con) {
    try (Statement stmt = con.createStatement();) {
        String SQL = "SELECT TOP 10 * Person.Contact";
        ResultSet rs = stmt.executeQuery(SQL);

        while (rs.next()) {
            System.out.println(rs.getString("FirstName") + " " + rs.getString("LastName"));
        }
    }
    catch (SQLException se) {
        do {
            System.out.println("SQL STATE: " + se.getSQLState());
            System.out.println("ERROR CODE: " + se.getErrorCode());
            System.out.println("MESSAGE: " + se.getMessage());
            System.out.println();
            se = se.getNextException();
        }
        while (se != null);
    }
}
