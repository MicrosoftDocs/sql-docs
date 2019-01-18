public static void executeUpdateStatement(Connection con) {
    try(Statement stmt = con.createStatement();) {
        String SQL = "INSERT INTO TestTable (Col2, Col3) VALUES ('a', 10)";
        int count = stmt.executeUpdate(SQL);
        System.out.println("ROWS AFFECTED: " + count);
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
